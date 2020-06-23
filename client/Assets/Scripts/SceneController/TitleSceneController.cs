using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using MasterData;
using AssetData;
using Protocol;

public class TitleSceneController : MonoBehaviour
{
    [SerializeField]
    public Button BtnTitleStart;
    [SerializeField]
    public InputField InputServer;
    // Start is called before the first frame update
    void Start()
    {
        BtnTitleStart.onClick.AddListener(OnTitleStartClick);
        if (!PlayerPrefs.HasKey(Config.Instance.KEY_SERVER_URL))
        {
            InputServer.text = "http://localhost";
        }
        else
        {
            var s = Config.Instance.SERVER_URL.Split(':');
            InputServer.text = string.Join(":", s.Take(2));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTitleStartClick() 
    {
        Config.Instance.SERVER_URL = InputServer.text + ":5000";
        Config.Instance.STORAGE_URL = InputServer.text + ":9000";
        api_login_req req = new api_login_req();
        req.user_id = PlayerPrefs.GetString(Config.Instance.KEY_USER_ID, "");
        StartCoroutine(SvApi.LoginAsync(req, (res) =>
        {
            Config.Instance.USER_ID = res.user_id.ToString();
            StartCoroutine(SvApi.GetMasterData(() =>
            {
                StartCoroutine(AssetDownloader.DownloadAssetsAsync((progress) =>
                {
                    Debug.Log($"Donwload [{progress.current}/{progress.max}]");
                },
                    () =>
                    {
                        Debug.Log("Download Completed");
                        SceneManager.LoadScene("HomeScene");
                    }));
            }));
        }));
        
    }

}
