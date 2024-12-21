using System;
using System.Collections.Generic;

namespace GameStartupSystem.Bootstrapper.Utility
{
    /// <summary>
    /// Dynamic version of Service Locator for Bootstrappers.
    /// </summary>
    public static class ServiceLocator
    {
        // Instead of dynamic dictionary we can have hardcoded services too.
        private static readonly Dictionary<Type, object> Services = new();

        public static void Register<T>(T service) where T : class
        {
            var type = typeof(T);
            if (!Services.TryAdd(type, service))
            {
                throw new InvalidOperationException($"Service of type {type.Name} is already registered.");
            }
        }

        public static T Resolve<T>() where T : class
        {
            var type = typeof(T);
            if (Services.TryGetValue(type, out var service))
            {
                return service as T;
            }

            throw new InvalidOperationException($"Service of type {type.Name} is not registered.");
        }

        public static void Clear()
        {
            Services.Clear();
        }
    }
}