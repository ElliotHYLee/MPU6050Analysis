/*============================================================
// Made by: Elliot Hongyun Lee
// Undergrad Research Project
============================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    static class AbsAddress
    {
        public static string getFolderAddr(string folderName)
        {
            // get VocabList Folder's address
            string dir = Environment.CurrentDirectory;
            // dir end with "Release or Debug" as long as development going on
            string check = dir.Substring(dir.Length - 7, 7);
            if (check.Equals("Release"))
            {
                // length(bin\Release) = 11
                dir = dir.Substring(0, dir.Length - 11) + folderName + "\\";
            }
            else
            {
                // length(bin\Debug) = 9
                dir = dir.Substring(0, dir.Length - 9) + folderName + "\\";
            }
            return dir;
        }
    }
}
