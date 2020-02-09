using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IExplosionBehavior
{
    public BoxCollider frontCollider;
    public Animator _animator;

    protected Vector3 forwardDirection = new Vector3(1, 0, 0);
    protected float speed = 2f;
    protected bool triggerReverseDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<BombermanController>() != null)
        {
            collision.gameObject.GetComponent<BombermanController>().kill();
            
        }
    }


    public void kill()
    {
        LevelManager._instance.enemiesInLevel.Remove(this);
        LevelManager._instance.score++;
        LevelManager._instance.scoreChange.Invoke();
        Destroy(this.gameObject);
    }

    public bool checkIfExplosionPassesThrough()
    {
        return true;
    }

    public void Explode()
    {
        kill();
    }
}
