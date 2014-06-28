using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;

using MongoNotes.Library.Models;
using MongoNotes.Service.Dal;

namespace MongoNotes.Service.Controllers
{
    public class NoteController : ApiController
    {
        private readonly NoteRepository _repository;

        public NoteController()
        {
            _repository = new NoteRepository();
        }

        public List<Note> Get()
        {
            return _repository.GetAllNotes();
        }

        public HttpResponseMessage Create(Note note)
        {
            try
            {
                BsonDocument document = _repository.CreateNote(note);

                return Request.CreateResponse(HttpStatusCode.OK, document);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}