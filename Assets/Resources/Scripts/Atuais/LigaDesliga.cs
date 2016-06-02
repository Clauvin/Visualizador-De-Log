using UnityEngine;
using System.Collections;

/// <summary>
/// Responsável por permitir que objetos do programa sejam ligados e desligados, o que
/// em teoria, vai economizar CPU e GPU.
/// </summary>
public class LigaDesliga : MonoBehaviour {

    private void SetActive(bool ativo)
    {
        SetActive(ativo);
    }

    public bool EstaLigado()
    {
        return GetComponent<GameObject>().activeSelf;
    }

    public bool EstaHieraquicamenteLigado()
    {
        return GetComponent<GameObject>().activeInHierarchy;
    }

    public void Alterna()
    {
        if (GetComponent<GameObject>().activeSelf == true) Desligar();
        else Ligar();
    }

	public void Ligar()
    {
        GetComponent<GameObject>().SetActive(true);
    }

    public void Desligar()
    {
        GetComponent<GameObject>().SetActive(false);
    }

    

}
