<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>





<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/danieljohnmiller/event-manager">
    <img src="https://i.imgur.com/pvE7uPU.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">Event Manager Unity Package</h3>

  <p align="center">
    C# Event manager package for Unity game engine.
    <br />
    <a href="https://github.com/danieljohnmiller/event-manager"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/danieljohnmiller/event-manager/issues">Report Bug</a>
    ·
    <a href="https://github.com/danieljohnmiller/event-manager/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Event Manager is a package for Unity game engine that simplifies communication between decoupled C# components. 
It allows you to subscribe to, and trigger custom events with minimal code and without direct references. 
Event Manager is designed to be lightweight, easy to use, and extensible. 
Whether you need to coordinate gameplay, UI, audio, or anything else, 
Event Manager can help you achieve a modular and maintainable architecture for your Unity projects.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

The event manager package can be easily added to your Unity project with [Unity's Package Manager](https://docs.unity3d.com/Manual/Packages.html). See installation instructions below.


### Installation

1. In your unity project, open [Unity's Package Manager](https://docs.unity3d.com/Manual/Packages.html) from Window > Package Manager.
2. Open the add package menu (+) and select "Add package from git URL...".

   ![add package from github](https://i.imgur.com/a9yYzDh.png)
3. Enter the event-manager repo URL : https://github.com/danieljohnmiller/event-manager.git, and press Add.

For more information about installing Unity packages from git, see [Unity Documentation](https://docs.unity3d.com/2022.3/Documentation/Manual/upm-ui-giturl.html).

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- USAGE EXAMPLES -->
## Usage

The interface `IEventManager` is provided by the event manager package to ensure concrete implementations of the service are consistent and interchangeable.

Events are identified using an `int`, and support event handlers with zero or one generic parameter.
Event handlers with different parameter types can be added to the same event, but the parameter type needs to be specified when adding the handler, and triggering its event.
When triggered, only event handlers with the provided parameter type will be invoked.

### Singleton VS instantiatable

The event manager package provides two classes that implement the `IEventManager` interface: `EventManager` and `GlobalEventManager`. `EventManager` is a regular class that can be instantiated multiple times, while `GlobalEventManager` is a singleton class that ensures only one instance exists at any time. Otherwise, both implementations are identical.

The following examples use `GlobalEventManager`, getting the singleton instance from static method `GlobalEventManager.Instance`. If using `EventManager` instead create an instance with `new EventManager();`.

### Simple example



```
IEventManager eventManager = GlobalEventManager.Instance;
   
const int eventId = 0;
      
eventManager.AddHandler(eventId, HandlerMethod);
      
eventManager.TriggerEvent(eventId);
   
   
void HandlerMethod()
{
   // handle the event
}

```
In this example, we add one handler (`HandlerMethod`) to event 0. 
`HandlerMethod` is invoked when event 0 is triggered.

### Multiple Event Handlers

Additional handlers can be added to the same event. As long as their signatures match the first one (in this case no parameters), they will all be triggered when `eventManager.TriggerEvent(eventId)` is called. Here's an example:


```
IEventManager eventManager = GlobalEventManager.Instance;
   
const int eventId = 0;
      
eventManager.AddHandler(eventId, HandlerMethod1);
eventManager.AddHandler(eventId, HandlerMethod2);
eventManager.AddHandler(eventId, HandlerMethod3);
      
eventManager.TriggerEvent(eventId);

   
void HandlerMethod1() { }
void HandlerMethod2() { }
void HandlerMethod3() { }

```

This time we add 3 different handlers to event 0. As they all have the same signature, all three are triggered on `eventManager.TriggerEvent(eventId);`.


### Event Handlers with Parameters

Event handlers with one generic parameter are supported by the event manager, and can be added to the same event as parameterless handlers.
To add an event handler with a parameter, specify the type of its parameter when adding it to the manager e.g. `eventManager.AddHandler<type>(eventId, Handler);`.

```
IEventManager eventManager = GlobalEventManager.Instance;
   
const int eventId = 0;
      
eventManager.AddHandler<int>(eventId, HandlerMethod1);
eventManager.AddHandler<int>(eventId, HandlerMethod2);
eventManager.AddHandler<string>(eventId, HandlerMethod3);
      
eventManager.TriggerEvent<int>(eventId, 5);

   
void HandlerMethod1(int a) { }
void HandlerMethod2(int b) { }
void HandlerMethod3(string c) { }

```

In this example, three handlers are added to event 0. `HandlerMethod1` and `HandlerMethod2` have an `int` parameter, and `HandlerMethod3` has a string parameter. 
When triggering an event with a parameter, specify the parameter type and pass in an object of that type (e.g. `eventManager.TriggerEvent<int>(eventId, 5);`).
Only handlers with an int parameter (`HandlerMethod1` and `HandlerMethod2`) will be invoked on this trigger. 
If we wanted to invoke `HandlerMethod3` then we would call `eventManager.TriggerEvent<string>(eventId, "exampleString")`.

Any combination and number of handlers with different parameter types can be added to a single event.

### Removing A Handler

Handlers can be removed from event manager to stop them responding to event triggers. `IEventManager` exposes several methods for this:





1. `IEventManager.RemoveHandler(int eventId, Action handler);` or `IEventManager.RemoveHandler<THandlerParam>(int eventId, Action<THandlerParam> handler);` - only specified handler is removed. For handler with parameter, specify type.
```
IEventManager eventManager = GlobalEventManager.Instance;
const int eventId = 1;

eventManager.AddHandler<int>(eventId, HandlerMethod);
eventManager.AddHandler(eventId, HandlerMethod2);

eventManager.RemoveHandler<int>(eventId, HandlerMethod); // remove handler with int parameter
eventManager.RemoveHandler(eventId, HandlerMethod2); // remove handler with no parameter.


void HandlerMethod(int i) { }
void HandlerMethod2() { }
```


2. `IEventManager.RemoveEvent(int eventId);` - specified event and all handlers regardless of parameter type are removed.
```
IEventManager eventManager = GlobalEventManager.Instance;
const int eventId = 1;

eventManager.AddHandler<int>(eventId, HandlerMethod);
eventManager.AddHandler(eventId, HandlerMethod2);

eventManager.RemoveEvent(eventId); // all handlers removed from event


void HandlerMethod(int i) { }
void HandlerMethod2() { }
```


3. `IEventManager.RemoveEventHandlerType(int eventId, [CanBeNull] Type handlerParamType);` - removes handlers of specified parameter type from event. Set `handlerParamType` to null to remove handlers with no parameter.
```
IEventManager eventManager = GlobalEventManager.Instance;
const int eventId = 1;

eventManager.AddHandler<int>(eventId, HandlerMethod);
eventManager.AddHandler(eventId, HandlerMethod2);

eventManager.RemoveEventHandlerType(eventId, typeof(int)); // only handlers with int parameter removed


void HandlerMethod(int i) { }
void HandlerMethod2() { }
```

4. `IEventManager.RemoveAll();` - removes all events along with all handlers.
```
IEventManager eventManager = GlobalEventManager.Instance;
const int eventId = 1;

eventManager.AddHandler<int>(eventId, HandlerMethod);
eventManager.AddHandler(eventId, HandlerMethod2);

eventManager.RemoveAll(); // all events and handlers removed


void HandlerMethod(int i) { }
void HandlerMethod2() { }
```

<p align="right">(<a href="#readme-top">back to top</a>)</p>


## Upcoming features

The following features are expected in version 8.0.0:

- Event manager implementation that extends `Monobehavior` so can be attached to game objects in Unity. This will include serializing parts of the event manager to expose them to the Unity editor.
- Support for event handlers with up to three generic parameters.
- 'Event manager lite' implementation where only one handler signature can be assigned to each event ID.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Daniel Miller - daniel.miller.djm@gmail.com

Project Link: [https://github.com/danieljohnmiller/event-manager](https://github.com/danieljohnmiller/event-manager)

<p align="right">(<a href="#readme-top">back to top</a>)</p>




<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/danieljohnmiller/event-manager.svg?style=for-the-badge
[contributors-url]: https://github.com/danieljohnmiller/event-manager/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/danieljohnmiller/event-manager.svg?style=for-the-badge
[forks-url]: https://github.com/danieljohnmiller/event-manager/network/members
[stars-shield]: https://img.shields.io/github/stars/danieljohnmiller/event-manager.svg?style=for-the-badge
[stars-url]: https://github.com/danieljohnmiller/event-manager/stargazers
[issues-shield]: https://img.shields.io/github/issues/danieljohnmiller/event-manager.svg?style=for-the-badge
[issues-url]: https://github.com/danieljohnmiller/event-manager/issues
[license-shield]: https://img.shields.io/github/license/danieljohnmiller/event-manager.svg?style=for-the-badge
[license-url]: https://github.com/danieljohnmiller/event-manager/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/daniel-miller-690978170/
[product-screenshot]: images/screenshot.png
[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D
[Vue-url]: https://vuejs.org/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00
[Svelte-url]: https://svelte.dev/
[Laravel.com]: https://img.shields.io/badge/Laravel-FF2D20?style=for-the-badge&logo=laravel&logoColor=white
[Laravel-url]: https://laravel.com
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com 
