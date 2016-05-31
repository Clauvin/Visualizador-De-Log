using UnityEngine;
using System.Collections;
using System;
using System.Globalization;

/// <summary>
/// Classe derivada de LidaComErros. <para>Criada para detectar erros nos valores de tempo mínimo e máximo usados no programa,
/// e uma vez encontrados, avisar isso e gerar mensagens de erro adequadas para que apareçam na GUI.</para>
/// </summary>
public class LidaComErrosTempoMinimoEMaximo : LidaComErros
{

    //variáveis que definem se houve ou não erro, o que mostra mensagens de erro.
    public bool erro_de_input_errado_minimo = false;
    public bool erro_de_input_errado_maximo = false;
    public bool erro_de_input_vazio_minimo = false;
    public bool erro_de_input_vazio_maximo = false;

    // nesse caso limite minimo é o Int32.MinValue, já que a função de deixar visível ou invisível tem como input dois ints.
    // essa detecção poderia ser feita na função de deixar visível ou invisível, mas
    // é melhor deixar as deteccoes e tratamentos de input errado numa função só pra isso
    // do que espalhar elas pelo código.
    public bool erro_de_input_minimo_menor_que_limite_minimo = false;
    public bool erro_de_input_minimo_maior_que_limite_maximo = false;
    public bool erro_de_input_maximo_menor_que_limite_minimo = false;
    public bool erro_de_input_maximo_maior_que_limite_maximo = false;
    public bool erro_de_maior_que = false;

    public int x_da_janela_de_erro_de_erro_minimo;
    public int largura_da_janela_de_erro_de_erro_minimo;
    public int altura_da_janela_de_erro_de_erro_minimo;

    public int x_da_janela_de_erro_de_erro_maximo;
    public int largura_da_janela_de_erro_de_erro_maximo;
    public int altura_da_janela_de_erro_de_erro_maximo;

    public void ConfigurarVariaveisParaVisualizacaoDeObjetos()
    {

        x_da_janela_de_erro_de_erro_minimo = 0;
        largura_da_janela_de_erro_de_erro_minimo = 210;
        altura_da_janela_de_erro_de_erro_minimo = 40;

        x_da_janela_de_erro_de_erro_maximo = 0;
        largura_da_janela_de_erro_de_erro_maximo = 210;
        altura_da_janela_de_erro_de_erro_maximo = 40;

        posicao_da_mensagem_de_erro_y = 100;
        quantidade_de_mudanca_de_posicao_y = 40;

    }

    public void ConfigurarVariaveisParaPreload()
    {
        x_da_janela_de_erro_de_erro_minimo = Screen.width / 4;
        largura_da_janela_de_erro_de_erro_minimo = 240;
        altura_da_janela_de_erro_de_erro_minimo = 40;

        x_da_janela_de_erro_de_erro_maximo = Screen.width / 2;
        largura_da_janela_de_erro_de_erro_maximo = 240;
        altura_da_janela_de_erro_de_erro_maximo = 40;

        posicao_da_mensagem_de_erro_y = Screen.height / 2 + 80;
        quantidade_de_mudanca_de_posicao_y = 0;
    }

    public void DetectarETratarErrosEExcecoesDeInput(string tempo_minimo, string tempo_maximo)
    {
        // esse reset das variáveis ao usar essa função é necessário para evitar
        // que as mensagens de inputs errados bloqueiem as mensagens de inputs vazios.
        erro_de_input_vazio_minimo = false;
        erro_de_input_vazio_maximo = false;
        erro_de_input_errado_minimo = false;
        erro_de_input_errado_maximo = false;
        erro_de_input_minimo_menor_que_limite_minimo = false;
        erro_de_input_minimo_maior_que_limite_maximo = false;
        erro_de_input_maximo_menor_que_limite_minimo = false;
        erro_de_input_maximo_maior_que_limite_maximo = false;
        erro_de_maior_que = false;

        if (tempo_minimo == "") { erro_de_input_vazio_minimo = true; }

        if (tempo_maximo == "") { erro_de_input_vazio_maximo = true; }

        // teste de valor mínimo: está alem do limite minimo e máximo que o int comporta?
        if (!erro_de_input_vazio_minimo)
        {
            try { Int32.Parse(tempo_minimo); }
            catch (FormatException fe) { erro_de_input_errado_minimo = true; }
            catch (OverflowException oe)
            {
                long long_minimo = long.Parse(tempo_minimo, NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign);
                if (long_minimo < Int32.MinValue)
                {
                    erro_de_input_minimo_menor_que_limite_minimo = true;
                }
                else if (long_minimo > Int32.MaxValue)
                {
                    erro_de_input_minimo_maior_que_limite_maximo = true;
                }
            }
        }

        // teste de valor máximo: está alem do limite minimo e máximo que o int comporta?
        if (!erro_de_input_vazio_maximo)
        {
            try { Int32.Parse(tempo_maximo); }
            catch (FormatException fe) { erro_de_input_errado_maximo = true; }
            catch (OverflowException oe)
            {
                long long_maximo = long.Parse(tempo_maximo, NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign);

                if (long_maximo < Int32.MinValue)
                {
                    erro_de_input_maximo_menor_que_limite_minimo = true;
                }
                else if (long_maximo > Int32.MaxValue)
                {
                    erro_de_input_maximo_maior_que_limite_maximo = true;
                }
            }
        }

        if (!(erro_de_input_errado_minimo || erro_de_input_errado_maximo ||
              erro_de_input_vazio_minimo || erro_de_input_vazio_maximo ||
              erro_de_input_minimo_menor_que_limite_minimo || erro_de_input_minimo_maior_que_limite_maximo ||
              erro_de_input_maximo_menor_que_limite_minimo || erro_de_input_maximo_maior_que_limite_maximo))
        {
            if (Int32.Parse(tempo_minimo) > Int32.Parse(tempo_maximo))
            {
                erro_de_maior_que = true;
            }
            else { erro_de_maior_que = false; }
        }
    }

