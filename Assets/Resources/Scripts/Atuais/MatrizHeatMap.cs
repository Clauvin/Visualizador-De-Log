using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Basicas;

/// <summary>
/// Classe Heatmap, responsável por guardar os dados referentes ao que deve ser desenhado no heatmap, através de uma matriz,]
/// que cores devem ser usadas, e a textura2D do heatmap em si.
/// </summary>
public class HeatMap {

    int[,] matriz;
    public int x = 800;
    public int y = 600;
    public int text_x = 800;
    public int text_y = 600;
    Dictionary<int, Color> cores;
    Color32[] array_de_cores;
    ArrayList numeros_diferentes;
    int maior_numero;
    public Texture2D heatmap;
    public Color cor_minima = Color.blue;
    Color cor_um;
    public Color cor_maxima = Color.red;
    Pintar pintar;


    void Awake () {

    }

    void Start()
    {
        Inicializa();
    }

    public HeatMap()
    {
        Inicializa();
    }

    public void Inicializa()
    {
        matriz = new int[x, y];
        cores = new Dictionary<int, Color>();
        numeros_diferentes = new ArrayList();
        heatmap = new Texture2D(text_x, text_y);
        heatmap.filterMode = FilterMode.Point;
        maior_numero = 0;
        pintar = new Pintar();
        cor_um = new Color();
        cor_um.r = (cor_maxima.r - cor_minima.r) / 2;
        cor_um.g = (cor_maxima.g - cor_minima.g) / 2;
        cor_um.b = (cor_maxima.b - cor_minima.b) / 2;
        cor_um.a = (cor_maxima.a - cor_minima.a) / 2;
        cor_um = cor_um + cor_minima;
    }

    /// <summary>
    /// Lê quais as coordenadas ocupadas por objetos no BancoDeDadosFIT e preenche a matriz de pintura a partir delas.
    /// </summary>
    /// <param name="bdfit">Guarda todas as informações lidas no log do FIT.</param>
    /// <param name="personagem">Inteiro que representa qual personagem deve ter suas coordenadas lidas.
    /// O valor = 0 representa que todos os personagens devem ter suas coordenadas lidas, valores maiores que 0
    /// significam um personagem específico.</param>
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

    /// <summary>
    /// Lê quais as coordenadas ocupadas por objetos no BancoDeDadosBolhas e preenche a matriz de pintura a partir delas.
    /// </summary>
    /// <param name="bdbolhas">Guarda todas as informações lidas no log do Bolhas.</param>
    /// <param name="qualobjeto">String que representa qual personagem deve ter sua imagem lida.
    /// "Todos" representa que todos os tipos de objeto devem ter suas imagens lidas,
    /// Outras strings significam que apenas um tipo de objeto específico é levado em consideração.
    /// <para>ATENÇÃO: "Mouse" lê apenas uma coordenada, e não todo o desenho do mouse.</para></param>
    public void ReadPointsBolhas(BancoDeDadosBolhas bdbolhas, string qualobjeto = "Todos")
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


    /// <summary>
    /// Recebe um banco de dados do Bolhas e uma posição nesse banco de dados para passar a imagem do objeto correspondente
    /// para a matriz de pontos a serem pintados.
    /// </summary>
    /// <param name="bdbolhas">Guarda todas as informações lidas no log do Bolhas.</param>
    /// <param name="i">Posição a ser lida em bdbolhas.</param>
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
        if (py < 0) { vk = -py; limity -= py; }

