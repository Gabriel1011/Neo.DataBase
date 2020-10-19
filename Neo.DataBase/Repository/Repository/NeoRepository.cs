using Neo.DataBase.Repository.Interface.Repository;
using Neo.DataBaseHelpers.Configurations;
using Neo.DataBaseHelpers.Vadations;
using Neo.DataBaseRepository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Neo.DataBaseRepository.Repository
{
    public class NeoRepository<TEntity> : INeoRepository<TEntity>, INeoRepositoryAsync<TEntity> where TEntity : NeoEntity
    {
        private string LocalRepository;
        public NeoRepository()
        {
            Validation.ValidateConnection();
            LocalRepository = NeoDataBaseConfiguration.LocalDataRepository + typeof(TEntity).Name + FileFormat.Json;
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter) => GetListAsync(filter).Result;
        public TEntity Get(Expression<Func<TEntity, bool>> filter) => GetAsync(filter).Result;
        public TEntity Create(TEntity entity) => CreateAsync(entity).Result;
        public IEnumerable<TEntity> CreateList(IEnumerable<TEntity> entities) => CreateListAsync(entities).Result;
        public TEntity Update(TEntity entity) => UpdateAsync(entity).Result;
        public IEnumerable<TEntity> UpdateList(IEnumerable<TEntity> entities) => UpdateListAsync(entities).Result;
        public void Delete(TEntity entity) => DeleteAsync(entity).ConfigureAwait(false);
        public void DeleteList(IEnumerable<TEntity> entities) => DeleteListAsync(entities).ConfigureAwait(false);

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
        public async Task<IEnumerable<TEntity>> CreateListAsync(IEnumerable<TEntity> entities)
        {
            var data = await GetDataAsync();
            data.ToList().AddRange(entities);
            await SetDataAsync(data);
            return await Task.FromResult(entities);
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var data = await GetDataAsync();
            data.Remove(data.FirstOrDefault(p => p.Id == entity.Id) ?? throw new Exception("Data not found to update."));
            data.Add(entity);
            await SetDataAsync(data);
            return await Task.FromResult(entity);
        }
        public async Task<IEnumerable<TEntity>> UpdateListAsync(IEnumerable<TEntity> entities)
        {
            var data = await GetDataAsync();
            entities.ToList().ForEach(entity => { data.Remove(data.FirstOrDefault(p => p.Id == entity.Id) ?? throw new Exception("Data not found to delete.")); });
            data.ToList().AddRange(entities);
            await SetDataAsync(data);
            return await Task.FromResult(entities);
        }
        public async Task DeleteAsync(TEntity entity)
        {
            var data = await GetDataAsync();
            data.Remove(data.FirstOrDefault(p => p.Id == entity.Id) ?? throw new Exception("Data not found to delete."));
            await SetDataAsync(data);
        }
        public async Task DeleteListAsync(IEnumerable<TEntity> entities)
        {
            var data = await GetDataAsync();
            entities.ToList().ForEach(entity => { data.Remove(data.FirstOrDefault(p => p.Id == entity.Id) ?? throw new Exception("Data not found to delete.")); });
            await SetDataAsync(data);
        }
        private async Task SetDataAsync(IEnumerable<TEntity> data)
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
