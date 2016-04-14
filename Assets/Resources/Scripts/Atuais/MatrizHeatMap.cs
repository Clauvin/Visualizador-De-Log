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

    public void ReadPointsFIT(BancoDeDadosFIT bdfit, int personagem = 0)
    {
        for (int i = 0; i < bdfit.GetQuantidadeDeEntradas(); i++)
        {
            if (personagem != 0)
            {
                if (bdfit.GetPersonagem(i) == personagem) matriz[bdfit.GetGridX(i) / 32, bdfit.GetGridY(i) / 32] += 1;
            } else matriz[bdfit.GetGridX(i) / 32, bdfit.GetGridY(i) / 32] += 1;
        }
    }

    public void ReadPointsBolhas(BancoDeDadosBolhas bdbolhas, string qualobjeto)
    {
        int contagem = 0;
        for (int i = 0; i < bdbolhas.GetQuantidadeDeEntradas(); i++)
        {
            if (qualobjeto == "Todos")
            {
                ImagemCompletaPraMatriz(bdbolhas, i);
                contagem++;
            } else {
                if (qualobjeto == "Mouse")
                {
                    if (bdbolhas.GetMouseOuObjeto(i) == qualobjeto)
                    {
                        matriz[bdbolhas.GetCoordenadaX(i), bdbolhas.GetCoordenadaY(i)] += 1;
                        contagem++;
                    }
                }
                else
                {
                    if (bdbolhas.GetQualObjeto(i) == qualobjeto) ImagemCompletaPraMatriz(bdbolhas, i);
                    contagem++;
                }
            }
        }
    }

    //testada. Não funcionando completamente por alguma razão.
    private void ImagemCompletaPraMatriz(BancoDeDadosBolhas bdbolhas, int i)
    {
        //vj e vk são os valores a serem usados inicialmente nos for que passam por toda a textura a ser mostrada no heatmap.
        int vj = 0, vk = 0;

        string qual = bdbolhas.GetQualObjeto(i);

        Texture2D textura = UnityEngine.Object.FindObjectOfType<NovoLeitor2>().texturas.Get(qual);
        int px = bdbolhas.GetCoordenadaX(i);
        int py = bdbolhas.GetCoordenadaY(i);

        //limitx e limity existem para calcular se o objeto a ser desenhado futuramente vai além da tela ou não.
        int texture_length = textura.width; int limitx = px + texture_length;
        int texture_height = textura.height; int limity = py + texture_height;

        //Se limitx > x, onde x é o tamanho da tela, o limitx é reduzido no quanto ele ultrapassa a tela.
        //Se não, limitx vira alargura da textura, e o mesmo vale pra y.
        //Esse é o teste para ultrapassagens à direita e abaixo da tela.
        if (limitx > x) { limitx = texture_length - (limitx - x); } else { limitx = texture_length; }
        if (limity > y) { limity = texture_height - (limity - y); } else { limity = texture_height; }

        //Se px ou py < 0, ou seja, os objetos estão à esquerda ou acima do heatmap, é necessário começar a desenhá-los
        //  APENAS a partir de onde começam a aparecer em tela, daí j = -px e k = -py;
        if (px < 0) { vj = -px; } 
        if (py < 0) { vk = -py; }

        //Para cada pixel da textura, checar se a transparência
        //Se sim, adiciona um ponto para a matriz de desenho.
        for (int j = vj ; j < limitx; j++)
        {
            for (int k = vk ; k < limity; k++)
            {
                try {
                    if (textura.GetPixel(j, k).a != 0) matriz[px + j, py + k] += 1;
                } catch(System.IndexOutOfRangeException e)
                {
                    Debug.Log("px e j " + px + " " + j + " - " + "py e k " + py + " " + k);
                }
            }
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

        Color cordonumero;
        for (int i = 0; i < numerosdiferentes.Count; i++)
        {
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

    public void DebugMatrizNoOlho()
    {
        int count;
        for (int i = 0; i < y; i++)
        {
            count = 0;
            for (int j = 0; j < x; j++)
            {
                if (matriz[j, i] != 0) count++;
            }
        }
    }

    public void PreencherOArrayOQueÉMUITOLENTO()
    {
        arraydecores = new Color32[x * y];
        int px = 0;
        int py = 0;

        for(int i = 0; i < x * y; i++)
        {
            arraydecores[i] = cores[matriz[InverterXPraDesenho(px), py]];
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
        int pintura = 0;
        heatmap = pintar.SetPixelsEmTextura(heatmap, 0, 0, textx, texty, corminima);

        int cx = 0, cy = 0;

        for (int i = 0; i < x * y; i++)
        {
            if (matriz[cx, cy] != 0) { PintarPosicao(cx, cy, cores[matriz[cx, cy]]); pintura++; }
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
