using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonRead.Model
{
    public class SeralPortModel
    {
        public SerialPortMessage PLCPort { set; get; }
    }

    public class SerialPortMessage
    {
        public string PortName { set; get; }
        public int BaudRate { set; get; }
    }
}
