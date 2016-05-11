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

    //variáveis que definem se houve ou não erro, o que mostra mensagens de erro.
    bool erro_de_input_errado_minimo = false;
    bool erro_de_input_errado_maximo = false;
    bool erro_de_input_vazio_minimo = false;
    bool erro_de_input_vazio_maximo = false;
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
                if (tempominimo == "") { erro_de_input_vazio_minimo = true; }
                else erro_de_input_vazio_minimo = false;

                if (tempomaximo == "") { erro_de_input_vazio_maximo = true; }
                else erro_de_input_vazio_maximo = false;

                if (!erro_de_input_vazio_minimo) { 
                    try { Int32.Parse(tempominimo); erro_de_input_errado_minimo = false; }
                    catch (FormatException fe) { erro_de_input_errado_minimo = true; }
                }

                if (!erro_de_input_vazio_maximo) { 
                    try { Int32.Parse(tempomaximo); erro_de_input_errado_maximo = false; }
                    catch (FormatException fe) { erro_de_input_errado_maximo = true; }
                }

                if (!(erro_de_input_errado_minimo || erro_de_input_errado_maximo ||
                      erro_de_input_vazio_minimo || erro_de_input_vazio_maximo))
                {
                    if (Int32.Parse(tempominimo) > Int32.Parse(tempomaximo))
                    {
                        erro_de_maior_que = true;
                    } else { erro_de_maior_que = false; }
                }

                //Deus abençoe que C# me permite fazer essas comparações em sequência.
                if (!(erro_de_input_errado_minimo || erro_de_input_errado_maximo || erro_de_maior_que ||
                      erro_de_input_vazio_minimo || erro_de_input_vazio_maximo))
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
            if (erro_de_input_errado_minimo)
            {
                GUI.Label(new Rect(0, posicao_erro_y, 210, 40), "Valor mínimo precisa ser de\n" + "apenas números.", "textfield");
                posicao_erro_y += 40;
            }
            if (erro_de_input_errado_maximo)
            {
                GUI.Label(new Rect(0, posicao_erro_y, 210, 40), "Valor máximo precisa ser de\n" + "apenas números.", "textfield");
                posicao_erro_y += 40;
            }
            if (erro_de_input_vazio_minimo)
            {
                GUI.Label(new Rect(0, posicao_erro_y, 210, 40), "Falta preencher o valor mínimo.", "textfield");
                posicao_erro_y += 40;
            }
            if (erro_de_input_vazio_maximo)
            {
                GUI.Label(new Rect(0, posicao_erro_y, 210, 40), "Falta preencher o valor máximo.", "textfield");
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
