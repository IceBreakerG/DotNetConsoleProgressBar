using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressBar.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WriteLine($"Number of arguments: {args.Length}");
            Console.WriteLine($"Current Console Color: {Console.BackgroundColor.ToString()}");

            foreach (var arg in args)
                Console.WriteLine($"Argument: {arg}");

            //Console.WriteLine($"Number of Records to Process: {count}\n");

            for (var i = 0; i < 100; i++)
            {
                System.Threading.Thread.Sleep(50);

                DisplayConsoleOutput(i, 100, i.ToString());
            }

            Console.CursorVisible = true;
            Console.Write("\nComplete! Press any key to continue...");
            Console.ReadLine();
        }

        static void DisplayConsoleOutput(int CurrentPosition, int TotalRecords, string UserName)
        {
            var CountSpacer = TotalRecords.ToString().Length - CurrentPosition.ToString().Length;

            Console.Write($"\r[Completed: {new String('0', CountSpacer)}{CurrentPosition + 1}/{TotalRecords}] ");
            Console.Write($"Current User: {UserName}");

            DrawTextProgressBar(CurrentPosition + 1, TotalRecords);
        }

        static void DrawTextProgressBar(int progress, int total)
        {
            var spacer = 3 - Math.Round((double)progress / (double)total * 100).ToString().Length;
            float onechunk = 18.0f / total;

            //Draw empty progress bar
            SetCursorPositionAndWrite(50, "[");     //Start
            SetCursorPositionAndWrite(70, "]");     //End
            Console.CursorLeft = 51;

            //Draw filled part
            int position = 51;

            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                SetCursorPositionAndWrite(position++, " ");
            }

            //Draw unfilled part
            //for (int i = position; i <= 31; i++)
            //{
            //    Console.BackgroundColor = ConsoleColor.Black;
            //    SetCursorPositionAndWrite(position++, " ");
            //}

            //Draw Totals
            //Console.BackgroundColor = ConsoleColor.Black;
            Console.ResetColor();
            SetCursorPositionAndWrite(71, $" [{new String(' ', spacer)}{Math.Round((double)progress / (double)total * 100)}%]");
        }

        static void SetCursorPositionAndWrite(int position, string output)
        {
            Console.CursorLeft = position;
            Console.Write(output);
        }
    }
}
