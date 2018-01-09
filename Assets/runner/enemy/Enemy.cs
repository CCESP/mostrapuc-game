using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private float minSpawnInterval = 0.5f;
    private float maxSpawnInterval = 2.5f;

    private float movePosX = -1.81f;
    private float playerDistanceX = 10.25f;

    private float projectileSpawnXDiff = -1.0f;

    private Vector3 projectileOverheadPosition = new Vector3(-250, 7.5f, 0f);
    private Vector3 projectileGroundPosition = new Vector3(-175, 0, 0);

    private float moveTime = 0.6667f;
    private float unspawnTime = 0.3333f;

    private bool throwing = false;
    private bool spawned = false;
    private bool unspawned = false;

    private Vector3 targetPosition;

    private GameSceneController gsc;

    private Transform macaco;

    private Vector3 velocity;

    public GameObject projectilePrefab;

    // Use this for initialization
    void Start () {
        gsc = this.transform.parent.gameObject.GetComponent<GameSceneController>();
        macaco = transform.Find("macaco");
        throwing = false;
        spawned = false;
        unspawned = false;
    }

    public void Spawn()
    {
        throwing = false;
        spawned = false;
        unspawned = false;
        StartCoroutine(InternalSpawn());
    }

    IEnumerator InternalSpawn()
    {
        float spawnTime = minSpawnInterval + Random.value * maxSpawnInterval;
        yield return new WaitForSeconds(spawnTime);
        if (gsc.IsRunning()) {
            targetPosition = new Vector3(movePosX, 0, 0);
            velocity = Vector3.zero;
            spawned = true;
        }
    }

    void ThrowCurriculo() {
        throwing = true;
        GameObject curriculoProjectile = Instantiate(projectilePrefab);
        curriculoProjectile.transform.position = this.transform.position;
        curriculoProjectile.transform.position += new Vector3(projectileSpawnXDiff, 0, 0);
        curriculoProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector3(-250, 7.5f, 0));
        
        StartCoroutine(InternalUnspawn());

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("genObstacle");
        for(int i = 0; i < obstacles.Length; i++)
        {
            Collider2D collider = obstacles[i].transform.GetChild(0).GetComponent<Collider2D>();
            print(collider);
            Physics2D.IgnoreCollision(curriculoProjectile.GetComponent<Collider2D>(), collider);
        }
    }

    IEnumerator InternalUnspawn()
    {
        yield return new WaitForSeconds(unspawnTime);
        targetPosition = new Vector3(0, 0, 0);
        unspawned = true;
    }

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(
            gsc.transform.Find("Player").transform.position.x + playerDistanceX,
            transform.position.y,
            transform.position.z);

        if (gsc.IsRunning() && spawned) {
            macaco.localPosition = Vector3.SmoothDamp(macaco.localPosition, targetPosition, ref velocity, moveTime);
            
            if(Mathf.Abs(velocity.x) < 1E-2) {
                if (!throwing) {
                    ThrowCurriculo();
                } else if(unspawned) {
                    Spawn();
                }
            }
        }
    }
}
