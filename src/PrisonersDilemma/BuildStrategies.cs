using System.Reflection;
using PrisonersDilemma.Abstraction.Interfaces;

namespace PrisonersDilemma;

public class BuildStrategies
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
                    strategy = Activator.CreateInstance(pluginType) as IStrategy;
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
