using System;
using System.Collections.Generic;
using System.Linq;
using SimpleApi.Models;

namespace SimpleApi.Persistence
{
    public interface IValueRepository
    {
        IEnumerable<ValueModel> GetAll();
        ValueModel GetById(int id);
        void Add(ValueModel model);
        void Save(int id, ValueModel model);
        void Delete(int id);
    }
}