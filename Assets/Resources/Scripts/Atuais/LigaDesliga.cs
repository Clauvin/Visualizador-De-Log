using UnityEngine;
using System.Collections;

/// <summary>
/// Responsável por permitir que objetos do programa sejam ligados e desligados, o que
/// em teoria, vai economizar CPU e GPU.
/// </summary>
public class LigaDesliga : MonoBehaviour {

    private void SetarActive(bool ativo)
    {
        gameObject.SetActive(ativo);
    }

    public bool EstaLigado()
    {
        return gameObject.activeSelf;
    }

    public bool EstaHieraquicamenteLigado()
    {
        return gameObject.activeInHierarchy;
    }

    public void Alterna()
    {
        if (gameObject.activeSelf == true) Desligar();
        else Ligar();
    }

	public void Ligar()
    {
        SetarActive(true);
    }

    public void Desligar()
    {
        SetarActive(false);
    }

    

}
