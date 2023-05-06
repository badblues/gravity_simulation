namespace GravitationSim
{
  public static class Extensions
  {
    private static float _scale = 0.0001f;
    public static (float, float) AsNormalizedXY(this CelestialObject obj)
    {
      
      //float centerX = 0;
      //float centerY = 0; 
      return (obj.X * _scale, obj.Y * _scale);
    }

    public static float AsNormalizedRadius(this CelestialObject obj)
    {
      return obj.Radius * _scale;
    }
  }
}