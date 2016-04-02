using UnityEngine;
using System.Collections;

public class Dados : MonoBehaviour
{
    public string personagem;
    public float largurax;
    public float alturaz;
    public int xlog;
    public int ylog;
    public Vector2 centro;
    public int tempo;
    public string oquefez;
    public string noquefez;
    public string criadoagora;
    public string quemcriou;

    // Use this for initialization
    void Start () {
	
    }
	
	// Update is called once per frame
	void Update () {
	
	}



    public void Atualizar()
    {
        Vector3 vector = GetComponent<MeshCollider>().bounds.center;
        centro = new Vector2(vector.x, vector.z);

        largurax = GetComponent<MeshCollider>().bounds.max.x - GetComponent<MeshCollider>().bounds.min.x;
        alturaz = GetComponent<MeshCollider>().bounds.max.z - GetComponent<MeshCollider>().bounds.min.z;
    }
}
