using UnityEngine;
using System.Collections;

/// <summary>
/// Classe responsável por definir a tela inicial do programa.
/// </summary>
public class GuiTelaInicial : GuiPadrao2
{
    GUIStyle estilotitulotelainicial;
    GUIStyle estilobotoestelainicial;
    private int posicaox;
    private int qualbotao = -1;
    private int resultado = -1;                          

    private bool creditos = false;
    
    public void set_F() {
        resultado = 0;
    }
    public void set_B()
    {
        resultado = 1;
    }
    public void set_C()
    {
        resultado = 2;
    }
    public override void OnGUI()
    {
        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

        posicaox = 0;
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), string.Empty);
        GUI.Box(new Rect(Screen.width / 4, Screen.height / 6, Screen.width / 2, Screen.height / 2), "Visualizador de Logs\n\n" +
            "F!T e Bolhas\n\n" + "Protótipo versão 05/04/2016", estilotitulotelainicial); 

        switch (resultado)
        {
            //Vai para o loading do FIT
            case 0:
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                break;
            //Vai para o loading do Bolhas
            case 1:
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                break;
            //Abre créditos
            case 2:
                creditos = true;
                break;
            //Fecha o programa
            case 3:
                Application.Quit();
                break;
            default:
                break;
        }
        
        resultado = -1;

        GUI.EndGroup();

        if (creditos)
        {
            GUI.BeginGroup(new Rect(Screen.width * 0.4f, Screen.height / 4, Screen.width * 0.2f, Screen.height * 0.3f));

            GUI.Box(new Rect(0, 0, Screen.width * 0.2f, Screen.height * 0.3f), "Créditos\n\n" +
                "Cláuvin Erlan José\n da Costa Curty de Almeida\n\n" +
                "e Contribuidores do GitHub:\n" +
                "adicionar link.");

            if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.25f, Screen.width * 0.1f, Screen.height * 0.05f),
                "Fechar")){

                creditos = false;

            }

            GUI.EndGroup();
        }
    }

    // Use this for initialization
    void Start () {
        estilotitulotelainicial = new GUIStyle();
        estilotitulotelainicial.alignment = TextAnchor.MiddleCenter;
        estilotitulotelainicial.font = Font.CreateDynamicFontFromOSFont("Verdana", 40);

        estilobotoestelainicial = new GUIStyle("box");
        estilobotoestelainicial.alignment = TextAnchor.MiddleCenter;
        estilobotoestelainicial.font = Font.CreateDynamicFontFromOSFont("Verdana", 10);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
