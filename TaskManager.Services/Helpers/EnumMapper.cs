using System.Collections.Generic;
using TaskManager.Domain.Enums;
using TaskManager.Services.Tasks.Data;

namespace TaskManager.Services.Helpers
{
   public static class EnumMapper
    {
        public static Dictionary<UiTaskStatusEnum, TaskStatusEnum> TaskStatuses { get; }
        public static Dictionary<TaskStatusEnum, UiTaskStatusEnum> UiTaskStatuses { get; }
        static EnumMapper()
        {
            TaskStatuses = new Dictionary<UiTaskStatusEnum, TaskStatusEnum> {
                {UiTaskStatusEnum.Annuled, TaskStatusEnum.Annuled },
                {UiTaskStatusEnum.CLosed, TaskStatusEnum.CLosed },
                {UiTaskStatusEnum.New, TaskStatusEnum.New },
                {UiTaskStatusEnum.Ongoing, TaskStatusEnum.Ongoing },
            };
            UiTaskStatuses = new Dictionary<TaskStatusEnum, UiTaskStatusEnum> {
                {TaskStatusEnum.Annuled, UiTaskStatusEnum.Annuled },
                {TaskStatusEnum.CLosed, UiTaskStatusEnum.CLosed },
                {TaskStatusEnum.New, UiTaskStatusEnum.New },
                {TaskStatusEnum.Ongoing, UiTaskStatusEnum.Ongoing },
            };
        }
    }
}
