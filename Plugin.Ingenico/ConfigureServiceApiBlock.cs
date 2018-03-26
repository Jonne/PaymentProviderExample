using Plugin.Ingenico.Components;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Builder;

namespace Plugin.Ingenico
{
    [PipelineDisplayName("Plugin.Ingenico:blocks:ConfigureServiceApi")]
    public class ConfigureServiceApiBlock : PipelineBlock<ODataConventionModelBuilder, ODataConventionModelBuilder, CommercePipelineExecutionContext>
    {
        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="modelBuilder">
        /// The argument.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="ODataConventionModelBuilder"/>.
        /// </returns>
        public override Task<ODataConventionModelBuilder> Run(ODataConventionModelBuilder modelBuilder, CommercePipelineExecutionContext context)
        {
            Condition.Requires(modelBuilder).IsNotNull($"{this.Name}: The argument cannot be null.");
            modelBuilder.AddEntityType(typeof(IngenicoPaymentComponent));

            var configuration = modelBuilder.Action("AddIngenicoPayment");
            configuration.Parameter<string>("cartId");
            configuration.Parameter<IngenicoPaymentComponent>("payment");
            configuration.ReturnsFromEntitySet<CommerceCommand>("Commands");

            var updateConfiguration = modelBuilder.Action("UpdateIngenicoPayment");
            updateConfiguration.Parameter<string>("orderId");
            updateConfiguration.Parameter<string>("brand");
            updateConfiguration.Parameter<string>("status");
            updateConfiguration.ReturnsFromEntitySet<CommerceCommand>("Commands");

            return Task.FromResult(modelBuilder);
        }
    }
}
