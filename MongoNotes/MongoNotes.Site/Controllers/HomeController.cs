using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

using MongoNotes.Library.Models;

namespace MongoNotes.Site.Controllers
{
    public class HomeController : Controller
    {
        private const string BackendUrl = "http://YOUR-SERVICE-URL/api/note";

        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(BackendUrl);
                var notes = await response.Content.ReadAsAsync<List<Note>>();

                return View(notes); 
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Note note)
        {
            using (var client = new HttpClient())
            {
                await client.PostAsJsonAsync(BackendUrl, note); 
            }

            return RedirectToAction("Index");
        }
    }
}