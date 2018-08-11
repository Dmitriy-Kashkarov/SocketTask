using System;
using SocketTask.Interfaces;

namespace SocketTask.Implementarions
{
    /// <summary>
    /// NLog logger implementation
    /// </summary>
    /// <seealso cref="SocketTask.Interfaces.ILogger" />
    public class Logger : ILogger
    {
        /// <summary>
        /// The NLog logger
        /// </summary>
        private static readonly NLog.Logger NLogLogger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            NLogLogger.Error(message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void Error(string message, Exception ex)
        {
            NLogLogger.Error(ex, message);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            NLogLogger.Info(message);
        }
    }
}