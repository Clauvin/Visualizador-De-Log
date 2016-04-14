using UnityEngine;
using Basicas;
using System.Collections;

/*Modos de uso -
    1 - Um de Cada Vez, em 3D. Mostra uma série de N pontos com N backgrounds, navegáveis com os botões esquerda e
        direita do teclado, um de cada vez, do lado esquerdo da tela.
    2 - Todos de Uma Vez, em 2D. Mostra o mapa todo de uma vez, com todos os pontos.
    3 - Um de Cada Vez, em 2D. Mostra uma série de N pontos com 1 background, navegável com os botões esquerda e
        direita do teclado, um de cada vez.

*/

/// <summary>
/// Classe Controlador, responsável por ler inputs de mouse e teclado para uso do visualizador de log.
/// <para></para>
/// </summary>
public class Controlador : MonoBehaviour {

    Vector3 posicaocamera;
    BancoDeDadosModos modos = new BancoDeDadosModos();
    public string modo_de_visualizacao = "Todos De Uma Vez em 2D";
    int count;
    string instrucoes_genericas;

    Transform objeto_clicado = null;

    float cam_sensitividade_X = 10.0f;
    float cam_sensitividade_Y = 10.0f;
    float rotationY = 0.0f;
    float rotationX = 0.0f;
    float minX = -360.0f;
    float maxX = 360.0f;
    float minY = -45.0f;
    float maxY = 45.0f;

    //valor inicial guardado de câmera para o modo 'Todos De Uma Vez em 3D'
    Vector3 poscamerainicial = new Vector3();
    Quaternion posrotinicial = new Quaternion();
    bool pegarvalorcameratodosdeumavez3D = true;

    //para a seleção de heatmaps
    int qualheatmap = 0;

    //para o modo automático do 3
    bool automode = false;

    //para o modo automático custom do 3
    bool automodecustom = false;
    int posicaotemporal1;
    int posicaotemporal2;


    //agoravai serve pra bloquear, se necessário, o input do jogador, o que evita bugs iniciais
    bool agoravai = false;
	
