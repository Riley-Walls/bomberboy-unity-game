using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IExplosionBehavior {

    public SphereCollider mainCollider;
    //USED TO DETECT INITIAL COLLISIONS
    public SphereCollider triggerCollider;

    public GameObject explosionBlockPrefab;
    public int explosionSize = 3;
    public float explosionDelay = 3f;

    private bool hasExploded = false;

    private float timeSinceSpawn = 0f;

    private bool collisionEnabled = false;

	// Use this for initialization
	void Start () {
        //STARTS SET TO FALSE TO ALLOW CHARACTERS TO INITIALLY OVERLAP
        mainCollider.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
        timeSinceSpawn += Time.deltaTime;
        //ENABLES COLLIDER NOW THAT TRIGGERS HAVE HAD A CHANCE TO SET COLLISION EXCEPTIONS
        mainCollider.enabled = true;

        if(timeSinceSpawn > explosionDelay && hasExploded == false)
        {
            explode();
        }
    }

    private void explode()
    {
        SoundMgr._instance.PlaySfx(SoundMgr._instance.bombExplosionSfx);

        if(hasExploded == false)
        {
            hasExploded = true;
            //DRAW CENTER BLOCK
            Instantiate(explosionBlockPrefab, gameObject.transform.position, Quaternion.identity);

            //BLASTS!
            blast(new Vector3(0, 0, 1));
            blast(new Vector3(1, 0, 0));
            blast(new Vector3(0, 0, -1));
            blast(new Vector3(-1, 0, 0));
        }
        GameObject.Destroy(this.gameObject);
    }

    private void blast(Vector3 dir)
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position,dir * 50, Mathf.Abs(50));

        //FILTER OUT ONLY OBJECTS WITHIN EXPLOSION RANGE, THEN ADD TO LIST
        List<RaycastHit> objsHitList = new List<RaycastHit>();
        foreach (RaycastHit hit in hits)
        {
            if(Mathf.Floor(hit.distance) < explosionSize)
            {
                objsHitList.Add(hit);
            }
        }

        //SORT BY DISTANCE, THEN ADD TO A QUEUE, CLOSEST FIRST
        Queue<RaycastHit> hitsByDistance = new Queue<RaycastHit>();
        while (objsHitList.Count > 0)
        {
            RaycastHit closestHit = new RaycastHit();
            closestHit.distance = float.MaxValue;
            foreach(RaycastHit hit in objsHitList)
            {
                if(Mathf.Floor(hit.distance) < Mathf.Floor(closestHit.distance))
                {
                    closestHit = hit;
                }
            }
            hitsByDistance.Enqueue(closestHit);
            objsHitList.Remove(closestHit);
        }

        int finalExplosionSize = explosionSize;

        //CHECK THE EXPLOSION BEHAVIOR OF EACH OBJECT, STARTING WITH THE CLOSEST
        while(hitsByDistance.Count > 0)
        {
            RaycastHit hit = hitsByDistance.Dequeue();
            if (hit.collider.gameObject.GetComponent<IExplosionBehavior>() != null)
            {
                IExplosionBehavior explosionBehavior = hit.collider.gameObject.GetComponent<IExplosionBehavior>();

                //IF THE OBJECT IS A TYPE THAT STOPS EXPLOSIONS, CAP THE EXPLOSION LENGTH AND BREAK THE LOOP
                if (explosionBehavior.checkIfExplosionPassesThrough() == false)
                {
                    finalExplosionSize = Convert.ToInt32(Mathf.Floor(hit.distance));
                    //STILL CALL EXPLODE() ON OBJECTS THAT STOP EXPLOSIONS, IN CASE THEY HAVE A REACTION
                    //EX. BREAKABLE BLOCK BLOCKS AN EXPLOSION BUT STILL NEEDS TO EXPLODE
                    explosionBehavior.Explode();
                    break;
                }
                else
                {
                    explosionBehavior.Explode();
                }
            }
        }

        //CREATE EXPOSION BLOCKS
        for (int i = 0; i < finalExplosionSize; i++)
        {
            Instantiate(explosionBlockPrefab, gameObject.transform.position + (dir * (i + 1)), Quaternion.identity);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //SETS ANY INITIAL COLLISIONS TO BE IGNORED
        if(other.gameObject.GetComponent<BombermanController>() != null && timeSinceSpawn < .1f)
        {
            Physics.IgnoreCollision(other.GetComponent<Collider>(), mainCollider, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //RE-ENABLES MAIN COLLIDER ONCE OBJECTS HAVE EXITED
        if (other.gameObject.GetComponent<BombermanController>() != null)
        {
            Physics.IgnoreCollision(other.GetComponent<Collider>(), mainCollider, false);
        }
    }

    public bool checkIfExplosionPassesThrough()
    {
        return false;
    }

    public void Explode()
    {
        if(hasExploded == false)
        {
            explode();
        }
    }
}
