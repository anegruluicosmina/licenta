using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Models
{
    public class MessageCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Message> Messages { get; set; }
    }
}
