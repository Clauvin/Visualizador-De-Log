using UnityEngine;
using Basicas;
using System.Collections;

/// <summary>
/// Responsável por passar valores do pre-load para o load.
/// <para>Isso é necessário porquê o pre-load e o load são ambos scenes diferentes, e antes de uma scene ser carregada,
/// tudo da scene anterior é apagado... a menos que você queira que algo não seja.</para>
/// <para>E eu prefiro passar o mínimo de dados possíveis de uma scene para a outra, por isso essa classe específica,
/// e não uma já existente.</para>
/// </summary>
public class PassadorDeDados : MonoBehaviour {

    public int tempo_minimo;
    public int tempo_maximo;
    public string endereco_do_arquivo;
    public BancoDeDadosFIT bd_fit;

    public void SetValuesDePassagem(int tempo_min, int tempo_max, string endereco, BancoDeDadosFIT bd = null)
    {
        tempo_minimo = tempo_min;
        tempo_maximo = tempo_max;
        endereco_do_arquivo = endereco;
        bd_fit = bd;
    }

    public void Destruir()
    {
        Destroy(transform.gameObject);
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
