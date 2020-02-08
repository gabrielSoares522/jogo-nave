using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class dadosJogo
{
    public float dinheiro;
    public int naveEscolhida;
    public bool[] navesCompradas;
    public bool[] buffsCompradas;
    public bool mutado;
    public float recordeH;
    public float recordeM;
    public float recordeS;


    public dadosJogo(float di,int ne,bool[] cn,bool[] cb,bool mu,float rh, float rm, float rs) {
        dinheiro = di;
        naveEscolhida = ne;
        navesCompradas = cn;
        buffsCompradas = cb;
        mutado = mu;
        recordeH = rh;
        recordeM = rm;
        recordeS = rs;
}
}
