using UnityEngine;
using System.Collections;

public class GuiFITAssinatura : GuiPadrao2
{

    bool gambiarra;

    protected string texto;
    protected string instrucoes;
    private int posicaox;

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
            GUI.BeginGroup(new Rect(posx, posy, 200, 40));

            posicaox = 0;
            GUI.TextField(new Rect(0, posicaox, 200, 40), texto);

            GUI.EndGroup();
        }
    }

    public override bool MudarTexto(string novotexto)
    {

        texto = novotexto;
        return true;

    }

    public override string GetTexto() { return texto; }

    void Awake()
    {
        gambiarra = true;
        posx = 0;
        posy = Screen.height - 40;
        texto = "";
        revelado = true;
        texto = "Feito por Cláuvin Almeida\nclauvin_almeida@hotmail.com";
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gambiarra)
        {
            gambiarra = false;
        }
        posy = Screen.height - 40;
    }
}
