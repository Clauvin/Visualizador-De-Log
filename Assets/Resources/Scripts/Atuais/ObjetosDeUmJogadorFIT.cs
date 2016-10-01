using UnityEngine;
using System.Collections;
using Basicas;

/// <summary>
/// Classe responsável por guardar todos os objetos relacionados às ações de um jogador
/// num log, incluindo o banco de dados com as informações referentes ao log em questão.
/// </summary>
public class ObjetosDeUmJogadorFIT : MonoBehaviour {

    public ArrayList lista_de_backgrounds;
    public ArrayList lista_de_objetos;
    public ArrayList matrizes_dos_heatmaps;
    public GameObject ancora_dos_dados;
    public GameObject heatmap;
    public BancoDeDadosFIT bd_fit;

    public ObjetosDeUmJogadorFIT()
    {
        lista_de_backgrounds = new ArrayList();
        lista_de_objetos = new ArrayList();
        matrizes_dos_heatmaps = new ArrayList();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
