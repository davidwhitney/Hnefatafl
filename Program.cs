using System;
namespace Hnefatafl
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Tafl())
            {
                game.Run();
            }
        }
    }
#endif
}
