﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        StartCoroutine(SvApi.Get("login", (text) => {
            Debug.Log(text);
            throw new System.Exception(text);
        }));
        //SceneManager.LoadScene("HomeScene");
    }
}