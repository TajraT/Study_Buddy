using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Study_Buddy { 

public class TaskCategoryToIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
{
    if (value is TaskCategory category)
        return TaskCategoryHelper.GetIcon(category);
    return "📘";
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
{
    throw new NotImplementedException();
        }
    }
}

