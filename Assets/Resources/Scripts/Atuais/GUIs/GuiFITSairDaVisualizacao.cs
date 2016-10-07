using Basicas;

/// <summary>
/// Classe GuiFITSairDaVisualizacao. Filha de GuiSairDaVisualizacao, e responsável pelos dois botões
/// na tela de visualização dos elementos do log do FIT para voltar para o Pre-Load e voltar para a tela inicial.
/// </summary>
public class GuiFITSairDaVisualizacao : GuiSairDaVisualizacao {

    public override void RetornarParaTelaDePreLoad() { MudaCenas.MudarCenaPara_Pre_Fit(); }

}
