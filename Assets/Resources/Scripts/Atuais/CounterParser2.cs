using UnityEngine;
using System.Collections;

public class CounterParser2 : MonoBehaviour
{

    ArrayList posicoes;
    ArrayList oque;
    ArrayList qual;
    public int initx = 100;
    public int inity = 100;
    public int quantasvezes = 2000;
    string endereco;

    // Use this for initialization
    void Start()
    {

        posicoes = new ArrayList();
        oque = new ArrayList();
        qual = new ArrayList();
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

        int x, y;
        string oquefoi, noquefoi;
        float r1;
        x = initx;
        y = inity;
        for (int i = 0; i < quantasvezes; i++)
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

            posicoes.Add(new Vector3(x, y, i * 1000));
            oque.Add(oquefoi);
            qual.Add(noquefoi);
        }

        for (int i = 0; i < quantasvezes; i++)
        {
            Vector3 posicao = (Vector3)posicoes[i];
            Debug.Log(posicao.x + "-" + posicao.y + "-" + posicao.z + "-" + oque[i] + "-" + qual[i]);


        }
    }

    void Salvador()
    {
        System.IO.StreamWriter file = new System.IO.StreamWriter(endereco);
        for (int i = 0; i < quantasvezes; i++)
        {
            Vector3 posicao = (Vector3)posicoes[i];
            file.WriteLine(posicao.x + "-" + posicao.y + "-" + posicao.z + "-" + oque[i] + "-" + qual[i]);
        }
        file.Close();
    }

    void QualEndereco()
    {
        endereco = "C:\\Teste\\Teste4.txt";

    }
}