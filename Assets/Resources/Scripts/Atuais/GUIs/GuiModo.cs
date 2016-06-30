using UnityEngine;
using System.Collections;

/// <summary>
/// Classe responsável por mostrar qual modo de visualização está sendo visto pelo usuário.
/// </summary>
public class GuiModo : GuiPadrao2
{

    bool gambiarra;

    protected string texto;
    protected string instrucoes;
    protected int posicaox;

    public GuiModo()
    {
        gambiarra = true;
        posx = Screen.width - 250;
        posy = 0;
        texto = "";
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
            GUI.BeginGroup(new Rect(posx, posy, 250, 390));

            posicaox = 0;
            GUI.Label(new Rect(10, posicaox, 250, 20), "Modo: " + texto, "textfield");
            posicaox += 20;
            GUI.Label(new Rect(10, posicaox, 250, 370), instrucoes, "textfield");

            GUI.EndGroup();
        }
    }

    public override bool MudarTexto(string novotexto) {

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
            gambiarra = false;
        }
        posx = Screen.width - 250;
    }
}
