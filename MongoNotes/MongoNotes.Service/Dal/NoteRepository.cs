using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

using MongoNotes.Library.Models;

namespace MongoNotes.Service.Dal
{
    public class NoteRepository
    {
        private const string ConnectionString = "YOUR-MONGODB-CONNECTION-STRING";

        private const string DbName = "mongonotes";

        private const string CollectionName = "Notes";

        public List<Note> GetAllNotes()
        {
            try
            {
                MongoCollection<Note> collection = GetNotesCollection();
                
                return collection.FindAll().ToList();
            }
            catch (MongoConnectionException)
            {
                return new List<Note>();
            }
        }

        public BsonDocument CreateNote(Note note)
        {
            MongoCollection<Note> collection = GetNotesCollection();

            try
            {
                WriteConcernResult result = collection.Insert(note, WriteConcern.Acknowledged);

                return result.Response;
            }
            catch (MongoCommandException e)
            {
                return e.ToBsonDocument();
            }
        }

        private static MongoCollection<Note> GetNotesCollection()
        {
            var client = new MongoClient(ConnectionString);
            MongoDatabase database = client.GetServer().GetDatabase(DbName);
            MongoCollection<Note> notesCollection = database.GetCollection<Note>(CollectionName);

            return notesCollection;
        }
    }
}