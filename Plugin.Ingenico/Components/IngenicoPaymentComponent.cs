// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleComponent.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Plugin.Ingenico.Components
{
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Plugin.Payments;

    /// <inheritdoc />
    /// <summary>
    /// The SampleComponent.
    /// </summary>
    public class IngenicoPaymentComponent : PaymentComponent
    {
        /// <summary>
        /// Gets or sets the name of the brand used for the transaction.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the status of the transaction.
        /// </summary>
        public string TransactionStatus { get; set; }

        /// <summary>
        /// The billing address party
        /// </summary>
        public Party BillingParty { get; set; }
    }
}