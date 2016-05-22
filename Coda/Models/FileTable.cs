using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coda.Models
{
    public class FileTable
    {
        public int Id { get; set; } 

        public byte[] UploadedFile { get; set; }
    }
}
