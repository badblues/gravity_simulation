namespace GravitationSim
{
  public class CelestialObjectWrapper
  {
    public CelestialObject CelestialObject { get; set; }
    public List<(double, double)> Trail { get; } = new List<(double, double)>();

    public CelestialObjectWrapper(CelestialObject celestialObject) {
      CelestialObject = celestialObject;
    }
  }
}