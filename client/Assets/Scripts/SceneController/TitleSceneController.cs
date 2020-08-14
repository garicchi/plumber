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
    public InputField InputApiServer;
    [SerializeField]
    public InputField InputStorageServer;
    // Start is called before the first frame update
    void Start()
    {
        BtnTitleStart.onClick.AddListener(OnTitleStartClick);
        if (!PlayerPrefs.HasKey(Config.Instance.KEY_API_HOST))
        {
            InputApiServer.text = "localhost";
        }
        else
        {
            InputApiServer.text = Config.Instance.API_HOST;
        }
        if (!PlayerPrefs.HasKey(Config.Instance.KEY_STORAGE_HOST))
        {
            InputStorageServer.text = "localhost";
        }
        else
        {
            InputStorageServer.text = Config.Instance.STORAGE_HOST;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTitleStartClick() 
    {
        Config.Instance.API_HOST = InputApiServer.text;
        Config.Instance.STORAGE_HOST = InputStorageServer.text;
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
