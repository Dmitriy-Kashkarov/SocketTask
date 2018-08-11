using System.Net;

namespace SocketTask.Interfaces
{
    public interface IConfigurationManager
    {
        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        int Port { get; }

        /// <summary>
        /// Gets the ip address.
        /// </summary>
        /// <value>
        /// The ip adress.
        /// </value>
        IPAddress IpAddress { get; }

        /// <summary>
        /// Gets the size of the buffer.
        /// </summary>
        /// <value>
        /// The size of the buffer.
        /// </value>
        int BufferSize { get; }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        string ConnectionString { get; }

        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        string DatabaseName { get; }
    }
}