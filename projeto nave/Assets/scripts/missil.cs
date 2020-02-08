using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missil : MonoBehaviour {
    public float velocidade;
    public float rotacao;
    Transform alvo;
    public GameObject explosao;

	void Start () {
        alvo = GameObject.Find("nave").transform;
        float variacao = Random.value;
        //if (variacao > 0.5f)
        //{
        //    velocidade = velocidade - 0.5f + variacao;
        //    rotacao = rotacao - 1f + variacao;
        //}
        //else
        //{

        //    velocidade = velocidade - 0.5f + variacao;
        //    rotacao = rotacao + variacao;
        //}
        //transform.rotation = Quaternion.LookRotation(alvo.position - transform.position, -transform.forward);
        transform.up = new Vector2(alvo.position.x - transform.position.x, alvo.position.y - transform.position.y);


    }

    void Update() {
        //float veloAtual = transform.GetComponent<Rigidbody2D>().velocity.magnitude;
        //if (veloAtual < 15) transform.GetComponent<Rigidbody2D>().velocity +=  rect*velocidade * Time.deltaTime;

        transform.Translate(0, velocidade * Time.deltaTime, 0);

        Vector2 direcaoAlvo = new Vector2(alvo.position.x - transform.position.x, alvo.position.y - transform.position.y);

        transform.up = Vector2.Lerp(transform.up,direcaoAlvo, rotacao * Time.deltaTime);

        //Quaternion novaRotacao = Quaternion.LookRotation(alvo.position - transform.position, -transform.forward);
        
        //novaRotacao.x = 0;
        //novaRotacao.y = 0;
        //transform.rotation = Quaternion.Slerp(transform.rotation, novaRotacao, rotacao * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(Instantiate(explosao,transform.position,transform.rotation),1);
        Destroy(gameObject);
    }
}
