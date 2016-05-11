using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Classe GuiVisualizarPorTempo. Responsável por permitir ao usuário sumir e aparecer com objetos baseando-se no
/// tempo em que aparecem.
/// <para>Atenção: essa classe tem um tratamento grande de exceções, o que no futuro precisa ser feito em outras
/// classes, conforme o projeto vai sendo usado por mais e mais usuários.</para>
/// </summary>
public class GuiVisualizarPorTempo : GuiPadrao2 {

    string tempominimo = "Apenas >= 0 aqui.";
    //... e apenas <= tempo final, mas não cabe na GUI e na prática não atrapalha.
    string tempomaximo = "Apenas >= 0 aqui.";
    int visivel_ou_invisivel = 1;
    public string[] o_que_escrever_nos_botoes;
    bool erro_de_input_a = false;
    bool erro_de_input_b = false;
    bool erro_de_maior_que = false;
    int posicao_erro_y = 100;

    void MudaVisibilidade()
    {
        if (visivel_ou_invisivel == 1) visivel_ou_invisivel = 0;
        else visivel_ou_invisivel = 1;
    }

    public override void OnGUI()
    {
        posicao_erro_y = 100;
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 290, 180));
            GUI.TextField(new Rect(0, 0, 290, 20), "Invisibilidade de Espaço de Tempo");
            GUI.Label(new Rect(0, 20, 210, 20), "Tempo Mínimo", "textfield");
            tempominimo = GUI.TextField(new Rect(0, 40, 210, 20), tempominimo);
            GUI.Label(new Rect(0, 60, 210, 20), "Tempo Máximo", "textfield");
            tempomaximo = GUI.TextField(new Rect(0, 80, 210, 20), tempomaximo);
            if (GUI.Button(new Rect(210, 20, 80, 80), o_que_escrever_nos_botoes[visivel_ou_invisivel]))
            {
                try { Int32.Parse(tempominimo); erro_de_input_a = false; } catch (FormatException fe) { erro_de_input_a = true; }
                try { Int32.Parse(tempomaximo); erro_de_input_b = false; } catch (FormatException fe) { erro_de_input_b = true; }

                if (!(erro_de_input_a || erro_de_input_b))
                {
                    if (Int32.Parse(tempominimo) > Int32.Parse(tempomaximo))
                    {
                        erro_de_maior_que = true;
                    } else { erro_de_maior_que = false; }
                }

                //Deus abençoe que C# me permite fazer essas comparações em sequência.
                if (!(erro_de_input_a || erro_de_input_b || erro_de_maior_que))
                {
                    if (visivel_ou_invisivel == 1)
                    {
                        GetComponent<Controlador>().DeixarObjetosEmEspacoDeTempoInvisiveisEIninteragiveis(
                            Int32.Parse(tempominimo), Int32.Parse(tempomaximo));
                        MudaVisibilidade();
                    }
                    else
                    {
                        GetComponent<Controlador>().DeixarObjetosEmEspacoDeTempoVisiveisEInteragiveis(
                            Int32.Parse(tempominimo), Int32.Parse(tempomaximo));
                        MudaVisibilidade();
                    }
                }
            }
            if (erro_de_input_a)
            {
                GUI.Label(new Rect(0, posicao_erro_y, 210, 40), "Valor mínimo precisa ser de\n" + "apenas números.", "textfield");
                posicao_erro_y += 40;
            }
            if (erro_de_input_b)
            {
                GUI.Label(new Rect(0, posicao_erro_y, 210, 40), "Valor máximo precisa ser de\n" + "apenas números.", "textfield");
                posicao_erro_y += 40;
            }
            if (erro_de_maior_que)
            {
                GUI.Label(new Rect(0, posicao_erro_y, 210, 40), "Valor mínimo tem que ser\n" + "menor que o máximo.", "textfield");
            }

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
    }
	
	// Update is called once per frame
	void Update () {
        posx = Screen.width - 290;
    }
}
