// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleArgument.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Plugin.Ingenico.Pipelines.Arguments
{
    using Sitecore.Commerce.Core;

    public class UpdateIngenicoPaymentArgument : PipelineArgument
    {
        public string OrderId { get; set; }

        public string Brand { get; set; }

        public string Status { get; set; }
    }
}
