using UnityEngine;
using System.Collections;

/// <summary>
/// Classe GuiFITInfoObjeto, derivada de GuiInfoObjeto.
/// <para>Responsável por mostrar dados referentes aos objetos do FIT para o usuário.</para>
/// </summary>
public class GuiFITInfoObjeto : GuiInfoObjeto
{
    public override void OnGUI()
    {
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 220, 80));
            posicaoy = 0;
            GUI.TextField(new Rect(10, posicaoy, 120, 20), "Personagem = " + dadosdoponto.personagem);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 120, 20), "X = " + dadosdoponto.x_log / 32);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 120, 20), "Y = " + dadosdoponto.y_log / 32);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 120, 20), "Tempo = " + dadosdoponto.tempo);

            GUI.EndGroup();
        }
    }

    void Awake()
    {
        dadosdoponto = null;
        gambiarra = true;
        posx = 0;
        posy = 0;
        texto = "";
        revelado = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gambiarra)
        {
            gambiarra = false;
        }
    }
}
