using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Classe GuiFITEscolhaDeJogadores, derivada de GuiPadrao2.
/// <para>Responsável por mostrar dados referentes à escolha de jogadores para o cenário.</para>
/// </summary>
public class GuiFITEscolhaDeNiveis : GuiPadrao2
{
    public int position = 0;

    private Vector2 scrollViewVector = Vector2.zero;
    public int largura_da_janela = 400;
    public int altura_da_janela = 200;
    private SortedList lista_de_niveis;
    public bool[] selecoes_de_niveis;

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

            position = 0;

            for (int i = 0; i < 4; i++)
            {
                selecoes_de_niveis[i] = GUI.Toggle(new Rect(0, position, 100, 30), selecoes_de_niveis[i], "Nivel " + i);
                position += 20;
            }

            position = 0;

            for (int i = 4; i < 7; i++)
            {
                selecoes_de_niveis[i] = GUI.Toggle(new Rect(100, position, 100, 30), selecoes_de_niveis[i], "Nivel " + i);
                position += 20;
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
}
