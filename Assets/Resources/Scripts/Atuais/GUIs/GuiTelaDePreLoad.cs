using UnityEngine;
using System.Collections;
using System.IO;
using System;
using Basicas;

/// <summary>
/// Classe básica semi-esqueletal(por conta de classes virtuais vazias)
/// responsável pela interface de usuário da tela de preload e definir o que cada botão nela faz.
/// </summary>
public class GuiTelaDePreLoad : GuiPadrao2 {

    protected GUIStyle estilo_titulo_tela_de_preload;
    protected GUIStyle estilo_botoes_tela_de_preload;
    protected int posicaox;
    protected int qualbotao = -1;
    protected int resultado = -1;
    protected string[] toolbarStrings;
    protected string tempo_minimo = "Apenas números >= a 0 aqui.";
    protected string tempo_maximo = "Apenas números >= a 0 aqui.";

    protected bool creditos = false;

    protected PegarEnderecoDeLog pegar_endereco_do_log;
    protected string endereco = "Aqui ficará o endereço do log.";
    protected string nome_do_arquivo = "Nome do Arquivo de Log - apenas arquivos .txt são aceitos.";
    protected LidaComErrosTempoMinimoEMaximo lida_com_erros_min_e_max;
    protected LidaComErrosEnderecoDeLog lida_com_erros_endereco_de_log;

    // todas as variáveis abaixo são para a leitura do tempo mínimo e máximo do log.
    protected FileStream fs;
    protected StreamReader theReader;
    protected string line;
    protected string control_line;
    protected string[] entradas_separadas;

    protected string titulo;

    protected PassadorDeDados pd;

    protected LidaComTexto lida_com_texto = new LidaComTexto();

    public override void OnGUI()
    {
        lida_com_erros_min_e_max.ConfigurarVariaveisDePosicionamentoDeGuiParaPreload();
        lida_com_erros_endereco_de_log.ConfigurarVariaveis();

        DesenharTelaDePreLoad();

        // graças a C# File Browser, foi necessário adicionar esse código
        // ao invés de usar FindFile() como antes.
        if (pegar_endereco_do_log.navegador_de_arquivos.outputFile != null)
        {
            pegar_endereco_do_log.Inverter_Desenhar_Navegador();
            EscolhaDeArquivo();
            pegar_endereco_do_log.navegador_de_arquivos.outputFile = null;
        }

        pegar_endereco_do_log.DesenharNavegadorDeArquivos();

    }

    // Inicialização comum a todos os tipos de log existentes
    protected void InicializacaoGenerica()
    {
        estilo_titulo_tela_de_preload = new GUIStyle();
        estilo_titulo_tela_de_preload.alignment = TextAnchor.MiddleCenter;
        estilo_titulo_tela_de_preload.font = Font.CreateDynamicFontFromOSFont("Verdana", 30);

        estilo_botoes_tela_de_preload = new GUIStyle("box");
        estilo_botoes_tela_de_preload.alignment = TextAnchor.MiddleCenter;
        estilo_botoes_tela_de_preload.font = Font.CreateDynamicFontFromOSFont("Verdana", 10);

        pegar_endereco_do_log = new PegarEnderecoDeLog();
        lida_com_erros_min_e_max = new LidaComErrosTempoMinimoEMaximo();
        lida_com_erros_endereco_de_log = new LidaComErrosEnderecoDeLog();
    }

    // Inicialização específica dependendo do log que se vai ler
    protected virtual void InicializacaoEspecifica()
    {

    }

    protected virtual void DesenharTelaDePreLoad()
    {

    }

    protected virtual void FuncionamentoDosBotoes()
    {

    }

    // Mudança de Scene
    protected virtual void IrParaLoad()
    {

    }

    // Essa função diverge entre FIT e Bolhas, por conta da diferença do formato do log de ambos.
    protected virtual void EscolhaDeArquivo()
    {

    }

}
