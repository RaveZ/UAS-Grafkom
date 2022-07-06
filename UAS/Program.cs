using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace LightFast
{
	class Program
	{
		static void Main(string[] args)
		{
			var ourWindow = new NativeWindowSettings()
			{
				Size = new Vector2i(1500, 1000),
				Title = "Five night at Freedy's"
			};

			using (var window = new Window(GameWindowSettings.Default, ourWindow))
			{
				window.Run();
			}
		}
	}
}
