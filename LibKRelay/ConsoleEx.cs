#define USE_EXT_LOGGING

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibKRelay
{
    /// <summary>
    /// Provides a suite of helper methods for uniform console interaction.
    /// </summary>
    public static class ConsoleEx
    {
        private static object ioLock = new object();
        private static ConsoleColor lastFore;
        private static ConsoleColor lastBack;

        private static void RememberColors()
        {
            Monitor.Enter(ioLock);
            lastFore = Console.ForegroundColor;
            lastBack = Console.BackgroundColor;
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        private static void RestoreColors()
        {
            Input();
            Console.ForegroundColor = lastFore;
            Console.BackgroundColor = lastBack;
            Monitor.Exit(ioLock);
        }

        /// <summary>
        /// Prints a message to the console. Thread safe.
        /// </summary>
        public static void WriteLine(string message)
        {
            lock (ioLock)
                Console.WriteLine(message);
        }

        /// <summary>
        /// Prints a message with the desired forecolor. Thread safe.
        /// </summary>
        /// <param name="forecolor">Forecolor of the message</param>
        /// <param name="message">The message to be printed</param>
        public static void WriteLine(ConsoleColor forecolor, string message)
        {
            RememberColors();
            Console.ForegroundColor = forecolor;
            Console.WriteLine(message);
            RestoreColors();
        }

        /// <summary>
        /// Prints a message with the desired colors. Thread safe.
        /// </summary>
        /// <param name="backcolor">Backcolor of the message</param>
        /// <param name="forecolor">Forecolor of the message</param>
        /// <param name="message">The message to be printed</param>
        public static void WriteLine(ConsoleColor backcolor, ConsoleColor forecolor, string message)
        {
            RememberColors();
            Console.ForegroundColor = forecolor;
            Console.BackgroundColor = backcolor;
            Console.WriteLine(message);
            RestoreColors();
        }

        /// <summary>
        /// Uniformly asks for input. Thread safe.
        /// </summary>
        public static void Input()
        {
#if USE_EXT_LOGGING
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("< ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(' ');
#endif
        }

        /// <summary>
        /// Prints a standard message to the console. Thread safe.
        /// </summary>
        public static void Log(string message)
        {
#if USE_EXT_LOGGING
            RememberColors();
            foreach (string line in message.Split('\n'))
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("> ");

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(' ');

                //Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(line);
            }
            RestoreColors();
#else
            Console.WriteLine(message);
#endif
        }

        /// <summary>
        /// Prints a warning to the console. Thread safe.
        /// </summary>
        public static void Warn(string warning)
        {
#if USE_EXT_LOGGING
            RememberColors();
            foreach (string line in warning.Split('\n'))
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("> ");

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(' ');

                //Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(line);
            }
            RestoreColors();
#else
            Console.WriteLine(message);
#endif
        }

        /// <summary>
        /// Prints an error to the console. Thread safe.
        /// </summary>
        public static void Error(string error)
        {
#if USE_EXT_LOGGING
            RememberColors();
            foreach (string line in error.Split('\n'))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("> ");

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(' ');

                //Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(line);
            }
            RestoreColors();
#else
            Console.WriteLine(message);
#endif
        }

        /// <summary>
        /// Prints an positive message to the console. Thread safe.
        /// </summary>
        public static void Ok(string message)
        {
#if USE_EXT_LOGGING
            RememberColors();
            foreach (string line in message.Split('\n'))
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("> ");

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(' ');

                //Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(line);
            }
            RestoreColors();
#else
            Console.WriteLine(message);
#endif
        }
    }
}