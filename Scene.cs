using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Newtonsoft.Json;

namespace GravitationSim
{
  public class Scene
  {

    private List<CelestialObject> _celestialObjects;

    public Scene() {
      string jsonString = File.ReadAllText("Objects.json");
      _celestialObjects = JsonConvert.DeserializeObject<List<CelestialObject>>(jsonString);
      if (_celestialObjects is null)
      {
        Console.WriteLine("Objects.json not provided.");
        _celestialObjects = new List<CelestialObject>();
      }
    }

    public void Draw()
    {
      foreach(CelestialObject obj in _celestialObjects)
      {
        DrawCelestialObject(obj);
      }
    }

    public void UpdatePositions(double speed)
    {
      var newCoordinates = new List<(CelestialObject, double, double)>();
      foreach(CelestialObject obj1 in _celestialObjects)
      {
        (double, double) sumForce = (0, 0);
        foreach(CelestialObject obj2 in  _celestialObjects) if (obj1 != obj2)
        {
          double g = 6.67E-11;
          double force = (g * obj1.Mass * obj2.Mass) / Math.Pow(obj1.DistanceTo(obj2), 2);
          //Console.WriteLine($"Obj1 = {obj1.Name}, Obj2 = {obj2.Name}, force = {force}");
          (double, double) direction = obj1.DirectionTo(obj2);
          sumForce = (sumForce.Item1 + direction.Item1 * force, sumForce.Item2 + direction.Item2 * force);
        }
        (double, double) acceleration = (sumForce.Item1 / obj1.Mass, sumForce.Item2 / obj1.Mass);
        //Console.WriteLine($"Obj = {obj1.Name}, Acceleration = {acceleration}");
        obj1.Velocity = (obj1.Velocity.Item1 + acceleration.Item1, obj1.Velocity.Item2 + acceleration.Item2);
        newCoordinates.Add((obj1, obj1.X + (speed * obj1.Velocity.Item1), obj1.Y + (speed * obj1.Velocity.Item2)));
      }
      foreach((CelestialObject, double, double) update in newCoordinates)
      {
        Console.WriteLine($"obj = {update.Item1.Name}, {(update.Item2, update.Item3)}");
        update.Item1.X = update.Item2;
        update.Item1.Y = update.Item3;
      }
    }

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
  }
}