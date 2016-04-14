using UnityEngine;
using System.Collections;

/// <summary>
/// Classe TestLogBolhasGenerator.
/// <para>Criar logs para testes do visualizador do bolhas. Gera posições de objetos de forma aleatória.</para>
/// </summary>
public class TestLogBolhasGenerator : MonoBehaviour
{

    ArrayList posicoes_de_objetos;
    ArrayList o_que;
    ArrayList qual;
    public int quantos_objetos_criar = 2000;
    string endereco;

    // Use this for initialization
    void Start()
    {

        posicoes_de_objetos = new ArrayList();
        o_que = new ArrayList();
        qual = new ArrayList();
        Onde_Salvar_Texto_De_Log();
        Gerador_De_Texto_De_Log();
        Salvador_De_Texto_De_Log();

    }

    void Gerador_De_Texto_De_Log()
    {

        int x, y;
        string oquefoi, noquefoi;
        float r1;
        for (int i = 0; i < quantos_objetos_criar; i++)
        {
            r1 = Random.Range(0f, 800f);
            x = (int)r1;

            r1 = Random.Range(0f, 600f);
            y = (int)r1;

            r1 = Random.Range(0f, +4.0f);
            if (r1 < 1) { oquefoi = "Clicou"; }
            else if (r1 < 2) { oquefoi = "Pressionou"; }
            else if (r1 < 3) { oquefoi = "Arrastou"; }
            else { oquefoi = "Soltou"; }

            r1 = Random.Range(0f, +3.0f);
            if (r1 < 1) { noquefoi = "Bolha"; }
            else if (r1 < 2) { noquefoi = "Baleia"; }
            else { noquefoi = "Nada"; }

            posicoes_de_objetos.Add(new Vector3(x, y, i * 1000));
            o_que.Add(oquefoi);
            qual.Add(noquefoi);
        }

        for (int i = 0; i < quantos_objetos_criar; i++)
        {
            Vector3 posicao = (Vector3)posicoes_de_objetos[i];


        }
    }

    void Salvador_De_Texto_De_Log()
    {
        System.IO.StreamWriter file = new System.IO.StreamWriter(endereco);
        for (int i = 0; i < quantos_objetos_criar; i++)
        {
            Vector3 posicao = (Vector3)posicoes_de_objetos[i];
            file.WriteLine(posicao.x + "-" + posicao.y + "-" + posicao.z + "-" + o_que[i] + "-" + qual[i]);
        }
        file.Close();
    }

    void Onde_Salvar_Texto_De_Log()
    {
        endereco = "C:\\Teste\\TesteBolhas.txt";

    }
}