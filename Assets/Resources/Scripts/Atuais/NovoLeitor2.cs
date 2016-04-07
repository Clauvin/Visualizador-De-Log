using UnityEngine;
using Basicas;
using System;
using System.IO;
using System.Collections;
using UnityEditor;

public class NovoLeitor2 : MonoBehaviour
{

    protected BancoDeDadosBolhas bdbolhas;
    protected BancoDeDadosFIT bdfit;
    protected Vector2 resolucao;
    protected ParaHeatmap<GameObject> objetos;
    protected ParaHeatmap<Material> materiais;

    public ParaHeatmap<Texture2D> texturas;
    public ParaHeatmap<Texture2D> texturasselecionadas;

    Pintar pintar;
    public ArrayList listadepontos;
    public ArrayList listadebackgrounds;
    protected string modofit;

    protected ArrayList matrizesdosheatmaps;
    public int[] numerosdecores;

    GameObject heatmap;

    protected string enderecodearquivo;

    public string[] listadeobjetosdobolhas = { "Mouse", "Baleia", "Bolha", "Peixe", "Nuvem" };

    private string qualleitor;

    public void StartFIT()
    {
        qualleitor = "FIT";

        objetos.Add("Qualquer Coisa FIT", (GameObject)Resources.Load("Objetos/Qualquer Coisa FIT"));

        materiais.Add("1", (Material)Resources.Load("Materiais/MaterialCharacter"));
        materiais.Add("2", (Material)Resources.Load("Materiais/MaterialCharacter"));
        materiais.Add("3", (Material)Resources.Load("Materiais/MaterialCharacter"));
        materiais.Add("4", (Material)Resources.Load("Materiais/MaterialCharacter"));

        texturas.Add("1", (Texture2D)Resources.Load("Texturas/P1"));
        texturas.Add("2", (Texture2D)Resources.Load("Texturas/P2"));
        texturas.Add("3", (Texture2D)Resources.Load("Texturas/P3"));
        texturas.Add("4", (Texture2D)Resources.Load("Texturas/P4"));

        texturasselecionadas.Add("1", (Texture2D)Resources.Load("Texturas/P1 Selecionado"));
        texturasselecionadas.Add("2", (Texture2D)Resources.Load("Texturas/P2 Selecionado"));
        texturasselecionadas.Add("3", (Texture2D)Resources.Load("Texturas/P3 Selecionado"));
        texturasselecionadas.Add("4", (Texture2D)Resources.Load("Texturas/P4 Selecionado"));

    }

    public void StartBolhas()
    {
        qualleitor = "Bolhas";

        objetos.Add("Qualquer Coisa Bolhas", (GameObject)Resources.Load("Objetos/Qualquer Coisa Bolhas"));

        materiais.Add("Mouse", (Material)Resources.Load("Materiais/MaterialMouse"));
        materiais.Add("Baleia", (Material)Resources.Load("Materiais/MaterialBaleia"));
        materiais.Add("Bolha", (Material)Resources.Load("Materiais/MaterialBolha"));
        materiais.Add("Peixe", (Material)Resources.Load("Materiais/MaterialPeixe"));
        materiais.Add("Nuvem", (Material)Resources.Load("Materiais/MaterialNuvem"));

        texturas.Add("Mouse", (Texture2D)Resources.Load("Texturas/P0"));
        texturas.Add("Baleia", (Texture2D)Resources.Load("Texturas/Baleia"));
        texturas.Add("Bolha", (Texture2D)Resources.Load("Texturas/Bolha"));
        texturas.Add("Peixe", (Texture2D)Resources.Load("Texturas/P3"));
        texturas.Add("Nuvem", (Texture2D)Resources.Load("Texturas/Nuvem"));

        texturasselecionadas.Add("Mouse", (Texture2D)Resources.Load("Texturas/P0 Selecionado"));
        texturasselecionadas.Add("Baleia", (Texture2D)Resources.Load("Texturas/Baleia Clicada"));
        texturasselecionadas.Add("Bolha", (Texture2D)Resources.Load("Texturas/Bolha Clicada"));
        texturasselecionadas.Add("Peixe", (Texture2D)Resources.Load("Texturas/P3 Selecionado"));
        texturasselecionadas.Add("Nuvem", (Texture2D)Resources.Load("Texturas/Nuvem Clicada"));
    }

