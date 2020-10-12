using JsonDataBase.Helpers.Configurations;
using JsonDataBase.Helpers.Vadations;
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
    public class BaseRepository<T> where T : JsonEntity
    {
        private string LocalRepository;
        public BaseRepository()
        {
            Validation.ValidateConnection();
            LocalRepository = JsonDataBaseConfiguration.LocalDataRepository + typeof(T).Name + FileFormat.Json;
        }


        public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> filter)
        {
            using (StreamReader fs = new StreamReader(new FileStream(LocalRepository, FileMode.Open, FileAccess.Read)))
            {
                var data = JsonConvert.DeserializeObject<IList<T>>(fs.ReadToEnd());
                return await Task.FromResult(data.AsQueryable().Where(filter));
            }
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            using (StreamReader fs = new StreamReader(new FileStream(LocalRepository, FileMode.Open, FileAccess.Read)))
            {
                var data = JsonConvert.DeserializeObject<List<T>>(fs.ReadToEnd());
                return await Task.FromResult(data.AsQueryable().FirstOrDefault(filter));
            }
        }

        public async Task<T> Create(T entity)
        {
            var data = await GetData();
            data.Add(entity);
            await SetData(data);
            return await Task.FromResult(entity);
        }

        public async Task<T> Update(T entity)
        {
            var data = await GetData();
            data.Remove(data.FirstOrDefault(p => p.Id == entity.Id));
            data.Add(entity);
            await SetData(data);
            return await Task.FromResult(entity);
        }

        public async Task Delete(T entity)
        {
            var data = await GetData();
            data.Remove(data.FirstOrDefault(p => p.Id == entity.Id));
            await SetData(data);
        }
        public void GerarJsonLista(IList<T> objeto)
        {
            using (StreamWriter fs = new StreamWriter(new FileStream(LocalRepository, FileMode.Create, FileAccess.Write)))
                fs.Write(JsonConvert.SerializeObject(objeto, Formatting.Indented));
        }


        public async Task GerarJsons(T objeto)
        {
            using (StreamWriter fs = new StreamWriter(new FileStream(LocalRepository, FileMode.Create, FileAccess.Write)))
                await Task.Run(() => new JsonSerializer().Serialize(fs, objeto));
        }

        private async Task SetData(IList<T> data)
        {
            using (StreamWriter fs = new StreamWriter(new FileStream(LocalRepository, FileMode.Create, FileAccess.Write)))
                await Task.Run(() => fs.Write(JsonConvert.SerializeObject(data, Formatting.Indented)));
        }

        private async Task<IList<T>> GetData()
        {
            using (StreamReader fs = new StreamReader(new FileStream(LocalRepository, FileMode.Open, FileAccess.Read)))
                return await Task.FromResult(JsonConvert.DeserializeObject<IList<T>>(fs.ReadToEnd()) ?? new List<T>());
        }
    }
}
