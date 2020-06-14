using System.Collections.Generic;
using SQLite4Unity3d;

namespace MasterData
{
    
    public class m_game_param
    {
        
        public string key { get; set; }
        
        public int value { get; set; }
        

        public static IEnumerable<m_game_param> Select(string cond="", params object[] args)
        {
            return (new SQLiteConnection(Config.Instance.MasterDataPath, SQLiteOpenFlags.ReadOnly))
                .Query<m_game_param>($"SELECT * FROM m_game_param {cond}", args);
        }
    }
    
    public class m_home_asset
    {
        
        public string name { get; set; }
        
        public string asset_path { get; set; }
        

        public static IEnumerable<m_home_asset> Select(string cond="", params object[] args)
        {
            return (new SQLiteConnection(Config.Instance.MasterDataPath, SQLiteOpenFlags.ReadOnly))
                .Query<m_home_asset>($"SELECT * FROM m_home_asset {cond}", args);
        }
    }
    
    public class asset
    {
        
        public string path { get; set; }
        
        public string url { get; set; }
        

        public static IEnumerable<asset> Select(string cond="", params object[] args)
        {
            return (new SQLiteConnection(Config.Instance.MasterDataPath, SQLiteOpenFlags.ReadOnly))
                .Query<asset>($"SELECT * FROM asset {cond}", args);
        }
    }
    
}