using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Basicas;

public class GuiBolhasHeatmap : GuiHeatmap
{

    public override void OnGUI()
    {
        int numero;

        if ((revelado) && (!gambiarra))
        {
            int qualheatmap = GetComponent<Controlador>().QualHeatmapMostra();
            GUI.BeginGroup(new Rect(posx, posy, 170, 20 *
                (dados[qualheatmap].numerosdecor.Count + 3)));

            posicaoy = 0;

            GUI.Box(new Rect(10, posicaoy, 170, 40 + 20 * dados[qualheatmap].numerosdecor.Count), string.Empty);

            if (qualheatmap == 0)
            {
                GUI.TextField(new Rect(10, posicaoy, 170, 20), "Cores e Quantos Objetos");
                posicaoy += 20;
            }
            else
            {
                GUI.TextField(new Rect(10, posicaoy, 170, 40), "Cores e Quantos Objetos\n - " +
                    lista_de_objetos[qualheatmap-1]);
                posicaoy += 40;
            }

            //lembrando, uma das cores já foi
            for (int i = 0; i < dados[qualheatmap].numerosdecor.Count; i++)
            {
                GUI.Box(new Rect(10, posicaoy, 20, 20), dados[qualheatmap].texturasdecor[i]);
                numero = dados[qualheatmap].numerosdecor[i];
                GUI.TextField(new Rect(35, posicaoy, 145, 20), numero.ToString());
                posicaoy += 20;
            }


            GUI.EndGroup();
        }
    }

    // Use this for initialization
    public void Start()
    {
        lista_de_objetos = GetComponent<NovoLeitor2>().lista_de_objetos_do_bolhas;
    }

}