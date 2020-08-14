using System;
using System.Collections;
using System.Collections.Generic;
using Protocol;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public static class SvApi
{
    
    private static IEnumerator GetAsync(string endpoint, Action<DownloadHandler> callback)
    {
        var url = $"{Config.Instance.API_URL}{endpoint}";
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

    private static IEnumerator PostAsync(string endpoint, string data, Action<DownloadHandler> callback)
    {
        var url = $"{Config.Instance.API_URL}{endpoint}";
        Debug.Log(url);
        var www = UnityWebRequest.Post(url, data);
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
    public static IEnumerator GetMasterData(Action callback)
    {
        yield return GetAsync("/masterdata", (handler) => {
            var dest = Config.Instance.MasterDataPath;
            File.WriteAllBytes(dest, handler.data);
            callback();
        });
    }

    public static IEnumerator LoginAsync(api_login_req data, Action<api_login_res> callback) 
    {
        yield return PostAsync(api_login_req.GetEndpoint(), data.Serialize(), (handler) => {
            callback(api_login_res.Deserialize(handler.text));
        });
    }

    public static IEnumerator FinishGameAsync(api_finish_game_req data, Action<api_finish_game_res> callback) 
    {
        yield return PostAsync(api_finish_game_req.GetEndpoint(), data.Serialize(), (handler) => {
            callback(api_finish_game_res.Deserialize(handler.text));
        });
    }
}
