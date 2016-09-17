using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Classe GuiFITEscolhaDeJogadores, derivada de GuiPadrao2.
/// <para>Responsável por mostrar dados referentes à escolha de jogadores para o cenário.</para>
/// </summary>
public class GuiFITEscolhaDeNiveis : GuiPadrao2
{
    
    private Vector2 scrollViewVector = Vector2.zero;
    public int largura_da_janela = 400;
    public int altura_da_janela = 200;
    private SortedList lista_de_niveis;
    public bool[] selecoes_de_niveis;
    public int position_x = 0;
    public int position_y = 0;
    public int largura_entre_niveis = 20;
    public int altura_entre_niveis = 20;

    public void InitGuiFITEscolhaDeNiveis()
    {
        selecoes_de_niveis = new bool[lista_de_niveis.Count];
    }

    public override void OnGUI()
    {
        if (revelado)
        {

            GUI.Label(new Rect(posx, posy - 20, largura_da_janela / 2, 20), "Niveis", "textarea");

            // Begin the ScrollView
            scrollViewVector = GUI.BeginScrollView(new Rect(posx, posy, largura_da_janela, altura_da_janela),
                                                   scrollViewVector,
                                                   new Rect(0, 0, largura_da_janela - 20, altura_da_janela));

            GUI.Label(new Rect(0, 0, 200, 600), "", "textarea");

            position_x = 0;
            position_y = 0;

            for (int i = 0; i < lista_de_niveis.Count; i++)
            {
                selecoes_de_niveis[i] = GUI.Toggle(new Rect(position_x, position_y, 100, 20), selecoes_de_niveis[i], "Nivel " + i);
                position_x += largura_entre_niveis;
                if (position_y >= 2 * largura_entre_niveis)
                {
                    position_x = 0;
                    position_y += altura_entre_niveis;
                }
            }

            // End the ScrollView
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

    public void SetListaDeNiveis(SortedList lista)
    {
        lista_de_niveis = lista;
    }
}
