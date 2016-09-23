using UnityEngine;
using Basicas;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// <para>Responsável por usar as funções de NovoLeitor2 de forma a ler os dados do log do FIT para permitir ao usuário</para>
/// <para>escolher que usuários, personagens e níveis ele vai querer analisar.</para>
/// </summary>
public class NovoLeitorDeSelecaoFIT : NovoLeitor2
{

    // Use this for initialization
    void Start()
    {
        NovoLeitor2Init();

        string endereco;
#if UNITY_EDITOR
        endereco = Application.dataPath + "/Arquivos de Teste de Log";
#else
        endereco = System.IO.Directory.GetCurrentDirectory();
#endif

        PassadorDeDados pd = FindObjectOfType<PassadorDeDados>();
        if (pd.endereco_do_arquivo != "")
        {
            pegar_endereco_de_log.endereco_de_arquivo[0] = pd.endereco_do_arquivo;

            // Gambiarra temporária para testar o carregar do arquivo de mapas.
            pegar_endereco_de_log.endereco_de_arquivo.Add(endereco + "/InfoInicialDasFases.txt");

            pegar_endereco_de_log.CriarIniDeUltimoLogChecado(pd.endereco_do_arquivo);
            LoadStuffSelecaoFIT(pd.tempo_minimo, pd.tempo_maximo);
            pd.Destruir();

            GetComponent<GuiFITEscolhas>().gui_escolhas_de_jogadores.SetListaDeJogadores(SelecionaJogadores());
            GetComponent<GuiFITEscolhas>().gui_escolhas_de_jogadores.InitGuiFITEscolhaDeJogadores();
            GetComponent<GuiFITEscolhas>().gui_escolhas_de_niveis.SetListaDeNiveis(SelecionaNiveis());
            GetComponent<GuiFITEscolhas>().gui_escolhas_de_niveis.InitGuiFITEscolhaDeNiveis();
            GetComponent<GuiFITEscolhas>().gui_escolhas_de_personagens.SetListaDePersonagens(SelecionaPersonagens());
            GetComponent<GuiFITEscolhas>().gui_escolhas_de_personagens.InitGuiFITEscolhaDePersonagens();

        }
        else
        {
            RetornarParaTelaInicial();
        }
    }

    public SortedList SelecionaJogadores()
    {
        int quant = obj_jogador_fit.bd_fit.GetQuantidadeDeEntradas();
        SortedList lista_de_jogadores = new SortedList();

        for (int i = 0; i < quant; i++)
        {
            if (!lista_de_jogadores.ContainsKey(obj_jogador_fit.bd_fit.GetNomeDoJogador(i)))
            {
                lista_de_jogadores.Add(obj_jogador_fit.bd_fit.GetNomeDoJogador(i), obj_jogador_fit.bd_fit.GetNomeDoJogador(i));
            }

        }

        return lista_de_jogadores;

    }

    public SortedList SelecionaNiveis()
    {
        int quant = obj_jogador_fit.bd_fit.GetQuantidadeDeEntradas();
        SortedList lista_de_niveis = new SortedList();

        for (int i = 0; i < quant; i++)
        {
            if (!lista_de_niveis.ContainsKey(obj_jogador_fit.bd_fit.GetNivel(i)))
            {
                lista_de_niveis.Add(obj_jogador_fit.bd_fit.GetNivel(i), obj_jogador_fit.bd_fit.GetNivel(i));
            }

        }

        return lista_de_niveis;

    }

    public SortedList SelecionaPersonagens()
    {
        int quant = obj_jogador_fit.bd_fit.GetQuantidadeDeEntradas();
        SortedList lista_de_personagens = new SortedList();

        for (int i = 0; i < quant; i++)
        {
            if (!lista_de_personagens.ContainsKey(obj_jogador_fit.bd_fit.GetPersonagem(i)))
            {
                lista_de_personagens.Add(obj_jogador_fit.bd_fit.GetPersonagem(i), obj_jogador_fit.bd_fit.GetPersonagem(i));
            }
        }

        return lista_de_personagens;

    }

}

