using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study_Buddy;

public static class TaskCategoryHelper
{
    public static string GetIcon(TaskCategory category)
    {
        return category switch
        {
            TaskCategory.Study => "📝",
            TaskCategory.Homework => "💻",
            TaskCategory.Project => "📋",
            TaskCategory.Revision => "🙂",
            _ => "📝"
        };
    }
        }
