using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GravitationSim
{
  public class MainWindow : GameWindow
  {

    private float _frameTime = .0f;
    private float _cameraDistance = 1.0f;
    private float _cameraPositionX = 0.0f;
    private float _cameraPositionY = 0.0f;
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
      GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
      base.OnRenderFrame(args);

      GL.Clear(ClearBufferMask.ColorBufferBit);

      //TODO where to put it
      _scene.UpdatePositions(100000);

      GL.Viewport(0, 0, this.Size.X, this.Size.Y);

      Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(
                              MathHelper.PiOver4,    // field of view angle
                              (float)this.Size.X / this.Size.Y, // aspect ratio
                              0.1f,                  // near clipping plane
                              100.0f);               // far clipping plane
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadMatrix(ref perspective);

      Matrix4 modelview = Matrix4.LookAt(
                            new Vector3(_cameraPositionX, _cameraPositionY, _cameraDistance),  // camera position
                            new Vector3(_cameraPositionX, _cameraPositionY, 0.0f),          // look at point
                            Vector3.UnitY);        // up vector
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadMatrix(ref modelview);

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
      var mouse = MouseState;

      if (key.IsKeyDown(Keys.Escape))
      {
        Close();
      }

      if (mouse.IsButtonDown(MouseButton.Left))
      {
          _cameraPositionX -= mouse.Delta.X * 0.0005f * _cameraDistance;
          _cameraPositionY += mouse.Delta.Y * 0.0005f * _cameraDistance;
          Console.WriteLine($"X = {_cameraPositionX}, Y = {_cameraPositionY}");
      }

      _cameraDistance -= mouse.ScrollDelta.Y * 0.1f;
      if (_cameraDistance < 0.1)
      {
        _cameraDistance = 0.1f;
      }

      base.OnUpdateFrame(args);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);
    }

  }
}