using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Collections;
using System.Linq;
using MasterData;

namespace AssetData 
{
    public class DownloadProgress
    {
        public int current { get; set; }
        public int max { get; set; }
    }
    public static class AssetDownloader
    {
        public static IEnumerable<asset> GetAssets()
        {
            return asset.Select();
        }

        private static IEnumerator GetRequestAsync(string url, Action<DownloadHandler> callback)
        {
            var www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                throw new Exception(www.error);
            }
            else
            {
                callback(www.downloadHandler);
            }
        }

        public static IEnumerator DownloadAssetsAsync(Action<DownloadProgress> onUpdateCallback, Action onCompletedCallback)
        {
            IList<asset> assetList = asset.Select().ToList();
            int maxAssets = assetList.Count;
            for (var index = 0; index < maxAssets; index++)
            {
                var asset_data = assetList[index];
                yield return GetRequestAsync(asset_data.url, (handler)=> {
                    var savePath = Path.Combine(Config.Instance.ABSaveRootPath, asset_data.path);
                    var saveDir = Path.GetDirectoryName(savePath);
                    Debug.Log(saveDir);
                    if (!Directory.Exists(saveDir))
                        Directory.CreateDirectory(saveDir);
                    File.WriteAllBytes(savePath, handler.data);
                    var progress = new DownloadProgress 
                    {
                        current = index,
                        max = maxAssets
                    };
                    onUpdateCallback(progress);
                });
            }
            onCompletedCallback();
        }
    }

    public static class AssetLoader
    {
        public static Dictionary<string, AssetBundle> LoadedAssetBundles = new Dictionary<string, AssetBundle>();
        private static IEnumerator LoadDependentAssetAsync(string assetPath, Action<AssetBundle> onComplete)
        {
            var absolutePath = Path.Combine(Config.Instance.PlatformAbSaveRootPath, assetPath);
            var depFile = Path.Combine(Config.Instance.PlatformAbSaveRootPath, assetPath + ".dep");
            var deps = File.ReadAllLines(depFile);
            foreach(var d in deps)
            {
                yield return LoadDependentAssetAsync(d, (ab) =>{});
            }
            AssetBundle result = null;
            if (!LoadedAssetBundles.ContainsKey(assetPath)) 
            {
                var requestCreate = AssetBundle.LoadFromFileAsync(absolutePath);
                LoadedAssetBundles[assetPath] = requestCreate.assetBundle;
                yield return requestCreate;
            }
            result = LoadedAssetBundles[assetPath];
            
            onComplete(result);
            
        }
        public static IEnumerator LoadTexture2DAsync(string path, Action<Texture2D> callback)
        {
            yield return LoadDependentAssetAsync(path, (assetBundle) => {
                var asset = assetBundle.LoadAsset<Texture2D>(assetBundle.name);
                callback(asset);
            });
        }

        public static IEnumerator LoadPrefabAsync(string path, Action<GameObject> callback)
        {
            yield return LoadDependentAssetAsync(path, (assetBundle) => {
                var asset = assetBundle.LoadAsset<GameObject>(assetBundle.name);
                callback(asset);
            });
        }
    }
}