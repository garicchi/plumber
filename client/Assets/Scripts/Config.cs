
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

    }

    public string SERVER_URL = "http://localhost:5000";

    public string STORAGE_URL = "http://localhost:9000";

    public string MasterDataPath
    {
        get 
        {
            return Path.Combine(Application.persistentDataPath, "masterdata.db");
        }
    }
  
}