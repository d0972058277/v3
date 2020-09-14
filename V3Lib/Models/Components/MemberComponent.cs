using System.Collections.Generic;
using V3Lib.Models.Conditions;
using V3Lib.Models.Extensions;
using V3Lib.Models.Medias;
using V3Lib.Models.Operations;
using V3Lib.Models.Styles;

namespace V3Lib.Models.Components
{
    public class MemberComponent : Component
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Description { get; set; }

        public Style Style { get; set; }

        public Extension Extension { get; set; }

        public List<Media> Medias { get; set; }

        public List<Operation> Operations { get; set; }

        public List<MemberComponent> SubComponents { get; set; }

        public override void TrimLowerLayer()
        {
            SubComponents?.ForEach(component => component.TrimUpperLayer());
        }
    }
}