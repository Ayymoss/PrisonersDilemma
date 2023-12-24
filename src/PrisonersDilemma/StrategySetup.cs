using System.Reflection;
using PrisonersDilemma.Caches;
using PrisonersDilemma.Strategy.Interfaces;

namespace PrisonersDilemma;

public class StrategySetup(StrategiesCache strategiesCache)
{
    public IEnumerable<IStrategy> SetupStrategies()
    {
        var pluginsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "_Strategies");

        foreach (var dllPath in Directory.GetFiles(pluginsPath, "*.dll"))
        {
            var assembly = Assembly.LoadFile(dllPath);

            foreach (var pluginType in assembly.GetTypes().Where(t => typeof(IStrategy).IsAssignableFrom(t)))
            {
                IStrategy? strategy = null;
                try
                {
                    strategy = (IStrategy?)Activator.CreateInstance(pluginType);
                }
                catch
                {
                    // ignored 
                }

                if (strategy is null) continue;
                yield return strategy;
            }
        }
    }
}
