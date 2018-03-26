// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISamplePipeline.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Plugin.Ingenico.Pipelines
{
    using Plugin.Ingenico.Pipelines.Arguments;
    using Sitecore.Commerce.Core;
    using Sitecore.Framework.Pipelines;

    [PipelineDisplayName("Plugin.Ingenico.UpdateIngenicoPaymentPipeline")]
    public interface IUpdateIngenicoPaymentPipeline : IPipeline<UpdateIngenicoPaymentArgument, bool, CommercePipelineExecutionContext>
    {
    }
}
