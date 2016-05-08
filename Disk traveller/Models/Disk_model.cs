using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Disk_traveller.Models
{

 

    public class Folderer
    {

        public Folderer()
        { 
        }


        public List<string> drives;
        public Dictionary<string, Dictionary<string,string>> folders;

        public void  GetDrives()
        {
            drives = new List<string>();

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo di in allDrives)
            {
                drives.Add(di.Name);
            }

        }

        public void GetFolders( string path)
        {

            folders = new Dictionary<string, Dictionary<string, string>>();
            folders.Add("Folders", new Dictionary<string, string>());
            folders.Add("Error", new Dictionary<string, string>());
            folders.Add("Up", new Dictionary<string, string>());
            folders.Add("Current", new Dictionary<string, string>());
            folders.Add("Files", new Dictionary<string, string>());
            folders.Add("Count", new Dictionary<string, string>());

            string upone = Path.GetFullPath(Path.Combine(path, @"..\"));

            folders["Up"].Add("Path", upone);
            folders["Current"].Add("Path", path);
            folders["Count"].Add("f010", "0");
            folders["Count"].Add("f1050", "0");
            folders["Count"].Add("f100", "0");



            DirectoryInfo di=null;


            try
            {
                di = new DirectoryInfo(path);
                DirectoryInfo[] diAll = di.GetDirectories();

                foreach (DirectoryInfo diOne in diAll)
                {
                    folders["Folders"].Add(diOne.Name, diOne.FullName);
                }
            
            }
            catch (Exception e)
            {
                folders["Error"].Add("Text", e.Message);

            }


            if (di != null)
            {
                try
                {
                    FileInfo[] fiAll = di.GetFiles();

                    foreach (FileInfo fiOne in fiAll)
                    {
                        folders["Files"].Add(fiOne.Name, fiOne.FullName);
                        long size = fiOne.Length / 1048576;

                        if (size <= 10)
                        {
                            int total;
                            bool parse = Int32.TryParse(folders["Count"]["f010"], out total);
                            if (!parse)
                            {
                                total = 0;
                            }
                            total++;
                            folders["Count"]["f010"] = total.ToString();
                        
                        }
                        else if ((size > 10) && size <= 50)
                        {
                            int total;
                            bool parse = Int32.TryParse(folders["Count"]["f1050"], out total);
                            if (!parse)
                            {
                                total = 0;
                            }
                            total++;
                            folders["Count"]["f1050"]=total.ToString(); 
                        }
                        else if (size>=100)
                        {
                            int total;
                            bool parse = Int32.TryParse(folders["Count"]["f100"], out total);
                            if (!parse)
                            {
                                total = 0;
                            }
                            total++;
                            folders["Count"]["f100"] = total.ToString(); 

                        }

                    }

                }
                catch (Exception e)
                {
                    folders["Error"].Add("Text2", e.Message);
                }
            }
            




        }


    }
}