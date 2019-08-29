[![AppVeyor](https://img.shields.io/appveyor/ci/riezebosch/consoleum/master.svg)](https://ci.appveyor.com/project/riezebosch/consoleum?branch=master)
[![NuGet Consoleum](https://img.shields.io/nuget/v/Consoleum.svg)](https://www.nuget.org/packages/Consoleum/)
[![NuGet Consoleum.PageObjects](https://img.shields.io/nuget/v/Consoleum.PageObjects.svg)](https://www.nuget.org/packages/Consoleum.PageObjects/)

## Consoleum

A [Selenium](http://www.seleniumhq.org/)-like solution for automating console applications.

![action](action.gif?raw=true)

## Origin

This project is a spin-off from a [SpecFlow](http://specflow.org/) exercise. Don't get bogged down with [automation-id's](https://docs.microsoft.com/en-us/dotnet/framework/ui-automation/use-the-automationid-property) or [css selectors](https://www.w3schools.com/cssref/css_selectors.asp) but write UI tests against a good-ol' console application while still using practices like [Page Object Design Pattern](http://www.seleniumhq.org/docs/06_test_design_considerations.jsp#page-object-design-pattern).

## How

Use the `IConsoleDriver` and `ConsoleDriver` to start the process and the provided `ICaptureOutput` and [`IKeyboardSimulator`](https://github.com/michaelnoonan/inputsimulator) to interact with the console.

## Page Objects

You can use this package without or use plain POCO's, but you might want to use the `Page` base class from the [Consoleum.PageObjects](https://www.nuget.org/packages/Consoleum.PageObjects/) package instead.

```cs
public void SomeTestMethod()
{
    using (var driver = new ConsoleDriver("some-app.exe"))
    {
        driver.Start();

        Page
            .StartWith<Main>(driver)
            .SomeActionOnCurrentPage()
            .SomeNavigationAction()
            .ShouldBeLoggedInByNow();
    }
}
```

```cs
class Main : Page
{
    public override bool IsOpen => ExistsInOutput("pattern-matching-output-on-this-page");

    public Main SomeActionOnCurrentPage()
    {
        Driver
            .Keyboard
            .KeyPress(...)
            .Sleep(200);
        
        return this;
    }

    public Another SomeNavigationAction()
    {
        Driver
            .Keyboard
            .KeyPress(...)
            .Sleep(200);
        
        return NavigateTo<Another>();
    }
}
```
Look at the [tests](Consoleum.PageObjects.Tests) for more inspiration on reusing the driver over multiple tests and assertions using [Shouldly](https://www.nuget.org/packages/Shouldly/).

## Why not xplat?

This package relies heavily on Windows Desktop functions for starting the process, capturing the output on the clipboard using keystrokes and native Win32 invocations to grab that data from the clipboard. 
You do the math why it can't be xplat.
