using System;
using Microsoft.SPOT;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using System.Threading;


namespace Robot_P16.Robot.composants
{
    public class RecepteurCodeCouleur : Composant
    {
        private GTM.GHIElectronics.XBeeAdapter adapteur;
        private Gadgeteer.SocketInterfaces.Serial m_port;

        public RecepteurCodeCouleur(int socket)
            : base(socket)
        {
            this.adapteur = new GTM.GHIElectronics.XBeeAdapter(socket);
            adapteur.Configure(9600, GT.SocketInterfaces.SerialParity.None, GT.SocketInterfaces.SerialStopBits.One, 8, GT.SocketInterfaces.HardwareFlowControl.NotRequired);

            new Thread(() =>
            {
                string code = null;
                while (code== null)
                {
                    Thread.Sleep(1000);
                    code = reception();
                }
                Robot.robot.codeCouleur = code;
            }).Start();
        }

        public string reception()
        {
            if (m_port.BytesToRead <= 0)
                return null;

            byte[] buffer = new byte[m_port.BytesToRead];
            int test = m_port.Read(buffer, 0, m_port.BytesToRead);
            Debug.Print(test.ToString());
            char[] chars = System.Text.Encoding.UTF8.GetChars(buffer);
            for (int i = 0; i < chars.Length; i++)
            {
                Debug.Print(chars[i].ToString());
            }
            return new string(chars);
        }
    }
}

