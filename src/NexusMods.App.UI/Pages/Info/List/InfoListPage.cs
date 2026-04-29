using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using NexusMods.Abstractions.Loadouts;
using NexusMods.Abstractions.Loadouts.Ids;
using NexusMods.Abstractions.Serialization.Attributes;
using NexusMods.App.UI.Resources;
using NexusMods.App.UI.WorkspaceSystem;
using NexusMods.UI.Sdk.Icons;
using NexusMods.MnemonicDB.Abstractions;
using NexusMods.Sdk.Loadouts;

namespace NexusMods.App.UI.Pages.Info;

public record InfoListPageContext : IPageFactoryContext
{
    public required LoadoutId LoadoutId { get; init; }
}

[UsedImplicitly]
public class InfoListPageFactory : APageFactory<IInfoListViewModel, InfoListPageContext>
{
    private readonly IConnection _conn;
    public InfoListPageFactory(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _conn = serviceProvider.GetRequiredService<IConnection>();
    }

    public static readonly PageFactoryId StaticId = PageFactoryId.From(Guid.Parse("db77a8c2-61ad-4d59-8e95-4bebbba9ea5b"));

    public override PageFactoryId Id => StaticId;

    public override IInfoListViewModel CreateViewModel(InfoListPageContext context)
    {
        var vm = ServiceProvider.GetRequiredService<IInfoListViewModel>();
        vm.LoadoutId = context.LoadoutId;
        return vm;
    }
}