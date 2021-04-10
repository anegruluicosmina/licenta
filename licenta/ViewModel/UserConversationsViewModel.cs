using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class UserConversationsViewModel
    {
        public string Name { get; set; }
        public string Sender { get; set; }
        public string Text { get; set; }
        public DateTime Time{ get; set; }
        public bool IsSeen { get; set; }
    }
}
