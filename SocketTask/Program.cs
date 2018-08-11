using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SocketTask.Interfaces;

namespace SocketTask
{
    public class Program
    {
        /// <summary>
        /// The locker
        /// </summary>
        private readonly object _locker = new object();

        /// <summary>
        /// The socket communication
        /// </summary>
        private readonly ISocketCommunication _socketCommunication;

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        /// <param name="socketCommunication">The socket communication.</param>
        public Program(ISocketCommunication socketCommunication)
        {
            _socketCommunication = socketCommunication;
            HandleConnection();
        }

        /// <summary>
        /// Handles the connection.
        /// </summary>
        private void HandleConnection()
        {
            var timer = new System.Timers.Timer(10000);

            timer.Elapsed += async (sender, eventArgs) => await HandleTimer();

            _socketCommunication.Connected += delegate
            {
                Task.Factory.StartNew(_socketCommunication.Receive);
                timer.Stop();
            };

            timer.Start();
        }

        /// <summary>
        /// Handles the timer.
        /// </summary>
        /// <returns></returns>
        private Task HandleTimer()
        {
            lock (_locker)
            {
                _socketCommunication.Connect();
            }
            return Task.FromResult(true);
        }

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Builds the web host.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
