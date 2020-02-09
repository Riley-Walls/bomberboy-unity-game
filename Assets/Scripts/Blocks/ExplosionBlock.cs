using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlock : MonoBehaviour
{
    public float timeToLive = .3f;
    private float timeElapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > timeToLive)
        {
            Destroy(this.gameObject);
        }
    }
/*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IExplodable>() != null){
            IExplodable explodable = other.gameObject.GetComponent<IExplodable>();
            explodable.Explode();
        }
    }
*/
}
