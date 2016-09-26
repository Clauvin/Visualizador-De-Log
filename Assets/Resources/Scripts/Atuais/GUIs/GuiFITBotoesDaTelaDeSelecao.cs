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
                pd_vai.bd_fit = GetComponent<NovoLeitor2>().GetBancoDeDadosFIT();
                pd_vai.heatmaps = GetComponent<NovoLeitor2>().Heatmaps;
                pd_vai.NaoDestruirAoDescarregar();
                MudaCenas.MudarCenaPara_Load_Fit();
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
        estilo_botoes_tela_de_selecao = new GUIStyle("box");
        estilo_botoes_tela_de_selecao.alignment = TextAnchor.MiddleCenter;
        estilo_botoes_tela_de_selecao.font = Font.CreateDynamicFontFromOSFont("Verdana", 10);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
