using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameSceneController : MonoBehaviour
{
    [SerializeField]
    Button BtnBack;

    [SerializeField]
    Button BtnAttack;

    [SerializeField]
    Text TextTime;

    [SerializeField]
    Text TextScore;
    public enum GameState 
    {
        Playing,
        Finish
    }
    private float remain = 0;
    private int score;
    private int score_point_attack;
    private GameState state;
    // Start is called before the first frame update
    void Start()
    {
        remain = MasterData.m_game_param.Select($"WHERE key = 'init_game_time_sec'").First().value;
        score = 0;
        score_point_attack = MasterData.m_game_param.Select($"WHERE key = 'score_point_attack'").First().value;
        state = GameState.Playing;

        BtnBack.onClick.AddListener(OnBack);
        BtnAttack.onClick.AddListener(OnAttack);
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
            TextTime.text = Math.Round(remain, 1).ToString();
            TextScore.text = score.ToString();
        }
    }

    void OnAttack()
    {
        if (state == GameState.Playing) 
        {
            score += score_point_attack;
        }
    }

    void OnBack() 
    {
        SceneManager.LoadScene("HomeScene");
    }
}
