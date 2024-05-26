using GymManagmentSystemMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GymManagmentSystemMVC.Controllers
{

    public class TrainerController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44320/api");
        private readonly HttpClient _client;


        public TrainerController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("XApiKey", "eae37b0e-950b-4e92d0b310326f66");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<TrainerViewModel> trainerList = new List<TrainerViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Trainer/GetTrainers").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                trainerList = JsonConvert.DeserializeObject<List<TrainerViewModel>>(data);
            }

            return View(trainerList);
        }
        public async Task<IActionResult> Details(int? id)
        {
            TrainerViewModel model = new TrainerViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Trainer/GetTrainers/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<TrainerViewModel>(data);
            }

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Phone,Specialization,StartedAt,UpdatedAt")] TrainerViewModel trainerViewModel)
        {
            if (ModelState.IsValid)
            {
                TrainerViewModel model = new TrainerViewModel();
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Trainer/CreateTrainer/", JsonContent.Create(trainerViewModel));
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                TrainerViewModel trainer = new TrainerViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Trainer/GetTrainer/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    trainer = JsonConvert.DeserializeObject<TrainerViewModel>(data);
                }
                return View(trainer);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }

        }

        [HttpPost]
        public IActionResult Edit(TrainerViewModel trainer)
        {
            try
            {
                string data = JsonConvert.SerializeObject(trainer);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Trainer/UpdateTrainer/" + trainer.Id, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(trainer);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Trainer/DeleteTrainer/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
