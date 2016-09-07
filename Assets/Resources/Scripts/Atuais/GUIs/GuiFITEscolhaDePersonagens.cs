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

    private Vector2 scrollViewVector = Vector2.zero;

    public int posicao_em_x;

    public GuiFITEscolhaDePersonagens(int pos_x = 0)
    {
        posicao_em_x = pos_x;
    }

    public override void OnGUI()
    {

        // Begin the ScrollView
        scrollViewVector = GUI.BeginScrollView(new Rect(posicao_em_x, 25, 420, 200), scrollViewVector, new Rect(0, 0, 400, 200));

        GUI.Label(new Rect(0, 0, 200, 600), "", "textarea");

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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
