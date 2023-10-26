
using ConsumingWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
namespace ConsumingWebApi.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:44345/api");
        private readonly HttpClient _client;
        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Details()
        {
            List<UserViewModel> user = new List<UserViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/GetUsers").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Insert(UserViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/User/AddUser", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "User Created.";
                    return RedirectToAction("Details");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                UserViewModel user = new UserViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/GetUser/" + id).Result;        // it should be changed by get id action method
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<UserViewModel>(data);
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(UserViewModel model, int id)
        {
            try
            {
                UserViewModel user = new UserViewModel();
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/User/UpdateUser/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "User Updated.";
                    return RedirectToAction("Details");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                UserViewModel customer = new UserViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/GetUser/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    customer = JsonConvert.DeserializeObject<UserViewModel>(data);
                }
                return View(User);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/User/DeleteUser/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "User Deleted.";
                    return RedirectToAction("Details");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
    }
}





