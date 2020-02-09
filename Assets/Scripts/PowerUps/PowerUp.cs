using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour, IExplosionBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkIfExplosionPassesThrough()
    {
        return true;
    }

    public void Explode()
    {
        Destroy(this.gameObject);
    }

}
