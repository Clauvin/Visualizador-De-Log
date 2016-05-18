using UnityEngine;
using Basicas;
using System;
using System.IO;
using System.Collections;
using UnityEditor;

/// <summary>
/// Classe NovoLeitor2.
/// <para>Responsável por carregar as informações necessárias para o programa funcionar e fazer o que precisa ser feito com elas.</para>
/// </summary>
public class NovoLeitor2 : MonoBehaviour
{

    protected BancoDeDadosBolhas bd_bolhas;
    protected BancoDeDadosFIT bd_fit;
    protected Vector2 resolucao;
    protected ParaHeatmap<GameObject> objetos;
    protected ParaHeatmap<Material> materiais;

    public ParaHeatmap<Texture2D> texturas;
    public ParaHeatmap<Texture2D> texturas_selecionadas;

    Pintar pintar;
    public ArrayList lista_de_objetos;
    public ArrayList lista_de_backgrounds;
    protected string modo_fit;

    protected ArrayList matrizes_dos_heatmaps;
    public int[] numeros_de_cores;

    GameObject heatmap;

    protected string endereco_de_arquivo;

    public string[] lista_de_nomes_de_objetos_do_FIT = { "1", "2", "3", "4" };
    public string[] lista_de_nomes_de_objetos_do_bolhas = { "Mouse", "Baleia", "Bolha", "Peixe", "Nuvem" };

