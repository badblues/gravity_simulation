using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using GravitySim.Shaders;

namespace GravitySim
{
  public class MainWindow : GameWindow
  {

    private int _vertexBufferObject;
    private int _vertexArrayObject;

    float[] vertices = {
      -0.5f, -0.5f, 0.0f, //Bottom-left vertex
      0.5f, -0.5f, 0.0f, //Bottom-right vertex
      0.0f,  0.5f, 0.0f  //Top vertex
    };

    Shader _shader;

    public MainWindow(int width, int height, string title)
    : base(GameWindowSettings.Default, new NativeWindowSettings()
      {
        Size = (width, height),
        Title = title
      }
    ) {}

    protected override void OnLoad()
    {
      base.OnLoad();
      
      GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

      _vertexBufferObject = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
      GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
      
      _vertexArrayObject = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject);

      
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);

      _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
      _shader.Use();
    }



    protected override void OnRenderFrame(FrameEventArgs args)
    {
      base.OnRenderFrame(args);

      GL.Clear(ClearBufferMask.ColorBufferBit);

      _shader.Use();

      GL.BindVertexArray(_vertexArrayObject);

      GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

      SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
      base.OnUpdateFrame(args);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);

      GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnUnload()
    {

      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.BindVertexArray(0);
      GL.UseProgram(0);

      GL.DeleteBuffer(_vertexBufferObject);
      GL.DeleteVertexArray(_vertexArrayObject);

      GL.DeleteProgram(_shader.handle);

      _shader.Dispose();

      base.OnUnload();
    }
  }
}