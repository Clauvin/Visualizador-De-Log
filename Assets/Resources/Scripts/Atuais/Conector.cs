using UnityEngine;
using System.Collections;

public class Conector : MonoBehaviour {

    public GameObject ponto1;
    public GameObject ponto2;
    public GameObject backgroundprincipal;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetPonto(GameObject ponto, int pos)
    {
        if (pos == 0) { ponto1 = ponto; }
        else if (pos == 1) { ponto2 = ponto; }
        else { Debug.Log("Erro - Classe Conector - posição inexistente - " + pos); }
        
    }

    public void Conectar()
    {
        Debug.Log("ponto 1 " + ponto1);
        Debug.Log("ponto 2 " + ponto2);
        if (ponto1 == null) ponto1 = ponto2;
        if (ponto2 == null) ponto2 = ponto1;
        ponto1.transform.parent = backgroundprincipal.transform;
        ponto2.transform.parent = backgroundprincipal.transform;
    }

    public void Desconectar()
    {
        //Debug.Log("Back before " + backgroundprincipal);
        ponto1.transform.parent = null;
        ponto2.transform.parent = null;
        //Debug.Log("Back after " + backgroundprincipal);
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
