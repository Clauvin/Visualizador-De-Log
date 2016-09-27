using UnityEngine;
using System.Collections;
using Basicas;

/// <summary>
/// Classe responsável por definir os botões da tela de seleção de opções de leitura de dados do FIT.
/// </summary>
public class GuiFITBotoesDaTelaDeSelecao : GuiPadrao2
{

    GUIStyle estilo_botoes_tela_de_selecao;
    private int posicao_x;
    private int qual_botao = -1;
    private int resultado = -1;
    private string[] toolbar_Strings = { "Voltar à\n Tela Inicial", "Voltar à\n Escolha de Log FIT",
                                        "Visualizar" };

    private bool janela_de_erro = false;

    public override void OnGUI()
    {
        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

        posicao_x = 0;

        resultado = GUI.Toolbar(new Rect(Screen.width / 12 * 3, Screen.height / 10 * 8,
                                         Screen.width / 12 * 6, Screen.height / 10), qual_botao,
                                         toolbar_Strings);

        switch (resultado)
        {

            case 0:
                MudaCenas.MudarCenaPara_Tela_Inicial();
                break;
            case 1:
                MudaCenas.MudarCenaPara_Pre_Fit();
                break;
            //Abre créditos
            case 2:
                Object.Instantiate
                    (Resources.Load("Objetos\\Passador De Dados"));
                PassadorDeDados pd_vai = FindObjectOfType<PassadorDeDados>().GetComponent<PassadorDeDados>();
                pd_vai.bd_fit = (BancoDeDadosFIT)GetComponent<NovoLeitor2>().GetBancoDeDadosFIT().Clone();
                pd_vai.bd_fit.RemoveEntradas(FindObjectOfType<GuiFITEscolhas>().
                                             gui_escolhas_de_jogadores.GetListaDeJogadores(),
                                             FindObjectOfType<GuiFITEscolhas>().
                                             gui_escolhas_de_jogadores.GetSelecoesDeJogadores(),
                                             FindObjectOfType<GuiFITEscolhas>().
                                             gui_escolhas_de_personagens.GetListaDePersonagens(),
                                             FindObjectOfType<GuiFITEscolhas>().
                                             gui_escolhas_de_personagens.GetSelecoesDePersonagens(),
                                             FindObjectOfType<GuiFITEscolhas>().
                                             gui_escolhas_de_niveis.GetListaDeNiveis(),
                                             FindObjectOfType<GuiFITEscolhas>().
                                             gui_escolhas_de_niveis.GetSelecoesDeNiveis());

                if (pd_vai.bd_fit.GetQuantidadeDeEntradas() > 0)
                {
                    pd_vai.heatmaps = GetComponent<NovoLeitor2>().Heatmaps;
                    pd_vai.NaoDestruirAoDescarregar();
                    MudaCenas.MudarCenaPara_Load_Fit();
                }
                else
                {
                    pd_vai.Destruir();
                    janela_de_erro = true;
                }

                break;
            default:
                break;
        }

        if (janela_de_erro)
        {
            GUI.BeginGroup(new Rect(Screen.width/4, 0, Screen.width/2, 30));

            GUI.Label(new Rect(0, 0, Screen.width / 2 - 40, 30), "A seleção escolhida não contém dados para análise.",
                      "textarea");

            if (GUI.Button(new Rect(Screen.width / 2 - 40, 0, 40, 30), "OK")) {
                janela_de_erro = false;
            }

            GUI.EndGroup();
        }

        resultado = -1;

        GUI.EndGroup();
    }

    // Use this for initialization
    void Start()
    {
        estilo_botoes_tela_de_selecao = new GUIStyle("box");
        estilo_botoes_tela_de_selecao.alignment = TextAnchor.MiddleCenter;
        estilo_botoes_tela_de_selecao.font = Font.CreateDynamicFontFromOSFont("Verdana", 10);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
