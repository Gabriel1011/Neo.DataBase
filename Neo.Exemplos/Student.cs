using Neo.DataBaseRepository.Interface;

namespace Neo.Console
{
    public class Student : NeoEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}