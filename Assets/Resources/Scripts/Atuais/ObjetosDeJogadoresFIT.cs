using UnityEngine;
using System.Collections;

/// <summary>
/// Classe responsável por guardar todas as instâncias de ObjetosDeUmJogadorFIT,
/// uma para cada jogador que fez ações registradas no log, para que seja possível a visualização
/// 2 a 2 de dados de jogadores.
/// </summary>
public class ObjetosDeJogadoresFIT : MonoBehaviour {

    public ArrayList obj_jogadores_fit;
    int pos;

    public ObjetosDeJogadoresFIT()
    {
        obj_jogadores_fit = new ArrayList();
        Add(new ObjetosDeUmJogadorFIT());
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

    public int QuantosJogadores()
    {
        return obj_jogadores_fit.Count;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
