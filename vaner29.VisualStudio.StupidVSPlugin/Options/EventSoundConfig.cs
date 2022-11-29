namespace vaner29.VisualStudio.StupidVSPlugin.Shared.Options
{
    public class EventSoundConfig
    {
        public EventSoundConfig()
        {

        }
        public EventSoundConfig(IDEEventType iDEEventType)
        {
            IDEEventType = iDEEventType;
            IsEnabled = true;
        }
        public IDEEventType IDEEventType { get; set; }
        public bool IsEnabled { get; set; }
    }
}
