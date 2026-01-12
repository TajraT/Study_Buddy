
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study_Buddy
{
    public static class TaskService
    {
        public static ObservableCollection<TaskItem> Tasks { get; } = new();
    }
}
