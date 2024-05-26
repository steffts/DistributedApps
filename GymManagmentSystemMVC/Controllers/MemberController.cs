using GymManagmentSystemMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GymManagmentSystemMVC.Controllers
{

    public class MemberController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44320/api");
        private readonly HttpClient _client;


        public MemberController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("XApiKey", "eae37b0e-950b-4e92d0b310326f66");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<MemberViewModel> memberList = new List<MemberViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Member/GetMembers").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                memberList = JsonConvert.DeserializeObject<List<MemberViewModel>>(data);
            }

            return View(memberList);
        }
        public async Task<IActionResult> Details(int? id)
        {
            MemberViewModel model = new MemberViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Member/GetMember/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<MemberViewModel>(data);
            }

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,DateOfBirth,MembershipType,CreatedAt,UpdatedAt")] MemberViewModel memberViewModel)
        {
            if (ModelState.IsValid)
            {
                MemberViewModel model = new MemberViewModel();
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Member/CreateMember/", JsonContent.Create(memberViewModel));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                MemberViewModel member = new MemberViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Member/GetMember/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    member = JsonConvert.DeserializeObject<MemberViewModel>(data);
                }
                return View(member);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }

        }

        [HttpPost]
        public IActionResult Edit(MemberViewModel member)
        {
            try
            {
                string data = JsonConvert.SerializeObject(member);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Member/UpdateMember/" + member.Id, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(member);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Member/DeleteMember/" + id).Result;

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