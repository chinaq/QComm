using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace QComm
{


    public class SerialClient : IClient
    {
        private SerialPort port;

        public int ReadTimeOut { get => port.ReadTimeout; set => port.ReadTimeout = value; }
        public string Status { get; private set; }
        public int RevDuration { get; set; }


        private SerialClient() {
            Status = CommStatus.Closed;
            RevDuration = 100;
        }

        public SerialClient(string portName):this()
        {
            port = new SerialPort(portName);
        }

        public SerialClient(string portName, int baudRate):this() {
            port = new SerialPort(portName, baudRate);
        }

        public SerialClient(string portName, int baudRate, Parity parity):this() {
            port = new SerialPort(portName, baudRate, parity);
        }

        public SerialClient(string portName, int baudRate, Parity parity, int dataBits):this() {
            port = new SerialPort(portName, baudRate, parity, dataBits);
        }

        public SerialClient(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits):this() {
            port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        }



        public void Close()
        {
            port.Close();
            Status = CommStatus.Closed;
        }

        public void Open()
        {
            port.Open();
            Status = CommStatus.Stopped;
        }

        public byte[] Rev()
        {
            Status = CommStatus.Reading;

            int revLen = 0;
            Byte[] rev = new Byte[4096];
            DateTime start = DateTime.Now;

            //int round = ReadTimeOut < 0 ? int.MaxValue : ReadTimeOut / 1;
            //for (int i = 0; i < round; i++)
            while (!IsTimeOut(start, ReadTimeOut))
            {
                if (port.BytesToRead > 0)
                {
                    Thread.Sleep(RevDuration);
                    revLen = port.Read(rev, 0, rev.Length);
                    break;
                }
                else
                {
                    if (Status == CommStatus.Stopping)
                        //|| IsTimeOut(start, ReadTimeOut))
                        break;
                    //Thread.Sleep(ReactionDuration);
                    Thread.Sleep(1);
                }
            }
            Array.Resize(ref rev, revLen);

            Status = CommStatus.Stopped;
            return rev;
        }

        private bool IsTimeOut (DateTime start, int readTimeOut)
        {
            if (readTimeOut < 0)
                return false;

            DateTime now = DateTime.Now;
            TimeSpan span = now - start;
            long passed = span.Ticks / TimeSpan.TicksPerMillisecond;
            return passed > readTimeOut;
        }

        public void Send(byte[] data)
        {
            Status = CommStatus.Writing;

            port.DiscardInBuffer();
            port.DiscardOutBuffer();
            port.Write(data, 0, data.Length);

            Status = CommStatus.Stopped;
        }

        public void Stop()
        {
            lock (Status)
            {
                if (Status == CommStatus.Reading || Status == CommStatus.Writing)
                    Status = CommStatus.Stopping;
            }

            while (Status != CommStatus.Stopped && Status != CommStatus.Closed)
            {
                //Thread.Sleep(ReactionDuration);
                Thread.Sleep(1);
            }
        }
    }
}
