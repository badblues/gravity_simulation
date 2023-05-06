using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GravitySim
{
  public class MainWindow : GameWindow
  {

    private float _frameTime = .0f;
    private int _fps = 0;
    float[] vertices = new float[] { 
      -0.5f, -0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
      -0.5f,  0.5f, 0.0f,
        0.5f,  0.5f, 0.0f
    };

    float[] colors = new float[] {
      1.0f, 0.0f, 0.0f, 1.0f,
      0.0f, 1.0f, 0.0f, 1.0f,
      0.0f, 0.0f, 1.0f, 1.0f,
      0.8f, 0.6f, 0.2f, 1.0f
    };
    int vaoId = 0;
    int vboVertex = 0;
    int vboColor = 0;

    public MainWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
      : base(gameWindowSettings, nativeWindowSettings) {}

    protected override void OnLoad()
    {
      base.OnLoad();

      GL.ClearColor(0.1f, 0.0f, 0.0f, 1.0f);

      GL.Enable(EnableCap.CullFace);
      GL.CullFace(CullFaceMode.Back);


      vboVertex = CreateVBO(vertices);
      vboColor = CreateVBO(colors);
      vaoId = CreateVAOnoShaders(vertices, colors);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
      base.OnRenderFrame(args);

      GL.Clear(ClearBufferMask.ColorBufferBit);

      DrawVAOnoShaders();

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

    private int CreateVBO(float[] data)
    {
      int vbo = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
      GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      return vbo;
    }

    private int CreateVAOnoShaders(float[] vertices, float[] colors)
    {
      int vao = GL.GenVertexArray();
      GL.BindVertexArray(vao);

      GL.EnableClientState(ArrayCap.VertexArray);
      GL.EnableClientState(ArrayCap.ColorArray);

      GL.BindBuffer(BufferTarget.ArrayBuffer, vboVertex);
      GL.VertexPointer(3, VertexPointerType.Float, 0, 0);

      GL.BindBuffer(BufferTarget.ArrayBuffer, vboColor);
      GL.ColorPointer(4, ColorPointerType.Float, 0, 0);

      GL.BindVertexArray(0);
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.DisableClientState(ArrayCap.VertexArray);
      GL.DisableClientState(ArrayCap.ColorArray);
      return vao;
    }

    private void DrawVAOnoShaders()
    {
      GL.BindVertexArray(vaoId);
      GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);
    }

    protected override void OnUnload()
    {
      DeleteVAOnoShaders();

      base.OnUnload();
    }

    private void DeleteVAOnoShaders()
    {
      GL.BindVertexArray(0);
      GL.DeleteVertexArray(vaoId);
      GL.DeleteBuffer(vboVertex);
      GL.DeleteBuffer(vboColor);
    }
  }
}