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
        - [Analisando seus parâmetros](#analisando-seus-parc3a2metros-1)
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

The system has the ``EasyUIPage``, which are containers where it will be programmed every command for building up the page. Each page on your game will be a different `EasyUIPage``. Every command must be added inside the ``OnDrawPage``.

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

``UIContainer`` como seu nome ja nos diz, é responsável por conter um conjunto de ``UIElements``. São blocos que irão compor a sua página, seja uma cabeçalho, footer, um menu, uma página com scrollview e entre outras várias possibilidades. ``UIContainer`` tem também como objetivo facilitar a estruturação de sua página. Please refer to Please refer to [Making your own uicontainer](#making-your-own-uicontainers).

Para adicionar um ``UIContainer`` dentro da sua página, assim como os ``UIElements``, seus comandos de criação devem ser realizados dentro da função ``OnDrawnPage()``, da classe ``BuilderUI``. O comando a ser chamado deverá ser o ``AddUIContainer``. Vejamos sua representação abaixo:


```CS
BuilderUI.AddContainer<UIContainerType>(UIContainer parent, Dictionary<string, object> args = null, Action<UIContainerType> onElementCreated = null);
```

### Analisando seus parâmetros

* ``ElementData parent``: É possível aninhar ``UIContainers``.
* ``Dictionary<string, object> args``: Parâmetro opcional que define todos os atributos do ``UIContainer`` instanciado na tela.
* ``Action<UIElementType> OnElementCreated``: Callback opcional que é disparada logo depois que o elemento é instanciado na tela.

## Building your page: ScreenViewer

Todos os elementos são criados dentro de um ``UIContainer`` raiz chamado de ``ScreenViewer``. Esse container é instanciado por padrão quando o sistema cria uma página. É com este elemento que pode configurar o ``padding`` do seu aplicativo. Para definir o ``padding``, é nessecário chamar a função ``SetupScreenViewer(float padding)``, passando o valor desejado no argumento.

## General Layout

De modo geral, uma página irá conter uma estrutura hierarquizada de ``UIContainer`` e ``UIElement``. Essa estrutura pode ser observada no asset ``RuntimeDataContainer``, como demonstra a imagem abaixo:

<img src="README-FILES/RuntimeDataContainer-Hierarchy.png" style="margin-top: 10px; margin-bottom: 10px">


## Making your own uielements

É possível criar ``UIElements`` personalizados, com objetivo de suprir as necessidades do seu projeto. Vamos analisar a implemetação de um elemento já presente nesta package, o elemento ``Label``:

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
Como dito na seção [sobre os UIElements](#building-your-page-uielement). Seu dado é definido no momento em que é instanciado na tela. Logo, ao chamar a função ``SetupElement``, o código define qual vai ser o seu dado e em seguida atribui este dado no elemento de texto ``label``.

A função abaixo ``ApplyArgs`` estará presente em todos os ``UIElements`` presentes no projeto. Nessa função, pode ser definida algumas palavras chaves para algumas definições dos atributos do elemento. No caso de ``Label``, foram criadas atributos que ajudam a definir o estilo do texto, afetando em como ele deve ser quando for instanciado.

> **_NOTE:_** Não esqueça de chamar as classes bases das funções citadas acima, caso contrário, algumas funcionalidades do sistema não vão funcionar.

## Making your own uicontainers

É possível criar ``UIContainers`` personalizados com o objetivo de suprir as necessidades do seu projeto. Vamos analisar a implementação de um elemento já presente nesta páckage, o container ``ScrollableContainer``:

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

Este container tem como objetivo possibilitar o usuário de scrollar a página. Assim como os ``UIElement``, o container também apresenta as funções ``SetupEmelement`` e ``ApplyArgs``. No exemplo acima, o container está implementando um outro cotainer chamado ``VerticalContainer``, que tem como objetivo dispor os elementos filho verticalmente. E por sua vez, essa classe herda a classe base ``UIContainer``. No final, esta classe acima irá apresentar os elementos verticalmente e será possível scrollar a página.

> **_NOTE:_** Não esqueça de chamar as classes bases das funções citadas acima, caso contrário, algumas funcionalidades do sistema não vão funcionar.

## The EasyUI.Library

Todos os elementos de interface já criados para esta pacakage poderão ser acessados dentro da namespace ``EasyUI.Library``. Abaixo estão alguns dos elementos já desenvolvidos até então:

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