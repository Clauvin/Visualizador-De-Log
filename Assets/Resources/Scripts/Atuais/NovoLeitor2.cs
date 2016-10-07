using UnityEngine;
using Basicas;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Responsável por carregar as informações necessárias para o programa funcionar e usá-las.
/// </summary>
public class NovoLeitor2 : MonoBehaviour
{

    // Arraylist de ObjetosDeUmJogadorFIT
    public ObjetosDeJogadoresFIT objs_jogadores_fit;

    protected BancoDeDadosBolhas bd_bolhas;

    protected Vector2 resolucao;

    /// <summary>
    /// Guarda o objeto base a ser instanciado para cada posição de um personagem.
    /// </summary>
    protected ParaVisualizacao<GameObject> objetos;
    
    /// <summary>
    /// Guarda os materiais a serem instanciados e colocados nos objetos.
    /// </summary>
    protected ParaVisualizacao<Material> materiais;

    /// <summary>
    /// Guarda as texturas a serem colocadas nos materiais.
    /// </summary>
    public ParaVisualizacao<Texture2D> texturas;

    /// <summary>
    /// Guarda as texturas a serem colocadas nos materiais para representar que um objeto foi selecionado.
    /// </summary>
    public ParaVisualizacao<Texture2D> texturas_selecionadas;

    
    Pintar pintar;
    public int qual_jogador = 0;

    private ArrayList lista_de_objetos;
    public ArrayList Lista_de_objetos
    {
        get
        {
            if (qual_leitor == "FIT")
            {
                return objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).lista_de_objetos;
            }
            else if (qual_leitor == "Bolhas")
            {
                return lista_de_objetos;
            }
            else return null;
        }

