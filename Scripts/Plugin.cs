namespace MVRPlugins
{
    /// <summary>
    /// Plugin Definition
    /// </summary>
    public abstract class Plugin
    {
        /// <summary>
        /// Name of your kool plugin
        /// </summary>
        public virtual string Name => GetType().Name;

        /// <summary>
        /// Version of your plugin
        /// </summary>
        public virtual string Version => "1.0.0";

        /// <summary>
        /// Set by the server on startup
        /// </summary>
        public PluginAPI pluginAPI;

        /// <summary>
        /// Set by the server on startup
        /// </summary>
        public PluginLogging pluginLogging;

        /// <summary>
        /// Called by the server when this plugin is activated
        /// </summary>
        public virtual void OnEnable()
        {

        }

        /// <summary>
        /// Called by the server when this plugin is deactivated
        /// </summary>
        public virtual void OnDisable()
        {

        }
    }
}