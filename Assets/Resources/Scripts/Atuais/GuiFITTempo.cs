using UnityEngine;
using System.Collections;

public class GuiFITTempo : GuiPadrao2
{

    bool gambiarra;

    protected string texto;
    protected string instrucoes;
    private int posicaoy;
    float posinicialcamera;
    float postempofloat;
    int postempo;

    public GuiFITTempo()
    {
        gambiarra = true;
        posy = 80;
        texto = "Tempo = ";
        revelado = false;
    }

    public void PegarQualModo(string modo)
    {

        texto = modo;

    }

    public void PegarInstrucoes(string instrucao)
    {

        instrucoes = instrucao;

    }

    public override void OnGUI()
    {
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 220, 40));

            posicaoy = 0;

            GUI.TextField(new Rect(10, 0, 210, 20), "Tempo Mostrado em Câmera: " + postempo);
            if (GetComponent<Controlador>().GetAutoMode()) GUI.TextField(new Rect(10, 20, 210, 20), "Modo Automático ativado");

            GUI.EndGroup();
        }
    }

    public override bool MudarTexto(string novotexto)
    {

        texto = novotexto;
        return true;

    }

    public bool MudarInstrucoes(string instrucao)
    {
        instrucoes = instrucao;
        return true;
    }

    public override string GetTexto() { return texto; }

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

        postempofloat = (posinicialcamera - GetComponent<Camera>().transform.position.y)/20;
        if (GetComponent<Controlador>().modo == "Um Frame De Cada Vez em 2D")
        {
            postempofloat += 0.25f;
        }
        postempo = (int)postempofloat;
        if (postempo < 0) postempo = 0;
    }
}