        //Para cada pixel da textura, checar se a transparência
        //Se sim, adiciona um ponto para a matriz de desenho.
        if (py >= 0)
        {
            for (int j = vj; j < limitx; j++)
            {
                for (int k = vk; k < limity; k++)
                {
                    //vale lembrar: a aqui é um float, não um inteiro.
                    //py + limity - k - 1 está assim porquê a Unity tem como (x=0,y=0) 
                    //  o canto inferior esquerdo das imagens/texturas. Ou seja, y precisa ser invertido.
                    try
                    {
                        if (textura.GetPixel(j, k).a == 1) matriz[px + j, py + limity - k - 1] += 1;
                    }
                    catch (System.IndexOutOfRangeException exc)
                    {
                        //Debug.Log(exc.Message);
                        //Debug.Log(i + " - X = " + (px + j) + " Y = " + (py + limity - k - 1));
                    }

                }
            }

        //py < 0? Ahã. Eu tive que alterar a equação para resolver o caso de ter que
        //desenhar algo que está um pouco fora do desenho por cima.
        //Sinceramente, até eu(Cláuvin) estou meio confuso com esse método, então
        //simplificar ele seria algo bom pra se fazer no futuro.
        } else {
            for (int j = vj; j < limitx; j++)
            {
                for (int k = vk; k < limity; k++)
                {
                    //vale lembrar: a aqui é um float, não um inteiro.
                    //py + limity - k - 1 está assim porquê a Unity tem como (x=0,y=0) 
                    //  o canto inferior esquerdo das imagens/texturas. Ou seja, y precisa ser invertido.
                    try
                    {
                        if (textura.GetPixel(j, k-vk).a == 1) matriz[px + j, py + limity - k - 1] += 1;
                    }
                    catch (System.IndexOutOfRangeException exc)
                    {
                        //Debug.Log(exc.Message);
                        //Debug.Log(i + " - X = " + (px + j) + " Y = " + (py + limity - k - 1));
                    }

                }
            }
        }
        
    }

    /// <summary>
    /// Analisa a matriz de pontos a serem pintados e guarda a quantidade de números diferentes presentes na matriz.
    /// Esses números serão considerados para a pintura do heatmap: quanto maior o número, mais "calor" naquela coordenada.
    /// </summary>
    public void AllTheDifferentPoints()
    {
        int cx = 0, cy = 0;

        for (int i = 0; i < x * y; i++)
        {
            if ((numeros_diferentes.BinarySearch(matriz[cx, cy]) > numeros_diferentes.Count) ||
                    (numeros_diferentes.BinarySearch(matriz[cx, cy]) < 0))
            {
                numeros_diferentes.Add(matriz[cx, cy]);

                //Necessário pois o Sort funciona dentro da condição prévia do ArrayList estar ordenado.
                numeros_diferentes.Sort();

                if (matriz[cx, cy] > maior_numero) maior_numero = matriz[cx, cy];
            }
            cx++; if (cx > x-1) { cx = 0; cy++; }
        }
    }

    public void OrganizePoints()
    {
        numeros_diferentes.Sort();
    }

    public int HowManyPoints()
    {
        return numeros_diferentes.Count;
    }

    /// <summary>
    /// Analisando os diferentes números representativos de quantas vezes uma posição foi ocupada,
    /// esta função dá uma cor diferente para cada número.
    /// ATENÇÃO: talvez essa função fique melhor fazendo os cálculos de forma a usar o tamanho relativo
    /// de cada número em erelação aos outros e não a quantidade de números somente.
    /// </summary>
    public void FillingTheDictionary()
    {
        float transicaodecorr;
        float transicaodecorg;
        float transicaodecorb;
        float transicaodecora;

        transicaodecorr = (cor_maxima.r - cor_um.r) / HowManyPoints();
        transicaodecorg = (cor_maxima.g - cor_um.g) / HowManyPoints();
        transicaodecorb = (cor_maxima.b - cor_um.b) / HowManyPoints();
        transicaodecora = (cor_maxima.a - cor_um.a) / HowManyPoints();

        Color cordonumero;
        for (int i = 0; i < numeros_diferentes.Count; i++)
        {
            if (i == 0)
            {
                cordonumero = cor_minima;
            } else {
            
                cordonumero = new Color(cor_um.r + transicaodecorr * (i + 1),
                                          cor_um.g + transicaodecorg * (i + 1),
                                          cor_um.b + transicaodecorb * (i + 1),
                                          cor_um.a + transicaodecora * (i + 1));
            }
            
            cores.Add((int)numeros_diferentes[i], cordonumero);

        }
    }

    public void DebugContadorDeEspacosOcupadosPorLinhaNaMatrizDoHeatmap()
    {
        int count;
        for (int i = 0; i < y; i++)
        {
            count = 0;
            for (int j = 0; j < x; j++)
            {
                if (matriz[j, i] != 0) count++;
            }
            Debug.Log("Espacos ocupados na linha " + i + " da matriz: " + count);
        }
        
    }

    /// <summary>
    /// Essa função... ok, sinceramente, ela foi uma tentativa de encontrar uma forma mais rápida e menos custosa
    /// de pintar a textura do heatmap.
    /// Não deu certo. Mas como pode ser que tenha alguma utilidade no futuro, ela fica aqui, mas sinceramente,
    /// nao recomendo seu uso, daí o nome.
    /// </summary>
    public void PreencherOArrayOQueÉMUITOLENTO()
    {
        array_de_cores = new Color32[x * y];
        int px = 0;
        int py = 0;

        for(int i = 0; i < x * y; i++)
        {
            array_de_cores[i] = cores[matriz[InverterXPraDesenho(px), py]];
            px++;
            if (px >= x) {
                py++;
                px = 0;
            }
            
        }


    }

    /// <summary>
    /// Uma tentativa mal sucedida de achar uma forma mais rápida de pintar as texturas dos heatmaps.
    /// Uso não recomendado.
    /// </summary>
    public void FillingTheHeatmapSlow()
    {

        pintar.SetPixelsEmTodaTextura(heatmap, array_de_cores);

        heatmap.Apply();

    }

    public void PaintingTheHeatmap()
    {
        int pintura = 0;

        //pinta todo o heatmap da cor referente a zero ocupações de uma coordenada.
        heatmap = pintar.SetPixelsEmTextura(heatmap, 0, 0, text_x, text_y, cor_minima);

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
        heatmap = pintar.SetPixelsEmTextura(heatmap, text_x / x * coordx, text_y / y * coordy, text_x / x, text_y / y, cor);
    }

    /// <summary>
    /// Porquê InverterXPraDesenho? Porquê por alguma razão, a coordenada X usada em SetPixelsEmTextura
    /// pinta de forma invertida, então essa função é necessária para desinverter a coordenada.
    /// </summary>
    /// <param name="velhox"></param>
    /// <returns></returns>
    int InverterXPraDesenho(int velhox)
    {
        return x - 1 - velhox;
    }

    public Dictionary<int, Color> GetDicionarioDeCores()
    {
        return cores;
    }
}
