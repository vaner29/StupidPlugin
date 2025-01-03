﻿using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.Settings;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Shell;
using System.Linq;
using System.Text.Json;

namespace vaner29.VisualStudio.StupidVSPlugin.Shared.Options
{
    public static class SettingsManagerHelper
    {
        private const string EventTypeConfigName = "EventTypeConfigDict";
        public static Dictionary<IDEEventType, EventSoundConfig> LoadData()
        {
            var settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
            var userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            if (!userSettingsStore.PropertyExists(nameof(StupidVSPluginPackage), EventTypeConfigName))
            {
                return GetDefaultValues();
            }
            return JsonSerializer.Deserialize<Dictionary<IDEEventType, EventSoundConfig>>
                (userSettingsStore.GetString(nameof(StupidVSPluginPackage), EventTypeConfigName));
        }

        public static void SaveData(Dictionary<IDEEventType, EventSoundConfig> input)
        {
            var settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
            var userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            if (!userSettingsStore.CollectionExists(nameof(StupidVSPluginPackage)))
            {
                userSettingsStore.CreateCollection(nameof(StupidVSPluginPackage));
            }
            userSettingsStore.SetString(nameof(StupidVSPluginPackage), EventTypeConfigName, JsonSerializer.Serialize(input));
        }

        public static Dictionary<IDEEventType, EventSoundConfig> GetDefaultValues()
        => Enum.GetValues(typeof(IDEEventType)).Cast<IDEEventType>().ToList()
                .Select(x => new KeyValuePair<IDEEventType, EventSoundConfig>(x, new EventSoundConfig(x)))
                .ToDictionary(x => x.Key, x => x.Value);

    }
}
