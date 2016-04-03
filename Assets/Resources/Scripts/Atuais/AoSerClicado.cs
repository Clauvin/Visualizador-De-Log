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
        Dados d = GetComponent<Dados>();
        FindObjectOfType<Camera>().GetComponent<GuiPonto>().PegarDados(d);
        FindObjectOfType<Camera>().GetComponent<Controlador>().PontoFoiClicado(GetComponent<Transform>());
        FindObjectOfType<Camera>().GetComponent<GuiPonto>().RevelarGui();
    }

    
}
