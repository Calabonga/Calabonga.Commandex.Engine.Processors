# Calabonga.Commandex.Engine.Processors

This is a [nuget-package](https://www.nuget.org/packages/Calabonga.Commandex.Engine.Processors/) for modular monolith application on WPF platform with plugins as modules. Results Processors for Calabonga.Commandex.Shell commands execution results. This is an extended version of the just show string in the notification dialog.

## What is Calabonga.Commandex

The `Calabonga.Commandex` - This is an application on WPF-platform built with CommunityToolkit.MVVM for modules (plugins) using: launch and execute.

What is the `Calabonga.Commandex` can:
* Find a modules `.dll` (plugins) in the folder you set up.
* Launch or execute modules `.dll` (plugins) from GUI.
* Get the results of the module's (plugins) work after they completed.

It's a complex solution with a few repositories:

* **[Calabonga.Commandex.Shell](https://github.com/Calabonga/Calabonga.Commandex.Shell)** → Command Executer or Command Launcher. To run commands of any type for any purpose. For example, to execute a stored procedure or just to copy some files to some destination.
* **[Calabonga.Commandex.Commands](https://github.com/Calabonga/Calabonga.Commandex.Commands)** → Commands for Calabonga.Commandex.Shell that can execute them from unified shell.
* **[Calabonga.Commandex.Shell.Develop.Template](https://github.com/Calabonga/Calabonga.Commandex.Shell.Develop.Template)** → This is a Developer version of the Command Executer (`Calabonga.Commandex`). Which is created to runs commands of any type for any purposes. For example, to execute a stored procedure or just to co…
* **[Calabonga.Commandex.Engine](https://github.com/Calabonga/Calabonga.Commandex.Engine)** → Engine and contracts library for Calabonga.Commandex. Contracts are using for developing a modules for Commandex Shell.
* **[Calabonga.Commandex.Engine.Processors](https://github.com/Calabonga/Calabonga.Commandex.Engine.Processors)** → Results Processors for Calabonga.Commandex.Shell commands execution results. This is an extended version of the just show string in the notification dialog.
 
## History of changes

### v2.4.0 2025-06-28

`Calabonga.Commandex.Engine` nuget-package updated where a Toast Notification were implemented: Success, Information, Warning, Error. How it works? It's really easy.
1. Inject `INotificationManager` into your ViewModel constructor:
    ```csharp
    public partial class MainWindowsViewModel : ViewModelBase, IDisposable
    {
        private readonly INotificationManager _notificationManager;
        
        public MainWindowsViewModel(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }
        
        // ... other code...
    }
    ```
2. Create a toast message:
    ```csharp
        var errorToast = NotificationManager.CreateErrorToast("Message text", "Title");
        //  or
        var successToast = NotificationManager.CreateSuccessToast("Message text", "Title");
        // or 
        var warningToast = NotificationManager.CreateWarningToast("Message text", "Title");
        // or 
        var informationToast = NotificationManager.CreateInformationToast("Message text", "Title");
    ```
3. The show a toast:
    ```csharp
     _notificationManager.Show(errorToast);
     // or
     _notificationManager.Show(successToast);
     // or
     _notificationManager.Show(warningToast);
     // or
     _notificationManager.Show(informationToast);
    ```
4. Ypu can find any other options for show toast.

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