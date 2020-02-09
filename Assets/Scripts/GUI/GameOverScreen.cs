using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Button resetButton;

    public UnityAction resetButtonClicked;

    public Text scoreDisplay;

    private bool resetFlag;

    // Start is called before the first frame update
    void Start()
    {
        resetButton.onClick.AddListener(resetButtonClickEvent);
        afterReset();
    }

    // Update is called once per frame
    void Update()
    {
        if (resetFlag)
        {
           afterReset();
        }
    }

    //use after game is restarted once
    void afterReset()
    {
        if (LevelManager._instance != null)
            scoreDisplay.text = LevelManager._instance.lastLevelScore + "";
        SoundMgr._instance.PlaySong(SoundMgr._instance.gameOverSong);
        SoundMgr._instance.ClearSfx();
        resetFlag = false;
    }

    private void resetButtonClickEvent()
    {
        LevelManager._instance.resetGame();
        Time.timeScale = 1;
        resetButtonClicked.Invoke();
        resetFlag = true;
    }
}
