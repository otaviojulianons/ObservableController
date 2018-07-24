using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class LanguageService
    {
        private List<Language> _data = new List<Language>();

        public LanguageService()
        {
            _data.Add(new Language() { Id = 1, Name = "C#" });
            _data.Add(new Language() { Id = 2, Name = "Javascript" });
            _data.Add(new Language() { Id = 3, Name = "Java" });
        }

        public List<Language> GetAll()
        {
            return _data;
        }

        public void Insert(string name)
        {
            var max = _data.Count > 0 ? _data.Max(x => x.Id) : 0;
            _data.Add(new Language() { Name = name, Id = max + 1 });
        }

        public void Delete(int id)
        {
            _data.RemoveAll(l => l.Id == id);
        }
    }
}
