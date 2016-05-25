using UnityEngine;
using System.Collections;

/// <summary>
/// Classe GuiBolhasSairDaVisualizacao. Filha de GuiSairDaVisualizacao, e responsável pelos dois botões
/// na tela de visualização dos elementos do log do Bolhas para voltar para o Pre-Load e voltar para a tela inicial.
/// </summary>
public class GuiBolhasSairDaVisualizacao : GuiSairDaVisualizacao {

    public override void RetornarParaTelaDePreLoad() { UnityEngine.SceneManagement.SceneManager.LoadScene(2); }

}
