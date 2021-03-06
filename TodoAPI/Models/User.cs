﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string School { get; set; }
        public string Opleiding { get; set; }
        public List<Vak> Vak { get; set; }
        public bool Access { get; set; }
        public bool Status { get; set; }
        public List<Setting> Examsetting { get; set; }
    }
}
