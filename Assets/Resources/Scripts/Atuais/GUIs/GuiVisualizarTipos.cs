using UnityEngine;
using System.Collections;

public class GuiVisualizarTipos : GuiPadrao2 {

    public int posicao_y = 0;
    public string[] lista_de_nomes_de_objetos = { "Mouse", "Baleia", "Bolha", "Peixe", "Nuvem" };
    int tamanho;

    public override void OnGUI()
    {
        if (revelado)
        {
            
            GUI.BeginGroup(new Rect(posx, posy, 270, 120));
            GUI.TextField(new Rect(0, posicao_y, 270, 20), "Visibilidade de Tipos de Objetos");

            GUI.EndGroup();
        }
    }

    // Use this for initialization
    void Start () {

        posx = 10;
        posy = 400;
        revelado = true;
        tamanho = 20 * (lista_de_nomes_de_objetos.Length + 1);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
