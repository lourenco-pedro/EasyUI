using System.Linq;

using UnityEngine;

using EasyUI.Debug;

namespace EasyUI.Library
{
    [CreateAssetMenu(menuName = "EasyUI/ResourcesLibrary", fileName = "ResourcesLibrary")]
    public class SO_ResourcesLibrary : ScriptableObject
    {

        static SO_ResourcesLibrary instance;
        

        [System.Serializable]
        struct EasyUIElementValuePair 
        {
            public string TypeName;
            public UIElement Prefab;
        }

        [System.Serializable]
        struct SpriteValuePair 
        {
            public string TypeName;
            public Sprite Sprite;
        }

        [SerializeField] EasyUIElementValuePair[] collections;
        [SerializeField] SpriteValuePair[] sprites;

        [RuntimeInitializeOnLoadMethod]
        static void SetupInstance() 
        {
            instance = Resources.Load<SO_ResourcesLibrary>("ResourcesLibrary");
            UnityEngine.Debug.Log(instance);
        }

        public static BaseUIType GetEasyUI<BaseUIType, ElementData>()
            where BaseUIType : UIElement<ElementData>
        {
            var items = instance.collections.Where(element => element.TypeName.Equals(typeof(BaseUIType).Name)).ToArray();
            
            if (items.Length == 0)
            {
                EasyUIConsole.Log("PrefabsLibrary", $"There is no UI element with such type. {typeof(BaseUIType).Name}");
                return null;    
            }
            
            return (BaseUIType)items[0].Prefab;
        }

        public static BaseUIType GetEasyUI<BaseUIType>()
            where BaseUIType : UIElement
        {
            var items = instance.collections.Where(element => element.TypeName.ToLower().Equals(typeof(BaseUIType).Name.ToLower())).ToArray();

            if (items.Length == 0)
            {
                EasyUIConsole.Log("PrefabsLibrary", $"There is no UI element with such type. {typeof(BaseUIType).Name}");
                return null;
            }

            return (BaseUIType)items[0].Prefab;
        }

        public static Sprite GetSprite(string spriteName) 
        {
            var items = instance.sprites.Where(element => element.TypeName.Equals(spriteName)).ToArray();

            if (items.Length == 0)
            {
                EasyUIConsole.Log("PrefabsLibrary", $"There is no UI element with such type. {spriteName}");
                return null;
            }

            return items[0].Sprite;
        }
    }
}