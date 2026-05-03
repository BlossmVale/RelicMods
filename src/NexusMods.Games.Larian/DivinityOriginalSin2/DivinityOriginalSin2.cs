using System.Collections.Immutable;
using DynamicData.Kernel;
using Microsoft.Extensions.DependencyInjection;
using NexusMods.Abstractions.Diagnostics.Emitters;
using NexusMods.Abstractions.Games;
using NexusMods.Abstractions.Library.Installers;
using NexusMods.Abstractions.Loadouts.Synchronizers;
using NexusMods.Games.Generic.Installers;
using NexusMods.Games.Larian.DivinityOriginalSin2.Emitters;
using NexusMods.Games.Larian.DivinityOriginalSin2.Installers;
using NexusMods.Paths;
using NexusMods.Paths.Utilities;
using NexusMods.Sdk.Games;
using NexusMods.Sdk.IO;

namespace NexusMods.Games.Larian.DivinityOriginalSin2;

public class DivinityOriginalSin2 : IGame, IGameData<DivinityOriginalSin2>
{
    public static GameId GameId { get; } = GameId.From("Larian.DivinityOriginalSin2");
    public static string DisplayName => "Divinity Original Sin 2";
    public static Optional<Sdk.NexusModsApi.NexusModsGameId> NexusModsGameId => Sdk.NexusModsApi.NexusModsGameId.From(2569);

    public StoreIdentifiers StoreIdentifiers { get; } = new(GameId)
    {
        SteamAppIds = [435150u],
    };

    public IStreamFactory IconImage { get; } = new EmbeddedResourceStreamFactory<DivinityOriginalSin2>("NexusMods.Games.Larian.Resources.DivinityOriginalSin2.thumbnail.webp");
    public IStreamFactory TileImage { get; } = new EmbeddedResourceStreamFactory<DivinityOriginalSin2>("NexusMods.Games.Larian.Resources.DivinityOriginalSin2.tile.webp");

    private readonly Lazy<ILoadoutSynchronizer> _synchronizer;
    public ILoadoutSynchronizer Synchronizer => _synchronizer.Value;
    public ILibraryItemInstaller[] LibraryItemInstallers { get; }
    private readonly Lazy<ISortOrderManager> _sortOrderManager;
    public ISortOrderManager SortOrderManager => _sortOrderManager.Value;
    public IDiagnosticEmitter[] DiagnosticEmitters { get; }

    public DivinityOriginalSin2(IServiceProvider provider)
    {
        _synchronizer = new Lazy<ILoadoutSynchronizer>(() => new DivinityOriginalSin2Synchronizer(provider));
        _sortOrderManager = new Lazy<ISortOrderManager>(() =>
        {
            var sortOrderManager = provider.GetRequiredService<SortOrderManager>();
            sortOrderManager.RegisterSortOrderVarieties([], this);
            return sortOrderManager;
        });

        DiagnosticEmitters = [provider.GetRequiredService<DependencyDiagnosticEmitter>()];

        LibraryItemInstallers =
        [
            new GenericPatternMatchInstaller(provider) {
                InstallFolderTargets = [
                    new InstallFolderTarget {
                        DestinationGamePath = new GamePath(Dos2Constants.ModsLocationId, ""),
                        KnownValidFileExtensions = [Dos2Constants.PakFileExtension],
FileExtensionsToDiscard =
                        [
                            KnownExtensions.Txt, KnownExtensions.Md, KnownExtensions.Pdf, KnownExtensions.Png,
                            KnownExtensions.Json, new Extension(".lnk"),
                        ],
                    },
new InstallFolderTarget
                    {
                        DestinationGamePath = new GamePath(LocationId.Game, "DefEd"),
                        KnownSourceFolderNames = ["DefEd"],
                        Names = ["Generated", "Public"],
                    },
                ]
            }
            // new DOS2SEInstaller(provider),
            // new GenericPatternMatchInstaller(provider)
            // {
            //     InstallFolderTargets =
            //     [
            //         // Pak mods
            //         // Examples:
            //         // - <see href="https://www.nexusmods.com/baldursgate3/mods/366?tab=description">ImpUI (ImprovedUI) Patch7Ready</see>
            //         // - <see href="https://www.nexusmods.com/baldursgate3/mods/11373?tab=description">NPC Visual Overhaul (WIP) - NPC VO</see>
            //         new InstallFolderTarget
            //         {
            //             DestinationGamePath = new GamePath(Dos2Constants.ModsLocationId, ""),
            //             KnownValidFileExtensions = [Dos2Constants.PakFileExtension],
            //             FileExtensionsToDiscard =
            //             [
            //                 KnownExtensions.Txt, KnownExtensions.Md, KnownExtensions.Pdf, KnownExtensions.Png,
            //                 KnownExtensions.Json, new Extension(".lnk"),
            //             ],
            //         },

            //         // bin and NativeMods mods
            //         // Examples:
            //         // - <see href="https://www.nexusmods.com/baldursgate3/mods/944">Native Mod Loader</see>
            //         // - <see href="https://www.nexusmods.com/baldursgate3/mods/668?tab=files">Achievement Enabler</see>
            //         new InstallFolderTarget
            //         {
            //             DestinationGamePath = new GamePath(LocationId.Game, "bin"),
            //             KnownSourceFolderNames = ["bin"],
            //             Names = ["NativeMods"],
            //         },

            //         // loose files Data mods
            //         // Examples:
            //         // - <see href="https://www.nexusmods.com/baldursgate3/mods/555?tab=description">Fast XP</see>
            //         new InstallFolderTarget
            //         {
            //             DestinationGamePath = new GamePath(LocationId.Game, "Data"),
            //             KnownSourceFolderNames = ["Data"],
            //             Names = ["Generated", "Public"],
            //         },
            //     ],
            // },
        ];
    }

    public ImmutableDictionary<LocationId, AbsolutePath> GetLocations(IFileSystem fileSystem, GameLocatorResult gameLocatorResult)
    {
        return new Dictionary<LocationId, AbsolutePath>()
        {
            { LocationId.Game, gameLocatorResult.Path },
            { Dos2Constants.ModsLocationId, gameLocatorResult.LinuxCompatabilityDataProvider!.WinePrefixDirectoryPath.Combine("drive_c/users/steamuser/Documents").Combine("Larian Studios/Divinity Original Sin 2 Definitive Edition/Mods") },
            { Dos2Constants.PlayerProfilesLocationId, fileSystem.GetKnownPath(KnownPath.MyDocumentsDirectory).Combine("Larian Studios/Divinity Original Sin 2 Definitive Edition/PlayerProfiles") },
            // { Dos2Constants.ScriptExtenderConfigLocationId, fileSystem.GetKnownPath(KnownPath.LocalApplicationDataDirectory).Combine("Larian Studios/Baldur's Gate 3/ScriptExtender") },
        }.ToImmutableDictionary();
    }

    public GamePath GetPrimaryFile(GameInstallation installation)
    {
        if (installation.LocatorResult.TargetOS.IsOSX) return new GamePath(LocationId.Game, "Contents/MacOS/Baldur's Gate 3");

        // Use launcher to allow choosing between DirectX11 and Vulkan on GOG, Steam already always starts the launcher
        return new GamePath(LocationId.Game, "DefEd/bin/EoCApp.exe");
    }
}
