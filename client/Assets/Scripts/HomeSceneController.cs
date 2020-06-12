using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        var homeTextureAb = AssetBundle.LoadFromFile("AssetBundle/Windows/asset01");
        var homeTexture = homeTextureAb.LoadAsset<Texture2D>("asset01");
        
        HomeSprite.GetComponent<SpriteRenderer>().sprite = Sprite.Create(homeTexture, new Rect(0.0f, 0.0f, homeTexture.width, homeTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
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
