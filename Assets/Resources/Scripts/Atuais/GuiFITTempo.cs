using UnityEngine;
using System.Collections;

public class GuiFITTempo : GuiTempo
{

    public GuiFITTempo()
    {
        gambiarra = true;
        posy = 80;
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
                GetComponent<NovoLeitor2>().GetUltimoTempoFIT(), "textfield");
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

    // Use this for initialization
    public void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gambiarra)
        {
            posinicialcamera = GetComponent<Camera>().transform.position.y;
            gambiarra = false;
        }

        postempofloat = (posinicialcamera - GetComponent<Camera>().transform.position.y) / 20;
        if (GetComponent<Controlador>().modo == "Um Frame De Cada Vez em 2D")
        {
            postempofloat += 0.25f;
        }
        postempo = (int)postempofloat;
        if (postempo < 0) postempo = 0;
    }
}
