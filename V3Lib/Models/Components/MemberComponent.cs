using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using V3Lib.Models.Additionals;
using V3Lib.Models.Conditions;
using V3Lib.Models.Medias;
using V3Lib.Models.Operations;
using V3Lib.Models.Reactions;
using V3Lib.Models.Styles;
using V3Lib.NewtonsoftJsonExtensions;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Models.Components
{
    [AddJsonTypeName]
    public class MemberComponent : CompositeComponent
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Description { get; set; }

        public Style Style { get; set; }

        public Additional Additional { get; set; }

        public Reaction Reaction { get; set; }

        public List<Media> Medias { get; set; } = new List<Media>();

        // public List<Operation> Operations { get; set; } = new List<Operation>();
    }
}