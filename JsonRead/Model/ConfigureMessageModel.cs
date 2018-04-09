using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonRead.Model
{
    public class ConfigureMessageModel
    {
        public SeralPortModel SerialPorts { set; get; }
        public TCPModel TCPS { set; get; }
    }
}
