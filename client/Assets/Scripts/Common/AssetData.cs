using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;

namespace MasterData 
{
    public static class AssetData
    {
        public static IEnumerable<asset> GetAssets()
        {
            var conn = new SQLiteConnection(Config.Instance.MasterDataPath, SQLiteOpenFlags.ReadOnly);
            return conn.Table<asset>();
        }
    }
}