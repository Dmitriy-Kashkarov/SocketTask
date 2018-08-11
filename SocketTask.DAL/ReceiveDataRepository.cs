using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using SocketTask.Interfaces;
using SocketTask.Models;

namespace SocketTask.DAL
{
    public class ReceiveDataRepository : IReceiveDataRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IDatabaseContext _context;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiveDataRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public ReceiveDataRepository(IDatabaseContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReceivedData>> GetAll()
        {
            try
            {
                var filter = Builders<ReceivedData>.Filter.Empty;
                var result = await _context.ReceivedDatas.Find(filter).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error("Not data in db", ex);
            }
            return new List<ReceivedData>();
        }

        /// <summary>
        /// Adds the data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public async Task AddData(ReceivedData data)
        {
            try
            {
                await _context.ReceivedDatas.InsertOneAsync(data);
            }
            catch (Exception ex)
            {
                _logger.Error("Adding data error", ex);
            }
        }

        /// <summary>
        /// Gets the name of the by assert.
        /// </summary>
        /// <param name="assertName">Name of the assert.</param>
        /// <returns></returns>
        public async Task<ReceivedData> GetByAssertName(string assertName)
        {
            try
            {
                var result = await _context.ReceivedDatas.Find(rd => rd.AssetName == assertName).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting assert by name", ex);
            }

            //TODO: Custom exceptions
            return null;
        }

        /// <summary>
        /// Updates the data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public async Task UpdateData(ReceivedData data)
        {
            try
            {
                var filter = Builders<ReceivedData>.Filter.Eq(x => x.AssetName, data.AssetName);
                await _context.ReceivedDatas.FindOneAndReplaceAsync(filter, data);
            }
            catch (Exception ex)
            {
                _logger.Error("Update data error", ex);
            }

        }
    }
}