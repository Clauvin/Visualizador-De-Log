using UnityEngine;
using System.Collections;

/// <summary>
/// Classe TestLogFITGenerator. 
/// <para>Responsável por criar um log aleatório no formato do do FIT para testes.</para>
/// </summary>
public class TestLogFITGenerator : MonoBehaviour
{

    ArrayList tempo;
    ArrayList qual_objeto;
    ArrayList posicoes_dos_objetos;
    public int initx1 = 1;
    public int inity1 = 1;
    public int initx2 = 1;
    public int inity2 = 2;
    public int quantos_objetos_criar = 100;
    string endereco_onde_salvar;

    // Use this for initialization
    void Start()
    {
        tempo = new ArrayList();
        qual_objeto = new ArrayList();
        posicoes_dos_objetos = new ArrayList();
        Onde_Salvar_Texto_De_Log();
        Gerador_De_Dados_De_Log();
        Salvador_De_Texto_De_Log();
    }

    void Gerador_De_Dados_De_Log()
    {
        int temp;
        int x1 = initx1 * 32;
        int y1 = inity1 * 32;
        int x2 = initx2 * 32;
        int y2 = inity2 * 32;
        int o_que_foi;

        for (int i = 0; i < quantos_objetos_criar; i++)
        {
            if ((i % 2 == 0) && (i < 21))             x1 += 32;
            if ((i % 2 == 0) && (i > 21) && (i < 38)) y1 += 32;
            if ((i % 2 == 0) && (i > 52) && (i < 72)) x1 -= 32;
            if ((i % 2 == 1) && (i > 20) && (i < 40)) x2 += 32;
            if ((i % 2 == 1) && (i > 40) && (i < 52)) y2 += 32;
            if ((i % 2 == 1) && (i > 72) && (i < 92)) x2 -= 32;

            temp = i / 2;
            o_que_foi = (i % 2) + 1;
            if (i % 2 == 0) posicoes_dos_objetos.Add(new Vector2(x1, y1));
            if (i % 2 == 1) posicoes_dos_objetos.Add(new Vector2(x2, y2));
            tempo.Add(temp);
            qual_objeto.Add(o_que_foi);
        }

        for (int i = 0; i < quantos_objetos_criar; i++) {
            Vector2 posicao = (Vector2)posicoes_dos_objetos[i];
        }
    }

    void Salvador_De_Texto_De_Log()
    {
        System.IO.StreamWriter file = new System.IO.StreamWriter(endereco_onde_salvar);

        file.WriteLine("[Mode 01]");

        for (int i = 0; i < quantos_objetos_criar; i++)
        {
            Vector2 posicao = (Vector2)posicoes_dos_objetos[i];
            if (i%2==0) file.WriteLine("TimeA:" + tempo[i] + "=Char:" + qual_objeto[i] + "=GridX:" + posicao.x + "=GridY:"
                + posicao.y);
            else file.WriteLine("TimeB:" + tempo[i] + "=Char:" + qual_objeto[i] + "=GridX:" + posicao.x + "=GridY:"
               + posicao.y);
        }
        file.Close();
    }

    void Onde_Salvar_Texto_De_Log()
    {
        endereco_onde_salvar = "C:\\Teste\\Teste2.txt";
    }
}