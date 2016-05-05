using UnityEngine;
using System.Collections;

public class AoSerClicadoBolhas : MonoBehaviour {

    void OnMouseDown()
    {
        Dados d = GetComponent<Dados>();
        Camera cam = FindObjectOfType<Camera>();

        cam.GetComponent<GuiInfoObjeto>().PegarDados(d);
        cam.GetComponent<Controlador>().PontoFoiClicado(GetComponent<Transform>());
        cam.GetComponent<GuiInfoObjeto>().RevelarGui();
    }

}
