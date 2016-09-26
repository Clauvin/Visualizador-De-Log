using UnityEngine;
using System.Collections;

/// <summary>
/// Classe GuiFITEscolhaDeJogadores, derivada de GuiPadrao2.
/// <para>Responsável por mostrar dados referentes à escolha de jogadores para o cenário.</para>
/// </summary>
public class GuiFITEscolhaDeJogadores : GuiPadrao2
{

    public int largura_da_janela = 200;
    public int altura_da_janela = 300;
    private int distancia_entre_jogadores = 30;
    private SortedList lista_de_jogadores;
    public bool[] selecoes_de_jogadores;

    private Vector2 scrollViewVector = Vector2.zero;

    public void InitGuiFITEscolhaDeJogadores()
    {
        selecoes_de_jogadores = new bool[lista_de_jogadores.Count];
    }

    public override void OnGUI() {

        if (revelado)
        {
            GUI.Label(new Rect(posx, posy - 20, largura_da_janela, 20), "Jogadores", "textarea");

            // Begin the ScrollView
            scrollViewVector = GUI.BeginScrollView(new Rect(posx, posy, 200, 300), scrollViewVector,
                                                   new Rect(0, 0, largura_da_janela - 20, altura_da_janela * 2));

            GUI.Label(new Rect(0, 0, 200, 600), "", "textarea");

            for (int i = 0; i < lista_de_jogadores.Count; i++)
            {
                selecoes_de_jogadores[i] = GUI.Toggle(new Rect(0, distancia_entre_jogadores * i, 100, 20),
                    selecoes_de_jogadores[i], lista_de_jogadores.GetByIndex(i).ToString());
            }

            // End the ScrollView
            GUI.EndScrollView();
        }
        

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetListaDeJogadores(SortedList lista)
    {
        lista_de_jogadores = lista;
    }

    public SortedList GetListaDeJogadores()
    {
        return lista_de_jogadores;
    }
}
