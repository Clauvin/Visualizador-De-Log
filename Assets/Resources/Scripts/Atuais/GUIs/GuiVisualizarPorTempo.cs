using UnityEngine;
using System.Collections;
using System;

public class GuiVisualizarPorTempo : GuiPadrao2 {

    string tempominimo = "Apenas >= 0 aqui.";
    string tempomaximo = "Apenas >= 0 aqui.";
    int visivel_ou_invisivel = 1;
    public string[] o_que_escrever_nos_botoes;

    void MudaVisibilidade()
    {
        if (visivel_ou_invisivel == 1) visivel_ou_invisivel = 0;
        else visivel_ou_invisivel = 1;
    }

    public override void OnGUI()
    {
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 290, 120));
            GUI.TextField(new Rect(0, 0, 290, 20), "Invisibilidade de Espaço de Tempo");
            GUI.Label(new Rect(0, 20, 210, 20), "Tempo Mínimo", "textfield");
            tempominimo = GUI.TextField(new Rect(0, 40, 210, 20), tempominimo);
            GUI.Label(new Rect(0, 60, 210, 20), "Tempo Máximo", "textfield");
            tempomaximo = GUI.TextField(new Rect(0, 80, 210, 20), tempomaximo);
            if (GUI.Button(new Rect(210, 20, 80, 80), o_que_escrever_nos_botoes[visivel_ou_invisivel]))
            {
                if (visivel_ou_invisivel == 1)
                {
                    GetComponent<Controlador>().DeixarObjetosEmEspacoDeTempoInvisiveisEIninteragiveis(
                        Int32.Parse(tempominimo), Int32.Parse(tempomaximo));
                    MudaVisibilidade();
                }
                else
                {
                    GetComponent<Controlador>().DeixarObjetosEmEspacoDeTempoVisiveisEInteragiveis(
                        Int32.Parse(tempominimo), Int32.Parse(tempomaximo));
                    MudaVisibilidade();
                }
            }
            GUI.EndGroup();
        }
    }

    // Use this for initialization
    void Start () {

        revelado = true;

        posx = Screen.width - 290;
        posy = 400;

        o_que_escrever_nos_botoes = new string[2];
        o_que_escrever_nos_botoes[0] = "Apertar\npara\nvisível";
        o_que_escrever_nos_botoes[1] = "Apertar\npara\ninvisível";
    }
	
	// Update is called once per frame
	void Update () {
        posx = Screen.width - 290;
    }
}
