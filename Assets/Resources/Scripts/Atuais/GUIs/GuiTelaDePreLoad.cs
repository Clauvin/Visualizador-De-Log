using UnityEngine;
using System.Collections;
using System.IO;
using System;

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
    protected string[] toolbarStrings = { "Escolher Log", "Visualizar Log",
                                        "Retornar Para Tela Título" };
    protected string tempo_minimo = "Apenas números >= a 0 aqui.";
    protected string tempo_maximo = "Apenas números >= a 0 aqui.";

    protected bool creditos = false;

    protected PegarEnderecoDeLog pegar_endereco_do_log;
    protected string endereco = "Aqui ficará o endereço do log.";
    protected string nome_do_arquivo = "Nome do Arquivo de Log";
    protected LidaComErrosTempoMinimoEMaximo lida_com_erros_min_e_max;
    protected LidaComErrosEnderecoDeLog lida_com_erros_endereco_de_log;

    // todas as variáveis abaixo são para a leitura do tempo mínimo e máximo do log.
    protected FileStream fs;
    protected StreamReader theReader;
    protected string line;
    protected string control_line;
    protected string[] entradas_separadas;

    protected string titulo = "Escolha de Log do FIT\n" + "e Tempo Carregado do Log";

    PassadorDeDados pd;

    public override void OnGUI()
    {
        lida_com_erros_min_e_max.ConfigurarVariaveisDePosicionamentoDeGuiParaPreload();
        lida_com_erros_endereco_de_log.ConfigurarVariaveis();
        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

        posicaox = 0;
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), string.Empty);
        GUI.Box(new Rect(Screen.width / 4, Screen.height / 9, Screen.width / 2, Screen.height / 4), titulo, estilo_titulo_tela_de_preload);

        GUI.Label(new Rect(Screen.width / 4, Screen.height / 2 - 20, Screen.width / 2, 20), nome_do_arquivo);
        GUI.Label(new Rect(Screen.width / 4, Screen.height / 2, Screen.width / 2, 20), endereco, "textfield");
        GUI.Label(new Rect(Screen.width / 4, Screen.height / 2 + 40, 120, 20), "Instante Mínimo", "textfield");
        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 + 40, 120, 20), "Instante Máximo", "textfield");
        tempo_minimo = GUI.TextArea(new Rect(Screen.width / 4, Screen.height / 2 + 60, 240, 20), tempo_minimo);
        tempo_maximo = GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2 + 60, 240, 20), tempo_maximo);

        resultado = GUI.Toolbar(new Rect(Screen.width / 12 * 3, Screen.height / 10 * 8, Screen.width / 12 * 6, Screen.height / 10), qualbotao,
            toolbarStrings);

        pegar_endereco_do_log.DesenharNavegadorDeArquivos();

        switch (resultado)
        {
            // Abre a janela do FIT de escolher arquivo, e lê do arquivo escolhido seu tempo inicial e final.
            case 0:

                pegar_endereco_do_log.Inverter_Desenhar_Navegador();

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
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                break;
            default:
                break;
        }

        if (pegar_endereco_do_log.navegador_de_arquivos.outputFile != null)
        {
            pegar_endereco_do_log.Inverter_Desenhar_Navegador();
            EscolhaDeArquivo();
            pegar_endereco_do_log.navegador_de_arquivos.outputFile = null;
        }

        lida_com_erros_min_e_max.PossiveisMensagensDeErro();
        lida_com_erros_endereco_de_log.PossiveisMensagensDeErro();

        resultado = -1;

        GUI.EndGroup();

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

    // Mudança de Scene
    protected virtual void IrParaLoad()
    {

    }

    // Essa função diverge entre FIT e Bolhas, por conta da diferença do formato do log de ambos.
    protected virtual void EscolhaDeArquivo()
    {

    }

}
