using Sitecore.Commerce.Plugin.Management;

namespace Plugin.Ingenico.Pipelines.Blocks
{
    public class TranslateItemsToPaymentMethodsBlock : Sitecore.Commerce.Plugin.Management.TranslateItemsToPaymentMethodsBlock
    {
        public TranslateItemsToPaymentMethodsBlock(IGetItemByIdPipeline getItemByIdPipeline) : base(getItemByIdPipeline)
        {
        }

        protected override string GetPaymentTemplateName(string paymentOptionType)
        {
            return paymentOptionType == "5" ? "Ingenico" : base.GetPaymentTemplateName(paymentOptionType);
        }
    }
}
