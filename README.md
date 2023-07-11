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

Event Manager is a package for Unity game engine that manages communication between decoupled C# components. 
It allows you to subscribe to, and trigger custom events with minimal code and without direct references.
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

todo - briefly mention type safety and argument exceptions.


### Creating Event Manager Instance

The class `EventManager<TEventId>` is a generic class that allows you to manage events of different types. 
The generic type parameter `TEventId` specifies the type that events will be identified with. For example, 
you can use `int`, `string`, or a custom class as `TEventId`. However, there are some recommendations and 
limitations for using different types.

- When using custom classes or structs as `TEventId`, it is important to override the `GetHashCode` and `Equals` methods to ensure that keys are compared correctly.
- Built-in .NET types such as `int`, `string`, `double`, and `DateTime` can be used as `TEventId` without needing to override the `GetHashCode` and `Equals` methods.

```csharp
// Create an instance of EventManager with string as the event identifier type
var eventManager = new EventManager<string>();

// Define some event identifiers
const string EVENT_A = "EventA";
const string EVENT_B = "EventB";
const string EVENT_C = "EventC";
const string EVENT_B = "EventD";
const string EVENT_C = "EventE";

```

### Adding Observers

You can add observers to events using the `AddObserver` methods. The observers are actions that will be invoked when the event is triggered.

Observers may have up to four generic parameters. The generic parameters of these methods are strongly typed, which means that they must be 
explicitly declared when adding observers. For example, if you want to add an observer with one `int` parameter, you must use the `AddObserver<TParam>` 
method and specify `int` as the type for `TParam`.

Each `TEventId` only supports one observer signature. This means that if an event already has an observer with a certain 
number of parameters and types, you cannot add another observer with a different number of parameters or types. For example, 
if an event already has an observer with one `int` parameter, you cannot add another observer with no parameters or with a 
different type of parameter. If you try to do so, an `ArgumentException` will be thrown. It is important to ensure that both the number of parameters and their types match when adding observers to events.

```csharp
// Add an observer with no parameters to EVENT_A
eventManager.AddObserver(EVENT_A, ObserverNoParams);

// Add an observer with one parameter to EVENT_B
eventManager.AddObserver<int>(EVENT_B, ObserverOneParams);

// Add an observer with two parameters to EVENT_C
eventManager.AddObserver<string, bool>(EVENT_C, ObserverTwoParams);

// Add an observer with three parameters to EVENT_D
eventManager.AddObserver<float, string, int>(EVENT_D, ObserverThreeParams);

// Add an observer with four parameters to EVENT_E
eventManager.AddObserver<int, string, int[], object>(EVENT_E, ObserverFourParams);

void ObserverNoParams() { }
void ObserverOneParams(int i) { }
void ObserverTwoParams(string s, bool o) { }
void ObserverThreeParams(float f, string s, int i) { }
void ObserverFourParams(int i, string s, int[] i, object o) { }

```

### Triggering Events

To trigger an event, you can use the `TriggerEvent` methods. You need to pass the event identifier and the event data (if any) as parameters. For example:

```csharp
// Trigger EVENT_A with no data
eventManager.TriggerEvent(EVENT_A);

// Trigger EVENT_B with an integer as data
eventManager.TriggerEvent(EVENT_B, 42);

// Trigger EVENT_C with a string and a boolean as data
eventManager.TriggerEvent(EVENT_C, "Hello", true);

// Trigger EVENT_C with a string and a boolean as data
eventManager.TriggerEvent(EVENT_D, 0.1f, "Hello", 0);

// Trigger EVENT_C with a string and a boolean as data
eventManager.TriggerEvent(EVENT_E, 0, "Hello", new int[] { 1, 2, 3, 4 }, new object());
```

Although you may explicitly declare the types of the event data (e.g. `eventManager.TriggerEvent<int>(EVENT_B, 42);`) 
you may not need to due to type inference.

### Removing Observers

To remove an observer from an event, you can use the `RemoveObserver` methods. You need to pass the event identifier and the observer action as parameters. For example:

```csharp
// Remove the observer from EVENT_A
eventManager.RemoveObserver(EVENT_A, () => Console.WriteLine("Event A triggered"));
```

### Get Observer Count

To get the number of observers for an event, you can use the `GetObserverCount` method. You need to pass the event identifier as a parameter. For example:

```csharp
// Get the number of observers for EVENT_B
int count = eventManager.GetObserverCount(EVENT_B);
Console.WriteLine($"There are {count} observers for Event B");
```

### Clear Observers

To clear all the observers for an event, you can use the `ClearObservers` method. You need to pass the event identifier as a parameter. For example:

```csharp
// Clear all the observers for EVENT_C
eventManager.ClearObservers(EVENT_C);
```

### Clear All Observers

To clear all the observers for all the events, you can use the `ClearAllObservers` method. For example:

```csharp
// Clear all the observers for all the events
eventManager.ClearAllObservers();
```




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
