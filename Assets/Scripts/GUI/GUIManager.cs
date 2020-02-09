using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public InGameHUD inGameHUD;
    public MainMenuScreen mainMenuScreen;
    public GameOverScreen gameOverScreen; 

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        mainMenuScreen.startGameButtonClicked += startGameButtonClickHandler;
        gameOverScreen.resetButtonClicked += resetButtonClickHandler;
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelManager._instance.livesHitZero += onLivesHitZero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGameButtonClickHandler()
    {
        Time.timeScale = 1;
        mainMenuScreen.gameObject.SetActive(false);
        inGameHUD.gameObject.SetActive(true);
        SoundMgr._instance.PlaySong(SoundMgr._instance.levelSong);
    }

    public void resetButtonClickHandler()
    {
        gameOverScreen.gameObject.SetActive(false);
        inGameHUD.gameObject.SetActive(true);
    }

    public void onLivesHitZero()
    {
        Time.timeScale = 0;
        inGameHUD.gameObject.SetActive(false);
        gameOverScreen.scoreDisplay.text = LevelManager._instance.score.ToString();
        gameOverScreen.gameObject.SetActive(true);

    }

}
