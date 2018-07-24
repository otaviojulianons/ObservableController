using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using ObservableController;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("Developers/[action]")]
    public class DeveloperController : Controller, IObservableController<List<Developer>>
    {
        private DeveloperService _service;

        public DeveloperController(DeveloperService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<Developer> GetData() => _service.GetAll();

        [HttpPost]
        public void Add(string name) => _service.Insert(name);


        [HttpDelete]
        public void Delete(int id) => _service.Delete(id);


    }
}
