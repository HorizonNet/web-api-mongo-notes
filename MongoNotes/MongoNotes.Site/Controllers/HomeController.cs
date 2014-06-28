using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

using MongoNotes.Library.Models;

namespace MongoNotes.Site.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://YOUR-SERVICE-URL/api/note");
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
                await client.PostAsJsonAsync("http://YOUR-SERVICE-URL/api/note", note); 
            }

            return RedirectToAction("Index");
        }
    }
}