using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonRead.Model
{
    public class TCPModel
    {
        public TcpMessage TCPCameria { set; get; }
    }

    public class TcpMessage
    {
        public string IP { set; get; }
        public int Port { set; get; }
    }
}
