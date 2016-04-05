using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System;

public class Leitor3 : MonoBehaviour
{

    public ArrayList coordenadasx;
    public ArrayList coordenadasy;
    public ArrayList tempo;
    public ArrayList oquefez;
    public ArrayList noquefez;
    public GameObject objetodeclicar;
    public GameObject objetodearrastar;
    public GameObject objetodepressionar;
    public GameObject objetodesoltar;

    void Start()
    {
        coordenadasx = new ArrayList();
        coordenadasy = new ArrayList();
        tempo = new ArrayList();
        oquefez = new ArrayList();
        noquefez = new ArrayList();
        LoadStuff("C:\\Jet Bread\\Experimentos do\\Cláuvin\\Prova de Conceito - Heatmap 3D\\Teste4.txt");
        PrintStuff();
        CreateStuff();
    }

    void Update()
    {

    }

    public bool LoadStuff(string fileName)
    {
        // Handle any problems that might arise when reading the text
        string line;
        // Create a new StreamReader, tell it which file to read and what encoding the file
        // was saved as
        StreamReader theReader = new StreamReader(fileName, Encoding.Default);

        using (theReader)
        {
            // While there's lines left in the text file, do this:
            do
            {
                line = theReader.ReadLine();

                if (line != null)
                {
                    // Do whatever you need to do with the text line, it's a string now
                    // In this example, I split it into arguments based on comma
                    // deliniators, then send that array to DoStuff()
                    string[] entries = line.Split('-');
                    if (entries.Length == 5)
                    {
                        coordenadasx.Add(entries[0]);
                        coordenadasy.Add(entries[1]);
                        tempo.Add(entries[2]);
                        oquefez.Add(entries[3]);
                        noquefez.Add(entries[4]);
                    }
                }
            } while (line != null);

            theReader.Close();
            return true;
        }
    }

    public bool PrintStuff()
    {
        for (int i = 0; i < coordenadasx.Count; i++)
        {
            Debug.Log(coordenadasx[i] + " " + coordenadasy[i] + " " + tempo[i] + " " + oquefez[i] + " " + noquefez[i]);
        }
        return true;

    }

    public void CreateStuff()
    {
        GameObject objeto = null;
        for (int i = 0; i < coordenadasx.Count; i++)
        {
            switch (oquefez[i].ToString())
            {
                case ("Clicou"):
                    objeto = Instantiate(objetodeclicar);
                    break;

                case ("Pressionou"):
                    objeto = Instantiate(objetodepressionar);
                    break;
                
                case ("Arrastou"):
                    objeto = Instantiate(objetodearrastar);
                    break;

                case ("Soltou"):
                    objeto = Instantiate(objetodesoltar);
                    break;

                default:
                    break;
            }

            objeto.name = oquefez[i].ToString();

            switch (noquefez[i].ToString())
            {
                case ("Baleia"):
                    objeto.GetComponent<Renderer>().material = Resources.Load("Materiais/MaterialBaleia") as Material;
                    break;

                case ("Bolha"):
                    objeto.GetComponent<Renderer>().material = Resources.Load("Materiais/MaterialBolha") as Material;
                    break;

                default:
                    break;
            }

            objeto.name += " " + noquefez[i].ToString() + " " + i.ToString();

            Vector3 newpos = new Vector3(Int32.Parse(Convert.ToString(coordenadasx[i])),
                                         Int32.Parse(Convert.ToString(tempo[i]))/1000,
                                         Int32.Parse(Convert.ToString(coordenadasy[i])));
            objeto.transform.position = newpos;
        }
    }
}