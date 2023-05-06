using OpenTK.Graphics.OpenGL;

namespace GravitationSim
{
  public class Scene
  {

    private List<CelestialObject> _celestialObjects;

    public Scene() {
      _celestialObjects = new List<CelestialObject>();
      _celestialObjects.Add(new Planet() {
        Name = "Earth",
        Mass = 0.000003f,
        Radius = 6371f,
        X = 0.0f,
        Y = 0.0f
        });
    }

    public void Draw()
    {
      foreach(CelestialObject obj in _celestialObjects)
      {
        DrawPlanet((Planet)obj);
      }
    }

    public void Update()
    {

    }

    public void DrawPlanet(Planet planet)
    {
      float x, y;
      GL.Color4(0.5f, 0.3f, 0.3f, 1.0f);
      (float, float) center = planet.AsNormalizedXY();
      float radius = planet.AsNormalizedRadius();
      float a = (float)Math.PI * 2/100;
      GL.Begin(PrimitiveType.TriangleFan);
      GL.Vertex2(center.Item1, center.Item2);
      for (int i = -1; i < 100; i++)
      {
        x = (float)Math.Sin(a * i) * radius;
        y = (float)Math.Cos(a * i) * radius;
        GL.Vertex2(x, y);
      }
      GL.End();
    }
  }
}