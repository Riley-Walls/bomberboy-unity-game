using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour, IExplosionBehavior
{
    public int spawnPwrUpChance = 35;

    public bool checkIfExplosionPassesThrough()
    {
        return false;
    }

    public void Explode()
    {
        Vector3 pos = this.gameObject.transform.position;

        LevelManager._instance.score++;
        LevelManager._instance.scoreChange.Invoke();

        Destroy(gameObject);

        if (Random.Range(0, 100) < spawnPwrUpChance)
        {
            if(PowerUpManager.powerUpMgrInstance != null)
            {
                PowerUpManager.powerUpMgrInstance.spawnRandomPowerUp(pos);
            }
            else
            {
                Debug.Log("BreakableBlock: PowerUpManager instance is null!");
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
