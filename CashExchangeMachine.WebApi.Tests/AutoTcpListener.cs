using System;
using System.Net;
using System.Net.Sockets;

namespace CashExchangeMachine.WebApi.Tests
{
    internal sealed class AutoTcpListener : IDisposable
    {
        private readonly TcpListener _listener;
        private readonly bool _started;

        public AutoTcpListener(IPAddress localaddr, int port)
        {
            _listener = new TcpListener(localaddr, port);
            _listener.Start();
            _started = true;
        }

        public int Port => ((IPEndPoint)_listener.LocalEndpoint).Port;

        public void Dispose()
        {
            if (_started)
            {
                _listener.Stop();
            }
        }
    }
}
