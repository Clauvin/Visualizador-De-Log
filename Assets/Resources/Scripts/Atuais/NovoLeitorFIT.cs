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
        if (pegar_endereco_de_log.FindFile())
        {
            pegar_endereco_de_log.CriarIniDeUltimaPaginaChecada(pegar_endereco_de_log.endereco_de_arquivo);
            StartFIT();
            LoadStuffFIT();
            CreateStuffFIT();
            GetComponent<Controlador>().InicializacaoFIT();
        } else
        {
            RetornarParaTelaInicial();
        }
        

    }

    // Update is called once per frame
    void Update () {
	
	}
}
