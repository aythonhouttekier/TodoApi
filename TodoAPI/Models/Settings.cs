using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Settings
    {
        public long id;
        public bool Exam;
        public bool Spelling;
        public bool Woord;
        public bool Woordenboek;
        public bool Vertalen;
        public bool Drive;
        public bool Online;
    }
}
