using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Classe GuiFITEscolhaDePersonagem, derivada de GuiPadrao2.
/// <para>Responsável por mostrar dados referentes à escolha de jogadores para o cenário.</para>
/// </summary>
public class GuiFITEscolhaDePersonagens : GuiPadrao2
{

    public bool[] togglePersonagens = new bool[4];
    public int position = 0;
    public int largura_da_janela = 420;
    public int altura_da_janela = 200;

    private Vector2 scrollViewVector = Vector2.zero;

    public override void OnGUI()
    {
        if (revelado)
        {

            
            // Begin the ScrollView
            scrollViewVector = GUI.BeginScrollView(new Rect(posx, posy, largura_da_janela, altura_da_janela),
                                               scrollViewVector, new Rect(0, 0, largura_da_janela - 20, altura_da_janela));

            GUI.Label(new Rect(0, 0, (largura_da_janela - 20) / 2, altura_da_janela * 3), "", "textarea");

            position = 0;

            for (int i = 0; i < 2; i++)
            {
                togglePersonagens[i] = GUI.Toggle(new Rect(0, position, 100, 30), togglePersonagens[i], "Personagem " + i);
                position += 20;
            }

            position = 0;

            for (int i = 2; i < 4; i++)
            {
                togglePersonagens[i] = GUI.Toggle(new Rect(100, position, 100, 30), togglePersonagens[i], "Personagem " + i);
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
