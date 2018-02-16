using System;
using System.Collections.Generic;
using System.Linq;
using SimpleApi.Models;

namespace SimpleApi.Persistence
{
    public class InMemoryValueRepository : IValueRepository
    {
        private readonly List<ValueModel> _valueModels = new List<ValueModel>();

        public void Add(ValueModel model)
        {
            model.Id = _valueModels.Count;
            _valueModels.Add(model);
        }

        public void Delete(int id)
        {
            var modelToDelete = _valueModels.FirstOrDefault(v => v.Id == id);
            if (modelToDelete != null)
            {
                _valueModels.Remove(modelToDelete);
            }
        }

        public IEnumerable<ValueModel> GetAll()
        {
            return _valueModels;
        }

        public ValueModel GetById(int id)
        {
            return _valueModels.FirstOrDefault(v => v.Id == id);
        }

        public void Save(int id, ValueModel value)
        {
            var modelToSave = _valueModels.FirstOrDefault(v => v.Id == id);
            if (modelToSave != null)
            {
                _valueModels.Remove(modelToSave);
                value.Id = id;
            }
            _valueModels.Add(value);
        }
    }
}