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
            pegar_endereco_de_log.endereco_de_arquivo = pd.endereco_do_arquivo;
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
