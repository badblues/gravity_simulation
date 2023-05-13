using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Newtonsoft.Json;

namespace GravitationSim
{
  public class Scene
  {

    private List<CelestialObject> _celestialObjects;
    private List<(float, float)> _trails = new List<(float, float)>();
    private Painter _painter = new Painter();

    public Scene() {
      try {
        string jsonString = File.ReadAllText("Objects.json");
        _celestialObjects = JsonConvert.DeserializeObject<List<CelestialObject>>(jsonString);
      }
      catch (Exception ex) {
          Console.WriteLine("Error reading or deserializing Objects.json: " + ex.Message);
          _celestialObjects = new List<CelestialObject>();
      }
      if (_celestialObjects is null)
      {
        Console.WriteLine("Objects.json not provided.");
        _celestialObjects = new List<CelestialObject>();
      }
    }

    public void DrawAll()
    {
      foreach(CelestialObject obj in _celestialObjects)
      {
        _painter.DrawCelestialObject(obj);
      }
    }

    public void UpdatePositions(double dt)
    {
      var newCoordinates = new List<(CelestialObject, double, double)>();
      foreach(CelestialObject obj1 in _celestialObjects)
      {
        (double, double) forceVec = (0, 0);
        foreach(CelestialObject obj2 in _celestialObjects) if (obj1 != obj2)
        {
          double g = 6.67E-11;
          double force = g * obj1.Mass * obj2.Mass / Math.Pow(obj1.DistanceTo(obj2), 2);
          (double, double) nVec = obj1.DirectionTo(obj2);
          forceVec = (forceVec.Item1 + force * nVec.Item1,
                      forceVec.Item2 + force * nVec.Item2);
        }
        (double, double) accelerationVec = (forceVec.Item1 / obj1.Mass, forceVec.Item2 / obj1.Mass);
        obj1.Velocity = (obj1.Velocity.Item1 + accelerationVec.Item1 * dt,
                         obj1.Velocity.Item2 + accelerationVec.Item2 * dt);
        double x = obj1.X + obj1.Velocity.Item1 * dt;
        double y = obj1.Y + obj1.Velocity.Item2 * dt;
        newCoordinates.Add((obj1, x, y));
      }
      foreach((CelestialObject, double, double) update in newCoordinates)
      {
        Console.WriteLine($"obj = {update.Item1.Name}, {(update.Item2, update.Item3)}");
        update.Item1.X = update.Item2;
        update.Item1.Y = update.Item3;
      }
    }
  }
}