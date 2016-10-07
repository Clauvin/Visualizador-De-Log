using UnityEngine;
using System.Collections;

/// <summary>
/// Classe GuiFITEscolhaDePersonagem, derivada de GuiPadrao2.
/// <para>Responsável por mostrar dados referentes à escolha de jogadores para o cenário.</para>
/// </summary>
public class GuiFITEscolhaDePersonagens : GuiPadrao2
{

    public int largura_da_janela = 420;
    public int altura_da_janela = 200;
    private SortedList lista_de_personagens;
    public bool[] selecoes_de_personagens;
    public int position_x = 0;
    public int position_y = 0;
    public int largura_entre_personagens = 100;
    public int altura_entre_personagens = 20;

    private Vector2 scrollViewVector = Vector2.zero;

    public void InitGuiFITEscolhaDePersonagens()
    {
        selecoes_de_personagens = new bool[lista_de_personagens.Count];
    }

    public override void OnGUI()
    {
        if (revelado)
        {
            GUI.Label(new Rect(posx, posy - 20, (largura_da_janela - 20)/2, 20), "Personagens", "textarea");

            // Begin the ScrollView
            scrollViewVector = GUI.BeginScrollView(new Rect(posx, posy, largura_da_janela, altura_da_janela),
                                               scrollViewVector, new Rect(0, 0, largura_da_janela - 20, altura_da_janela));

            GUI.Label(new Rect(0, 0, (largura_da_janela - 20) / 2, altura_da_janela * 3), "", "textarea");

            position_y = 0;

            

            for (int i = 0; i < selecoes_de_personagens.Length; i++)
            {
                selecoes_de_personagens[i] = GUI.Toggle(new Rect(position_x, position_y, 100, 20),
                    selecoes_de_personagens[i], "Personagem " + i);
                position_x += largura_entre_personagens;
                if (position_x >= 2 * largura_entre_personagens)
                {
                    position_x = 0;
                    position_y += altura_entre_personagens;
                }
            }

            position_y = 0;

            GUI.EndScrollView();

        }

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetListaDePersonagens(SortedList lista)
    {
        lista_de_personagens = lista;
    }

    public SortedList GetListaDePersonagens()
    {
        return lista_de_personagens;
    }

    public bool[] GetSelecoesDePersonagens()
    {
        return selecoes_de_personagens;
    }
}
