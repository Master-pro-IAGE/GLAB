﻿using Ids.Files.Foundations;
using Microsoft.AspNetCore.Components;

namespace Ids.Files.Views;

public partial class ViewFile : ComponentBase
{
    [Inject] private IFileService fileService { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public FileId FileId { get; set; }
    private bool fileExists;
    [Parameter] public string FileIdParamName { get; set; } = "FileId";
    [Parameter] public string Route { get; set; } = $"api/Files/load";

    private string path => $"{Route}/{FileId.Value.ToString()}";

    protected override void OnInitialized() =>
        fileExists = !string.IsNullOrEmpty(FileId?.Value.ToString()) && fileService.FileExists(FileId!);
}