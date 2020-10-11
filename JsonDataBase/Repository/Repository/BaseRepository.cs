using JsonDataBase.Helpers.Configurations;
using JsonDataBase.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JsonDataBase.Repository.Repository
{
    public class BaseRepository<T> where T : IJsonEntity
    {
        public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> filter)
        {
            using (StreamReader fs = new StreamReader(new FileStream(Configuration.LocalDataRepository, FileMode.Open, FileAccess.Read)))
            {
                var data = JsonConvert.DeserializeObject<IList<T>>(fs.ReadToEnd());
                return await Task.FromResult(data.AsQueryable().Where(filter));
            }
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            using (StreamReader fs = new StreamReader(new FileStream(Configuration.LocalDataRepository, FileMode.Open, FileAccess.Read)))
            {
                var data = JsonConvert.DeserializeObject<List<T>>(fs.ReadToEnd());
                return await Task.FromResult(data.AsQueryable().FirstOrDefault(filter));
            }
        }

        public async Task<T> Add(T entity)
        {
            var data = GetData();
            data.Add(entity);
            SetData(data);
            return await Task.FromResult(entity);
        }

        public async Task<T> Update(T entity)
        {
            var data = GetData();
            data.Remove(data.FirstOrDefault(p => p.Id == entity.Id));
            data.Add(entity);
            SetData(data);
            return await Task.FromResult(entity);
        }

        public void GerarJsonLista(IList<T> objeto)
        {
            using (StreamWriter fs = new StreamWriter(new FileStream(Configuration.LocalDataRepository, FileMode.Create, FileAccess.Write)))
                fs.Write(JsonConvert.SerializeObject(objeto, Formatting.Indented));
        }


        public async Task GerarJsons(T objeto)
        {
            using (StreamWriter fs = new StreamWriter(new FileStream(Configuration.LocalDataRepository, FileMode.Create, FileAccess.Write)))
                new JsonSerializer().Serialize(fs, objeto);
        }

        private static void SetData(IList<T> data)
        {
            using (StreamWriter fs = new StreamWriter(new FileStream(Configuration.LocalDataRepository, FileMode.Create, FileAccess.Write)))
                fs.Write(JsonConvert.SerializeObject(data, Formatting.Indented));
        }

        private static IList<T> GetData()
        {
            using (StreamReader fs = new StreamReader(new FileStream(Configuration.LocalDataRepository, FileMode.Open, FileAccess.Read)))
                return JsonConvert.DeserializeObject<IList<T>>(fs.ReadToEnd()) ?? new List<T>();
        }
    }
}
