﻿using Calabonga.Commandex.Engine.Dialogs;
using Calabonga.Commandex.Engine.Processors.Results;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace Calabonga.Commandex.Engine.Processors.Base;

/// <summary>
/// Processor object used as the Visitor for the other visitor clients
/// </summary>
public class Processor : IProcessor
{
    private readonly IDialogService _dialogService;

    public Processor(IDialogService dialogService) => _dialogService = dialogService;

    /// <summary>
    /// Process <see cref="TextFileResult"/>
    /// </summary>
    /// <param name="result"></param>
    public void Visit(TextFileResult result)
    {
        var ext = Path.GetExtension(result.FileName);
        var saveFileDialog = new SaveFileDialog
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Filter = $"Commandex file *{ext}|*{ext}"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            File.WriteAllText(saveFileDialog.FileName, result.Text);
        }
    }

    /// <summary>
    /// Process <see cref="ClipboardResult"/>
    /// </summary>
    /// <param name="result"></param>
    public void Visit(ClipboardResult result)
    {
        Clipboard.SetText(result.ClipboardData);
        _dialogService.ShowNotification("Copied");
    }
}