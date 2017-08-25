using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int jumpForce;
	public int moveVelocity;

	private const int MAX_JUMPS = 1;
	private int jumpCounter = 0;

    private Animator anim;
    private Animation jumpingAnimation;

	void Start () {
		jumpCounter = MAX_JUMPS;
        anim = GetComponent<Animator>();
        jumpingAnimation = GetComponent<Animation>();
	}
	
	void Update ()
	{
		if (Input.GetKeyDown("space") && jumpCounter > 0 ) {
			//removendo todas as forças pra fazer "pulo limpo"
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpForce));
			this.jumpCounter--;
            anim.Play("PlayerJumping");
		}

		/* Acelerando player pra direita */
		this.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveVelocity , this.GetComponent<Rigidbody2D>().velocity.y);

		/* Travando angulo para  objeto nao rotacionar */
		transform.eulerAngles = new Vector3(0,0,0);
	}

	private void GameOver ()
	{
		//GameObject.Destroy (this.gameObject);
        GetComponent<LevelManager>().loadNextLevel();
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
        Collider2D c = coll.collider;

		//Debug.Log (c.tag);
		if (c.tag == "Obstacle") {
			GameOver ();
		} else if (c.tag == "Walkable Obstacle") {
            Vector3 contact = coll.contacts[0].point;
            Vector3 center = c.bounds.center;

            bool topCollision = Mathf.Abs(Mathf.Abs(contact.y) - Mathf.Abs(center.y)) > 0.5f;

			if (!topCollision) {
				GameOver ();
			} else {
				this.jumpCounter = MAX_JUMPS;
                anim.Play("PlayerRunning");
            }
		} else if (c.tag == "Plataform") {
			this.jumpCounter = MAX_JUMPS;
            anim.Play("PlayerRunning");
        }

	}

} /* fim classe */
