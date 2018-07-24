using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using ObservableController;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("Languages/[action]")]
    public class LanguagesController : Controller, IObservableController<List<Language>>
    {
        private LanguageService _service;

        public LanguagesController(LanguageService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<Language> GetData() => _service.GetAll();

        [HttpPost]
        public void Add(string name) => _service.Insert(name);


        [HttpDelete]
        public void Delete(int id) => _service.Delete(id);


    }
}
