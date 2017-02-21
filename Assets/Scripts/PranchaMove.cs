using UnityEngine;

public class PranchaMove : MonoBehaviour {

    public float velocidade;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        Vector3 velocidadevetorial = Vector3.left * velocidade;

        transform.position = transform.position + velocidadevetorial * Time.deltaTime;

       
        }
 
}
