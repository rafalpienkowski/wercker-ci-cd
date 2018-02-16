using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleApi.Models;
using SimpleApi.Persistence;

namespace SimpleApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IValueRepository _valueRepository;

        public ValuesController(IValueRepository valueRepository)
        {
            _valueRepository = valueRepository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<ValueModel> Get()
        {
            return _valueRepository.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ValueModel Get(int id)
        {
            return _valueRepository.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]ValueModel value)
        {
            _valueRepository.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]ValueModel value)
        {
            _valueRepository.Save(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
             _valueRepository.Delete(id);
        }
    }
}
