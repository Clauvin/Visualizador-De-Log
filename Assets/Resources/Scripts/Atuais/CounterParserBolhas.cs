using UnityEngine;
using System.Collections;

public class CounterParserBolhas : MonoBehaviour
{

    ArrayList arraytempo;
    ArrayList arraymouseouobjeto;
    ArrayList arrayposicoes;

    ArrayList arrayqual;
    ArrayList arrayframe;
    ArrayList arraycriadoagora;
    ArrayList arrayquemcriou;

    ArrayList arrayclicando;
    ArrayList arraysegurando;
    ArrayList arrayarrastando;

    public int quantasvezes = 100;

    public int tamanhodatelax = 800;
    public int tamanhodatelay = 600;

    string endereco;

    void Awake()
    {
        arraytempo = new ArrayList();
        arraymouseouobjeto = new ArrayList();
        arrayposicoes = new ArrayList();

        arrayqual = new ArrayList();
        arrayframe = new ArrayList();
        arraycriadoagora = new ArrayList();
        arrayquemcriou = new ArrayList();

        arrayclicando = new ArrayList();
        arraysegurando = new ArrayList();
        arrayarrastando = new ArrayList();
    }

    // Use this for initialization
    void Start()
    {

        QualEndereco();
        Gerador();
        Salvador();
        Debug.Log("ok");

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Gerador()
    {
        int timer = 0;
        int x = 0;
        int y = 0;

        string[] objetos = { "Baleia", "Bolha", "Peixe", "Nuvem" };
        string[] criado = { "S", "N" };
        string[] criou = { "Baleia", "Bolha", "Peixe" };

        string[] acao = { "Clicando", "Segurando", "Arrastando" };

        int quantdeobjetos = 1;

        for (int i = 0; i < quantasvezes; i++)
        {
            timer = i;

            if (i % 10 == 1) quantdeobjetos++;

            /*Time:0=Mouse=GridX:15=GridY:20=Clicando:S=Segurando:N=Arrastando:N*/

            //Posicao de Mouse
            arraytempo.Add(i);
            arraymouseouobjeto.Add("Mouse");
            x = (int)(Random.Range(0.0f, 800.0f));
            y = (int)(Random.Range(0.0f, 600.0f));
            arrayposicoes.Add(new Vector2(x, y));
            int rand = (int)Random.Range(0.0f, 3.0f);
            while (rand == 3) { rand = (int)Random.Range(0.0f, 3.0f); }
            if (rand == 0) { arrayclicando.Add("S"); arraysegurando.Add("N"); arrayarrastando.Add("N"); }
            else if (rand == 1) { arrayclicando.Add("N"); arraysegurando.Add("S"); arrayarrastando.Add("N"); }
            else if (rand == 2) { arrayclicando.Add("N"); arraysegurando.Add("N"); arrayarrastando.Add("S"); }

            //Enchendo o resto dos arrays de nada, para evitar problemas de leitura
            arrayqual.Add(string.Empty);
            arrayframe.Add(-1);
            arraycriadoagora.Add(string.Empty);
            arrayquemcriou.Add(string.Empty);

            /*Time:0=Objeto=GridX:20=GridY:25=Baleia=Frame:1=CriadoAgora:N=QuemCriou:?=Clicado:S=Segurado:N=Arrastado:S*/

            //Posicao de Objetos
            for (int j = 0; j < quantdeobjetos; j++)
            {
                arraytempo.Add(i);
                arraymouseouobjeto.Add("Objeto");
                x = (int)(Random.Range(0.0f, 800.0f));
                y = (int)(Random.Range(0.0f, 600.0f));
                arrayposicoes.Add(new Vector2(x, y));

                rand = (int)Random.Range(0.0f, 4.0f);
                while (rand == 4) { rand = (int)Random.Range(0.0f, 4.0f); }
                arrayqual.Add(objetos[rand]);

                rand = (int)Random.Range(0.0f, 2.0f);
                arrayframe.Add(rand);

                rand = (int)Random.Range(0.0f, 2.0f);
                while (rand == 2) { rand = (int)Random.Range(0.0f, 2.0f); }
                arraycriadoagora.Add(rand);


                rand = (int)Random.Range(0.0f, 3.0f);
                while (rand == 3) { rand = (int)Random.Range(0.0f, 3.0f); }
                arrayquemcriou.Add(objetos[rand]);

                rand = (int)Random.Range(0.0f, 3.0f);
                while (rand == 3) { rand = (int)Random.Range(0.0f, 3.0f); }
                if (rand == 0) { arrayclicando.Add("S"); arraysegurando.Add("N"); arrayarrastando.Add("N"); }
                else if (rand == 1) { arrayclicando.Add("N"); arraysegurando.Add("S"); arrayarrastando.Add("N"); }
                else if (rand == 2) { arrayclicando.Add("N"); arraysegurando.Add("N"); arrayarrastando.Add("S"); }

            }

        }

    }

    void Salvador()
    {
        string linha;
        string mouseobjeto;
        Vector2 posicao;

        /*Time:0=Mouse=GridX:15=GridY:20=Clicando:S=Segurando:N=Arrastando:N*/
        /*Time:0=Objeto=GridX:20=GridY:25=Baleia=Frame:1=CriadoAgora:N=QuemCriou:?=Clicado:S=Segurado:N=Arrastado:S*/

        System.IO.StreamWriter file = new System.IO.StreamWriter(endereco);
        file.WriteLine("[Mode Bolhas]");
        for (int i = 0; i < arraytempo.Count; i++)
        {
            linha = string.Empty;
            mouseobjeto = string.Empty;
            posicao = new Vector2();

            linha += "Time:" + arraytempo[i];
            linha += "=";

            mouseobjeto = (string)arraymouseouobjeto[i];
            linha += mouseobjeto;
            linha += "=";

            posicao = (Vector2)arrayposicoes[i];

            linha += "GridX:" + posicao.x + "=GridY:" + posicao.y;
            linha += "=";

            if (mouseobjeto == "Mouse")
            {
                linha += "Clicando:" + arrayclicando[i] + "=Segurando:" + arraysegurando[i] + "=Arrastando:" + arrayarrastando[i];
            }
            else if (mouseobjeto == "Objeto")
            {
                linha += arrayqual[i];
                linha += "=";

                linha += "Frame:" + arrayframe[i];
                linha += "=";

                linha += "CriadoAgora:" + arraycriadoagora[i];
                linha += "=";

                linha += "QuemCriou:" + arrayquemcriou[i];
                linha += "=";

                linha += "Clicou:" + arrayclicando[i] + "=Segurou:" + arraysegurando[i] + "=Arrastou:" + arrayarrastando[i];
            }
            file.WriteLine(linha);
        }
        file.Close();
    }

    void QualEndereco()
    {
        endereco = "C:\\Teste\\TesteBolhas.txt";

    }
}