using System;
using UnityEngine;

namespace Helpers.PlayerPrefs
{
    public static class PlayerPrefsWrapper
    {
        public static void SaveObject<TType>(TType saveObject)
        {
            var key = typeof(TType).Name;
            
            UnityEngine.PlayerPrefs.SetString(key, Serialize(saveObject));
            UnityEngine.PlayerPrefs.Save();
        }

        public static TType LoadObject<TType>()
        {
            var key = typeof(TType).Name;
            var value = UnityEngine.PlayerPrefs.GetString(key, null);

            if (value == null)
            {
                return default;
            }
            
            return Deserialize<TType>(value);
        }

        private static string Serialize<TType>(TType saveObject)
        {
            try
            {
                return JsonUtility.ToJson(saveObject);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        private static TType Deserialize<TType>(string json)
        {
            try
            {
                return JsonUtility.FromJson<TType>(json);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }
}
