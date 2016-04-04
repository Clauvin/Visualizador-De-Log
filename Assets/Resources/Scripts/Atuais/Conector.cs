using UnityEngine;
using System.Collections;

public class Conector : MonoBehaviour {

    public ArrayList listadepontos;
    public GameObject backgroundprincipal;

    void Awake()
    {
        listadepontos = new ArrayList(1);
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
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
