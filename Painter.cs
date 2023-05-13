using OpenTK.Graphics.OpenGL;

namespace GravitationSim
{
  public class Painter
  {
    public void DrawCelestialObject(CelestialObject obj)
    {
      float x, y;
      GL.Color4(obj.Color);
      (float, float) center = obj.ScaledXY();
      float radius = obj.ScaledRadius();
      float a = (float)Math.PI * 2/100;
      GL.Begin(PrimitiveType.TriangleFan);
      GL.Vertex2(center.Item1, center.Item2);
      for (int i = -1; i < 100; i++)
      {
        x = center.Item1 + (float)Math.Sin(a * i) * radius;
        y = center.Item2 + (float)Math.Cos(a * i) * radius;
        GL.Vertex2(x, y);
      }
      GL.End();
    }
    public void DrawTrail((float, float)[] points)
    {
      GL.Begin(PrimitiveType.Points);
      foreach((float, float) point in points)
      {
        GL.Vertex2(point.Item1, point.Item2);
        GL.Color4(1, 1, 1, 1);
      }
      GL.End();
    }
  }
}