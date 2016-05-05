using UnityEngine;
using System.Collections;

public class GuiVisualizarTipos : GuiPadrao2 {

    public int posicao_y = 0;
    public string[] lista_de_nomes_de_objetos;
    int tamanho;
    public int[] visivel_ou_invisivel;
    public string[] o_que_escrever_nos_botoes;

    void MudaVisibilidade(int posicao)
    {
        if (visivel_ou_invisivel[posicao] == 1) visivel_ou_invisivel[posicao] = 0;
        else visivel_ou_invisivel[posicao] = 1;
    }

    public override void OnGUI()
    {
        if (revelado)
        {
            posicao_y = 0;
            GUI.BeginGroup(new Rect(posx, posy, 270, 120));
            GUI.TextField(new Rect(0, posicao_y, 270, 20), "Visibilidade de Tipos de Objetos");
            for (int i = 0; i < lista_de_nomes_de_objetos.Length; i++)
            {
                
                posicao_y += 20;
                GUI.TextField(new Rect(0, posicao_y, 135, 20), lista_de_nomes_de_objetos[i]);
                if (GUI.Button(new Rect(135, posicao_y, 135, 20), o_que_escrever_nos_botoes[visivel_ou_invisivel[i]]))
                {
                    if (visivel_ou_invisivel[i] == 1)
                    {
                        GetComponent<Controlador>().DeixarTipoDeObjetoInvisivelEIninteragivel(lista_de_nomes_de_objetos[i]);
                    }
                    else
                    {
                        GetComponent<Controlador>().DeixarTipoDeObjetoVisivelEInteragivel(lista_de_nomes_de_objetos[i]);
                    }
                    MudaVisibilidade(i);
                }
            }
            

            GUI.EndGroup();
        }
    }

    // Use this for initialization
    void Start () {

        lista_de_nomes_de_objetos = new string[5];
        lista_de_nomes_de_objetos[0] = "Mouse"; lista_de_nomes_de_objetos[1] = "Baleia";
        lista_de_nomes_de_objetos[2] = "Bolha"; lista_de_nomes_de_objetos[3] = "Peixe";
        lista_de_nomes_de_objetos[4] = "Nuvem";

        visivel_ou_invisivel = new int[lista_de_nomes_de_objetos.Length];
        for (int i = 0; i < visivel_ou_invisivel.Length; i++)
        {
            visivel_ou_invisivel[i] = 1;
        }

        o_que_escrever_nos_botoes = new string[2];
        o_que_escrever_nos_botoes[0] = "Apertar para visível";
        o_que_escrever_nos_botoes[1] = "Apertar para invisível";

        posx = 10;
        posy = 400;
        revelado = true;
        tamanho = 20 * (lista_de_nomes_de_objetos.Length + 1);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
