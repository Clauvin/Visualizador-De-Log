using UnityEngine;
using System.Collections;

public class AoSerClicadoBolhas : MonoBehaviour {

    void OnMouseDown()
    {
        Dados d = GetComponent<Dados>();
        Camera cam = FindObjectOfType<Camera>();
        Clicavel clicavel = GetComponent<Clicavel>();

        if ((cam.GetComponent<Controlador>().GetValorDePosicaoDeVisiveisDoBolhas(d.personagem)) && (clicavel.GetClicavel()))
        {
            cam.GetComponent<GuiInfoObjeto>().PegarDados(d);
            cam.GetComponent<Controlador>().PontoFoiClicado(GetComponent<Transform>());
            cam.GetComponent<GuiInfoObjeto>().RevelarGui();
        }
    }

}
