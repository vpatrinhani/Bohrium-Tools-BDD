using System;
using System.ComponentModel;

namespace Bohrium.Tools.BDDManagementTool.Infrastructure.Core.Extensions
{
    public static class EnumExtensionMethods
    {
        public static string GetDescription(this Enum e)
        {
            object[] attr = e.GetType().GetField(e.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attr.Length > 0
                ? ((DescriptionAttribute)attr[0]).Description
                : e.ToString();
        }

        public static T ParseEnum<T>(this string stringVal)
        {
            return (T)Enum.Parse(typeof(T), stringVal);
        }
    }
}