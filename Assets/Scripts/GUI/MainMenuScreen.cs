using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{

    public Button startGameButton;

    public UnityAction startGameButtonClicked;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        startGameButton.onClick.AddListener(startGameButtonClickEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startGameButtonClickEvent()
    {
        startGameButtonClicked.Invoke();
    }
}