	// Update is called once per frame
	void Update () {

        if (agoravai) { 

            posicaocamera = transform.position;

            if (modo_de_visualizacao == "Um Frame De Cada Vez em 2D") {

                if (Input.GetKeyUp("r"))
                {
                    if (automode) automode = false;
                    else automode = true;
                }

                if (automode)
                {
                    float y = modos.GetMovimentacao(modo_de_visualizacao);

                    posicaocamera.y -= y;

                    if (posicaocamera.y <= ((GameObject)GetComponent<NovoLeitor2>().listadebackgrounds[
                        GetComponent<NovoLeitor2>().listadebackgrounds.Count - 1]).transform.position.y)
                    {
                        posicaocamera.y = ((GameObject)GetComponent<NovoLeitor2>().listadebackgrounds[0]).transform.position.y + 20.0f;
                    }

                    FindObjectOfType<Camera>().transform.position = posicaocamera;
                }

                if (automodecustom)
                {
                    float y = modos.GetMovimentacao(modo_de_visualizacao);

                    posicaocamera.y -= y;

                    

                    if (posicaocamera.y <= ((GameObject)GetComponent<NovoLeitor2>().listadebackgrounds[
                        posicaotemporal2]).transform.position.y)
                    {
                        posicaocamera.y = ((GameObject)GetComponent<NovoLeitor2>().listadebackgrounds[
                            posicaotemporal1]).transform.position.y;
                    }

                    FindObjectOfType<Camera>().transform.position = posicaocamera;
                }

                if (Input.GetKeyUp("e"))
                {
                    string tempo = GetComponent<GuiTempo>().GetStringEditavel();
                    int posicaotemporal;
                    bool resultado = int.TryParse(tempo, out posicaotemporal);
                    if (resultado)
                    {
                        if (posicaotemporal <= GetComponent<NovoLeitor2>().GetUltimoTempo())
                        {
                            posicaocamera.y = ((GameObject)GetComponent<NovoLeitor2>().listadebackgrounds[0]).transform.position.y +
                                              20.0f - 20.0f * posicaotemporal;
                        } else
                        {
                            GetComponent<GuiTempo>().SetStringEditavel("Tempo " + tempo + " não existe.");
                        }
                    }

                    FindObjectOfType<Camera>().transform.position = posicaocamera;
                }

                if (Input.GetKeyUp("t"))
                {
                    if (automodecustom == false) {  
                        string tempo1 = GetComponent<GuiTempo>().GetAutoCustomComecoEditavel();
                        string tempo2 = GetComponent<GuiTempo>().GetAutoCustomFinalEditavel();

                        bool resultado1 = int.TryParse(tempo1, out posicaotemporal1);
                        bool resultado2 = int.TryParse(tempo2, out posicaotemporal2);

                        if (resultado1 && resultado2) {
                        
                            if (posicaotemporal1 <= GetComponent<NovoLeitor2>().GetUltimoTempo() &&
                                posicaotemporal2 <= GetComponent<NovoLeitor2>().GetUltimoTempo()) 
                            {
                                if (posicaotemporal1 <= GetComponent<NovoLeitor2>().GetUltimoTempo())
                                {
                                    posicaocamera.y = ((GameObject)GetComponent<NovoLeitor2>().listadebackgrounds[0]).transform.position.y +
                                                        20.0f - 20.0f * posicaotemporal1;
                                }

                                FindObjectOfType<Camera>().transform.position = posicaocamera;
                                automodecustom = true;
                            }
                        } else
                        {
                            if (!resultado1) GetComponent<GuiTempo>().SetAutoCustomComecoEditavel("Tempo " + tempo1 + " não existe.");
                            if (!resultado2) GetComponent<GuiTempo>().SetAutoCustomFinalEditavel("Tempo " + tempo2 + " não existe.");
                        }

                        FindObjectOfType<Camera>().transform.position = posicaocamera;
                    } else { automodecustom = false; }
                }
            }

            //Necessário para esse modo específico por conta da movimentação livre dele
            if (modo_de_visualizacao == "Todos De Uma Vez em 3D") {

                //Esse código movimenta a câmera para esquerda e para direita.
                if (Input.GetAxis("Horizontal") != 0)
                {
                    float x = modos.GetMovimentacao(modo_de_visualizacao);

                    if (Input.GetAxis("Horizontal") > 0) posicaocamera.x += x;
                    else if (Input.GetAxis("Horizontal") < 0) posicaocamera.x -= x;

                    FindObjectOfType<Camera>().transform.position = posicaocamera;
                }

                //Esse código movimenta a câmera para frente e para trás.
                if (Input.GetAxis("Vertical") != 0)
                {
                    float y = modos.GetMovimentacao(modo_de_visualizacao);

                    if (Input.GetAxis("Vertical") > 0) posicaocamera.y -= y;
                    else if (Input.GetAxis("Vertical") < 0) posicaocamera.y += y;

                    FindObjectOfType<Camera>().transform.position = posicaocamera;
                }

                //Esse código movimenta a câmera para cima e para baixo.
                if (Input.GetAxis("Zertical") != 0)
                {
                    float z = modos.GetMovimentacao(modo_de_visualizacao);

                    if (Input.GetAxis("Zertical") > 0) posicaocamera.z += z;
                    else if (Input.GetAxis("Zertical") < 0) posicaocamera.z -= z;

                    FindObjectOfType<Camera>().transform.position = posicaocamera;
                }

                //Esse código movimenta a câmera para esquerda e para direita mais rápido.
                if ((Input.GetButton("a")) || (Input.GetButton("d")))
                {
                    float x = modos.GetMovimentacao(modo_de_visualizacao);

                    if (Input.GetButton("a")) posicaocamera.x -= (7 * x);
                    else if (Input.GetButton("d")) posicaocamera.x += (7 * x);

                    FindObjectOfType<Camera>().transform.position = posicaocamera;
                }

                if (Input.GetMouseButton(1))
                {
                    rotationX += Input.GetAxis("Mouse X") * cam_sensitividade_X * Time.deltaTime;
                    rotationY += Input.GetAxis("Mouse Y") * cam_sensitividade_Y * Time.deltaTime;
                    rotationY = Mathf.Clamp(rotationY, minY, maxY);
                    FindObjectOfType<Camera>().transform.Rotate(-rotationY, rotationX, 0);
                }

                if (Input.GetMouseButtonUp(1))
                {
                    rotationX = 0;
                    rotationY = 0;
                }

                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    float roda = modos.GetMovimentacao(modo_de_visualizacao) * 5;
                    if (Input.GetAxis("Mouse ScrollWheel") > 0) FindObjectOfType<Camera>().transform.Rotate(0, 0, roda);
                    else if (Input.GetAxis("Mouse ScrollWheel") < 0) FindObjectOfType<Camera>().transform.Rotate(0, 0, -roda);

                }

                if (Input.GetButton("r"))
                {

                    FindObjectOfType<Camera>().transform.position = poscamerainicial;
                    FindObjectOfType<Camera>().transform.rotation = posrotinicial;

                }

                if (Input.GetButton("s"))
                {

                    FindObjectOfType<Camera>().transform.rotation = posrotinicial;

                }

            } else if (modo_de_visualizacao == "Heatmap") {

                if ((Input.GetKeyUp("left")) || ((Input.GetKeyUp("right"))))
                {
                    if ((Input.GetKeyUp("left"))) { AlterarQualHeatmapMostraPre(); }
                    if ((Input.GetKeyUp("right"))) { AlterarQualHeatmapMostraPro(); }
                    GetComponent<NovoLeitor2>().ChangeTexturaHeatmap(qualheatmap);
                }
                   

            } else {
                //Esse código movimenta a câmera para frente e para trás.
                if (Input.GetAxis("Horizontal") != 0) {

                    if ((modo_de_visualizacao == "Um Frame De Cada Vez em 2D") && (automode)){ }
                    else { 

                        float y = modos.GetMovimentacao(modo_de_visualizacao);

                        if (Input.GetAxis("Horizontal") > 0) posicaocamera.y -= y;
                        else if (Input.GetAxis("Horizontal") < 0) posicaocamera.y += y;

                        FindObjectOfType<Camera>().transform.position = posicaocamera;

                    }
                }

                //Esse código movimenta a câmera para frente e para trás mais rápido.
                if ((Input.GetButton("a")) || (Input.GetButton("d")))
                {
                    if ((modo_de_visualizacao == "Um Frame De Cada Vez em 2D") && (automode)) { }
                    else
                    {
                        float y = modos.GetMovimentacao(modo_de_visualizacao);

                        if (Input.GetButton("a")) posicaocamera.y += (7 * y);
                        else if (Input.GetButton("d")) posicaocamera.y -= (7 * y);

                        FindObjectOfType<Camera>().transform.position = posicaocamera;
                    }
                }

            }

            if ((Input.GetButtonDown("1")) && (modo_de_visualizacao != "Todos De Uma Vez em 2D"))
            {
                GetComponent<NovoLeitor2>().ConectarTodos();
                Mudanca("Todos De Uma Vez em 2D");

                GetComponent<NovoLeitor2>().PosicionarBackgrounds(20f);
                GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
                GetComponent<NovoLeitor2>().DesconectarTodos();
            }

            if ((Input.GetButtonDown("2")) && (modo_de_visualizacao != "Um Frame De Cada Vez em 3D"))
            {
                GetComponent<NovoLeitor2>().ConectarTodos();
                Mudanca("Um Frame De Cada Vez em 3D");

                GetComponent<NovoLeitor2>().PosicionarBackgrounds(20f);
                GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
                GetComponent<NovoLeitor2>().DesconectarTodos();
            }

            if ((Input.GetButtonDown("3")) && (modo_de_visualizacao != "Um Frame De Cada Vez em 2D"))
            {
                //Daqui pra baixo, parte do FIT
                GetComponent<NovoLeitor2>().ConectarTodos();
                Mudanca("Um Frame De Cada Vez em 2D");

                GetComponent<NovoLeitor2>().PosicionarBackgrounds(20f);
                GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
                GetComponent<NovoLeitor2>().DesconectarTodos();
            }

            if ((Input.GetButtonDown("4")) && (modo_de_visualizacao != "Todos De Uma Vez em 3D"))
            {
                //Daqui pra baixo, parte do FIT
                GetComponent<NovoLeitor2>().ConectarTodos();
                Mudanca("Todos De Uma Vez em 3D");

                GetComponent<NovoLeitor2>().PosicionarBackgrounds(1f);
                GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
                GetComponent<Camera>().backgroundColor = Color.black;
                GetComponent<NovoLeitor2>().DesconectarTodos();
            }

            if ((Input.GetButtonDown("5")) && (modo_de_visualizacao != "Heatmap"))
            {
                //Daqui pra baixo, parte do FIT
                GetComponent<NovoLeitor2>().ConectarTodos();
                Mudanca("Heatmap");

                GetComponent<NovoLeitor2>().PosicionarBackgrounds(1f);
                GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
                GetComponent<Camera>().backgroundColor = Color.black;
                GetComponent<NovoLeitor2>().DesconectarTodos();
            }

            if (Input.GetButtonDown("6"))
            {
                Transparencia0();
            }

            if (Input.GetButtonDown("7"))
            {
                TransparenciaDeVoltaAoNormal(modo_de_visualizacao);

                if (modo_de_visualizacao == "Todos De Uma Vez em 2D"){ TransparenciaDoBackground(1f); }
            }

            if (Input.GetButtonDown("8"))
            {
                TransparenciaPontos(-0.2f);
            }

            if (Input.GetButtonDown("9"))
            {
                TransparenciaPontos(0.2f);
            }

            if (Input.GetButtonDown("-"))
            {
                Vermelhidao(-0.2f);
            }

            if (Input.GetButtonDown("="))
            {
                Vermelhidao(0.2f);
            }

            if (Input.GetButtonDown("Q"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }

            //Esse código serve para fazer a Gui dos pontos funcionar.
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit acerto = new RaycastHit();
                Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

                //Lembrando que só dá pra ver na Scene, não no jogo
                //Debug.DrawRay(GetComponent<Camera>().transform.position, raio.direction, Color.green, 3000, false);

                if (Physics.Raycast(raio, out acerto))
                {
                    if (acerto.collider.name.Contains("Background"))
                    {
                        GetComponent<GuiPonto>().EsconderGui();
                        NenhumPontoFoiClicado();
                    }
                } else {
                    GetComponent<GuiPonto>().EsconderGui();
                    NenhumPontoFoiClicado();
                }
            }

        }

    }

    //Esse código serve para ajeitar inicialmente as posições.
    public void AtualizarValoresDePosicionamento(float posicaoy0)
    {
        int pos = GetComponent<NovoLeitor2>().listadebackgrounds.Count/2;

        modos.AddCameraInitY("Um Frame De Cada Vez em 3D", posicaoy0);
        modos.AddCameraInitY("Todos De Uma Vez em 2D", posicaoy0);
        modos.AddCameraInitY("Um Frame De Cada Vez em 2D", posicaoy0);
        modos.AddCameraInitY("Todos De Uma Vez em 3D", posicaoy0+10f);
        modos.AddCameraInitX("Todos De Uma Vez em 3D", 0f);
        modos.AddCameraInitY("Heatmap", posicaoy0);
    }

    public void Mudanca(string modonovo, bool forcar = false)
    {
        if ((modo_de_visualizacao != modonovo) || (forcar == true))
        {
            Vector3 posicaonova = GetComponent<Camera>().transform.position;
            posicaonova.x = modos.GetCameraX(modonovo);
            modos.SetCameraInitY(modo_de_visualizacao, posicaonova.y);
            posicaonova.y = modos.GetCameraInitY(modonovo);
            if (modo_de_visualizacao == "Todos De Uma Vez em 3D") modos.SetCameraInitZ(modo_de_visualizacao, posicaonova.z);
            if (modonovo == "Todos De Uma Vez em 3D") posicaonova.z = modos.GetCameraInitZ(modonovo);
            else posicaonova.z = 0.0f;
            GetComponent<Camera>().transform.position = posicaonova;
            GetComponent<Camera>().orthographic = modos.GetOrthographic(modonovo);

            GetComponent<NovoLeitor2>().ControlarAlpha(modos.GetAlpha(modonovo));
            GetComponent<NovoLeitor2>().GirarBackgrounds(modos.GetRotationChange(modonovo));
            GetComponent<NovoLeitor2>().GirarCamera(modos.GetRotationChangeCamera(modonovo));
            GetComponent<NovoLeitor2>().AlterarLayer(modos.GetLayer(modonovo));

            TransparenciaDoBackground(modos.GetVisibleBackgroundAlpha(modonovo));

            modo_de_visualizacao = modonovo;

            GetComponent<GuiFITModo>().MudarTexto(modo_de_visualizacao);
            GetComponent<GuiFITModo>().MudarInstrucoes(instrucoes_genericas + modos.GetInstrucao(modo_de_visualizacao));

            if ((modo_de_visualizacao == "Um Frame De Cada Vez em 3D") || (modo_de_visualizacao == "Um Frame De Cada Vez em 2D"))
            {
                GetComponent<GuiTempo>().RevelarGui();
            }
            else { GetComponent<GuiTempo>().EsconderGui(); }

            if (modo_de_visualizacao == "Heatmap")
            {
                GetComponent<GuiFITHeatmap>().RevelarGui();
            }
            else { GetComponent<GuiFITHeatmap>().EsconderGui(); }

            if ((pegarvalorcameratodosdeumavez3D) && (modo_de_visualizacao == "Todos De Uma Vez em 3D"))
            {
                poscamerainicial = GetComponent<Camera>().transform.position;
                posrotinicial = GetComponent<Camera>().transform.rotation;
                pegarvalorcameratodosdeumavez3D = false;
            }
        }
    }

    void Transparencia0()
    {
        GetComponent<NovoLeitor2>().ControlarAlpha(0f);
    }

    void TransparenciaDeVoltaAoNormal(string modo)
    {
        GetComponent<NovoLeitor2>().ControlarAlpha(modos.GetAlpha(modo));
    }

    void TransparenciaPontos(float change) {

        MudancaDeCor(true, false, false, false, change);

    }

    void TransparenciaDoBackground(float trans)
    {
        Color cor = ((GameObject)GetComponent<NovoLeitor2>().listadebackgrounds[count - 1]).
                GetComponent<MeshRenderer>().material.GetColor("_Color");

        cor.a = trans;
        ((GameObject)GetComponent<NovoLeitor2>().listadebackgrounds[count - 1]).
            GetComponent<MeshRenderer>().material.SetColor("_Color", cor);
    }

    void Vermelhidao(float change)
    {

        MudancaDeCor(false, false, true, true, change);

    }

    public void PontoFoiClicado(Transform click)
    {
        if (objeto_clicado == null)
        {
            objeto_clicado = click;
            click.GetComponent<MeshRenderer>().material.mainTexture = GetComponent<NovoLeitor2>().
                texturasselecionadas.Get(click.GetComponent<Dados>().personagem.ToString());
        } else if (objeto_clicado != click) {
            objeto_clicado.GetComponent<MeshRenderer>().material.mainTexture = GetComponent<NovoLeitor2>().
                texturas.Get(objeto_clicado.GetComponent<Dados>().personagem.ToString());
            objeto_clicado = click;
            click.GetComponent<MeshRenderer>().material.mainTexture = GetComponent<NovoLeitor2>().
                texturasselecionadas.Get(click.GetComponent<Dados>().personagem.ToString());
        }
              
    }

    public void NenhumPontoFoiClicado()
    {
        if (objeto_clicado != null) {
            objeto_clicado.GetComponent<MeshRenderer>().material.mainTexture = GetComponent<NovoLeitor2>().
                    texturas.Get(objeto_clicado.GetComponent<Dados>().personagem.ToString());
            objeto_clicado = null;
        }
    }

    private void MudancaDeCor(bool a, bool r, bool g, bool b, float change)
    {
        for (int i = 0; i < GetComponent<NovoLeitor2>().listadepontos.Count; i++)
        {
            Color cor = ((GameObject)GetComponent<NovoLeitor2>().listadepontos[i]).GetComponent<MeshRenderer>().
                material.GetColor("_Color");

            if (a) cor.a += change;
            if (r) cor.r += change;
            if (g) cor.g += change;
            if (b) cor.b += change;

            ((GameObject)GetComponent<NovoLeitor2>().listadepontos[i]).GetComponent<MeshRenderer>().
                material.SetColor("_Color", cor);

        }
    }

    public int QualHeatmapMostra()
    {
        return qualheatmap;
    }

    public void AlterarQualHeatmapMostra(int qual)
    {
        if ((qual >= 0) && (qual < GetComponent<NovoLeitor2>().GetQuantHeatmaps()))
        {
            qualheatmap = qual;
        }
    }

    public void AlterarQualHeatmapMostraPre()
    {
        qualheatmap--;
        if (qualheatmap < 0) qualheatmap = GetComponent<NovoLeitor2>().GetQuantHeatmaps() - 1;
    }

    public void AlterarQualHeatmapMostraPro()
    {
        qualheatmap++;
        if (qualheatmap >= GetComponent<NovoLeitor2>().GetQuantHeatmaps()) qualheatmap = 0;
    }

    public bool GetAutoMode()
    {
        return automode;
    }


    public bool GetAutoModeCustom()
    {
        return automodecustom;
    }

    public void InicializacaoBolhas()
    {
        instrucoes_genericas = "Instrucoes:\n" +
                                 "1 - Muda para 'Todos De Uma Vez em 2D'\n" +
                                 "2 - Muda para 'Um Frame De Cada Vez em 3D'\n" +
                                 "3 - Muda para 'Um Frame De Cada Vez em 2D'\n" +
                                 "4 - Muda para 'Todos De Uma Vez em 3D'\n" +
                                 "5 - Muda para Heatmap\n";
        Inicializacao();
    }

    public void InicializacaoFIT()
    {
        instrucoes_genericas = "Instrucoes:\n" +
                                 "1 - Muda para 'Todos De Uma Vez em 2D'\n" +
                                 "2 - Muda para 'Um Frame De Cada Vez em 3D'\n" +
                                 "3 - Muda para 'Um Frame De Cada Vez em 2D'\n" +
                                 "4 - Muda para 'Todos De Uma Vez em 3D'\n" +
                                 "5 - Muda para Heatmap\n";
        Inicializacao();
    }

    public void Inicializacao()
    {

        //modos.Add("Um De Cada Vez 3D Bolhas", 0.1f, 15f, 30f, false, 1f, new Vector3(0f, 0f, 330f), new Vector3(90f, 0f, 0f));
        modos.Add("Todos De Uma Vez em 2D", 0f, 0f, 15f, 0f, 0f, true, 0f, new Vector3(0f, 0f, 0f),
            new Vector3(90f, 0f, 0f), 1f, 2, "6 - Some com os grids\n" + "7 - Faz os grids aparecerem\n" +
                                             "8 - Aumenta a transparencia dos pontos\n" +
                                             "9 - Diminui a transparencia dos pontos\n" +
                                             "- - Faz as cores dos pontos ficarem mais\n" + "    vermelhas\n" +
                                             "= - Faz as cores dos pontos voltarem ao normal\n" +
                                             "Q - Voltar à tela inicial");
        modos.Add("Um Frame De Cada Vez em 3D", 0.5f, 5f, 30f, 0f, 0f, false, 1f, new Vector3(0f, 0f, 330f),
            new Vector3(90f, 0f, 0f), 1f, 2, "6 - Some com os grids\n" + "7 - Faz os grids aparecerem\n" +
                                             "8 - Aumenta a transparencia dos pontos\n" +
                                             "9 - Diminui a transparencia dos pontos\n" +
                                             "- - Faz as cores dos pontos ficarem mais\n" + "    vermelhas\n" +
                                             "= - Faz as cores dos pontos voltarem ao normal\n" +
                                             "<- - Move a câmera para trás\n" +
                                             "-> - Move a câmera para frente(ela pode \n" +
                                             "atravessar um grid para ver o proximo\n" +
                                             "A - Move a câmera para trás mais rápido\n" +
                                             "D - Move a câmera para frente mais rápido\n" +
                                             "Q - Voltar à tela inicial");

        modos.Add("Um Frame De Cada Vez em 2D", 2f, 0f, 10f, 0f, 0f, true, 1f, new Vector3(0f, 0f, 0f),
            new Vector3(90f, 0f, 0f), 1f, 0, "6 - Some com os grids\n" + "7 - Faz os grids aparecerem\n" +
                                             "8 - Aumenta a transparencia dos pontos\n" +
                                             "9 - Diminui a transparencia dos pontos\n" +
                                             "- - Faz as cores dos pontos ficarem mais\n" + "    vermelhas\n" +
                                             "= - Faz as cores dos pontos voltarem ao normal\n" +
                                             "<- - Vai para a posição anterior dos pontos\n" +
                                             "-> - Vai para a posição seguinte dos pontos\n" +
                                             "A - Vai para a posição anterior dos pontos\n" +
                                             "mais rápido\n" +
                                             "D - Vai para a posição seguinte dos pontos\n" +
                                             "mais rápido\n" +
                                             "E - Vai para a posição do tempo escolhido\n" +
                                             "em 'Pular Para Posição'\n" +
                                             "R - Ativa/desativa modo automático\n" +
                                             "T - Ativa/desativa modo automático customizado\n" +
                                             "Q - Voltar à tela inicial");

        modos.Add("Todos De Uma Vez em 3D", 0.5f, 5f, 2f, 0f, 0f, false, 0f, new Vector3(0f, 0f, 0f),
            new Vector3(90f, 0f, 0f), 0f, 2, "8 - Aumenta a transparencia dos pontos\n" +
                                             "9 - Diminui a transparencia dos pontos\n" +
                                             "- - Faz as cores dos pontos ficarem mais\n" + "    vermelhas\n" +
                                             "= - Faz as cores dos pontos voltarem ao normal\n" +
                                             "<- - Move a câmera para trás\n" +
                                             "-> - Move a câmera para frente\n" +
                                             "A - Move a câmera para trás mais rápido\n" +
                                             "D - Move a câmera para frente mais rápido\n" +
                                             "R - Reseta a posição e rotação da câmera\n" +
                                             "S - Reseta a rotação da câmera\n" +
                                             "Q - Voltar à tela inicial");

        modos.Add("Heatmap", 0f, 200f, 15f, 0f, 0f, true, 0f, new Vector3(0f, 0f, 0f),
            new Vector3(90f, 0f, 0f), 1f, 2, "<- - Avança na lista de heatmaps\n" +
                                             "-> - Retorna na lista de heatmaps\n" +
                                             "Q - Voltar à tela inicial");

        GameObject background = (GameObject)(GetComponent<NovoLeitor2>().listadebackgrounds[0]);

        AtualizarValoresDePosicionamento(background.transform.position.y);

        //gambiarra a ser corrigida posteriormente
        Vector3 pos = FindObjectOfType<Camera>().transform.position;
        pos.y = modos.GetCameraInitY(modo_de_visualizacao);
        FindObjectOfType<Camera>().transform.position = pos;
        //Daqui pra baixo, parte do FIT
        count = GetComponent<NovoLeitor2>().listadebackgrounds.Count;

        TransparenciaDoBackground(1f);
        GetComponent<NovoLeitor2>().ConectarTodos();
        Mudanca("Todos De Uma Vez em 2D", true);
        GetComponent<NovoLeitor2>().PosicionarBackgrounds(20f);
        GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
        GetComponent<NovoLeitor2>().DesconectarTodos();
        GetComponent<GuiFITModo>().MudarInstrucoes(instrucoes_genericas + modos.GetInstrucao(modo_de_visualizacao));
        GetComponent<GuiFITModo>().RevelarGui();

        agoravai = true;

    }
}