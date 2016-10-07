using UnityEngine;

/// <summary>
/// Classe derivada de GuiHeatmap.
/// <para>É a classe responsável por mostrar as cores usadas nos heatmaps do Bolhas e quanto objetos naquele espaço
/// a cor representa.</para>
/// <para>A única diferença dela de GuiFITHeatmap é o Start(), no resto, são idênticas o suficiente
/// para que o método OnGUI possa ser colocado em GuiHeatmap posteriormente.</para>
/// </summary>
public class GuiBolhasHeatmap : GuiHeatmap
{

    public override void OnGUI()
    {
        int numero;
        int qualheatmap;

        if ((revelado) && (!gambiarra))
        {
            qualheatmap = GetComponent<Controlador>().QualHeatmapMostra();
            GUI.BeginGroup(new Rect(posx, posy, 170, 20 *
                (dados[qualheatmap].numeros_de_cor.Count + 3)));

            posicao_y = 0;

            GUI.Box(new Rect(10, posicao_y, 170, 40 + 20 * dados[qualheatmap].numeros_de_cor.Count), string.Empty);

            if (qualheatmap == 0)
            {
                GUI.TextField(new Rect(10, posicao_y, 160, 20), "Cores e Quantos Objetos");
                posicao_y += 20;
            }
            else
            {
                GUI.TextField(new Rect(10, posicao_y, 160, 40), "Cores e Quantos Objetos\n - " +
                    lista_de_objetos[qualheatmap-1]);
                posicao_y += 40;
            }

            //lembrando, uma das cores já foi
            for (int i = 0; i < dados[qualheatmap].numeros_de_cor.Count; i++)
            {
                GUI.Box(new Rect(10, posicao_y, 20, 20), dados[qualheatmap].texturas_de_cor[i]);
                numero = dados[qualheatmap].numeros_de_cor[i];
                GUI.TextField(new Rect(35, posicao_y, 80, 20), numero.ToString());
                posicao_y += 20;
            }


            GUI.EndGroup();
        }
    }

    // Use this for initialization
    public void Start()
    {
        lista_de_objetos = GetComponent<NovoLeitor2>().lista_de_nomes_de_objetos_do_bolhas;
    }

}