using MongoDB.Driver;
using SocketTask.Models;

namespace SocketTask.Interfaces
{
    public interface IDatabaseContext
    {
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        MongoClient Client { get; }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        IMongoDatabase Database { get; }

        /// <summary>
        /// Gets or sets the received datas.
        /// </summary>
        /// <value>
        /// The received datas.
        /// </value>
        IMongoCollection<ReceivedData> ReceivedDatas { get; set; }
    }
}