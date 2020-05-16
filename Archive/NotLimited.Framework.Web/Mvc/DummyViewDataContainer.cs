using System.Web.Mvc;

namespace NotLimited.Framework.Web.Mvc
{
    public class DummyViewDataContainer : IViewDataContainer
    {
        public DummyViewDataContainer(ViewDataDictionary viewData)
        {
            ViewData = viewData;
        }

        public ViewDataDictionary ViewData { get; set; }
    }
}