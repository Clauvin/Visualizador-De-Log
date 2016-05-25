using UnityEngine;
using System.Collections;
using System.IO;
using System;

/// <summary>
/// Classe GuiTelaDePreLoadBolhas. Classe filha de GuiTelaDePreLoad, responsável por definir
/// para a Scene de PreLoad do Bolhas o que o programa deve fazer.
/// </summary>
public class GuiTelaDePreLoadBolhas : GuiTelaDePreLoad
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

            // Parte 1: ignora o [Mode Bolhas]
            control_line = theReader.ReadLine();

            // Lê a linha com resolução do Bolhas.
            line = control_line;

            control_line = theReader.ReadLine();

            // Lê a linha de dados de log do Bolhas.
            line = control_line;

            control_line = theReader.ReadLine();

            entradas_separadas = control_line.Split('=');

            // == 7 ou 11 porquê existem 7 ou 11 termos por linha de dados no log do Bolhas.
            if ((entradas_separadas.Length == 7) || (entradas_separadas.Length == 11))
            {
                tempo_minimo = entradas_separadas[0].Split(':')[1];
            }

            do
            {
                line = control_line;
                control_line = theReader.ReadLine();

            } while (control_line != null);

            entradas_separadas = line.Split('=');

            // == 7 ou 11 porquê existem 7 ou 11 termos por linha de dados no log do Bolhas.
            if ((entradas_separadas.Length == 7) || (entradas_separadas.Length == 11))
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
        lida_com_erros_endereco_de_log.valor_de_comparacao_de_tipo_de_log = "[Mode Bolhas]";
        titulo = "Escolha de Log do Bolhas\n" + "e Tempo Carregado do Log";
    }

    protected override void IrParaLoad()
    {
        // Vai pra Scene "Version Bolhas"
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
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

