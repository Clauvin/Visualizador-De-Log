using UnityEngine;

/// <summary>
/// Classe responsável por mostrar ao usuário que jogadores, níveis e personagens ele pode escolher para serem
/// carregados.
/// <para>Não segue o padrão de ser uma filha de uma classe GUI genérica por questões de tempo de desenvolvimento: o ideal
/// é que houvesse uma classe GUI genérica GuiEscolhas.</para>
/// </summary>
public class GuiFITEscolhas : GuiPadrao2 {

    public GuiFITEscolhaDeJogadores gui_escolhas_de_jogadores;
    public GuiFITEscolhaDeNiveis gui_escolhas_de_niveis;
    public GuiFITEscolhaDePersonagens gui_escolhas_de_personagens;

    int meio_da_tela_x = Screen.width / 2;
    int meio_da_tela_y = Screen.height / 2;

    public GuiFITEscolhas()
    {
        gui_escolhas_de_jogadores = new GuiFITEscolhaDeJogadores();
        gui_escolhas_de_niveis = new GuiFITEscolhaDeNiveis();
        gui_escolhas_de_personagens = new GuiFITEscolhaDePersonagens();

        gui_escolhas_de_jogadores.RevelarGui();
        gui_escolhas_de_niveis.RevelarGui();
        gui_escolhas_de_personagens.RevelarGui();
    }

    public override void OnGUI()
    {

        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), string.Empty);

        gui_escolhas_de_jogadores.OnGUI();
        gui_escolhas_de_niveis.OnGUI();
        gui_escolhas_de_personagens.OnGUI();

    }

    void Awake()
    {
        gui_escolhas_de_jogadores = new GuiFITEscolhaDeJogadores();
        gui_escolhas_de_niveis = new GuiFITEscolhaDeNiveis();
        gui_escolhas_de_personagens = new GuiFITEscolhaDePersonagens();

        gui_escolhas_de_jogadores.RevelarGui();
        gui_escolhas_de_niveis.RevelarGui();
        gui_escolhas_de_personagens.RevelarGui();
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        meio_da_tela_x = Screen.width / 2;
        gui_escolhas_de_jogadores.SetX(meio_da_tela_x - Screen.width / 4 - gui_escolhas_de_jogadores.largura_da_janela/2);
        gui_escolhas_de_niveis.SetX(meio_da_tela_x - gui_escolhas_de_niveis.largura_da_janela / 2);
        gui_escolhas_de_personagens.SetX(meio_da_tela_x + Screen.width / 4 - gui_escolhas_de_personagens.largura_da_janela / 2);

        meio_da_tela_y = Screen.height / 4;
        gui_escolhas_de_jogadores.SetY(meio_da_tela_y);
        gui_escolhas_de_niveis.SetY(meio_da_tela_y);
        gui_escolhas_de_personagens.SetY(meio_da_tela_y);

    }
}
