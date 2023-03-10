using System.Collections.Generic;

using EasyUI.Runtime;
using EasyUI.Library;

namespace EasyUI.Page
{
    public class EasyUIPage_Search : EasyUIPage
    {
        public override List<string> OnDrawPage()
        {
            List<string> elements = new List<string>();

            BuilderUI.AddContainer<VerticalContainer>(BuilderUI.GetScreenMargins(), args: new Dictionary<string, object> 
            {
                { "size", "fit" },
                { "offsetMin", UnityEngine.Vector2.zero },
                { "offsetMax", UnityEngine.Vector2.zero },
                { "childForceExpandWidth", true },
                { "childForceExpandHeight", false },
            }, onElementCreated: container => 
            {
                elements.Add(container.Id);

                BuilderUI.AddUIElement<Label, string>("Search page", container, args: new Dictionary<string, object> 
                {
                    { "fontSize", 56f },
                    { "font", "SEGOEUIL SDF" }
                });

                BuilderUI.AddUIElement<Label, string>("Lorem inpsum", container, args: new Dictionary<string, object>
                {
                    { "fontSize", 36f },
                    { "font", "SEGOEUIL SDF" }
                });

                BuilderUI.AddUIElement<SimpleButton, System.Action>(() => { EasyUIPage.Add(new EasyUIPage_Home()); }, container, args: new Dictionary<string, object> 
                {
                    { "label", "Home" },
                    { "height", 300f }
                });
            });

            return elements;
        }
    }
}
