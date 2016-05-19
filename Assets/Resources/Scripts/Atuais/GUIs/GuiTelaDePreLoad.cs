﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Classe GuiTelaDePreLoad. Responsável por definir a tela de pré-load de log.
/// </summary>
public class GuiTelaDePreLoad : GuiPadrao2
{
    GUIStyle estilo_titulo_tela_de_preload;
    GUIStyle estilo_botoes_tela_de_preload;
    private int posicaox;
    private int qualbotao = -1;
    private int resultado = -1;
    private string[] toolbarStrings = { "Escolher Log", "Visualizar Log",
                                        "Retornar Para Tela Título" };
    protected string tempo_minimo = "Apenas números >= a 0 aqui.";
    protected string tempo_maximo = "Apenas números >= a 0 aqui.";
    protected int tempo_minimo_convertido;
    protected int tempo_maximo_convertido;

    private bool creditos = false;

    protected PegarEnderecoDeLog pegar_endereco_do_log;
    protected string endereco = "Aqui ficará o endereço do log.";
    protected string nome_do_arquivo = "Nome do Arquivo de Log";

    public override void OnGUI()
    {
        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

        posicaox = 0;
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), string.Empty);
        GUI.Box(new Rect(Screen.width / 4, Screen.height / 9, Screen.width / 2, Screen.height / 4), "Escolha de Log do FIT\n" +
            "e Tempo Carregado do Log", estilo_titulo_tela_de_preload);

        GUI.Label(new Rect(Screen.width / 4, Screen.height / 2 - 20, Screen.width / 2, 20), nome_do_arquivo);
        GUI.Label(new Rect(Screen.width / 4, Screen.height / 2, Screen.width / 2, 20), endereco, "textfield");
        GUI.Label(new Rect(Screen.width / 4, Screen.height / 2 + 20, 120, 20), "Tempo Mínimo", "textfield");
        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 + 20, 120, 20), "Tempo Máximo", "textfield");
        tempo_minimo = GUI.TextArea(new Rect(Screen.width / 4, Screen.height / 2 + 40, 240, 20), tempo_minimo);
        tempo_maximo = GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2 + 40, 240, 20), tempo_maximo);


        resultado = GUI.Toolbar(new Rect(Screen.width / 12 * 3, Screen.height / 10 * 8, Screen.width / 12 * 6, Screen.height / 10), qualbotao,
            toolbarStrings);

        switch (resultado)
        {
            // Abre a janela do FIT de escolher arquivo, e lê do arquivo escolhido seu tempo inicial e final.
            case 0:
                if (pegar_endereco_do_log.FindFile())
                {
                    endereco = pegar_endereco_do_log.endereco_de_arquivo;
                    nome_do_arquivo = pegar_endereco_do_log.GetNomeDeArquivoDeLog();


                }
                
                break;
            // Vai para o visualizador do FITs
            case 1:
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                break;
            // Retorna para a tela título
            case 2:
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                break;
            default:
                break;
        }

        resultado = -1;

        GUI.EndGroup();

    }

    // Use this for initialization
    void Start()
    {
        estilo_titulo_tela_de_preload = new GUIStyle();
        estilo_titulo_tela_de_preload.alignment = TextAnchor.MiddleCenter;
        estilo_titulo_tela_de_preload.font = Font.CreateDynamicFontFromOSFont("Verdana", 30);

        estilo_botoes_tela_de_preload = new GUIStyle("box");
        estilo_botoes_tela_de_preload.alignment = TextAnchor.MiddleCenter;
        estilo_botoes_tela_de_preload.font = Font.CreateDynamicFontFromOSFont("Verdana", 10);

        pegar_endereco_do_log = new PegarEnderecoDeLog();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
