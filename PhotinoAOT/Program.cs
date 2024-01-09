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

                int width = random.Next(400, 800);
                int height = (int)Math.Round(width * 0.625, 0);

                int offset = 20;
                int left = random.Next(offset, workAreaWidth - width - offset);
                int top = random.Next(offset, workAreaHeight - height - offset);

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
                    .WaitForClose();
            }
        }

        static void FirstWindowMessageDelegate(object ? sender, string message)
        {
            if (sender == null)
                return;

            var window = (PhotinoWindow)sender;

            if (message == "first-window")
            {
                var random = new Random();

                int workAreaWidth = window.MainMonitor.WorkArea.Width;
                int workAreaHeight = window.MainMonitor.WorkArea.Height;

                int width = random.Next(400, 800);
                int height = (int)Math.Round(width * 0.625, 0);

                int offset = 20;
                int left = random.Next(offset, workAreaWidth - width - offset);
                int top = random.Next(offset, workAreaHeight - height - offset);

                _childCount++;

                new PhotinoWindow()
                    .SetTitle($"First Window ({_childCount})")
                    .SetUseOsDefaultSize(false)
                    .SetHeight(height)
                    .SetWidth(width)
                    .SetUseOsDefaultLocation(false)
                    .SetTop(top)
                    .SetLeft(left)
                    .RegisterWebMessageReceivedHandler(CloseWindowMessageDelegate)
                    .Load("wwwroot/random.html")
                    .WaitForClose();
            }
        }
    }
}
