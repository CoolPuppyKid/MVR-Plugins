using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MVRPlugins.Examples;
using UnityEngine;


namespace MVRPlugins
{
    /// <summary>
    /// The server manager for plugins
    /// </summary>
    public class PluginManager : MonoBehaviour
    {
        private readonly List<Plugin> plugins = new();
        private string pluginPath;

        // logging stuf
        private PluginManagerLogging pluginManagerLogging;
        private PluginLogging pluginLogging;

        // the goofy ahh api
        static PluginAPI currentAPI;

        private void Awake()
        {
            pluginPath = Path.Combine(Application.persistentDataPath, "MVR_Plugins");
            CreateAPI();
            CreateLogging();
            LoadPlugins();
        }

        void Start()
        {
            currentAPI.FireEvent(new ExampleEvent(82));
        }


        /// <summary>
        /// Creates a API (You need to override this to make a API)
        /// </summary>
        private void CreateAPI()
        {
            // ngl this check might be redundent if this is used properly but meh
            if (currentAPI == null)
            {
                currentAPI = new PluginAPI();
            }
        }

        /// <summary>
        /// Creates a Logging component for logging
        /// </summary>
        private void CreateLogging()
        {
            // gyatt to make pluginManagerLogging
            if (TryGetComponent(out PluginManagerLogging pluginManagerLoggingComponent))
            {
                pluginManagerLogging = pluginManagerLoggingComponent;
            }
            else
            {
                pluginManagerLogging = gameObject.AddComponent<PluginManagerLogging>();
            }

            // then gyatt to make pluginLogging
            if (TryGetComponent(out PluginLogging pluginLoggingComponent))
            {
                pluginLogging = pluginLoggingComponent;
            }
            else
            {
                pluginLogging = gameObject.AddComponent<PluginLogging>();
            }
        }

        /// <summary>
        /// Loads all the plugins
        /// </summary>

        // TODO: maybe change this from System.Reflection to maybe straight up compiling the source code
        private void LoadPlugins()
        {
            if (!Directory.Exists(pluginPath))
            {
                Directory.CreateDirectory(pluginPath);
                pluginManagerLogging.LogWarn($"Could not find {pluginPath} so created it");
            }

            foreach (string dll in Directory.GetFiles(pluginPath, "*.dll"))
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(dll);

                    // I hate using var but im not bouta figure out what ts is
                    var pluginTypes = assembly.GetTypes().Where(t => typeof(Plugin).IsAssignableFrom(t) && !t.IsAbstract);

                    foreach (Type type in pluginTypes)
                    {
                        var plugin = (Plugin)Activator.CreateInstance(type);
                        plugin.pluginLogging = pluginLogging;
                        GiveApiToPlugin(plugin);

                        plugins.Add(plugin);
                        pluginManagerLogging.LogInfo($"Loaded plugin with name: <color=#ACEC6D>{plugin.Name}</color>, With version: {plugin.Version}");
                        
                        plugin.OnEnable();
                    }
                }
                catch (Exception err)
                {
                    pluginManagerLogging.LogErr($"{err.StackTrace},\nthis happend when loading: {dll}");
                }
            }

            if (plugins.Count == 0)
            {
                pluginManagerLogging.LogInfo("Found <color=#EA0028>no plugins</color>");
            }
        }

        /// <summary>
        /// Sets the pluginAPI of a plugin
        /// </summary>
        /// <param name="plugin">The plugin to give the API to</param>
        private void GiveApiToPlugin(Plugin plugin)
        {
            plugin.pluginAPI = currentAPI;
        }

        private void OnApplicationQuit()
        {
            foreach (Plugin plugin in plugins)
            {
                try
                {
                    plugin.OnDisable();
                    pluginManagerLogging.LogInfo($"Disabled {plugin.Name}");
                }
                catch (Exception err) { pluginManagerLogging.LogErr(err.Message); }
            }
        }
    }
}