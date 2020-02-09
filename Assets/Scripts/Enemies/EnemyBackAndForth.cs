using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackAndForth : BaseEnemy
{
    public CardinalDirection startingDirection = CardinalDirection.EAST;

    GameObject bombermanObj;

    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        switch (startingDirection)
        {
            case CardinalDirection.NORTH:
                forwardDirection = new Vector3(0, 0, 1);
                break;
            case CardinalDirection.EAST:
                forwardDirection = new Vector3(1, 0, 0);
                break;
            case CardinalDirection.SOUTH:
                forwardDirection = new Vector3(0, 0, -1);
                break;
            case CardinalDirection.WEST:
                forwardDirection = new Vector3(-1, 0, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            gameObject.transform.forward = forwardDirection;
            GetComponent<Rigidbody>().velocity = forwardDirection * speed;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            if(bombermanObj != null)
            {
                gameObject.transform.LookAt(bombermanObj.transform);
            }
        }
        
    }

    void FixedUpdate()
    {
        if (triggerReverseDirection == true)
        {
            triggerReverseDirection = false;
            reverseDirection();
        }
    }

    void reverseDirection()
    {
        forwardDirection = -forwardDirection;

        /*
        if (forwardDirection.x == 1)
        {
            forwardDirection.x = -1;
            transform.forward = forwardDirection;
        }
        else
        {
            forwardDirection.x = 1;
            transform.forward = forwardDirection;
        }
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BombermanController>() != null)
        {
            isAttacking = true;
            collision.gameObject.GetComponent<BombermanController>().kill();
            bombermanObj = collision.gameObject;
            _animator.SetTrigger("Attack");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<StaticBlock>() != null
            || other.gameObject.GetComponent<BreakableBlock>() != null
            || other.gameObject.GetComponent<BaseEnemy>() != null
            || other.gameObject.GetComponent<Bomb>() != null)
        {
            triggerReverseDirection = true;
        }
    }
}
