using UnityEngine;
using System.Collections;

/// <summary>
/// Responsável por visualizar quais tipos de objetos do Bolhas estão visíveis, quais não
/// e permite ao usuário alterar isso.
/// </summary>
public class GuiBolhasVisualizarTipos : GuiVisualizarTipos
{

	// Use this for initialization
	void Start () {

        lista_de_nomes_de_objetos = GetComponent<NovoLeitor2>().lista_de_nomes_de_objetos_do_bolhas;

        InicializacaoComumATodos();

    }
}
