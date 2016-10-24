using UnityEngine;
using System.Collections;

/// <summary>
/// Classe GuiFITTempo, derivada de GuiTempo. 
/// <para>Responsável por passar e receber do usuário informações referentes ao sistema de passagem automática
/// dos objetos em relação ao tempo, quando visualizando dados do FIT.</para>
/// </summary>
public class GuiFITTempo : GuiTempo
{
    public GuiFITTempo()
    {
        gambiarra = true;
        posy = 160;
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

            GUI.Label(new Rect(10, 0, 150, 40), "Instante Mostrado\n" + "em Câmera: " + pos_tempo + " de " +
                QualEOUltimoInstante(), "textfield");

            if (mostra_auto_mode)
            {
                if (GetComponent<Controlador>().GetAutoMode()) GUI.TextField(new Rect(10, 40, 150, 20), "Modo Automático ativado");
                GUI.Label(new Rect(10, 60, 150, 20), "Pular para Posição", "textfield");
                string_Para_Editar = GUI.TextField(new Rect(10, 80, 150, 20), string_Para_Editar);

                GUI.Label(new Rect(10, 100, 150, 40), "Começo de Modo\nAutomático Customizado", "textfield");
                auto_custom_1 = GUI.TextField(new Rect(10, 140, 150, 20), auto_custom_1);
                GUI.Label(new Rect(10, 160, 150, 40), "Fim de Modo\nAutomático Customizado", "textfield");
                auto_custom_2 = GUI.TextField(new Rect(10, 200, 150, 20), auto_custom_2);
                if (GetComponent<Controlador>().GetAutoMode())
                {
                    GUI.TextField(new Rect(10, 220, 150, 20), "Modo Automático Customizado ativado");
                }
            }

            GUI.EndGroup();

        }
    }
    
    void Awake()
    {

    }

}
