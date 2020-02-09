using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullPowerUp : PowerUp
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
            //other.gameObject.GetComponent<BombermanController>().bombDelay = Random.Range(0, 75);

            SoundMgr._instance.PlaySfx(SoundMgr._instance.sickSfx);

            other.gameObject.GetComponent<BombermanController>().sick = true;

            other.gameObject.GetComponent<BombermanController>().speed -= 1f;
            Destroy(this.gameObject);
        }
    }

}
