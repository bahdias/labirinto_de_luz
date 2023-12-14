using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float vidaInicial = 1f; // Valor inicial da vida
    public float vidaAtual = 1f;
    public float fragmento = 0;
    public int memorias = 0;

    public bool tomandoDano = true; 
    public Slider barraDeVida;
    //public GameObject textluzes;

    public AudioClip somLuz;
    public AudioClip somLuzVida;
    private AudioSource meuAudioSource;

    public Slider barraFragmentos;
    public GameObject pergaminho;
    public GameObject textMemorias; //texto no canvas
    public List<GameObject> paginas;

    public GameObject esferaAmarela;
    public AudioClip somEsferaAmarela;
    public GameObject paredeSecreta;






    void Start()
    {
        vidaAtual = vidaInicial; // Inicializa a vida atual com o valor inicial
        AtualizarBarraDeVida(); // Chama a função para atualizar a barra de vida
        meuAudioSource = GetComponent<AudioSource>();
        AtivaPagina(0);
        textMemorias.GetComponent<TextMeshProUGUI>().text = (memorias) + "/5";
    }

    void Update()
    {
        AtualizarBarraDeVida();
        AtualizarBarraDeFragmento();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1; //despausando o jogo
            pergaminho.active = false; //torna o pergaminho invisivel
        }
    }

    void AtivaPagina(int pagina)
    {
        foreach(GameObject pag in paginas)
        {
            pag.active = false;
        }
        Debug.Log("Pagina: " + pagina);
        paginas[pagina].active = true;
    }
    void AtualizarBarraDeFragmento()
    {
        barraFragmentos.value = fragmento;
    }
    void AtualizarBarraDeVida()
    {
        if (barraDeVida != null)
        {
            barraDeVida.value = vidaAtual; // Atualiza o valor da barra de vida
        }
        else
        {
            Debug.LogWarning("A barra de vida não foi atribuída ao script."); // Adiciona um aviso se a barra de vida não estiver atribuída
        }

        if(vidaAtual <= 0){
            Debug.Log("Perdeu todas as vidas");
            SceneManager.LoadScene("GameOver");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Lava" && tomandoDano)
        {
            tomandoDano = false;
            StartCoroutine(dano_de_lava());
        }
        else if(other.tag == "Inimigo")
        {
            vidaAtual -= 1f;
            Debug.Log("Perdeu todas as vidas");
            SceneManager.LoadScene("GameOver");
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Luz"){
            fragmento += 0.2f;
            if (fragmento >= 1)
            {
                if (memorias < 5)
                {
                    AtivaPagina(memorias);
                    memorias += 1;
                    textMemorias.GetComponent<TextMeshProUGUI>().text = memorias + "/5";
                    if(memorias <5)
                        fragmento = 0f;
                    pergaminho.active = true;
                    Time.timeScale = 0; //pausando o jogo
                                        //desbloqueia um fragmento de memória
                }

                if (memorias == 5)
                {
                    Debug.Log("Coletou todos os pergaminhos");
                    esferaAmarela.active = true;
                    //continua aqui se ja tiver coletado as 5 memórias
                }

            }
            Destroy(other.gameObject);
            meuAudioSource.PlayOneShot(somLuz);

        }
        if(other.tag == "Luz_de_vida"){
            Destroy(other.gameObject);
            meuAudioSource.PlayOneShot(somLuzVida);
            if(vidaAtual < 1f)
                vidaAtual += 0.2f;
            AtualizarBarraDeVida();
        }

        if (other.tag == "Esfera_amarela")
        {
            //coleta
            Destroy(other.gameObject);
            meuAudioSource.PlayOneShot(somEsferaAmarela);
            paredeSecreta.GetComponent<AudioSource>().Play();
            paredeSecreta.GetComponent<Animator>().Play("abrindo");
        }

    }

    IEnumerator dano_de_lava(){
        vidaAtual -= 0.2f;
        AtualizarBarraDeVida();
        Debug.Log("Perdeu 10 de vida");
        yield return new WaitForSeconds(1f);
        tomandoDano = true;

    }
}
