using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Classe GuiFITEscolhaDeJogadores, derivada de GuiPadrao2.
/// <para>Responsável por mostrar dados referentes à escolha de jogadores para o cenário.</para>
/// </summary>
public class GuiFITEscolhaDeNiveis : GuiPadrao2
{

    public bool[] toggleNiveis = new bool[8];
    public int position = 0;

    private Vector2 scrollViewVector = Vector2.zero;
    private int largura_da_janela = 400;
    private int altura_da_janela = 200;

    public override void OnGUI()
    {

        // Begin the ScrollView
        scrollViewVector = GUI.BeginScrollView(new Rect(posx, posy, largura_da_janela, altura_da_janela),
                                               scrollViewVector,
                                               new Rect(0, 0, largura_da_janela - 20, altura_da_janela));

        GUI.Label(new Rect(0, 0, 200, 600), "", "textarea");

        position = 0;

        for (int i = 0; i < 4; i++)
        {
            toggleNiveis[i] = GUI.Toggle(new Rect(0, position, 100, 30), toggleNiveis[i], "Nivel " + i);
            position += 20;
        }

        position = 0;

        for (int i = 4; i < 7; i++)
        {
            toggleNiveis[i] = GUI.Toggle(new Rect(100, position, 100, 30), toggleNiveis[i], "Nivel " + i);
            position += 20;
        }

        // End the ScrollView
        GUI.EndScrollView();

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
