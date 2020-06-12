using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    [SerializeField]
    Button BtnBack;
    public enum GameState 
    {
        Playing,
        Finish
    }
    private float remain = 0;
    private GameState state;
    // Start is called before the first frame update
    void Start()
    {
        remain = 3.0f;
        state = GameState.Playing;

        BtnBack.onClick.AddListener(OnBack);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Playing) 
        {
            remain -= Time.deltaTime;
            if (remain <= 0) 
            {
                state = GameState.Finish;
            }
        }
    }

    void OnBack() 
    {
        SceneManager.LoadScene("HomeScene");
    }
}
