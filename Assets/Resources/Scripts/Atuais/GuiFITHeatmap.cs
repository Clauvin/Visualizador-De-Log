using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Basicas;

public class GuiFITHeatmap : GuiPadrao2
{

    bool gambiarra;

    protected string texto;
    protected string instrucoes;
    private int posicaoy;
    float posinicialcamera;
    float postempofloat;
    int postempo;
    int quantdeheatmaps;
    Dictionary<int, Color> dicionarioheatmap;
    List<DadosGUIHashMap> dados;

    Rect posicaomovel;

    public GuiFITHeatmap()
    {
        gambiarra = true;
        posy = 120;
        texto = "Significado das Cores";
        revelado = false;
        dados = new List<DadosGUIHashMap>();
        
    }

    public override void OnGUI()
    {
        int numero;

        if ((revelado) && (!gambiarra))
        {
            int qualheatmap = GetComponent<Controlador>().QualHeatmapMostra();
            GUI.BeginGroup(new Rect(posx, posy, 170, 20 *
                (dados[qualheatmap].numerosdecor.Count + 3)));

            posicaoy = 0;

            GUI.Box(new Rect(10, posicaoy, 170, 40 + 20 * dados[qualheatmap].numerosdecor.Count), string.Empty);

            if (qualheatmap == 0)
            {
                GUI.TextField(new Rect(10, posicaoy, 170, 20), "Cores e Quantos Objetos");
                posicaoy += 20;
            }
            else
            {
                GUI.TextField(new Rect(10, posicaoy, 170, 40), "Cores e Quantos Objetos\n - Jogador " + qualheatmap);
                posicaoy += 40;
            }

            //lembrando, uma das cores já foi
            for (int i = 0; i < dados[qualheatmap].numerosdecor.Count; i++)
            {
                GUI.Box(new Rect(10, posicaoy, 20, 20), dados[qualheatmap].texturasdecor[i]);
                numero = dados[qualheatmap].numerosdecor[i];
                GUI.TextField(new Rect(35, posicaoy, 145, 20), numero.ToString());
                posicaoy += 20;
            }


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
            quantdeheatmaps = GetComponent<NovoLeitor2>().GetQuantHeatmaps();
            for (int i = 0; i < quantdeheatmaps; i++) {

                DadosGUIHashMap infoheatmap = new DadosGUIHashMap();

                dicionarioheatmap = GetComponent<NovoLeitor2>().GetMatrizHeatmap(i).GetDicionarioDeCores();
                infoheatmap.numerosdecor.AddRange(dicionarioheatmap.Keys);
                infoheatmap.cores.AddRange(dicionarioheatmap.Values);

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
                    infoheatmap.texturasdecor.Add(textura);
                }

                dados.Add(infoheatmap);

            }

            gambiarra = false;
        }

    }
}