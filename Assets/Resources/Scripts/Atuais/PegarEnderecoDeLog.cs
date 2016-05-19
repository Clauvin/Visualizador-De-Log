using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// Classe PegarEnderecoDeLog. Responsável por abrir a janela de endereço de log, guardar e analisar o endereço dele
/// e guardar a posição da última pasta aberta na janela de endereço de log.
/// </summary>
public class PegarEnderecoDeLog : MonoBehaviour {

    string endereco_de_arquivo;

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

    public bool FindFile()
    {

        //Abre uma janela de procurar arquivos .txt para abrir.
        endereco_de_arquivo = EditorUtility.OpenFilePanel("Teste", CarregarEnderecoDeUltimaPaginaChecada(), "txt");

        return Checagem(endereco_de_arquivo);
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

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
