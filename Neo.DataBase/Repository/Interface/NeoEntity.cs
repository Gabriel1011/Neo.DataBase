using System;

namespace Neo.DataBaseRepository.Interface
{
    public abstract class NeoEntity
    {
         public virtual Guid Id { get; set; }        
    }
}
