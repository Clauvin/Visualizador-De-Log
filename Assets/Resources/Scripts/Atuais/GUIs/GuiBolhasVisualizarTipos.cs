using UnityEngine;
using System.Collections;

public class GuiBolhasVisualizarTipos : GuiVisualizarTipos
{

	// Use this for initialization
	void Start () {

        lista_de_nomes_de_objetos = GetComponent<NovoLeitor2>().lista_de_nomes_de_objetos_do_bolhas;

        InicializacaoComumATodos();

    }
}
