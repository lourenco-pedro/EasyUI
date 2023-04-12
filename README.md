# EasyUI #

EasyUI is a package developed with the objective of speeding up the process of implementing in-game screens inside Unity. Its focus is on building screens entirely from code, very inspired by how the interfaces of apps in Flutter are made.

# Summary #

- [How does it works](#how-does-it-works)
    - [Navigating between pages already made for you](#navigating-between-pages-already-made-for-you)
    - [Creating pages](#creating-pages)
        - [Implementing a new page](#implementing-a-new-page)
        - [Displaying this page](#displaying-this-page)
    - [Building your page: UIElement](#building-your-page-uielement)
        - [Checking the parameters](#checking-the-parameters)
    - [Building your page: UIContainer](#building-your-page-uicontainer)
        - [Checking the parameters](#checking-the-parameters-1)
    - [Building your page: ScreenViewer](#building-your-page-screenviewer)
    - [General Layout](#general-layout)
    - [Making your own uielements](#making-your-own-uielements)
    - [Making your own uicontainer](#making-your-own-uielements)
    - [The EasyUI.Library](#the-easyuilibrary)
- [License](#license)


# How does it works #

## Navigating between pages already made for you ##

EasyUI already has a navigation system implemented. It's not a complex system but it covers enough to make apps with more than one page. Helping the user to focus only on what screens the apps must have instead of how to navigate between them.

## Creating pages ##

The system has the ``EasyUIPage``, which are containers where it will be programmed every command for building up the page. Each page on your game will be a different ``EasyUIPage``. Every command must be added inside the ``OnDrawPage``.

If you have your page implemented and you want to open it, you'll have to add an instance of that page inside of a stack of EasyUIPages through the command `` EasyUIPage.Add(your_page_here); ``
<br>
<br>
The example below shows how to implement a new page for your project.
<br>
<br>

### Implementing a new page
```C#

//Don't forget to include the EasyUI.Page
using EasyUI.Page;

public class EasyUIPage_MainMenu : EasyUIPage
{

    //Constructor for initializing any additional properties of your classes
    public EasyUIPage_MainMenu(string name) : base(name) 
    {
    }

    //Where all the magic happens 🪄
    //Here you will draw your page adding the necessary commands to display your user interface
    public override List<string> OnDrawPage()
    {
        //Drawn your page here!!
    }
}
```

### Displaying this page

```C#

using UnityEngine;

public class YouControllerClass : Monobehaviour
{
    void Start()
    {
        EasyUIPage.Add(new EasyUIPage_MainMenu(name: "my page!"));
    }
}

```
When calling this command, the system adds this new instance onto the ``EasyUIPages`` stack. The system will do all the processes of transition between pages from the actual page to the latest one added to the stack. It's also possible to do the reverse process, closing the actual page by calling the ``Close()`` function. By calling this function, the system will remove the current page from the stack.

## Building your page: UIElement

The ``UIElements`` are the components that will compose your screen. There are a few ``UIElements`` already implemented inside this package that provides you with the basic needs of a screen. 

Those elements have a field in their superclass called ElementData, which holds the actual value which this ``UIElement`` represents. So let's say you want to add a label on your screen with the value ``Hello World`` on it. Its ``ElementData`` will be the one that will hold this value. Every element must have its ``ElementData`` defined, be it a button, a label, a mask, etc. 

The ``ElementData`` is defined inside the ``SetupElement<ElementData>(ElementData data)`` function. It is possible to create your own ``UIElement``for your project's needs. Please, refer to [Making your own uielements](#making-your-own-uielements)

You can use the ``AddUIElement()`` function to instantiate UIElement on your page. This function can be accessed from the ``BuilderUI``. Every command to draw any ``UIElement`` on your page must be added inside the ``OnDrawPage()`` function. When opening a page, the system executes this function once the page is ready to be drawn. By calling this function, you must add some arguments inside it, let's break through it:

```CS
Builder.AddUIElement<UIElementType, ElementData>(ElementData data, UIContainer parent = null, Dictionary<string, object> args = null,Action<UIElementType> onElementCreated = null, BuildSettings settings = null);
```

The function above will create an element inside the screen. Every element derives from a superclass called ``UIElement<ElementType>`` - where the ``ElementType`` is the type of data this element will hold. The type of element and the value of its data will be specified when calling the function above.

### Checking the parameters

* ``ElementData data``: What is going to be the value of this UIElement.
* ``UIContainer parent``: Optional parameter that defines the element's parent. [See about UIContainer](#building-your-page-uicontainer). If it is null then this element will be instantiated inside the [``ScreenViewer``](#building-your-page-screenviewer)  
* ``Dictionary<string, object> args``: Optional parameter that defines every attributes of this instantiated element.
* ``Action<UIElementType> OnElementCreated``: Optional callback fired once the element is instantiated. Often used to create other elements sequentially.
* ``BuildSettings settings``: Optional parameter that holds additional config of how this instantiation process must happen.


## Building your page: UIContainer

``UIContainer`` is responsible for containing a set of ``UIElements``. It is a block that will compose your page. It is for grouping a set of ``UIElements`` for organizational purposes. With ``UIContainer`` you can create know groups such as Headers, footers, menu,s and so on. Just like the ``UIElements``, you can also create your own ``UIContainers``, please, refer to [Making your own uicontainer](#making-your-own-uicontainers).

You can add a new ``UIContainer``to your page by calling the ``AddUIContainer`` function, from the ``BuilderUI`` class. Remember that this function must be called inside the ``OnDrawPage``


```CS
BuilderUI.AddContainer<UIContainerType>(UIContainer parent, Dictionary<string, object> args = null, Action<UIContainerType> onElementCreated = null);
```

### Checking the parameters

* ``ElementData parent``: It is possible to add nested ``UIContainers``.
* ``Dictionary<string, object> args``: Optional parameter that defines every attributes of this instantiated container.
* ``Action<UIElementType> OnElementCreated``: Optional callback fired once the container is instantiated. Often used to create other elements sequentially.

## Building your page: ScreenViewer

Every element is created inside a root ``UIContainer`` called ``ScreenViewer``. This container is instantiated by default when the System creates your first page. it is with that element that you can configure the general padding of the app. To configure the general padding of the app, you can call the ``SetupScreenViewer(float padding)`` function - also inside the ``BuilderUI`` class.

## General Layout

In general, your page will have a hierarchy structure made of ``UIContainer`` and ``UIElement``. This structure can be observed in the ``RuntimeDataContainer`` asset, just as shown bellow:

<img src="README-FILES/RuntimeDataContainer-Hierarchy.png" style="margin-top: 10px; margin-bottom: 10px">


## Making your own uielements

It is possible to create your own ``UIElement`` as your project needs. Let's check how the ``Label`` is implemented as a model:

```CS
namespace EasyUI.Library
{
    public class Label : UIElement<string>
    {
        [SerializeField]
        protected TextMeshProUGUI label;

        public override void SetupElement(string data, Dictionary<string, object> args = null)
        {
            base.SetupElement(data, args);

            label.text = data;
        }

        protected override void ApplyArgs(Dictionary<string, object> args = null)
        {
            if (args.TryGetValue("fontSize", out object fontSize)) 
            {
                label.fontSize = (float)fontSize;
            }

            if (args.TryGetValue("font", out object font)) 
            {
                label.font = Resources.Load<TMP_FontAsset>($"Fonts/{(string)font}");
            }

            if (args.TryGetValue("fontStyle", out object fontStyle)) 
            {
                label.fontStyle = (FontStyles)fontStyle;
            }

            if (args.TryGetValue("color", out object color)) 
            {
                label.color = (Color)color;
            }

            if (args.TryGetValue("alignment", out object alignment)) 
            {
                label.alignment = (TextAlignmentOptions)alignment;
            }

            if (args.TryGetValue("lineSpacing", out object lineSpacing)) 
            {
                label.lineSpacing = (float)lineSpacing;
            }

            base.ApplyArgs(args);
        }
    }
}
```
As said before about UIElements, their data is defined at the moment the element is instantiated. Under the hood, the instantiation process calls the ``SetupElement`` function, giving the desired data value passed inside the argument when called the ``CreateUIElement`` before.

The ``ApplyArgs`` function will be on every ``UIElement``. The system uses this function to define every attribute of this instantiated ``UIElement`` you desire to change.

> **_NOTE:_** Don't forget to call the ``base.ApplyArgs()`` and ``base.SetupElement()``. Otherwise, the UIElement will not work properly.

## Making your own uicontainers

It is possible to create your own ``UIContainer`` as your project needs. Let's check how the ``ScrollableContainer`` is implemented as a model:

```CS
namespace EasyUI.Library
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollableContainer : VerticalContainer
    {

        [SerializeField] protected ScrollRect scrollRect;

        public override void SetupElement(Dictionary<string, object> args = null)
        {
            scrollRect.viewport = transform.parent as RectTransform;
            scrollRect.content = transform as RectTransform;

            base.SetupElement(args);
        }

        protected override void ApplyArgs(Dictionary<string, object> args = null)
        {
            if (args.TryGetValue("movementType", out object movementType))
                scrollRect.movementType = (ScrollRect.MovementType)movementType;

            if (args.TryGetValue("inertia", out object inertia))
                scrollRect.inertia = (bool)inertia;

            if (args.TryGetValue("scrollHorizontal", out object scrollHorizontal))
                scrollRect.horizontal = (bool)scrollHorizontal;

            if(args.TryGetValue("scrollVertical", out object scrollVertical))
                scrollRect.vertical = (bool)scrollVertical;

            base.ApplyArgs(args);
        }

    }
}
```

This container has the objective to let the users scroll through the page. Once the container is instantiated, the system will set it up by calling the ``SetupElement`` function. And will define their attributes by calling the ``ApplyArgs`` function.

Note that in this case, the scrollable is deriving from another class called ``VerticalContainer`` instead of deriving directly from ``UIContainer``. It was made like that because ``VerticalContainer`` already derives from ``UIContainer``, and so the user can also create only vertical containers that don't have any scrollable capability.

> **_NOTE:_** Don't forget to call the ``base.ApplyArgs()`` and ``base.SetupElement()``. Otherwise, the UIContainer will not work properly.

## The EasyUI.Library

Every ``UIElement`` and ``UIContainer`` created for this package can be found in EasyUI.Library namespace. Here are all the elements already made for you:

### UIElements

- BackgroundImage
- Label
- SimpleButton
- Mask

### UIContainer

- HorizontalOrVerticalContainer
    - HorizontalContainer
        - ScrollableContainer
    - VerticalContainer
- GridContainer


# License 

EasyUI is a free software; you can redistribute it and/or modify it under the terms of the MIT license. See LICENSE for details.
