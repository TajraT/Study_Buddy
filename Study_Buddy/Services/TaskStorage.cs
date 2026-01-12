using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Study_Buddy
{
   public static class TaskStorage
    {
        private const string TasksKey = "tasks";

        public static void SaveTasks() {
            var json = JsonSerializer.Serialize(TaskService.Tasks);
            Preferences.Set(TasksKey, json);
        }

        public static void LoadTasks() {
            if (!Preferences.ContainsKey(TasksKey))
                return;
            var json = Preferences.Get(TasksKey, string.Empty);
            var tasks = JsonSerializer.Deserialize<List<TaskItem>>(json);
            if (tasks == null)
                return;
            TaskService.Tasks.Clear();
            foreach (var task in tasks)
                TaskService.Tasks.Add(task);

        }
    }
}
