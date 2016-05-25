using UnityEngine;
using System.Collections;
using System.IO;
using System;

/// <summary>
/// Classe GuiTelaDePreLoadFIT. Classe filha de GuiTelaDePreLoad, responsável por definir
/// para a Scene de PreLoad do FIT o que o programa deve fazer.
/// </summary>
public class GuiTelaDePreLoadFIT : GuiTelaDePreLoad
{
    
    // Essa função diverge entre FIT e Bolhas, por conta da diferença do formato do log de ambos.
    protected override void EscolhaDeArquivo()
    {
        if (pegar_endereco_do_log.FindFile())
        {
            endereco = pegar_endereco_do_log.endereco_de_arquivo;
            nome_do_arquivo = pegar_endereco_do_log.GetNomeDeArquivoDeLog();

            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            fs = new FileStream(pegar_endereco_do_log.endereco_de_arquivo, FileMode.Open);
            theReader = new StreamReader(fs);

            // Parte 1: ignora o [Mode 01]
            control_line = theReader.ReadLine();

            // Lê a primeira linha com dados do pre-load do FIT.
            line = control_line;

            control_line = theReader.ReadLine();

            entradas_separadas = control_line.Split('=');

            // == 4 porquê existem 4 termos por linha de dados no log do FIT.
            if (entradas_separadas.Length == 4)
            {
                tempo_minimo = entradas_separadas[0].Split(':')[1];
            }

            do
            {
                line = control_line;
                control_line = theReader.ReadLine();

            } while (control_line != null);

            entradas_separadas = line.Split('=');

            // == 4 porquê existem 4 termos por linha de dados no log do FIT.
            if (entradas_separadas.Length == 4)
            {
                tempo_maximo = entradas_separadas[0].Split(':')[1];
            }

            theReader.Close();
            theReader.Dispose();
            fs.Close();
            fs.Dispose();

            pegar_endereco_do_log.CriarIniDeUltimoLogChecado(endereco);
        }
    }

    protected override void InicializacaoEspecifica()
    {
        lida_com_erros_endereco_de_log.valor_de_comparacao_de_tipo_de_log = "[Mode FIT]";
        titulo = "Escolha de Log do FIT\n" + "e Tempo Carregado do Log";
    }

    protected override void IrParaLoad()
    {
        // Vai pra Scene "Version FIT 3"
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    // Use this for initialization
    void Start()
    {

        InicializacaoGenerica();
        InicializacaoEspecifica();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
