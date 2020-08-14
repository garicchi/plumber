
using System.IO;
using UnityEngine;

public class Config
{
    private static Config _instance = null;
    public static Config Instance
    {
        get
        {
            if (Config._instance == null)
                Config._instance = new Config();
            return Config._instance;
        }
    }
    private Config() 
    {
        if (!Directory.Exists(ABSaveRootPath))
        {
            Directory.CreateDirectory(ABSaveRootPath);
        }

        switch(Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
                PLATFORM_STRING = "Windows";
                break;
            case RuntimePlatform.WindowsEditor:
                PLATFORM_STRING = "Windows";
                break;
            case RuntimePlatform.Android:
                PLATFORM_STRING = "Android";
                break;
            case RuntimePlatform.IPhonePlayer:
                PLATFORM_STRING = "iOS";
                break;
        }
    }

    public string KEY_API_HOST = "API_HOST";
    public string API_HOST
    {
        get 
        {
            return PlayerPrefs.GetString(KEY_API_HOST, "localhost");
        }
        set
        {
            PlayerPrefs.SetString(KEY_API_HOST, value);
        }
    }

    public string API_URL
    {
        get
        {
            return $"http://{API_HOST}:5000";
        }
    }

    public string KEY_STORAGE_HOST = "STORAGE_HOST";
    public string STORAGE_HOST
    {
        get 
        {
            return PlayerPrefs.GetString(KEY_STORAGE_HOST, "localhost");
        }
        set
        {
            PlayerPrefs.SetString(KEY_STORAGE_HOST, value);
        }
    }
    
    public string STORAGE_URL
    {
        get
        {
            return $"http://{STORAGE_HOST}:9000";
        }
    }

    public string KEY_USER_ID = "USER_ID";
    public string USER_ID
    {
        get 
        {
            return PlayerPrefs.GetString(KEY_USER_ID, "unknown user");
        }
        set
        {
            PlayerPrefs.SetString(KEY_USER_ID, value);
        }
    }

    public string PLATFORM_STRING = "Unknown";


    public string MasterDataPath
    {
        get 
        {
            return Path.Combine(Application.persistentDataPath, "masterdata.db");
        }
    }
    
    public string ABSaveRootPath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, "assetbundles");
        }
    }

    public string PlatformAbSaveRootPath
    {
        get
        {
            return Path.Combine(ABSaveRootPath, PLATFORM_STRING);
        }
    }
  
}