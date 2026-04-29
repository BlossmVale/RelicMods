using Microsoft.Extensions.DependencyInjection;

using NexusMods.Abstractions.Loadouts.Synchronizers;
using NexusMods.Sdk.Games;
using NexusMods.Sdk.Settings;

namespace NexusMods.Games.Larian.DivinityOriginalSin2;

public class DivinityOriginalSin2Synchronizer : ALoadoutSynchronizer
{
    private readonly DivinityOriginalSin2Settings _settings;

    private static GamePath GameFolder => new(LocationId.Game, "");
    private static GamePath PublicPlayerProfiles => new(Dos2Constants.PlayerProfilesLocationId, "");
    private static GamePath ModSettingsFile => new(Dos2Constants.PlayerProfilesLocationId, "modsettings.lsx");

    public DivinityOriginalSin2Synchronizer(IServiceProvider provider) : base(provider)
    {
        var settingsManager = provider.GetRequiredService<ISettingsManager>();
        _settings = settingsManager.Get<DivinityOriginalSin2Settings>();
    }

    protected override IGamePathFilter GamePathFilter { get; } = Abstractions.Loadouts.Synchronizers.GamePathFilters.Create(path =>
    {
        // ignore all files inside the public player profiles directory except the modsettings.lsx file
        if (path.InFolder(PublicPlayerProfiles)) return !path.Equals(ModSettingsFile);
        return false;
    });

    public override bool IsIgnoredBackupPath(GamePath path)
    {
        if (_settings.DoFullGameBackup) return false;
        return path.InFolder(GameFolder) || (path.InFolder(PublicPlayerProfiles) && path.Path != ModSettingsFile.Path);
    }
}
