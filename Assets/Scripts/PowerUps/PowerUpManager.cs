using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager powerUpMgrInstance;

    public GameObject bombUp;
    public GameObject fire;
    public GameObject skate;
    public GameObject skull;

    public List<GameObject> powerUpFullList;

    enum PowerUpsList
    {
        zeroPlaceHolder,
        BombUp,
        Fire,
        Skate,
        Skull
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
        //only keep first copy of script
        if (powerUpMgrInstance == null)
        {
            powerUpMgrInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        powerUpFullList.Add(bombUp);
        powerUpFullList.Add(fire);
        powerUpFullList.Add(skate);
        powerUpFullList.Add(skull);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnRandomPowerUp(Vector3 pos)
    {
        int pwrup = Random.Range(1, 5);

        

        switch (pwrup)
        {
            case (int)PowerUpsList.BombUp:
                Instantiate(bombUp, pos, Quaternion.identity);
                break;
            case (int)PowerUpsList.Fire:
                Instantiate(fire, pos, Quaternion.identity);
                break;
            case (int)PowerUpsList.Skate:
                Instantiate(skate, pos, Quaternion.identity);
                break;
            case (int)PowerUpsList.Skull:
                Instantiate(skull, pos, Quaternion.identity);
                break;
            default:
                break;
        }
    }

}
