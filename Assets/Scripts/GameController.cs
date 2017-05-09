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
	public Text txtMaiorPontuacao;
    public float espera;
    public float tempoDestruiocao;
    public static GameController instancia = null;
	private int pontos;
	public GameObject gameOverPanel;
	public GameObject pontosPanel;
	private List<GameObject> obstaculos;

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
		obstaculos = new List<GameObject>();
		estado = Estado.AguardoComecar;
		PlayerPrefs.SetInt("HighScore", 0);
		menuCamera.SetActive (true);
		menuPanel.SetActive (true);
		gameOverPanel.SetActive (false);
		pontosPanel.SetActive (false);
      }
	IEnumerator DestruirObstaculo(GameObject obj) {
		yield return new WaitForSeconds(15);
		if (obstaculos.Remove(obj)) {
			Destroy(obj);
		}
	}

    IEnumerator GerarObstaculos() {
        while (GameController.instancia.estado == Estado.Jogando) {
            Vector3 pos = new Vector3(10f, Random.Range(1f, 4f), 0f);
				GameObject obj = Instantiate(obstaculo, pos, Quaternion.Euler(90f,0f,0f)) as GameObject;
			obstaculos.Add(obj);
			StartCoroutine(DestruirObstaculo(obj));
				yield return new WaitForSeconds(espera);
        }
    }

	private void atualizarPontos(int x) {
		pontos = x;
		txtPontos.text = "" + x;
	}

	public void PlayerVoltou() {
		while (obstaculos.Count > 0) {
			GameObject obj = obstaculos[0];
			if (obstaculos.Remove(obj)) {
				Destroy(obj);
			}
		}
		estado = Estado.AguardoComecar;
		menuCamera.SetActive(true);
		menuPanel.SetActive(true);
		gameOverPanel.SetActive(false);
		pontosPanel.SetActive(false);
		GameObject.Find("Coelho").GetComponent<PlayerController>().recomecar();
	}

	public void PlayerComecou() {
		estado = Estado.Jogando;
		menuCamera.SetActive(false);
		menuPanel.SetActive(false);
		pontosPanel.SetActive (true);
		atualizarPontos(0);
		StartCoroutine(GerarObstaculos());
	}

	public void incrementarPontos(int x) {
		atualizarPontos(pontos + x);
	}

    public void PlayerMorreu() {
        estado = Estado.GameOver;
		if (pontos > PlayerPrefs.GetInt ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", pontos);
			txtMaiorPontuacao.text = "" + pontos;
		}
		gameOverPanel.SetActive (true);
    }

}
