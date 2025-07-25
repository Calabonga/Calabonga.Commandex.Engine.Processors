﻿# Calabonga.Commandex.Engine.Processors

This is a [nuget-package](https://www.nuget.org/packages/Calabonga.Commandex.Engine.Processors/) for modular monolith application on WPF platform with plugins as modules. Results Processors for Calabonga.Commandex.Shell commands execution results. This is an extended version of the just show string in the notification dialog.

## What is Calabonga.Commandex

The `Calabonga.Commandex` - This is an application on WPF-platform built with CommunityToolkit.MVVM for modules (plugins) using: launch and execute.

What is the `Calabonga.Commandex` can:
* Find a modules `.dll` (plugins) in the folder you set up.
* Launch or execute modules `.dll` (plugins) from GUI.
* Get the results of the module's (plugins) work after they completed.

It's a complex solution with a few repositories:

* **[Calabonga.Commandex.Shell](https://github.com/Calabonga/Calabonga.Commandex.Shell)** →  Command Executer or Command Launcher. To run commands of any type for any purpose. For example, to execute a stored procedure or just to copy some files to some destination.
* **[Calabonga.Commandex.Commands](https://github.com/Calabonga/Calabonga.Commandex.Commands)** →  Commands for Calabonga.Commandex.Shell that can execute them from unified shell.
* **[Calabonga.Commandex.Shell.Develop.Template](https://github.com/Calabonga/Calabonga.Commandex.Shell.Develop.Template)** →  (`Tool Template`) This is a Developer version of the Command Executer Shell (`Calabonga.Commandex`). Which is created to runs commands of any type for any purposes. For example, to execute a stored procedure or just to co…
* **[Calabonga.Commandex.Engine](https://github.com/Calabonga/Calabonga.Commandex.Engine)** →  Engine and contracts library for Calabonga.Commandex. Contracts are using for developing a modules for Commandex Shell.
* **[Calabonga.Commandex.Engine.Processors](https://github.com/Calabonga/Calabonga.Commandex.Engine.Processors)** →  Results Processors for Calabonga.Commandex.Shell commands execution results. This is an extended version of the just show string in the notification dialog.
* **[Calabonga.CommandexCommand.Template](https://github.com/Calabonga/Calabonga.CommandexCommand.Template)** →  (`Tool Template`) This is a template of the project to create a Command for Commandex. Just install this nuget as a template for Visual Studio (Rider or dotnet CLI) and then you can create a DialogCommand faster.
 
## History of changes

### v3.0.0 2025-07-18

* `Calabonga.Commandex.Engine` package updated. New type of the `CommandexCommand` added. `ZoneCommandexCommand<TView,TViewModel>` can be shown in a `Shell` Window in the special `ContentControl` marked as `MainZone`.
* Engine nuget dependency updated.

### v2.8.1 2025-07-09

* `Calabonga.Commandex.Engine` package updated

### v2.8.0 2025-07-08

* `Calabonga.Commandex.Engine` package updated

### v2.7.0 2025-07-07

* `Calabonga.Commandex.Engine` package updated

### v2.6.0 2025-07-04

* `Calabonga.Commandex.Engine` package updated
* Result not shown in UI when parameter `IsPushToShellEnabled` is `false`. Only logger save results in this case.

### v2.5.0 2025-06-29

[Toast Notifications](https://github.com/Calabonga/Calabonga.Commandex.Engine/wiki/Toast-Notifications) added into a [Calabonga.Commandex.Engine](https://github.com/Calabonga/Calabonga.Commandex.Engine). Please use [How To Toast Notifications](https://github.com/Calabonga/Calabonga.Commandex.Engine/wiki/Toast-Notifications) on the Engine repository.

### v2.3.0 2025-06-18

Main dependency [Calabonga.Commandex.Engine](https://github.com/Calabonga/Calabonga.Commandex.Engine) was updated. There are changes. `ViewModelLocationProvider` and `ViewModelLocation` created for Views and ViewModels binding automation. If you want to use `AutoBindingViewModel` on the View (XAML), something like shown below:

```diff
<UserControl x:Class="Commandex.MyDemoCommand.Views.MyDemoView"
            xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
            xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
            xmlns:local="clr-namespace:Commandex.MyDemoCommand.Views"
            xmlns:viewModels="clr-namespace:Commandex.MyDemoCommand.ViewModels"
+           xmlns:viewModelLocator="clr-namespace:Calabonga.Commandex.Engine.ViewModelLocator;assembly=Calabonga.Commandex.Engine"
+           viewModelLocator:ViewModelLocator.AutoBindingViewModel="True"
            mc:Ignorable="d" 
            d:DesignHeight="450" d:DesignWidth="800">
```

Than you should initialize `ViewModelLocationProvider` in your Shell project in the `Composition Root` of your Application. For example:

```csharp
var buildServiceProvider = services.BuildServiceProvider();
ViewModelLocationProvider.SetDefaultViewModelFactory(type => buildServiceProvider.GetRequiredService(type));
return buildServiceProvider;
```

You should also follow the naming rules for Views and ViewModels (or create your own overrides). What's the rule? Everything is simple. For example, if your have a view with name **PersonProfileView.xaml** than you should create a ViewModel for it with name **PersonProfileViewModel**.


### v2.2.0 2025-04-15

* **Engine nuget**: Open dialog in the window maximized now available. See the override for DialogResult `IsMaximized`
* Some refactoring made, syntax error fixed. 

### v2.1.0 2025-01-30

* Identity abstraction added (Engine package)

### v2.0.1 2024-11-21

* More info shown on `ClipboardResult` processing competed.

### v2.0.0 2024-11-10

* Migration to NET9
* Nuget package `Calabonga.Commandex.Engine` updated NET9

### v1.4.2 2024-11-05

* * Nuget-package versions `Calabonga.Commandex.Engine` updated where new ConfirnationDialog added (Func too).

### v1.4.1 2024-11-01

* Nuget-package versions `Calabonga.Commandex.Engine` updated where new ConfirnationDialog added.

### v1.3.0 2024-10-12

* ENGINE: The `SettingsPath` parameter was created to allow you to store the command's settings env-files in a separate folder
* ENGINE: Summaries for some members were updated
* Summaries updated for some members
* Nuget package versions Engine and current nuget was synchronized

### v1.0.0 2024-10-09

* First beta with `TextFileResult` and `ClipboardResult` processors created.