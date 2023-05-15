using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Newtonsoft.Json;

namespace GravitationSim
{
  public class Scene
  {
    public bool DrawTrail {get; set;} = true;
    private List<CelestialObjectWrapper> _celestialObjects = new List<CelestialObjectWrapper>();
    private List<(float, float)> _trails = new List<(float, float)>();
    private Painter _painter = new Painter();

    public Scene() {
      try {
        string jsonString = File.ReadAllText("Objects.json");
        List<CelestialObject>? readCelestialObjects = JsonConvert.DeserializeObject<List<CelestialObject>>(jsonString);
        if (readCelestialObjects is not null)
          foreach (CelestialObject celestialObject in readCelestialObjects)
          {
            Console.WriteLine($"Loaded object: {celestialObject.Name}");
            _celestialObjects.Add(new CelestialObjectWrapper(celestialObject));
          }
      }
      catch (Exception ex) {
          Console.WriteLine("Error reading or deserializing Objects.json: " + ex.Message);
          _celestialObjects = new List<CelestialObjectWrapper>();
      }
    }

    public void DrawAll()
    {
      foreach(CelestialObjectWrapper obj in _celestialObjects)
      {
        _painter.DrawCelestialObject(obj.CelestialObject);
        if (DrawTrail)
          _painter.DrawTrail(obj.Trail);
      }
    }

    public void UpdatePositions(double dt)
    {
      var newCoordinates = new List<(CelestialObjectWrapper, double, double)>();
      foreach(CelestialObjectWrapper wrapper1 in _celestialObjects)
      {
        CelestialObject obj1 = wrapper1.CelestialObject;
        (double, double) forceVec = (0, 0);
        foreach(CelestialObjectWrapper wrapper2 in _celestialObjects) if (wrapper1 != wrapper2)
        {
          CelestialObject obj2 = wrapper2.CelestialObject;
          double g = 6.67E-11;
          double force = g * obj1.Mass * obj2.Mass / Math.Pow(obj1.DistanceTo(obj2), 2);
          (double, double) nVec = obj1.DirectionTo(obj2);
          forceVec = (forceVec.Item1 + force * nVec.Item1,
                      forceVec.Item2 + force * nVec.Item2);
        }
        (double, double) accelerationVec = (forceVec.Item1 / obj1.Mass, forceVec.Item2 / obj1.Mass);
        obj1.Velocity = (obj1.Velocity.Item1 + accelerationVec.Item1 * dt,
                         obj1.Velocity.Item2 + accelerationVec.Item2 * dt);
        double x = obj1.Coordinates.Item1 + obj1.Velocity.Item1 * dt;
        double y = obj1.Coordinates.Item2 + obj1.Velocity.Item2 * dt;
        newCoordinates.Add((wrapper1, x, y));
      }
      foreach((CelestialObjectWrapper, double, double) update in newCoordinates)
      {
        (double, double) xy = (update.Item2, update.Item3);
        List<(double, double)> trail = update.Item1.Trail;
        //Console.WriteLine($"obj = {update.Item1.CelestialObject.Name}, {(update.Item2, update.Item3)}");
        trail.Add((update.Item1.CelestialObject.Coordinates.Item1, update.Item1.CelestialObject.Coordinates.Item2));
        if (trail.Count >= 1000)
          trail.RemoveAt(0);
        update.Item1.CelestialObject.Coordinates = xy;
      }
    }
  }
}