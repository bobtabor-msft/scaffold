using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scaffold
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Print version number.
            // TODO: Check to make sure all the templates are in the right folder.

            var originalColor = Console.ForegroundColor;

            Console.WriteLine("Hi, I scaffold Learn modules!");
            Console.WriteLine("");
            Console.WriteLine("First, what is the module's UID?");
            string moduleUID = Console.ReadLine();

            Console.WriteLine("");
            Console.Write("Next, please enter each unit UID, starting with ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("'introduction'");
            Console.ForegroundColor = originalColor;
            Console.WriteLine(", selecting [Enter] to add another.");

            Console.Write("When I see ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("'summary'");
            Console.ForegroundColor = originalColor;
            Console.WriteLine(" I'll know you're done.");

            Console.Write("Type ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("'quit'");
            Console.ForegroundColor = originalColor;
            Console.WriteLine(" to exit before generating any files.");

            var unitUIDs = new List<string>();
            while (true)
            {
                var unitUID = Console.ReadLine();

                if (unitUID == "quit") return;

                unitUIDs.Add(unitUID);

                if (unitUIDs.Last() == "summary") break;
            }

            // Do some input checking here ...

            Console.WriteLine("");
            Console.WriteLine("Working ...");
            Console.WriteLine("");


            // Create folders

            string baseDirectory = $"c:\\working\\{moduleUID}";
            System.IO.Directory.CreateDirectory(baseDirectory);
            Console.WriteLine($"Created {baseDirectory}");

            string mediaDirectory = baseDirectory + "\\media";
            System.IO.Directory.CreateDirectory(mediaDirectory);
            Console.WriteLine($"Created {mediaDirectory}");

            string includesDirectory = baseDirectory + "\\includes";
            System.IO.Directory.CreateDirectory(includesDirectory);
            Console.WriteLine($"Created {includesDirectory}");

            // Use templates

            createIndex(moduleUID, unitUIDs, baseDirectory);
            createYamls(moduleUID, unitUIDs, baseDirectory);

            createMarkdowns(moduleUID, unitUIDs, includesDirectory);

            createReference(baseDirectory);

            // Spit out next steps

            Console.WriteLine("");
            Console.WriteLine("Success!");

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Next steps:");
            Console.ForegroundColor = originalColor;
            Console.WriteLine($"Your scaffolded module is saved at: {baseDirectory}");
            Console.WriteLine($"Copy this to your local Learn-PR repo");


            // todo: in the future, perhaps I could automate even more first steps:
            // https://github.com/libgit2/libgit2sharp/wiki

        }

        private static void createReference(string baseDirectory)
        {
            var content = System.IO.File.ReadAllText("c:\\scaffold\\templates\\reference.txt");

            var fileName = baseDirectory + "\\reference.txt";
            System.IO.File.AppendAllText(fileName, content);

            Console.WriteLine("Created the reference.txt");
        }

        static void createIndex(string moduleUID, List<string> unitUIDs, string baseDirectory)
        {

            // Get data from index.txt
            var content = System.IO.File.ReadAllText("c:\\scaffold\\templates\\index.txt");

            // String replace
            content = content.Replace("{{moduleUID}}", moduleUID);

            var myDate = DateTime.Now.ToString("MM/dd/yyyy");
            content = content.Replace("{{date}}", myDate);

            var uids = new StringBuilder();
            foreach (var item in unitUIDs)
            {
                uids.AppendLine("- learn.languages." + moduleUID + "." + item);
            }
            content = content.Replace("{{unitUIDs}}", uids.ToString());

            // Create file, save
            var fileName = baseDirectory + "\\index.yml";
            System.IO.File.AppendAllText(fileName, content);

            Console.WriteLine("Created the index.yml");
        }

        static void createYamls(string moduleUID, List<string> unitUIDs, string baseDirectory)
        {
            var content = System.IO.File.ReadAllText("c:\\scaffold\\templates\\unit-yaml.txt");
            var interactive = System.IO.File.ReadAllText("c:\\scaffold\\templates\\unit-yaml-interactive.txt");

            var myDate = DateTime.Now.ToString("MM/dd/yyyy");

            content = content.Replace("{{date}}", myDate);
            interactive = interactive.Replace("{{date}}", myDate);

            var counter = 0;

            foreach (var unitUID in unitUIDs)
            {
                counter++;
                string newContent = "";

                if (unitUID == "knowledge-check")
                {
                    createKnowledgeCheck(moduleUID, unitUIDs, baseDirectory, counter);
                    continue;
                } else if (unitUID == "introduction" || unitUID == "summary")
                {
                    newContent = content;
                } else
                {
                    newContent = interactive;
                }

                var numberModuleUID = $"{counter}-{unitUID}";
                
                newContent = newContent.Replace("{{numberModuleUID}}", numberModuleUID);

                newContent = newContent.Replace("{{moduleUID}}", moduleUID);
                newContent = newContent.Replace("{{unitUID}}", unitUID);

                var fileName = baseDirectory + "\\" + numberModuleUID + ".yml";
                System.IO.File.AppendAllText(fileName, newContent);

                Console.WriteLine($"Created {fileName}");
            }
        }

        static void createMarkdowns(string moduleUID, List<string> unitUIDs, string includesDirectory)
        {
            var content = System.IO.File.ReadAllText("c:\\scaffold\\templates\\unit-markdown.txt");
            var introduction = System.IO.File.ReadAllText("c:\\scaffold\\templates\\unit-introduction.txt");
            var summary = System.IO.File.ReadAllText("c:\\scaffold\\templates\\unit-summary.txt");

            var exercise = System.IO.File.ReadAllText("c:\\scaffold\\templates\\unit-exercise.txt");
            var challenge = System.IO.File.ReadAllText("c:\\scaffold\\templates\\unit-challenge.txt");
            var solution = System.IO.File.ReadAllText("c:\\scaffold\\templates\\unit-solution.txt");

            var counter = 0;

            foreach (var unitUID in unitUIDs)
            {
                counter++;
                
                if (unitUID == "knowledge-check") continue;

                var numberModuleUID = $"{counter}-{unitUID}";

                var fileName = includesDirectory + "\\" + numberModuleUID + ".md";

                if (unitUID == "introduction")
                {
                    System.IO.File.AppendAllText(fileName, introduction);
                }
                else if (unitUID == "summary")
                {
                    System.IO.File.AppendAllText(fileName, summary);
                }
                else if (unitUID.StartsWith("exercise"))
                {
                    System.IO.File.AppendAllText(fileName, exercise);
                }
                else if (unitUID.StartsWith("challenge"))
                {
                    System.IO.File.AppendAllText(fileName, challenge);
                }
                else if (unitUID.StartsWith("solution"))
                {
                    System.IO.File.AppendAllText(fileName, solution);
                }
                else
                {
                    System.IO.File.AppendAllText(fileName, content);
                }

                Console.WriteLine($"Created {fileName}");
            }
        }

        static void createKnowledgeCheck(string moduleUID, List<string> unitUIDs, string baseDirectory, int counter)
        {
            var content = System.IO.File.ReadAllText("c:\\scaffold\\templates\\unit-knowledge-check.txt");

            var myDate = DateTime.Now.ToString("MM/dd/yyyy");
            content = content.Replace("{{date}}", myDate);

            content = content.Replace("{{moduleUID}}", moduleUID);

            var numberModuleUID = $"{counter}-knowledge-check";

            var fileName = baseDirectory + "\\" + numberModuleUID + ".yml";
            System.IO.File.AppendAllText(fileName, content);

            Console.WriteLine($"Created {fileName}");
        }

    }
}
