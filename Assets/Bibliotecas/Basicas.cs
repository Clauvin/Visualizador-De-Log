//#define DEBUG 
//#undef DEBUG

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Basicas
{

    public class BancoDeDadosFIT
    {
        private ArrayList tempo;
        private ArrayList personagem;
        private ArrayList gridx;
        private ArrayList gridy;
        //Time: 1 = Char:1 = GridX:5 = GridY:7

        public BancoDeDadosFIT()
        {
            tempo = new ArrayList();
            personagem = new ArrayList();
            gridx = new ArrayList();
            gridy = new ArrayList();
        }

        public bool Add(int time, int pers, int x, int y)
        {
            tempo.Add(time);
            personagem.Add(pers);
            gridx.Add(x);
            gridy.Add(y);
            return true;
        }

        public int GetQuantidadeDeEntradas()
        {
            return tempo.Count;
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
            try { return (int)gridx[pos]; }
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
            try { return (int)gridy[pos]; }
            //ATENÇAO: Esse catch está correto?
            catch (ArgumentOutOfRangeException excecao)
            {
#if (DEBUG)
                Debug.Log("BancoDeDadosFIT.GetGridY - Não há posição " + pos);
#endif
                return -1;
            }
        }
    }

    public class BancoDeDadosBolhas
    {
        private ArrayList tempo;
        private ArrayList mouseouobjeto;
        private ArrayList coordenadasx;
        private ArrayList coordenadasy;

        private ArrayList qualobjeto;
        private ArrayList qualframe;
        private ArrayList criadoagora;
        private ArrayList quemcriou;

        private ArrayList clicando;
        private ArrayList segurando;
        private ArrayList arrastando;

        private ArrayList oquefez;
        private ArrayList noquefez;

        public BancoDeDadosBolhas()
        {
            tempo = new ArrayList();
            mouseouobjeto = new ArrayList();
            coordenadasx = new ArrayList();
            coordenadasy = new ArrayList();

            qualobjeto = new ArrayList();
            qualframe = new ArrayList();
            criadoagora = new ArrayList();
            quemcriou = new ArrayList();

            clicando = new ArrayList();
            segurando = new ArrayList();
            arrastando = new ArrayList();

            oquefez = new ArrayList();
            noquefez = new ArrayList();
    }

        public bool AddMouse(int time, string mouse, int x, int y, string clicado, string segurado, string arrastado)
        {

            tempo.Add(time);
            mouseouobjeto.Add(mouse);
            coordenadasx.Add(x);
            coordenadasy.Add(y);

            qualobjeto.Add("Mouse");
            qualframe.Add(string.Empty);
            criadoagora.Add(string.Empty);
            quemcriou.Add(string.Empty);

            clicando.Add(clicado);
            segurando.Add(segurado);
            arrastando.Add(arrastado);
            return true;
        }

        public bool AddObjeto(int time, string objeto, int x, int y, string qual, int frame, string criadonessemomento,
                              string qcriou, string clicado, string segurado, string arrastado)
        {
            tempo.Add(time);
            mouseouobjeto.Add(objeto);
            coordenadasx.Add(x);
            coordenadasy.Add(y);

            qualobjeto.Add(qual);
            qualframe.Add(frame);
            criadoagora.Add(criadonessemomento);
            quemcriou.Add(qcriou);

            clicando.Add(clicado);
            segurando.Add(segurado);
            arrastando.Add(arrastado);

            return true;
        }

        public int GetQuantidadeDeEntradas()
        {
            return mouseouobjeto.Count;
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
            try { return (string)mouseouobjeto[pos]; }
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
            try { return (int)coordenadasx[pos]; }
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
            try { return (int)coordenadasy[pos]; }
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
            try { return (string)qualobjeto[pos]; }
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
            try { return (int)qualframe[pos]; }
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
            try { return (string)criadoagora[pos]; }
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
            try { return (string)quemcriou[pos]; }
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
    class BancoDeDadosModos
    {
        private Dictionary<string, float> movimentacao;
        private Dictionary<string, float> camerax;
        private Dictionary<string, float> camerainity;
        private Dictionary<string, float> cameraz;
        private Dictionary<string, float> camerainitz;
        private Dictionary<string, bool> orthographic;
        private Dictionary<string, float> alpha;
        private Dictionary<string, Vector3> rotation;
        private Dictionary<string, Vector3> rotationcamera;
        private Dictionary<string, float> visiblebackgroundalpha;
        private Dictionary<string, int> layer;
        private Dictionary<string, string> instrucoes;

        public BancoDeDadosModos()
        {
            movimentacao = new Dictionary<string, float>();
            camerax = new Dictionary<string, float>();
            camerainity = new Dictionary<string, float>();
            cameraz = new Dictionary<string, float>();
            camerainitz = new Dictionary<string, float>();
            orthographic = new Dictionary<string, bool>();
            alpha = new Dictionary<string, float>();
            rotation = new Dictionary<string, Vector3>();
            rotationcamera = new Dictionary<string, Vector3>();
            visiblebackgroundalpha = new Dictionary<string, float>();
            layer = new Dictionary<string, int>();
            instrucoes = new Dictionary<string, string>();
        }

        public bool Add(string modo, float mov, float x, float y, float z, float initz, bool ort,
            float alph, Vector3 rot, Vector3 rotationcam, float backalpha, int novolayer, string instrucao)
        {
            movimentacao.Add(modo, mov);
            camerax.Add(modo, x);
            camerainity.Add(modo, y);
            cameraz.Add(modo, z);
            camerainitz.Add(modo, initz);
            orthographic.Add(modo, ort);
            alpha.Add(modo, alph);
            rotation.Add(modo, rot);
            rotationcamera.Add(modo, rotationcam);
            visiblebackgroundalpha.Add(modo, backalpha);
            layer.Add(modo, novolayer);
            instrucoes.Add(modo, instrucao);
            return true;
        }

        public int GetQuantidadeDeEntradas()
        {
            return movimentacao.Count;
        }

        public float GetMovimentacao(string modo)
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
            try { return camerax[modo]; }
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
            try { return camerainity[modo]; }
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
            try { return cameraz[modo]; }
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
            try { return camerainitz[modo]; }
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
            camerax.Remove(modo);
            camerax.Add(modo, x);
            //Debug.Log(modo + " " + x);
        }

        //Aperfeiçoar essa função, acho que ela não precisa do remove.
        public void AddCameraInitY(string modo, float add)
        {
            float y = GetCameraInitY(modo);
            y += add;
            camerainity.Remove(modo);
            camerainity.Add(modo, y);
            //Debug.Log(modo + " " + y);
        }

        public void AddCameraZ(string modo, float add)
        {
            float z = GetCameraZ(modo);
            z += add;
            camerax.Remove(modo);
            camerax.Add(modo, z);
        }

        public void SetCameraInitY(string modo, float set)
        {
            camerainity.Remove(modo);
            camerainity.Add(modo, set);
        }

        public void SetCameraInitZ(string modo, float set)
        {
            camerainitz.Remove(modo);
            camerainitz.Add(modo, set);
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
            try { return rotationcamera[modo]; }
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
            try { return visiblebackgroundalpha[modo]; }
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

    public class ParaHeatmap<T>
    {

        private Dictionary<string, T> paraheatmap;

        public ParaHeatmap()
        {
            paraheatmap = new Dictionary<string, T>();
        }

        public bool Add(string palavra, T objeto)
        {
            try
            {
                paraheatmap.Add(palavra, objeto);
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
                paraheatmap.Remove(palavra);
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
                paraheatmap.TryGetValue(palavra, out objeto);
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

    //melhorar depois para ter métodos de entrada, saída e etc
    class DadosGUIHashMap
    {
        public List<int> numerosdecor;
        public List<Color> cores;
        public List<Texture2D> texturasdecor;

        public DadosGUIHashMap()
        {
            numerosdecor = new List<int>();
            cores = new List<Color>();
            texturasdecor = new List<Texture2D>();
        }
    }

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

            int x = 0;
            int y = 0;
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

}