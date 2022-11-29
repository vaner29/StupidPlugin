using System.IO;
using System.Reflection;

namespace vaner29.VisualStudio.StupidVSPlugin.Events
{
    public class IDEEventTypeMapper
    {

        public static string IDEEventTypeToSoundPath(IDEEventType iDEEventType)
        {
            string path = iDEEventType.ToString();
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Audio", $"{path}.wav");
        }

        public static string IDEEventTypeToVSAction(IDEEventType iDEEventType)
        {
        return string.Empty;
        }
    }
}
