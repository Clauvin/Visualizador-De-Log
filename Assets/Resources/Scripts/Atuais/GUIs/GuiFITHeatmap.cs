using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Basicas;

/// <summary>
/// Classe GuiFITHeatmap, derivada de GuiHeatmap.
/// <para>É a classe responsável por mostrar as cores usadas nos heatmaps do Bolhas e quanto objetos naquele espaço
/// a cor representa.</para>
/// <para>A única diferença dela de GuiBolhasHeatmap é o Start(), no resto, são idênticas o suficiente
/// para que o método OnGUI possa ser colocado em GuiHeatmap posteriormente.</para>
/// </summary>
public class GuiFITHeatmap : GuiHeatmap
{

    public override void OnGUI()
    {
        int numero;

        if ((revelado) && (!gambiarra))
        {
            int qualheatmap = GetComponent<Controlador>().QualHeatmapMostra();

            posicao_y = 0;

            if (qualheatmap == 0)
            {
                GUI.TextField(new Rect(10, posy-20, 160, 20), "Cores e Quantos Objetos");
            }
            else
            {
                GUI.TextField(new Rect(10, posy-40, 160, 40), "Cores e Quantos Objetos\n - Jogador " +
                    lista_de_objetos[qualheatmap - 1]);
            }

            // Begin the ScrollView
            scrollViewVector = GUI.BeginScrollView(new Rect(posx, posy, largura_da_janela, altura_da_janela),
                                                   scrollViewVector,
                                                   new Rect(0, 0, 170, 20 *
                                                    (dados[qualheatmap].numeros_de_cor.Count + 3)));

            GUI.Box(new Rect(10, posicao_y, 170, 40 + 20 * dados[qualheatmap].numeros_de_cor.Count), string.Empty);

            

            //lembrando, uma das cores já foi
            for (int i = 0; i < dados[qualheatmap].numeros_de_cor.Count; i++)
            {
                GUI.Box(new Rect(10, posicao_y, 20, 20), dados[qualheatmap].texturas_de_cor[i]);
                numero = dados[qualheatmap].numeros_de_cor[i];
                GUI.TextField(new Rect(35, posicao_y, 80, 20), numero.ToString());
                posicao_y += 20;
            }

            GUI.EndScrollView();

        }
    }


    // Use this for initialization
    public void Start()
    {
        lista_de_objetos = GetComponent<NovoLeitor2>().lista_de_nomes_de_objetos_do_FIT;
        largura_da_janela = 200;
        altura_da_janela = 300;
    }

}