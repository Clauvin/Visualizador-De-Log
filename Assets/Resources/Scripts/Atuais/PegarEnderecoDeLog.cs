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

    private float navegador_pos_x = Screen.width / 4;
    private float navegador_pos_y = Screen.height / 9;
    private float navegador_largura = Screen.width / 2;
    private float navegador_altura = Screen.height / 1.25f;

    private Texture2D file, folder, back, drive;

    public PegarEnderecoDeLog()
    {
        endereco_de_arquivo = new List<string>();
        endereco_de_arquivo.Add("");
        navegador_de_arquivos = new FileBrowser();
        desenhar_navegador = false;

        file = Resources.Load<Texture2D>("Texturas/file menor");
        folder = Resources.Load<Texture2D>("Texturas/folder menor");
        back = Resources.Load<Texture2D>("Texturas/back menor");
        drive = Resources.Load<Texture2D>("Texturas/drive menor");

        // Eu QUERIA cortar a altura e largura assim MAS fazer isso chega a Texture.Set_width e Texture.Set_height...
        // e isso gera um erro de NullReferenceException: ainda não implementaram Texture.Set_width e Texture.Set_height -__-
        // (na Unity 5.3.5f1 Personal)
        /*file.width /= 2; file.height /= 2;
        folder.width /= 2; folder.height /= 2;
        back.width /= 2; back.height /= 2;
        drive.width /= 2; drive.height /= 2;*/

        // Colocando as imagens da GUI do desenhar_navegador pra dentro dele
        navegador_de_arquivos.fileTexture = file;
        navegador_de_arquivos.directoryTexture = folder;
        navegador_de_arquivos.backTexture = back;
        navegador_de_arquivos.driveTexture = drive;

        if (drive == null) Debug.Log("Nao foi");
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

    // necessário por conta de possíveis mudanças de tamanho da tela por parte do usuário.
    public void Atualizar_Posicao_E_Tamanho_Do_Navegador()
    {
        navegador_pos_x = Screen.width / 4;
        navegador_pos_y = Screen.height / 9;
        navegador_largura = Screen.width / 2;
        navegador_altura = Screen.height / 1.25f;

        navegador_de_arquivos.setGUIRect(new Rect(navegador_pos_x, navegador_pos_y, navegador_largura, navegador_altura));
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
        if (desenhar_navegador)
        {
            Atualizar_Posicao_E_Tamanho_Do_Navegador();
            navegador_de_arquivos.draw();
        }
    }

    public bool FindFile(int posicao = 0)
    {

        // Abre uma janela de procurar arquivos .txt para abrir.
        // Descomentar caso se queira usar a janela nativa do sistema operacional, e ainda assim, apenas dentro do 
        // Editor da Unity.
        //endereco_de_arquivo[posicao] = EditorUtility.OpenFilePanel("Teste", CarregarEnderecoDeUltimoLogChecado(), "txt");

        endereco_de_arquivo[posicao] = navegador_de_arquivos.outputFile.ToString();

        return AExtensaoETxt(endereco_de_arquivo[posicao]);
    }

    public string GetNomeDeArquivoDeLog(int posicao = 0)
    {
        string nome_final;
        string[] endereco_separado = endereco_de_arquivo[posicao].Split('\\');

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
