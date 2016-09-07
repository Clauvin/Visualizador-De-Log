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

    public override void OnGUI()
    {

        // Begin the ScrollView
        scrollViewVector = GUI.BeginScrollView(new Rect(posx, posy, 400, 200), scrollViewVector, new Rect(0, 0, 380, 200));

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
