﻿using UnityEngine;
using System.Collections;
using Basicas;

public class ObjetosDeUmJogadorFIT : MonoBehaviour {

    public ArrayList lista_de_backgrounds;
    public ArrayList lista_de_objetos;
    public ArrayList matrizes_dos_heatmaps;
    public BancoDeDadosFIT bd_fit;

    public ObjetosDeUmJogadorFIT()
    {
        lista_de_backgrounds = new ArrayList();
        lista_de_objetos = new ArrayList();
        //matrizes_dos_heatmaps = new ArrayList();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
