namespace GravitationSim
{
  public static class Extensions
  {
    public static (float, float) ScaledXY(this CelestialObject obj)
    {
      float scale = 0.0000000001f;
      //Console.WriteLine($"obj = {obj.Name}, {((float)obj.X * scale, (float)obj.Y * scale)}");
      return ((float)obj.X * scale, (float)obj.Y * scale);
    }

    public static float ScaledRadius(this CelestialObject obj)
    {
      float scale = 0.00000001f;
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
       Console.WriteLine($"obj1 = {obj1.Name}, obj2 = {obj2.Name}, direction = {((obj2.X - obj1.X) / distance, (obj2.Y - obj1.Y) / distance)}");
       return ((obj2.X - obj1.X) / distance, (obj2.Y - obj1.Y) / distance);
    }
  }
}