﻿using UnityEngine;
using System.Collections;

public class ObjetosDeJogadoresFIT : MonoBehaviour {

    public ArrayList obj_jogadores_fit;
    int pos;

    public ObjetosDeJogadoresFIT()
    {
        obj_jogadores_fit = new ArrayList();
        Pos = 0;
    }

    public int Pos
    {
        get
        {
            return pos;
        }

        set
        {
            if (value >= obj_jogadores_fit.Count)
            {
                pos = 0;
            } else
            {
                pos = value;
            }
        }
    }

    public void Add(ObjetosDeUmJogadorFIT objs)
    {
        obj_jogadores_fit.Add(objs);
    }

    public ObjetosDeUmJogadorFIT GetObjetosDeUmJogadorFIT(int i)
    {
        return (ObjetosDeUmJogadorFIT)obj_jogadores_fit[i];
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
