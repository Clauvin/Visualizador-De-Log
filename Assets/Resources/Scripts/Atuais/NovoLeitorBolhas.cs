using UnityEngine;
using Basicas;
using System;
using System.IO;
using System.Collections;

public class NovoLeitorBolhas : NovoLeitor2
{

    // Use this for initialization
    void Start()
    {
        NovoLeitor2Init();
        PassadorDeDados pd = FindObjectOfType<PassadorDeDados>();
        if (pd.endereco_do_arquivo != "")
        {
            pegar_endereco_de_log.endereco_de_arquivo[0] = pd.endereco_do_arquivo;
            pegar_endereco_de_log.CriarIniDeUltimoLogChecado(pd.endereco_do_arquivo);
            pegar_endereco_de_log.CriarIniDeUltimoLogChecado(pegar_endereco_de_log.endereco_de_arquivo[0]);
            StartBolhas();
            LoadStuffBolhas(pd.tempo_minimo, pd.tempo_maximo);
            CreateStuffBolhas();
            GetComponent<Controlador>().InicializacaoBolhas();
            pd.Destruir();
        }
        else
        {
            RetornarParaTelaInicial();
        }

    }

}
