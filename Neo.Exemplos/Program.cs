using Neo.Console;
using Neo.DataBaseDataBase;
using Neo.DataBaseRepository.Repository;
using System;

namespace Neo.Exemplos
{
    class Program
    {
        static void Main(string[] args)
        {
            NeoDataBaseGenerator.Generate();

            var repository = new NeoRepository<Student>();

            repository.Create(new Student { Id = Guid.NewGuid(), Name = "Test", Email = "teste@teste.com" });

            var student = repository.Get(p => p.Name == "Test");

            student.Name = "Test1";
            student = repository.Update(student);

            repository.Delete(student);
        }
    }
}
