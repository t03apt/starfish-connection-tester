using Serilog;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace StarfishConnectionTester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                var socket = new Socket(SocketType.Stream, ProtocolType.Tcp)
                {
                    ReceiveBufferSize = 65536,
                    SendBufferSize = 65536,
                    NoDelay = true
                };

                Log.Information("Connecting...");
                await socket.ConnectAsync("api.mqtt-staging.developer.ssni.com", 8883);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close(10);
                Log.Information("Connection succeed.");
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Connection failed. Exception:{Exception}", ex);
            }

            Log.Logger.Information("Exit in 5 minutes...");
            await Task.Delay(TimeSpan.FromMinutes(5));
        }
    }
}
