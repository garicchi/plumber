using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

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
            Debug.Log("DownloadCompleted");
            var a = MasterData.SelectHomeImage().Where(q => q.name == "home_char01").ToList();
            int b = 0;
        }));
        
        //SceneManager.LoadScene("HomeScene");
    }
}
