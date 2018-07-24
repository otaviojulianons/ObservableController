using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class DeveloperService
    {
        private List<Developer> _data = new List<Developer>();

        public DeveloperService()
        {
            _data.Add(new Developer() { Id = 1, Name = "Otávio" });
            _data.Add(new Developer() { Id = 2, Name = "Nathalia" });
        }

        public List<Developer> GetAll()
        {
            return _data;
        }

        public void Insert(string name)
        {
            var max = _data.Count > 0 ? _data.Max(x => x.Id) : 0;
            _data.Add(new Developer() { Name = name, Id = max + 1});
        }

        public void Delete(int id)
        {
            _data.RemoveAll(l => l.Id == id);
        }
    }
}
