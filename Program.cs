using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace GravitationSim
{
  public class Program
  {
    public static void Main()
    {
      using (MainWindow window = new MainWindow(
        GameWindowSettings.Default,
        new NativeWindowSettings() {
          Size = (1000, 1000),
          Location = (50, 50),
          WindowBorder = WindowBorder.Fixed,
          WindowState = WindowState.Normal,
          Title = "Gravity Simulation",
          Flags = ContextFlags.Default,
          Profile = ContextProfile.Compatability,
          API = ContextAPI.OpenGL, 
        }))
      {
        window.Run();
      }
    }
  }
}