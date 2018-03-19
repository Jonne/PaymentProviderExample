using Sitecore.Commerce.Plugin.Management;

namespace Plugin.Ingenico.Pipelines.Blocks
{
    public class TranslateItemsToPaymentOptionsBlock : Sitecore.Commerce.Plugin.Management.TranslateItemsToPaymentOptionsBlock
    {
        public TranslateItemsToPaymentOptionsBlock(IGetItemByIdPipeline getItemByIdPipeline) : base(getItemByIdPipeline)
        {
        }

        protected override string GetPaymentTemplateName(string paymentOptionType)
        {
            return paymentOptionType == "5" ? "Ingenico" : base.GetPaymentTemplateName(paymentOptionType);
        }
    }
}
