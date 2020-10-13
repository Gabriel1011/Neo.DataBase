using Neo.DataBaseHelpers.Configurations;
using Neo.DataBaseHelpers.Vadations;
using Neo.DataBaseRepository.Interface;
using Neo.DataBaseRepository.Interface.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Neo.DataBaseRepository.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : NeoEntity
    {
        private string LocalRepository;
        public BaseRepository()
        {
            Validation.ValidateConnection();
            LocalRepository = NeoDataBaseonfiguration.LocalDataRepository + typeof(TEntity).Name + FileFormat.Json;
        }


        public async Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> filter)
        {
            using (StreamReader fs = new StreamReader(new FileStream(LocalRepository, FileMode.Open, FileAccess.Read)))
            {
                var data = JsonConvert.DeserializeObject<IList<TEntity>>(fs.ReadToEnd());
                return await Task.FromResult(data.AsQueryable().Where(filter));
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (StreamReader fs = new StreamReader(new FileStream(LocalRepository, FileMode.Open, FileAccess.Read)))
            {
                var data = JsonConvert.DeserializeObject<List<TEntity>>(fs.ReadToEnd());
                return await Task.FromResult(data?.AsQueryable().FirstOrDefault(filter));
            }
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var data = await GetData();
            data.Add(entity);
            await SetData(data);
            return await Task.FromResult(entity);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var data = await GetData();
            data.Remove(data.FirstOrDefault(p => p.Id == entity.Id) ?? throw new Exception("Data not found to update."));
            data.Add(entity);
            await SetData(data);
            return await Task.FromResult(entity);
        }

        public async Task Delete(TEntity entity)
        {
            var data = await GetData();
            data.Remove(data.FirstOrDefault(p => p.Id == entity.Id) ?? throw new Exception("Data not found to delete."));
            await SetData(data);
        }
        public void GerarJsonLista(IList<TEntity> objeto)
        {
            using (StreamWriter fs = new StreamWriter(new FileStream(LocalRepository, FileMode.Create, FileAccess.Write)))
                fs.Write(JsonConvert.SerializeObject(objeto, Formatting.Indented));
        }

        private async Task SetData(IList<TEntity> data)
        {
            using (StreamWriter fs = new StreamWriter(new FileStream(LocalRepository, FileMode.Create, FileAccess.Write)))
                await Task.Run(() => fs.Write(JsonConvert.SerializeObject(data, Formatting.Indented)));
        }

        private async Task<IList<TEntity>> GetData()
        {
            using (StreamReader fs = new StreamReader(new FileStream(LocalRepository, FileMode.Open, FileAccess.Read)))
                return await Task.FromResult(JsonConvert.DeserializeObject<IList<TEntity>>(fs.ReadToEnd()) ?? new List<TEntity>());
        }
    }
}
