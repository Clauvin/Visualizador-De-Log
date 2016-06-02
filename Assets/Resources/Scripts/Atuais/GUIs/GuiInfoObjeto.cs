using UnityEngine;
using System.Collections;

/// <summary>
/// Classe de onde derivam GuiFITInfoObjeto e GuiBolhasInfoObjeto.
/// <para>Responsável por mostrar ao usuário informações sobre o objeto clicado.</para>
/// </summary>
public class GuiInfoObjeto : GuiPadrao2
{
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
        posx = 0;
        posy = 0;
        texto = "";
        revelado = false;
    }

    // Use this for initialization
    void Start()
    {

    }


}
