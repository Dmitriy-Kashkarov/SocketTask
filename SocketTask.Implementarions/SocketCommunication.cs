using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SocketTask.Interfaces;
using SocketTask.Models;

namespace SocketTask.Implementarions
{
    /// <summary>
    /// Socket communication class
    /// </summary>
    /// <seealso cref="SocketTask.Interfaces.ISocketCommunication" />
    public class SocketCommunication : ISocketCommunication
    {
        /// <summary>
        /// The configuration manager
        /// </summary>
        private readonly IConfigurationManager _configurationManager;

        /// <summary>
        /// The receive data repository
        /// </summary>
        private readonly IReceiveDataRepository _receiveDataRepository;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The socket
        /// </summary>
        private Socket _socket;

        /// <summary>
        /// The buffer
        /// </summary>
        private readonly byte[] _buffer;

        /// <summary>
        /// The server message
        /// </summary>
        private readonly StringBuilder _serverMessage;

        /// <summary>
        /// Gets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Occurs when [connected].
        /// </summary>
        public event EventHandler<EventArgs> Connected;

        /// <summary>
        /// Occurs when [disconnected].
        /// </summary>
        public event EventHandler<EventArgs> Disconnected;

        /// <summary>
        /// Occurs when [data received].
        /// </summary>
        public event EventHandler<EventArgs> DataReceived;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketCommunication" /> class.
        /// </summary>
        /// <param name="configurationManager">The configuration manager.</param>
        /// <param name="receiveDataRepository">The receive data repository.</param>
        public SocketCommunication(IConfigurationManager configurationManager, IReceiveDataRepository receiveDataRepository, ILogger logger)
        {
            _logger = logger;
            _configurationManager = configurationManager;
            _receiveDataRepository = receiveDataRepository;
            _buffer = new byte[_configurationManager.BufferSize];
            _serverMessage = new StringBuilder();
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        public void Connect()
        {
            if (IsConnected)
                return;

            SafeExecution(() =>
            {
                Disconnect();

                var endPoint = new IPEndPoint(_configurationManager.IpAddress, _configurationManager.Port);

                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                _socket.Connect(endPoint);

                if (_socket.Connected)
                {
                    IsConnected = true;

                    _logger.Log($"Socket connected to {_socket.RemoteEndPoint}");

                    Connected?.Invoke(this, new EventArgs());
                }
            }, "Connection error");
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public void Disconnect()
        {
            if (_socket != null)
            {
                if (_socket.Connected)
                {
                    _socket.Shutdown(SocketShutdown.Both);
                    _socket.Disconnect(false);
                }

                _socket.Close();

                IsConnected = false;

                _logger.Log("Socket was closed.");

                Disconnected?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Receives this instance.
        /// </summary>
        public void Receive()
        {
            SafeExecution(() =>
            {
                do
                {
                    var bytesRead = _socket.Receive(_buffer, SocketFlags.None);

                    _logger.Log($"Received from server: {bytesRead} bytes");

                    _serverMessage.Clear();

                    if (bytesRead > 0)
                    {
                        _serverMessage.Append(Encoding.ASCII.GetString(_buffer, 0, bytesRead));

                        ParseReceivedResult(_serverMessage.ToString());

                        DataReceived?.Invoke(this, new EventArgs());

                        //_socket.Receive(_buffer, SocketFlags.None);
                    }
                    else
                    {
                        _logger.Error($"Incorrect receive data");
                    }
                } while (IsConnected);

            }, "Receive error");
        }


        /// <summary>
        /// Parses the received result.
        /// </summary>
        /// <param name="received">The received.</param>
        private void ParseReceivedResult(string received)
        {
            const char separator = ' ';

            if (string.IsNullOrEmpty(received))
            {
                _logger.Error("Incorrect data for parsing");
                return;
            }

            SafeExecution(() =>
            {
                var parsedResult = received.Split(separator);

                var receivedData = new ReceivedData
                {
                    Id = Guid.NewGuid(),
                    AssetName = parsedResult[0],
                    DateTime = DateTime.Parse(parsedResult[1]),
                    BidPrice = double.Parse(parsedResult[2]),
                    AskPrice = double.Parse(parsedResult[3].TrimEnd('\r', '\n'))
                };

                if (_receiveDataRepository.GetByAssertName(receivedData.AssetName) != null)
                    _receiveDataRepository.UpdateData(receivedData);
                else
                    _receiveDataRepository.AddData(receivedData);

            }, "Parsing srting error");
        }

        /// <summary>
        /// Safes the execution.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="errorMessage">The error message.</param>
        private void SafeExecution(Action action, string errorMessage)
        {
            try { action(); }
            catch (Exception ex)
            {
                _logger.Error(errorMessage, ex);
            }
        }
    }
}