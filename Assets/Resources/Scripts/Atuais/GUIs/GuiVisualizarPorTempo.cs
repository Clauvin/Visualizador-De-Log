using UnityEngine;
using System.Collections;
using System;
using System.Globalization;

/// <summary>
/// Classe GuiVisualizarPorTempo. Responsável por permitir ao usuário sumir e aparecer com objetos baseando-se no
/// tempo em que aparecem.
/// <para>Atenção: essa classe tem um tratamento grande de exceções, o que no futuro precisa ser feito em outras
/// classes, conforme o projeto vai sendo usado por mais e mais usuários.</para>
/// </summary>
public class GuiVisualizarPorTempo : GuiPadrao2 {

    string tempo_minimo = "Apenas >= 0 aqui.";
    //... e apenas <= tempo final, mas não cabe na GUI e na prática não atrapalha.
    string tempo_maximo = "Apenas >= 0 aqui.";
    int visivel_ou_invisivel = 1;
    public string[] o_que_escrever_nos_botoes;

    //variáveis que definem se houve ou não erro, o que mostra mensagens de erro.
    bool erro_de_input_errado_minimo = false;
    bool erro_de_input_errado_maximo = false;
    bool erro_de_input_vazio_minimo = false;
    bool erro_de_input_vazio_maximo = false;
    // nesse caso limite minimo é o Int32.MinValue, já que a função de deixar visível ou invisível tem como input dois ints.
    // essa detecção poderia ser feita na função de deixar visível ou invisível, mas
    // é melhor deixar as deteccoes e tratamentos de input errado numa função só pra isso
    // do que espalhar elas pelo código.
    bool erro_de_input_minimo_menor_que_limite_minimo = false;
    bool erro_de_input_maximo_maior_que_limite_maximo = false;
    bool erro_de_maior_que = false;
    int posicao_erro_y = 100;

    void MudaVisibilidade()
    {
        if (visivel_ou_invisivel == 1) visivel_ou_invisivel = 0;
        else visivel_ou_invisivel = 1;
    }

    void DetectarETratarErrosEExcecoesDeInput(string tempo_minimo, string tempo_maximo)
    {

        // esse reset das variáveis ao usar essa função é necessário para evitar
        // que as mensagens de inputs errados bloqueiem as mensagens de inputs vazios.
        erro_de_input_errado_minimo = false;
        erro_de_input_errado_maximo = false;
        erro_de_input_vazio_minimo = false;
        erro_de_input_vazio_maximo = false;
        erro_de_input_minimo_menor_que_limite_minimo = false;
        erro_de_input_maximo_maior_que_limite_maximo = false;
        erro_de_maior_que = false;

        if (tempo_minimo == "") { erro_de_input_vazio_minimo = true; }

        if (tempo_maximo == "") { erro_de_input_vazio_maximo = true; }

        long long_minimo = long.Parse(tempo_minimo, NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign);
        long long_maximo = long.Parse(tempo_maximo, NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign);
        if (long_minimo < Int32.MinValue)
        {
            erro_de_input_minimo_menor_que_limite_minimo = true;
        }

        if (long_maximo > Int32.MaxValue)
        {
            erro_de_input_maximo_maior_que_limite_maximo = true;
        }

        if (!(erro_de_input_vazio_minimo || erro_de_input_minimo_menor_que_limite_minimo))
        {
            try { Int32.Parse(tempo_minimo); }
            catch (FormatException fe) { erro_de_input_errado_minimo = true; }
        }

        if (!(erro_de_input_vazio_maximo || erro_de_input_maximo_maior_que_limite_maximo))
        {
            try { Int32.Parse(tempo_maximo); }
            catch (FormatException fe) { erro_de_input_errado_maximo = true; }
        }

        if (!(erro_de_input_errado_minimo || erro_de_input_errado_maximo ||
              erro_de_input_vazio_minimo || erro_de_input_vazio_maximo || 
              erro_de_input_minimo_menor_que_limite_minimo || erro_de_input_maximo_maior_que_limite_maximo))
        {
            if (Int32.Parse(tempo_minimo) > Int32.Parse(tempo_maximo))
            {
                erro_de_maior_que = true;
            }
            else { erro_de_maior_que = false; }
        }
    }

    public override void OnGUI()
    {
        posicao_erro_y = 100;
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 290, 180));
            GUI.TextField(new Rect(0, 0, 290, 20), "Invisibilidade de Espaço de Tempo");
            GUI.Label(new Rect(0, 20, 210, 20), "Tempo Mínimo", "textfield");
            tempo_minimo = GUI.TextField(new Rect(0, 40, 210, 20), tempo_minimo);
            GUI.Label(new Rect(0, 60, 210, 20), "Tempo Máximo", "textfield");
            tempo_maximo = GUI.TextField(new Rect(0, 80, 210, 20), tempo_maximo);
            if (GUI.Button(new Rect(210, 20, 80, 80), o_que_escrever_nos_botoes[visivel_ou_invisivel]))
            {
                DetectarETratarErrosEExcecoesDeInput(tempo_minimo, tempo_maximo);

                // Deus abençoe que C# me permite fazer essas comparações em sequência.
                if (!(erro_de_input_errado_minimo || erro_de_input_errado_maximo || erro_de_maior_que ||
                      erro_de_input_vazio_minimo || erro_de_input_vazio_maximo))
                {
                    if (visivel_ou_invisivel == 1)
                    {
                        GetComponent<Controlador>().DeixarObjetosEmEspacoDeTempoInvisiveisEIninteragiveis(
                            Int32.Parse(tempo_minimo), Int32.Parse(tempo_maximo));
                        MudaVisibilidade();
                    }
                    else
                    {
                        GetComponent<Controlador>().DeixarObjetosEmEspacoDeTempoVisiveisEInteragiveis(
                            Int32.Parse(tempo_minimo), Int32.Parse(tempo_maximo));
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
            if (erro_de_input_minimo_menor_que_limite_minimo)
            {
                // Sim, eu me dou o direito de apontar isso, porquê quem vai colocar o tempo ABSURDAMENTE
                // menor que zero?!
                GUI.Label(new Rect(0, posicao_erro_y, 210, 40), "... por favor. Você fez isso de propósito.", "textfield");
                posicao_erro_y += 40;
            }
            if (erro_de_input_maximo_maior_que_limite_maximo)
            {

                GUI.Label(new Rect(0, posicao_erro_y, 210, 40), "...tempo máximo não pode ultrapassar 2147483647.", "textfield");
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
