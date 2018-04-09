using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JsonRead.Json;
using JsonRead.Model;

namespace 条烟纠错系统
{
    public partial class SystemSettingsForm : Form
    {

        ConfigureMessageModel configure;

        public SystemSettingsForm()
        {
            InitializeComponent();
        }


        private void SystemSettings_Load(object sender, EventArgs e)
        {
            CheckJsonFile();
            ComboxAddItemsPorts();
            LoadMessage();
        }

        private void LoadMessage()
        {
            this.txtIPPort.Text = configure.TCPS.TCPCameria.Port.ToString();
            this.txtIP.Text = configure.TCPS.TCPCameria.IP.ToString();
            this.txtBuadRate.Text = configure.SerialPorts.PLCPort.BaudRate.ToString();
        }

        private void ComboxAddItemsPorts()
        {
            var Ports = SerialPort.GetPortNames();
            foreach (string port in Ports)
            {
                cbPort.Items.Add(port);
            }
            cbPort.Text = configure.SerialPorts.PLCPort.PortName;
        }


        private void CheckJsonFile()
        {
            CustomerJsonRead<ConfigureMessageModel> configureJson = new CustomerJsonRead<ConfigureMessageModel>();
            configure = configureJson.GetJsonClass();
            if(configure==null)
            {
                ConfigureMessageModel tempConfigure = new ConfigureMessageModel();
                tempConfigure.TCPS = new TCPModel
                {
                    TCPCameria = new TcpMessage
                    {
                        IP = "127.0.0.1" ,Port = 51236
                    }
                };
                tempConfigure.SerialPorts = new SeralPortModel
                {
                    PLCPort = new SerialPortMessage
                    {
                        PortName = "", BaudRate = 9600
                    }
                };


                
                configureJson.SetJsonClass(tempConfigure);
                configureJson.SaveJsonString();

            }
            configure = configureJson.GetJsonClass();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CustomerJsonRead<ConfigureMessageModel> configureJson = new CustomerJsonRead<ConfigureMessageModel>();

            CheckJsonFile();
            configure.SerialPorts.PLCPort.PortName = cbPort.Text;
            configure.SerialPorts.PLCPort.BaudRate = Convert.ToInt32(this.txtBuadRate.Text);
            configure.TCPS.TCPCameria.IP = this.txtIP.Text;
            configure.TCPS.TCPCameria.Port = Convert.ToInt32(this.txtIPPort.Text);
            configureJson.SetJsonClass(configure);
            configureJson.SaveJsonString();

            this.Close();

        }
    }
}
