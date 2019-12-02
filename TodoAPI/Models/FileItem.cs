using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class FileItem
    {
        public long Id { get; set; }
        public string School { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public byte[] FileBytes { get; set; }
        public bool Exam { get; set; }
        public bool Spelling { get; set; }
        public bool Woord { get; set; }
        public bool Woordenboek { get; set; }
        public bool Vertalen { get; set; }
        public bool Drive { get; set; }
        public bool Online { get; set; }
        public bool Visibility { get; set; }
    }
}
