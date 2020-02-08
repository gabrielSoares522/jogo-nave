using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seta : MonoBehaviour {

    public Transform alvo;
    public Transform filho;
    Quaternion novaRotacao;
    float distancia;

	void Start () {
		
	}
	
	void Update () {
        try
        {
            distancia =Vector3.Distance(transform.position, alvo.position);
            novaRotacao = Quaternion.LookRotation(alvo.position - transform.position, -transform.forward);
        }
        catch
        {
            Destroy(gameObject);
            return;
        }

        novaRotacao.x = 0;
        novaRotacao.y = 0;
        transform.rotation = novaRotacao;

        if (distancia < 10) {
            filho.gameObject.SetActive(false);
        }
        else {
            filho.gameObject.SetActive(true);
        }
	}
}
