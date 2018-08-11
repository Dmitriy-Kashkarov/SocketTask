using MongoDB.Driver;
using SocketTask.Interfaces;
using SocketTask.Models;

namespace SocketTask.DAL
{
    public class DatabaseContext : IDatabaseContext
    {
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public MongoClient Client { get; }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public IMongoDatabase Database { get; }

        /// <summary>
        /// Gets or sets the received datas.
        /// </summary>
        /// <value>
        /// The received datas.
        /// </value>
        public IMongoCollection<ReceivedData> ReceivedDatas { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseContext"/> class.
        /// </summary>
        /// <param name="configurationManager">The configuration manager.</param>
        public DatabaseContext(IConfigurationManager configurationManager)
        {
            Client = new MongoClient(configurationManager.ConnectionString);

            Database = Client.GetDatabase(configurationManager.DatabaseName);

            ReceivedDatas = Database.GetCollection<ReceivedData>("ReceivedDatas");
        }
    }
}