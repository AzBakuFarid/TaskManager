using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entites;

namespace TaskManager.Domain.Interfaces
{
    public interface IAuditable
    {
        DateTime CreatedAt { get; set; }
        string CreatedById { get; set; }
        //DateTime? ModifiedAt { get; set; }
        //string ModifiedById { get; set; }
    }

}
