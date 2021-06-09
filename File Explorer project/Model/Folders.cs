using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Explorer_project.Model
{
    public class Folders
    {
        public string Foldername { get; set; }
        public string Folderpath { get; set; }
        public DateTimeOffset FolderCreatedDate { get; set; }
        public DateTimeOffset FolderModifiedDate { get; set; }
        public string Foldersize { get; set; }
    }
}
