using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Vak
    {
        [Key]
        public long Vak_Id { get; set; }
        public long User_Id { get; set; }
        public string Vakname { get; set; }
        public string Opleidingname { get; set; }
    }
}
