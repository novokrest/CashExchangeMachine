using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
