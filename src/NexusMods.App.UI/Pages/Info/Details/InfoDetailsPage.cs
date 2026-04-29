using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using NexusMods.Abstractions.Diagnostics;
using NexusMods.Abstractions.Loadouts;
using NexusMods.App.UI.Controls.MarkdownRenderer;
using NexusMods.App.UI.WorkspaceSystem;
using NexusMods.Sdk.Loadouts;

namespace NexusMods.App.UI.Pages.Info;

public record InfoDetailsPageContext : IPageFactoryContext
{
    public required LoadoutId LoadoutId { get; init; }

    /// <inheritdoc/>
    public bool IsEphemeral => true;

    /// <inheritdoc/>
    public PageData GetSerializablePageData()
    {
        return new PageData
        {
            FactoryId = InfoListPageFactory.StaticId,
            Context = new InfoListPageContext
            {
                LoadoutId = LoadoutId,
            },
        };
    }
}

[UsedImplicitly]
public class InfoDetailsPageFactory : APageFactory<IInfoDetailsViewModel, InfoDetailsPageContext>
{
    public static readonly PageFactoryId StaticId = PageFactoryId.From(Guid.Parse("96A85EAB-3748-4D30-8212-7A09CCDA225C"));

    public override PageFactoryId Id => StaticId;

    public InfoDetailsPageFactory(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override IInfoDetailsViewModel CreateViewModel(InfoDetailsPageContext context)
    {
        return new InfoDetailsViewModel(
            WindowManager
            // ServiceProvider.GetRequiredService<IDiagnosticWriter>(),
            // ServiceProvider.GetRequiredService<IMarkdownRendererViewModel>()
        );
    }
}
