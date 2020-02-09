using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanController : MonoBehaviour, IExplosionBehavior {

    public Animator animator;

    public GameObject model;
    public float speed = 10f;
    public GameObject bombPrefab;
    public int bombExplosionSize = 3;

    private Vector3 velocity = new Vector3();
    private List<Bomb> activeBombs = new List<Bomb>();
    private bool isMoving = false;
    public int bombDelay = 75;
    private int bombDelayTimer;
    public bool sick;

    private bool setToBeKilled = false;
    private int setToBeKilledTimer = 0;

    // Use this for initialization
    void Start ()
    {
        bombDelayTimer = bombDelay;

        sick = false;
    }

	
	// Update is called once per frame
	void Update ()
    {
        //SET TO BE KILLED
        if (setToBeKilled)
        {
            setToBeKilledTimer++;

            if(setToBeKilledTimer > 20)
            {
                animator.SetTrigger("death");
            }
            if(setToBeKilledTimer > 150)
            {
                Destroy(this.gameObject);
                if (LevelManager._instance != null)
                {
					//LevelManager._instance.score = 0;
					LevelManager._instance.scoreChange.Invoke();
                    LevelManager._instance.adjustLives(-1);
                    LevelManager._instance.reloadCurrentLevel();
                }
            }
        }


        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        bombDelayTimer++;

        velocity = new Vector3(0, 0, 0);

        if (!setToBeKilled)
        {
            //SET DIRECTION
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                velocity.z = speed;
                transform.forward = new Vector3(0, 0, 1);
                animator.SetBool("run", true);
                isMoving = true;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                velocity.z = -speed;
                transform.forward = new Vector3(0, 0, -1);
                animator.SetBool("run", true);
                isMoving = true;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                velocity.x = -speed;
                transform.forward = new Vector3(-1, 0, 0);
                animator.SetBool("run", true);
                isMoving = true;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                velocity.x = speed;
                transform.forward = new Vector3(1, 0, 0);
                animator.SetBool("run", true);
                isMoving = true;
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                velocity = new Vector3(0, 0, 0);
                animator.SetBool("run", false);
                isMoving = false;
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            velocity = new Vector3(0, 0, 0);
            isMoving = false;
        }


        //DROP BOMB
        if ((Input.GetKeyDown(KeyCode.Space) && bombDelayTimer > bombDelay) || (sick && bombDelayTimer > bombDelay))
        {
            bombDelayTimer = 0;
            Vector3 bombSpawnPosition = new Vector3();
            bombSpawnPosition.x = Mathf.Round(transform.position.x);
            bombSpawnPosition.y = 0;
            bombSpawnPosition.z = Mathf.Round(transform.position.z);

            Bomb newBomb = Instantiate(bombPrefab, bombSpawnPosition, transform.rotation).GetComponent<Bomb>();
            newBomb.explosionSize = bombExplosionSize;
            activeBombs.Add(newBomb);

            if(sick)
                bombDelay = Random.Range(0, 75);


        }


    }

    public void kill()
    {
        SoundMgr._instance.PlaySfx(SoundMgr._instance.bombermanDie);
        setToBeKilled = true;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            GetComponent<Rigidbody>().velocity = velocity;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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
