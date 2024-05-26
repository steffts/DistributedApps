using GymManagmentSystem.DataContext;
using GymManagmentSystemMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;


    public class ClassController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44320/api");
        private readonly HttpClient _client;


        public ClassController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("XApiKey", "eae37b0e-950b-4e92d0b310326f66");
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<ClassViewModel> classList = new List<ClassViewModel>();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Class/GetClasses").Result;

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            classList = JsonConvert.DeserializeObject<List<ClassViewModel>>(data);
        }

        return View(classList);
    }
    public async Task<IActionResult> Details(int? id)
    {
        ClassViewModel model = new ClassViewModel();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Class/GetClass/" + id).Result;

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            model = JsonConvert.DeserializeObject<ClassViewModel>(data);
        }

        return View(model);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,TrainerId,StartTime,EndTime,Capacity,Price")] ClassViewModel classViewModel)
    {
        if (ModelState.IsValid)
        {
            ClassViewModel model = new ClassViewModel();
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Class/CreateClass/", JsonContent.Create(classViewModel));
        }
        return RedirectToAction(nameof(Index));
    }



    [HttpGet]
    public IActionResult Edit(int id)
    {
        try
        {
            ClassViewModel classes = new ClassViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Class/GetClass/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                classes = JsonConvert.DeserializeObject<ClassViewModel>(data);
            }
            return View(classes);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View();
        }

    }

    [HttpPost]
    public IActionResult Edit(ClassViewModel classes)
    {
        try
        {
            string data = JsonConvert.SerializeObject(classes);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Class/UpdateClass/" + classes.Id, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
        }

        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View(classes);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        try
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Class/DeleteClass/" + id).Result;

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



