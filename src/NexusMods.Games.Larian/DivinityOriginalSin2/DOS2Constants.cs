
using NexusMods.Paths;
using NexusMods.Sdk.Games;

namespace NexusMods.Games.Larian.DivinityOriginalSin2;

public static class Dos2Constants
{
    public static readonly Extension PakFileExtension = new(".pak");

    public static readonly LocationId ModsLocationId = LocationId.From("Mods");

    public static readonly LocationId PlayerProfilesLocationId = LocationId.From("PlayerProfiles");

    public static readonly LocationId ScriptExtenderConfigLocationId = LocationId.From("ScriptExtenderConfig");

    public static readonly GamePath BG3SEGamePath = new(LocationId.Game, "bin/DWrite.dll");
}
