using UnityEngine;
using System.Collections;
using Basicas;

/// <summary>
/// Classe responsável por definir a tela inicial do programa.
/// </summary>
public class GuiFITSelecaoDeJogadores : GuiPadrao2
{

    GUIStyle estilo;

    public override void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width - 200, Screen.height - 290, 200, 110));

        GUI.Box(new Rect(0, 0, 200, 100), "", estilo);

        GUI.Label(new Rect(10, 10, 180, 20), "Visualização de");

        GUI.Button(new Rect(10, 30, 180, 20), GetComponent<NovoLeitor2>().
                                               objs_jogadores_fit.GetObjetosDeUmJogadorFIT(0).bd_fit.GetNomeDoJogador(0));

        if (GetComponent<NovoLeitor2>().objs_jogadores_fit.obj_jogadores_fit.Count > 1)
        {

            GUI.Button(new Rect(10, 50, 180, 20), GetComponent<NovoLeitor2>().
                                               objs_jogadores_fit.GetObjetosDeUmJogadorFIT(0).bd_fit.GetNomeDoJogador(0) +
                                               " e " +
                                               GetComponent<NovoLeitor2>().
                                               objs_jogadores_fit.GetObjetosDeUmJogadorFIT(1).bd_fit.GetNomeDoJogador(0));

            GUI.Button(new Rect(10, 70, 180, 20), GetComponent<NovoLeitor2>().
                                               objs_jogadores_fit.GetObjetosDeUmJogadorFIT(1).bd_fit.GetNomeDoJogador(0));

        }

        GUI.EndGroup();

    }

    // Use this for initialization
    void Start()
    {
        estilo = new GUIStyle("box");
        estilo.alignment = TextAnchor.MiddleCenter;
        estilo.font = Font.CreateDynamicFontFromOSFont("Verdana", 10);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
