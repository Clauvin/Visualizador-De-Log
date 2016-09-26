using UnityEngine;
using System.Collections;

/// <summary>
/// Classe padrão de onde outras classes de interface gráfica derivam.
/// <para>Contém funções padrões para posicionamento de GUI.</para>
/// </summary>
public class GuiPadrao2 : AbstractGui2
{

    protected bool revelado;
    protected int posx;
    protected int posy;

    public override void OnGUI() { }

    public override void MudarCoordenadasX(int x) { SetX(x); }

    public override void SetX(int x) { posx = x; }

    public override void MudarCoordenadasY(int y) { SetY(y); }

    public override void SetY(int y) { posy = y; }

    public override void MudarCoordenadas(int x, int y) { SetX(x); SetY(y); }

    public override bool MudarTexto(string novotexto) { return true; }

    public override string GetTexto() { return ""; }

    public override bool EstaRevelado()
    {

        return revelado;

    }

    public override bool EsconderGui() { revelado = false; return true; }

    public override bool RevelarGui() { revelado = true; return true; }

    // Use this for initialization
    void Start()
    {
        revelado = true;
    }

    public void MudarCenaPara_Tela_Inicial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void MudarCenaPara_Pre_Fit() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void MudarCenaPara_Pre_Bolhas() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void Fechar_Aplicacao() {
        Application.Quit();
    }

    public void MudarCenaPara_Load_Bolhas() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }

    public void MudarCenaPara_Load_Fit() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    public void MudarCenaPara_Selecao_Fit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

}
