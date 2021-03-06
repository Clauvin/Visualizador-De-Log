﻿using UnityEngine;

/// <summary>
/// Classe GuiFITInfoObjeto, derivada de GuiInfoObjeto.
/// <para>Responsável por mostrar dados referentes aos objetos do FIT para o usuário.</para>
/// </summary>
public class GuiFITInfoObjeto : GuiInfoObjeto
{
    public override void OnGUI()
    {
        //Personagem, X, Y, Tempo, Tempo Do Servidor, Jogador, ID do Jogador, Modo de Jogo e Instante Mostrado Em Câmera.
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 220, 180));
            posicaoy = 0;
            GUI.TextField(new Rect(10, posicaoy, 150, 20), "Nivel = " + dadosdoponto.nivel);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 150, 20), "Personagem = " + dadosdoponto.nome_do_objeto);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 75, 20), "X = " + dadosdoponto.x_log / 32);
            GUI.TextField(new Rect(85, posicaoy, 75, 20), "Y = " + dadosdoponto.y_log / 32);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 150, 20), "Instante = " + dadosdoponto.instante_em_camera);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 150, 20), "Tempo = " + dadosdoponto.tempo);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 150, 20), "do Servidor = " + dadosdoponto.tempo_do_servidor);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 150, 20), "Jogador = " + dadosdoponto.qual_jogador);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 150, 20), "ID do Jogador = " + dadosdoponto.id_do_jogador);

            

            GUI.EndGroup();
        }
    }

    void Awake()
    {
        dadosdoponto = null;
        posx = 0;
        posy = 0;
        texto = "";
        revelado = false;
    }

}
