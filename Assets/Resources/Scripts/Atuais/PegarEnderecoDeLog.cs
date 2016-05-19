﻿using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// Classe PegarEnderecoDeLog. Responsável por abrir a janela de endereço de log, guardar e analisar o endereço dele
/// e guardar a posição da última pasta aberta na janela de endereço de log.
/// </summary>
public class PegarEnderecoDeLog : MonoBehaviour {

    public string endereco_de_arquivo;

    private bool AExtensaoETxt(string endereco)
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
        endereco_de_arquivo = EditorUtility.OpenFilePanel("Teste", CarregarEnderecoDeUltimoLogChecado(), "txt");

        return AExtensaoETxt(endereco_de_arquivo);
    }

    public string GetNomeDeArquivoDeLog()
    {
        string nome_final;
        string[] endereco_separado = endereco_de_arquivo.Split('/');

        nome_final = endereco_separado[endereco_separado.GetUpperBound(0)];

        return nome_final;

    }

    //MELHORAR: No futuro, que hajam dois arquivos. Um para a posição do log do F!T e outro para a posição do log do Bolhas
    public void CriarIniDeUltimoLogChecado(string enderecotodo)
    {
        // Handle any problems that might arise when reading the text

        string posicao_do_arquivo = Path.GetFullPath("local.ini");
        FileStream fs;
        StreamWriter sw;

        string[] endereco_separado;
        string endereco_modificado = "";

        endereco_separado = enderecotodo.Split('/');
        for (int i = 0; i < endereco_separado.GetUpperBound(0); i++)
        {
            endereco_modificado += endereco_separado[i];
            endereco_modificado += "/";
        }

        // Create a new StreamReader, tell it which file to read and what encoding the file
        // was saved as
        fs = File.Create(posicao_do_arquivo);
        sw = new StreamWriter(fs);
        sw.Write(endereco_modificado);

        sw.Dispose();
        sw.Close();
        fs.Dispose();
        fs.Close();

    }

    public string CarregarEnderecoDeUltimoLogChecado()
    {
        FileStream fs = null;
        StreamReader sr = null;
        string saida;


        string posicao_do_arquivo = Path.GetFullPath("local.ini");
        // Handle any problems that might arise when reading the text
        if (File.Exists(posicao_do_arquivo))
        {
            fs = File.Open(posicao_do_arquivo, FileMode.Open);
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
