using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace GravitySim
{
  public class Program
  {
    public static void Main()
    {
      using (MainWindow window = new MainWindow(
        GameWindowSettings.Default,
        new NativeWindowSettings() {
          Size = (600, 450),
          Location = (370, 300),
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