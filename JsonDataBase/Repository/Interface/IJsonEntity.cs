using System;

namespace JsonDataBase.Repository.Interface
{
    public interface IJsonEntity
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
    }
}
