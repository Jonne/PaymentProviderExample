using Plugin.Ingenico.Components;
using Plugin.Ingenico.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Commerce.Plugin.Sample;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Ingenico.Pipelines.Blocks
{
    [PipelineDisplayName("Plugin.Ingenico.UpdateOgonePaymentStatusBlock")]
    public class UpdateIngenicoPaymentBlock : PipelineBlock<UpdateIngenicoPaymentArgument, bool, CommercePipelineExecutionContext>
    {
        private IGetOrderPipeline getOrderPipeline;
        private IPersistEntityPipeline persistEntityPipeline;
        private readonly IFindEntitiesInListPipeline findEntitiesInListPipeline;

        public UpdateIngenicoPaymentBlock(IGetOrderPipeline getOrderPipeline, IPersistEntityPipeline persistEntityPipeline, IFindEntitiesInListPipeline findEntitiesInListPipeline)
        {
            this.getOrderPipeline = getOrderPipeline;
            this.persistEntityPipeline = persistEntityPipeline;
            this.findEntitiesInListPipeline = findEntitiesInListPipeline;
        }
        
        public override async Task<bool> Run(UpdateIngenicoPaymentArgument arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{this.Name}: The argument cannot be null.");

            // Todo: Authenticate request


            // Fulfill pre-condition: get cart
            Order order = await getOrderPipeline.Run(arg.OrderId, context).ConfigureAwait(false);

            if (order == null)
            {
                await context.CommerceContext.AddMessage(context.CommerceContext.GetPolicy<KnownResultCodes>().Error,
                                                "EntityNotFound",
                                                new object[] { arg.OrderId },
                                                $"Entity {0} was not found.");

                return false;
            }

            var paymentStatus = order.GetComponent<IngenicoPaymentComponent>();
            paymentStatus.Brand = arg.Brand;
            paymentStatus.TransactionStatus = arg.Status;

            PersistEntityArgument result = await persistEntityPipeline.Run(new PersistEntityArgument(order), context);

            return true;
        }
    }
}
