using UnityEngine;
using System.Collections;

public class GuiTempo : GuiPadrao2
{

    protected bool gambiarra;

    protected string texto;
    protected string instrucoes;

    protected string stringParaEditar;
    protected string autocustom1;
    protected string autocustom2;

    protected int posicaoy;
    protected float posinicialcamera;
    protected float postempofloat;
    protected int postempo;
    

    public GuiTempo()
    {
        gambiarra = true;
        posy = 80;
        texto = "Tempo = ";
        revelado = false;
        stringParaEditar = "Apenas >= 0 aqui.";
        autocustom1 = "Apenas >= 0 aqui.";
        autocustom2 = "Apenas >= 0 aqui.";
    }

    public void PegarQualModo(string modo)
    {

        texto = modo;

    }

    public void PegarInstrucoes(string instrucao)
    {

        instrucoes = instrucao;

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

    public string GetStringEditavel() { return stringParaEditar; }

    public void SetStringEditavel(string editada) { stringParaEditar = editada; }

    public string GetAutoCustomComecoEditavel() { return autocustom1; }

    public void SetAutoCustomComecoEditavel(string editada) { autocustom1 = editada; }

    public string GetAutoCustomFinalEditavel() { return autocustom2; }

    public void SetAutoCustomFinalEditavel(string editada) { autocustom2 = editada; }

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
