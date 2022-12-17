using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Folder
    {
        public string Name { get; set; }
        public Folder ParentFolder { get; set; }
        public List<Folder> Folders { get; set; }
        public List<File> Files { get; set; }

        public Folder(string name, Folder parentFolder)
        {
            Name = name;
            Folders = new List<Folder>();
            Files = new List<File>();
            ParentFolder = parentFolder;
        }

        public int Size
        {
            get
            {
                return Files.Sum(file => file.Size) + Folders.Sum(folder => folder.Size);
            }
        }
    }
}
