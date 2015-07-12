using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingalRGame
{
    public class LineMessage
    {
        public string User { get; private set; }
        public string Message { get; private set; }

        public LineMessage(string user, string message)
        {
            User = user;
            Message = message;
        }
    }
}