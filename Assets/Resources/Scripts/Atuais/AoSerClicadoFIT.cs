using UnityEngine;
using System.Collections;

public class AoSerClicadoFIT : MonoBehaviour {

    void OnMouseDown()
    {
        Dados d = GetComponent<Dados>();
        Camera cam = FindObjectOfType<Camera>();
        cam.GetComponent<GuiPonto>().PegarDados(d);
        cam.GetComponent<Controlador>().PontoFoiClicado(GetComponent<Transform>());
        cam.GetComponent<GuiPonto>().RevelarGui();
    }

}
