using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EmployeeManager.ApiClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace EmployeeManager.ApiClient.Controllers
{
    [Authorize(Roles = "Manager")]
    public class EmployeeManagerController : Controller
    {
        private readonly HttpClient client = null;
        private string employeesApiUrl = null;

        public EmployeeManagerController(HttpClient client, IConfiguration config)
        {
            this.client = client;
            employeesApiUrl = config.GetValue<string>("AppSettings:EmployeesApiUrl");
        }

        public async Task<bool> FillCountriesAsync()
        {
            HttpResponseMessage response = await client.GetAsync(employeesApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var countries = JsonSerializer.Deserialize<List<Employee>>(stringData, options).Select(c =>
            c.Country).Distinct();

            List<SelectListItem> listOfCountries = new SelectList(countries).ToList();
            ViewBag.Countries = listOfCountries;
            return true;
        }

        public async Task<IActionResult> ListAsync()
        {
            HttpResponseMessage response = await client.GetAsync(employeesApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Deserialize stringData ignoring case as a List of Employees
            List<Employee> data = JsonSerializer.Deserialize<List<Employee>>(stringData,options);

            return View(data);
        }
    }
}
