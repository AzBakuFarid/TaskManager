using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Services.UiServices.Models;

namespace TaskManager.Services.Tasks.Data
{
    public interface ITaskChangeAssigneData
    {
        int TaskId { get; set; }
        IEnumerable<string> Users { get; set; }
    }
    public class TaskChangeAssigneData : ITaskChangeAssigneData
    {
        public int TaskId { get; set; }
        public IEnumerable<string> Users { get; set; }
    }


}
