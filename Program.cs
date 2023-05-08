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
          Size = (1280, 720),
          Location = (50, 50),
          WindowBorder = WindowBorder.Resizable,
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