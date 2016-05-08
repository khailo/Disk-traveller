using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Disk_traveller.Models;


namespace Disk_traveller.Controllers
{
    public class DiskController : ApiController
    {

        private Folderer folderer;

        public DiskController()
        {
            folderer = new Folderer();
        }

        public IEnumerable<string> Get()
        {
            
            folderer.GetDrives();
            return folderer.drives; 
        }

        public Dictionary<string, Dictionary<string, string>> Get(string id)
        {
            byte[] data = Convert.FromBase64String(id);
            string decodedString = System.Text.Encoding.UTF8.GetString(data);

            id = System.Web.HttpUtility.UrlDecode(decodedString);
            folderer.GetFolders(id);
            return folderer.folders;
        }  

    }
}
