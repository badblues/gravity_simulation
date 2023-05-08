namespace GravitationSim
{
  public static class Extensions
  {
    public static (float, float) AsNormalizedXY(this CelestialObject obj)
    {
      float scale = 0.00000000001f;
      //Console.WriteLine($"obj = {obj.Name}, {((float)obj.X * scale, (float)obj.Y * scale)}");
      return ((float)obj.X * scale, (float)obj.Y * scale);
    }

    public static float AsNormalizedRadius(this CelestialObject obj)
    {
      float scale = 0.0000001f;
      return (float)obj.Radius * scale;
    }

    public static double DistanceTo(this CelestialObject obj1, CelestialObject obj2)
    {
      //Console.WriteLine($"dist = {Math.Sqrt(Math.Pow(obj1.X - obj2.X, 2) + Math.Pow(obj1.Y - obj2.Y, 2))}");
      return Math.Sqrt(Math.Pow(obj1.X - obj2.X, 2) + Math.Pow(obj1.Y - obj2.Y, 2));
    }

    public static (double, double) DirectionTo(this CelestialObject obj1, CelestialObject obj2)
    {
       double distance = Math.Sqrt(Math.Pow(obj1.X - obj2.X, 2) + Math.Pow(obj1.Y + obj2.Y, 2));
       return ((obj2.X - obj1.X) / distance, (obj2.Y - obj1.Y) / distance);
    }
  }
}