using UnityEngine;
using System.Collections;

/// <summary>Classe Conector.
/// <para>Responsável por conectar e desconectar os objetos do visualizador a seus backgrounds respectivos.</para>
/// </summary>
public class Conector : MonoBehaviour {

    public ArrayList lista_de_pontos;
    public GameObject backgroundprincipal;

    void Awake()
    {
        lista_de_pontos = new ArrayList(1);
    }

    public void AddPonto(GameObject ponto)
    {
        lista_de_pontos.Add(ponto);
    }

    public void Conectar()
    {
        for (int i = 0; i < lista_de_pontos.Count; i++)
        {
            ((GameObject)lista_de_pontos[i]).transform.parent = backgroundprincipal.transform;
        }
    }

    public void Desconectar()
    {
        for (int i = 0; i < lista_de_pontos.Count; i++) ((GameObject)lista_de_pontos[i]).transform.parent = null;
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
