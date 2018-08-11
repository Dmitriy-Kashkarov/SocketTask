using System;
using System.Collections.Generic;
using SocketTask.Models;

namespace SocketTask.Interfaces
{
    public interface ISocketCommunication
    {
        /// <summary>
        /// Connects this instance.
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Receives this instance.
        /// </summary>
        void Receive();

        /// <summary>
        /// Gets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        bool IsConnected { get; }

        /// <summary>
        /// Occurs when [connected].
        /// </summary>
        event EventHandler<EventArgs> Connected;

        /// <summary>
        /// Occurs when [disconnected].
        /// </summary>
        event EventHandler<EventArgs> Disconnected;

        /// <summary>
        /// Occurs when [data received].
        /// </summary>
        event EventHandler<EventArgs> DataReceived;
    }
}