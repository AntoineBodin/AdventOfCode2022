using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day7
    {
        public static int FirstStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day7\\input.txt";

            using StreamReader reader = new StreamReader(inputFilePath);
            string line = reader.ReadLine();

            Folder root = new Folder("/", null);
            Folder activeFolder = root;
            Folder lastActiveFolder = null;
            int total = 0;

            while ((line = reader.ReadLine()) != null)
            {
                string[] tokens = line.Split(" ");
                if (tokens[0] == "$")
                {
                    if (tokens[1] == "cd")
                    {
                        lastActiveFolder = activeFolder;
                        if (tokens[2] == "/")
                        {
                            activeFolder = root;
                        }
                        else if (tokens[2] == "..")
                        {
                            Folder intermediateFolder = activeFolder;
                            activeFolder = activeFolder.ParentFolder;
                            lastActiveFolder = intermediateFolder;
                        }
                        else
                        {
                            activeFolder = activeFolder.Folders.First(af => af.Name == tokens[2]);
                        }
                    }
                }
                else if (tokens[0] == "dir")
                {
                    activeFolder.Folders.Add(new Folder(tokens[1], activeFolder));
                }
                else
                {
                    activeFolder.Files.Add(new File(tokens[1], int.Parse(tokens[0])));
                }
            }

            List<KeyValuePair<string, int>> folderSizes = new List<KeyValuePair<string, int>>();

            ComputeDirectoriesSize(root, folderSizes);

            return folderSizes.Where(f => f.Value <= 100000).Sum(f => f.Value);
        }

        public static int SecondStep()
        {
            string inputFilePath = "C:\\Users\\abodin-ext\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Day7\\input.txt";

            using StreamReader reader = new StreamReader(inputFilePath);
            string line = reader.ReadLine();

            Folder root = new Folder("/", null);
            Folder activeFolder = root;
            Folder lastActiveFolder = null;
            int total = 0;

            while ((line = reader.ReadLine()) != null)
            {
                string[] tokens = line.Split(" ");
                if (tokens[0] == "$")
                {
                    if (tokens[1] == "cd")
                    {
                        lastActiveFolder = activeFolder;
                        if (tokens[2] == "/")
                        {
                            activeFolder = root;
                        }
                        else if (tokens[2] == "..")
                        {
                            Folder intermediateFolder = activeFolder;
                            activeFolder = activeFolder.ParentFolder;
                            lastActiveFolder = intermediateFolder;
                        }
                        else
                        {
                            activeFolder = activeFolder.Folders.First(af => af.Name == tokens[2]);
                        }
                    }
                }
                else if (tokens[0] == "dir")
                {
                    activeFolder.Folders.Add(new Folder(tokens[1], activeFolder));
                }
                else
                {
                    activeFolder.Files.Add(new File(tokens[1], int.Parse(tokens[0])));
                }
            }

            List<KeyValuePair<string, int>> folderSizes = new List<KeyValuePair<string, int>>();

            ComputeDirectoriesSize(root, folderSizes);

            int totalSpace = 70_000_000;
            int osNeededSpace = 30_000_000;
            int rootSpace = root.Size;
            int unusedSpace = totalSpace - rootSpace;
            int neededpace = osNeededSpace - unusedSpace;

            return folderSizes.Where(f => f.Value >= neededpace).Min(f => f.Value);
        }

        private static void ComputeDirectoriesSize(Folder folder, List<KeyValuePair<string, int>> folderSizes)
        {
            folderSizes.Add(new KeyValuePair<string, int>(folder.Name, folder.Size));
            folder.Folders.ForEach(f => ComputeDirectoriesSize(f, folderSizes));
        }
    }
}