    private string qual_leitor;

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
    }

    public bool FindFile()
    {

        //Abre uma janela de procurar arquivos .txt para abrir.
        endereco_de_arquivo = EditorUtility.OpenFilePanel("Teste", CarregarEnderecoDeUltimaPaginaChecada(), "txt");

        return Checagem(endereco_de_arquivo);
    }

    public bool LoadStuffFIT(/*int tempo_minimo = 0, int tempo_maximo = int.MaxValue*/)
    {
        //number for number of HeatMaps
        int heatmaps = 1;

        // Handle any problems that might arise when reading the text
        string line;

        bd_fit = new BancoDeDadosFIT();
        // Create a new StreamReader, tell it which file to read and what encoding the file
        // was saved as
        FileStream fs = new FileStream(endereco_de_arquivo, FileMode.Open);
        StreamReader theReader = new StreamReader(fs);
        // Part 1: ignores the [Mode 01]
        line = theReader.ReadLine();

        // Part 2: reads the game events.
        // While there's lines left in the text file, do this:
        do
        {
            string[] entries;
            string[] entry_time;
            string[] entry_char;
            string[] entry_grid_x;
            string[] entry_grid_y;
            int checando_tempo_do_log;

            line = theReader.ReadLine();

            if (line != null)
            {
                // Do whatever you need to do with the text line, it's a string now
                entries = line.Split('=');
                //Correct Example: Time:1=Char:1=GridX:5=GridY:7
                if (entries.Length == 4)
                {
                    entry_time = entries[0].Split(':');
                    checando_tempo_do_log = Convert.ToInt32(entry_time[1]);
                    
                    /*if ((tempo_minimo <= checando_tempo_do_log) && (checando_tempo_do_log <= tempo_maximo))*/
                    //{
                        entry_char = entries[1].Split(':');
                        entry_grid_x = entries[2].Split(':');
                        entry_grid_y = entries[3].Split(':');

                        bd_fit.Add(Int32.Parse(entry_time[1]), Int32.Parse(entry_char[1]),
                                Int32.Parse(entry_grid_x[1]), Int32.Parse(entry_grid_y[1]));

                        if (Int32.Parse(entry_char[1]) == heatmaps) heatmaps++;
                    //}
                }

#if (DEBUG)

                else
                {
                    Debug.Log("NovoLeitor2.LoadStuff - Linha não tinha quatro elementos.");
                }

#endif

            }
        } while (line != null);

        theReader.Close();
        theReader.Dispose();
        fs.Close();
        fs.Dispose();

        for (int i = 0; i < heatmaps; i++)
        {
            matrizes_dos_heatmaps.Add(new HeatMap());
        }
        numeros_de_cores = new int[heatmaps];

        return true;

    }

    public bool LoadStuffBolhas()
    {
        //number of HeatMaps
        //no caso do Bolhas, 1 + Mouse mais 4 objetos = 6
        int heatmaps = 1 + lista_de_nomes_de_objetos_do_bolhas.GetUpperBound(0) + 1;

        // Handle any problems that might arise when reading the text
        string line;

        // Create a new StreamReader, tell it which file to read and what encoding the file
        // was saved as
        bd_bolhas = new BancoDeDadosBolhas();
        FileStream fs = new FileStream(endereco_de_arquivo, FileMode.Open);
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

                //Time:0
                //Mouse
                //GridX:776 e por aí vai.

                if ((entries.Length == 7) && (((string)entries[1]) == "Mouse"))
                {
                    int tempo = Int32.Parse(entries[0].Split(':')[1]);

                    //Os splits dividem os strings entre antes e depois dos ':' presentes
                    //Daí é só pegar o dado necessário, e não sua legenda.
                    bd_bolhas.AddMouse(tempo,
                            entries[1].ToString(),
                            Int32.Parse(entries[2].Split(':')[1]),
                            Int32.Parse(entries[3].Split(':')[1]),
                            entries[4].Split(':')[1].ToString(),
                            entries[5].Split(':')[1].ToString(),
                            entries[6].Split(':')[1].ToString());

                } else if ((entries.Length == 11) && (((string)entries[1]) == "Objeto"))
                {
                    int tempo = Int32.Parse(entries[0].Split(':')[1]);

                    //Os splits dividem os strings entre antes e depois dos ':' presentes
                    //Daí é só pegar o dado necessário, e não sua legenda.
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

#if (DEBUG)

                else
                {
                    Debug.Log("NovoLeitor2.LoadStuff - Linha formatada errada.");
                }

#endif

            }
        } while (line != null);

        theReader.Close();
        theReader.Dispose();
        fs.Close();
        fs.Dispose();

        for (int i = 0; i < heatmaps; i++)
        {
            matrizes_dos_heatmaps.Add(new HeatMap());
        }
        numeros_de_cores = new int[heatmaps];

        return true;
    }

    public bool PrintStuffFIT()
    {
        for (int i = 0; i < bd_fit.GetQuantidadeDeEntradas(); i++)
        {
            Debug.Log(bd_fit.GetTempo(i) + " " + bd_fit.GetPersonagem(i) + " " +
                      bd_fit.GetGridX(i) + " " + bd_fit.GetGridY(i));
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
            } else if (bd_bolhas.GetMouseOuObjeto(i) == "Objeto")
            {
                Debug.Log(bd_bolhas.GetTempo(i) + " " + bd_bolhas.GetMouseOuObjeto(i) + " " +
                       bd_bolhas.GetCoordenadaX(i) + " " + bd_bolhas.GetCoordenadaY(i) + " " +
                       bd_bolhas.GetQualObjeto(i) + " " + bd_bolhas.GetQualFrame(i) + " " + bd_bolhas.GetQuemCriou(i) + " " +
                       bd_bolhas.GetClicando(i) + " " + bd_bolhas.GetSegurando(i) + " " + bd_bolhas.GetArrastando(i));
            }

        }
        return true;

    }

    public void CreateStuffFIT(int tempo_minimo = 0, int tempo_maximo = int.MaxValue)
    {
        Camera acamera = FindObjectOfType<Camera>();
        GameObject objeto = null;
        GameObject background = null;
        Vector3 newpos = new Vector3(0f, 0f, 0f);
        Vector3 newpos_camera = new Vector3(0f, 0f, 0f);
        Material material_do_create = null;
        float x, y, z;
        int i;
        Material material_background = (Material)Resources.Load("Materiais/MaterialBackgroundFIT");
        Material[] rend;

        bool criar_background;
        bool fechar_background;

        Material materialheatmap = new Material((Material)Resources.Load("Materiais/MaterialHeatmap"));

        material_background.mainTexture = (Texture)Instantiate(Resources.Load("Texturas/Grid"));


        // Lê todos os dados do log do FIT...
        for (int j = 0; j < matrizes_dos_heatmaps.Count; j++)
        {

            ((HeatMap)matrizes_dos_heatmaps[j]).ReadPointsFIT(bd_fit, j);
            ((HeatMap)matrizes_dos_heatmaps[j]).AllTheDifferentPoints();
            ((HeatMap)matrizes_dos_heatmaps[j]).OrganizePoints();
            ((HeatMap)matrizes_dos_heatmaps[j]).FillingTheDictionary();
            ((HeatMap)matrizes_dos_heatmaps[j]).PaintingTheHeatmap();
            numeros_de_cores[j] = ((HeatMap)matrizes_dos_heatmaps[j]).HowManyPoints();
        }

        criar_background = true;
        fechar_background = false;

        // Para cada objeto...
        for (i = 0; i < bd_fit.GetQuantidadeDeEntradas(); i++)
        {
            // Controle de quando criar um background novo ou
            // não criá-lo. Backgrounds novos são criados, um para cada posição no tempo diferente.
            if ((i != bd_fit.GetQuantidadeDeEntradas() - 1) && (i != 0))
            {
                if (bd_fit.GetPersonagem(i) > bd_fit.GetPersonagem(i + 1))
                {
                    fechar_background = true;
                }
                if (bd_fit.GetPersonagem(i) < bd_fit.GetPersonagem(i - 1))
                {
                    criar_background = true;
                }
            }
            else if (i == bd_fit.GetQuantidadeDeEntradas() - 1) fechar_background = true;
            else if (i == 0) criar_background = true;
            
            material_do_create = null;

            objeto = Instantiate(objetos.Get("Qualquer Coisa FIT"));
            objeto.AddComponent<AoSerClicadoFIT>();
            
            // :p
            if (objeto == null) Debug.Log("Deu ruim.");

            objeto.name = bd_fit.GetTempo(i).ToString() + " " + bd_fit.GetPersonagem(i).ToString() + " " +
                bd_fit.GetGridX(i).ToString() + " " + bd_fit.GetGridY(i).ToString();

            material_do_create = Instantiate(materiais.Get(bd_fit.GetPersonagem(i).ToString()));

            // Essencialmente, materiais guardam texturas, que é o que queremos.
            // Foi um pouco de exagero fazer um material pra cada objeto, mas
            // isso vai ser útil no futuro para efeitos diferentes para cada objeto,
            // se necessário.
            if (material_do_create == null) { Debug.Log("Deu ruim 2."); }
            else
            {

                objeto.GetComponent<MeshRenderer>().material = material_do_create;

            }

            rend = objeto.GetComponent<MeshRenderer>().materials;
            rend[0].mainTexture = texturas.Get(bd_fit.GetPersonagem(i).ToString());
            objeto.GetComponent<MeshRenderer>().materials = rend;


            // Ponto já criado, agora adicionar dados a ele
            objeto.AddComponent<Dados>();
            objeto.GetComponent<Dados>().Atualizar();
            objeto.GetComponent<Dados>().nome_do_objeto = bd_fit.GetPersonagem(i).ToString();
            objeto.GetComponent<Dados>().tempo = bd_fit.GetTempo(i);
            objeto.GetComponent<Dados>().x_log = bd_fit.GetGridX(i);
            objeto.GetComponent<Dados>().y_log = bd_fit.GetGridY(i);

            lista_de_objetos.Add(objeto);

            // Criando background para os pontos
            if (criar_background) {

                background = Instantiate<GameObject>((GameObject)Resources.Load("Objetos/BackgroundFIT"));
                background.GetComponent<MeshRenderer>().material = Instantiate(material_background);
                background.GetComponent<Conector>().backgroundprincipal = background;

            }

            GameObject objeto2 = Instantiate(objeto);
            objeto2.transform.parent = objeto.transform;
            objeto2.transform.Rotate(new Vector3(0, 0, 180));


            newpos.x = 0;
            y = background.transform.position.y;
            newpos.y = y;
            newpos.z = 0;
            background.transform.position = newpos;

            //adicionando o background onde deve ficar
            if (criar_background)
            {
                background.AddComponent<Dados>();
                background.GetComponent<Dados>().Atualizar();
                background.name = "Background - " + i;
                lista_de_backgrounds.Add(background);                
            }

            // Passar os valores de x, y e tempo para a posição de objetos
            // sem tratar as posições é uma péssima ideia, já que podem haver valores MUITO
            // grande, logo...

            // Primeiro: conseguir a posição da borda esquerda do background
            x = background.GetComponent<Dados>().centro.x - background.GetComponent<Dados>().largura_x / 2;

            // Segundo: a partir daí, achar onde o centro do objeto precisa estar para apenas encostar
            // nessa borda.
            x += (objeto.GetComponent<MeshCollider>().bounds.max.x - objeto.GetComponent<MeshCollider>().bounds.min.x) / 2;

            // Terceiro: finalmente, posicionar o objeto com relação ao background.
            x += bd_fit.GetGridX(i)/32 * (background.GetComponent<Dados>().largura_x/20);

            //O mesmo descrito acima para z... que nesse caso é equivalente a y no plano 2D. Sim, paciência.
            z = background.GetComponent<Dados>().centro.y + background.GetComponent<Dados>().altura_z / 2;

            z -= (objeto.GetComponent<MeshCollider>().bounds.max.z - objeto.GetComponent<MeshCollider>().bounds.min.z) / 2;
                
            z -= (bd_fit.GetGridY(i)/32 * (background.GetComponent<Dados>().altura_z /15));

            newpos = new Vector3(x, y, z);

            objeto.transform.position = newpos;

            y += 0.5f;
            objeto.transform.position = new Vector3(x, y, z);

            background.GetComponent<Conector>().AddPonto(objeto);

            if (fechar_background)
            {
                background.GetComponent<Conector>().Conectar();
            }

            criar_background = false;
            fechar_background = false;

        }

        //ajeitando a câmera
        newpos_camera.y = ((GameObject)lista_de_backgrounds[0]).transform.position.y;
        acamera.transform.position = newpos_camera;
        acamera.transform.Rotate(90f, 0f, 0f);
        acamera.orthographic = true;

        //ajeitando o heatmap
        heatmap = Instantiate<GameObject>((GameObject)Resources.Load("Objetos/BackgroundHeatmap"));
        heatmap.name = "Heatmap";
        materialheatmap.SetTexture("_MainTex", ((HeatMap)matrizes_dos_heatmaps[0]).heatmap);
        heatmap.GetComponent<MeshRenderer>().material = Instantiate(materialheatmap);

        heatmap.transform.position = ((GameObject)lista_de_backgrounds[0]).transform.position + new Vector3(200f, 0, 0);
        GetComponent<Controlador>().MudarTransparenciaDosObjetos(0.2f);

    }

    public void CreateStuffBolhas()
    {
        Camera acamera = FindObjectOfType<Camera>();
        GameObject objeto = null;
        GameObject background = null;
        Vector3 newpos = new Vector3(0f, 0f, 0f);
        Vector3 newpos_camera = new Vector3(0f, 0f, 0f);
        Material material_do_create = null;
        float x, y, z;
        int i;
        Material material_background = (Material)Resources.Load("Materiais/MaterialBackgroundBolhas");
        Material[] rend;

        bool novo_tempo;
        bool vira_novo_tempo;
        int tempo;

        Material material_heatmap = new Material((Material)Resources.Load("Materiais/MaterialHeatmap"));

        material_background.mainTexture = (Texture)Instantiate(Resources.Load("Texturas/Fundo Bolhas Desenhado"));

        // Lê todos os dados do log do Bolhas...
        for (int j = 0; j < matrizes_dos_heatmaps.Count; j++)
        {
            if (j == 0)  ((HeatMap)matrizes_dos_heatmaps[j]).ReadPointsBolhas(bd_bolhas, "Todos");
            else ((HeatMap)matrizes_dos_heatmaps[j]).ReadPointsBolhas(bd_bolhas, lista_de_nomes_de_objetos_do_bolhas[j-1]);
            ((HeatMap)matrizes_dos_heatmaps[j]).AllTheDifferentPoints();
            ((HeatMap)matrizes_dos_heatmaps[j]).OrganizePoints();
            ((HeatMap)matrizes_dos_heatmaps[j]).FillingTheDictionary();
            ((HeatMap)matrizes_dos_heatmaps[j]).PaintingTheHeatmap();
            numeros_de_cores[j] = ((HeatMap)matrizes_dos_heatmaps[j]).HowManyPoints();
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
                if (tempo != bd_bolhas.GetTempo(i + 1)) novo_tempo = true;
            }
            else if (tempo != bd_bolhas.GetTempo(i - 1)) novo_tempo = true;
                if (i == bd_bolhas.GetQuantidadeDeEntradas() - 1) vira_novo_tempo = true;
            else if (tempo != bd_bolhas.GetTempo(i + 1)) vira_novo_tempo = true;


            material_do_create = null;

            objeto = Instantiate(objetos.Get("Qualquer Coisa Bolhas"));
            objeto.AddComponent<AoSerClicadoBolhas>();

            if (objeto == null) Debug.Log("Deu ruim.");

            objeto.name = bd_bolhas.GetTempo(i).ToString() + " " + bd_bolhas.GetQualObjeto(i).ToString() + " " +
                bd_bolhas.GetCoordenadaX(i).ToString() + " " + bd_bolhas.GetCoordenadaY(i).ToString();

            material_do_create = Instantiate(materiais.Get(bd_bolhas.GetQualObjeto(i).ToString()));

            // Essencialmente, materiais guardam texturas, que é o que queremos.
            // Foi um pouco de exagero fazer um material pra cada objeto, mas
            // isso vai ser útil no futuro para efeitos diferentes para cada objeto,
            // se necessário.
            if (material_do_create == null) { Debug.Log("Deu ruim 2."); }
            else
            {

                objeto.GetComponent<MeshRenderer>().material = material_do_create;

            }

            rend = objeto.GetComponent<MeshRenderer>().materials;
            rend[0].mainTexture = texturas.Get(bd_bolhas.GetQualObjeto(i).ToString());
            objeto.GetComponent<MeshRenderer>().materials = rend;


            // Ponto já criado, agora adicionar dados a ele
            objeto.AddComponent<Dados>();
            objeto.GetComponent<Dados>().Atualizar();
            objeto.GetComponent<Dados>().tempo = bd_bolhas.GetTempo(i);
            objeto.GetComponent<Dados>().x_log = bd_bolhas.GetCoordenadaX(i);
            objeto.GetComponent<Dados>().y_log = bd_bolhas.GetCoordenadaY(i);
            objeto.GetComponent<Dados>().nome_do_objeto = bd_bolhas.GetQualObjeto(i);

            if (bd_bolhas.GetMouseOuObjeto(i) == "Mouse")
            {
                if (bd_bolhas.GetArrastando(i) == "S") objeto.GetComponent<Dados>().acao_dele_ou_nele = "Arrastou";
                else if (bd_bolhas.GetClicando(i) == "S") objeto.GetComponent<Dados>().acao_dele_ou_nele = "Clicou";
                else if (bd_bolhas.GetSegurando(i) == "S") objeto.GetComponent<Dados>().acao_dele_ou_nele = "Segurando";
            }
            else if (bd_bolhas.GetMouseOuObjeto(i) == "Objeto")
            {
                if (bd_bolhas.GetArrastando(i) == "S") objeto.GetComponent<Dados>().acao_dele_ou_nele = "Arrastado";
                else if (bd_bolhas.GetClicando(i) == "S") objeto.GetComponent<Dados>().acao_dele_ou_nele = "Clicado";
                else if (bd_bolhas.GetSegurando(i) == "S") objeto.GetComponent<Dados>().acao_dele_ou_nele = "Segurado";
                objeto.GetComponent<Dados>().criado_agora = bd_bolhas.GetCriadoAgora(i);
                objeto.GetComponent<Dados>().quem_criou = bd_bolhas.GetQuemCriou(i);
            }

            lista_de_objetos.Add(objeto);

            //criando background para o par de pontos
            if (novo_tempo)
            {

                background = Instantiate<GameObject>((GameObject)Resources.Load("Objetos/BackgroundBolhas"));
                background.GetComponent<MeshRenderer>().material = Instantiate(material_background);
                background.GetComponent<Conector>().backgroundprincipal = background;

            }

            GameObject objeto2 = Instantiate(objeto);
            objeto2.transform.parent = objeto.transform;
            objeto2.transform.Rotate(new Vector3(0, 0, 180));

            newpos.x = 0;
            y = background.transform.position.y;
            newpos.y = y;
            newpos.z = 0;
            background.transform.position = newpos;

            //adicionando o background onde deve ficar
            if (novo_tempo)
            {
                background.AddComponent<Dados>();
                background.GetComponent<Dados>().Atualizar();
                background.name = "Background - " + i;
                lista_de_backgrounds.Add(background);
            }

            objeto.transform.localScale = new Vector3(texturas.Get(bd_bolhas.GetQualObjeto(i).ToString()).width / resolucao.x,
                                                      texturas.Get(bd_bolhas.GetQualObjeto(i).ToString()).width / resolucao.x,
                                                      texturas.Get(bd_bolhas.GetQualObjeto(i).ToString()).height / resolucao.y);

            // Passar os valores de x, y e tempo para a posição de objetos
            // sem tratar as posições é uma péssima ideia, já que podem haver valores MUITO
            // grande, logo...

            // Primeiro: conseguir a posição da borda esquerda do background
            x = background.GetComponent<Dados>().centro.x - background.GetComponent<Dados>().largura_x / 2;

            // Segundo: a partir daí, achar onde o centro do objeto precisa estar para apenas encostar
            // nessa borda.
            x += (objeto.GetComponent<MeshCollider>().bounds.max.x - objeto.GetComponent<MeshCollider>().bounds.min.x) / 2;

            // Terceiro: finalmente, posicionar o objeto com relação ao background.
            x += bd_bolhas.GetCoordenadaX(i) * (background.GetComponent<Dados>().largura_x / resolucao.x);

            //O mesmo descrito acima para z... que nesse caso é equivalente a y no plano 2D. Sim, paciência.
            z = background.GetComponent<Dados>().centro.y + background.GetComponent<Dados>().altura_z / 2;

            z -= (objeto.GetComponent<MeshCollider>().bounds.max.z - objeto.GetComponent<MeshCollider>().bounds.min.z) / 2; 
                
            z -= (bd_bolhas.GetCoordenadaY(i) * (background.GetComponent<Dados>().altura_z / resolucao.y));

            newpos = new Vector3(x, y, z);

            objeto.transform.position = newpos;

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

        // Ajeitando a câmera
        newpos_camera.y = ((GameObject)lista_de_backgrounds[0]).transform.position.y;
        acamera.transform.position = newpos_camera;
        acamera.transform.Rotate(90f, 0f, 0f);
        acamera.orthographic = true;

        // Ajeitando o heatmap
        heatmap = Instantiate<GameObject>((GameObject)Resources.Load("Objetos/BackgroundHeatmap"));
        heatmap.name = "Heatmap";
        material_heatmap.SetTexture("_MainTex", ((HeatMap)matrizes_dos_heatmaps[0]).heatmap);
        heatmap.GetComponent<MeshRenderer>().material = Instantiate(material_heatmap);

        heatmap.transform.position = ((GameObject)lista_de_backgrounds[0]).transform.position + new Vector3(200f, 0, 0);
        GetComponent<Controlador>().MudarTransparenciaDosObjetos(0.2f);

    }

    public void ControlarAlpha(float alpha)
    {
        for (int i = 0; i < lista_de_backgrounds.Count; i++)
        {
            Color cor = ((GameObject)lista_de_backgrounds[i]).GetComponent<MeshRenderer>().material.GetColor("_Color");
               
            cor.a = alpha;

            ((GameObject)lista_de_backgrounds[i]).GetComponent<MeshRenderer>().material.SetColor("_Color", cor);
        }
    }

    public void GirarBackgrounds(Vector3 coordenadas)
    {
        for (int i = 0; i < lista_de_backgrounds.Count; i++)
        {
            ((GameObject)lista_de_backgrounds[i]).transform.rotation = Quaternion.identity;
            ((GameObject)lista_de_backgrounds[i]).transform.Rotate(coordenadas);
        }
    }

    public void GirarCamera(Vector3 coordenadas)
    {
        GetComponent<Camera>().transform.rotation = Quaternion.identity;
        GetComponent<Camera>().transform.Rotate(coordenadas);
    }

    public void AlterarLayer(int alt)
    {
        for (int i = 0; i < lista_de_backgrounds.Count; i++)
        {
            ((GameObject)lista_de_backgrounds[i]).GetComponent<Conector>().NoLayer(alt);
        }
    }

    public void PosicionarBackgrounds(float dist)
    {
        float yback0 = ((GameObject)lista_de_backgrounds[0]).transform.position.y;
        Vector3 pos;

        for (int i = 1; i < lista_de_backgrounds.Count; i++)
        {
            yback0 -= dist;
            pos = ((GameObject)lista_de_backgrounds[i]).transform.position;
            pos.y = yback0;
            ((GameObject)lista_de_backgrounds[i]).transform.position = pos;
        }
    }

    public void ConectarTodos()
    {
        for (int j = 0; j < lista_de_backgrounds.Count; j++)
        {
            GameObject background = (GameObject)lista_de_backgrounds[j];
            background.GetComponent<Conector>().Conectar();
        }
    }

    public void DesconectarTodos()
    {
        for (int j = 0; j < lista_de_backgrounds.Count; j++)
        {
            GameObject background = (GameObject)lista_de_backgrounds[j];
            background.GetComponent<Conector>().Desconectar();
        }
    }

    public HeatMap GetMatrizHeatmap(int i = 0)
    {
        return ((HeatMap)matrizes_dos_heatmaps[i]);
    }

    public int GetQuantHeatmaps() { return matrizes_dos_heatmaps.Count; }

    public void ChangeTexturaHeatmap(int qual)
    {
        if ((qual >= 0) && (qual < GetQuantHeatmaps()))
        {
            heatmap.GetComponent<MeshRenderer>().material.
                SetTexture("_MainTex", ((HeatMap)matrizes_dos_heatmaps[qual]).heatmap);

        }
        
    }

    public int GetPrimeiroTempo()
    {
        if (qual_leitor == "FIT") return GetPrimeiroTempoFIT();
        else if (qual_leitor == "Bolhas") return GetPrimeiroTempoBolhas();
        else return 0;
    }

    public int GetPrimeiroTempoFIT()
    {
        return bd_fit.GetTempo(0);
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
        return bd_fit.GetTempo(bd_fit.GetQuantidadeDeEntradas() - 1);
    }

    public int GetUltimoTempoBolhas()
    {
        return bd_bolhas.GetTempo(bd_bolhas.GetQuantidadeDeEntradas() - 1);
    }

    public void RetornarParaTelaInicial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private bool Checagem(string endereco)
    {
        string[] checagem;
        string extensao;

        checagem = endereco_de_arquivo.Split('/');
        if (checagem[0] == string.Empty) return false;
        extensao = checagem[checagem.GetUpperBound(0)].Split('.')[1];

        if (extensao == "txt") return true;
        else return false;
    }

    //MELHORAR: No futuro, que hajam dois arquivos. Um para a posição do log do F!T e outro para a posição do log do Bolhas
    public void CriarIniDeUltimaPaginaChecada(string enderecotodo)
    {
        // Handle any problems that might arise when reading the text
        
        string posicaodoarquivo = Path.GetFullPath("local.ini");
        FileStream fs;
        StreamWriter sw;

        string[] enderecoseparado;
        string enderecomodificado = "";

        enderecoseparado = enderecotodo.Split('/');
        for (int i = 0; i < enderecoseparado.GetUpperBound(0); i++)
        {
            enderecomodificado += enderecoseparado[i];
            enderecomodificado += "/";
        }

        // Create a new StreamReader, tell it which file to read and what encoding the file
        // was saved as
        fs = File.Create(posicaodoarquivo);
        sw = new StreamWriter(fs);
        sw.Write(enderecomodificado);

        sw.Dispose();
        sw.Close();
        fs.Dispose();
        fs.Close();
        
    }

    public string CarregarEnderecoDeUltimaPaginaChecada()
    {
        FileStream fs = null;
        StreamReader sr = null;
        string saida;


        string posicaodoarquivo = Path.GetFullPath("local.ini");
        // Handle any problems that might arise when reading the text
        if (File.Exists(posicaodoarquivo))
        {
            fs = File.Open(posicaodoarquivo, FileMode.Open);
            sr = new StreamReader(fs);
            saida = sr.ReadLine();
            sr.Dispose();
            sr.Close();
            fs.Dispose();
            fs.Close();

        }
        else
        {
            saida = Path.GetFullPath("path");
            saida = saida.Split('/')[0] + "/";
        }

        return saida;

    }

    public void NovoLeitor2Init()
    {
        resolucao = new Vector2();
        objetos = new ParaHeatmap<GameObject>();
        materiais = new ParaHeatmap<Material>();
        texturas = new ParaHeatmap<Texture2D>();
        texturas_selecionadas = new ParaHeatmap<Texture2D>();
        pintar = new Pintar();
        lista_de_objetos = new ArrayList();
        lista_de_backgrounds = new ArrayList();
        matrizes_dos_heatmaps = new ArrayList();
    }
    
}