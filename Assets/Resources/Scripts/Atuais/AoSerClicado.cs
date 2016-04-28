using UnityEngine;
using System.Collections;

public class AoSerClicado : MonoBehaviour {

    void OnMouseDown()
    {
        Dados d = GetComponent<Dados>();
        Camera cam = FindObjectOfType<Camera>();
        //if (cam.GetComponent<Controlador>().Getpersonagem)
        cam.GetComponent<GuiPonto>().PegarDados(d);
        cam.GetComponent<Controlador>().PontoFoiClicado(GetComponent<Transform>());
        cam.GetComponent<GuiPonto>().RevelarGui();
    }

    
}
