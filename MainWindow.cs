using OpenTK;
using OpenTK.Graphics;
using OpenTK.Windowing.Desktop;

namespace GravitySim
{
  public class MainWindow : GameWindow
  {
    public MainWindow(int width, int height, string title)
      : base(GameWindowSettings.Default, new NativeWindowSettings()
        {
          Size = (width, height),
          Title = title
        }
      ) {}
  }
}