using System.Numerics;

namespace GravitationSim
{
  public class CelestialObject
  {
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public double Mass { get; init; } // kg
    public double Radius { get; init; } // m
    public double X { get; set; }
    public double Y { get; set; }
    public (float, float, float, float) Color { get; init; }
    public (double, double) Velocity { get; set; } // m/s

  }
}