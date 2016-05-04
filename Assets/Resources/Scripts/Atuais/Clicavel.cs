using UnityEngine;
using System.Collections;

/// <summary>
/// Classe Clicavel, unicamente responsável por definir se o objeto em que ela está é clicável para retornar a janela de dados ou não.
/// Claro, pode ser usada pra qualquer objeto ou coisa no programa, fica a seu critério, corajoso programador olhando essa descrição.
/// </summary>
public class Clicavel : MonoBehaviour {


    private bool clicavel = true;

    public bool GetClicavel() { return clicavel; }
    public void SetClicavel(bool click) { clicavel = click; }

}
