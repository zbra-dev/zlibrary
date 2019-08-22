using System.Runtime.Serialization;

namespace ZLibrary.Web.Controllers.Items
{
    public class FeatureSettingsDto
    {
        [DataMember(Name = "allowCoverImage")]
        public bool AllowCoverImage { get; set; }
    }
}
