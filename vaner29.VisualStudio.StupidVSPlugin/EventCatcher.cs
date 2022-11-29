using vaner29.VisualStudio.StupidVSPlugin.Player;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using vaner29.VisualStudio.StupidVSPlugin.Options;

namespace vaner29.VisualStudio.StupidVSPlugin
{
    public class EventCatcher
    {
        private DTE2 _dte;
        private Dictionary<string, CommandEvents> _actionCommandEvents = new Dictionary<string, CommandEvents>();
        private Events2 _events;
        private BuildEvents _buildEvents;
        private AsyncPackage _package;
        private static EventCatcher _instance;

        private EventCatcher()
        {
        }

        public static EventCatcher Instance => _instance ?? (_instance = new EventCatcher());

        public OptionsPage OptionsPage { get; private set; }

        public async Task<EventCatcher> InitAsync(AsyncPackage package, OptionsPage optionsPage)
        {
            if (_package != null)
            {
                return this;
            }

            OptionsPage = optionsPage;

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            _package = package;

            _dte = (DTE2)await _package.GetServiceAsync(typeof(DTE));
            Assumes.Present(_dte);

            _events = _dte.Events as Events2;
            _buildEvents = _events.BuildEvents;

            AddAction(IDEEventType.Building);

            return this;
        }

        [SuppressMessage("Usage", "VSTHRD010:Invoke single-threaded types on Main thread", Justification = "SwitchToMainThreadAsync added above")]
        private void AddAction(IDEEventType iDEEventType)
        {
            _buildEvents.OnBuildDone += Building_OnBuildDone;
            _buildEvents.OnBuildBegin += Building_OnBuildBegin;

        }
        private void Building_OnBuildBegin(vsBuildScope Scope, vsBuildAction Action)
        {
            SetSoundForSingleEvent(IDEEventType.Building, true);
        }

        private void Building_OnBuildDone(vsBuildScope Scope, vsBuildAction Action)
        {
            _ = System.Threading.Tasks.Task.Run(async () =>
              {
                  await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                  CustomPlayer.Instance.StopLoop();
                  if (_dte.Solution.SolutionBuild.LastBuildInfo != 0)
                  {
                      CustomPlayer.Instance.PlaySound(IDEEventType.BuildFails);
                  }
                  else
                  {
                      CustomPlayer.Instance.PlaySound(IDEEventType.BuildSuccess);
                  }
              }).ConfigureAwait(false);
        }

        private void SetSoundForSingleEvent(IDEEventType iDEEventType, bool loop)
        {
            _ = System.Threading.Tasks.Task.Run(async () =>
              {
                  await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                  CustomPlayer.Instance.PlaySound(iDEEventType, loop);
              }).ConfigureAwait(false);
        }
    }
}
