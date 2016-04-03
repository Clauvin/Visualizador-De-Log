using UnityEngine;
using System.Collections;

public class GuiBolhasPonto : GuiPonto
{
    public override void OnGUI()
    {
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 270, 80));
            posicaoy = 0;
            GUI.TextField(new Rect(10, posicaoy, 260, 20), "Personagem = " + dadosdoponto.personagem);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 260, 20), "X = " + dadosdoponto.xlog / 32);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 260, 20), "Y = " + dadosdoponto.ylog / 32);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 260, 20), "Tempo = " + dadosdoponto.tempo);

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