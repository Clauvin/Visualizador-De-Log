﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Responsável por armazenar informação sobre os objetos mostrados pelo visualizador do log.
/// <para>Essa classe merece métodos de Get e Set futuramente, o acesso aos dados dela é feito diretamente nas variáveis.</para>
/// </summary>
public class Dados : MonoBehaviour
{
    public string nome_do_objeto;
    public float largura_x;
    public float altura_z;
    public int x_log;
    public int y_log;
    public Vector2 centro;
    public int tempo;
    public string acao_dele_ou_nele;
    public string no_que_fez;
    public string criado_agora;
    public string quem_criou;

    // Use this for initialization
    void Start () {
	
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    /// <summary>
    /// Atualiza dados de posicionamento do objeto com relação ao espaço em que se encontra no visualizador.
    /// </summary>
    public void Atualizar()
    {
        Vector3 vector = GetComponent<MeshCollider>().bounds.center;
        centro = new Vector2(vector.x, vector.z);

        largura_x = GetComponent<MeshCollider>().bounds.max.x - GetComponent<MeshCollider>().bounds.min.x;
        altura_z = GetComponent<MeshCollider>().bounds.max.z - GetComponent<MeshCollider>().bounds.min.z;
    }
}
