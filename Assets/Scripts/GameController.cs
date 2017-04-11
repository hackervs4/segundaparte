	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    
    public Estado estado { get; private set; }

    public GameObject menu;
    public GameObject canvas;
    public GameObject obstaculo;
    public float espera;
    public float tempoDestruiocao;
    public static GameController instancia = null;

    private void Awake() {
        if (instancia == null) {
            instancia = this;
        }
        else if (instancia != null) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        estado = Estado.AguardoComecar;
      }

    IEnumerator GerarObstaculos() {
        while (GameController.instancia.estado == Estado.Jogando) {
            Vector3 pos = new Vector3(10f, Random.Range(8f, 0f), -1f);
				GameObject obj = Instantiate(obstaculo, pos, Quaternion.Euler(90f,0f,0f)) as GameObject;
				Destroy(obj, tempoDestruiocao);
				yield return new WaitForSeconds(espera);
        }
    }

	public void PlayerComecou() {
        estado = Estado.Jogando;
        menu.SetActive(false);
        canvas.SetActive(false);
        StartCoroutine(GerarObstaculos());
    }

    public void PlayerMorreu() {
        estado = Estado.GameOver;
    }
}
