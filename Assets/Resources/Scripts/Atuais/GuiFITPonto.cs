using UnityEngine;
using System.Collections;

public class GuiFITPonto : GuiPadrao2
{

    bool gambiarra;
    Dados dadosdoponto;

    protected string texto;
    private int posicaoy;

    public void PegarDados(Dados dados)
    {

        dadosdoponto = dados;

    }

    public override void OnGUI()
    {
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 220, 80));
            posicaoy = 0;
            GUI.TextField(new Rect(10, posicaoy, 120, 20), "Personagem = " + dadosdoponto.personagem);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 120, 20), "X = " + dadosdoponto.xlog / 32);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 120, 20), "Y = " + dadosdoponto.ylog / 32);
            posicaoy += 20;
            GUI.TextField(new Rect(10, posicaoy, 120, 20), "Tempo = " + dadosdoponto.tempo);

            GUI.EndGroup();
        }
    }

    public override bool MudarTexto(string novotexto) { return true; }

    public override string GetTexto() { return texto; }

    void Awake()
    {
        dadosdoponto = null;
        gambiarra = true;
        posx = 0;
        posy = 0;
        texto = "";
        revelado = false;
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
    }
}
