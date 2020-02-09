﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkatePowerUp : PowerUp, IExplosionBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BombermanController>() != null)
        {
            SoundMgr._instance.PlaySfx(SoundMgr._instance.skateSfx);
            other.gameObject.GetComponent<BombermanController>().speed += 1f;
            Destroy(this.gameObject);
        }
    }

}
