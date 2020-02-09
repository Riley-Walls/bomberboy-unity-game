using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance;
    public List<LevelNameToSceneNameMapping> levelSceneNames;
    private int currentLevelIndex = 0;

    public int score = 0;
    public int lastLevelScore = 0;

    public int bombermanStartingLives = 5;
    [HideInInspector]
    public int bombermanLivesLeft = 5;
    [HideInInspector]
    public List<BaseEnemy> enemiesInLevel;

    public UnityAction onLivesLeftChange;
    public UnityAction onSceneChange;
    public UnityAction scoreChange;
    public UnityAction livesHitZero;

    [HideInInspector]
    public bool levelScannerHasLoaded = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        SceneManager.LoadScene(levelSceneNames[0].sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {

        bombermanLivesLeft = bombermanStartingLives;

        SoundMgr._instance.PlaySong(SoundMgr._instance.menuSong);

    }

    // Update is called once per frame
    void Update()
    {

        //DEBUG RESET KEY----------
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
        //DEBUG NEXT LEVEL KEY ----------
        if (Input.GetKeyDown(KeyCode.N))
        {
            if(currentLevelIndex + 1 < levelSceneNames.Count)
            {
                lastLevelScore = score;
                scoreChange.Invoke();
                loadNextLevel();
                
            }
        }



        if (bombermanLivesLeft <= 0)
        {
            if(livesHitZero != null)
            {
                livesHitZero.Invoke();
            }
        }

        if(levelScannerHasLoaded == true)
        {
            //LOAD THE NEXT LEVEL WHEN THERE ARE NO ENEMIES LEFT IN THE SCENE
            if (enemiesInLevel.Count == 0)
            {
                lastLevelScore = score;
                scoreChange.Invoke();
                if ((currentLevelIndex + 1) < levelSceneNames.Count)
                loadNextLevel();
            }
        }

        Debug.Log("Score: " + score);
    }

    private void resetLevelInfo()
    {
        enemiesInLevel.Clear();
        levelScannerHasLoaded = false;

        onSceneChange.Invoke();
    }

    public void loadNextLevel()
    {
        resetLevelInfo();
        currentLevelIndex++;
        SceneManager.LoadScene(levelSceneNames[currentLevelIndex].sceneName);

        onSceneChange.Invoke();
    }

    public void resetGame()
    {
        setLives(bombermanStartingLives);
        resetLevelInfo();
        SceneManager.LoadScene(levelSceneNames[0].sceneName);
        currentLevelIndex = 0;
        score = 0;
        scoreChange.Invoke();
        SoundMgr._instance.PlaySong(SoundMgr._instance.levelSong);

        onSceneChange.Invoke();
    }

    public void reloadCurrentLevel()
    {
        resetLevelInfo();

        levelScannerHasLoaded = false;
        SceneManager.LoadScene(levelSceneNames[currentLevelIndex].sceneName);
    }

    private void setLives(int num)
    {
        bombermanLivesLeft = num;
        onLivesLeftChange.Invoke();
    }

    public void adjustLives(int num)
    {
        setLives(bombermanLivesLeft + num);
    }

    public string GetCurrentLevelName()
    {
        return levelSceneNames[currentLevelIndex].levelName;
    }
}

[Serializable]
public class LevelNameToSceneNameMapping
{
    public String levelName;
    public String sceneName;
}