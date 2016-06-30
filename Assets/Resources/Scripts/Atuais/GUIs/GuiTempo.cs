using UnityEngine;
using System.Collections;

/// <summary>
/// Classe de onde derivam GuiFITTempo e GuiBolhasTempo. 
/// <para>Responsável por passar e receber do usuário informações referentes ao sistema de passagem automática
/// dos objetos em relação ao tempo.</para>
/// </summary>
public class GuiTempo : GuiPadrao2
{

    protected bool gambiarra;

    protected string texto;
    protected string instrucoes;

    protected bool mostra_auto_mode;

    protected string string_Para_Editar;
    protected string auto_custom_1;
    protected string auto_custom_2;

    protected int posicao_y;
    protected float pos_inicial_camera;
    protected float pos_tempo_float;
    protected int pos_tempo;
    protected int tempo_inicial;


    public GuiTempo()
    {
        gambiarra = true;
        mostra_auto_mode = true;
        posy = 80;
        texto = "Tempo = ";
        revelado = false;
        string_Para_Editar = "Apenas >= 0 aqui.";
        auto_custom_1 = "Apenas >= 0 aqui.";
        auto_custom_2 = "Apenas >= 0 aqui.";
    }

    public override void OnGUI()
    {
        if (revelado)
        {
            GUI.BeginGroup(new Rect(posx, posy, 320, 240));

            posicao_y = 0;

            GUI.Label(new Rect(10, 0, 150, 40), "Tempo Mostrado\n" + "em Câmera: " + pos_tempo + " de ", "textfield");
            if (GetComponent<Controlador>().GetAutoMode()) GUI.TextField(new Rect(10, 40, 150, 20), "Modo Automático ativado");
            GUI.Label(new Rect(10, 60, 150, 20), "Pular para Posição", "textfield");
            string_Para_Editar = GUI.TextField(new Rect(10, 80, 210, 20), string_Para_Editar);

            GUI.Label(new Rect(10, 100, 210, 40), "Começo de Modo\nAutomático Customizado", "textfield");
            auto_custom_1 = GUI.TextField(new Rect(10, 140, 210, 20), auto_custom_1);
            GUI.Label(new Rect(10, 160, 210, 40), "Fim de Modo\nAutomático Customizado", "textfield");
            auto_custom_2 = GUI.TextField(new Rect(10, 200, 210, 20), auto_custom_2);
            if (GetComponent<Controlador>().GetAutoMode())
            {
                GUI.TextField(new Rect(10, 220, 210, 20), "Modo Automático Customizado ativado");
            }

            GUI.EndGroup();

        }

        
    }

    public void PegarQualModo(string modo)
    {

        texto = modo;

    }

    public void AtivarGuiDoAutoMode()
    {
        mostra_auto_mode = true;
    }

    public void DesativarGuiDoAutoMode()
    {
        mostra_auto_mode = false;
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

    public int GetTempo() { return pos_tempo; }

    public float GetPosicaoInicialDaCamera() { return pos_inicial_camera; }

    public string GetStringEditavel() { return string_Para_Editar; }

    public void SetStringEditavel(string editada) { string_Para_Editar = editada; }

    public string GetAutoPassagemDeTempoCustomComecoEditavel() { return auto_custom_1; }

    public void SetAutoPassagemDeTempoCustomComecoEditavel(string editada) { auto_custom_1 = editada; }

    public string GetAutoPassagemDeTempoCustomFinalEditavel() { return auto_custom_2; }

    public void SetAutoPassagemDeTempoCustomFinalEditavel(string editada) { auto_custom_2 = editada; }

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {

        tempo_inicial = GetComponent<NovoLeitor2>().GetPrimeiroTempo();

    }

    // Update is called once per frame
    void Update()
    {
        if (gambiarra)
        {
            pos_inicial_camera = GetComponent<Camera>().transform.position.y;
            gambiarra = false;
        }

        pos_tempo_float = (pos_inicial_camera - GetComponent<Camera>().transform.position.y) / 20 + tempo_inicial;
        if (GetComponent<Controlador>().modo_de_visualizacao == "Um Frame De Cada Vez em 2D")
        {
            pos_tempo_float += 0.25f;
        }
        pos_tempo = (int)pos_tempo_float;
        if (pos_tempo < 0) pos_tempo = 0;
    }


}
