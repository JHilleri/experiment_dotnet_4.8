using System;
using System.Collections.Generic;

namespace todo.mvc
{
    public class Dependencies
    {
        private static readonly Dictionary<Type, Func<object>> factories = [];
        private static readonly Dictionary<Type, object> singletons = [];

        public void Register<T>(Func<T> factory)
        {
            factories.Add(typeof(T), () => factory());
        }

        public void RegisterSingleton<T>(Func<T> factory)
        {
            singletons.Add(typeof(T), factory());
        }

        public T Resolve<T>()
        {
            if (singletons.ContainsKey(typeof(T)))
            {
                return (T)singletons[typeof(T)];
            }
            if (factories.ContainsKey(typeof(T)))
            {
                return (T)factories[typeof(T)]();
            }
            throw new Exception($"No factory registered for type {typeof(T)}");
        }
    }
}
