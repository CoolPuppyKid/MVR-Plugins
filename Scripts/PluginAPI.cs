using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MVRPlugins
{
    /// <summary>
    /// Plugin -> Game API
    /// </summary>
    [Serializable]
    public class PluginAPI
    {
        private static readonly Dictionary<Type, List<Delegate>> _eventObservers = new();

        public void Subscribe<T>(Action<T> observer)
        {
            Type t = typeof(T);
            if (!_eventObservers.ContainsKey(t))
                _eventObservers[t] = new List<Delegate>();
            _eventObservers[t].Add(observer);
        }

        public void FireEvent<T>(T ev)
        {
            Type evType = typeof(T);

            if (evType.GetCustomAttribute<EventAttribute>() == null)
            {
                return;
            }

            if (_eventObservers == null) return;

            if (_eventObservers.TryGetValue(typeof(T), out var list))
            {
                foreach (Delegate observer in list)
                    (observer as Action<T>)?.Invoke(ev);
            }
        }
    }
}