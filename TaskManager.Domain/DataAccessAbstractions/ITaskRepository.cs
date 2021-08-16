using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entites;

namespace TaskManager.Domain.DataAccessAbstractions
{
    public interface ITaskRepository : IBaseRepository
    {
        List<Task> ListForOrganization(int orgId);
        UserTask FindUserTask(string userId, int taskId);
    }
}
