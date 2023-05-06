using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GravitationSim
{
  public class MainWindow : GameWindow
  {

    private float _frameTime = .0f;
    private int _fps = 0;
    private Scene _scene;

    public MainWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
      : base(gameWindowSettings, nativeWindowSettings)
      {
        _scene = new Scene();
      }

    protected override void OnLoad()
    {
      base.OnLoad();

      GL.ClearColor(0.1f, 0.0f, 0.0f, 1.0f);

    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
      base.OnRenderFrame(args);

      GL.Clear(ClearBufferMask.ColorBufferBit);

      _scene.Draw();

      SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
      _frameTime += (float) args.Time;
      _fps++;
      if (_frameTime >= 1.0f)
      {
        Title = $"Gravity simulation - {_fps}";
        _frameTime = 0.0f;
        _fps = 0;
      }

      var key = KeyboardState;

      if (key.IsKeyDown(Keys.Escape))
      {
        Close();
      }

      base.OnUpdateFrame(args);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);
    }

  }
}