using UnityEngine;
using System.Collections;

/// <summary>
/// Responsável por visualizar quais tipos de objetos estão visíveis, quais não
/// e permite ao usuário alterar isso.
/// </summary>
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

    public bool AlgumTipoDeObjetoInvisivel()
    {
        for (int i = 0; i < visivel_ou_invisivel.GetLength(0); i++)
        {
            if (visivel_ou_invisivel[i] == 0) return true;
        }
        return false;
    }

    public bool EstaInvisivel(int posicao)
    {
        if (visivel_ou_invisivel[posicao] == 0) return true;
        else return false;
    }

    public override void OnGUI()
    {
        if (revelado)
        {
            posicao_y = 0;
            GUI.BeginGroup(new Rect(posx, posy, 200, 20));
            GUI.TextField(new Rect(0, posicao_y, 200, 20), "Visibilidade de Tipos de Objetos");
            GUI.EndGroup();

            GUI.BeginGroup(new Rect(posx, posy+20, 200, 200));

            for (int i = 0; i < lista_de_nomes_de_objetos.Length; i++)
            {
                
                GUI.TextField(new Rect(0, posicao_y, 135, 20), lista_de_nomes_de_objetos[i]);
                posicao_y += 20;
                if (GUI.Button(new Rect(0, posicao_y, 135, 20), o_que_escrever_nos_botoes[visivel_ou_invisivel[i]]))
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
                posicao_y += 20;
            }
            

            GUI.EndGroup();
        }
    }

    public void InicializacaoComumATodos()
    {

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

}
