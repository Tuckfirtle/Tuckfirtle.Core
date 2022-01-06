// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using Newtonsoft.Json;

namespace Tuckfirtle.Core.Utility
{
    /// <summary>
    /// Utility class that contains all the json functions.
    /// </summary>
    public static class JsonUtility
    {
        /// <summary>Serializes the specified object to a JSON string.</summary>
        /// <param name="value">The object to serialize.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public static string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>Deserializes the JSON to the specified .NET type.</summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="value">The JSON to deserialize.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}