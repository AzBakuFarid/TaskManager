using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Domain.Interfaces
{
    public interface IPrimaryKey<TKey>
    {
        TKey Id { get; set; }
    }
}
