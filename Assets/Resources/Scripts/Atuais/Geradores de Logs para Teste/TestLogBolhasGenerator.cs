using UnityEngine;
using System.Collections;

/// <summary>
/// Classe TestLogBolhasGenerator. 
/// <para>Responsável por criar um log aleatório no formato do do Bolhas para testes</para>
/// </summary>
public class TestLogBolhasGenerator : MonoBehaviour
{

    ArrayList array_tempo_de_objetos;
    ArrayList array_mouse_ou_objeto;
    ArrayList array_posicoes_de_objetos;

    ArrayList array_qual_objeto;
    ArrayList array_frame_de_animacao_do_objeto;
    ArrayList array_foi_criado_agora;
    ArrayList array_quem_criou_o_objeto;

    ArrayList array_mouse_clicando;
    ArrayList array_mouse_segurando;
    ArrayList array_mouse_arrastando;

    public int quantasvezes = 100;

    public int tamanhodatelax = 800;
    public int tamanhodatelay = 600;

    string endereco;

    void Awake()
    {
        array_tempo_de_objetos = new ArrayList();
        array_mouse_ou_objeto = new ArrayList();
        array_posicoes_de_objetos = new ArrayList();

        array_qual_objeto = new ArrayList();
        array_frame_de_animacao_do_objeto = new ArrayList();
        array_foi_criado_agora = new ArrayList();
        array_quem_criou_o_objeto = new ArrayList();

        array_mouse_clicando = new ArrayList();
        array_mouse_segurando = new ArrayList();
        array_mouse_arrastando = new ArrayList();
    }

    // Use this for initialization
    void Start()
    {

        Onde_Salvar_Texto_De_Log();
        Gerador_De_Dados_De_Log();
        Salvador_De_Texto_De_Log();

    }

    void Gerador_De_Dados_De_Log()
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

            //Posicao de Mouse
            array_tempo_de_objetos.Add(i);
            array_mouse_ou_objeto.Add("Mouse");
            x = (int)(Random.Range(0.0f, 800.0f));
            y = (int)(Random.Range(0.0f, 600.0f));
            array_posicoes_de_objetos.Add(new Vector2(x, y));
            int rand = (int)Random.Range(0.0f, 3.0f);
            while (rand == 3) { rand = (int)Random.Range(0.0f, 3.0f); }
            if (rand == 0) { array_mouse_clicando.Add("S"); array_mouse_segurando.Add("N"); array_mouse_arrastando.Add("N"); }
            else if (rand == 1) { array_mouse_clicando.Add("N"); array_mouse_segurando.Add("S"); array_mouse_arrastando.Add("N"); }
            else if (rand == 2) { array_mouse_clicando.Add("N"); array_mouse_segurando.Add("N"); array_mouse_arrastando.Add("S"); }

            //Enchendo o resto dos arrays de nada, para evitar problemas de leitura
            array_qual_objeto.Add(string.Empty);
            array_frame_de_animacao_do_objeto.Add(-1);
            array_foi_criado_agora.Add(string.Empty);
            array_quem_criou_o_objeto.Add(string.Empty);

            //Posicao de Objetos
            for (int j = 0; j < quantdeobjetos; j++)
            {
                array_tempo_de_objetos.Add(i);
                array_mouse_ou_objeto.Add("Objeto");

                x = (int)(Random.Range(0.0f, 800.0f));
                y = (int)(Random.Range(0.0f, 600.0f));
                array_posicoes_de_objetos.Add(new Vector2(x, y));

                rand = (int)Random.Range(0.0f, 4.0f);
                while (rand == 4) { rand = (int)Random.Range(0.0f, 4.0f); }
                array_qual_objeto.Add(objetos[rand]);

                rand = (int)Random.Range(0.0f, 2.0f);
                array_frame_de_animacao_do_objeto.Add(rand);

                rand = (int)Random.Range(0.0f, 2.0f);
                while (rand == 2) { rand = (int)Random.Range(0.0f, 2.0f); }
                array_foi_criado_agora.Add(rand);


                rand = (int)Random.Range(0.0f, 3.0f);
                while (rand == 3) { rand = (int)Random.Range(0.0f, 3.0f); }
                array_quem_criou_o_objeto.Add(objetos[rand]);

                rand = (int)Random.Range(0.0f, 3.0f);
                while (rand == 3) { rand = (int)Random.Range(0.0f, 3.0f); }
                if (rand == 0) { array_mouse_clicando.Add("S"); array_mouse_segurando.Add("N"); array_mouse_arrastando.Add("N"); }
                else if (rand == 1) { array_mouse_clicando.Add("N"); array_mouse_segurando.Add("S"); array_mouse_arrastando.Add("N"); }
                else if (rand == 2) { array_mouse_clicando.Add("N"); array_mouse_segurando.Add("N"); array_mouse_arrastando.Add("S"); }

            }

        }

    }

    void Salvador_De_Texto_De_Log()
    {
        string linha;
        string mouseobjeto;
        Vector2 posicao;

        System.IO.StreamWriter file = new System.IO.StreamWriter(endereco);
        file.WriteLine("[Mode Bolhas]");
        file.WriteLine(tamanhodatelax + "x" + tamanhodatelay);

        for (int i = 0; i < array_tempo_de_objetos.Count; i++)
        {
            linha = string.Empty;
            mouseobjeto = string.Empty;
            posicao = new Vector2();

            linha += "Time:" + array_tempo_de_objetos[i];
            linha += "=";

            mouseobjeto = (string)array_mouse_ou_objeto[i];
            linha += mouseobjeto;
            linha += "=";

            posicao = (Vector2)array_posicoes_de_objetos[i];

            linha += "GridX:" + posicao.x + "=GridY:" + posicao.y;
            linha += "=";

            if (mouseobjeto == "Mouse")
            {
                linha += "Clicando:" + array_mouse_clicando[i] + "=Segurando:" + array_mouse_segurando[i] +
                    "=Arrastando:" + array_mouse_arrastando[i];
            }
            else if (mouseobjeto == "Objeto")
            {
                linha += array_qual_objeto[i];
                linha += "=";

                linha += "Frame:" + array_frame_de_animacao_do_objeto[i];
                linha += "=";

                linha += "CriadoAgora:" + array_foi_criado_agora[i];
                linha += "=";

                linha += "QuemCriou:" + array_quem_criou_o_objeto[i];
                linha += "=";

                linha += "Clicou:" + array_mouse_clicando[i] + "=Segurou:" + array_mouse_segurando[i] +
                    "=Arrastou:" + array_mouse_arrastando[i];
            }
            file.WriteLine(linha);
        }
        file.Close();
    }

    void Onde_Salvar_Texto_De_Log()
    {
        endereco = "C:\\Teste\\TesteBolhas.txt";

    }
}