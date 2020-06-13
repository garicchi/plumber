using System;
using System.Collections;
using System.Collections.Generic;
using Proto;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public static class SvApi
{
    public static IEnumerator GetMasterData(Action callback)
    {
        var url = $"{Config.Instance.SERVER_URL}/masterdata";
        var www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError) 
        {
            throw new Exception(www.error);
        }
        else
        {
            var data = www.downloadHandler.data;
            var dest = Path.Combine(Application.persistentDataPath, "masterdata.db");
            File.WriteAllBytes(dest, data);
            callback();
        }
    }

    public static IEnumerator GetProduct(string endpoint, Action<Product> callback) 
    {
        var url = $"{Config.Instance.SERVER_URL}/{endpoint}";
        var www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError) 
        {
            throw new Exception(www.error);
        }
        else
        {
            var data = www.downloadHandler.data;
            var d = Product.Parser.ParseFrom(data);
            callback(d);
        }
    }
}
