using UnityEngine;
using System.Collections;

/// <summary>
/// Classe GuiFITEscolhaDeJogadores, derivada de GuiPadrao2.
/// <para>Responsável por mostrar dados referentes à escolha de jogadores para o cenário.</para>
/// </summary>
public class GuiFITEscolhaDeJogadores : GuiPadrao2
{

    public bool toggleBool1 = true;
    public bool toggleBool2 = true;

    public override void OnGUI() {

        toggleBool1 = GUI.Toggle(new Rect(25, 25, 100, 30), toggleBool, "Toggle");
        toggleBool2 = GUI.Toggle(new Rect(25, 55, 100, 30), toggleBool, "Toggle");

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
