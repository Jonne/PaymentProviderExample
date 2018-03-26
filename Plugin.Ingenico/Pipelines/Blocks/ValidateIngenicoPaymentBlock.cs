// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleBlock.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Plugin.Ingenico
{
    using System.Threading.Tasks;
    using global::Plugin.Ingenico.Components;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Plugin.Orders;
    
    using Sitecore.Framework.Pipelines;

    [PipelineDisplayName("Plugin.Ingenico.ValidateIngenicoPaymentBlock")]
    public class ValidateIngenicoPaymentBlock : PipelineBlock<Order, Order, CommercePipelineExecutionContext>
    {
        public override async Task<Order> Run(Order arg, CommercePipelineExecutionContext context)
        {
            if (!arg.HasComponent<IngenicoPaymentComponent>())
            {
                return arg;
            }

            var paymentComponent = arg.GetComponent<IngenicoPaymentComponent>();

            switch(paymentComponent.TransactionStatus)
            {
                case "Problem":
                    KnownOrderStatusPolicy knownOrderStatusPolicy = context.GetPolicy<KnownOrderStatusPolicy>();
                    arg.Status = knownOrderStatusPolicy.Problem;
                    await context.CommerceContext.AddMessage(context.CommerceContext.GetPolicy<KnownResultCodes>().Error,
                                                    "InvalidOrderState",
                                                    new object[] { arg.Id },
                                                    $"There was a problem with the Ingenico payment.");
                    break;
                case "Settled":
                    // The Payment has been settled, continue with the normal flow.
                    break;
                default:
                    // No payment received, abort the normal order flow.
                    context.Abort("Ogone payment has not yet been received.", context);
                    break;
            }
        
            return arg;
        }
    }
}
