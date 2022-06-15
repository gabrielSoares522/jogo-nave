using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class adm : MonoBehaviour {
    #region variaveis
    #region publico
    [Header ("Loja")]
    [SerializeField] public NaveLoja[] lojaNaves;
    [SerializeField] public BuffLoja[] lojaBuffs;
    [SerializeField] public int naveEscolhida;
    [SerializeField] public int indexNave;
    [SerializeField] public Image mostraNave;
    [SerializeField] public Text mostraNome;
    [SerializeField] public Text txtSelecionar;
    [SerializeField] public Text txtDinheiro;
    [SerializeField] public float dinheiro;

    [Header ("Spawn")]
    [SerializeField] public Transform[] posMissil;
    [SerializeField] public Transform[] posBuff;
    [SerializeField] public Transform campoAsteroide;
    [SerializeField] public GameObject buff;
    [SerializeField] public GameObject missil;
    [SerializeField] public GameObject asteroide;
    [SerializeField] public GameObject setaMissil;
    [SerializeField] public GameObject setaBuff;

    [Header ("telas")]
    [SerializeField] public Text lblTempo;
    [SerializeField] public Text lblMTempo;
    [SerializeField] public Text lblTimer;
    [SerializeField] public Text lblGanho;
    [SerializeField] public Transform[] telas;
    [SerializeField] public bool fim;
    [SerializeField] public Transform tl_Atual = null;
    [SerializeField] public Transform tl_Anterior = null;
    #endregion

    #region privado
    private float tempoMissil = 3f;
    private float cronometroMissil;
    private float tempoBuff = 12f;
    private float cronometroBuff;
    private Transform nave;
    private float segundosCount;
    private int minutosCount;
    private int horaCount;

    private bool mutado;
    //Color corNave;
    //Color corInimigos;
    #endregion

    [SerializeField] bool _testMode = true;
    private const string _gameId = "2983441";
    #endregion

    void Awake(){
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void InitializeAds()
    {
        Advertisement.Initialize(_gameId);
    }

    public void PlayAd(){
        if (Advertisement.IsReady("endGame"))
        {
            Advertisement.Show("endGame");
        }
    }
 
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        lblTempo.text = "Fail";
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    void Start() {
        
        InitializeAds();
        Contador.zerar();
        nave = GameObject.Find("nave").transform;
        cronometroBuff = Time.time;
        cronometroMissil = Time.time;
        mostrar(telas[0]);
        Time.timeScale = 0;

        carregarProgresso();
        #region spawn asteroide
        for (int i = 0; i < 10; i++) {
            float posx = 0;
            float posy = 0;
            do {
                posx = Random.Range(-30, 30);
                posy = Random.Range(-30, 30);
            } while ((posx > -10 && posx < 10) && (posy > -10 && posy < 10));

            Instantiate(asteroide, new Vector3(posx, posy, 0), transform.rotation,campoAsteroide);
        }
        #endregion
    }

    void Update() {
        #region spawn
        #region spawn missil
        if (Time.time > cronometroMissil)
        {
            cronometroMissil += tempoMissil;
            Vector3 posAux = new Vector3(0, 0, 0);

            posAux = posMissil[Random.Range(0, posMissil.Length)].position;

            GameObject missilAux = Instantiate(missil, posAux, transform.rotation);
            GameObject setaAux = Instantiate(setaMissil, nave);
            setaAux.GetComponent<seta>().alvo = missilAux.transform;
        }
        #endregion

        #region spawn buff
        if (Time.time > cronometroBuff)
        {
            cronometroBuff += tempoBuff;
            Vector3 posAux = new Vector3(0, 0, 0);
            float sorteio = Random.value * 10;

            posAux = posBuff[Random.Range(0, posBuff.Length)].position;

            int selecionado = 0;
            sorteio = Random.value * 10;
            if (sorteio < 4) selecionado = 0;
            else
            {
                if (sorteio < 6) selecionado = 1;
                else selecionado = 2;
            }
            GameObject buffAux = Instantiate(buff, posAux, transform.rotation);
            buff.GetComponent<buff>().codBuff = selecionado;
            GameObject setaAux = Instantiate(setaBuff, nave);
            setaAux.GetComponent<seta>().alvo = buffAux.transform;
        }
        #endregion
        #endregion
        
        lblTimer.text = Contador.atualizar();

        #region fim jogo
        if (fim) {
            if (Input.GetKeyDown("r")) {
                reiniciar();
            }
        }
        #endregion
    }

    #region funcoes botoes
    public void iniciar() {
        Time.timeScale = 1;
        mostrar(telas[1]);
    }

    public void finalizar() {
        int ganho = (int)((Contador.cronometro.horas*60+Contador.cronometro.minutos)*60+Contador.cronometro.segundos);
        dinheiro += ganho;
        txtDinheiro.text = "dinheiro: " + dinheiro.ToString();
        lblGanho.text = "ganho: +" + ganho.ToString();
        lblTempo.text = "Tempo: " + Contador.tempo();
        lblMTempo.text = "Melhor Tempo: "+Contador.melhorTempo();
        mostrar(telas[2]);
        fim = true;
        Time.timeScale = 0;
        PlayAd();
    }

    public void reiniciar() {
        salvarProgresso();
        SceneManager.LoadScene(0);
    }

    public void abrirLoja() {
        mostrar(telas[3]);
    }

    public void abrirOpcoes() {
        mostrar(telas[4]);
    }

    public void voltarTela() {
        mostrar(tl_Anterior);
    }
    #endregion

    #region funcoes tela
    public void esconder(Transform tela) {
        tela.gameObject.SetActive(false);
    }

    public void mostrar(Transform tela) {
        for (int i = 0; i < telas.Length; i++)
        {
            esconder(telas[i]);
        }
        tela.gameObject.SetActive(true);
        tl_Anterior = tl_Atual;
        tl_Atual = tela;
    }
    #endregion

    #region loja
    public void btnSeleDir() {
        if (indexNave == lojaNaves.Length-1) {
            indexNave = 0;
        }
        else {
            indexNave++;
        }
        mostrarNave(indexNave);
    }

    public void btnSeleEsq() {
        if (indexNave == 0) {
            indexNave = lojaNaves.Length-1;
        }
        else {
            indexNave--;
        }
        
        mostrarNave(indexNave);
    }

    public void mostrarNave(int index) {
        mostraNave.sprite = lojaNaves[index].textura;
        mostraNome.text = lojaNaves[index].nome;
        if (lojaNaves[index].comprado) {
            if(index == naveEscolhida) { txtSelecionar.text = "Selecionado"; }
            else { txtSelecionar.text = "Selecionar"; }
        }
        else {
            txtSelecionar.text = "Comprar " + lojaNaves[index].preco.ToString();
        }
    }
    public void btnSelecionar() {
        if (lojaNaves[indexNave].comprado) {
            nave.GetComponent<nave>().corpo.GetComponent<SpriteRenderer>().sprite = lojaNaves[indexNave].textura;
            naveEscolhida = indexNave;
        }
        else {
            if (dinheiro >= lojaNaves[indexNave].preco) {
                nave.GetComponent<nave>().corpo.GetComponent<SpriteRenderer>().sprite = lojaNaves[indexNave].textura;
                txtSelecionar.text = "Selecionado";
                naveEscolhida = indexNave;
                lojaNaves[indexNave].comprado = true;
                dinheiro -= lojaNaves[indexNave].preco;
                txtDinheiro.text = "Dinheiro: " + dinheiro.ToString();
            }
        }
    }
    #endregion

    #region funcoes progresso
    public void salvarProgresso() {
        bool[] navesCompradas = new bool[lojaNaves.Length];
        bool[] buffsComprados = new bool[lojaBuffs.Length];

        for (int i = 0; i < lojaNaves.Length; i++){
            navesCompradas[i] = lojaNaves[i].comprado;
        }

        for (int i = 0; i < lojaBuffs.Length; i++)  {
            buffsComprados[i]=lojaBuffs[i].comprado;
        }
        dadosJogo progresso = new dadosJogo(dinheiro,naveEscolhida,navesCompradas,buffsComprados,mutado, Contador.melhor.horas, Contador.melhor.minutos, Contador.melhor.segundos);

        salvarDados.executarSalvar(progresso);
    }

    public void carregarProgresso() {
        dadosJogo progresso = salvarDados.executarCarregar();
        //navesCompradas = new bool[lojaNaves.Length];
        //buffsComprados = new bool[progresso.buffsCompradas.Length];
        //navesCompradas = progresso.navesCompradas;

        dinheiro = progresso.dinheiro;
        naveEscolhida = progresso.naveEscolhida;
        mutado = progresso.mutado;
        //corNave = progresso.corNave;
        //corInimigos = progresso.corInimigos;

        Contador.melhor.horas = progresso.recordeH;
        Contador.melhor.minutos = progresso.recordeM;
        Contador.melhor.segundos = progresso.recordeS;
        
        for (int i = 0; i < progresso.navesCompradas.Length; i++) {
            lojaNaves[i].comprado = progresso.navesCompradas[i];
        }

        for (int i = 0; i < progresso.buffsCompradas.Length; i++) {
            lojaBuffs[i].comprado = progresso.buffsCompradas[i];
        }

        txtDinheiro.text = "Dinheiro: "+dinheiro.ToString();
        //nave.GetComponent<nave>().corpo.GetComponent<SpriteRenderer>().color = corNave;
        indexNave = naveEscolhida;
        mostrarNave(naveEscolhida);
        btnSelecionar();
    }
    #endregion
}