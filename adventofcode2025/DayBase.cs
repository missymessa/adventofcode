using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace adventofcode2025
{
    public abstract class DayBase<T>
    {
        protected List<string> _input;
        protected string _rawInput;

        public DayBase(string fileName)
        {
            CheckAndFetchInput(fileName);
            _rawInput = LoadInput(fileName);
            _input = LoadInputAsList(fileName);
        }

        private void CheckAndFetchInput(string filename)
        {
            // Try to find the correct input directory
            string inputDir = Path.Combine(Environment.CurrentDirectory, "input");
            
            // If running from test directory, look for the main project's input folder
            if (!Directory.Exists(inputDir))
            {
                var testBinPath = Environment.CurrentDirectory;
                var solutionRoot = Directory.GetParent(testBinPath)?.Parent?.Parent?.Parent?.Parent?.FullName;
                if (solutionRoot != null)
                {
                    inputDir = Path.Combine(solutionRoot, "adventofcode2025", "input");
                }
            }
            
            var filePath = Path.Combine(inputDir, filename);
            
            // Only fetch if file doesn't exist or is very small (placeholder)
            bool shouldFetch = !File.Exists(filePath);
            if (!shouldFetch && File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath).Trim();
                // Fetch if file is empty or contains common placeholder text or is less than 50 bytes
                shouldFetch = string.IsNullOrWhiteSpace(content) || 
                              content == "actual input" || 
                              content == "example input" ||
                              new FileInfo(filePath).Length < 50;
            }
            
            if (shouldFetch)
            {
                // Extract day number from filename (e.g., "day_01.txt" -> 1)
                var dayMatch = System.Text.RegularExpressions.Regex.Match(filename, @"day_(\d+)");
                if (dayMatch.Success && int.TryParse(dayMatch.Groups[1].Value, out int day))
                {
                    var sessionCookie = Environment.GetEnvironmentVariable("AOC_SESSION");
                    if (!string.IsNullOrEmpty(sessionCookie))
                    {
                        Console.WriteLine($"Fetching input for day {day}...");
                        var solutionRoot = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
                        var scriptPath = Path.Combine(solutionRoot, "get-input.ps1");
                        
                        var psi = new ProcessStartInfo
                        {
                            FileName = "powershell.exe",
                            Arguments = $"-ExecutionPolicy Bypass -File \"{scriptPath}\" -Day {day} -Year 2025 -SessionCookie \"{sessionCookie}\"",
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true
                        };
                        
                        using (var process = Process.Start(psi))
                        {
                            process.WaitForExit();
                        }
                    }
                }
            }
        }

        protected string LoadInput(string filename)
        {
            string inputDir = Path.Combine(Environment.CurrentDirectory, "input");
            
            // If running from test directory, look for the main project's input folder
            if (!Directory.Exists(inputDir))
            {
                var testBinPath = Environment.CurrentDirectory;
                var solutionRoot = Directory.GetParent(testBinPath)?.Parent?.Parent?.Parent?.Parent?.FullName;
                if (solutionRoot != null)
                {
                    inputDir = Path.Combine(solutionRoot, "adventofcode2025", "input");
                }
            }
            
            return File.ReadAllText(Path.Combine(inputDir, filename));
        }

        protected List<string> LoadInputAsList(string filename)
        {
            string inputDir = Path.Combine(Environment.CurrentDirectory, "input");
            
            // If running from test directory, look for the main project's input folder
            if (!Directory.Exists(inputDir))
            {
                var testBinPath = Environment.CurrentDirectory;
                var solutionRoot = Directory.GetParent(testBinPath)?.Parent?.Parent?.Parent?.Parent?.FullName;
                if (solutionRoot != null)
                {
                    inputDir = Path.Combine(solutionRoot, "adventofcode2025", "input");
                }
            }
            
            return File.ReadAllLines(Path.Combine(inputDir, filename)).ToList();
        }

        public virtual void Execute()
        {
            Console.WriteLine("Problem 1: {0}", Problem1());
            Console.WriteLine("Problem 2: {0}", Problem2());
        }

        public abstract T Problem1();

        public abstract T Problem2();

    }
}
