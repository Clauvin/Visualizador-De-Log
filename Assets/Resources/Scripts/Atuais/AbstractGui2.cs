using UnityEngine;
using System.Collections;

public abstract class AbstractGui2 : MonoBehaviour
{

    public abstract void OnGUI();

    public abstract void MudarCoordenadasX(int x);

    public abstract void MudarCoordenadasY(int y);

    public abstract void MudarCoordenadas(int x, int y);

    public abstract bool MudarTexto(string novotexto);

    public abstract string GetTexto();

    public abstract bool EstaRevelado();

    public abstract bool EsconderGui();

    public abstract bool RevelarGui();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
