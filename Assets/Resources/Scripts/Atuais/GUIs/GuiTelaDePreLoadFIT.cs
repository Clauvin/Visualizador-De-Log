﻿using UnityEngine;
using System.IO;
using System;
using Basicas;

/// <summary>
/// Classe derivada de GuiTelaDePreLoad, responsável por definir para a Scene de PreLoad do FIT o que o programa deve fazer.
/// </summary>
public class GuiTelaDePreLoadFIT : GuiTelaDePreLoad
{
    
    // Essa função diverge entre FIT e Bolhas, por conta da diferença do formato do log de ambos.
    protected override void EscolhaDeArquivo()
    {

        if (pegar_endereco_do_log.FindFile())
        {
            endereco = pegar_endereco_do_log.endereco_de_arquivo[0];
            nome_do_arquivo = pegar_endereco_do_log.GetNomeDeArquivoDeLog();

            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            fs = new FileStream(pegar_endereco_do_log.endereco_de_arquivo[0], FileMode.Open);
            theReader = new StreamReader(fs);

            // Parte 1: ignora o [Mode 01]
            control_line = theReader.ReadLine();

            // Lê a linha com dados do pre-load do FIT.
            line = control_line;

            control_line = theReader.ReadLine(); control_line = theReader.ReadLine(); control_line = theReader.ReadLine();
            control_line = theReader.ReadLine();

            int contagem = 0;

            if (control_line != null)
            {
                entradas_separadas = control_line.Split('=');

                // == 4 porquê existem 4 termos por linha de dados no log do FIT.
                if (entradas_separadas.Length == 7)
                {
                    tempo_minimo = Convert.ToString(0);
                }

                

                do
                {
                    line = control_line;
                    control_line = theReader.ReadLine();
                    contagem++;

                } while (control_line != null);

            }

            tempo_maximo = Convert.ToString(contagem);

            lida_com_texto.FecharReaders(fs, theReader);

            pegar_endereco_do_log.CriarIniDeUltimoLogChecado(endereco);
        }
    }

    protected override void InicializacaoEspecifica()
    {
        lida_com_erros_endereco_de_log.valor_de_comparacao_de_tipo_de_log = "[Mode FIT]";
        titulo = "Escolha de Log do FIT\n" + "e Tempo Carregado do Log";
        toolbarStrings = new string[] { "Escolher Log", "O Que Visualizar do Log", "Retornar Para Tela Título" };
    }

    protected override void DesenharTelaDePreLoad()
    {

        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

        if (!pegar_endereco_do_log.Get_Desenhar_Navegador())
        {

            posicaox = 0;
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), string.Empty);
            GUI.Box(new Rect(Screen.width / 4, Screen.height / 9, Screen.width / 2, Screen.height / 4),
                titulo, estilo_titulo_tela_de_preload);

            GUI.Label(new Rect(Screen.width / 4, Screen.height / 2 - 20, Screen.width / 2, 20), nome_do_arquivo);
            GUI.TextField(new Rect(Screen.width / 4, Screen.height / 2, Screen.width / 2, 20), endereco, "textfield");

            resultado = GUI.Toolbar(new Rect(Screen.width / 12 * 3, Screen.height / 10 * 8, Screen.width / 12 * 6, Screen.height / 10),
                qualbotao, toolbarStrings);
        }

        FuncionamentoDosBotoes();

        lida_com_erros_endereco_de_log.DesenharPossiveisMensagensDeErro();

        resultado = -1;

        GUI.EndGroup();

    }

    protected override void FuncionamentoDosBotoes()
    {
        switch (resultado)
        {
            // Abre a janela do FIT de escolher arquivo, e lê do arquivo escolhido seu tempo inicial e final.
            case 0:

                pegar_endereco_do_log.Inverter_Desenhar_Navegador();
                lida_com_erros_endereco_de_log.ResetarMensagensDeErro();

                break;
            // Vai para o visualizador do FITs
            case 1:

                lida_com_erros_min_e_max.DetectarETratarErrosEExcecoesDeInput(tempo_minimo, tempo_maximo);
                lida_com_erros_endereco_de_log.DetectarETratarErrosEExcecoesDeInput(endereco);

                if (lida_com_erros_min_e_max.NaoTemosErrosDeInput() && lida_com_erros_endereco_de_log.NaoTemosErrosDeInput())
                {
                    pd = FindObjectOfType<PassadorDeDados>();
                    pd.SetValuesDePassagem(Convert.ToInt32(tempo_minimo), Convert.ToInt32(tempo_maximo), endereco);
                    IrParaLoad();
                }
                break;
            // Retorna para a tela título
            case 2:
                pd = FindObjectOfType<PassadorDeDados>();
                pd.Destruir();
                MudaCenas.MudarCenaPara_Tela_Inicial();
                break;
            default:
                break;
        }
    }

    protected override void IrParaLoad()
    {
        MudaCenas.MudarCenaPara_Selecao_Fit();
    }

    // Use this for initialization
    void Start()
    {

        InicializacaoGenerica();
        InicializacaoEspecifica();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
