using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUpPowerUp : PowerUp, IExplosionBehavior
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
            SoundMgr._instance.PlaySfx(SoundMgr._instance.bombUpSfx);
            other.gameObject.GetComponent<BombermanController>().bombDelay -= 15;
            Destroy(this.gameObject);
        }
    }
}
