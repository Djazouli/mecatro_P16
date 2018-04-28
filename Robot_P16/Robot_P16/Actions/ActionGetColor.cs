using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using Gadgeteer.Networking;
using GT = Gadgeteer;
using Robot_P16.Robot;
using System.IO.Ports;
namespace Robot_P16.Actions
{
    public class ActionGetColor : Action
    {
        public readonly SerialPort m_port;
        public readonly int numPort = 4;
        public byte[] buffer = new byte[100];

        public ActionGetColor()
            : base("Attente de la couleur")
        {
            string COMPort = GT.Socket.GetSocket(numPort, true, null, null).SerialPortName;
            this.m_port = new SerialPort(COMPort, 9600, Parity.None, 8, StopBits.One);
        }

        public override void execute()
        {
            Debug.Print("Ouverture du port");
            m_port.ReadTimeout = 10000;
            m_port.WriteTimeout = 500;
            m_port.Open();
            if (m_port.IsOpen)
            {
                Debug.Print("Attente du code couleur");
                {
                    m_port.Read(buffer, 0, 100);
                }

            }
            m_port.Close();
        }
    }
}