using UnityEngine;
using System.Collections;

public class CounterParser3 : MonoBehaviour
{

    ArrayList tempo;
    ArrayList oque;
    ArrayList posicoes;
    public int initx1 = 1;
    public int inity1 = 1;
    public int initx2 = 1;
    public int inity2 = 2;
    public int quantasvezes = 100;
    string endereco;

    // Use this for initialization
    void Start()
    {
        tempo = new ArrayList();
        oque = new ArrayList();
        posicoes = new ArrayList();
        QualEndereco();
        Gerador();
        Salvador();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Gerador()
    {
        int temp;
        int x1 = initx1 * 32;
        int y1 = inity1 * 32;
        int x2 = initx2 * 32;
        int y2 = inity2 * 32;
        int oquefoi;
        float r1;
        int alpha = 0;
        int beta = 0;
        for (int i = 0; i < quantasvezes; i++)
        {
            if ((i % 2 == 0) && (i < 21))             x1 += 32;
            if ((i % 2 == 0) && (i > 21) && (i < 38)) y1 += 32;
            if ((i % 2 == 0) && (i > 52) && (i < 72)) x1 -= 32;
            if ((i % 2 == 1) && (i > 20) && (i < 40)) x2 += 32;
            if ((i % 2 == 1) && (i > 40) && (i < 52)) y2 += 32;
            if ((i % 2 == 1) && (i > 72) && (i < 92)) x2 -= 32;

            temp = i / 2;
            oquefoi = (i % 2) + 1;
            if (i % 2 == 0) posicoes.Add(new Vector2(x1, y1));
            if (i % 2 == 1) posicoes.Add(new Vector2(x2, y2));
            tempo.Add(temp);
            oque.Add(oquefoi);
        }

        for (int i = 0; i < quantasvezes; i++)
        {
            Vector2 posicao = (Vector2)posicoes[i];
            Debug.Log(posicao.x + "-" + posicao.y + "-"+ "-" + oque[i]);


        }
    }

    void Salvador()
    {
        System.IO.StreamWriter file = new System.IO.StreamWriter(endereco);
        file.WriteLine("[Mode 01]");
        for (int i = 0; i < quantasvezes; i++)
        {
            Vector2 posicao = (Vector2)posicoes[i];
            if (i%2==0) file.WriteLine("TimeA:" + tempo[i] + "=Char:" + oque[i] + "=GridX:" + posicao.x + "=GridY:"
                + posicao.y);
            else file.WriteLine("TimeB:" + tempo[i] + "=Char:" + oque[i] + "=GridX:" + posicao.x + "=GridY:"
               + posicao.y);
        }
        file.Close();
    }

    void QualEndereco()
    {
        endereco = "C:\\Teste\\Teste2.txt";

    }
}