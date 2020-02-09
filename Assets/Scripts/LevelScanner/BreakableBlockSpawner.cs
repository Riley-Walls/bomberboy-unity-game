using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlockSpawner : MonoBehaviour
{
    public GameObject breakableBlockPrefab;
    public int spawnChance = 30;
    bool spawnCompleted = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCompleted != true)
        {
            spawn();
        }
    }

    private void spawn()
    {
        
        if (LevelScanner._instance != null)
        {
            if (LevelScanner._instance.objectsAtCoords != null && LevelScanner._instance.objectsAtCoords.Count != 0)
            {
                
                foreach (KeyValuePair<Vector2, List<GameObject>> entry in LevelScanner._instance.objectsAtCoords)
                {
                    
                    bool isGroundOnly = true;
                    foreach (GameObject obj in entry.Value)
                    {
                        if (obj.GetComponent<IsGround>() == null)
                        {
                            isGroundOnly = false;
                        }
                    }
                    if(entry.Value.Count == 0)
                    {
                        isGroundOnly = false;
                    }


                    if (isGroundOnly)
                    {
                        if (Random.Range(0, 100) < spawnChance)
                        {
                            Instantiate(breakableBlockPrefab, new Vector3(entry.Key.x, 0, entry.Key.y), Quaternion.identity);
                        }
                    }
                }
                spawnCompleted = true;
            }
            else
            {
                Debug.Log("BreakableBlockSpawner: LevelScanner.objectsAtCoords is null");
            }
        }
        else
        {
            Debug.Log("BreakableBlockSpawner: LevelScanner is null");
        }
    }
}
