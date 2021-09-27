using System.ComponentModel;

namespace ODPContentRunner
{
    public enum JourneyType
    {
        None = 0,

        [Description("Association Script - CMS / Forms / Experiments / Email")]
        AssociationScript,

        [Description("CMS / Search / Form / Experimentation / Email")]
        CMS,

        [Description("Commerce (multi-day) / Experiment / Form / Add To Cart / Triggered Email / In-store")]
        CommerceMultiDay,

        [Description("Commerce / Experiment / Form / Add To Cart / Triggered Email / Purchase")]
        Commerce
    }
}