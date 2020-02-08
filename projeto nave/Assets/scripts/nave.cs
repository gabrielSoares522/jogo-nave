using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class nave : MonoBehaviour{

    #region variaveis
    public FixedJoystick joystick;
    public Transform controlador;

    public GameObject escudo;
    public GameObject tiro;

    public Transform corpo;
    public Transform posTiro;

    public float veloRotacao;
    public float veloPadrao;
    public float veloBuff;

    float veloAtual;
    bool temEscudo;
    bool temBuff=false;
    int cdBuff;
    bool disparando=false;
    float tempoDisparo = 0f;
    float cronoDisparo = 0f;
    #endregion

    void Start() {
        veloAtual = veloPadrao;
    }

    void Update() {
        #region movimentacao
        //float veloAtual = transform.GetComponent<Rigidbody2D>().velocity.magnitude;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            //if (veloAtual < 15) transform.GetComponent<Rigidbody2D>().velocity += joystick.Direction * aceleracao*Time.deltaTime;

            //if(Mathf.Abs(joystick.Horizontal) > 0.5 || Mathf.Abs(joystick.Vertical) > 0.5)
            //{
            //    corpo.up = Vector2.Lerp(joystick.Direction, (Vector2)transform.up, veloRotacao * Time.deltaTime);
            //}
            Quaternion corpoRot = Quaternion.LookRotation(new Vector3(joystick.Direction.x, joystick.Direction.y, 0), -corpo.forward);
            corpoRot.x = 0;
            corpoRot.y = 0;
            corpo.rotation = Quaternion.Lerp(corpo.rotation, corpoRot, veloRotacao * Time.deltaTime);
        }
        transform.Translate(corpo.up * veloAtual * Time.deltaTime);
        #endregion

        #region para desktop
        if (Input.GetKeyDown("q")) {
            ativarBuff();
        }
        if (Input.GetKeyDown("w")) {
            comecarDisparos();
        }
        if (Input.GetKeyUp("w")) {
            finalizarDisparos();
        }
        if (disparando) {
            disparo(0.25f);
        }
        #endregion
    }

    #region colisao 2D
    void OnCollisionEnter2D(Collision2D col)
    {
        #region atingido
        if (col.gameObject.tag == "missil" || col.gameObject.tag =="asteroide")
        {
            //if (temEscudo) {

            //    temEscudo = false;
            //}
            //else
            if (!temEscudo)
            {
                controlador.GetComponent<adm>().finalizar();
                temBuff = false;
            }
        }
        #endregion

        #region pegar buffs
        if (col.gameObject.tag == "buff")
        {
            temBuff = true;
            cdBuff = col.transform.GetComponent<buff>().codBuff;
        }
        #endregion
    }
    #endregion

    #region buffs
    public void ativarBuff()
    {
        if(!temBuff)
        {
            return;
        }

        switch (cdBuff)
        {
            case (0):
                StartCoroutine(buffEscudo(5f));
                break;
            case (1):
                buffExplosao();
                break;
            case (2):
                StartCoroutine(buffVelocidade(2f));
                break;
            case (3):
                StartCoroutine(teletransporte(4f));
                break;
        }

        temBuff = false;
    }
    private IEnumerator teletransporte(float duracao)
    {
        Color original = corpo.GetComponent<SpriteRenderer>().color;
        corpo.GetComponent<PolygonCollider2D>().isTrigger = true;
        corpo.GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(duracao);
        corpo.GetComponent<PolygonCollider2D>().isTrigger = false;
        corpo.GetComponent<SpriteRenderer>().color = original;
    }
    private IEnumerator buffVelocidade(float duracao)
    {
        veloAtual = veloBuff;
        yield return new WaitForSeconds(duracao);
        veloAtual = veloPadrao;
    }

    private IEnumerator buffEscudo(float duracao)
    {
        GameObject escudoAux = Instantiate(escudo, transform);
        temEscudo = true;
        yield return new WaitForSeconds(duracao);
        temEscudo = false;
        try { Destroy(escudoAux); }
        catch { }
    }

    private void buffExplosao()
    {
        GameObject[] misseisAux = GameObject.FindGameObjectsWithTag("missil");
        foreach (GameObject missil in misseisAux)
        {
            Destroy(missil);
        }
    }
    #endregion

    #region disparo
    public void comecarDisparos() {
        disparando = true;
    }
    public void finalizarDisparos() {
        disparando = false;
    }

    public void disparo(float coldown) {
        cronoDisparo += Time.deltaTime;
        if (tempoDisparo<cronoDisparo) {
            GameObject tiroA = Instantiate(tiro, posTiro.position, posTiro.rotation);
            tiroA.transform.GetComponent<tiro>().velocidade = 10f + veloAtual;
            tempoDisparo = cronoDisparo + coldown;
        }
    }
    #endregion
}
