using System;

namespace PL
{
    public static class ConsoleWorker
    {
        public static void WriteItem(object obj) => Console.WriteLine(obj);
        public static void WriteItem(string str, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(str);
            Console.ResetColor();
        }
        public static ConsoleKeyInfo ReadKey() => Console.ReadKey();
    }
}
