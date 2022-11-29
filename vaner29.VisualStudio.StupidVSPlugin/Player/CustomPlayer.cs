using vaner29.VisualStudio.StupidVSPlugin.Events;
using System.Media;

namespace vaner29.VisualStudio.StupidVSPlugin.Player
{
    public class CustomPlayer
    {
        private static CustomPlayer _instance;
        private static SoundPlayer _player = new SoundPlayer();

        public static CustomPlayer Instance => _instance ?? (_instance = new CustomPlayer());

        private CustomPlayer()
        {

        }

        public void PlaySound(IDEEventType iDEEventType, bool loop = false)
        {
            var path = IDEEventTypeMapper.IDEEventTypeToSoundPath(iDEEventType);
            _player.Stop();
            _player.SoundLocation = path;

            if (EventCatcher.Instance.OptionsPage.IsAudioActive(iDEEventType))
            {

                if (loop)
                {
                    _player.PlayLooping();
                }
                else
                {
                    _player.Play();
                }
            }
        }

        public void StopLoop()
        {
            _player.Stop();
        }
    }
}
