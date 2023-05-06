namespace GravitationSim
{
  public abstract class CelestialObject
  {
    public string Name { get; init; } = string.Empty;
    public float Mass { get; init; }
    public float Radius { get; init; }
    public float X { get; set; }
    public float Y { get; set; }
  }
}