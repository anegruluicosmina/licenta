using licenta.Data;
using licenta.Models;
using licenta.ViewModel;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatHub(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public async Task SendMessage(string who ,string message)
        {
            var user = Context.User.Identity.Name;
            if(user == null)
            {
               /* Clients.Caller.showErrorMessage("Could not find that user.");*/
            }
            await Clients.All.SendAsync("ReceiveMessage", message, who);
            /*Clients.Client(user.First()).sendMessage(message);*/
        }
    }
}
