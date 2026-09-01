# CLAUDE.md

Guidance for Claude Code (claude.ai/code) when working in the **`Calabonga.Commandex.Engine.Processors`** repository.

> Дополнительные правила — в [`.claude/rules/code-styles.md`](rules/code-styles.md) (стиль C#) и [`.claude/rules/workflow.md`](rules/workflow.md) (ветки, коммиты, тесты).
> Общий контекст рабочего пространства из шести репозиториев — в `../CLAUDE.md`.

## Что это за репозиторий

Один NuGet-пакет **`Calabonga.Commandex.Engine.Processors`** — расширение библиотеки `Calabonga.Commandex.Engine`. Он добавляет `AdvancedResultProcessor` — альтернативную реализацию `IResultProcessor` (контракт Engine), которая умеет не просто показать строку в диалоге уведомления (`DefaultResultProcessor` из Engine), а диспетчеризовать результат команды по его типу: сохранить в текстовый файл (`TextFileResult`) или положить в буфер обмена (`ClipboardResult`).

Пакет устанавливается **вместо** голого Engine — либо в `Shell` (тогда `Shell` вызывает `services.AddAdvancedResultProcessor()`), либо в проект команды. Engine он тянет транзитивно (`PackageReference` на `Calabonga.Commandex.Engine`), поэтому контрактная сборка для рефлексии в `Shell` присутствует автоматически.

Тип репозитория в терминах версионирования рабочего пространства — **Framework**: его версия `X.Y.Z` всегда совпадает с версией `Engine` и остальных Framework-пакетов одного релизного цикла. Публикуется после того, как соответствующий `Calabonga.Commandex.Engine X.Y.Z` станет доступен для restore с nuget.org (CI собирается против опубликованного Engine). Подробности и диаграмма порядка публикации — в `../CLAUDE.md`, раздел «Принципы версионирования».

## Структура

```
src/
  Calabonga.Commandex.Engine.Processors.sln
  Calabonga.Commandex.Processors/                 <- папка проекта
    Calabonga.Commandex.Engine.Processors.csproj  <- PackageId и AssemblyName = Calabonga.Commandex.Engine.Processors
    AdvancedResultProcessor.cs
    Base/
      IProcessor.cs         IProcessorResult.cs
      Processor.cs          ProcessorResult.cs
    Results/
      TextFileResult.cs     ClipboardResult.cs
    Extensions/
      ServiceCollectionExtensions.cs
      ProcessorResult.cs    <- ДУБЛЬ, см. «Известные проблемы»
```

Имя папки проекта — `Calabonga.Commandex.Processors`, но `PackageId`, `AssemblyName` и корень namespace — `Calabonga.Commandex.Engine.Processors`. При добавлении файлов ориентируйся на namespace (`Calabonga.Commandex.Engine.Processors[.Base|.Results|.Extensions]`), а не на путь.

## Сборка и публикация

```bash
dotnet build src/Calabonga.Commandex.Engine.Processors.sln -c Release
```

- **.NET 10 SDK**, только Windows: `net10.0-windows8.0`, `UseWPF=true` (нужен `SaveFileDialog` и `Clipboard`).
- `GeneratePackageOnBuild=True` — `.nupkg` появляется при обычном Release-билде.
- `Directory.Build.props` нет; `<Version>` и версия `PackageReference` на Engine прописаны прямо в `.csproj` и поднимаются **только вручную**.
- Тестового проекта в репозитории нет.
- **Публикация** — `.github/workflows/main.yml`: push в `main` → `dotnet build -c Release` → `dotnet nuget push *.nupkg --api-key $NUGET_API_KEY --source nuget.org --skip-duplicate`. Требуется secret `NUGET_API_KEY`. Отдельного шага `pack` нет — пакет берётся из вывода билда.

При подъёме версии в одном релизном цикле правь синхронно: `<Version>`, `<PackageReference Include="Calabonga.Commandex.Engine" Version="...">`, `<PackageReleaseNotes>` и раздел «History of changes» в `README.md` (он пакуется в пакет как `PackageReadmeFile`).

### Соглашение о release notes

Processors — вторичная (Framework) сборка: её классы расширяют контракты Engine (`IResultProcessor`, `JsonSerializerOptionsExt` и т. п.), а `<Version>` и `PackageReference` на Engine в одном релизном цикле всегда равны версии Engine. Поэтому многие релизы Processors по сути только «подтягивают» Engine. `<PackageReleaseNotes>` и запись в `README.md` должны явно называть версию Engine, против которой собрано (напр. «Built against Calabonga.Commandex.Engine 5.0.0 …»), и перечислять собственные изменения пакета — либо явно отмечать, что их нет.

## Архитектура

### AdvancedResultProcessor

`AdvancedResultProcessor : IResultProcessor` (DI-зависимости: `IDialogService`, `IProcessor`, `ILogger<>` — всё из Engine). `ProcessCommand(ICommandexCommand command)`:

1. `command.GetResult() is IProcessorResult` → `processorResult.Accept(_processor)` — дальше работает Visitor (см. ниже). Результат в UI не показывается, обработка полностью на конкретном `IProcessor`.
2. Иначе — результат логируется (`DisplayName`, `Version`, `Description`, `IsPushToShellEnabled`). Если `IsPushToShellEnabled == true` и результат не `null`, он сериализуется в JSON (`JsonSerializerOptionsExt.Cyrillic` из Engine) **в лог**; ошибка сериализации показывается через `IDialogService.ShowError` и пробрасывается дальше.

То есть «показ строки пользователю» из `DefaultResultProcessor` здесь заменён на журналирование; видимый эффект дают только типизированные результаты через Visitor.

### Visitor для типизированных результатов

- `IProcessorResult` — маркер с `Accept(IProcessor)`. Абстрактный базовый класс `ProcessorResult` (в `Base/`).
- `IProcessor` — «посетитель» с перегрузками `Visit(TextFileResult)` и `Visit(ClipboardResult)`. `sealed`-реализация `Processor` (DI-зависимость `ILogger<Processor>`):
  - `Visit(TextFileResult)` — `SaveFileDialog` (стартовая папка — рабочий стол, фильтр по расширению файла) и `File.WriteAllText`; итог (сохранено в путь / отменено пользователем) пишется в лог. Возвращает `void` — вызывающий `AdvancedResultProcessor` результат обработки не получает по контракту.
  - `Visit(ClipboardResult)` — `Clipboard.SetText`, факт копирования логируется.
- Готовые результаты (в `Results/`), оба `sealed`:
  - `TextFileResult(string fileName, string text)` — `.txt` дописывается только если у `fileName` нет расширения; явное расширение (`.csv`, `.json`, `.sql`, …) сохраняется.
  - `ClipboardResult(string clipboardData)`.

Добавить новый вид результата = новый класс `: ProcessorResult` в `Results/` + перегрузка `Visit(...)` в `IProcessor` и `Processor`. Это меняет публичный контракт `IProcessor` — соответствует подъёму версии всего Framework.

### Регистрация в DI

`ServiceCollectionExtensions.AddAdvancedResultProcessor(this IServiceCollection)`:

```csharp
source.AddScoped<IProcessor, Processor>();
source.TryAddScoped<IResultProcessor, AdvancedResultProcessor>();
```

`TryAdd` — чтобы не перетереть `IResultProcessor`, уже зарегистрированный в `Shell` (там же закомментирован альтернативный `AddResultProcessor<DefaultResultProcessor>()`).

## Известные проблемы

- **Имя папки проекта ≠ имя проекта.** Папка на диске — `src/Calabonga.Commandex.Processors/`, файл проекта внутри — `Calabonga.Commandex.Engine.Processors.csproj`. `AssemblyName`/`RootNamespace` MSBuild берёт из имени `.csproj`, поэтому сборка, `PackageId` и namespace корректны и консистентны (`Calabonga.Commandex.Engine.Processors`), `.sln` и CI (ссылаются на `*.sln`) не ломаются. Это только навигационное неудобство: имя папки намекает на несуществующий namespace `Calabonga.Commandex.Processors`. Чистится переименованием папки + правкой одного пути в `.sln`.

Исправлено в ходе ревизии: удалён дубль `Extensions/ProcessorResult.cs`; `AdvancedResultProcessor`, `Processor`, `ClipboardResult` помечены `sealed`; `Processor` логирует исход каждой обработки; `TextFileResult` сохраняет явное расширение файла.
