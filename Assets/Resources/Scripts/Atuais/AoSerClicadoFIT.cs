using UnityEngine;
using System.Collections;

/// <summary>
/// Responsável por fazer aparecer as informações referente a objetos do FIT quando clicados.
/// </summary>
public class AoSerClicadoFIT : MonoBehaviour {

    void OnMouseDown()
    {
        Dados d = GetComponent<Dados>();
        Camera cam = FindObjectOfType<Camera>();

        cam.GetComponent<GuiInfoObjeto>().PegarDados(d);
        cam.GetComponent<Controlador>().PontoFoiClicado(GetComponent<Transform>());
        cam.GetComponent<GuiInfoObjeto>().RevelarGui();
    }

}
