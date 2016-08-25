using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Responsável por abrir a janela de endereço de log, guardar endereços na classe, analisá-los
/// e também guardar a posição da última pasta aberta na janela de endereço de log.
/// </summary>
public class PegarEnderecoDeLog : MonoBehaviour {

    public List<string> endereco_de_arquivo;
    public FileBrowser navegador_de_arquivos;

    private bool desenhar_navegador;

    
    
    public PegarEnderecoDeLog()
    {
        endereco_de_arquivo = new List<string>();
        endereco_de_arquivo.Add("");
        navegador_de_arquivos = new FileBrowser(new Rect(Screen.width / 4, Screen.height / 9, Screen.width / 2, Screen.height / 1.25f));
        desenhar_navegador = false;
    }

    public void Set_Desenhar_Navegador(bool true_ou_false)
    {
        desenhar_navegador = true_ou_false;
    }

    public bool Get_Desenhar_Navegador()
    {
        return desenhar_navegador;
    }

    // muda o valor de desenhar_navegador para o único valor possível.
    // ou seja, se true, vira false. Se false, vira true.
    public void Inverter_Desenhar_Navegador()
    {
        desenhar_navegador = !desenhar_navegador;
    }

    private bool AExtensaoETxt(string endereco, int posicao = 0)
    {
        string[] checagem;
        string extensao;

        checagem = endereco_de_arquivo[posicao].Split('/');
        if (checagem[0] == string.Empty) return false;
        extensao = checagem[checagem.GetUpperBound(0)].Split('.')[1];

        if (extensao == "txt") return true;
        else return false;
    }

    public void DesenharNavegadorDeArquivos()
    {
        if (desenhar_navegador) navegador_de_arquivos.draw();
    }

    public bool FindFile(int posicao = 0)
    {

        //Abre uma janela de procurar arquivos .txt para abrir.
        endereco_de_arquivo[posicao] = EditorUtility.OpenFilePanel("Teste", CarregarEnderecoDeUltimoLogChecado(), "txt");
        /*if (fb == null)
        {
            fb = new FileBrowser(CarregarEnderecoDeUltimoLogChecado(), 0, new Rect(100, 100, 100, 100));
            fb.showSearch = true;
            fb.searchRecursively = true;
        }

        if (fb.draw())
        {
            if (fb.outputFile == null)
            {
                Debug.Log("Cancel hit");
            }
            else
            {
                endereco_de_arquivo[posicao] = fb.outputFile.ToString();
            }
        }*/

        return AExtensaoETxt(endereco_de_arquivo[posicao]);
    }

    public string GetNomeDeArquivoDeLog(int posicao = 0)
    {
        string nome_final;
        string[] endereco_separado = endereco_de_arquivo[posicao].Split('/');

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

    void Awake()
    {

    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

    }
}
