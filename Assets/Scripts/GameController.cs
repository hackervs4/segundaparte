	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    public Estado estado { get; private set; }

    public GameObject menuCamera;
	public GameObject menuPanel;
    public GameObject obstaculo;
	public Text txtPontos;
    public float espera;
    public float tempoDestruiocao;
    public static GameController instancia = null;
	private int pontos;


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
            Vector3 pos = new Vector3(10f, Random.Range(8f, 0f), 0f);
				GameObject obj = Instantiate(obstaculo, pos, Quaternion.Euler(90f,0f,0f)) as GameObject;
				Destroy(obj, tempoDestruiocao);
				yield return new WaitForSeconds(espera);
        }
    }

	private void atualizarPontos(int x) {
		pontos = x;
		txtPontos.text = "" + x;
	}


	public void PlayerComecou() {
		estado = Estado.Jogando;
		menuCamera.SetActive(false);
		menuPanel.SetActive(false);
		atualizarPontos(0);
		StartCoroutine(GerarObstaculos());
	}

	public void incrementarPontos(int x) {
		atualizarPontos(pontos + x);
	}

    public void PlayerMorreu() {
        estado = Estado.GameOver;
    }

}
