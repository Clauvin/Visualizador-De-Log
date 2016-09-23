using UnityEngine;
using Basicas;
using System;
using System.IO;
using System.Collections;

/// <summary>
/// Responsável por usar as funções de NovoLeitor2 de forma a ler e usar os dados do log do FIT.
/// </summary>
public class NovoLeitorFIT : NovoLeitor2 {

    // Use this for initialization
    void Start()
    {
        NovoLeitor2InitFIT();

        string endereco;
#if UNITY_EDITOR
        endereco = Application.dataPath + "/Arquivos de Teste de Log";
#else
        endereco = System.IO.Directory.GetCurrentDirectory();
#endif

        PassadorDeDados pd = FindObjectOfType<PassadorDeDados>();

        pegar_endereco_de_log.endereco_de_arquivo[0] = pd.endereco_do_arquivo;

        StartFIT();
        obj_jogador_fit.bd_fit = pd.bd_fit;
        Heatmaps = pd.heatmaps;
        InicializarHeatmaps();

        CreateStuffFIT();
        GetComponent<Controlador>().InicializacaoFIT();
        
        pd.Destruir();

    }

}
