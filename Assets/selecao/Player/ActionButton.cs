using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour {

    private LevelManager manager;

    public GameObject macaco;
    public GameObject standDialog;

    private float micoSpawnTime = 1.0f;
    private int micoRotationPerSec = 2;
    private float micoMessageTime = 1.0f;

    private float animaFrac;

    private bool micoReady = false;
    private bool animaMico = false;

    public void Start()
    {
        manager = transform.GetComponent<LevelManager>();
    }

	public void cancel(){
		Debug.Log ("Cancel");
        retomarMovimentoPersonagem();
    }
	public void ok(){
		Debug.Log ("Ok.");
        standDialog.SetActive(false);
        macaco.SetActive(true);
        GameObject c = macaco.transform.Find("Canvas").gameObject;
        c.transform.Find("MicoMsg").gameObject.SetActive(false);
        c.transform.Find("continua1").gameObject.SetActive(false);
        c.transform.Find("continua2").gameObject.SetActive(false);
        StartCoroutine(rouboMico());
    }

    IEnumerator rouboMico()
    {
        animaFrac = Time.time;
        animaMico = true;
        yield return new WaitForSeconds(micoSpawnTime);
        animaMico = false;
        StartCoroutine(fimRouboMico());
    }

    IEnumerator fimRouboMico()
    {
        GameObject c = macaco.transform.Find("Canvas").gameObject;
        c.transform.Find("MicoMsg").gameObject.SetActive(true);
        yield return new WaitForSeconds(micoMessageTime);
        c.transform.Find("continua1").gameObject.SetActive(true);
        c.transform.Find("continua2").gameObject.SetActive(true);
        micoReady = true;
    }

    private void retomarMovimentoPersonagem ()
    {
        GameObject jogador = GameObject.Find("personagemJogador");
        jogador.GetComponent<Animator>().enabled = true;  // Retoma a animação
        jogador.GetComponent<PlayerMovement>().speed = 4.0f;  // Permite a movimentação do personagem
        GameObject StartScene = transform.parent.GetComponent<StandsControl>().nextScene;
        StartScene.SetActive(false);
    }

    public void Update()
    {
        Transform m = macaco.transform.Find("mico");

        if (animaMico)
        {
            float t = Time.time;
            float scale = (t - animaFrac) / micoSpawnTime;
            scale = Mathf.Min(1f, scale);
            m.localScale = new Vector3(scale, scale, 1f);
            m.localRotation = Quaternion.Euler(0f, 0f, scale * micoSpawnTime * micoRotationPerSec * 360);
        } else
        {
            m.localScale = new Vector3(1f, 1f, 1f);
            m.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (micoReady && Input.GetMouseButtonUp(0))
        {
            manager.loadNextLevel();
        }
    } 
}
