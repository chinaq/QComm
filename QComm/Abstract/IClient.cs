using System;

namespace QComm
{
    public interface IClient
    {
        int ReadTimeOut { get; set; }
        int RevDuration { get; set; }
        string Status { get; }

        byte[] Rev();
        void Send(byte[] data);
        void Stop();
        void Open();
        void Close();
    }


    public class CommStatus
    {
        public static readonly string Writing = "Writing";
        public static readonly string Reading = "Reading";
        public static readonly string Stopping = "Stopping";
        public static readonly string Stopped = "Stopped";
        public static readonly string Closed = "Closed";

        //Writing,
        //Reading,
        //Stopping,
        //Stopped,
        //Closed
    }
}