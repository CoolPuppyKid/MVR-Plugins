using UnityEngine;

namespace MVRPlugins
{
    public class PluginLogging : MonoBehaviour
    {
        /// <summary>
        /// Logs a warning with the red color
        /// </summary>
        public void LogErr(string msg)
        {
            Debug.Log("[Loaded Plugin Logging] [ERROR]: " + "<color=#EA0028>" + msg + "</color>");
        }

        /// <summary>
        /// Logs a warning with the white color
        /// </summary>
        public void LogInfo(string msg)
        {
            Debug.Log("[Loaded Plugin Logging] [Info]: " + msg);
        }

        /// <summary>
        /// Logs a warning with the yellow color
        /// </summary>
        public void LogWarn(string msg)
        {
            Debug.Log("[Loaded Plugin Logging] [Warning]: " + "<color=#EADE52>" + msg + "</color>");
        }
    }
}