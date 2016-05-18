using UnityEngine;
using System.Collections;

/// <summary>
/// Classe GuiBolhasTempo, derivada de GuiTempo. 
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
        stringParaEditar = "Apenas >= 0 aqui.";
        autocustom1 = "Apenas >= 0 aqui.";
        autocustom2 = "Apenas >= 0 aqui.";
    }

    public override void OnGUI()
    {
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 320, 200));

            posicaoy = 0;

            GUI.Label(new Rect(10, 0, 210, 40), "Tempo Mostrado em Câmera: " + postempo + " de \n" +
                GetComponent<NovoLeitor2>().GetUltimoTempoBolhas(), "textfield");
            if (GetComponent<Controlador>().GetAutoMode()) GUI.TextField(new Rect(10, 40, 210, 20), "Modo Automático ativado");
            GUI.Label(new Rect(10, 60, 210, 20), "Pular para Posição", "textfield");
            stringParaEditar = GUI.TextField(new Rect(10, 80, 210, 20), stringParaEditar);

            GUI.Label(new Rect(10, 100, 210, 20), "Começo de Modo Automático Customizado", "textfield");
            autocustom1 = GUI.TextField(new Rect(10, 120, 210, 20), autocustom1);
            GUI.Label(new Rect(10, 140, 210, 20), "Fim de Modo Automático Customizado", "textfield");
            autocustom2 = GUI.TextField(new Rect(10, 160, 210, 20), autocustom2);
            if (GetComponent<Controlador>().GetAutoMode())
            {
                GUI.TextField(new Rect(10, 180, 210, 20), "Modo Automático Customizado ativado");
            }
            GUI.EndGroup();

        }
    }

    void Awake()
    {

    }
}
