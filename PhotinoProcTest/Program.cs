using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"PhotinoWindow/PhotinoAOT.exe");

        Console.WriteLine(Path.Combine(GetProjectRoot()));

        try
        {
            // Start the process
            Process process = StartExternalProcess(exePath);

            if (process != null)
            {
                Console.WriteLine($"Process {process.ProcessName} started with ID {process.Id}");

                // Wait for the process to exit
                process.WaitForExit();

                Console.WriteLine($"Process {process.ProcessName} exited with code {process.ExitCode}");
            }
            else
            {
                Console.WriteLine("Failed to start the process.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        Console.WriteLine("Waiting 5 seconds for second window to launch");
        // waiting for 5 seconds
        Thread.Sleep(5000);
       
        try
        {
            // Start the process
            Process process = StartExternalProcess(exePath);

            if (process != null)
            {
                Console.WriteLine($"Process {process.ProcessName} started with ID {process.Id}");

                // Wait for the process to exit
                process.WaitForExit();

                Console.WriteLine($"Process {process.ProcessName} exited with code {process.ExitCode}");
            }
            else
            {
                Console.WriteLine("Failed to start the process.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static Process StartExternalProcess(string exePath)
    {
        // Create a new process start info
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = exePath,
            // You can set additional properties like working directory, command-line arguments, etc.
            // WorkingDirectory = @"C:\Path\To\Working\Directory",
            // Arguments = "your command line arguments here",
            UseShellExecute = true, // Use the operating system shell to start the process
            RedirectStandardOutput = false, // Set to true if you want to capture the standard output
            RedirectStandardError = false, // Set to true if you want to capture the standard error
            CreateNoWindow = false // Set to true if you want to run the process without creating a new window
        };

        // Start the process
        return Process.Start(startInfo);
    }

    static string GetProjectRoot()
    {
        // Get the current directory of the executable
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Go up in the directory hierarchy until finding the solution file or reaching the root
        while (!File.Exists(Path.Combine(currentDirectory, "YourSolutionFileName.sln")) && Directory.GetParent(currentDirectory) != null)
        {
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        }

        return currentDirectory;
    }
}
