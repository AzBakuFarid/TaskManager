using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Domain.DataAccessAbstractions;
using TaskManager.Domain.Entites;
using TaskManager.EfCore.Data;

namespace TaskManager.EfCore.Repositories
{
    public class TaskRepository : BaseRepository, ITaskRepository
    {
        public TaskRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public List<Task> ListForOrganization(int orgId)
        {
            var tasks = _dbContext.Tasks
                .Where(w => w.CreatedBy.OrganizationId == orgId)
                .Include("UserTasks.User").Include("CreatedBy")
                .OrderBy(o => o.Status).ThenByDescending(o => o.Deadline).ToList();
            return tasks;
        }
        public UserTask FindUserTask(string userId, int taskId) 
        {
            return _dbContext.UserTasks.Find(userId, taskId);
        }
    }
}
