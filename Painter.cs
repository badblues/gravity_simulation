using OpenTK.Graphics.OpenGL;

namespace GravitationSim
{
  public class Painter
  {
    public void DrawCelestialObject(CelestialObject obj)
    {
      float x, y;
      GL.Color4(obj.Color);
      (float, float) center = obj.Coordinates.ScaledXY();
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
    public void DrawTrail(List<(double, double)> points)
    {
      GL.Color4(1.0, 1.0, 1.0, 1.0);
      GL.Begin(PrimitiveType.Points);
      foreach((double, double) point in points)
      {
        (float, float) xy = point.ScaledXY();
        GL.Vertex2(xy.Item1, xy.Item2);
      }
      GL.End();
    }
  }
}