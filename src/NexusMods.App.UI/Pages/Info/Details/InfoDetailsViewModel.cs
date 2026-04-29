using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia;
using NexusMods.App.UI.Windows;
using NexusMods.App.UI.WorkspaceSystem;
using ReactiveUI;

namespace NexusMods.App.UI.Pages.Info;

public sealed class InfoDetailsViewModel : APageViewModel<IInfoDetailsViewModel>, IInfoDetailsViewModel
{
    public InfoDetailsViewModel(IWindowManager windowManager)
        : base(windowManager)
    {
        TabTitle = "Info";
    }

    public string Text => "Test";
}