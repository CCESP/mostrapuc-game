using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int jumpForce;
	public int moveVelocity;

	private const int MAX_JUMPS = 2;
	private int jumpCounter = 0;


	void Start () {
		jumpCounter = MAX_JUMPS;
	}
	
	void Update ()
	{
		if (Input.GetKeyDown("space") && jumpCounter > 0 ) {
			//removendo todas as forças pra fazer "pulo limpo"
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			this.GetComponent<Rigidbody2D>().AddForce( new Vector2(0,jumpForce));
			this.jumpCounter--;
		}

		/* Acelerando player pra direita */
		this.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveVelocity , this.GetComponent<Rigidbody2D>().velocity.y);

		/* Travando angulo para  objeto nao rotacionar */
		transform.eulerAngles = new Vector3(0,0,0);
	
	}

	private void GameOver ()
	{
		GameObject.Destroy (this.gameObject);

	}


	void OnTriggerEnter2D (Collider2D c)
	{
		//Debug.Log (c.tag);
		if (c.tag == "Obstacle") {
			GameOver ();
		} else if (c.tag == "Walkable Obstacle") {
			if (this.transform.position.y < (c.transform.position.y + this.GetComponent<BoxCollider2D> ().size.y / 2 + c.GetComponent<BoxCollider2D> ().size.y / 2)) {
				GameOver ();
			} else {
				this.jumpCounter = MAX_JUMPS;

			}
		} else if (c.tag == "Plataform") {
			this.jumpCounter = MAX_JUMPS;
		}

	}

} /* fim classe */
