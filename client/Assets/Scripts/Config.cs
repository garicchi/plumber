
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

    public string SERVER_URL = "http://localhost:5000";

    public string STORAGE_URL = "http://localhost:9000";

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