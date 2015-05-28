using System.Diagnostics;
using System.Reflection;

namespace Bohrium.Tools.BDDManagementTool.WebApp.Utils
{
    public class AppInfo
    {
        public static string GetAppVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return info.FileVersion;
        }
    }
}