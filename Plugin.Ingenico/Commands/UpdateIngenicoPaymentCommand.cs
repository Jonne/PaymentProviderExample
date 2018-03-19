// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleCommand.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Plugin.Ingenico.Commands
{
    using System;
    using System.Threading.Tasks;
    using Plugin.Ingenico.Pipelines;
    using Plugin.Ingenico.Pipelines.Arguments;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Core.Commands;

    public class UpdateIngenicoPaymentCommand : CommerceCommand
    {
        private readonly IUpdateIngenicoPaymentPipeline pipeline;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sitecore.Commerce.Plugin.Sample.SampleCommand" /> class.
        /// </summary>
        /// <param name="pipeline">
        /// The pipeline.
        /// </param>
        /// <param name="serviceProvider">The service provider</param>
        public UpdateIngenicoPaymentCommand(IUpdateIngenicoPaymentPipeline pipeline, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.pipeline = pipeline;
        }

        /// <summary>
        /// The process of the command
        /// </summary>
        /// <param name="commerceContext">
        /// The commerce context
        /// </param>
        /// <param name="parameter">
        /// The parameter for the command
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<bool> Process(CommerceContext commerceContext, string orderId, string brand, string status)
        {
            using (var activity = CommandActivity.Start(commerceContext, this))
            {
                var arg = new UpdateIngenicoPaymentArgument
                {
                    OrderId = orderId,
                    Brand = brand,
                    Status = status
                };

                return await pipeline.Run(arg, new CommercePipelineExecutionContextOptions(commerceContext));
            }
        }
    }
}