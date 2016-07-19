using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coda.ViewModels
{
    public class InstructorViewModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public int ZipCode { get; set; }
        public byte[] Image { get; set; }
    }
}