        set
        {
            if (qual_leitor == "FIT")
            {
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).lista_de_objetos = value;
            }
            else if (qual_leitor == "Bolhas")
            {
                lista_de_objetos = value;
            }
            else Debug.Log("NovoLeitor2.qual_leitor está com valor diferente de Fit e Bolhas.");
        }
    }

    private ArrayList lista_de_backgrounds;

    public ArrayList Lista_de_backgrounds
    {
        get
        {
            if (qual_leitor == "FIT")
            {
                return objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).lista_de_backgrounds;
            }
            else if (qual_leitor == "Bolhas")
            {
                return lista_de_backgrounds;
            }
            else return null;
        }

        set
        {
            if (qual_leitor == "FIT")
            {
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).lista_de_backgrounds = value;
            }
            else if (qual_leitor == "Bolhas")
            {
                lista_de_backgrounds = value;
            }
            else Debug.Log("NovoLeitor2.qual_leitor está com valor diferente de Fit e Bolhas.");
        }
    }

    private ArrayList matrizes_dos_heatmaps;

    protected ArrayList Matrizes_dos_heatmaps
    {
        get
        {
            if (qual_leitor == "FIT")
            {
                return objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).matrizes_dos_heatmaps;
            }
            else if (qual_leitor == "Bolhas")
            {
                return matrizes_dos_heatmaps;
            }
            else return null;
        }

        set
        {
            if (qual_leitor == "FIT")
            {
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).matrizes_dos_heatmaps = value;
            }
            else if (qual_leitor == "Bolhas")
            {
                matrizes_dos_heatmaps = value;
            }
            else Debug.Log("NovoLeitor2.qual_leitor está com valor diferente de Fit e Bolhas.");
        }
    }

    protected string modo_fit;

    
    public int[] numeros_de_cores;

    GameObject heatmap;

    public GameObject Heatmap
    {
        get
        {
            if (qual_leitor == "FIT")
            {
                return objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).heatmap;
            }
            else if (qual_leitor == "Bolhas")
            {
                return heatmap;
            }
            else return null;
        }

        set
        {
            if (qual_leitor == "FIT")
            {
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).heatmap = value;
            }
            else if (qual_leitor == "Bolhas")
            {
                heatmap = value;
            }
            else Debug.Log("NovoLeitor2.qual_leitor está com valor diferente de Fit e Bolhas.");
        }
    }

    protected PegarEnderecoDeLog pegar_endereco_de_log;

    public string[] lista_de_nomes_de_objetos_do_FIT = { "1", "2", "3", "4" };
    public string[] lista_de_nomes_de_objetos_do_bolhas = { "Mouse", "Baleia", "Bolha", "Peixe", "Nuvem" };

    // Criado especificamente pra resolver um bug de sobreposição de desligamento de objetos lá em Controlador
    public Dictionary<string, int> nomes_e_numeros_de_objetos_do_FIT;
    public Dictionary<string, int> nomes_e_numeros_de_objetos_do_bolhas;

    private string qual_leitor;

    protected LidaComTexto lida_com_texto = new LidaComTexto();

    // No caso, é mesmo um list que guarda lists de Vector2.
    // No caso, cada list<Vector2> são as posições iniciais para um mapa do FIT.
    // O primeiro é o mapa 1, o segundo é o mapa 2, etc.
    List<List<Vector2>> posicoes_iniciais_de_personagens_nos_mapas_do_FIT;

    int heatmaps;

    public int Heatmaps
    {
        get
        {
            return heatmaps;
        }

        set
        {
            heatmaps = value;
        }
    }

    private GameObject ancora;

    public GameObject Ancora
    {
        get
        {
            if (qual_leitor == "FIT")
            {
                return objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).ancora_dos_dados;
            }
            else if (qual_leitor == "Bolhas")
            {
                return ancora;
            }
            else return null;
        }

        set
        {
            if (qual_leitor == "FIT")
            {
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).ancora_dos_dados= value;
            }
            else if (qual_leitor == "Bolhas")
            {
                ancora = value;
            }
            else Debug.Log("NovoLeitor2.qual_leitor está com valor diferente de Fit e Bolhas.");
        }
    }

    public void StartFIT()
    {
        qual_leitor = "FIT";

        objetos.Add("Qualquer Coisa FIT", (GameObject)Resources.Load("Objetos/Qualquer Coisa FIT"));

        //Sim, todos os objetos do FIT usam o mesmo material base. :p
        materiais.Add("1", (Material)Resources.Load("Materiais/MaterialCharacter"));
        materiais.Add("2", (Material)Resources.Load("Materiais/MaterialCharacter"));
        materiais.Add("3", (Material)Resources.Load("Materiais/MaterialCharacter"));
        materiais.Add("4", (Material)Resources.Load("Materiais/MaterialCharacter"));

        texturas.Add("1", (Texture2D)Resources.Load("Texturas/P1"));
        texturas.Add("2", (Texture2D)Resources.Load("Texturas/P2"));
        texturas.Add("3", (Texture2D)Resources.Load("Texturas/P3"));
        texturas.Add("4", (Texture2D)Resources.Load("Texturas/P4"));

        texturas_selecionadas.Add("1", (Texture2D)Resources.Load("Texturas/P1 Selecionado"));
        texturas_selecionadas.Add("2", (Texture2D)Resources.Load("Texturas/P2 Selecionado"));
        texturas_selecionadas.Add("3", (Texture2D)Resources.Load("Texturas/P3 Selecionado"));
        texturas_selecionadas.Add("4", (Texture2D)Resources.Load("Texturas/P4 Selecionado"));

        nomes_e_numeros_de_objetos_do_FIT.Add("1", 0);
        nomes_e_numeros_de_objetos_do_FIT.Add("2", 1);
        nomes_e_numeros_de_objetos_do_FIT.Add("3", 2);
        nomes_e_numeros_de_objetos_do_FIT.Add("4", 3);
    }

    public void StartBolhas()
    {
        qual_leitor = "Bolhas";

        objetos.Add("Qualquer Coisa Bolhas", (GameObject)Resources.Load("Objetos/Qualquer Coisa Bolhas"));

        materiais.Add("Mouse", (Material)Resources.Load("Materiais/MaterialMouse"));
        materiais.Add("Baleia", (Material)Resources.Load("Materiais/MaterialBaleia"));
        materiais.Add("Bolha", (Material)Resources.Load("Materiais/MaterialBolha"));
        materiais.Add("Peixe", (Material)Resources.Load("Materiais/MaterialPeixe"));
        materiais.Add("Nuvem", (Material)Resources.Load("Materiais/MaterialNuvem"));

        texturas.Add("Mouse", (Texture2D)Resources.Load("Texturas/Mouse"));
        texturas.Add("Baleia", (Texture2D)Resources.Load("Texturas/Baleia"));
        texturas.Add("Bolha", (Texture2D)Resources.Load("Texturas/Bolha"));
        texturas.Add("Peixe", (Texture2D)Resources.Load("Texturas/Peixe"));
        texturas.Add("Nuvem", (Texture2D)Resources.Load("Texturas/Nuvem"));

        texturas_selecionadas.Add("Mouse", (Texture2D)Resources.Load("Texturas/Mouse Clicado"));
        texturas_selecionadas.Add("Baleia", (Texture2D)Resources.Load("Texturas/Baleia Clicada"));
        texturas_selecionadas.Add("Bolha", (Texture2D)Resources.Load("Texturas/Bolha Clicada"));
        texturas_selecionadas.Add("Peixe", (Texture2D)Resources.Load("Texturas/Peixe Clicado"));
        texturas_selecionadas.Add("Nuvem", (Texture2D)Resources.Load("Texturas/Nuvem Clicada"));

        nomes_e_numeros_de_objetos_do_bolhas.Add("Mouse", 0);
        nomes_e_numeros_de_objetos_do_bolhas.Add("Baleia", 1);
        nomes_e_numeros_de_objetos_do_bolhas.Add("Bolha", 2);
        nomes_e_numeros_de_objetos_do_bolhas.Add("Peixe", 3);
        nomes_e_numeros_de_objetos_do_bolhas.Add("Nuvem", 4);
    }

    public bool LoadStuffSelecaoFIT(int instante_minimo = 0, int instante_maximo = int.MaxValue)
    {

        //number for number of HeatMaps
        Heatmaps = 0;

        // Handle any problems that might arise when reading the text
        string line;

        // Abrindo o arquivo com os mapas
        FileStream fs = new FileStream(pegar_endereco_de_log.endereco_de_arquivo[1], FileMode.Open);
        StreamReader theReader = new StreamReader(fs);

        line = theReader.ReadLine();

        string[] linha_alterada;
        string[] coordenadas;

        do
        {
            List<Vector2> posicoes = new List<Vector2>();

            //Level 1: A(X96,Y96) B(X64,Y384)
            linha_alterada = line.Split(':');
            //Level 1| A(X96, Y96) B(X64, Y384)

            linha_alterada[1] = linha_alterada[1].Remove(0, 1);
            //Level 1|A(X96,Y96) B(X64,Y384)

            linha_alterada = linha_alterada[1].Split('(', ')');

            //A|X96,Y96| B|X64,Y384|
            for (int i = 1; i < linha_alterada.GetLength(0); i = i + 2)
            {
                coordenadas = linha_alterada[i].Split(',');
                coordenadas[0] = coordenadas[0].Remove(0, 1);
                coordenadas[1] = coordenadas[1].Remove(0, 1);

                Vector2 x_e_y = new Vector2(Int32.Parse(coordenadas[0]), Int32.Parse(coordenadas[1]));
                posicoes.Add(x_e_y);


            }

            posicoes_iniciais_de_personagens_nos_mapas_do_FIT.Add(posicoes);

            // Próxima linha...
            line = theReader.ReadLine(); line = theReader.ReadLine();


        } while (line != null);

        lida_com_texto.FecharReaders(fs, theReader);

        objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit = new BancoDeDadosFIT();
        // Create a new StreamReader, tell it which file to read and what encoding the file
        // was saved as
        fs = new FileStream(pegar_endereco_de_log.endereco_de_arquivo[0], FileMode.Open);
        theReader = new StreamReader(fs);
        // Part 1: ignores  the [], the == START =, the [Test01] e ServerTime:481=CONNECTED =
        line = theReader.ReadLine(); line = theReader.ReadLine(); line = theReader.ReadLine(); line = theReader.ReadLine();

        // variaveis a serem usadas dentro do loop de leitura do log
        int estagio_atual = -1;
        int personagem_atual = -1;
        bool checagem_de_acao_do_jogador = true;
        List<List<Vector2>> posicoes_atuais_de_personagens_nos_mapas_do_FIT =
                            posicoes_iniciais_de_personagens_nos_mapas_do_FIT;
        int checando_instante_do_log = 0;

        // Part 2: reads the game events.
        // While there's lines left in the text file, do this:

        do
        {
            string[] entries;
            string[] entry_time;
            string[] entry_tempo_do_servidor;
            string[] entry_nome_do_jogador;
            string[] entry_id_do_jogador;
            string[] entry_modo_de_jogo;
            string[] entry_nivel;
            int input;
            Vector2 vetor_de_passagem;

            line = theReader.ReadLine();

            if (line != null)
            {
                // Do whatever you need to do with the text line, it's a string now
                entries = line.Split('=');
                // Correct Example: ServerTime:491=ServerID:1=Player:THIAGO=MODE:3=Level:1=Input:4=Time:237
                if (entries.Length == 7)
                {
                    // Nesse ponto, seguindo o exemplo, entries é um vetor com os sete valores
                    // ServerTime:491 | ServerID:1 | Player:THIAGO | MODE:3 | Level:1 | Input:4 | Time:237
                    // O próximo passo é conseguir os valores de cada caso

                    entry_time = entries[6].Split(':');
                    // entry_time = Time | 1

                    if ((instante_minimo <= checando_instante_do_log) && (checando_instante_do_log <= instante_maximo))
                    {

                        entry_tempo_do_servidor = entries[0].Split(':');
                        entry_id_do_jogador = entries[1].Split(':');
                        entry_nome_do_jogador = entries[2].Split(':');
                        entry_modo_de_jogo = entries[3].Split(':');
                        entry_nivel = entries[4].Split(':');
                        input = Int32.Parse(entries[5].Split(':')[1]);

                        if (Int32.Parse(entry_nivel[1]) - 1 != estagio_atual) personagem_atual = 0;
                        estagio_atual = Int32.Parse(entry_nivel[1]) - 1;

                        for (int i = 0; i < posicoes_atuais_de_personagens_nos_mapas_do_FIT[estagio_atual].Count; i++)
                        {

                            if ((i == personagem_atual) && (checagem_de_acao_do_jogador))
                            {
                                checagem_de_acao_do_jogador = false;
                                switch (input)
                                {
                                    // Movimentacao para cima;
                                    case 1:
                                        vetor_de_passagem = posicoes_atuais_de_personagens_nos_mapas_do_FIT[estagio_atual][i];
                                        vetor_de_passagem.y -= 32;
                                        posicoes_atuais_de_personagens_nos_mapas_do_FIT[estagio_atual][i] = vetor_de_passagem;
                                        break;
                                    // Movimentacao para esquerda;
                                    case 2:
                                        vetor_de_passagem = posicoes_atuais_de_personagens_nos_mapas_do_FIT[estagio_atual][i];
                                        vetor_de_passagem.x -= 32;
                                        posicoes_atuais_de_personagens_nos_mapas_do_FIT[estagio_atual][i] = vetor_de_passagem;
                                        break;
                                    // Movimentacao para baixo;
                                    case 3:
                                        vetor_de_passagem = posicoes_atuais_de_personagens_nos_mapas_do_FIT[estagio_atual][i];
                                        vetor_de_passagem.y += 32;
                                        posicoes_atuais_de_personagens_nos_mapas_do_FIT[estagio_atual][i] = vetor_de_passagem;
                                        break;
                                    // Movimentacao para direita;
                                    case 4:
                                        vetor_de_passagem = posicoes_atuais_de_personagens_nos_mapas_do_FIT[estagio_atual][i];
                                        vetor_de_passagem.x += 32;
                                        posicoes_atuais_de_personagens_nos_mapas_do_FIT[estagio_atual][i] = vetor_de_passagem;
                                        break;
                                    // Troca de Personagem
                                    case 0:
                                        personagem_atual = personagem_atual + 1;
                                        personagem_atual %= posicoes_atuais_de_personagens_nos_mapas_do_FIT[estagio_atual].Count;
                                        break;
                                    default:
                                        break;
                                }

                            }

                            objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.Add(checando_instante_do_log,
                                Int32.Parse(entry_time[1]), Int32.Parse(entry_nivel[1]), i + 1,
                                (int)posicoes_atuais_de_personagens_nos_mapas_do_FIT[estagio_atual][i].x,
                                (int)posicoes_atuais_de_personagens_nos_mapas_do_FIT[estagio_atual][i].y,
                                Int32.Parse(entry_tempo_do_servidor[1]), entry_nome_do_jogador[1],
                                Int32.Parse(entry_id_do_jogador[1]), Int32.Parse(entry_modo_de_jogo[1]));



                            // como o fit precisa ter, para ajudar na visualização, um heatmap para cada jogador,
                            // essa linha se aproveita do fato das informações de char serem guardadas em ordem crescente em cada
                            // tempo para descobrir o número de heatmaps extras, além do com todos os jogadores.

                            // ou seja: começamos com um heatmap, lemos 1 em entry_char, colocamos mais um heatmap, temos dois.
                            // lemos 2 em entry_char, colocamos mais um, temos três, e por aí vai.

                            // Mas sinceramente eu posso não fazer isso e só pegar do vetor de nomes do FIT...
                            if (i == Heatmaps) Heatmaps++;

                        }

                    }

                    checagem_de_acao_do_jogador = true;
                    checando_instante_do_log++;

                }

#if (DEBUG)

                else
                {
                    Debug.Log("NovoLeitor2.LoadStuff - Linha não tinha sete elementos, tinha " + entries.GetLength(0) + ".");
                }

#endif

            }
        } while (line != null);

        lida_com_texto.FecharReaders(fs, theReader);

        //Com isso, o último heatmap, do último personagem, não vai sumir.
        if (Heatmaps > 2) Heatmaps++;

        return true;

    }

    public void InicializarHeatmaps()
    {
        for (int i = 0; i < Heatmaps; i++)
        {
            Matrizes_dos_heatmaps.Add(new HeatMap());
        }
        numeros_de_cores = new int[Heatmaps];
    }

    public void TestMultiplePlayers()
    {
        List<BancoDeDadosFIT> bds_de_jogadores = new List<BancoDeDadosFIT>();

        for(int i = 0; i < objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetQuantidadeDeEntradas(); i++)
        {
            while (objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).
                    bd_fit.GetIdDoJogador(i) > bds_de_jogadores.Count){

                bds_de_jogadores.Add(new BancoDeDadosFIT());

            }

            bds_de_jogadores[objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetIdDoJogador(i) - 1].Add(
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetInstante(i),
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetTempo(i),
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetNivel(i),
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetPersonagem(i),
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetGridX(i),
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetGridY(i),
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetTempoDeServidor(i),
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetNomeDoJogador(i),
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetIdDoJogador(i),
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetModoDeJogo(i));


        }

    }

    public bool LoadStuffBolhas(int tempo_minimo = 0, int tempo_maximo = int.MaxValue)
    {
        //number of HeatMaps
        //no caso do Bolhas, 1 + Mouse mais 4 objetos = 6
        Heatmaps = 1 + lista_de_nomes_de_objetos_do_bolhas.GetUpperBound(0) + 1;

        // Handle any problems that might arise when reading the text
        string line;

        // Create a new StreamReader, tell it which file to read and what encoding the file
        // was saved as
        bd_bolhas = new BancoDeDadosBolhas();
        FileStream fs = new FileStream(pegar_endereco_de_log.endereco_de_arquivo[0], FileMode.Open);
        StreamReader theReader = new StreamReader(fs);

        // Part 1: ignores the [Mode Bolhas]
        // No futuro, não ignorar.
        line = theReader.ReadLine();


        // Part 2: reads the game screen's resolution in the log.
        line = theReader.ReadLine();
        if (line != null)
        {
            string[] entriesresolution = line.Split('x');
            if (entriesresolution.Length == 2)
            {
                resolucao = new Vector2(Int32.Parse(entriesresolution[0]), Int32.Parse(entriesresolution[1]));
            }

            #if (DEBUG)

            else
            { 
                Debug.Log("NovoLeitor2.LoadStuff - Linha não tinha dois elementos.");
            }

            #endif

         }

        #if (DEBUG)

        else
        {
            Debug.Log("NovoLeitor2.LoadStuff - Linha de resolução = null.");
        }

        #endif

        // Part 2: reads the game events.
        // While there's lines left in the text file, do this:
        do
        {
            line = theReader.ReadLine();

            //Exemplo de linha lida:
            //Time:0=Mouse=GridX:776=GridY:97=Clicando:N=Segurando:S=Arrastando:N
            if (line != null)
            {
                // Do whatever you need to do with the text line, it's a string now

                string[] entries = line.Split('=');


                //Após o split:

                // Time:0
                // Mouse
                // GridX:776 e por aí vai.

                if ((entries.Length == 7) && (((string)entries[1]) == "Mouse"))
                {
                    int checando_tempo_do_log = Int32.Parse(entries[0].Split(':')[1]);

                    //Os splits dividem os strings entre antes e depois dos ':' presentes
                    //Daí é só pegar o dado necessário, e não sua legenda.
                    if ((tempo_minimo <= checando_tempo_do_log) && (checando_tempo_do_log <= tempo_maximo))
                    {
                        bd_bolhas.AddMouse(checando_tempo_do_log,
                            entries[1].ToString(),
                            Int32.Parse(entries[2].Split(':')[1]),
                            Int32.Parse(entries[3].Split(':')[1]),
                            entries[4].Split(':')[1].ToString(),
                            entries[5].Split(':')[1].ToString(),
                            entries[6].Split(':')[1].ToString());
                    }

                }
                else if ((entries.Length == 11) && (((string)entries[1]) == "Objeto"))
                {
                    int checando_tempo_do_log = Int32.Parse(entries[0].Split(':')[1]);

                    //Os splits dividem os strings entre antes e depois dos ':' presentes
                    //Daí é só pegar o dado necessário, e não sua legenda.
                    if ((tempo_minimo <= checando_tempo_do_log) && (checando_tempo_do_log <= tempo_maximo))
                    {
                        bd_bolhas.AddObjeto(Int32.Parse(entries[0].Split(':')[1]),
                                    entries[1].ToString(),
                                    Int32.Parse(entries[2].Split(':')[1]),
                                    Int32.Parse(entries[3].Split(':')[1]),
                                    entries[4].ToString(),
                                    Int32.Parse(entries[5].Split(':')[1]),
                                    entries[6].Split(':')[1].ToString(),
                                    entries[7].Split(':')[1].ToString(),
                                    entries[8].Split(':')[1].ToString(),
                                    entries[9].Split(':')[1].ToString(),
                                    entries[10].Split(':')[1].ToString());
                    }
                }

                #if (DEBUG)

                else
                {
                    Debug.Log("NovoLeitor2.LoadStuff - Linha formatada errada.");
                }

                #endif

            }
        } while (line != null);

        lida_com_texto.FecharReaders(fs, theReader);

        InicializarHeatmaps();

        return true;
    }

    public bool PrintStuffFIT()
    {
        for (int i = 0; i < objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetQuantidadeDeEntradas(); i++)
        {
            Debug.Log(objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetTempo(i) + " " + 
                      objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetPersonagem(i) + " " +
                      objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetGridX(i) + " " +
                      objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetGridY(i));
        }
        return true;

    }

    public bool PrintStuffBolhas()
    {
        for (int i = 0; i < bd_bolhas.GetQuantidadeDeEntradas(); i++)
        {
            if (bd_bolhas.GetMouseOuObjeto(i) == "Mouse")
            {
                Debug.Log(bd_bolhas.GetTempo(i) + " " + bd_bolhas.GetMouseOuObjeto(i) + " " +
                       bd_bolhas.GetCoordenadaX(i) + " " + bd_bolhas.GetCoordenadaY(i) + " " +
                       bd_bolhas.GetClicando(i) + " " + bd_bolhas.GetSegurando(i) + " " + bd_bolhas.GetArrastando(i));
            }
            else if (bd_bolhas.GetMouseOuObjeto(i) == "Objeto")
            {
                Debug.Log(bd_bolhas.GetTempo(i) + " " + bd_bolhas.GetMouseOuObjeto(i) + " " +
                       bd_bolhas.GetCoordenadaX(i) + " " + bd_bolhas.GetCoordenadaY(i) + " " +
                       bd_bolhas.GetQualObjeto(i) + " " + bd_bolhas.GetQualFrame(i) + " " + bd_bolhas.GetQuemCriou(i) + " " +
                       bd_bolhas.GetClicando(i) + " " + bd_bolhas.GetSegurando(i) + " " + bd_bolhas.GetArrastando(i));
            }

        }
        return true;

    }

    public void CreateStuffFIT()
    {
        Camera a_camera = FindObjectOfType<Camera>();
        GameObject objeto = null;
        GameObject background = null;
        Vector3 new_pos = new Vector3(0f, 0f, 0f);
        Vector3 new_pos_camera = new Vector3(0f, 0f, 0f);
        Material material_do_create = null;
        float x, y, z;
        int i;
        Material material_background = (Material)Resources.Load("Materiais/MaterialBackgroundFIT");
        Material[] rend;

        bool criar_background; bool fechar_background;

        // Definição inicial de valores de x, y e z para que possam ser usados
        // nas funções de definição de posição de objetos
        x = 0.0f; y = 0.0f; z = 0.0f;

        Material material_heatmap = new Material((Material)Resources.Load("Materiais/MaterialHeatmap"));

        material_background.mainTexture = (Texture)Instantiate(Resources.Load("Texturas/Grid"));

        // Cria o Heatmap
        //PreenchedorDeHeatmapsFIT();

        criar_background = true;
        fechar_background = false;

        // Para cada objeto...

        for (i = 0; i < objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetQuantidadeDeEntradas(); i++)
        {
            // Controle de quando criar um background novo ou
            // não criá-lo. Backgrounds novos são criados, um para cada posição no tempo diferente.
            if ((i != objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetQuantidadeDeEntradas() - 1) && (i != 0))
            {

                if (objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetPersonagem(i) >=
                    objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetPersonagem(i + 1))
                {
                    fechar_background = true;
                }
                if (objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetPersonagem(i) <=
                    objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetPersonagem(i - 1))
                {
                    criar_background = true;
                }
            }
            else if (i == objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetQuantidadeDeEntradas() - 1)
            {
                if (objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetPersonagem(i) <=
                    objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetPersonagem(i - 1))
                {
                    criar_background = true;
                }

                fechar_background = true;
            }
            else if (i == 0) criar_background = true;

            material_do_create = null;

            objeto = Instantiate(objetos.Get("Qualquer Coisa FIT"));
            objeto.AddComponent<AoSerClicadoFIT>();
            objeto.AddComponent<LigaDesliga>();

            objeto.name = objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetNomeDoJogador(i) + " " +
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetTempo(i).ToString() + " " +
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetPersonagem(i).ToString() + " " +
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetGridX(i).ToString() + " " +
                objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetGridY(i).ToString();

            material_do_create = Instantiate(materiais.Get(objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).
                                             bd_fit.GetPersonagem(i).ToString()));

            // Essencialmente, materiais guardam texturas, que é o que queremos.
            // Foi um pouco de exagero fazer um material pra cada objeto, mas
            // isso vai ser útil no futuro para efeitos diferentes para cada objeto,
            // se necessário.
            if (material_do_create != null)
            {

                objeto.GetComponent<MeshRenderer>().material = material_do_create;

            }

            rend = objeto.GetComponent<MeshRenderer>().materials;
            AddMaterialAObjeto(rend, objeto, i);

            // Ponto já criado, agora adicionar dados a ele
            AddDados(objeto, i);

            Lista_de_objetos.Add(objeto);

            // Criando background para os pontos
            if (criar_background)
            {

                background = CriarBackground(background, material_background);

            }

            PermitindoObjetosVisiveisPorTras(objeto);

            CriarPosicaoDoBackground(new_pos, background);

            //adicionando o background onde deve ficar
            if (criar_background)
            {
                background = ColocarDadosEmBackground(background, i);
                Lista_de_backgrounds.Add(background);
            }

            // Passar os valores de x, y e tempo para a posição de objetos
            // sem tratar as posições é uma péssima ideia, já que podem haver valores MUITO
            // grande, logo...
            y = background.transform.position.y;

            x = DefinirXDeObjeto(objeto, background, x, i);

            // Z... que nesse caso é equivalente a y no plano 2D. Sim, paciência.
            z = DefinirZDeObjeto(objeto, background, z, i);

            new_pos = new Vector3(x, y, z);

            objeto.transform.position = new_pos;

            y += 0.5f;
            objeto.transform.position = new Vector3(x, y, z);

            background.GetComponent<Conector>().AddPonto(objeto);

            if (fechar_background)
            {
                background.GetComponent<Conector>().Conectar();
                background.name = background.GetComponent<Dados>().qual_jogador + " - Background - " + i;
            }

            criar_background = false;
            fechar_background = false;

        }

        Ancora = Instantiate(Resources.Load<GameObject>("Objetos/Ancora de Movimentacao"));
        Ancora.name = "Ancora";
        ConectarTodosAAncora();
        objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).ancora_dos_dados = Ancora;
        Ancora.name = objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetNomeDoJogador(0) + " " + Ancora.name;

        AjeitandoACamera(new_pos_camera, a_camera);

        AjeitandoOHeatmap(material_heatmap);

        GetComponent<Controlador>().MudarTransparenciaDosObjetos(0.2f);



    }

    protected void PreenchedorDeHeatmapsFIT()
    {
        LendoPontosDeHeatmapsFIT();
        OrganizacaoDePontosEPinturaDeHeatmapsFIT();
    }

    protected void LendoPontosDeHeatmapsFIT()
    {
        for (int j = 0; j < Matrizes_dos_heatmaps.Count; j++)
        {
            ((HeatMap)Matrizes_dos_heatmaps[j]).AlterarValoresDeTamanhoDeHeatmap(20, 15);
            ((HeatMap)Matrizes_dos_heatmaps[j]).ReadPointsFIT(objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit,
                                                              j);
            
        }
    }

    protected void OrganizacaoDePontosEPinturaDeHeatmapsFIT()
    {
        for (int j = 0; j < Matrizes_dos_heatmaps.Count; j++)
        {
            ((HeatMap)Matrizes_dos_heatmaps[j]).OrganizePoints();
            ((HeatMap)Matrizes_dos_heatmaps[j]).FillingTheDictionary();
            ((HeatMap)Matrizes_dos_heatmaps[j]).PaintingTheHeatmap();
            numeros_de_cores[j] = ((HeatMap)Matrizes_dos_heatmaps[j]).HowManyPoints();
        }
    }

    public void CreateStuffBolhas()
    {
        Camera a_camera = FindObjectOfType<Camera>();
        GameObject objeto = null;
        GameObject background = null;
        Vector3 new_pos = new Vector3(0f, 0f, 0f);
        Vector3 new_pos_camera = new Vector3(0f, 0f, 0f);
        Material material_do_create = null;

        float x, y, z;
        int i;
        Material material_background = (Material)Resources.Load("Materiais/MaterialBackgroundBolhas");
        Material[] rend;

        bool novo_tempo;
        bool vira_novo_tempo;
        int tempo;

        // Definição inicial de valores de x, y e z para que possam ser usados
        // nas funções de definição de posição de objetos
        x = 0.0f; y = 0.0f; z = 0.0f;

        Material material_heatmap = new Material((Material)Resources.Load("Materiais/MaterialHeatmap"));

        material_background.mainTexture = (Texture)Instantiate(Resources.Load("Texturas/Fundo Bolhas Desenhado"));

        // Lê todos os dados do log do Bolhas...
        for (int j = 0; j < Matrizes_dos_heatmaps.Count; j++)
        {
            ((HeatMap)Matrizes_dos_heatmaps[j]).AlterarValoresDeTamanhoDeHeatmap(800, 600);
            if (j == 0)  ((HeatMap)Matrizes_dos_heatmaps[j]).ReadPointsBolhas(bd_bolhas, "Todos");
            else ((HeatMap)Matrizes_dos_heatmaps[j]).ReadPointsBolhas(bd_bolhas, lista_de_nomes_de_objetos_do_bolhas[j-1]);
            ((HeatMap)Matrizes_dos_heatmaps[j]).AllTheDifferentPoints();
            ((HeatMap)Matrizes_dos_heatmaps[j]).OrganizePoints();
            ((HeatMap)Matrizes_dos_heatmaps[j]).FillingTheDictionary();
            ((HeatMap)Matrizes_dos_heatmaps[j]).PaintingTheHeatmap();
            numeros_de_cores[j] = ((HeatMap)Matrizes_dos_heatmaps[j]).HowManyPoints();
        }

        tempo = bd_bolhas.GetTempo(0);
        novo_tempo = true;
        vira_novo_tempo = false;

        for (i = 0; i < bd_bolhas.GetQuantidadeDeEntradas(); i++)
        {
            // Controle de quando criar um background novo ou
            // não criá-lo. Backgrounds novos são criados, um para cada posição no tempo diferente.
            // refatorar esse código...
            tempo = bd_bolhas.GetTempo(i);
            if (i != bd_bolhas.GetQuantidadeDeEntradas() - 1)
            {
                if (i == 0) novo_tempo = true;
                else if (tempo != bd_bolhas.GetTempo(i - 1)) novo_tempo = true;
            }
            else if (tempo != bd_bolhas.GetTempo(i - 1))
            {
                novo_tempo = true;
            }

            if (i == bd_bolhas.GetQuantidadeDeEntradas() - 1) vira_novo_tempo = true;
            else if (tempo != bd_bolhas.GetTempo(i + 1)) vira_novo_tempo = true;

            material_do_create = null;

            objeto = Instantiate(objetos.Get("Qualquer Coisa Bolhas"));
            objeto.AddComponent<AoSerClicadoBolhas>();
            objeto.AddComponent<LigaDesliga>();

            objeto.name = bd_bolhas.GetTempo(i).ToString() + " " + bd_bolhas.GetQualObjeto(i).ToString() + " " +
                bd_bolhas.GetCoordenadaX(i).ToString() + " " + bd_bolhas.GetCoordenadaY(i).ToString();

            material_do_create = Instantiate(materiais.Get(bd_bolhas.GetQualObjeto(i).ToString()));

            // Essencialmente, materiais guardam texturas, que é o que queremos.
            // Foi um pouco de exagero fazer um material pra cada objeto, mas
            // isso vai ser útil no futuro para efeitos diferentes para cada objeto,
            // se necessário.
            if (material_do_create != null) {

                objeto.GetComponent<MeshRenderer>().material = material_do_create;

            }

            rend = objeto.GetComponent<MeshRenderer>().materials;
            AddMaterialAObjeto(rend, objeto, i);
            
            // Ponto já criado, agora adicionar dados a ele
            AddDados(objeto, i);

            Lista_de_objetos.Add(objeto);

            //criando background para o par de pontos
            if (novo_tempo)
            {

                background = CriarBackground(background, material_background);

            }

            PermitindoObjetosVisiveisPorTras(objeto);

            CriarPosicaoDoBackground(new_pos, background);

            //adicionando o background onde deve ficar
            if (novo_tempo)
            {
                background = ColocarDadosEmBackground(background, i);
                Lista_de_backgrounds.Add(background);
            }

            objeto.transform.localScale = new Vector3(texturas.Get(bd_bolhas.GetQualObjeto(i).ToString()).width / resolucao.x,
                                                      texturas.Get(bd_bolhas.GetQualObjeto(i).ToString()).width / resolucao.x,
                                                      texturas.Get(bd_bolhas.GetQualObjeto(i).ToString()).height / resolucao.y);

            // Passar os valores de x, y e tempo para a posição de objetos
            // sem tratar as posições é uma péssima ideia, já que podem haver valores MUITO
            // grande, logo...
            y = background.transform.position.y;

            x = DefinirXDeObjeto(objeto, background, x, i);

            // Z... nesse caso é equivalente a y no plano 2D. Sim, paciência.
            z = DefinirZDeObjeto(objeto, background, z, i);

            new_pos = new Vector3(x, y, z);

            objeto.transform.position = new_pos;

            y += 0.5f;
            objeto.transform.position = new Vector3(x, y, z);

            background.GetComponent<Conector>().AddPonto(objeto);

            if (vira_novo_tempo)
            {
                background.GetComponent<Conector>().Conectar();
            }

            novo_tempo = false;
            vira_novo_tempo = false;

        }

        AjeitandoACamera(new_pos_camera, a_camera);

        AjeitandoOHeatmap(material_heatmap);

        GetComponent<Controlador>().MudarTransparenciaDosObjetos(0.2f);

    }

    public void ControlarAlpha(float alpha)
    {
        for (int i = 0; i < Lista_de_backgrounds.Count; i++)
        {
            Color cor = ((GameObject)Lista_de_backgrounds[i]).GetComponent<MeshRenderer>().material.GetColor("_Color");
               
            cor.a = alpha;

            ((GameObject)Lista_de_backgrounds[i]).GetComponent<MeshRenderer>().material.SetColor("_Color", cor);
        }
    }

    public void GirarBackgrounds(Vector3 coordenadas)
    {
        for (int i = 0; i < Lista_de_backgrounds.Count; i++)
        {
            ((GameObject)Lista_de_backgrounds[i]).transform.rotation = Quaternion.identity;
            ((GameObject)Lista_de_backgrounds[i]).transform.Rotate(coordenadas);
        }
    }

    public void GirarCamera(Vector3 coordenadas)
    {
        GetComponent<Camera>().transform.rotation = Quaternion.identity;
        GetComponent<Camera>().transform.Rotate(coordenadas);
    }

    public void AlterarLayer(int alt)
    {
        for (int i = 0; i < Lista_de_backgrounds.Count; i++)
        {
            ((GameObject)Lista_de_backgrounds[i]).GetComponent<Conector>().NoLayer(alt);
        }
    }

    public void PosicionarBackgrounds(float dist)
    {
        float yback0 = ((GameObject)Lista_de_backgrounds[0]).transform.position.y;
        Vector3 pos;

        for (int i = 1; i < Lista_de_backgrounds.Count; i++)
        {
            yback0 -= dist;
            pos = ((GameObject)Lista_de_backgrounds[i]).transform.position;
            pos.y = yback0;
            ((GameObject)Lista_de_backgrounds[i]).transform.position = pos;
        }
    }

    public void ConectarTodosAAncora()
    {
        for (int j = 0; j < Lista_de_backgrounds.Count; j++)
        {
            GameObject background = (GameObject)Lista_de_backgrounds[j];
            background.transform.parent = Ancora.transform;
        }
    }

    public void ConectarTodos()
    {
        for (int j = 0; j < Lista_de_backgrounds.Count; j++)
        {
            GameObject background = (GameObject)Lista_de_backgrounds[j];
            background.GetComponent<Conector>().Conectar();
        }
    }

    public void DesconectarTodos()
    {
        for (int j = 0; j < Lista_de_backgrounds.Count; j++)
        {
            GameObject background = (GameObject)Lista_de_backgrounds[j];
            background.GetComponent<Conector>().Desconectar();
        }
    }

    public float DefinirXDeObjeto(GameObject objeto, GameObject background, float x, int i)
    {
        // Primeiro: conseguir a posição da borda esquerda do background
        x = background.GetComponent<Dados>().centro.x - background.GetComponent<Dados>().largura_x / 2;

        // Segundo: a partir daí, achar onde o centro do objeto precisa estar para apenas encostar
        // nessa borda.
        x += (objeto.GetComponent<MeshCollider>().bounds.max.x - objeto.GetComponent<MeshCollider>().bounds.min.x) / 2;

        // Terceiro: finalmente, posicionar o objeto com relação ao background.
        if (qual_leitor == "FIT") x += objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetGridX(i) / 32 * 
                (background.GetComponent<Dados>().largura_x / 20);
        if (qual_leitor == "Bolhas") x += bd_bolhas.GetCoordenadaX(i) * 
                (background.GetComponent<Dados>().largura_x / resolucao.x);

        return x;
    }

    public float DefinirZDeObjeto(GameObject objeto, GameObject background, float z, int i)
    {
        // Primeiro: conseguir a posição da borda superior do background
        z = background.GetComponent<Dados>().centro.y + background.GetComponent<Dados>().altura_z / 2;

        // Segundo: a partir daí, achar onde o centro do objeto precisa estar para apenas encostar
        // nessa borda.
        z -= (objeto.GetComponent<MeshCollider>().bounds.max.z - objeto.GetComponent<MeshCollider>().bounds.min.z) / 2;

        // Terceiro: finalmente, posicionar o objeto com relação ao background.
        if (qual_leitor == "FIT") z -= (objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetGridY(i) / 32 * (background.GetComponent<Dados>().altura_z / 15));
        if (qual_leitor == "Bolhas") z -= (bd_bolhas.GetCoordenadaY(i) * (background.GetComponent<Dados>().altura_z / resolucao.y));

        return z;
    }

    public HeatMap GetMatrizHeatmap(int i = 0)
    {
        return ((HeatMap)Matrizes_dos_heatmaps[i]);
    }

    public int GetQuantHeatmaps() { return Matrizes_dos_heatmaps.Count; }

    public void ChangeTexturaHeatmap(int qual)
    {
        if ((qual >= 0) && (qual < GetQuantHeatmaps()))
        {
            Heatmap.GetComponent<MeshRenderer>().material.
                SetTexture("_MainTex", ((HeatMap)Matrizes_dos_heatmaps[qual]).heatmap);

        }
        
    }

    public int GetPrimeiroTempo()
    {
        if (qual_leitor == "FIT") return GetPrimeiroTempoFIT();
        else if (qual_leitor == "Bolhas") return GetPrimeiroTempoBolhas();
        else return 0;
    }

    protected GameObject CriarBackground(GameObject background, Material material_background)
    {
        GameObject objeto = null;
        if (qual_leitor == "FIT") objeto = CriarBackgroundFIT(background, material_background);
        else if (qual_leitor == "Bolhas") objeto = CriarBackgroundBolhas(background, material_background);
        return objeto;
    }

    protected GameObject ColocarDadosEmBackground(GameObject background, int i)
    {
        background.AddComponent<Dados>();
        background.GetComponent<Dados>().Atualizar();
        return background;
    }

    protected GameObject CriarBackgroundFIT(GameObject background, Material material_background)
    {
        background = Instantiate<GameObject>((GameObject)Resources.Load("Objetos/BackgroundFIT"));
        background.GetComponent<MeshRenderer>().material = Instantiate(material_background);
        background.GetComponent<Conector>().backgroundprincipal = background;
        background.AddComponent<LigaDesliga>();
        return background;
    }

    protected GameObject CriarBackgroundBolhas(GameObject background, Material material_background)
    {
        background = Instantiate<GameObject>((GameObject)Resources.Load("Objetos/BackgroundBolhas"));
        background.GetComponent<MeshRenderer>().material = Instantiate(material_background);
        background.GetComponent<Conector>().backgroundprincipal = background;
        background.AddComponent<LigaDesliga>();
        return background;
    }

    protected void CriarPosicaoDoBackground(Vector3 new_pos, GameObject background)
    {
        float y = background.transform.position.y;

        new_pos.x = 0;
        y = background.transform.position.y;
        new_pos.y = y;
        new_pos.z = 0;
        background.transform.position = new_pos;
    }

    protected void PermitindoObjetosVisiveisPorTras(GameObject objeto)
    {
        GameObject objeto2 = Instantiate(objeto);
        objeto2.transform.parent = objeto.transform;
        objeto2.transform.Rotate(new Vector3(0, 0, 180));
    }

    protected void AddMaterialAObjeto(Material[] rend, GameObject objeto, int i)
    {
        if (qual_leitor == "FIT") AddMaterialAObjetoFIT(rend, objeto, i);
        else if (qual_leitor == "Bolhas") AddMaterialAObjetoBolhas(rend, objeto, i);
    }

    protected void AddMaterialAObjetoFIT(Material[] rend, GameObject objeto, int i)
    {
        rend[0].mainTexture = texturas.Get(objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).
                                bd_fit.GetPersonagem(i).ToString());
        objeto.GetComponent<MeshRenderer>().materials = rend;
    }

    protected void AddMaterialAObjetoBolhas(Material[] rend, GameObject objeto, int i)
    {
        rend[0].mainTexture = texturas.Get(bd_bolhas.GetQualObjeto(i).ToString());
        objeto.GetComponent<MeshRenderer>().materials = rend;
    }

    protected void AddDados(GameObject objeto_a_receber_dados, int i)
    {
        if (qual_leitor == "FIT") AddDadosFIT(objeto_a_receber_dados, i);
        else if (qual_leitor == "Bolhas") AddDadosBolhas(objeto_a_receber_dados, i);
    }

    protected void AddDadosFIT(GameObject objeto_a_receber_dados, int i)
    {
        objeto_a_receber_dados.AddComponent<Dados>();
        objeto_a_receber_dados.GetComponent<Dados>().Atualizar();
        objeto_a_receber_dados.GetComponent<Dados>().nome_do_objeto = objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).
                                                                         bd_fit.GetPersonagem(i).ToString();
        objeto_a_receber_dados.GetComponent<Dados>().tempo = objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).
                                                                bd_fit.GetTempo(i);
        objeto_a_receber_dados.GetComponent<Dados>().x_log = objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).
                                                                bd_fit.GetGridX(i);
        objeto_a_receber_dados.GetComponent<Dados>().y_log = objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).
                                                                bd_fit.GetGridY(i);
        objeto_a_receber_dados.GetComponent<Dados>().tempo_do_servidor = objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).
                                                                bd_fit.GetTempoDeServidor(i);
        objeto_a_receber_dados.GetComponent<Dados>().id_do_jogador = objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).
                                                                bd_fit.GetIdDoJogador(i);
        objeto_a_receber_dados.GetComponent<Dados>().qual_jogador = objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).
                                                                bd_fit.GetNomeDoJogador(i);
        objeto_a_receber_dados.GetComponent<Dados>().instante_em_camera = objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).
                                                                bd_fit.GetInstante(i);
        objeto_a_receber_dados.GetComponent<Dados>().nivel = objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).
                                                                bd_fit.GetNivel(i);
    }

    protected void AddDadosBolhas(GameObject objeto_a_receber_dados, int i)
    {
        objeto_a_receber_dados.AddComponent<Dados>();
        objeto_a_receber_dados.GetComponent<Dados>().Atualizar();
        objeto_a_receber_dados.GetComponent<Dados>().tempo = bd_bolhas.GetTempo(i);
        objeto_a_receber_dados.GetComponent<Dados>().x_log = bd_bolhas.GetCoordenadaX(i);
        objeto_a_receber_dados.GetComponent<Dados>().y_log = bd_bolhas.GetCoordenadaY(i);
        objeto_a_receber_dados.GetComponent<Dados>().nome_do_objeto = bd_bolhas.GetQualObjeto(i);

        if (bd_bolhas.GetMouseOuObjeto(i) == "Mouse")
        {
            if (bd_bolhas.GetArrastando(i) == "S") objeto_a_receber_dados.GetComponent<Dados>().acao_dele_ou_nele = "Arrastou";
            else if (bd_bolhas.GetClicando(i) == "S") objeto_a_receber_dados.GetComponent<Dados>().acao_dele_ou_nele = "Clicou";
            else if (bd_bolhas.GetSegurando(i) == "S") objeto_a_receber_dados.GetComponent<Dados>().acao_dele_ou_nele = "Segurando";
        }
        else if (bd_bolhas.GetMouseOuObjeto(i) == "Objeto")
        {
            if (bd_bolhas.GetArrastando(i) == "S") objeto_a_receber_dados.GetComponent<Dados>().acao_dele_ou_nele = "Arrastado";
            else if (bd_bolhas.GetClicando(i) == "S") objeto_a_receber_dados.GetComponent<Dados>().acao_dele_ou_nele = "Clicado";
            else if (bd_bolhas.GetSegurando(i) == "S") objeto_a_receber_dados.GetComponent<Dados>().acao_dele_ou_nele = "Segurado";
            objeto_a_receber_dados.GetComponent<Dados>().criado_agora = bd_bolhas.GetCriadoAgora(i);
            objeto_a_receber_dados.GetComponent<Dados>().quem_criou = bd_bolhas.GetQuemCriou(i);
        }
    }

    protected void AjeitandoACamera(Vector3 new_pos_camera, Camera a_camera)
    {
        new_pos_camera.y = ((GameObject)Lista_de_backgrounds[0]).transform.position.y;
        a_camera.transform.position = new_pos_camera;
        a_camera.transform.Rotate(90f, 0f, 0f);
        a_camera.orthographic = true;
    }

    protected void AjeitandoOHeatmap(Material materialheatmap)
    {
        Heatmap = Instantiate<GameObject>((GameObject)Resources.Load("Objetos/BackgroundHeatmap"));
        Heatmap.name = "Heatmap";
        materialheatmap.SetTexture("_MainTex", ((HeatMap)Matrizes_dos_heatmaps[0]).heatmap);
        Heatmap.GetComponent<MeshRenderer>().material = Instantiate(materialheatmap);

        Heatmap.transform.position = ((GameObject)Lista_de_backgrounds[0]).transform.position
            + new Vector3(200f, 0, 0);

        Heatmap.transform.parent = Ancora.transform;
    }

    public void ResetandoAncoras()
    {

    }

    public void ReposicionandoPosicoesEsquerdaUmDeCadaVez3D()
    {
        ConectarTodos();
        Ancora.transform.position = new Vector3(GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetPosEsq("Um Frame De Cada Vez em 3D"),
                                                0f, 0f);

        Ancora.transform.localScale = GetComponent<Controlador>().GetBancoDeDadosModos().
            GetEscalaEsquerdaOuDireita("Um Frame De Cada Vez em 3D");

        for (int i = 0; i < Lista_de_backgrounds.Count; i++)
        {
            ((GameObject)Lista_de_backgrounds[i]).transform.localEulerAngles = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetRotacaoCentro("Um Frame De Cada Vez em 3D");
        }
        //DesconectarTodos();

    }

    public void ReposicionandoPosicoesCentroUmDeCadaVez3D()
    {
        ConectarTodos();
        Ancora.transform.position = new Vector3(GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetPosCen("Um Frame De Cada Vez em 3D"),
                                                0f, 0f);

        Ancora.transform.localScale = GetComponent<Controlador>().GetBancoDeDadosModos().
            GetEscalaNoCentro("Um Frame De Cada Vez em 3D");

        for (int i = 0; i < Lista_de_backgrounds.Count; i++)
        {
            ((GameObject)Lista_de_backgrounds[i]).transform.localEulerAngles = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetRotacaoCentro("Um Frame De Cada Vez em 3D");
        }

        //DesconectarTodos();
    }

    public void ReposicionandoPosicoesDireitaUmDeCadaVez3D()
    {
        ConectarTodos();
        Ancora.transform.position = new Vector3(GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetPosDir("Um Frame De Cada Vez em 3D"),
                                                0f, 0f);

        Ancora.transform.localScale = GetComponent<Controlador>().GetBancoDeDadosModos().
            GetEscalaEsquerdaOuDireita("Um Frame De Cada Vez em 3D");

        for (int i = 0; i < Lista_de_backgrounds.Count; i++)
        {
            ((GameObject)Lista_de_backgrounds[i]).transform.localEulerAngles = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetRotacaoEsquerdaOuDireita("Um Frame De Cada Vez em 3D");
        }
        ///ATENÇÃO, NÃO DESCOMENTAR ESSA LINHA.
        //DesconectarTodos();
    }

    public void ReposicionandoPosicoesEsquerdaUmDeCadaVez2D()
    {
        Ancora.transform.position = new Vector3(GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetPosEsq("Um Frame De Cada Vez em 2D"),
                                                0f, 0f);
        Ancora.transform.localScale = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetEscalaEsquerdaOuDireita("Um Frame De Cada Vez em 2D");

        for (int i = 0; i < Lista_de_backgrounds.Count; i++)
        {
            ((GameObject)Lista_de_backgrounds[i]).transform.localEulerAngles = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetRotacaoCentro("Um Frame De Cada Vez em 2D");
        }
    }

    public void ReposicionandoPosicoesCentroUmDeCadaVez2D()
    {
        Ancora.transform.position = new Vector3(0f, 0f, 0f);
        Ancora.transform.localScale = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetEscalaNoCentro("Um Frame De Cada Vez em 2D");

        for (int i = 0; i < Lista_de_backgrounds.Count; i++)
        {
            ((GameObject)Lista_de_backgrounds[i]).transform.localEulerAngles = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetRotacaoCentro("Um Frame De Cada Vez em 2D");
        }
    }

    public void ReposicionandoPosicoesDireitaUmDeCadaVez2D()
    {
        Ancora.transform.position = new Vector3(GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetPosDir("Um Frame De Cada Vez em 2D"),
                                                0f, 0f);
        Ancora.transform.localScale = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetEscalaEsquerdaOuDireita("Um Frame De Cada Vez em 2D");

        for (int i = 0; i < Lista_de_backgrounds.Count; i++)
        {
            ((GameObject)Lista_de_backgrounds[i]).transform.localEulerAngles = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetRotacaoCentro("Um Frame De Cada Vez em 2D");
        }
    }

    public void ReposicionandoPosicoesEsquerdaTodosDeUmaVez3D()
    {
        ConectarTodos();
        Ancora.transform.position = new Vector3(GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetPosEsq("Todos De Uma Vez em 3D"),
                                                0f, 0f);

        Ancora.transform.localScale = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetEscalaEsquerdaOuDireita("Todos De Uma Vez em 3D");

        for (int i = 0; i < Lista_de_backgrounds.Count; i++)
        {
            ((GameObject)Lista_de_backgrounds[i]).transform.localEulerAngles = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetRotacaoCentro("Todos De Uma Vez em 3D");
        }
        DesconectarTodos();
    }

    public void ReposicionandoPosicoesCentroTodosDeUmaVez3D()
    {
        ConectarTodos();
        Ancora.transform.position = new Vector3(GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetPosCen("Todos De Uma Vez em 3D"),
                                                0f, 0f);

        Ancora.transform.localScale = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetEscalaNoCentro("Todos De Uma Vez em 3D");

        for (int i = 0; i < Lista_de_backgrounds.Count; i++)
        {
            ((GameObject)Lista_de_backgrounds[i]).transform.localEulerAngles = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetRotacaoCentro("Todos De Uma Vez em 3D");
        }
        DesconectarTodos();
    }

    public void ReposicionandoPosicoesDireitaTodosDeUmaVez3D()
    {
        ConectarTodos();
        Ancora.transform.position = new Vector3(GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetPosDir("Todos De Uma Vez em 3D"),
                                                0f, 0f);

        Ancora.transform.localScale = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetEscalaEsquerdaOuDireita("Todos De Uma Vez em 3D");

        for (int i = 0; i < Lista_de_backgrounds.Count; i++)
        {
            ((GameObject)Lista_de_backgrounds[i]).transform.localEulerAngles = GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetRotacaoCentro("Todos De Uma Vez em 3D");
        }

        DesconectarTodos();
    }

    public void ReposicionandoHeatmapEsquerda()
    {
        Ancora.transform.position = new Vector3(GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetPosEsq("Heatmap"),
                                                0f, 0f);

        Ancora.transform.localScale = new Vector3(1f, 1f, 1f);

        Heatmap.transform.localScale = GetComponent<Controlador>().GetBancoDeDadosModos().
            GetEscalaEsquerdaOuDireita("Heatmap");
    }

    public void ReposicionandoHeatmapCentro()
    {
        Ancora.transform.position = new Vector3(GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetPosCen("Heatmap"),
                                                0f, 0f);

        Ancora.transform.localScale = new Vector3(1f, 1f, 1f);

        Heatmap.transform.localScale = GetComponent<Controlador>().GetBancoDeDadosModos().
            GetEscalaNoCentro("Heatmap");
    }

    public void ReposicionandoHeatmapDireita()
    {
        Ancora.transform.position = new Vector3(GetComponent<Controlador>().
                                                GetBancoDeDadosModos().GetPosDir("Heatmap"),
                                                0f, 0f);

        Ancora.transform.localScale = new Vector3(1f, 1f, 1f);

        Heatmap.transform.localScale = GetComponent<Controlador>().GetBancoDeDadosModos().
            GetEscalaEsquerdaOuDireita("Heatmap");
    }

    public string GetQualLeitor() { return qual_leitor; }
    public void SetQualLeitor(string qual) { qual_leitor = qual; }

    public int GetPrimeiroTempoFIT()
    {
        return objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetInstante(0);
    }

    public int GetPrimeiroTempoBolhas()
    {
        return bd_bolhas.GetTempo(0);
    }

    public int GetUltimoTempo()
    {
        if (qual_leitor == "FIT") return GetUltimoTempoFIT();
        else if (qual_leitor == "Bolhas") return GetUltimoTempoBolhas();
        else return 0;
    }

    public int GetUltimoTempoFIT()
    {

        // Estas duas linhas comentadas abaixo retornam mais instantes do que realmente existem.
        // Por ora, esta gambiarra abaixo, que retorna a quantidade de backgrounds, resolve todos os problemas.
        /*return objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.
                    GetInstante(objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit.GetQuantidadeDeEntradas() - 1);*/
        return objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).lista_de_backgrounds.Count - 1;
    }

    public int GetUltimoTempoBolhas()
    {
        return bd_bolhas.GetTempo(bd_bolhas.GetQuantidadeDeEntradas() - 1);
    }

    public BancoDeDadosFIT GetBancoDeDadosFIT()
    {
        return objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit;
    }

    /// <summary>
    /// Função que retorna dos 1 a 2 conjuntos de dados de jogadores a serem analisados, quais estão ativos.
    /// </summary>
    /// <returns>Retorna um array de 2 booleans.</returns>
    public bool[] GetQuaisJogadoresEstaoAtivos()
    {
        bool[] result = new bool[2];
        result[0] = false; result[1] = false;
        if (objs_jogadores_fit.GetObjetosDeUmJogadorFIT(0).ancora_dos_dados.activeSelf)
        {
            result[0] = true;
        }
        if (objs_jogadores_fit.QuantosJogadores() == 2)
        {
            if (objs_jogadores_fit.GetObjetosDeUmJogadorFIT(1).ancora_dos_dados.activeSelf)
            {
                result[1] = true;
            }
        }

        return result;
    }

    public void RetornarParaTelaInicial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }



    public void NovoLeitor2InitFIT()
    {
        qual_leitor = "FIT";
        NovoLeitor2Init();
    }

    public void NovoLeitor2InitBolhas()
    {
        qual_leitor = "Bolhas";
        NovoLeitor2Init();
    }

    public void NovoLeitor2Init()
    {
        objs_jogadores_fit = new ObjetosDeJogadoresFIT();

        resolucao = new Vector2();
        objetos = new ParaVisualizacao<GameObject>();
        materiais = new ParaVisualizacao<Material>();
        texturas = new ParaVisualizacao<Texture2D>();
        texturas_selecionadas = new ParaVisualizacao<Texture2D>();
        pintar = new Pintar();
        pegar_endereco_de_log = new PegarEnderecoDeLog();
        nomes_e_numeros_de_objetos_do_FIT = new Dictionary<string, int>();
        nomes_e_numeros_de_objetos_do_bolhas = new Dictionary<string, int>();
        posicoes_iniciais_de_personagens_nos_mapas_do_FIT = new List<List<Vector2>>();
    }

    public void NovoLeitor2InitSelecaoFIT()
    {
        qual_leitor = "FIT";

        objs_jogadores_fit = new ObjetosDeJogadoresFIT();
        objs_jogadores_fit.Add(new ObjetosDeUmJogadorFIT());

        resolucao = new Vector2();
        objetos = new ParaVisualizacao<GameObject>();
        materiais = new ParaVisualizacao<Material>();
        texturas = new ParaVisualizacao<Texture2D>();
        texturas_selecionadas = new ParaVisualizacao<Texture2D>();
        pintar = new Pintar();
        Lista_de_objetos = new ArrayList();
        Lista_de_backgrounds = new ArrayList();
        Matrizes_dos_heatmaps = new ArrayList();
        pegar_endereco_de_log = new PegarEnderecoDeLog();
        nomes_e_numeros_de_objetos_do_FIT = new Dictionary<string, int>();
        nomes_e_numeros_de_objetos_do_bolhas = new Dictionary<string, int>();
        posicoes_iniciais_de_personagens_nos_mapas_do_FIT = new List<List<Vector2>>();
    }

}