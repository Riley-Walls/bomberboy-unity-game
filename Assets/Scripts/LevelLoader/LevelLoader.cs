using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public string levelFileName;
    System.IO.StreamReader file;

    public GameObject floorTilePrefab;
    public CharLevelObjMapping[] charLevelObjMappings;

    // Start is called before the first frame update
    void Start()
    {
        IDictionary<string, GameObject> charLevelObjMappingsDict = new Dictionary<string, GameObject>();
        foreach(CharLevelObjMapping charObj in charLevelObjMappings)
        {
            charLevelObjMappingsDict.Add(charObj.character, charObj.obj);
        }

        file = new System.IO.StreamReader(Application.dataPath + "/LevelLayouts/" + levelFileName + ".csv");
        string line;
        int z = 0;
        while ((line = file.ReadLine()) != null)
        {
            z -= 1;

            string[] splitLine = line.Split(',');

            for(int i = 0; i < splitLine.Length; i++)
            {
                GameObject prefab;
                if(charLevelObjMappingsDict.TryGetValue(splitLine[i], out prefab))
                {
                    Instantiate(prefab, new Vector3(i, 0, z), Quaternion.identity);
                    Instantiate(floorTilePrefab, new Vector3(i, 0, z), Quaternion.identity);
                }
            }
        }
        file.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public struct CharLevelObjMapping
{
    public string character;
    public GameObject obj;
}
