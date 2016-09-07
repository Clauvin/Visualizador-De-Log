﻿using UnityEngine;
using System.Collections;

public class GuiFITEscolhas : GuiPadrao2 {

    GuiFITEscolhaDeJogadores gui_escolhas_de_jogadores;
    GuiFITEscolhaDeNiveis gui_escolhas_de_niveis;
    GuiFITEscolhaDePersonagens gui_escolhas_de_personagens;

    int meio_da_tela = Screen.width / 2;

    public GuiFITEscolhas()
    {
        gui_escolhas_de_jogadores = new GuiFITEscolhaDeJogadores();
        gui_escolhas_de_niveis = new GuiFITEscolhaDeNiveis();
        gui_escolhas_de_personagens = new GuiFITEscolhaDePersonagens();
    }

    public override void OnGUI()
    {

        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), string.Empty);

        gui_escolhas_de_jogadores.OnGUI();
        gui_escolhas_de_niveis.OnGUI();
        gui_escolhas_de_personagens.OnGUI();

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        meio_da_tela = Screen.width / 2;

    }
}