using NLog;
using System;
using System.IO;
using System.Configuration;

namespace FileMover
{
    class Program
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {               
                if (args.Length > 0)
                {
                    log.Info("Getting source and target from args");
                    
                    string source = args[0];
                    string target = args[1];

                    if (!String.IsNullOrEmpty(source) && !String.IsNullOrEmpty(target))
                    {
                        // move recursively
                        FileMover(source, target);
                    }
                }
                else
                {
                    log.Info("Getting source and target from config file");

                    string source = ConfigurationManager.AppSettings["source"];
                    string target = ConfigurationManager.AppSettings["target"];

                    if (!String.IsNullOrEmpty(source) && !String.IsNullOrEmpty(target))
                    {
                        // move recursively 
                        FileMover(source, target);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "An exception has occurred");
            }
        }

        public static void FileMover (string source, string target)
        {
            try
            {
                // get the list of directories from source
                string[] directories = Directory.GetDirectories(source);

                log.Info("Found " + directories.Length + " directories");

                if (directories.Length > 0)
                {
                    foreach (string directory in directories)
                    {
                        if (!String.IsNullOrEmpty(directory))
                        {
                            string subDirectoryName = Path.GetFileName(directory);

                            // create new directory
                            if (!Directory.Exists(Path.Combine(target, subDirectoryName)))
                                Directory.CreateDirectory(Path.Combine(target, subDirectoryName));

                            // Move directory content
                            FileMover(Path.Combine(source, subDirectoryName), Path.Combine(target, Path.Combine(target, subDirectoryName)));
                        }
                    }
                }
                else
                {
                    // get the list of files into an array
                    string[] files = Directory.GetFiles(source);

                    log.Info("Found " + files.Length + " files");

                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        string destination = Path.Combine(target, fileName);

                        log.Info("Moving " + fileName + " to " + destination);

                        // move, override if exists
                        File.Move(file, destination, true);
                    }
                }

                // delete source directory
                if (Directory.Exists(source)) Directory.Delete(source, true);
            }
            catch (Exception ex)
            {
                log.Error(ex, "An exception has occurred");
            }
        }
    }
}