namespace GravitationSim
{
  public static class Extensions
  {
    private const float _distanceScale = 0.0000000001f;
    private const float _radiusScale = 0.00000001f;

    public static (float, float) ScaledXY(this (double, double) coordinates)
    {
      //Console.WriteLine($"obj = {obj.Name}, {((float)obj.X * scale, (float)obj.Y * scale)}");
      return ((float)coordinates.Item1 * _distanceScale, (float)coordinates.Item2 * _distanceScale);
    }

    public static float ScaledRadius(this CelestialObject obj)
    {
      return (float)obj.Radius * _radiusScale;
    }

    public static double DistanceTo(this CelestialObject obj1, CelestialObject obj2)
    {
      //Console.WriteLine($"dist = {Math.Sqrt(Math.Pow(obj1.X - obj2.X, 2) + Math.Pow(obj1.Y - obj2.Y, 2))}");
      return Math.Sqrt(Math.Pow(obj1.Coordinates.Item1 - obj2.Coordinates.Item1, 2) + Math.Pow(obj1.Coordinates.Item2 - obj2.Coordinates.Item2, 2));
    }

    public static (double, double) DirectionTo(this CelestialObject obj1, CelestialObject obj2)
    {
       double distance = Math.Sqrt(Math.Pow(obj1.Coordinates.Item1 - obj2.Coordinates.Item1, 2) + Math.Pow(obj1.Coordinates.Item2 + obj2.Coordinates.Item2, 2));
       //Console.WriteLine($"obj1 = {obj1.Name}, obj2 = {obj2.Name}, direction = {((obj2.Coordinates.Item1 - obj1.Coordinates.Item1) / distance, (obj2.Coordinates.Item2 - obj1.Coordinates.Item2) / distance)}");
       return ((obj2.Coordinates.Item1 - obj1.Coordinates.Item1) / distance, (obj2.Coordinates.Item2 - obj1.Coordinates.Item2) / distance);
    }
  }
}