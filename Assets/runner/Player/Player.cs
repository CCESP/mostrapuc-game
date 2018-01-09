using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int jumpForce;
	public int moveVelocity;

	private const int MAX_JUMPS = 1;
	private int jumpCounter = 0;

	private int SCORE_PER_PROJECTILE = 100;

    private Animator anim;
    private Animation jumpingAnimation;

    private GameSceneController gsc;

    public void EnablePlayer ()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        this.GetComponent<Animator>().enabled = true;
    }

    public void DisablePlayer()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.GetComponent<Animator>().enabled = false;
    }

	void Start () {
        gsc = this.transform.parent.gameObject.GetComponent<GameSceneController>();
        DisablePlayer();
        jumpCounter = MAX_JUMPS;
        anim = GetComponent<Animator>();
        jumpingAnimation = GetComponent<Animation>();
	}
	
	void Update ()
	{
        if(gsc.IsRunning()) { 
		    if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")) && jumpCounter > 0 ) {
			    //removendo todas as forças pra fazer "pulo limpo"
			    this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			    this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpForce));
			    this.jumpCounter--;
                anim.Play("PlayerJumping", -1, 0f);
		    }

		    /* Acelerando player pra direita */
		    this.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveVelocity, this.GetComponent<Rigidbody2D>().velocity.y);

		    /* Travando angulo para  objeto nao rotacionar */
		    transform.eulerAngles = new Vector3(0,0,0);
        }
    }

	private void GameOver (bool win)
	{
        DisablePlayer();
        gsc.GameOver(win);
    }

	void OnTriggerEnter2D (Collider2D c) {
		if (c.tag == "Curriculo") {
			gsc.AddGoalScore(SCORE_PER_PROJECTILE);
			Destroy(c.gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
        Collider2D c = coll.collider;

		//Debug.Log (c.tag);
		if (c.tag == "Obstacle") {
			GameOver (false);
		} else if (c.tag == "Walkable Obstacle") {
			Vector3 contact = coll.contacts [0].point;
			Vector3 center = c.bounds.center;

			bool topCollision = Mathf.Abs (Mathf.Abs (contact.y) - Mathf.Abs (center.y)) > 0.5f;

			if (!topCollision) {
				GameOver (false);
			} else {
				this.jumpCounter = MAX_JUMPS;
				anim.Play ("PlayerRunning");
			}

		} else if (c.tag == "Plataform") {
			this.jumpCounter = MAX_JUMPS;
			anim.Play ("PlayerRunning");
		}

	}

} /* fim classe */
