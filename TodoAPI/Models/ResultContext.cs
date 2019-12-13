using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class ResultContext : DbContext
    {
        public ResultContext(DbContextOptions<ResultContext> options)
            : base(options)
        {
        }

        public DbSet<ResultItem> ResultItems { get; set; }
    }
}
