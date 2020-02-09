using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameHUD : MonoBehaviour
{
    public Text LivesDisplay;
    public Text LevelNameDisplay;
    public Text ScoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager._instance.onLivesLeftChange += updateLivesDisplay;
        LevelManager._instance.onSceneChange += updateLevelNameDisplay;
        LevelManager._instance.scoreChange += updateScoreDisplay;

        updateLivesDisplay();
        updateLevelNameDisplay();
        updateScoreDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateLivesDisplay()
    {
        LivesDisplay.text = LevelManager._instance.bombermanLivesLeft + "";
    }

    void updateLevelNameDisplay()
    {
        LevelNameDisplay.text = LevelManager._instance.GetCurrentLevelName();
    }

    void updateScoreDisplay()
    {
        ScoreDisplay.text = LevelManager._instance.score + "";
    }

}
