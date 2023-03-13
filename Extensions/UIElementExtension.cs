
using EasyUI.Library;
using EasyUI.Runtime;

namespace EasyUI
{
    public static class UIElementExtension
    {
        public static UIElementType ToUIElement<UIElementType>(this string id)
            where UIElementType : UIElement
        {
            return SO_EasyUIRuntimeDataContainer.GetUIElementWithId<UIElementType>(id);
        }

        public static UIElement[] ToUIElements(this string[] id)
        {
            UIElement[] foundElements = new UIElement[id.Length];

            for (int i = 0; i < foundElements.Length; i++) 
            {
                foundElements[i] = SO_EasyUIRuntimeDataContainer.GetUIElementWithId<UIElement>(id[i]);
            }

            return foundElements;
        }
    }
}
