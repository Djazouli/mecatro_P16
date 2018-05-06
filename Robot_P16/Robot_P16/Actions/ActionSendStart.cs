using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using Robot_P16.Map;
using Gadgeteer.Networking;
using Robot_P16.Robot;
using GT = Gadgeteer;
using System.IO.Ports;
using System.Threading;
namespace Robot_P16.Actions
{
    public class ActionSendStart : Action
    {
        public SerialPort m_port; 
        public int numPort = 11;//A changer quand on connaitra les branchements
        public byte[] buffer = new byte[100];

        public ActionSendStart() : base("Action de depart")
        {
            string COMPort = GT.Socket.GetSocket(numPort, true, null, null).SerialPortName;
            this.m_port = new SerialPort(COMPort, 9600, Parity.None, 8, StopBits.One);
        }

        public override void Execute()
        {
            String commande;
            Debug.Print("Ouverture du port");
            string COMPort = GT.Socket.GetSocket(numPort, true, null, null).SerialPortName;
            m_port.ReadTimeout = 500;
            m_port.WriteTimeout = 500;
            m_port.Open();
            if (m_port.IsOpen)
            {

                commande = "start\r\n";
                Debug.Print("Envoi du signal");
                buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                Debug.Print(commande);
                m_port.Write(buffer, 0, commande.Length);
                Thread.Sleep(200);
                m_port.Write(buffer, 0, commande.Length);
                Thread.Sleep(2000);
                m_port.Write(buffer, 0, commande.Length);
                
            }
            m_port.Close();
        }

        public override void Feedback(Action a)
        {
            throw new NotImplementedException();
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            throw new NotImplementedException();
        }
    }
}