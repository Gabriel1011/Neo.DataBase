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
    public class NeoRepository<TEntity> : INeoRepository<TEntity> where TEntity : NeoEntity
    {
        private string LocalRepository;
        public NeoRepository()
        {
            Validation.ValidateConnection();
            LocalRepository = NeoDataBaseonfiguration.LocalDataRepository + typeof(TEntity).Name + FileFormat.Json;
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter)
        {
            using (StreamReader fs = new StreamReader(new FileStream(LocalRepository, FileMode.Open, FileAccess.Read)))
            {
                var data = JsonConvert.DeserializeObject<IList<TEntity>>(fs.ReadToEnd());
                return await Task.FromResult(data.AsQueryable().Where(filter));
            }
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            using (StreamReader fs = new StreamReader(new FileStream(LocalRepository, FileMode.Open, FileAccess.Read)))
            {
                var data = JsonConvert.DeserializeObject<List<TEntity>>(fs.ReadToEnd());
                return await Task.FromResult(data?.AsQueryable().FirstOrDefault(filter));
            }
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var data = await GetDataAsync();
            data.Add(entity);
            await SetDataAsync(data);
            return await Task.FromResult(entity);
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var data = await GetDataAsync();
            data.Remove(data.FirstOrDefault(p => p.Id == entity.Id) ?? throw new Exception("Data not found to update."));
            data.Add(entity);
            await SetDataAsync(data);
            return await Task.FromResult(entity);
        }
        public async Task DeleteAsync(TEntity entity)
        {
            var data = await GetDataAsync();
            data.Remove(data.FirstOrDefault(p => p.Id == entity.Id) ?? throw new Exception("Data not found to delete."));
            await SetDataAsync(data);
        }
        public void GerarJsonListaAsync(IList<TEntity> objeto)
        {
            using (StreamWriter fs = new StreamWriter(new FileStream(LocalRepository, FileMode.Create, FileAccess.Write)))
                fs.Write(JsonConvert.SerializeObject(objeto, Formatting.Indented));
        }
        private async Task SetDataAsync(IList<TEntity> data)
        {
            using (StreamWriter fs = new StreamWriter(new FileStream(LocalRepository, FileMode.Create, FileAccess.Write)))
                await Task.Run(() => fs.Write(JsonConvert.SerializeObject(data, Formatting.Indented)));
        }
        private async Task<IList<TEntity>> GetDataAsync()
        {
            using (StreamReader fs = new StreamReader(new FileStream(LocalRepository, FileMode.Open, FileAccess.Read)))
                return await Task.FromResult(JsonConvert.DeserializeObject<IList<TEntity>>(fs.ReadToEnd()) ?? new List<TEntity>());
        }
    }
}
