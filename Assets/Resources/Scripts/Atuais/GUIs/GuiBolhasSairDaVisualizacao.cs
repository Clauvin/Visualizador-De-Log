using UnityEngine;
using System.Collections;

/// <summary>
/// Classe derivada de GuiSairDaVisualizacao.
/// <para>Responsável pelos dois botões
/// na tela de visualização dos elementos do log do Bolhas para voltar para o Pre-Load e voltar para a tela inicial.</para>
/// </summary>
public class GuiBolhasSairDaVisualizacao : GuiSairDaVisualizacao {

    public override void RetornarParaTelaDePreLoad() { MudarCenaPara_Load_Bolhas(); }

}
