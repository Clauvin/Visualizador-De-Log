using UnityEngine;
using Basicas;

/// <summary>
/// Classe GuiFITSairDaVisualizacao. Filha de GuiSairDaVisualizacao, e responsável pelos três botões
/// na tela de visualização dos elementos do log do FIT para voltar para o Pre-Load,
/// para a seleção de jogadores/níveis/personagens e voltar para a tela inicial.
/// </summary>
public class GuiFITSairDaVisualizacao : GuiSairDaVisualizacao {

    public override void RetornarParaTelaDePreLoad() { MudaCenas.MudarCenaPara_Pre_Fit(); }

    protected override void Escolha()
    {
        switch (resultado)
        {
            case 0:
                RetornarParaTelaDePreLoad();
                break;

            case 1:
                Instantiate(Resources.Load<GameObject>("Objetos\\Passador De Dados"));
                PassadorDeDados pd = FindObjectOfType<PassadorDeDados>();
                pd.SetValuesDePassagem(0, int.MaxValue,
                    GetComponent<NovoLeitorFIT>().pegar_endereco_de_log.endereco_de_arquivo[0]);
                pd.NaoDestruirAoDescarregar();
                MudaCenas.MudarCenaPara_Selecao_Fit();
                break;

            case 2:
                MudaCenas.MudarCenaPara_Tela_Inicial();
                break;
        }
    }

    // Use this for initialization
    void Start () {
        largura_dos_botoes = 480;
        altura_dos_botoes = 22;
        strings_da_toolbar = new string[3] { "Pre-Load", "Seleção de Jogadores", "Tela Título" };
	}

}
