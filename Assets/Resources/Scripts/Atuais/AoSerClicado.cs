using UnityEngine;
using System.Collections;

public class AoSerClicado : MonoBehaviour {

    void OnMouseDown()
    {
        Dados d = GetComponent<Dados>();
        FindObjectOfType<Camera>().GetComponent<GuiPonto>().PegarDados(d);
        FindObjectOfType<Camera>().GetComponent<Controlador>().PontoFoiClicado(GetComponent<Transform>());
        FindObjectOfType<Camera>().GetComponent<GuiPonto>().RevelarGui();
    }

    
}
