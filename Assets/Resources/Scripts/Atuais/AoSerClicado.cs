using UnityEngine;
using System.Collections;

public class AoSerClicado : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        //FindObjectOfType<Camera>().GetComponent<GuiFITPonto>().EsconderGui();
        Dados d = GetComponent<Dados>();
        FindObjectOfType<Camera>().GetComponent<GuiFITPonto>().PegarDados(d);
        FindObjectOfType<Camera>().GetComponent<Controlador>().PontoFoiClicado(GetComponent<Transform>());
        FindObjectOfType<Camera>().GetComponent<GuiFITPonto>().RevelarGui();
    }

    
}
