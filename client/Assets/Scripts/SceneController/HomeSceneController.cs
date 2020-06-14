using System.Collections;
using System.Collections.Generic;
using AssetData;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MasterData;
using System.Linq;

public class HomeSceneController : MonoBehaviour
{
    [SerializeField]
    public Button BtnGameStart;

    [SerializeField]
    public GameObject HomeSprite;
    // Start is called before the first frame update
    void Start()
    {
        BtnGameStart.onClick.AddListener(OnGameStartClick);
        var asset_data = m_home_asset.Select("WHERE name = ?", "home_texture01").First();
        StartCoroutine(AssetLoader.LoadTexture2DAsync(asset_data.asset_path, (texture) => {
            HomeSprite.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);    
        }));

        asset_data = m_home_asset.Select("WHERE name = ?", "home_prefab01").First();
        StartCoroutine(AssetLoader.LoadPrefabAsync(asset_data.asset_path, (obj) => {
            Instantiate(obj);
        }));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameStartClick() 
    {
        SceneManager.LoadScene("GameScene");
    }
}
