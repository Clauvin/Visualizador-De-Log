using UnityEngine;
using System.Collections;
using System.IO;
using System;

/// <summary>
/// Classe GuiTelaDePreLoad. Responsável por definir a tela de pré-load de log.
/// </summary>
public class GuiTelaDePreLoadBolhas : GuiTelaDePreLoad
{

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

            // Lê a primeira linha com resolução do Bolhas.
            line = control_line;

            control_line = theReader.ReadLine();

            // Lê a primeira linha de dados de log do Bolhas.
            line = control_line;

            control_line = theReader.ReadLine();

            entries = control_line.Split('=');

            if ((entries.Length == 7) || (entries.Length == 11))
            {
                tempo_minimo = entries[0].Split(':')[1];
            }

            do
            {
                line = control_line;
                control_line = theReader.ReadLine();

            } while (control_line != null);

            entries = line.Split('=');

            if ((entries.Length == 7) || (entries.Length == 11))
            {
                tempo_maximo = entries[0].Split(':')[1];
            }

            theReader.Close();
            theReader.Dispose();
            fs.Close();
            fs.Dispose();

        }
    }

    protected override void InicializacaoEspecifica()
    {
        lida_com_erros_endereco_de_log.valor_de_comparacao_de_tipo_de_log = "[Mode Bolhas]";
        titulo = "Escolha de Log do Bolhas\n" + "e Tempo Carregado do Log";
    }

    protected override void IrParaLoad()
    {
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

