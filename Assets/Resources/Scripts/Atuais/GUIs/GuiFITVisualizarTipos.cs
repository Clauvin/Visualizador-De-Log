using UnityEngine;
using System.Collections;

public class GuiFITVisualizarTipos : GuiVisualizarTipos
{

	// Use this for initialization
	void Start () {

        lista_de_nomes_de_objetos = GetComponent<NovoLeitor2>().lista_de_nomes_de_objetos_do_FIT;

        InicializacaoComumATodos();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
