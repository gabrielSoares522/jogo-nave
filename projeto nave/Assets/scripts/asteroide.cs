using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroide : MonoBehaviour
{
    float rotacao =0;
    float velocidade = 0;
    Vector2 direcao = new Vector2();

    void Start()
    {
        rotacao = Random.Range(0, 3);
        velocidade = Random.Range(0, 3);
        direcao = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));

        transform.localScale = new Vector3(Random.Range(0.5f,1), Random.Range(0.5f, 1), 1);
    }

    void Update()
    {
        transform.Rotate(0, 0, rotacao);
        transform.Translate(direcao * velocidade * Time.deltaTime,Space.World);
    }
}
