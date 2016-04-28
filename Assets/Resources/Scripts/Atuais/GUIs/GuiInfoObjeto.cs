using UnityEngine;
using System.Collections;

/// <summary>
/// Classe GuiPonto, responsável por mostrar ao usuário informações sobre
/// o objeto clicado.
/// </summary>
public class GuiInfoObjeto : GuiPadrao2
{

    protected bool gambiarra;
    protected Dados dadosdoponto;

    protected string texto;
    protected int posicaoy;

    public void PegarDados(Dados dados)
    {

        dadosdoponto = dados;

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
