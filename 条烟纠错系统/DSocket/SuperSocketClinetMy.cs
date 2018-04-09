using System;
//using MiscUtil.Conversion;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.ClientEngine;
using SuperSocket.ProtoBase;
using System.Net;
using System.Threading.Tasks;
using 条烟纠错系统.DSocket;
using 条烟纠错系统.DSocket.DataFilter;

namespace 条烟纠错系统.DSocket
{
    class TCPCommunication 
    {

        public event EventHandler Connected;

        private readonly EasyClient _tcpClient = new EasyClient();
        //private readonly EndianBitConverter _endianBitConverter;

        
        public string EndPoint { get; set; }
        public int Port { get; set; } = 51236;
        public bool IsConnected { get { return _tcpClient.IsConnected; } }

        public TCPCommunication(string ip,Action<DataPackageInfo> handler=null)
        {
            EndPoint = ip;
            
            //_endianBitConverter = new BigEndianBitConverter();

            _tcpClient.Connected += _tcpClient_Connected;

            _tcpClient.Initialize(new DataReceiveFilter(), response =>
            {
                Console.WriteLine(response.CodeData);
                if (response != null )
                {
                    handler(response);
                }
            });
        }

        private void _tcpClient_Connected(object sender, EventArgs e)
        {
            Connected?.Invoke(this, e);
        }

        public async Task<bool> ConnectAsync()
        {
              //_tcpClient.ConnectAsync(new IPEndPoint(IPAddress.Parse(EndPoint), Port));
            return await _tcpClient.ConnectAsync(new IPEndPoint(IPAddress.Parse(EndPoint), Port));
        }

        public void Close()
        {
            if (_tcpClient.IsConnected) _tcpClient.Close();
        }
    }
}
