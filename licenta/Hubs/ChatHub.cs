using licenta.Data;
using licenta.Models;
using licenta.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Hubs
{
    public class ChatHub: Hub
    {
        /*List<UserConnection> uList = new List<UserConnection>();*/
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatHub(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        //send message
        public async Task SendMessage(string who ,string message)
        {
            //take current user from context
            var senderName = Context.User.Identity.Name;

            if(senderName == null)
            {
                //the user does not exist
                /* Clients.Caller.showErrorMessage("Could not find sender.");*/
            }
            else
            {
                //send message to the user who send the message
                var sender = _context.Users.Where(u => u.UserName == senderName).Include(u => u.Connections).FirstOrDefault();
                if (sender == null)
                {
                    //user does not found in db
                }
                else
                {
                    foreach (var connection in sender.Connections)
                    {
                        await Clients.Caller.SendAsync("receiveMessage", message, senderName);
                    }
                }


                //take receiver from db
                var receiver = _context.Users.Where(u => u.UserName == who).Include(u => u.Connections).FirstOrDefault();

                if(receiver == null)
                {
                    //receiver does not exists in db
                    /* Clients.Caller.showErrorMessage("Could not find receiver");*/
                }
                else
                {
                    //take all the connections of the reciver to send the message to all of them
                    if(receiver.Connections != null)
                    {
                        foreach(var conection in receiver.Connections)
                        {
                            //call the receiverMessage from js with the message and sender param
                            await Clients.Client(conection.ConnectionID).SendAsync("receiveMessage", message, senderName);

                        }
                    }
                }
            }
        }

        public override Task OnConnectedAsync()
        {
            var httpCtx = Context.GetHttpContext();

            var user = Context.User.Identity.Name;

            var userInDb = _context.Users.Include(u => u.Connections).SingleOrDefault(u => u.UserName == user);

            if(userInDb == null)
            {
                //the user does not exist
            }
            else
            {
                //add new connection of user in db
                userInDb.Connections.Add(new Connection
                {
                    ConnectionID = Context.ConnectionId,
                    UserAgent = httpCtx.Request.Headers["User-Agent"],
                    IsConnected = true
                });
                _context.SaveChanges();
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {            
            var conection = _context.Connections.Find(Context.ConnectionId);
            _context.Remove(conection);
            _context.SaveChanges();

            return base.OnDisconnectedAsync(exception);
        }
    }
}
