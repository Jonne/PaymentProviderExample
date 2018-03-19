// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandsController.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sitecore.Commerce.Plugin.Sample
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http.OData;
    using global::Plugin.Ingenico.Commands;
    using global::Plugin.Ingenico.Components;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Plugin.Carts;
    using Sitecore.Commerce.Plugin.Payments;

    /// <inheritdoc />
    /// <summary>
    /// Defines a controller
    /// </summary>
    /// <seealso cref="T:Sitecore.Commerce.Core.CommerceController" />
    public class CommandsController : CommerceController
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sitecore.Commerce.Plugin.Sample.CommandsController" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="globalEnvironment">The global environment.</param>
        public CommandsController(IServiceProvider serviceProvider, CommerceEnvironment globalEnvironment)
            : base(serviceProvider, globalEnvironment)
        {
        }

        [HttpPut]
        [Route("AddIngenicoPayment()")]
        public async Task<IActionResult> AddIngenicoPayment([FromBody] ODataActionParameters value)
        {
            CommandsController commandsController = this;
            if (!commandsController.ModelState.IsValid || value == null)
            {
                return (IActionResult)new BadRequestObjectResult(commandsController.ModelState);
            }

            if (!value.ContainsKey("cartId") || value["cartId"] == null || !value.ContainsKey("payment") || value["payment"] == null)
            {
                return (IActionResult)new BadRequestObjectResult(value);
            }

            string cartId = value["cartId"].ToString();
            IngenicoPaymentComponent paymentComponent = JsonConvert.DeserializeObject<IngenicoPaymentComponent>(value["payment"].ToString());
            AddPaymentsCommand command = commandsController.Command<AddPaymentsCommand>();
            Cart cart = await command.Process(commandsController.CurrentContext, cartId, new List<PaymentComponent>()
            {
                paymentComponent
            });
            return (IActionResult)new ObjectResult((object)command);
        }

        [HttpPut]
        [Route("UpdateIngenicoPayment()")]
        public async Task<IActionResult> UpdateIngenicoPayment([FromBody] ODataActionParameters value)
        {
            CommandsController commandsController = this;
            if (!commandsController.ModelState.IsValid || value == null)
            {
                return (IActionResult)new BadRequestObjectResult(commandsController.ModelState);
            }

            string orderId = value["orderId"].ToString();
            string brand = value["brand"].ToString();
            string status = value["status"].ToString();

            UpdateIngenicoPaymentCommand command = commandsController.Command<UpdateIngenicoPaymentCommand>();
            bool result = await command.Process(commandsController.CurrentContext, orderId, brand, status);
            return (IActionResult)new ObjectResult((object)command);
        }
    }
}