    public bool FindFile()
    {

        enderecodearquivo = EditorUtility.OpenFilePanel("Teste", CarregarEnderecoDeUltimaPaginaChecada(), "txt");

        return Checagem(enderecodearquivo);
    }

    public bool LoadStuffFIT()
    {
        //number for number of HeatMaps
        int heatmaps = 1;

        // Handle any problems that might arise when reading the text
        string line;
        // Create a new StreamReader, tell it which file to read and what encoding the file
        // was saved as

        bdfit = new BancoDeDadosFIT();
        FileStream fs = new FileStream(enderecodearquivo, FileMode.Open);
        StreamReader theReader = new StreamReader(fs);
        // Part 1: ignores the [Mode 01]
        line = theReader.ReadLine();

        // Part 2: reads the game events.
        // While there's lines left in the text file, do this:
        do
        {
            string[] entries;
            string[] entrytime;
            string[] entrychar;
            string[] entrygridx;
            string[] entrygridy;

            line = theReader.ReadLine();

            if (line != null)
            {
                // Do whatever you need to do with the text line, it's a string now
                // In this example, I split it into arguments based on comma
                // deliniators, then send that array to DoStuff()
                entries = line.Split('=');
                //Correct Example: Time:1=Char:1=GridX:5=GridY:7
                if (entries.Length == 4)
                {
                    entrytime = entries[0].Split(':');
                    entrychar = entries[1].Split(':');
                    entrygridx = entries[2].Split(':');
                    entrygridy = entries[3].Split(':');

                    bdfit.Add(Int32.Parse(entrytime[1]), Int32.Parse(entrychar[1]),
                            Int32.Parse(entrygridx[1]), Int32.Parse(entrygridy[1]));

                    if (Int32.Parse(entrychar[1]) == heatmaps) heatmaps++;
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
            matrizesdosheatmaps.Add(new MatrizHeatMap());
        }
        numerosdecores = new int[heatmaps];

        return true;

    }

    public bool LoadStuffBolhas()
    {
        //number for number of HeatMaps
        //no caso do Bolhas, 1 + 5 do Mouse mais 4 objetos = 6
        int heatmaps = 1 + listadeobjetosdobolhas.GetUpperBound(0) + 1;

        // Handle any problems that might arise when reading the text
        string line;
        // Create a new StreamReader, tell it which file to read and what encoding the file
        // was saved as

        bdbolhas = new BancoDeDadosBolhas();
        FileStream fs = new FileStream(enderecodearquivo, FileMode.Open);
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

            if (line != null)
            {
                // Do whatever you need to do with the text line, it's a string now
                // In this example, I split it into arguments based on comma
                // deliniators, then send that array to DoStuff()
                string[] entries = line.Split('=');
                if ((entries.Length == 7) && (((string)entries[1]) == "Mouse"))
                {

                    //TEMPO=MOUSE=X=Y=CLICANDO=SEGURANDO=ARRASTANDO
                    bdbolhas.AddMouse(Int32.Parse(entries[0].Split(':')[1]),
                            entries[1].ToString(),
                            Int32.Parse(entries[2].Split(':')[1]),
                            Int32.Parse(entries[3].Split(':')[1]),
                            entries[4].Split(':')[1].ToString(),
                            entries[5].Split(':')[1].ToString(),
                            entries[6].Split(':')[1].ToString());

                } else if ((entries.Length == 11) && (((string)entries[1]) == "Objeto"))
                {
                    bdbolhas.AddObjeto(Int32.Parse(entries[0].Split(':')[1]),
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
            matrizesdosheatmaps.Add(new MatrizHeatMap());
        }
        numerosdecores = new int[heatmaps];

        return true;
    }

    public bool PrintStuffFIT()
    {
        for (int i = 0; i < bdfit.GetQuantidadeDeEntradas(); i++)
        {
            Debug.Log(bdfit.GetTempo(i) + " " + bdfit.GetPersonagem(i) + " " +
                      bdfit.GetGridX(i) + " " + bdfit.GetGridY(i));
        }
        return true;

    }

    public bool PrintStuffBolhas()
    {
        for (int i = 0; i < bdbolhas.GetQuantidadeDeEntradas(); i++)
        {
            if (bdbolhas.GetMouseOuObjeto(i) == "Mouse")
            {
                Debug.Log(bdbolhas.GetTempo(i) + " " + bdbolhas.GetMouseOuObjeto(i) + " " +
                       bdbolhas.GetCoordenadaX(i) + " " + bdbolhas.GetCoordenadaY(i) + " " +
                       bdbolhas.GetClicando(i) + " " + bdbolhas.GetSegurando(i) + " " + bdbolhas.GetArrastando(i));
            } else if (bdbolhas.GetMouseOuObjeto(i) == "Objeto")
            {
                Debug.Log(bdbolhas.GetTempo(i) + " " + bdbolhas.GetMouseOuObjeto(i) + " " +
                       bdbolhas.GetCoordenadaX(i) + " " + bdbolhas.GetCoordenadaY(i) + " " +
                       bdbolhas.GetQualObjeto(i) + " " + bdbolhas.GetQualFrame(i) + " " + bdbolhas.GetQuemCriou(i) + " " +
                       bdbolhas.GetClicando(i) + " " + bdbolhas.GetSegurando(i) + " " + bdbolhas.GetArrastando(i));
            }

        }
        return true;

    }

    public void CreateStuffFIT()
    {
        Camera acamera = FindObjectOfType<Camera>();
        GameObject objeto = null;
        GameObject background = null;
        Vector3 newpos = new Vector3(0f, 0f, 0f);
        Vector3 newposcamera = new Vector3(0f, 0f, 0f);
        Material materialdocreate = null;
        float x, y, z;
        int i;
        Material materialbackground = (Material)Resources.Load("Materiais/MaterialBackgroundFIT");
        Material[] rend;

        bool criarbackground;
        bool fecharbackground;

        Material materialheatmap = new Material((Material)Resources.Load("Materiais/MaterialHeatmap"));

        materialbackground.mainTexture = (Texture)Instantiate(Resources.Load("Texturas/Grid"));

        for (int j = 0; j < matrizesdosheatmaps.Count; j++)
        {
            //Lê e organiza os pontos dos heatmaps
            ((MatrizHeatMap)matrizesdosheatmaps[j]).ReadPointsFIT(bdfit, j);
            ((MatrizHeatMap)matrizesdosheatmaps[j]).AllTheDifferentPoints();
            ((MatrizHeatMap)matrizesdosheatmaps[j]).OrganizePoints();
            ((MatrizHeatMap)matrizesdosheatmaps[j]).FillingTheDictionary();
            ((MatrizHeatMap)matrizesdosheatmaps[j]).FillingTheHeatmap();
            //((MatrizHeatMap)matrizesdosheatmaps[j]).PreencherOArrayOQueÉMUITOLENTO();
            //((MatrizHeatMap)matrizesdosheatmaps[j]).FillingTheHeatmapSlow();
            numerosdecores[j] = ((MatrizHeatMap)matrizesdosheatmaps[j]).HowManyPoints();
        }

        criarbackground = true;
        fecharbackground = false;

        for (i = 0; i < bdfit.GetQuantidadeDeEntradas(); i++)
        {

            if ((i != bdfit.GetQuantidadeDeEntradas() - 1) && (i != 0))
            {
                if (bdfit.GetPersonagem(i) > bdfit.GetPersonagem(i + 1))
                {
                    fecharbackground = true;
                }
                if (bdfit.GetPersonagem(i) < bdfit.GetPersonagem(i - 1))
                {
                    criarbackground = true;
                }
            }
            else if (i == bdfit.GetQuantidadeDeEntradas() - 1) fecharbackground = true;
            else if (i == 0) criarbackground = true;
            
            materialdocreate = null;

            objeto = Instantiate(objetos.Get("Qualquer Coisa FIT"));
            objeto.AddComponent<AoSerClicado>();
            
            if (objeto == null) Debug.Log("Deu ruim.");

            objeto.name = bdfit.GetTempo(i).ToString() + " " + bdfit.GetPersonagem(i).ToString() + " " +
                bdfit.GetGridX(i).ToString() + " " + bdfit.GetGridY(i).ToString();

            materialdocreate = Instantiate(materiais.Get(bdfit.GetPersonagem(i).ToString()));

            if (materialdocreate == null) { Debug.Log("Deu ruim 2."); }
            else
            {

                objeto.GetComponent<MeshRenderer>().material = materialdocreate;

            }

            rend = objeto.GetComponent<MeshRenderer>().materials;
            rend[0].mainTexture = texturas.Get(bdfit.GetPersonagem(i).ToString());
            //rend[1].mainTexture = texturas.Get(bdfit.GetPersonagem(i).ToString());
            objeto.GetComponent<MeshRenderer>().materials = rend;


            //ponto já criado, agora adicionar dados a ele
            objeto.AddComponent<Dados>();
            objeto.GetComponent<Dados>().Atualizar();
            objeto.GetComponent<Dados>().personagem = bdfit.GetPersonagem(i).ToString();
            objeto.GetComponent<Dados>().tempo = bdfit.GetTempo(i);
            objeto.GetComponent<Dados>().xlog = bdfit.GetGridX(i);
            objeto.GetComponent<Dados>().ylog = bdfit.GetGridY(i);

            listadepontos.Add(objeto);

            //criando background para o par de pontos
            if (criarbackground) {

                background = Instantiate<GameObject>((GameObject)Resources.Load("Objetos/BackgroundFIT"));
                background.GetComponent<MeshRenderer>().material = Instantiate(materialbackground);
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
            if (criarbackground)
            {
                background.AddComponent<Dados>();
                background.GetComponent<Dados>().Atualizar();
                background.name = "Background - " + i;
                listadebackgrounds.Add(background);                
            }

            // Explaining this:
            // Passing the values of x, y and time to the position of the objects is a VERY bad idea,
            //      since they can be REALLY big values for the camera, so...
            x = background.GetComponent<Dados>().centro.x - background.GetComponent<Dados>().largurax / 2 +
                (objeto.GetComponent<MeshCollider>().bounds.max.x - objeto.GetComponent<MeshCollider>().bounds.min.x)/2 +
                    bdfit.GetGridX(i)/32 * (background.GetComponent<Dados>().largurax/20);

            z = background.GetComponent<Dados>().centro.y + background.GetComponent<Dados>().alturaz / 2 -
                (objeto.GetComponent<MeshCollider>().bounds.max.z - objeto.GetComponent<MeshCollider>().bounds.min.z)/2 -
                (bdfit.GetGridY(i)/32 * (background.GetComponent<Dados>().alturaz /15));

            newpos = new Vector3(x, y, z);

            objeto.transform.position = newpos;

            y += 0.5f;
            objeto.transform.position = new Vector3(x, y, z);

            background.GetComponent<Conector>().AddPonto(objeto);

            if (fecharbackground)
            {
                background.GetComponent<Conector>().Conectar();
            }

        }

        //ajeitando a câmera
        newposcamera.y = ((GameObject)listadebackgrounds[0]).transform.position.y;
        acamera.transform.position = newposcamera;
        acamera.transform.Rotate(90f, 0f, 0f);
        acamera.orthographic = true;

        //ajeitando o heatmap
        heatmap = Instantiate<GameObject>((GameObject)Resources.Load("Objetos/BackgroundHeatmap"));
        heatmap.name = "Heatmap";
        materialheatmap.SetTexture("_MainTex", ((MatrizHeatMap)matrizesdosheatmaps[0]).heatmap);
        heatmap.GetComponent<MeshRenderer>().material = Instantiate(materialheatmap);

        heatmap.transform.position = ((GameObject)listadebackgrounds[0]).transform.position + new Vector3(200f, 0, 0);

    }

    public void CreateStuffBolhas()
    {
        Camera acamera = FindObjectOfType<Camera>();
        GameObject objeto = null;
        GameObject background = null;
        Vector3 newpos = new Vector3(0f, 0f, 0f);
        Vector3 newposcamera = new Vector3(0f, 0f, 0f);
        Material materialdocreate = null;
        float x, y, z;
        int i;
        Material materialbackground = (Material)Resources.Load("Materiais/MaterialBackgroundBolhas");
        Material[] rend;

        bool novotempo;
        bool viranovotempo;
        int tempo;

        Material materialheatmap = new Material((Material)Resources.Load("Materiais/MaterialHeatmap"));

        materialbackground.mainTexture = (Texture)Instantiate(Resources.Load("Texturas/Fundo Bolhas Desenhado"));

        for (int j = 0; j < matrizesdosheatmaps.Count; j++)
        {
            //Lê e organiza os pontos dos heatmaps
            if (j == 0)  ((MatrizHeatMap)matrizesdosheatmaps[j]).ReadPointsBolhas(bdbolhas, "Todos");
            else ((MatrizHeatMap)matrizesdosheatmaps[j]).ReadPointsBolhas(bdbolhas, listadeobjetosdobolhas[j-1]);
            ((MatrizHeatMap)matrizesdosheatmaps[j]).AllTheDifferentPoints();
            ((MatrizHeatMap)matrizesdosheatmaps[j]).OrganizePoints();
            ((MatrizHeatMap)matrizesdosheatmaps[j]).FillingTheDictionary();
            ((MatrizHeatMap)matrizesdosheatmaps[j]).FillingTheHeatmap();
            numerosdecores[j] = ((MatrizHeatMap)matrizesdosheatmaps[j]).HowManyPoints();
        }

        tempo = bdbolhas.GetTempo(0);
        novotempo = true;
        viranovotempo = false;

        for (i = 0; i < bdbolhas.GetQuantidadeDeEntradas(); i++)
        {
            //refatorar esse código...
            tempo = bdbolhas.GetTempo(i);
            if (i != bdbolhas.GetQuantidadeDeEntradas() - 1)
            {
                if (tempo != bdbolhas.GetTempo(i + 1)) novotempo = true;
            }
            else if (tempo != bdbolhas.GetTempo(i - 1)) novotempo = true;
                if (i == bdbolhas.GetQuantidadeDeEntradas() - 1) viranovotempo = true;
            else if (tempo != bdbolhas.GetTempo(i + 1)) viranovotempo = true;


            materialdocreate = null;

            objeto = Instantiate(objetos.Get("Qualquer Coisa Bolhas"));
            objeto.AddComponent<AoSerClicado>();

            if (objeto == null) Debug.Log("Deu ruim.");

            objeto.name = bdbolhas.GetTempo(i).ToString() + " " + bdbolhas.GetQualObjeto(i).ToString() + " " +
                bdbolhas.GetCoordenadaX(i).ToString() + " " + bdbolhas.GetCoordenadaY(i).ToString();

            materialdocreate = Instantiate(materiais.Get(bdbolhas.GetQualObjeto(i).ToString()));

            if (materialdocreate == null) { Debug.Log("Deu ruim 2."); }
            else
            {

                objeto.GetComponent<MeshRenderer>().material = materialdocreate;

            }

            rend = objeto.GetComponent<MeshRenderer>().materials;
            rend[0].mainTexture = texturas.Get(bdbolhas.GetQualObjeto(i).ToString());
            objeto.GetComponent<MeshRenderer>().materials = rend;


            //ponto já criado, agora adicionar dados a ele
            objeto.AddComponent<Dados>();
            objeto.GetComponent<Dados>().Atualizar();
            objeto.GetComponent<Dados>().tempo = bdbolhas.GetTempo(i);
            objeto.GetComponent<Dados>().xlog = bdbolhas.GetCoordenadaX(i);
            objeto.GetComponent<Dados>().ylog = bdbolhas.GetCoordenadaY(i);
            objeto.GetComponent<Dados>().personagem = bdbolhas.GetQualObjeto(i);

            if (bdbolhas.GetMouseOuObjeto(i) == "Mouse")
            {
                if (bdbolhas.GetArrastando(i) == "S") objeto.GetComponent<Dados>().oquefez = "Arrastou";
                else if (bdbolhas.GetClicando(i) == "S") objeto.GetComponent<Dados>().oquefez = "Clicou";
                else if (bdbolhas.GetSegurando(i) == "S") objeto.GetComponent<Dados>().oquefez = "Segurando";
            }
            else if (bdbolhas.GetMouseOuObjeto(i) == "Objeto")
            {
                if (bdbolhas.GetArrastando(i) == "S") objeto.GetComponent<Dados>().oquefez = "Arrastado";
                else if (bdbolhas.GetClicando(i) == "S") objeto.GetComponent<Dados>().oquefez = "Clicado";
                else if (bdbolhas.GetSegurando(i) == "S") objeto.GetComponent<Dados>().oquefez = "Segurado";
                objeto.GetComponent<Dados>().criadoagora = bdbolhas.GetCriadoAgora(i);
                objeto.GetComponent<Dados>().quemcriou = bdbolhas.GetQuemCriou(i);
            }

            listadepontos.Add(objeto);

            //criando background para o par de pontos
            if (novotempo)
            {

                background = Instantiate<GameObject>((GameObject)Resources.Load("Objetos/BackgroundBolhas"));
                background.GetComponent<MeshRenderer>().material = Instantiate(materialbackground);
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
            if (novotempo)
            {
                background.AddComponent<Dados>();
                background.GetComponent<Dados>().Atualizar();
                background.name = "Background - " + i;
                listadebackgrounds.Add(background);
            }

            Debug.Log("string " + bdbolhas.GetQualObjeto(i).ToString());
            Debug.Log("textura " + texturas.Get(bdbolhas.GetQualObjeto(i).ToString()));
            Debug.Log("text " + texturas.Get(bdbolhas.GetQualObjeto(i).ToString()).width);
            Debug.Log("res " + resolucao.x);
            Debug.Log("---------------------");
            objeto.transform.localScale = new Vector3(texturas.Get(bdbolhas.GetQualObjeto(i).ToString()).width / resolucao.x,
                                                      texturas.Get(bdbolhas.GetQualObjeto(i).ToString()).width / resolucao.x,
                                                      texturas.Get(bdbolhas.GetQualObjeto(i).ToString()).height / resolucao.y);

            // Explaining this:
            // Passing the values of x, y and time to the position of the objects is a VERY bad idea,
            //      since they can be REALLY big values for the camera, so...
            x = background.GetComponent<Dados>().centro.x - background.GetComponent<Dados>().largurax / 2 +
                (objeto.GetComponent<MeshCollider>().bounds.max.x - objeto.GetComponent<MeshCollider>().bounds.min.x) / 2 +
                    bdbolhas.GetCoordenadaX(i) * (background.GetComponent<Dados>().largurax / resolucao.x);

            z = background.GetComponent<Dados>().centro.y + background.GetComponent<Dados>().alturaz / 2 -
                (objeto.GetComponent<MeshCollider>().bounds.max.z - objeto.GetComponent<MeshCollider>().bounds.min.z) / 2 -
                (bdbolhas.GetCoordenadaY(i) * (background.GetComponent<Dados>().alturaz / resolucao.y));

            newpos = new Vector3(x, y, z);

            objeto.transform.position = newpos;

            y += 0.5f;
            objeto.transform.position = new Vector3(x, y, z);

            background.GetComponent<Conector>().AddPonto(objeto);

            if (viranovotempo)
            {
                background.GetComponent<Conector>().Conectar();
            }

            novotempo = false;
            viranovotempo = false;

        }

        //ajeitando a câmera
        newposcamera.y = ((GameObject)listadebackgrounds[0]).transform.position.y;
        acamera.transform.position = newposcamera;
        acamera.transform.Rotate(90f, 0f, 0f);
        acamera.orthographic = true;

        //ajeitando o heatmap
        heatmap = Instantiate<GameObject>((GameObject)Resources.Load("Objetos/BackgroundHeatmap"));
        heatmap.name = "Heatmap";
        materialheatmap.SetTexture("_MainTex", ((MatrizHeatMap)matrizesdosheatmaps[0]).heatmap);
        heatmap.GetComponent<MeshRenderer>().material = Instantiate(materialheatmap);

        heatmap.transform.position = ((GameObject)listadebackgrounds[0]).transform.position + new Vector3(200f, 0, 0);

    }

    public void ControlarAlpha(float alpha)
    {
        for (int i = 0; i < listadebackgrounds.Count; i++)
        {
            Color cor = ((GameObject)listadebackgrounds[i]).GetComponent<MeshRenderer>().material.GetColor("_Color");
               
            cor.a = alpha;

            ((GameObject)listadebackgrounds[i]).GetComponent<MeshRenderer>().material.SetColor("_Color", cor);
        }
    }

    public void GirarBackgrounds(Vector3 coordenadas)
    {
        for (int i = 0; i < listadebackgrounds.Count; i++)
        {
            ((GameObject)listadebackgrounds[i]).transform.rotation = Quaternion.identity;
            ((GameObject)listadebackgrounds[i]).transform.Rotate(coordenadas);
        }
    }

    public void GirarCamera(Vector3 coordenadas)
    {
        GetComponent<Camera>().transform.rotation = Quaternion.identity;
        GetComponent<Camera>().transform.Rotate(coordenadas);
    }

    public void AlterarLayer(int alt)
    {
        for (int i = 0; i < listadebackgrounds.Count; i++)
        {
            ((GameObject)listadebackgrounds[i]).GetComponent<Conector>().NoLayer(alt);
        }
    }

    public void PosicionarBackgrounds(float dist)
    {
        float yback0 = ((GameObject)listadebackgrounds[0]).transform.position.y;
        Vector3 pos;

        for (int i = 1; i < listadebackgrounds.Count; i++)
        {
            yback0 -= dist;
            pos = ((GameObject)listadebackgrounds[i]).transform.position;
            pos.y = yback0;
            ((GameObject)listadebackgrounds[i]).transform.position = pos;
        }
    }

    public void ConectarTodos()
    {
        for (int j = 0; j < listadebackgrounds.Count; j++)
        {
            GameObject background = (GameObject)listadebackgrounds[j];
            background.GetComponent<Conector>().Conectar();
        }
    }

    public void DesconectarTodos()
    {
        for (int j = 0; j < listadebackgrounds.Count; j++)
        {
            GameObject background = (GameObject)listadebackgrounds[j];
            background.GetComponent<Conector>().Desconectar();
        }
    }

    public MatrizHeatMap GetMatrizHeatmap(int i = 0)
    {
        return ((MatrizHeatMap)matrizesdosheatmaps[i]);
    }

    public int GetQuantHeatmaps() { return matrizesdosheatmaps.Count; }

    public void ChangeTexturaHeatmap(int qual)
    {
        if ((qual >= 0) && (qual < GetQuantHeatmaps()))
        {
            heatmap.GetComponent<MeshRenderer>().material.
                SetTexture("_MainTex", ((MatrizHeatMap)matrizesdosheatmaps[qual]).heatmap);

        }
        
    }

    public int GetUltimoTempo()
    {
        if (qualleitor == "FIT") return GetUltimoTempoFIT();
        else if (qualleitor == "Bolhas") return GetUltimoTempoBolhas();
        else return 0;
    }

    public int GetUltimoTempoFIT()
    {
        return bdfit.GetTempo(bdfit.GetQuantidadeDeEntradas() - 1);
    }

    public int GetUltimoTempoBolhas()
    {
        return bdbolhas.GetTempo(bdbolhas.GetQuantidadeDeEntradas() - 1);
    }

    public void RetornarParaTelaInicial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private bool Checagem(string endereco)
    {
        string[] checagem;
        string extensao;

        checagem = enderecodearquivo.Split('/');
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
        texturasselecionadas = new ParaHeatmap<Texture2D>();
        pintar = new Pintar();
        listadepontos = new ArrayList();
        listadebackgrounds = new ArrayList();
        matrizesdosheatmaps = new ArrayList();
    }
    
}