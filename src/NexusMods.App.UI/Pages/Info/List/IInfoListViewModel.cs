using System.Collections.ObjectModel;
using System.Reactive;
using NexusMods.Abstractions.Diagnostics;
using NexusMods.Abstractions.Loadouts;
using NexusMods.Abstractions.Loadouts.Ids;
using NexusMods.App.UI.Controls.Diagnostics;
using NexusMods.App.UI.WorkspaceSystem;
using NexusMods.Sdk.Loadouts;
using ReactiveUI;

namespace NexusMods.App.UI.Pages.Info;

public interface IInfoListViewModel : IPageViewModelInterface
{
    public LoadoutId LoadoutId { get; set; }
}