using LazyCalculator.Controller;
using LazyCalculator.Model;
using LazyCalculator.View;

namespace LazyCalculator;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        LazinessModel model = new();
        LazinessController controller = new(model);
        MainForm view = new(model, controller);
        
        Application.Run(view);
    }
}