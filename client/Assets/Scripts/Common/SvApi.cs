using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class SvApi
{
    public static IEnumerator Get(string endpoint, Action<string> callback) 
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
            var text = www.downloadHandler.text;
            callback(text);
        }
    }
}
