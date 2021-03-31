using licenta.Data;
using licenta.Models;
using licenta.ViewModel;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Hubs
{
    public class ChatHub: Hub
    {
        List<UserConnection> uList = new List<UserConnection>();
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }
/*        public override async Task OnConnectedAsync()
        {
            var currentUser = Context.User.Identity.Name;

            var transmitter =  _context.Users.Where(u => u.Email == currentUser).FirstOrDefault();

            var httpcontext = Context.GetHttpContext();
            var receiverUserEmail = httpcontext.Request.Query["username"].ToString();

            if(receiverUserEmail == "undefined")
            {

            }
        }*/

        public async Task sendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            /*Clients.Client(user.First()).sendMessage(message);*/
        }
    }
}
