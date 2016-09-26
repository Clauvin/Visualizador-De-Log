﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Classe responsável pelos dois botões na tela de visualização dos elementos do log
/// para voltar para o Pre-Load e voltar para a tela inicial.
/// <para>ATENÇÃO: o botão dessa classe para voltar para o Pre-Load só funciona em suas classes filhas, uma para cada
/// tipo de log.</para>
/// </summary>
public class GuiSairDaVisualizacao : GuiPadrao2 {

    private int largura_dos_botoes;
    private int altura_dos_botoes;
    private int resultado;
    private int qual_botao = -1;

    protected string[] strings_da_toolbar = { "Tela De Pre-Load", "Tela Inicial" };

    public virtual void RetornarParaTelaDePreLoad() { }

    public override void OnGUI()
    {
        MudarCoordenadas(Screen.width - largura_dos_botoes, Screen.height - altura_dos_botoes);

        GUI.BeginGroup(new Rect(posx, posy, largura_dos_botoes, altura_dos_botoes));
        resultado = GUI.Toolbar(new Rect(0, 0, largura_dos_botoes, altura_dos_botoes), qual_botao,
            strings_da_toolbar);
        switch (resultado)
        {
            case 0:
                RetornarParaTelaDePreLoad();
                break;

            case 1:

                MudarCenaPara_Tela_Inicial();
                break;
        }

        GUI.EndGroup();

    }

    // Use this for initialization
    void Start () {
        largura_dos_botoes = 240;
        altura_dos_botoes = 22;
	
	}
}
