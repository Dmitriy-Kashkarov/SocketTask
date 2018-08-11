using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SocketTask.Interfaces;

namespace SocketTask.Implementarions
{
    public class ConfigurationManager : IConfigurationManager
    {
        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port => 4092;

        /// <summary>
        /// Gets the ip address.
        /// </summary>
        /// <value>
        /// The ip adress.
        /// </value>
        public IPAddress IpAddress => IPAddress.Parse("79.125.80.209");

        /// <summary>
        /// Gets the size of the buffer.
        /// </summary>
        /// <value>
        /// The size of the buffer.
        /// </value>
        public int BufferSize => 131072;

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString => "mongodb://192.168.99.100:27017";

        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName => "SomeDatabase";
    }
}