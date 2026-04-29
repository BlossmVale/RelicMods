using Microsoft.Extensions.DependencyInjection;
using NexusMods.Abstractions.Games;
using NexusMods.Abstractions.Loadouts;
using NexusMods.Sdk.Settings;
using NexusMods.Games.Larian.DivinityOriginalSin2.Emitters;
using NexusMods.Games.Larian.DivinityOriginalSin2.RunGameTools;

namespace NexusMods.Games.Larian.DivinityOriginalSin2;

public static class Services
{
    public static IServiceCollection AddDivinityOriginalSin2(this IServiceCollection services)
    {
        services
            .AddGame<DivinityOriginalSin2>()
            .AddSingleton<ITool, DOS2RunGameTool>()
            .AddSettings<DivinityOriginalSin2Settings>()
            .AddPipelines()
            // diagnostics
            .AddSingleton<DependencyDiagnosticEmitter>();

        return services;
    }
}
