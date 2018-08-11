using System.Collections.Generic;
using System.Threading.Tasks;
using SocketTask.Models;

namespace SocketTask.Interfaces
{
    public interface IReceiveDataRepository
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        Task<List<ReceivedData>> GetAll();

        /// <summary>
        /// Adds the data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        Task AddData(ReceivedData data);

        /// <summary>
        /// Gets the name of the by assert.
        /// </summary>
        /// <param name="assertName">Name of the assert.</param>
        /// <returns></returns>
        Task<ReceivedData> GetByAssertName(string assertName);

        /// <summary>
        /// Updates the data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        Task UpdateData(ReceivedData data);
    }
}