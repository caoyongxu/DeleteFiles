using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Reflection;

namespace DeleteFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            var exeFile = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", ""); ;
            var currntfolder = Path.GetDirectoryName(exeFile);

            List<DirectoryInfo> Folders = new List<DirectoryInfo>();
            foreach (var folder in Directory.EnumerateDirectories(currntfolder))
            {
                foreach (var f in Directory.EnumerateDirectories(folder))
                {
                    Folders.Add(new DirectoryInfo(f));
                }
            }

            Folders.OrderBy(o => o.CreationTime);

            foreach (var f in Folders)
            {
                System.Console.WriteLine($"{f.Name}  :  {f.CreationTime} ");
            }

            List<DirectoryInfo> Folders2Delete = Folders.Where(o => o.CreationTime < DateTime.Now.AddMonths(-4)).ToList();

            foreach (var fd in Folders2Delete)
            {
                try
                {
                    fd.Delete(true);
                    System.Console.WriteLine($"Deleted the folder: {fd}");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                    //throw;
                }               

            }

        }
    }
}
