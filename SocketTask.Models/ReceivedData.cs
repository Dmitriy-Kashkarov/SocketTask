using System;
using MongoDB.Bson.Serialization.Attributes;

namespace SocketTask.Models
{
    public class ReceivedData
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [BsonId]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the asset.
        /// </summary>
        /// <value>
        /// The name of the asset.
        /// </value>
        public string AssetName { get; set; }

        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the bid price.
        /// </summary>
        /// <value>
        /// The bid price.
        /// </value>
        public double BidPrice { get; set; }

        /// <summary>
        /// Gets or sets the ask price.
        /// </summary>
        /// <value>
        /// The ask price.
        /// </value>
        public double AskPrice { get; set; }
    }
}