//#define DEBUG 
//#undef DEBUG

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Basicas
{
    /// <summary>
    /// Responsável por guardar os dados do log carregado do FIT.
    /// </summary>
    public class BancoDeDadosFIT
    {
        // int
        private ArrayList instante;
        // int
        private ArrayList tempo;
        // int
        private ArrayList nivel;
        // int
        private ArrayList personagem;
        // int
        private ArrayList grid_x;
        // int
        private ArrayList grid_y;
        // int
        private ArrayList tempo_do_servidor;
        // string
        private ArrayList nome_do_jogador;
        // int
        private ArrayList id_do_jogador;
        // int
        private ArrayList modo_de_jogo;


        //Time: 1 = Char:1 = GridX:5 = GridY:7

        public BancoDeDadosFIT()
        {
            instante = new ArrayList();
            tempo = new ArrayList();
            nivel = new ArrayList();
            personagem = new ArrayList();
            grid_x = new ArrayList();
            grid_y = new ArrayList();
            tempo_do_servidor = new ArrayList();
            nome_do_jogador = new ArrayList();
            id_do_jogador = new ArrayList();
            modo_de_jogo = new ArrayList();
        }

        public bool Add(int inst, int time, int niv, int pers, int x, int y, int server_time,
                        string player_name, int player_id, int game_mode)
        {
            instante.Add(inst);
            tempo.Add(time);
            nivel.Add(niv);
            personagem.Add(pers);
            grid_x.Add(x);
            grid_y.Add(y);
            tempo_do_servidor.Add(server_time);
            nome_do_jogador.Add(player_name);
            id_do_jogador.Add(player_id);
            modo_de_jogo.Add(game_mode);
            return true;
        }

        public int GetQuantidadeDeEntradas()
        {
            return tempo.Count;
        }

        public int GetInstante(int pos)
        {
            try { return (int)instante[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {



#if (DEBUG)

                Debug.Log("BancoDeDadosFIT.GetInstante - Não há posição " + pos);

#endif
                return -1;
            }
        }

        public int GetNivel(int pos)
        {
            try { return (int)nivel[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {

#if (DEBUG)

                Debug.Log("BancoDeDadosFIT.GetNivel - Não há posição " + pos);

#endif
                return -1;
            }
        }

        public int GetTempo(int pos)
        {
            try { return (int)tempo[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {


            
                #if (DEBUG)

                Debug.Log("BancoDeDadosFIT.GetTempo - Não há posição " + pos);

                #endif
                return -1;
            }
        }

        public int GetPersonagem(int pos)
        {
            try { return (int)personagem[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {
                #if (DEBUG)

                Debug.Log("BancoDeDadosFIT.GetPersonagem - Não há posição " + pos);

                #endif
                return -1;
            }
        }

        public int GetGridX(int pos)
        {
            try { return (int)grid_x[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {
                #if (DEBUG)

                Debug.Log("BancoDeDadosFIT.GetGridX - Não há posição " + pos);

                #endif
                return -1;
            }
        }

        public int GetGridY(int pos)
        {
            try { return (int)grid_y[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {
                #if (DEBUG)

                Debug.Log("BancoDeDadosFIT.GetGridY - Não há posição " + pos);

                #endif
                return -1;
            }
        }

        public int GetTempoDeServidor(int pos)
        {
            try { return (int)tempo_do_servidor[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {

                #if (DEBUG)

                Debug.Log("BancoDeDadosFIT.GetTempoDeServidor - Não há posição " + pos);

                #endif
                return -1;
            }
        }

        public string GetNomeDoJogador(int pos)
        {
            try { return (string)nome_do_jogador[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {

#if (DEBUG)

                Debug.Log("BancoDeDadosFIT.GetNomeDoJogador - Não há posição " + pos);

#endif
                return "";
            }
        }

        public int GetIdDoJogador(int pos)
        {
            try { return (int)id_do_jogador[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {

#if (DEBUG)

                Debug.Log("BancoDeDadosFIT.GetIdDoJogador - Não há posição " + pos);

#endif
                return -1;
            }
        }

        public int GetModoDeJogo(int pos)
        {
            try { return (int)modo_de_jogo[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {

#if (DEBUG)

                Debug.Log("BancoDeDadosFIT.GetModoDeJogo - Não há posição " + pos);

#endif
                return -1;
            }
        }

        /// <summary>
        /// Remove a entrada em bd_fit de posição i.
        /// <para>PRECISA ter checagem de erros.</para>
        /// </summary>
        /// <param name="i"></param>
        private void Remove(int i)
        {
            if (i < GetQuantidadeDeEntradas())
            {
                instante.RemoveAt(i);
                tempo.RemoveAt(i);                
                nivel.RemoveAt(i);                
                personagem.RemoveAt(i);                
                grid_x.RemoveAt(i);                
                grid_y.RemoveAt(i);                
                tempo_do_servidor.RemoveAt(i);
                nome_do_jogador.RemoveAt(i);
                id_do_jogador.RemoveAt(i);
                modo_de_jogo.RemoveAt(i);
            }
            else
            {
                //Tratamento de erro
            }
        }

        public void RemoveEntradasDoJogador()
        {

        }

        public void RemoveEntradasDoPersonagem()
        {

        }

        public void RemoveEntradasDoNivel()
        {

        }

        /// <summary>
        /// Função que remove entradas do BDFIT, especificamente caso ele tenha:
        ///     1 - Personagens específicos
        ///     2 - Níveis específicos
        ///     3 - Jogadores específicos
        /// </summary>
        public void RemoveEntradas(SortedList lista_de_jogadores = null, bool[] quais_jogadores = null,
                                   SortedList lista_de_personagens = null, bool[] quais_personagens = null,
                                   SortedList lista_de_niveis = null, bool[] quais_niveis = null
                                   )
        {
            if (lista_de_jogadores != null && quais_jogadores != null)
            {
                for (int i = lista_de_jogadores.Count - 1; i >= 0; i--)
                {
                    if (!quais_jogadores[i])
                    {
                        lista_de_jogadores.RemoveAt(i);
                    }
                }
            }
            
            for (int i = GetQuantidadeDeEntradas() - 1; i >= 0; i--)
            {
                if (lista_de_jogadores.
                    ContainsKey
                    (GetNomeDoJogador(i)))
                {
                    Remove(i);
                }
            }
            
        }



    }

    /// <summary>
    /// Responsável por guardar os dados do log carregado do Bolhas.
    /// </summary>
    public class BancoDeDadosBolhas
    {
        private ArrayList tempo;
        private ArrayList mouse_ou_objeto;
        private ArrayList coordenadas_x;
        private ArrayList coordenadas_y;

        private ArrayList qual_objeto;
        private ArrayList qual_frame;
        private ArrayList criado_agora;
        private ArrayList quem_criou;

        private ArrayList clicando;
        private ArrayList segurando;
        private ArrayList arrastando;

        private ArrayList o_que_fez;
        private ArrayList no_que_fez;

        public BancoDeDadosBolhas()
        {
            tempo = new ArrayList();
            mouse_ou_objeto = new ArrayList();
            coordenadas_x = new ArrayList();
            coordenadas_y = new ArrayList();

            qual_objeto = new ArrayList();
            qual_frame = new ArrayList();
            criado_agora = new ArrayList();
            quem_criou = new ArrayList();

            clicando = new ArrayList();
            segurando = new ArrayList();
            arrastando = new ArrayList();

            o_que_fez = new ArrayList();
            no_que_fez = new ArrayList();
    }

        public bool AddMouse(int time, string mouse, int x, int y, string clicado, string segurado, string arrastado)
        {

            tempo.Add(time);
            mouse_ou_objeto.Add(mouse);
            coordenadas_x.Add(x);
            coordenadas_y.Add(y);

            qual_objeto.Add("Mouse");
            qual_frame.Add(string.Empty);
            criado_agora.Add(string.Empty);
            quem_criou.Add(string.Empty);

            clicando.Add(clicado);
            segurando.Add(segurado);
            arrastando.Add(arrastado);
            return true;
        }

        public bool AddObjeto(int time, string objeto, int x, int y, string qual, int frame, string criadonessemomento,
                              string qcriou, string clicado, string segurado, string arrastado)
        {
            tempo.Add(time);
            mouse_ou_objeto.Add(objeto);
            coordenadas_x.Add(x);
            coordenadas_y.Add(y);

            qual_objeto.Add(qual);
            qual_frame.Add(frame);
            criado_agora.Add(criadonessemomento);
            quem_criou.Add(qcriou);

            clicando.Add(clicado);
            segurando.Add(segurado);
            arrastando.Add(arrastado);

            return true;
        }

        public int GetQuantidadeDeEntradas()
        {
            return mouse_ou_objeto.Count;
        }

        public int GetTempo(int pos)
        {
            try { return (int)tempo[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao) {
#if (DEBUG)
                Debug.Log("BancoDeDadosBolhas.GetTempo - Não há posição " + pos);
#endif
                return -1;
            }
        }

        public string GetMouseOuObjeto(int pos)
        {
            try { return (string)mouse_ou_objeto[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosBolhas.GetMouseOuObjeto - Não há posição " + pos);
#endif
                return string.Empty;
            }
        }

        public int GetCoordenadaX(int pos)
        {
            try { return (int)coordenadas_x[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosBolhas.GetCoordenadaX - Não há posição " + pos);
#endif
                return -1;
            }
        }

        public int GetCoordenadaY(int pos)
        {
            try { return (int)coordenadas_y[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosBolhas.GetCoordenadaY - Não há posição " + pos);
#endif
                return -1;
            }
        }

        public string GetQualObjeto(int pos)
        {
            try { return (string)qual_objeto[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao) {
#if (DEBUG)
                Debug.Log("BancoDeDadosBolhas.QualObjeto - Não há posição " + pos);
#endif
                return "";
            }
        }

        public int GetQualFrame(int pos)
        {
            try { return (int)qual_frame[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosBolhas.GetQualFrame - Não há posição " + pos);
#endif
                return -1;
            }
        }

        public string GetCriadoAgora(int pos)
        {
            try { return (string)criado_agora[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosBolhas.GetCriadoAgora - Não há posição " + pos);
#endif
                return "";
            }
        }

        public string GetQuemCriou(int pos)
        {
            try { return (string)quem_criou[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosBolhas.GetQuemCriou - Não há posição " + pos);
#endif
                return "";
            }
        }

        public string GetClicando(int pos)
        {
            try { return (string)clicando[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosBolhas.GetClicando - Não há posição " + pos);
#endif
                return "";
            }
        }

        public string GetSegurando(int pos)
        {
            try { return (string)segurando[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosBolhas.GetSegurando - Não há posição " + pos);
#endif
                return "";
            }
        }

        public string GetArrastando(int pos)
        {
            try { return (string)arrastando[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosBolhas.GetArrastando - Não há posição " + pos);
#endif
                return "";
            }
        }
    }

    /*
        (string pro dicionário)0 - O nome dos modos.
        (float, um, revertido pra esquerda)1 - Movimentação feita com as teclas esquerda e direita do teclado;
	    (float)2 - O x da câmera;
	    (float)3 - O y inicial da câmera;
	    (boolean)4 - Se a câmera é ortográfica ou em perspectiva?
	    (float, entre 0 e 1)5 - O alpha dos backgrounds;
	    (Vector3)6 - A rotação dos backgrounds.
        (Vector3)7 - A rotação da câmera.
    */
    /// <summary>
    /// Classe BancoDeDadosModos. Responsável por guardar os dados dos modos de visão possível dos logs.
    /// </summary>
    class BancoDeDadosModos
    {
        private Dictionary<string, float> movimentacao;
        private Dictionary<string, float> camera_x;
        private Dictionary<string, float> camera_init_y;
        private Dictionary<string, float> camera_z;
        private Dictionary<string, float> camera_init_z;
        private Dictionary<string, bool> orthographic;
        private Dictionary<string, float> alpha;
        private Dictionary<string, Vector3> rotation;
        private Dictionary<string, Vector3> rotation_camera;
        private Dictionary<string, float> visible_background_alpha;
        private Dictionary<string, int> layer;
        private Dictionary<string, string> instrucoes;

        public BancoDeDadosModos()
        {
            movimentacao = new Dictionary<string, float>();
            camera_x = new Dictionary<string, float>();
            camera_init_y = new Dictionary<string, float>();
            camera_z = new Dictionary<string, float>();
            camera_init_z = new Dictionary<string, float>();
            orthographic = new Dictionary<string, bool>();
            alpha = new Dictionary<string, float>();
            rotation = new Dictionary<string, Vector3>();
            rotation_camera = new Dictionary<string, Vector3>();
            visible_background_alpha = new Dictionary<string, float>();
            layer = new Dictionary<string, int>();
            instrucoes = new Dictionary<string, string>();
        }

        public bool Add(string modo, float mov, float x, float y, float z, float initz, bool ort,
            float alph, Vector3 rot, Vector3 rotationcam, float backalpha, int novolayer, string instrucao)
        {
            movimentacao.Add(modo, mov);
            camera_x.Add(modo, x);
            camera_init_y.Add(modo, y);
            camera_z.Add(modo, z);
            camera_init_z.Add(modo, initz);
            orthographic.Add(modo, ort);
            alpha.Add(modo, alph);
            rotation.Add(modo, rot);
            rotation_camera.Add(modo, rotationcam);
            visible_background_alpha.Add(modo, backalpha);
            layer.Add(modo, novolayer);
            instrucoes.Add(modo, instrucao);
            return true;
        }

        public int GetQuantidadeDeEntradas()
        {
            return movimentacao.Count;
        }

        public float GetVelocidadeDeMovimentacao(string modo)
        {
            try { return movimentacao[modo]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentNullException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetMovimentacao - Key modo é nula.");
#endif
                return -1;
            }
            catch (KeyNotFoundException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetMovimentacao - Key " + modo + " não existe.");
#endif
                return -1;
            }
        }

        public float GetCameraX(string modo)
        {
            try { return camera_x[modo]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentNullException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetCameraX - Key modo é nula.");
#endif
                return -1;
            }
            catch (KeyNotFoundException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetCameraX - Key " + modo + " não existe.");
#endif
                return -1;
            }
        }

        public float GetCameraInitY(string modo)
        {
            try { return camera_init_y[modo]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentNullException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetCameraInitY - Key modo é nula.");
#endif
                return -1;
            }
            catch (KeyNotFoundException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetCameraInitY - Key " + modo + " não existe.");
#endif
                return -1;
            }
        }

        public float GetCameraZ(string modo)
        {
            try { return camera_z[modo]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentNullException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetCameraZ - Key modo é nula.");
#endif
                return -1;
            }
            catch (KeyNotFoundException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetCameraZ - Key " + modo + " não existe.");
#endif
                return -1;
            }
        }

        public float GetCameraInitZ(string modo)
        {
            try { return camera_init_z[modo]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentNullException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetCameraInitZ - Key modo é nula.");
#endif
                return -1;
            }
            catch (KeyNotFoundException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetCameraInitZ - Key " + modo + " não existe.");
#endif
                return -1;
            }
        }

        public void AddCameraInitX(string modo, float add)
        {
            float x = GetCameraX(modo);
            x += add;
            camera_x.Remove(modo);
            camera_x.Add(modo, x);
        }

        //Aperfeiçoar essa função, acho que ela não precisa do remove.
        public void AddCameraInitY(string modo, float add)
        {
            float y = GetCameraInitY(modo);
            y += add;
            camera_init_y.Remove(modo);
            camera_init_y.Add(modo, y);
        }

        public void AddCameraZ(string modo, float add)
        {
            float z = GetCameraZ(modo);
            z += add;
            camera_x.Remove(modo);
            camera_x.Add(modo, z);
        }

        public void SetCameraInitY(string modo, float set)
        {
            camera_init_y.Remove(modo);
            camera_init_y.Add(modo, set);
        }

        public void SetCameraInitZ(string modo, float set)
        {
            camera_init_z.Remove(modo);
            camera_init_z.Add(modo, set);
        }

        public bool GetOrthographic(string modo)
        {
            try { return orthographic[modo]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentNullException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetOrthographic - Key modo é nula.");
#endif
                //Isso ainda vai dar problema um dia...
                return false;
            }
            catch (KeyNotFoundException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetOrthographic - Key " + modo + " não existe.");
#endif
                //Isso ainda vai dar problema um dia...
                return false;
            }
        }

        public float GetAlpha(string modo)
        {
            try { return alpha[modo]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentNullException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetAlpha - Key modo é nula.");
#endif
                //Isso ainda vai dar problema um dia...
                return -1;
            }
            catch (KeyNotFoundException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetAlpha - Key " + modo + " não existe.");
#endif
                //Isso ainda vai dar problema um dia...
                return -1;
            }
        }

        public Vector3 GetRotationChange(string modo)
        {
            try { return rotation[modo]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentNullException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetRotationChange - Key modo é nula.");
#endif
                //Isso ainda vai dar problema um dia...
                return new Vector3(float.MinValue, float.MinValue, float.MinValue);
            }
            catch (KeyNotFoundException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetRotationChange - Key " + modo + " não existe.");
#endif
                //Isso ainda vai dar problema um dia...
                return new Vector3(float.MinValue, float.MinValue, float.MinValue);
            }
        }

        public Vector3 GetRotationChangeCamera(string modo)
        {
            try { return rotation_camera[modo]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentNullException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetRotationChangeCamera - Key modo é nula.");
#endif
                //Isso ainda vai dar problema um dia...
                return new Vector3(float.MinValue, float.MinValue, float.MinValue);
            }
            catch (KeyNotFoundException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetRotationChangeCamera - Key " + modo + " não existe.");
#endif
                //Isso ainda vai dar problema um dia...
                return new Vector3(float.MinValue, float.MinValue, float.MinValue);
            }
        }

        public float GetVisibleBackgroundAlpha(string modo)
        {
            try { return visible_background_alpha[modo]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentNullException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetVisibleBackgroundAlpha - Key modo é nula.");
#endif
                //Isso ainda vai dar problema um dia...
                return -1;
            }
            catch (KeyNotFoundException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetVisibleBackgroundAlpha - Key " + modo + " não existe.");
#endif
                //Isso ainda vai dar problema um dia...
                return -1;
            }
        }

        public int GetLayer(string modo)
        {
            try { return layer[modo]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentNullException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetLayer - Key modo é nula.");
#endif
                //Isso ainda vai dar problema um dia...
                return -1;
            }
            catch (KeyNotFoundException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosModos.GetLayer - Key " + modo + " não existe.");
#endif
                //Isso ainda vai dar problema um dia...
                return -1;
            }
        }

        public string GetInstrucao(string modo)
        {
            return instrucoes[modo];
        }

    }

    /// <summary>
    /// Responsável por guardar todos os tipos de dados que fossem necessários para a criação da visualização
    /// dos dados do log.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ParaVisualizacao<T>
    {

        private Dictionary<string, T> para_heatmap;

        public ParaVisualizacao()
        {
            para_heatmap = new Dictionary<string, T>();
        }

        public bool Add(string palavra, T objeto)
        {
            try
            {
                para_heatmap.Add(palavra, objeto);
                return true;
            } catch (ArgumentNullException a) // value = NULL;
            {
#if (DEBUG)
                Debug.Log("ParaHeatmap.Add - Valor igual a null");
#endif
                return false;
            } catch (ArgumentException b) // palavra já existe no ParaHeatmap
            {
#if (DEBUG)
                Debug.Log("ParaHeatmap.Add - Palavra já existe");
#endif
                return false;
            }
        }

        public bool Remove(string palavra)
        {
            try
            {
                para_heatmap.Remove(palavra);
                return true;
            }
            catch (ArgumentNullException a) // value = NULL;
            {
#if (DEBUG)
                Debug.Log("ParaHeatmap.Remove - Valor igual a null");
#endif
                return false;
            }
        }

        public T Get(string palavra)
        {
            T objeto;

            try
            {
                para_heatmap.TryGetValue(palavra, out objeto);
                return objeto;
            } catch (ArgumentNullException a) // value = NULL;
            {
#if (DEBUG)
                Debug.Log("ParaHeatmap.Get - Palavra igual a null");
#endif
                return default(T);
            }

        }

    }

    /// <summary>
    /// Classe DadosGUIHeatMap. Responsável por guardar os dados sobre as cores do HeatMap. Precisa ter funções de entrada e saída.
    /// </summary>
    public class DadosGUIHeatMap
    {
        public List<int> numeros_de_cor;
        public List<Color> cores;
        public List<Texture2D> texturas_de_cor;

        public DadosGUIHeatMap()
        {
            numeros_de_cor = new List<int>();
            cores = new List<Color>();
            texturas_de_cor = new List<Texture2D>();
        }
    }

    /// <summary>
    /// Classe que guarda funções para pintura de Texturas2D, e em um caso, alterar Alpha de Materiais.
    /// </summary>
    class Pintar {

        public Texture2D TexturaBemA(Texture2D B, Texture2D A)
        {

            int x = 0;
            int y = 0;

            for (y = 0; y < B.height; y++)
            {
                for (x = 0; x < B.width; x++)
                {
                    A.SetPixel(x, y, B.GetPixel(x, y));
                }
            }

            return A;
        }


        public Material AlterarAlphaDeMaterial(Material A, float alpha)
        {
            Color cor = new Color();

            cor = A.color;
            cor.a = alpha;
            A.color = cor;

            return A;
        }

        public Texture2D SetPixelsEmTextura(Texture2D a, int x, int y, int blockwidth, int blockheight, Color cor)
        {

            for (int i = x; i < x + blockwidth; i++)
            {
                for (int j = y; j < y + blockheight; j++)
                {
                    a.SetPixel(i, j, cor);
                }
            }

            return a;
        }

        public Texture2D SetPixelsEmTodaTextura(Texture2D a, Color32[] cores)
        {
            //Checar se o tamanho de cores está correto.
            //Pintar

            if (cores.Length == a.height * a.width)
            {
                a.SetPixels32(cores);
            } else { return null; }

            return a;
            
        }

    }

    public class LidaComTexto
    {
        /// <summary>
        /// Função privada para fechar readers usados em outras funções de NovoLeitor2.
        /// </summary>
        public void FecharReaders(FileStream fs, StreamReader theReader)
        {
            theReader.Close();
            theReader.Dispose();
            fs.Close();
            fs.Dispose();
        }
    }

    /// <summary>
    /// Muda cenas e fecha a aplicação.
    /// </summary>
    public class MudaCenas
    {

        public static void MudarCenaPara_Tela_Inicial()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        public static void MudarCenaPara_Pre_Fit()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

        public static void MudarCenaPara_Pre_Bolhas()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }

        public static void Fechar_Aplicacao()
        {
            Application.Quit();
        }

        public static void MudarCenaPara_Load_Bolhas()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
        }

        public static void MudarCenaPara_Load_Fit()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }

        public static void MudarCenaPara_Selecao_Fit()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(5);
        }

    }

}