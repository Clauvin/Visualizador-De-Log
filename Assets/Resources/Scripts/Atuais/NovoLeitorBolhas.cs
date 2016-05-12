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
        if (FindFile())
        {
            CriarIniDeUltimaPaginaChecada(endereco_de_arquivo);
            StartBolhas();
            //LoadStuffBolhas(10, 30);
            LoadStuffBolhas();
            CreateStuffBolhas();
            GetComponent<Controlador>().InicializacaoBolhas();
        }
        else
        {
            RetornarParaTelaInicial();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
