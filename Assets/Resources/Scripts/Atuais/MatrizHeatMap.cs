using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Basicas;

public class MatrizHeatMap {

    int[,] matriz;
    public int x = 800;
    public int y = 600;
    public int textx = 800;
    public int texty = 600;
    Dictionary<int, Color> cores;
    Color32[] arraydecores;
    ArrayList numerosdiferentes;
    int maiornumero;
    public Texture2D heatmap;
    public Color corminima = Color.blue;
    Color corum;
    public Color cormaxima = Color.red;
    Pintar pintar;


    void Awake () {

    }

    void Start()
    {
        Acorda();
    }

    public MatrizHeatMap()
    {
        Acorda();
    }

    public void Acorda()
    {
        matriz = new int[x, y];
        cores = new Dictionary<int, Color>();
        numerosdiferentes = new ArrayList();
        heatmap = new Texture2D(textx, texty);
        heatmap.filterMode = FilterMode.Point;
        maiornumero = 0;
        pintar = new Pintar();
        corum = new Color();
        corum.r = (cormaxima.r - corminima.r) / 2;
        corum.g = (cormaxima.g - corminima.g) / 2;
        corum.b = (cormaxima.b - corminima.b) / 2;
        corum.a = (cormaxima.a - corminima.a) / 2;
        corum = corum + corminima;
    }

    public void ReadPoints(BancoDeDadosFIT bdfit, int personagem = 0)
    {
        for (int i = 0; i < bdfit.GetQuantidadeDeEntradas(); i++)
        {
            if (personagem != 0)
            {
                if (bdfit.GetPersonagem(i) == personagem) matriz[bdfit.GetGridX(i) / 32, bdfit.GetGridY(i) / 32] += 1;
            } else matriz[bdfit.GetGridX(i) / 32, bdfit.GetGridY(i) / 32] += 1;
        }
    }

    public void AllTheDifferentPoints()
    {
        int cx = 0, cy = 0;

        for (int i = 0; i < x * y; i++)
        {
            if ((numerosdiferentes.BinarySearch(matriz[cx, cy]) > numerosdiferentes.Count) ||
                    (numerosdiferentes.BinarySearch(matriz[cx, cy]) < 0))
            {
                numerosdiferentes.Add(matriz[cx, cy]);

                //Necessário pois o Sort funciona dentro da condição prévia do ArrayList estar ordenado.
                numerosdiferentes.Sort();

                if (matriz[cx, cy] > maiornumero) maiornumero = matriz[cx, cy];
            }
            cx++; if (cx > x-1) { cx = 0; cy++; }
        }
    }

    public void OrganizePoints()
    {
        numerosdiferentes.Sort();
    }

    public int HowManyPoints()
    {
        return numerosdiferentes.Count;
    }

    public void FillingTheDictionary()
    {
        float transicaodecorr;
        float transicaodecorg;
        float transicaodecorb;
        float transicaodecora;

        transicaodecorr = (cormaxima.r - corum.r) / HowManyPoints();
        transicaodecorg = (cormaxima.g - corum.g) / HowManyPoints();
        transicaodecorb = (cormaxima.b - corum.b) / HowManyPoints();
        transicaodecora = (cormaxima.a - corum.a) / HowManyPoints();


        for (int i = 0; i < numerosdiferentes.Count; i++)
        {
            Debug.Log(numerosdiferentes[i]);
        }
            
        for (int i = 0; i < numerosdiferentes.Count; i++)
        {
            Color cordonumero;
            if (i == 0)
            {
                cordonumero = corminima;
            } else {
            
                cordonumero = new Color(corum.r + transicaodecorr * (i + 1),
                                          corum.g + transicaodecorg * (i + 1),
                                          corum.b + transicaodecorb * (i + 1),
                                          corum.a + transicaodecora * (i + 1));
            }
            
            cores.Add((int)numerosdiferentes[i], cordonumero);
        }
    }

    public void PreencherOArrayOQueÉMUITOLENTO()
    {
        arraydecores = new Color32[x * y];
        int px = 0;
        int py = 0;
        Debug.Log(x + " " + y);

        for(int i = 0; i < x * y; i++)
        {
            arraydecores[i] = cores[matriz[InverterXPraDesenho(px), py]];
            Debug.Log(px + " - " + py + " - " + arraydecores[i]);
            px++;
            if (px >= x) {
                py++;
                px = 0;
            }
            
        }


    }

    public void FillingTheHeatmapSlow()
    {

        pintar.SetPixelsEmTodaTextura(heatmap, arraydecores);

        heatmap.Apply();

    }

    public void FillingTheHeatmap()
    {

        heatmap = pintar.SetPixelsEmTextura(heatmap, 0, 0, textx, texty, corminima);

        int cx = 0, cy = 0;

        for (int i = 0; i < x * y; i++)
        {
            if (matriz[cx, cy] != 0) PintarPosicao(cx, cy, cores[matriz[cx, cy]]);
            cx++; if (cx > x - 1) { cx = 0; cy++; }
        }

        heatmap.Apply();
    }

    public void PintarPosicao(int coordx, int coordy, Color cor)
    {
        coordx = InverterXPraDesenho(coordx);
        heatmap = pintar.SetPixelsEmTextura(heatmap, textx / x * coordx, texty / y * coordy, textx / x, texty / y, cor);
    }

    int InverterXPraDesenho(int velhox)
    {
        return x - 1 - velhox;
    }

    public Dictionary<int, Color> GetDicionarioDeCores()
    {
        return cores;
    }
}
