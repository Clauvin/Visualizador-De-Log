using UnityEngine;
using System.Collections;

/// <summary>
/// Classe GuiBolhasInfoObjeto, derivada de GuiInfoObjeto.
/// <para>Responsável por mostrar dados referentes aos objetos do Bolhas para o usuário.</para>
/// </summary>
public class GuiBolhasInfoObjeto : GuiInfoObjeto
{

    public override void OnGUI()
    {
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 270, 120));
            posicaoy = 0;
            GUI.TextField(new Rect(10, posicaoy, 260, 20), "Objeto = " + dadosdoponto.personagem);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 260, 20), "X = " + dadosdoponto.x_log);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 260, 20), "Y = " + dadosdoponto.y_log);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 260, 20), "Tempo = " + dadosdoponto.tempo);
            posicaoy += 20;
            if (dadosdoponto.personagem == "Mouse")
            {
                GUI.TextField(new Rect(10, posicaoy, 260, 20), "O Que Fez = " + dadosdoponto.acao_dele_ou_nele);
            } else
            {
                GUI.TextField(new Rect(10, posicaoy, 260, 20), "O Que Fizeram Com Ele = " + dadosdoponto.acao_dele_ou_nele);
                posicaoy += 20;
                GUI.TextField(new Rect(10, posicaoy, 260, 20), "Quem Criou = " + dadosdoponto.quem_criou);
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