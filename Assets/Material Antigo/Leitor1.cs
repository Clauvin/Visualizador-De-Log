using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System;

public class Leitor1 : MonoBehaviour {

    public ArrayList coordenadasx;
    public ArrayList coordenadasy;
    public ArrayList tempo;
    public ArrayList oquefez;
    public bool gambiarra;
    public GameObject cubo;

    void Start()
    {
        coordenadasx = new ArrayList();
        coordenadasy = new ArrayList();
        tempo = new ArrayList();
        oquefez = new ArrayList();
        gambiarra = true;
        LoadStuff("C:\\Jet Bread\\Experimentos do\\Cláuvin\\Prova de Conceito - Heatmap 3D\\Teste1.txt");
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
        // Immediately clean up the reader after this block of code is done.
        // You generally use the "using" statement for potentially memory-intensive objects
        // instead of relying on garbage collection.
        // (Do not confuse this with the using directive for namespace at the 
        // beginning of a class!)
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
                    if (entries.Length == 4) { 
                        coordenadasx.Add(entries[0]);
                        coordenadasy.Add(entries[1]);
                        tempo.Add(entries[2]);
                        oquefez.Add(entries[3]);
                    }
                }
            } while (line != null);
            // Done reading, close the reader and return true to broadcast success    
            theReader.Close();
            return true;
        }
    }

    public bool PrintStuff()
    {
        for (int i = 0; i < coordenadasx.Count; i++)
        {
            Debug.Log(coordenadasx[i] + " " + coordenadasy[i] + " " + tempo[i] + " " + oquefez[i]);
        }
        return true;

    }

    public void CreateStuff()
    {
        for (int i = 0; i < coordenadasx.Count; i++)
        {
            GameObject obj = Instantiate(cubo);
            Vector3 newpos = new Vector3(Int32.Parse(Convert.ToString(coordenadasx[i])),
                                         Int32.Parse(Convert.ToString(tempo[i])),
                                         Int32.Parse(Convert.ToString(coordenadasy[i])));
            obj.transform.position = newpos;
        }
    }
}
