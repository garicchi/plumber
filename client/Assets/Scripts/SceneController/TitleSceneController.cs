using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using MasterData;
using AssetData;

public class TitleSceneController : MonoBehaviour
{
    [SerializeField]
    public Button BtnTitleStart;
    // Start is called before the first frame update
    void Start()
    {
        BtnTitleStart.onClick.AddListener(OnTitleStartClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTitleStartClick() 
    {
        StartCoroutine(SvApi.GetMasterData(() => {
            StartCoroutine(AssetDownloader.DownloadAssetsAsync((progress) => {
                Debug.Log($"Donwload [{progress.current}/{progress.max}]");
                },
                () => {
                Debug.Log("Download Completed");
                SceneManager.LoadScene("HomeScene");
            }));
        }));
    }
}
