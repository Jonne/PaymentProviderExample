// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureSitecore.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Plugin.Ingenico
{
    using System.Reflection;
    using global::Plugin.Ingenico.Pipelines;
    using global::Plugin.Ingenico.Pipelines.Blocks;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Plugin.Orders;
    using Sitecore.Commerce.Plugin.Payments;
    using Sitecore.Framework.Configuration;
    using Sitecore.Framework.Pipelines.Definitions.Extensions;

    /// <summary>
    /// The configure sitecore class.
    /// </summary>
    public class ConfigureSitecore : IConfigureSitecore
    {
        /// <summary>
        /// The configure services.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            services.Sitecore().Pipelines(config => config
            .ConfigurePipeline<IConfigureServiceApiPipeline>(configure => configure.Add<Plugin.Ingenico.ConfigureServiceApiBlock>())
            .ConfigurePipeline<IPendingOrdersMinionPipeline>(d =>
            {
                d.Add<ValidateIngenicoPaymentBlock>().After<ValidatePendingOrderBlock>();
            })
            .ConfigurePipeline<IGetPaymentMethodsPipeline>(d =>
            {
                d.Replace<Sitecore.Commerce.Plugin.Management.TranslateItemsToPaymentMethodsBlock, TranslateItemsToPaymentMethodsBlock>();
            })
            .ConfigurePipeline<IGetPaymentOptionsPipeline>(d =>
            {
                d.Replace<Sitecore.Commerce.Plugin.Management.TranslateItemsToPaymentOptionsBlock, TranslateItemsToPaymentOptionsBlock>();
            })
             .AddPipeline<IUpdateIngenicoPaymentPipeline, UpdateIngenicoPaymentPipeline>(
                    configure =>
                        {
                            configure.Add<UpdateIngenicoPaymentBlock>();
                        })
               );

            services.RegisterAllCommands(assembly);
        }
    }
}