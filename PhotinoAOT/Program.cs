using PhotinoNET;
using System;

namespace PhotinoAOT
{
    class Program
    {
        static int _childCount;

        [STAThread]
        static void Main(string[] args)
        {
            new PhotinoWindow()
                .SetTitle("Main Window")
                .RegisterWebMessageReceivedHandler(SecondWindowMessageDelegate)
                .RegisterWebMessageReceivedHandler(FirstWindowMessageDelegate)
                .SetUseOsDefaultSize(false)
                .SetWidth(600)
                .SetHeight(400)
                .Center()
                .Load("wwwroot/main.html")
                .WaitForClose();
        }

        static void CloseWindowMessageDelegate(object ? sender, string message)
        {
            if (sender == null)
                return;

            var window = (PhotinoWindow)sender;

            if (message == "close-window")
            {
                Console.WriteLine($"Closing \"{window.Title}\".");
                window.Close();
            }
        }

        static void SecondWindowMessageDelegate(object ? sender, string message)
        {
            if (sender == null)
                return;

            var window = (PhotinoWindow)sender;

            if (message == "second-window")
            {
                var random = new Random();

                int workAreaWidth = window.MainMonitor.WorkArea.Width;
                int workAreaHeight = window.MainMonitor.WorkArea.Height;

                int width = 500;
                int height = (int)Math.Round(width * 0.625, 0);

                int left = workAreaWidth - width;
                int top =workAreaHeight - height;

                _childCount++;

                new PhotinoWindow()
                    .SetTitle($"Second Window ({_childCount})")
                    .SetUseOsDefaultSize(false)
                    .SetHeight(height)
                    .SetWidth(width)
                    .SetUseOsDefaultLocation(false)
                    .SetTop(top)
                    .SetLeft(left)
                    .RegisterWebMessageReceivedHandler(CloseWindowMessageDelegate)
                    .Load("wwwroot/random.html")
                    .SetChromeless(true)
                    .WaitForClose();
            }
        }

        static void FirstWindowMessageDelegate(object? sender, string message)
        {
            if (sender == null)
                return;

            var window = (PhotinoWindow)sender;

            if (message == "first-window")
            {
                window.Size = new System.Drawing.Size(1000, 700);
            }
        }
    }
}
