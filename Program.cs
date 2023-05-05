namespace GravitySim
{
  public class Program
  {
    public static void Main()
    {
      using (MainWindow window = new MainWindow(1280, 720, "GravitySim"))
      {
        window.Run();
      }
    }
  }
}