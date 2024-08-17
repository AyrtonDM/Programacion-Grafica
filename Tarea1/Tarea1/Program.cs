using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Tarea1;


class Program
{
    static void Main(String[] args)
    {
        //Game game = new Game(800, 600, "T 3D");
        //game.Run();
        var nativeWindowSettings = new NativeWindowSettings()
        {
            Size = new Vector2i(1000, 900),
            Title = "Letra T 3D",
            Flags = ContextFlags.ForwardCompatible
        };

        using (var window = new TLetterWindow(GameWindowSettings.Default, nativeWindowSettings))
        {
            window.Run();
        }

    }


}