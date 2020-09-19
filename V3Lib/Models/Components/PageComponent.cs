using System.Collections.Generic;

namespace V3Lib.Models.Components
{
    public abstract class PageComponent : CompositeComponent
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Version { get; set; }
    }
}