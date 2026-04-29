using NexusMods.Abstractions.Games;

namespace NexusMods.Games.Larian.DivinityOriginalSin2.RunGameTools;

public class DOS2RunGameTool : RunGameTool<DivinityOriginalSin2>
{
    public DOS2RunGameTool(IServiceProvider serviceProvider, DivinityOriginalSin2 game) : base(serviceProvider, game)
    {
    }
}
