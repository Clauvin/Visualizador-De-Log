using UnityEngine;
using System.Collections;

public class GuiBolhasPonto : GuiPonto
{

    public override void OnGUI()
    {
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 270, 120));
            posicaoy = 0;
            GUI.TextField(new Rect(10, posicaoy, 260, 20), "Objeto = " + dadosdoponto.personagem);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 260, 20), "X = " + dadosdoponto.xlog);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 260, 20), "Y = " + dadosdoponto.ylog);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 260, 20), "Tempo = " + dadosdoponto.tempo);
            posicaoy += 20;
            if (dadosdoponto.personagem == "Mouse")
            {
                GUI.TextField(new Rect(10, posicaoy, 260, 20), "O Que Fez = " + dadosdoponto.oquefez);
            } else
            {
                GUI.TextField(new Rect(10, posicaoy, 260, 20), "O Que Fizeram Com Ele = " + dadosdoponto.oquefez);
                posicaoy += 20;
                GUI.TextField(new Rect(10, posicaoy, 260, 20), "Quem Criou = " + dadosdoponto.quemcriou);
            }

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