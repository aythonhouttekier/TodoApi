using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class ResultItem
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string School { get; set; }
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
    }
}
