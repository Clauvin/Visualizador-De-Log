using UnityEngine;
using Basicas;
using System;
using System.IO;
using System.Collections;

public class NovoLeitorFIT : NovoLeitor2 {

    // Use this for initialization
    void Start()
    {
        NovoLeitor2Init();
        PassadorDeDados pd = FindObjectOfType<PassadorDeDados>();
        if (pd.endereco_do_arquivo != "")
        {
            pegar_endereco_de_log.endereco_de_arquivo[0] = pd.endereco_do_arquivo;

            // Gambiarra temporária para testar o carregar do arquivo de mapas.
            pegar_endereco_de_log.endereco_de_arquivo.Add("C:\\Teste\\InfoInicialDasFases.txt");

            pegar_endereco_de_log.CriarIniDeUltimoLogChecado(pd.endereco_do_arquivo);
            StartFIT();
            LoadStuffFIT(pd.tempo_minimo, pd.tempo_maximo);
            CreateStuffFIT();
            GetComponent<Controlador>().InicializacaoFIT();
            pd.Destruir();
        } else
        {
            RetornarParaTelaInicial();
        }
    }

}
