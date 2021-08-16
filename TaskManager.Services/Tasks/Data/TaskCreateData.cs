using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManager.Services.Tasks.Data
{
    public interface ITaskCreatedata
    {
        string Title { get; set; }
        string Description { get; set; }
        DateTime? Deadline { get; set; }
    }
    public class TaskCreateModel : ITaskCreatedata
    {
        [Required, MaxLength(1000)] public string Title { get; set; }
        [Required, MaxLength(2000)] public string Description { get; set; }
        [Required] public DateTime? Deadline { get; set; }
    }
}
