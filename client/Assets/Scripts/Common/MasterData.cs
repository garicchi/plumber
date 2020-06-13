using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;

public class asset
{
    public string path { get; set; }
    public string url { get; set; }
}

public class home_image
{
    public string name { get; set; }
    public string asset_path { get; set; }
}
public static class MasterData
{
    public static IEnumerable<asset> GetAssets()
    {
        var conn = new SQLiteConnection(Config.Instance.MasterDataPath, SQLiteOpenFlags.ReadOnly);
        return conn.Table<asset>();
    }

    public static IEnumerable<home_image> SelectHomeImage()
    {
        var conn = new SQLiteConnection(Config.Instance.MasterDataPath, SQLiteOpenFlags.ReadOnly);
        return conn.Table<home_image>();
    }
}