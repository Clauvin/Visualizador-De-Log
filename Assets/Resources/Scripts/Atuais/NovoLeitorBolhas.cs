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
        StartBolhas();

        LoadStuffBolhas("C:\\Teste\\TesteBolhas.txt");
        CreateStuffBolhas();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
