using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// Classe LidaComErrosTempoMinimoEMaximo. Criada para detectar erros no arquivo escolhido para ser carregado pelo visualizador de logs,
/// e uma vez encontrados, avisar isso e gerar mensagens de erro adequadas para que apareçam na GUI.
/// </summary>
public class LidaComErrosEnderecoDeLog : MonoBehaviour
{

    // variáveis que definem se houve ou não erro, o que mostra mensagens de erro.
    public bool erro_de_inexistencia_do_arquivo;
    public bool erro_de_caminho_escolhido_invalido;
    public bool erro_de_caminho_escolhido_longo_demais;
    public bool erro_de_extensao_nao_txt;
    public bool erro_de_log_errado;

    // considerando que essa classe vai ser usada tanto pra logs do FIT quanto do Bolhas, é necessária esta variável para comparar
    // com a primeira linha, se ela é [Mode FIT] ou [Mode Bolhas]
    public string valor_de_comparacao_de_tipo_de_log;

    public int x_da_janela_de_erro;
    public int posicao_da_mensagem_de_erro_y;
    public int largura_da_janela_de_erro;
    public int altura_da_janela_de_erro;
    public int quantidade_de_mudanca_de_posicao_y;

    // Variáveis necessárias para tentar abrir o log através do endereco dado e ver se existem erros.
    FileStream filestream_de_teste;
    StreamReader streamreader_de_teste;

    public void ConfigurarVariaveis()
    {

        /*x_da_janela_de_erro = ;
        posicao_da_mensagem_de_erro_y = ;
        largura_da_janela_de_erro = ;
        altura_da_janela_de_erro = ;
        quantidade_de_mudanca_de_posicao_y = ;*/

    }

    private bool AExtensaoETxt(string endereco)
    {
        string[] checagem;
        string extensao;

        checagem = endereco.Split('/');
        if (checagem[0] == string.Empty) return false;
        extensao = checagem[checagem.GetUpperBound(0)].Split('.')[1];

        if (extensao == "txt") return true;
        else return false;
    }

    public void DetectarETratarErrosEExcecoesDeInput(string endereco)
    {
        // esse reset das variáveis ao usar essa função é necessário para evitar
        // que mensagens bloqueiem umas as outras.
        erro_de_inexistencia_do_arquivo = false;
        erro_de_caminho_escolhido_invalido = false;
        erro_de_caminho_escolhido_longo_demais = false;
        erro_de_extensao_nao_txt = false;
        erro_de_log_errado = false;

        if (endereco == "") { erro_de_caminho_escolhido_invalido = true; }

        // aqui fazemos um teste de abertura de arquivo. As exceptions encontradas são tratadas, setando as variáveis
        // adequadas como true.
        try {
            filestream_de_teste = new FileStream(endereco, FileMode.Open);
        }
        // O arquivo escolhido existe?
        catch (FileNotFoundException ex)
        {
            erro_de_inexistencia_do_arquivo = true;
        }
        // O caminho escolhido é inválido?
        catch (DirectoryNotFoundException dn)
        {
            erro_de_caminho_escolhido_invalido = true;
        }
        // O caminho escolhido é longo demais?
        catch (PathTooLongException pt)
        {
            erro_de_caminho_escolhido_longo_demais = true;
        }

        // Se não foram encontrados erros até agora...
        if ((erro_de_inexistencia_do_arquivo) && (erro_de_caminho_escolhido_invalido) && (erro_de_caminho_escolhido_longo_demais)){
        // O arquivo escolhido não tem extensão txt?
            if (!AExtensaoETxt(endereco))
            {
                erro_de_extensao_nao_txt = true;
            }
        // Se tem extensão txt, é o arquivo correto para o que se pretende carregar?
            else
            {
                streamreader_de_teste = new StreamReader(filestream_de_teste);
                if (streamreader_de_teste.ReadLine() != valor_de_comparacao_de_tipo_de_log)
                {
                    erro_de_log_errado = true;
                }
            }
        }
        filestream_de_teste.Dispose();
        filestream_de_teste.Close();
    }

    public void PossiveisMensagensDeErro()
    {
        if (erro_de_inexistencia_do_arquivo)
        {
            GUI.Label(new Rect(x_da_janela_de_erro, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro, altura_da_janela_de_erro),
                                "Valor mínimo precisa ser de\n" + "apenas números.", "textfield");
            posicao_da_mensagem_de_erro_y += quantidade_de_mudanca_de_posicao_y;
        }
        if (erro_de_caminho_escolhido_invalido)
        {
            GUI.Label(new Rect(x_da_janela_de_erro, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro, altura_da_janela_de_erro),
                                "Valor máximo precisa ser de\n" + "apenas números.", "textfield");
            posicao_da_mensagem_de_erro_y += quantidade_de_mudanca_de_posicao_y;
        }
        if (erro_de_caminho_escolhido_longo_demais)
        {
            GUI.Label(new Rect(x_da_janela_de_erro, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro, altura_da_janela_de_erro),
                                "Falta preencher o valor mínimo.", "textfield");
            posicao_da_mensagem_de_erro_y += quantidade_de_mudanca_de_posicao_y;
        }
        if (erro_de_extensao_nao_txt)
        {
            GUI.Label(new Rect(x_da_janela_de_erro, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro, altura_da_janela_de_erro),
                                "Falta preencher o valor máximo.", "textfield");
            posicao_da_mensagem_de_erro_y += quantidade_de_mudanca_de_posicao_y;
        }
        if (erro_de_log_errado)
        {
            // Sim, eu me dou o direito de apontar isso, porquê quem vai colocar o tempo ABSURDAMENTE
            // menor que zero?!
            GUI.Label(new Rect(x_da_janela_de_erro, posicao_da_mensagem_de_erro_y,
                                largura_da_janela_de_erro, altura_da_janela_de_erro),
                                "... por favor. Você fez isso\n" + "de propósito.", "textfield");
            posicao_da_mensagem_de_erro_y += quantidade_de_mudanca_de_posicao_y;
        }
    }

    public bool NaoTemosErrosDeInput()
    {
        // Deus abençoe que C# me permite fazer essas comparações em sequência.
        return !(erro_de_inexistencia_do_arquivo || erro_de_caminho_escolhido_invalido ||
                      erro_de_caminho_escolhido_longo_demais || erro_de_extensao_nao_txt ||
                      erro_de_log_errado
                      );
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

