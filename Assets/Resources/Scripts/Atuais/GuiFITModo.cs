﻿using UnityEngine;
using System.Collections;

public class GuiFITModo : GuiPadrao2
{

    bool gambiarra;

    protected string texto;
    protected string instrucoes;
    private int posicaox;

    public GuiFITModo()
    {
        gambiarra = true;
        posx = Screen.width - 300;
        posy = 0;
        texto = "";
        revelado = false;
    }

    public void PegarQualModo(string modo)
    {

        texto = modo;

    }

    public void PegarInstrucoes(string instrucao)
    {

        instrucoes = instrucao;

    }

    public override void OnGUI()
    {
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 300, 420));

            posicaox = 0;
            GUI.TextField(new Rect(10, posicaox, 300, 20), "Modo: " + texto);
            posicaox += 20;
            GUI.TextField(new Rect(10, posicaox, 300, 400), instrucoes);

            GUI.EndGroup();
        }
    }

    public override bool MudarTexto(string novotexto) {

        texto = novotexto;
        return true;

    }

    public bool MudarInstrucoes(string instrucao)
    {
        instrucoes = instrucao;
        return true;
    }

    public override string GetTexto() { return texto; }

    void Awake()
    {

    }

    // Use this for initialization
    public void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gambiarra)
        {
            gambiarra = false;
        }
        posx = Screen.width - 300;
    }
}
