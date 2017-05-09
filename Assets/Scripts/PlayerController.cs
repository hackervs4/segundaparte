using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float ForcaDoPulo = 10f;
    public AudioClip somPulo;
    public AudioClip somMorte;
    private Animator anim;
    private Rigidbody rb;
    private AudioSource audioSource;
    private bool pulando = false;
	private Vector3 posicaoInicial;
	private Quaternion rotacaoInicial;

    void Start() {
		posicaoInicial = transform.localPosition;
		rotacaoInicial = transform.localRotation;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update() {
        if (GameController.instancia.estado == Estado.Jogando) {
            if (Input.GetMouseButtonDown(0)) {
                anim.Play("Pulando");
                audioSource.PlayOneShot(somPulo);
                rb.useGravity = true;
                pulando = true;
           
            }
        }
    }

	public void recomecar() {
		rb.useGravity = false;
		rb.velocity = Vector3.zero;
		rb.detectCollisions = true;
		transform.localPosition = posicaoInicial;
		transform.localRotation = rotacaoInicial;
	} 

	void FixedUpdate() {
        if (GameController.instancia.estado == Estado.Jogando) {
            if (pulando) {
                pulando = false;
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * ForcaDoPulo, ForceMode.Impulse);
            }
        }
    }
    void OnCollisionEnter(Collision outro) {
        if (GameController.instancia.estado == Estado.Jogando) {
            if (outro.gameObject.tag == "obstaculo" || outro.gameObject.tag == "pranchas") {
                rb.AddForce(new Vector3(-2f, 1f, 0f), ForceMode.Impulse);
                rb.detectCollisions = false;
                anim.Play("Morrendo");
				anim.Play("Olhos");		
                audioSource.PlayOneShot(somMorte);
                GameController.instancia.PlayerMorreu();
            }
        }
    }



}
