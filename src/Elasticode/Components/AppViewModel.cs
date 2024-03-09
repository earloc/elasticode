﻿
using CommunityToolkit.Mvvm.ComponentModel;

namespace Elasticode;

public partial class AppViewModel : ObservableObject
{
    [ObservableProperty]
    private string? globalSearchText;

    public event EventHandler<string?>? GlobalSearchTextChanged;

    partial void OnGlobalSearchTextChanged(string? value)
    {
        GlobalSearchTextChanged?.Invoke(this, value);
    }
}
