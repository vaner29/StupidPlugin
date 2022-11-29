using vaner29.VisualStudio.StupidVSPlugin.Helper;
using vaner29.VisualStudio.StupidVSPlugin.Shared.Options;
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
using System.ComponentModel;

namespace vaner29.VisualStudio.StupidVSPlugin.Options
{
    public class OptionsPage : DialogPage
    {
        public Dictionary<IDEEventType, EventSoundConfig> _eventTypeConfig = SettingsManagerHelper.GetDefaultValues();

        public override void SaveSettingsToStorage()
        {
            base.SaveSettingsToStorage();
            SettingsManagerHelper.SaveData(_eventTypeConfig);
        }

        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();
            _eventTypeConfig = SettingsManagerHelper.LoadData();
        }

        public bool IsAudioActive(IDEEventType iDEEventType)
            => _eventTypeConfig[iDEEventType].IsEnabled;

        [Category(Consts.OptionSubmenu)]
        [DisplayName(Consts.OptionsEnableAudioBuildFailsText)]
        [Description(Consts.OptionsEnableAudioBuildFailsDescription)]
        public bool EnableAudioBuildFails
        {
            get
            {
                return _eventTypeConfig[IDEEventType.BuildFails].IsEnabled;
            }
            set
            {
                _eventTypeConfig[IDEEventType.BuildFails].IsEnabled = value;
            }
        }

        [Category(Consts.OptionSubmenu)]
        [DisplayName(Consts.OptionsEnableAudioBuildingText)]
        [Description(Consts.OptionsEnableAudioBuildingDescription)]
        public bool EnableAudioBuilding
        {
            get
            {
                return _eventTypeConfig[IDEEventType.Building].IsEnabled;
            }
            set
            {
                _eventTypeConfig[IDEEventType.Building].IsEnabled = value;
            }
        }

        [Category(Consts.OptionSubmenu)]
        [DisplayName(Consts.OptionsEnableAudioBuildSuccessText)]
        [Description(Consts.OptionsEnableAudioBuildSuccessedDescription)]
        public bool EnableBuildSuccessed
        {
            get
            {
                return _eventTypeConfig[IDEEventType.BuildSuccess].IsEnabled;
            }
            set
            {
                _eventTypeConfig[IDEEventType.BuildSuccess].IsEnabled = value;
            }
        }

    }
}
