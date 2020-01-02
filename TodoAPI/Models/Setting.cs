using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Setting
    {
        [Key]
        public long Setting_Id { get; set; }
        public long User_Id { get; set; }
        public string Filename { get; set; }
        public bool Exam { get; set; }
        public bool Spelling { get; set; }
        public bool Woord { get; set; }
        public bool Woordenboek { get; set; }
        public bool Vertalen { get; set; }
        public bool Drive { get; set; }
        public bool Online { get; set; }
    }
}