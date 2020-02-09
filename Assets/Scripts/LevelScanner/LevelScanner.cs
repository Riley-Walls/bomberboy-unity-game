using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SINGLETON
public class LevelScanner : MonoBehaviour
{
    public static LevelScanner _instance;

    public int scanSize = 10;
    public int scanY = 5;
    public int scanDepth = -10;
    public Transform bottomRightBlock;
    private LevelManager _levelManager;

    private bool scanCompleted = false;

    //DEBUG
    public int x = 0;
    public int z = 0;

    public IDictionary<Vector2, List<GameObject>> objectsAtCoords;

    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }

    void Start() {
        objectsAtCoords = new Dictionary<Vector2, List<GameObject>>();
        scan();
        scanCompleted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (scanCompleted == false)
        {
            scan();
            scanCompleted = true;
        }

        Debug.Log("Enemies in level: " + LevelManager._instance.enemiesInLevel.Count);
    }

    private void scan()
    {
        objectsAtCoords = new Dictionary<Vector2, List<GameObject>>();

        for (int i = 0; i < scanSize; i++)
        {
            for (int j = 0; j < scanSize; j++)
            {
                //Use bottomRightBlock's position as 0,0
                float RayStartX = bottomRightBlock.position.x + i;
                float RayStartZ = bottomRightBlock.position.z + j;

                //Debug.DrawRay(new Vector3(RayStartX, scanY, RayStartZ), new Vector3(0, scanDepth, 0), Color.green, 5);
                RaycastHit[] hits;
                hits = Physics.RaycastAll(new Vector3(RayStartX, scanY, RayStartZ), new Vector3(0, scanDepth, 0), Mathf.Abs(scanDepth));

                List<GameObject> objsHitList = new List<GameObject>();
                foreach (RaycastHit hit in hits)
                {
                    objsHitList.Add(hit.collider.gameObject);
                    if(hit.collider.gameObject.GetComponent<BaseEnemy>() != null)
                    {
                        if(LevelManager._instance == null)
                        {
                            Debug.Log("LevelScanner: LevelManager is null!");
                        }
                        LevelManager._instance.enemiesInLevel.Add(hit.collider.gameObject.GetComponent<BaseEnemy>());
                    }
                }

                objectsAtCoords.Add(new Vector2(RayStartX, RayStartZ), objsHitList);
            }
        }
        LevelManager._instance.levelScannerHasLoaded = true;
    }

    private void debug()
    {
        //DEBUG
        string debugOutput = "";
        List<GameObject> debugList = new List<GameObject>();
        if (objectsAtCoords.TryGetValue(new Vector2(x, z), out debugList))
        {

            debugOutput += "Hit Count: " + debugList.Count + "\n";
            foreach (GameObject obj in debugList)
            {

            }
            Debug.Log(debugOutput);
        }
        else
        {
            Debug.Log("No Key Found");
        }
    }
}
