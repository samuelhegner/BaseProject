using System;
using Helpers;
using UnityEngine;

namespace Wrappers
{
    public static class PlayerPrefsWrapper
    {
        public static void SaveObject<TType>(TType saveObject, bool encrypt = false)
        {
            var key = typeof(TType).Name;
            var value = Serialize(saveObject);

            if (encrypt)
            {
                value = EncryptionHelper.EncryptText(key, value);
            }
            
            SaveString(key, value);
        }

        public static TType LoadObject<TType>(bool isEncrypted = false)
        {
            var key = typeof(TType).Name;
            var value = LoadString(key);

            if (value == null)
            {
                return default;
            }
            
            if (isEncrypted)
            {
                value = EncryptionHelper.DecryptText(key, value);
            }
            
            return Deserialize<TType>(value);
        }

        private static void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        
        private static string LoadString(string key)
        { 
            return PlayerPrefs.GetString(key, null);
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
