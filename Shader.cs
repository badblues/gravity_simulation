using OpenTK.Graphics.OpenGL4;

namespace GravitySim.Shaders
{
  public class Shader : IDisposable
  {
    public int handle;
    int vertexShader;
    int fragmentShader;
    private bool disposedValue = false;

    public Shader(string vertexPath, string fragmentPath)
    {
      string vertexShaderSource = File.ReadAllText(vertexPath);
      string fragmentShaderSource = File.ReadAllText(fragmentPath);

       vertexShader = GL.CreateShader(ShaderType.VertexShader);
      GL.ShaderSource(vertexShader, vertexShaderSource);

      fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
      GL.ShaderSource(fragmentShader, fragmentShaderSource);

      GL.CompileShader(vertexShader);

      GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int success);
      if (success == 0)
      {
          string infoLog = GL.GetShaderInfoLog(vertexShader);
          Console.WriteLine(infoLog);
      }

      GL.CompileShader(fragmentShader);

      GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
      if (success == 0)
      {
          string infoLog = GL.GetShaderInfoLog(fragmentShader);
          Console.WriteLine(infoLog);
      }

      handle = GL.CreateProgram();

      GL.AttachShader(handle, vertexShader);
      GL.AttachShader(handle, fragmentShader);

      GL.LinkProgram(handle);

      GL.GetProgram(handle, GetProgramParameterName.LinkStatus, out success);
      if (success == 0)
      {
        string infoLog = GL.GetProgramInfoLog(handle);
        Console.WriteLine(infoLog);
      }

      GL.DetachShader(handle, vertexShader);
      GL.DetachShader(handle, fragmentShader);
      GL.DeleteShader(fragmentShader);
      GL.DeleteShader(vertexShader);
    }

    public void Use()
    {
      GL.UseProgram(handle);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        GL.DeleteProgram(handle);
        disposedValue = true;
      }
    }

    ~Shader()
    {
      if (disposedValue == false)
      {
        Console.WriteLine("GPU Resource leak!");
      }
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}