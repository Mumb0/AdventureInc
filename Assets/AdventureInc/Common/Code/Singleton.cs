using UnityEngine;

namespace AdventureInc
{
    /// <summary>
    /// Helper functions for finding singleton scripts
    /// </summary>
    public static class Singleton
    {
        /// <summary>
        /// Attempt to find a singleton script in the current scene
        /// </summary>
        /// <typeparam name="T">The type of the singleton. Can also be an interface</typeparam>
        /// <returns>The singleton if found</returns>
        public static T? TryFind<T>() where T : class
        {
            // NOTE: For this to work there must be a singleton container in the scene and the script be on the container
            var container = GameObject.FindWithTag("Singletons");
            if (container) return container.GetComponent<T>();

            Debug.LogWarning("Attempted to find singleton without a singleton-container in the scene!");
            return null;
        }
    }
}