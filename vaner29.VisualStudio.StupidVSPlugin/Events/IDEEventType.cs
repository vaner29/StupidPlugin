using vaner29.VisualStudio.StupidVSPlugin.Helper;
using Microsoft.VisualStudio.Utilities;

namespace vaner29.VisualStudio.StupidVSPlugin
{
    public enum IDEEventType
    {
        [Name("-")]
        Unknown = 0,
        [Name(Consts.OptionsEnableAudioBuildFailsDescription)]
        BuildFails = 1,
        [Name(Consts.OptionsEnableAudioBuildingDescription)]
        Building = 2,
        [Name(Consts.OptionsEnableAudioBuildSuccessedDescription)]
        BuildSuccess = 3
    }
}
