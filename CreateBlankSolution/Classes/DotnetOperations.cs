using System;
using System.Diagnostics;
using System.IO;

namespace CreateBlankSolution.Classes
{
    public class DotnetOperations
    {
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
