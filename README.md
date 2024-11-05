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

## v1.4.2 2024-11-05

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