using System;
using System.Diagnostics;
using System.IO;
using CreateBlankSolution.Models;
using Newtonsoft.Json;

namespace CreateBlankSolution.Classes
{
    public class DotnetOperations
    {

        /// <summary>
        /// Root folder to show for creating a new solution
        /// </summary>
        public static string SolutionRootFolder { get; set; }

        /// <summary>
        /// Text to display in label on the form
        /// </summary>
        public static string AboutText { get; set; }

        /// <summary>
        /// Read appsetting.json for getting folder path and about text.
        /// </summary>
        public static void Initialize()
        {
            var settings = JsonConvert.DeserializeObject<ApplicationSettings>(File.ReadAllText("appsettings.json"));
            SolutionRootFolder = settings.SolutionFolder;
            AboutText = settings.About;

        }
        /// <summary>
        /// Create a blank Visual Studio solution.
        /// Solution name is same as folder
        ///     use --name project_name to change the default solution name
        /// </summary>
        /// <param name="folder">Existing folder to create solution under</param>
        /// <returns>success of operation and an exception on failure</returns>
        public static (bool Success, Exception Exception) Create(string folder)
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            try
            {
                Directory.SetCurrentDirectory(folder);

                var start = new ProcessStartInfo
                {
                    FileName = "dotnet.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    Arguments = "new sln --force",
                    CreateNoWindow = true
                };

                using (var process = Process.Start(start))
                {
                    process.EnableRaisingEvents = true;
                    process.WaitForExit();
                }

                return (true, null);
            }
            catch (Exception exception)
            {
                return (false, exception);
            }
            finally
            {
                Directory.SetCurrentDirectory(currentDirectory);
            }

        }
    }
}
