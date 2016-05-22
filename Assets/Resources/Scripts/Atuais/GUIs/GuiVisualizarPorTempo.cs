using UnityEngine;
using System.Collections;
using System;
using System.Globalization;

/// <summary>
/// Classe GuiVisualizarPorTempo. Responsável por permitir ao usuário sumir e aparecer com objetos baseando-se no
/// tempo em que aparecem.
/// <para>Atenção: essa classe tem um tratamento grande de exceções, o que no futuro precisa ser feito em outras
/// classes, conforme o projeto vai sendo usado por mais e mais usuários.</para>
/// <para>Atenção: essa classe também contém dois easter eggs no tratamento de erros, caso o usuário
/// resolva que vai quebrar o limite mínimo que um int pode conter.</para>
/// </summary>
public class GuiVisualizarPorTempo : GuiPadrao2 {

    LidaComErrosTempoMinimoEMaximo lida_com_erros;

    string tempo_minimo = "Apenas >= 0 aqui.";
    string tempo_maximo = "Apenas >= 0 aqui.";
    //... e apenas <= tempo final, mas não cabe na GUI e na prática não atrapalha.

    int visivel_ou_invisivel = 1;
    public string[] o_que_escrever_nos_botoes;

    // bool responsável por fazer o botão dessa GUI alternar entre visível ou invisível
    // Futuramente, pode se tornar desnecessário caso tenhamos dois botões: um pra deixar visível
    // e outro pra deixar invisível.
    void E_Pra_Deixar_Visivel_Ou_Invisivel()
    {
        if (visivel_ou_invisivel == 1) visivel_ou_invisivel = 0;
        else visivel_ou_invisivel = 1;
    }

    public override void OnGUI()
    {
        lida_com_erros.posicao_da_mensagem_de_erro_y = 100;
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 290, 180));
            GUI.TextField(new Rect(0, 0, 290, 20), "Invisibilidade de Objetos em Espaço de Tempo", "textfield");
            GUI.Label(new Rect(0, 20, 210, 20), "Tempo Mínimo", "textfield");
            if (visivel_ou_invisivel == 1) tempo_minimo = GUI.TextField(new Rect(0, 40, 210, 20), tempo_minimo);
            else GUI.Label(new Rect(0, 40, 210, 20), tempo_minimo, "textfield");
            GUI.Label(new Rect(0, 60, 210, 20), "Tempo Máximo", "textfield");
            if (visivel_ou_invisivel == 1) tempo_maximo = GUI.TextField(new Rect(0, 80, 210, 20), tempo_maximo);
            else GUI.Label(new Rect(0, 80, 210, 20), tempo_maximo, "textfield");
            if (GUI.Button(new Rect(210, 20, 80, 80), o_que_escrever_nos_botoes[visivel_ou_invisivel]))
            {
                lida_com_erros.DetectarETratarErrosEExcecoesDeInput(tempo_minimo, tempo_maximo);

                
                if (lida_com_erros.NaoTemosErrosDeInput())
                {
                    if (visivel_ou_invisivel == 1)
                    {
                        GetComponent<Controlador>().DeixarObjetosEmEspacoDeTempoInvisiveisEIninteragiveis(
                            Int32.Parse(tempo_minimo), Int32.Parse(tempo_maximo));
                        E_Pra_Deixar_Visivel_Ou_Invisivel();
                    }
                    else
                    {
                        GetComponent<Controlador>().DeixarObjetosEmEspacoDeTempoVisiveisEInteragiveis(
                            Int32.Parse(tempo_minimo), Int32.Parse(tempo_maximo));
                        E_Pra_Deixar_Visivel_Ou_Invisivel();
                    }
                }
            }

            lida_com_erros.PossiveisMensagensDeErro();

            GUI.EndGroup();
        }
    }

    // Use this for initialization
    void Start () {

        revelado = true;

        posx = Screen.width - 290;
        posy = 400;

        o_que_escrever_nos_botoes = new string[2];
        o_que_escrever_nos_botoes[0] = "Apertar\npara\nvisível";
        o_que_escrever_nos_botoes[1] = "Apertar\npara\ninvisível";

        lida_com_erros = new LidaComErrosTempoMinimoEMaximo();
        lida_com_erros.ConfigurarVariaveisParaVisualizacaoDeObjetos();

    }
	
	// Update is called once per frame
	void Update () {
        posx = Screen.width - 290;
    }
}
