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
        if (FindFile())
        {
            CriarIniDeUltimaPaginaChecada(enderecodearquivo);
            StartFIT();
            LoadStuffFIT();
            CreateStuffFIT();
        } else
        {
            RetornarParaTelaInicial();
        }
        

    }

    // Update is called once per frame
    void Update () {
	
	}
}
