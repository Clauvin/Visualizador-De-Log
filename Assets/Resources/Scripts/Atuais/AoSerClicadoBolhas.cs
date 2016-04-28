using UnityEngine;
using System.Collections;

public class AoSerClicadoBolhas : MonoBehaviour {

    void OnMouseDown()
    {
        Dados d = GetComponent<Dados>();
        Camera cam = FindObjectOfType<Camera>();

        if (cam.GetComponent<Controlador>().GetValorDePosicaoDeVisiveisDoBolhas(d.personagem))
        {
            cam.GetComponent<GuiPonto>().PegarDados(d);
            cam.GetComponent<Controlador>().PontoFoiClicado(GetComponent<Transform>());
            cam.GetComponent<GuiPonto>().RevelarGui();
        }
    }

}
