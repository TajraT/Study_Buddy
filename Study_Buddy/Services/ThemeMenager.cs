using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Study_Buddy.Color;

namespace Study_Buddy.Services
{
    public static class ThemeMenager
    {
        private const string ThemeKey = "SelectedTheme";
        public static void ApplyTheme(string theme)
        {
            ResourceDictionary dictionary;
            switch (theme)
            {
                case "Warm":
                    dictionary = new WarmTone();
                    break;
                case "Cold":
                    dictionary = new ColdTone();
                    break;
                default:
                    dictionary = new NeutralTone();
                    theme = "Neutral";
                    break;
            }
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
            Preferences.Set(ThemeKey, theme);
        }
        public static void LoadTheme()
        {
            string theme = Preferences.Get(ThemeKey, "Neutral");
            ApplyTheme(theme);
        }
    }
}
