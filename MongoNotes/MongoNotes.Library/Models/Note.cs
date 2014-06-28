using System;

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MongoNotes.Library.Models
{
    public class Note
    {
        private DateTime _date;

        public Note()
        {
            Date = DateTime.UtcNow;
        }

        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        [BsonElement("Note")]
        public string Text { get; set; }

        [BsonElement("Date")]
        public DateTime Date
        {
            get
            {
                return _date.ToLocalTime();
            }
            set
            {
                _date = value;
            }
        }
    }
}