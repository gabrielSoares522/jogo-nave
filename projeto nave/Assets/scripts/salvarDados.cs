using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class salvarDados{
    public static void executarSalvar(dadosJogo dados)
    {
        BinaryFormatter formato = new BinaryFormatter();
        string local = Application.persistentDataPath+"/dadosPrjNave.fun";
        FileStream stream = new FileStream(local, FileMode.Create);

        formato.Serialize(stream, dados);
        stream.Close();
    }

    public static dadosJogo executarCarregar() {
        string local = Application.persistentDataPath + "/dadosPrjNave.fun";
        if (File.Exists(local)){
            BinaryFormatter formato = new BinaryFormatter();
            FileStream stream = new FileStream(local, FileMode.Open);
            dadosJogo dados = formato.Deserialize(stream) as dadosJogo;
            stream.Close();
            return dados;
        }
        else {

            bool[] naveDefault = new bool[4];

            naveDefault[0] = true;
            naveDefault[1] = false;
            naveDefault[2] = false;
            naveDefault[3] = false;

            bool[] BuffsDefault = new bool[3];
            BuffsDefault[0] = true;
            BuffsDefault[1] = true;
            BuffsDefault[2] = true;


            return new dadosJogo(0f, 0, naveDefault, BuffsDefault,false,0f,0f,0f);
        }
    }
}
