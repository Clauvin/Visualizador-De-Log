using UnityEngine;
using System.Collections;

/// <summary>Classe Conector.
/// <para>Responsável por conectar e desconectar os objetos do visualizador a seus backgrounds respectivos.</para>
/// </summary>
public class Conector : MonoBehaviour {

    public ArrayList listadepontos;
    public GameObject backgroundprincipal;

    void Awake()
    {
        listadepontos = new ArrayList(1);
    }

    public void AddPonto(GameObject ponto)
    {
        listadepontos.Add(ponto);
    }

    public void Conectar()
    {
        for (int i = 0; i < listadepontos.Count; i++)
        {
            ((GameObject)listadepontos[i]).transform.parent = backgroundprincipal.transform;
        }
    }

    public void Desconectar()
    {
        for (int i = 0; i < listadepontos.Count; i++) ((GameObject)listadepontos[i]).transform.parent = null;
    }

    public void NoLayer(int layer)
    {
        backgroundprincipal.layer = layer;
    }

    public void ForaDoLayer()
    {
        backgroundprincipal.layer = 0;
    }



}
