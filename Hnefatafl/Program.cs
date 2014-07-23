using System;
namespace Hnefatafl
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameStart())
            {
                game.Run();
            }
        }
    }
#endif
}
