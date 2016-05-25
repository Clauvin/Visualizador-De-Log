using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Basicas;


/// <summary>
/// Classe GuiHeatmap, de onde derivam as classes GuiFITHeatmap e GuiBolhasHeatmap.
/// <para>É a classe responsável por mostrar as cores usadas nos heatmaps e quanto objetos naquele espaço
/// a cor representa.</para>
/// </summary>
public class GuiHeatmap : GuiPadrao2
{
    public bool gambiarra;

    protected string texto;
    protected string instrucoes;
    protected int posicao_y;
    public float pos_inicial_camera;
    public float pos_tempo_float;
    public int pos_tempo;
    public int quant_de_heatmaps;
    public Dictionary<int, Color> dicionario_heatmap;
    public List<DadosGUIHashMap> dados;
    public string[] lista_de_objetos;

    Rect posicao_movel;

    public GuiHeatmap()
    {
        gambiarra = true;
        posy = 120;
        texto = "Significado das Cores";
        revelado = false;
        dados = new List<DadosGUIHashMap>();

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

    // Update is called once per frame
    // Todo esse código que só roda uma vez num update foi necessário porquê
    // ele pega dados de locais que não estão inicializados no momento em que Start() roda.
    void Update()
    {
        if (gambiarra)
        {
            quant_de_heatmaps = GetComponent<NovoLeitor2>().GetQuantHeatmaps();
            for (int i = 0; i < quant_de_heatmaps; i++)
            {
                //carrega os dados da GUI referente à lista de cores e quantos objetos representados por cor
                DadosGUIHashMap infoheatmap = new DadosGUIHashMap();

                dicionario_heatmap = GetComponent<NovoLeitor2>().GetMatrizHeatmap(i).GetDicionarioDeCores();
                infoheatmap.numeros_de_cor.AddRange(dicionario_heatmap.Keys);
                infoheatmap.cores.AddRange(dicionario_heatmap.Values);

                for (int h = 0; h < infoheatmap.cores.Count; h++)
                {
                    Texture2D textura = new Texture2D(20, 20);
                    for (int j = 0; j < textura.width; j++)
                    {
                        for (int k = 0; k < textura.height; k++)
                        {
                            textura.SetPixel(j, k, infoheatmap.cores[h]);
                        }
                    }
                    textura.Apply();
                    infoheatmap.texturas_de_cor.Add(textura);
                }

                dados.Add(infoheatmap);

            }

            gambiarra = false;
        }

    }
}
