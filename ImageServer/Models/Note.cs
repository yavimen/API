using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageServer.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
        public string Owner { get; set; }
    }
}
