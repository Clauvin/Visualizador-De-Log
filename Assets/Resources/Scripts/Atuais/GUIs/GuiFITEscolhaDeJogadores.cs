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

    private Vector2 scrollViewVector = Vector2.zero;

    public override void OnGUI() {

        // Begin the ScrollView
        scrollViewVector = GUI.BeginScrollView(new Rect(25, 25, 200, 300), scrollViewVector, new Rect(0, 0, 180, 600));

        GUI.Label(new Rect(0, 0, 200, 600), "", "textarea");

        toggleBool1 = GUI.Toggle(new Rect(0, 0, 100, 30), toggleBool1, "João");
        toggleBool2 = GUI.Toggle(new Rect(0, 30, 100, 30), toggleBool2, "José");

        // End the ScrollView
        GUI.EndScrollView();

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
