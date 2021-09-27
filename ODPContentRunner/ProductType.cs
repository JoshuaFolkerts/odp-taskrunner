using System.ComponentModel;

namespace ODPContentRunner
{
    public enum ProductType
    {
        Journey = 0,

        [Description("Content Cloud")]
        ContentCloud,

        [Description("Content & Commerce Cloud")]
        ContentCommerce
    }
}