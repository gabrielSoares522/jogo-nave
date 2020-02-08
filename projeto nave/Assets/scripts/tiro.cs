using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiro : MonoBehaviour
{
    public float velocidade = 20f;
    void Start()
    {
        Destroy(gameObject, 5f);
    }
    
    void Update()
    {
        transform.Translate(0, velocidade * Time.deltaTime,0);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
