using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coda.Models;

namespace Coda.Controllers
{
    public class FileTableController : Controller
    {
        public ActionResult AddFile()
        {
            return View();
        }
        // GET: FileTable

        [HttpPost]
        public ActionResult AddFile(HttpPostedFileBase file)
        {
            if (file != null)
            {
                //get the bytes from the uploaded file
                byte[] data = GetBytesFromFile(file);

                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    db.FileTable.Add(new FileTable() {UploadedFile = data});
                    db.SaveChanges();
                }
            }

            return View();
        }

        //Method to convert file into byte array
        private byte[] GetBytesFromFile(HttpPostedFileBase file)
        {
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                return memoryStream.ToArray();
            }
        }
    }
}