    public override void PossiveisMensagensDeErro()
    {
        if (erro_de_input_errado_minimo) {

            GUI.Label(new Rect(x_da_janela_de_erro_de_erro_minimo, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro_de_erro_minimo, altura_da_janela_de_erro_de_erro_minimo),
                                "Valor mínimo precisa ser de\n" + "apenas números.", "textfield");
            posicao_da_mensagem_de_erro_y += quantidade_de_mudanca_de_posicao_y;
        }
        if (erro_de_input_errado_maximo)
        {
            GUI.Label(new Rect(x_da_janela_de_erro_de_erro_maximo, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro_de_erro_maximo, altura_da_janela_de_erro_de_erro_maximo),
                                "Valor máximo precisa ser de\n" + "apenas números.", "textfield");
            posicao_da_mensagem_de_erro_y += quantidade_de_mudanca_de_posicao_y;
        }
        if (erro_de_input_vazio_minimo)
        {
            GUI.Label(new Rect(x_da_janela_de_erro_de_erro_minimo, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro_de_erro_minimo, altura_da_janela_de_erro_de_erro_minimo),
                                "Falta preencher o valor mínimo.", "textfield");
            posicao_da_mensagem_de_erro_y += quantidade_de_mudanca_de_posicao_y;
        }
        if (erro_de_input_vazio_maximo)
        {
            GUI.Label(new Rect(x_da_janela_de_erro_de_erro_maximo, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro_de_erro_maximo, altura_da_janela_de_erro_de_erro_maximo),
                                "Falta preencher o valor máximo.", "textfield");
            posicao_da_mensagem_de_erro_y += quantidade_de_mudanca_de_posicao_y;
        }
        if (erro_de_input_minimo_menor_que_limite_minimo)
        {
            // Sim, eu me dou o direito de apontar isso, porquê quem vai colocar o tempo ABSURDAMENTE
            // menor que zero?!
            GUI.Label(new Rect(x_da_janela_de_erro_de_erro_minimo, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro_de_erro_minimo, altura_da_janela_de_erro_de_erro_minimo),
                                "... por favor. Você fez isso\n" + "de propósito.", "textfield");
            posicao_da_mensagem_de_erro_y += quantidade_de_mudanca_de_posicao_y;
        }
        if (erro_de_input_minimo_maior_que_limite_maximo)
        {
            // Sim, eu me dou o direito de apontar isso, porquê quem vai colocar o tempo ABSURDAMENTE
            // menor que zero?!
            GUI.Label(new Rect(x_da_janela_de_erro_de_erro_minimo, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro_de_erro_minimo, altura_da_janela_de_erro_de_erro_minimo),
                                "...tempo mínimo não pode\n" + "ultrapassar 2147483647.", "textfield");
            posicao_da_mensagem_de_erro_y += quantidade_de_mudanca_de_posicao_y;
        }
        if (erro_de_input_maximo_menor_que_limite_minimo)
        {

            GUI.Label(new Rect(x_da_janela_de_erro_de_erro_maximo, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro_de_erro_maximo, altura_da_janela_de_erro_de_erro_maximo),
                                "... por favor. Você COM CERTEZA fez\n" + "isso de propósito.", "textfield");
            posicao_da_mensagem_de_erro_y += quantidade_de_mudanca_de_posicao_y;
        }
        if (erro_de_input_maximo_maior_que_limite_maximo)
        {

            GUI.Label(new Rect(x_da_janela_de_erro_de_erro_maximo, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro_de_erro_maximo, altura_da_janela_de_erro_de_erro_maximo),
                                "...tempo máximo não pode\n" + "ultrapassar 2147483647.", "textfield");
            posicao_da_mensagem_de_erro_y += quantidade_de_mudanca_de_posicao_y;
        }
        if (erro_de_maior_que)
        {
            GUI.Label(new Rect(x_da_janela_de_erro_de_erro_minimo, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro_de_erro_minimo, altura_da_janela_de_erro_de_erro_minimo),
                                "Valor mínimo tem que ser\n" + "menor que o máximo.", "textfield");
        }
    }

    public override bool NaoTemosErrosDeInput()
    {

        // Deus abençoe que C# me permite fazer essas comparações em sequência.
        return !(erro_de_input_errado_minimo || erro_de_input_errado_maximo || erro_de_maior_que ||
                      erro_de_input_vazio_minimo || erro_de_input_vazio_maximo ||
                      erro_de_input_minimo_menor_que_limite_minimo || erro_de_input_minimo_maior_que_limite_maximo ||
                      erro_de_input_maximo_menor_que_limite_minimo || erro_de_input_maximo_maior_que_limite_maximo
                      );

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
