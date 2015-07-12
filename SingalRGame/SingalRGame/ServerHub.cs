using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingalRGame
{
    public class ServerHub : Hub
    {
        private static List<LineMessage> _messages = new List<LineMessage>();
        private static Dictionary<string, string> _users = new Dictionary<string, string>();
        private static int _generatedNumber;

        static ServerHub()
        {
            GenerateNumber();
        }

        private static void GenerateNumber()
        {
            _generatedNumber = new Random().Next(1, 101);
        }

        public void SendMessage(string message)
        {
            string nickname = _users[Context.ConnectionId];

            _messages.Add(new LineMessage(nickname, message));
            Clients.All.broadcastMessage(nickname, message);
            
            short clientNumber = 0;
            if (Int16.TryParse(message, out clientNumber))
            {
                if (clientNumber == _generatedNumber)
                {
                    Clients.All.broadcastServerInformation("Winner is: " + nickname + " Winner number: "+ message);
                    GenerateNumber();
                }
                else
                {
                    string msgFromServer = (clientNumber > _generatedNumber) ? " is too large" : " is too small";
                    Clients.Caller.broadcastServerInformation(message + msgFromServer);
                }
            }
        }

        public void SetUserName(string name)
        {
            _users.Remove(Context.ConnectionId);
            _users.Add(Context.ConnectionId, name);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            _users.Add(Context.ConnectionId, "Unknown");
            foreach (LineMessage msg in _messages)
                Clients.Caller.broadcastMessage(msg.User, msg.Message);
            return base.OnConnected();
        }
    }
}