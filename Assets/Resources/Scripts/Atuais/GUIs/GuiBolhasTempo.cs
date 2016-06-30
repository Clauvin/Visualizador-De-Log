﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Classe derivada de GuiTempo. 
/// <para>Responsável por passar e receber do usuário informações referentes ao sistema de passagem automática
/// dos objetos em relação ao tempo, quando visualizando dados do Bolhas.</para>
/// </summary>
public class GuiBolhasTempo : GuiTempo
{

    public GuiBolhasTempo()
    {
        gambiarra = true;
        posy = 180;
        texto = "Tempo = ";
        revelado = false;
        string_Para_Editar = "Apenas >= 0 aqui.";
        auto_custom_1 = "Apenas >= 0 aqui.";
        auto_custom_2 = "Apenas >= 0 aqui.";
    }

    public override void OnGUI()
    {
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 320, 240));

            posicao_y = 0;

            GUI.Label(new Rect(10, 0, 210, 40), "Tempo Mostrado em Câmera: " + pos_tempo + " de \n" +
                GetComponent<NovoLeitor2>().GetUltimoTempoBolhas(), "textfield");

            if (mostra_auto_mode)
            {
                if (GetComponent<Controlador>().GetAutoMode()) GUI.TextField(new Rect(10, 40, 210, 20), "Modo Automático ativado");
                GUI.Label(new Rect(10, 60, 210, 20), "Pular para Posição", "textfield");
                string_Para_Editar = GUI.TextField(new Rect(10, 80, 210, 20), string_Para_Editar);

                GUI.Label(new Rect(10, 100, 210, 40), "Começo de Modo\nAutomático Customizado", "textfield");
                auto_custom_1 = GUI.TextField(new Rect(10, 140, 210, 20), auto_custom_1);
                GUI.Label(new Rect(10, 160, 210, 40), "Fim de Modo\nAutomático Customizado", "textfield");
                auto_custom_2 = GUI.TextField(new Rect(10, 200, 210, 20), auto_custom_2);
                if (GetComponent<Controlador>().GetAutoMode())
                {
                    GUI.TextField(new Rect(10, 220, 210, 20), "Modo Automático Customizado ativado");
                }
            }

            GUI.EndGroup();

        }
    }

    void Awake()
    {

    }
}
