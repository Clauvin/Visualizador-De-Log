using UnityEngine;
using Basicas;
using System.Collections;
using System.Collections.Generic;

/*Modos de uso -
    1 - Um de Cada Vez, em 3D. Mostra uma série de N pontos com N backgrounds, navegáveis com os botões esquerda e
        direita do teclado, um de cada vez, do lado esquerdo da tela.
    2 - Todos de Uma Vez, em 2D. Mostra o mapa todo de uma vez, com todos os pontos.
    3 - Um de Cada Vez, em 2D. Mostra uma série de N pontos com 1 background, navegável com os botões esquerda e
        direita do teclado, um de cada vez.

*/

/// <summary>
/// Responsável por ler inputs de mouse e teclado para uso do visualizador de log.
/// </summary>
public class Controlador : MonoBehaviour {

    Vector3 posicao_da_camera;
    BancoDeDadosModos modos = new BancoDeDadosModos();
    public string modo_de_visualizacao = "Todos De Uma Vez em 2D";
    int count;
    string instrucoes_genericas;
    public string tipo;

    Transform objeto_clicado = null;

    float cam_sensitividade_X = 10.0f;
    float cam_sensitividade_Y = 10.0f;
    float cam_rotation_X = 0.0f;
    float cam_rotation_Y = 0.0f;
    float cam_min_mov_X = -360.0f;
    float cam_max_mov_X = 360.0f;
    float cam_min_mov_Y = -45.0f;
    float cam_max_mov_Y = 45.0f;

    //valor inicial guardado de câmera para o modo 'Todos De Uma Vez em 3D'
    Vector3 pos_camera_inicial_todos_de_uma_vez_em_3D = new Vector3();
    Quaternion pos_rot_inicial_todos_de_uma_vez_em_3D = new Quaternion();
    bool pegar_valor_de_camera_todos_de_uma_vez_em_3D = true;

    //para a seleção de heatmaps
    int qual_heatmap_mostrar = 0;

    //para o modo automático do 3
    bool auto_mode = false;

    //para o modo automático custom do 3
    bool auto_mode_custom = false;
    int posicao_temporal_1;
    int posicao_temporal_2;

    bool usuario_pode_fazer_input = false;
	
	// Update is called once per frame
	void Update () {

        if (usuario_pode_fazer_input) {

            posicao_da_camera = transform.position;

            if (modo_de_visualizacao == "Um Frame De Cada Vez em 2D") {

                if (Input.GetKeyUp("r"))
                {
                    if (auto_mode) auto_mode = false;
                    else auto_mode = true;
                }

                if (auto_mode)
                {
                    float y = modos.GetVelocidadeDeMovimentacao(modo_de_visualizacao);

                    posicao_da_camera.y -= y;

                    if (posicao_da_camera.y <= ((GameObject)GetComponent<NovoLeitor2>().lista_de_backgrounds[
                        GetComponent<NovoLeitor2>().lista_de_backgrounds.Count - 1]).transform.position.y)
                    {
                        posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().lista_de_backgrounds[0]).transform.position.y + 20.0f;
                    }

                    FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                }

                if (auto_mode_custom)
                {
                    float y = modos.GetVelocidadeDeMovimentacao(modo_de_visualizacao);

                    posicao_da_camera.y -= y;

                    if (posicao_da_camera.y <= ((GameObject)GetComponent<NovoLeitor2>().lista_de_backgrounds[
                        posicao_temporal_2]).transform.position.y)
                    {
                        posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().lista_de_backgrounds[
                            posicao_temporal_1]).transform.position.y;
                    }

                    FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                }

                if (Input.GetKeyUp("e"))
                {
                    string tempo = GetComponent<GuiTempo>().GetStringEditavel();
                    int posicao_temporal_;
                    bool resultado = int.TryParse(tempo, out posicao_temporal_);
                    if (resultado)
                    {
                        if (posicao_temporal_ <= GetComponent<NovoLeitor2>().GetUltimoTempo())
                        {
                            posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().lista_de_backgrounds[0]).transform.position.y +
                                              20.0f - 20.0f * posicao_temporal_;
                        } else
                        {
                            GetComponent<GuiTempo>().SetStringEditavel("Tempo " + tempo + " não existe.");
                        }
                    }

                    FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                }

                if (Input.GetKeyUp("t"))
                {
                    if (auto_mode_custom == false) {  
                        string tempo1 = GetComponent<GuiTempo>().GetAutoPassagemDeTempoCustomComecoEditavel();
                        string tempo2 = GetComponent<GuiTempo>().GetAutoPassagemDeTempoCustomFinalEditavel();

                        bool resultado1 = int.TryParse(tempo1, out posicao_temporal_1);
                        bool resultado2 = int.TryParse(tempo2, out posicao_temporal_2);

                        if (resultado1 && resultado2) {
                        
                            if (posicao_temporal_1 <= GetComponent<NovoLeitor2>().GetUltimoTempo() &&
                                posicao_temporal_2 <= GetComponent<NovoLeitor2>().GetUltimoTempo()) 
                            {
                                if (posicao_temporal_1 <= GetComponent<NovoLeitor2>().GetUltimoTempo())
                                {
                                    posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().lista_de_backgrounds[0]).transform.position.y +
                                                        20.0f - 20.0f * posicao_temporal_1;
                                }

                                FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                                auto_mode_custom = true;
                            }
                        } else
                        {
                            if (!resultado1) GetComponent<GuiTempo>().SetAutoPassagemDeTempoCustomComecoEditavel("Tempo " + tempo1 + " não existe.");
                            if (!resultado2) GetComponent<GuiTempo>().SetAutoPassagemDeTempoCustomFinalEditavel("Tempo " + tempo2 + " não existe.");
                        }

                        FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                    } else { auto_mode_custom = false; }
                }
            }

            //Necessário para esse modo específico por conta da movimentação livre dele
            if (modo_de_visualizacao == "Todos De Uma Vez em 3D") {

                if (Input.GetAxis("Horizontal") != 0)
                {
                    float x = modos.GetVelocidadeDeMovimentacao(modo_de_visualizacao);

                    if (Input.GetAxis("Horizontal") > 0) posicao_da_camera.x += x;
                    else if (Input.GetAxis("Horizontal") < 0) posicao_da_camera.x -= x;

                    FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                }

                //Esse código movimenta a câmera para frente e para trás.
                if (Input.GetAxis("Vertical") != 0)
                {
                    float y = modos.GetVelocidadeDeMovimentacao(modo_de_visualizacao);

                    if (Input.GetAxis("Vertical") > 0) posicao_da_camera.y -= y;
                    else if (Input.GetAxis("Vertical") < 0) posicao_da_camera.y += y;

                    FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                }

                //Esse código movimenta a câmera para cima e para baixo.
                if (Input.GetAxis("Zertical") != 0)
                {
                    float z = modos.GetVelocidadeDeMovimentacao(modo_de_visualizacao);

                    if (Input.GetAxis("Zertical") > 0) posicao_da_camera.z += z;
                    else if (Input.GetAxis("Zertical") < 0) posicao_da_camera.z -= z;

                    FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                }

                //Esse código movimenta a câmera para esquerda e para direita mais rápido.
                if ((Input.GetButton("a")) || (Input.GetButton("d")))
                {
                    float x = modos.GetVelocidadeDeMovimentacao(modo_de_visualizacao);

                    if (Input.GetButton("a")) posicao_da_camera.x -= (7 * x);
                    else if (Input.GetButton("d")) posicao_da_camera.x += (7 * x);

                    FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                }

                // Girar câmera no plano XY.
                if (Input.GetMouseButton(1))
                {
                    cam_rotation_X += Input.GetAxis("Mouse X") * cam_sensitividade_X * Time.deltaTime;
                    cam_rotation_Y += Input.GetAxis("Mouse Y") * cam_sensitividade_Y * Time.deltaTime;
                    cam_rotation_Y = Mathf.Clamp(cam_rotation_Y, cam_min_mov_Y, cam_max_mov_Y);
                    FindObjectOfType<Camera>().transform.Rotate(-cam_rotation_Y, cam_rotation_X, 0);
                }

                if (Input.GetMouseButtonUp(1))
                {
                    cam_rotation_X = 0;
                    cam_rotation_Y = 0;
                }

                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    float roda = modos.GetVelocidadeDeMovimentacao(modo_de_visualizacao) * 5;
                    if (Input.GetAxis("Mouse ScrollWheel") > 0) FindObjectOfType<Camera>().transform.Rotate(0, 0, roda);
                    else if (Input.GetAxis("Mouse ScrollWheel") < 0) FindObjectOfType<Camera>().transform.Rotate(0, 0, -roda);

                }

                if (Input.GetButton("r"))
                {

                    FindObjectOfType<Camera>().transform.position = pos_camera_inicial_todos_de_uma_vez_em_3D;
                    FindObjectOfType<Camera>().transform.rotation = pos_rot_inicial_todos_de_uma_vez_em_3D;

                }

                if (Input.GetButton("s"))
                {

                    FindObjectOfType<Camera>().transform.rotation = pos_rot_inicial_todos_de_uma_vez_em_3D;

                }

            } else if (modo_de_visualizacao == "Heatmap") {

                if ((Input.GetKeyUp("left")) || ((Input.GetKeyUp("right"))))
                {
                    if ((Input.GetKeyUp("left"))) { MostrarHeatmapAnterior(); }
                    if ((Input.GetKeyUp("right"))) { MostrarHeatmapPosterior(); }
                    GetComponent<NovoLeitor2>().ChangeTexturaHeatmap(qual_heatmap_mostrar);
                }
                   

            } else {
                //Esse código movimenta a câmera para frente e para trás.
                if (Input.GetAxis("Horizontal") != 0) {

                    if ((modo_de_visualizacao == "Um Frame De Cada Vez em 2D") && (auto_mode)){ }
                    else { 

                        float y = modos.GetVelocidadeDeMovimentacao(modo_de_visualizacao);

                        if (Input.GetAxis("Horizontal") > 0) posicao_da_camera.y -= y;
                        else if (Input.GetAxis("Horizontal") < 0) posicao_da_camera.y += y;

                        FindObjectOfType<Camera>().transform.position = posicao_da_camera;

                    }
                }

                //Esse código movimenta a câmera para frente e para trás mais rápido.
                if ((Input.GetButton("a")) || (Input.GetButton("d")))
                {
                    if ((modo_de_visualizacao == "Um Frame De Cada Vez em 2D") && (auto_mode)) { }
                    else
                    {
                        float y = modos.GetVelocidadeDeMovimentacao(modo_de_visualizacao);

                        if (Input.GetButton("a")) posicao_da_camera.y += (7 * y);
                        else if (Input.GetButton("d")) posicao_da_camera.y -= (7 * y);

                        FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                    }
                }

            }

            //if 

            if ((Input.GetButtonDown("1")) && (modo_de_visualizacao != "Todos De Uma Vez em 2D"))
            {
                GetComponent<NovoLeitor2>().ConectarTodos();
                Mudanca_De_Modo_De_Visualizacao("Todos De Uma Vez em 2D");

                GetComponent<NovoLeitor2>().PosicionarBackgrounds(20f);
                GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
                GetComponent<NovoLeitor2>().DesconectarTodos();
            }

            if ((Input.GetButtonDown("2")) && (modo_de_visualizacao != "Um Frame De Cada Vez em 3D"))
            {
                GetComponent<NovoLeitor2>().ConectarTodos();
                Mudanca_De_Modo_De_Visualizacao("Um Frame De Cada Vez em 3D");

                GetComponent<NovoLeitor2>().PosicionarBackgrounds(20f);
                GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
                GetComponent<NovoLeitor2>().DesconectarTodos();
            }

            if ((Input.GetButtonDown("3")) && (modo_de_visualizacao != "Um Frame De Cada Vez em 2D"))
            {
                GetComponent<NovoLeitor2>().ConectarTodos();
                Mudanca_De_Modo_De_Visualizacao("Um Frame De Cada Vez em 2D");

                GetComponent<NovoLeitor2>().PosicionarBackgrounds(20f);
                GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
                GetComponent<NovoLeitor2>().DesconectarTodos();
            }

            if ((Input.GetButtonDown("4")) && (modo_de_visualizacao != "Todos De Uma Vez em 3D"))
            {
                GetComponent<NovoLeitor2>().ConectarTodos();
                Mudanca_De_Modo_De_Visualizacao("Todos De Uma Vez em 3D");

                GetComponent<NovoLeitor2>().PosicionarBackgrounds(1f);
                GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
                GetComponent<Camera>().backgroundColor = Color.black;
                GetComponent<NovoLeitor2>().DesconectarTodos();
            }

            if ((Input.GetButtonDown("5")) && (modo_de_visualizacao != "Heatmap"))
            {
                GetComponent<NovoLeitor2>().ConectarTodos();
                Mudanca_De_Modo_De_Visualizacao("Heatmap");

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
                MudarTransparenciaDosObjetos(-0.2f);
                //DeixarObjetosEmEspacoDeTempoInvisiveisEIninteragiveis(0, 25);
                //DeixarTipoDeObjetoInvisivelEIninteragivel("Baleia");
            }

            if (Input.GetButtonDown("9"))
            {
                MudarTransparenciaDosObjetos(0.2f);
                //DeixarObjetosEmEspacoDeTempoVisiveisEInteragiveis(0, 25);
                //DeixarTipoDeObjetoVisivelEInteragivel("Baleia");
            }

            if (Input.GetButtonDown("-"))
            {
                AumentarVermelhidao(-0.2f);
            }

            if (Input.GetButtonDown("="))
            {
                AumentarVermelhidao(0.2f);
            }

            if (Input.GetButtonDown("Q"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }

            //Esse código serve para fazer a Gui dos pontos funcionar.
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit acerto = new RaycastHit();
                Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(raio, out acerto))
                {
                    if (acerto.collider.name.Contains("Background"))
                    {
                        GetComponent<GuiInfoObjeto>().EsconderGui();
                        NenhumPontoFoiClicado();
                    }
                } else {
                    GetComponent<GuiInfoObjeto>().EsconderGui();
                    NenhumPontoFoiClicado();
                }
            }
        }
    }

    public void AtualizarValoresDePosicionamento(float posicaoy0)
    {
        int pos = GetComponent<NovoLeitor2>().lista_de_backgrounds.Count/2;

        modos.AddCameraInitY("Um Frame De Cada Vez em 3D", posicaoy0);
        modos.AddCameraInitY("Todos De Uma Vez em 2D", posicaoy0);
        modos.AddCameraInitY("Um Frame De Cada Vez em 2D", posicaoy0);
        modos.AddCameraInitY("Todos De Uma Vez em 3D", posicaoy0+10f);
        modos.AddCameraInitX("Todos De Uma Vez em 3D", 0f);
        modos.AddCameraInitY("Heatmap", posicaoy0);
    }

    public void Mudanca_De_Modo_De_Visualizacao(string modonovo, bool forcar = false)
    {
        if ((modo_de_visualizacao != modonovo) || (forcar == true))
        {
            Vector3 posicaonova = GetComponent<Camera>().transform.position;
            posicaonova.x = modos.GetCameraX(modonovo);
            modos.SetCameraInitY(modo_de_visualizacao, posicaonova.y);
            posicaonova.y = modos.GetCameraInitY(modonovo);

            if (modo_de_visualizacao == "Todos De Uma Vez em 3D") modos.SetCameraInitZ(modo_de_visualizacao, posicaonova.z);
            if (modo_de_visualizacao == "Todos De Uma Vez em 3D")
            {
                ArrayList lista_de_backs = GetComponent<NovoLeitor2>().lista_de_backgrounds;
                int contagem = lista_de_backs.Count;
                for (int i = 0; i < contagem; i++)
                {
                    ((GameObject)lista_de_backs[i]).GetComponent<LigaDesliga>().Ligar();
                }
            }
            if (modo_de_visualizacao == "Todos De Uma Vez em 2D")
            {
                ArrayList lista_de_backs = GetComponent<NovoLeitor2>().lista_de_backgrounds;
                int contagem = lista_de_backs.Count - 1;
                for (int i = 0; i < contagem; i++)
                {
                    ((GameObject)lista_de_backs[i]).GetComponent<LigaDesliga>().Ligar();
                }
            }
        
            if (modonovo == "Todos De Uma Vez em 3D") posicaonova.z = modos.GetCameraInitZ(modonovo);
            else posicaonova.z = 0.0f;

            if (modonovo == "Todos De Uma Vez em 3D")
            {
                ArrayList lista_de_backs = GetComponent<NovoLeitor2>().lista_de_backgrounds;
                int contagem = lista_de_backs.Count;
                for (int i = 0; i < contagem; i++)
                {
                    ((GameObject)lista_de_backs[i]).GetComponent<LigaDesliga>().Desligar();
                }
            }
            if (modonovo == "Todos De Uma Vez em 2D")
            {
                ArrayList lista_de_backs = GetComponent<NovoLeitor2>().lista_de_backgrounds;
                int contagem = lista_de_backs.Count - 1;
                for (int i = 0; i < contagem; i++)
                {
                    ((GameObject)lista_de_backs[i]).GetComponent<LigaDesliga>().Desligar();
                }
            }

            GetComponent<Camera>().transform.position = posicaonova;
            GetComponent<Camera>().orthographic = modos.GetOrthographic(modonovo);

            GetComponent<NovoLeitor2>().ControlarAlpha(modos.GetAlpha(modonovo));
            GetComponent<NovoLeitor2>().GirarBackgrounds(modos.GetRotationChange(modonovo));
            GetComponent<NovoLeitor2>().GirarCamera(modos.GetRotationChangeCamera(modonovo));
            GetComponent<NovoLeitor2>().AlterarLayer(modos.GetLayer(modonovo));

            TransparenciaDoBackground(modos.GetVisibleBackgroundAlpha(modonovo));

            modo_de_visualizacao = modonovo;

            GetComponent<GuiModo>().MudarTexto(modo_de_visualizacao);
            GetComponent<GuiModo>().MudarInstrucoes(instrucoes_genericas + modos.GetInstrucao(modo_de_visualizacao));

            if ((modo_de_visualizacao == "Um Frame De Cada Vez em 3D") || (modo_de_visualizacao == "Um Frame De Cada Vez em 2D"))
            {
                GetComponent<GuiTempo>().RevelarGui();
            }
            else { GetComponent<GuiTempo>().EsconderGui(); }

            if (modo_de_visualizacao == "Heatmap")
            {
                GetComponent<GuiVisualizarTipos>().EsconderGui();
                GetComponent<GuiVisualizarPorTempo>().EsconderGui();
                GetComponent<GuiHeatmap>().RevelarGui();
            }
            else {
                GetComponent<GuiVisualizarTipos>().RevelarGui();
                GetComponent<GuiVisualizarPorTempo>().RevelarGui();
                GetComponent<GuiHeatmap>().EsconderGui();
            }

            if ((pegar_valor_de_camera_todos_de_uma_vez_em_3D) && (modo_de_visualizacao == "Todos De Uma Vez em 3D"))
            {
                pos_camera_inicial_todos_de_uma_vez_em_3D = GetComponent<Camera>().transform.position;
                pos_rot_inicial_todos_de_uma_vez_em_3D = GetComponent<Camera>().transform.rotation;
                pegar_valor_de_camera_todos_de_uma_vez_em_3D = false;
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

    public void MudarTransparenciaDosObjetos(float change) {

        MudancaDeCor(true, false, false, false, change);

    }

    void MudarTransparenciaDeTipoEspecificoDeObjetos(string nome, float transparencia)
    {

        int limit = GetComponent<NovoLeitor2>().lista_de_objetos.Count;

        for (int i = 0; i < limit; i++)
        {
            Color cor;
            if (((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).GetComponent<Dados>().nome_do_objeto == nome)
            {
                cor = ((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).
                            GetComponent<MeshRenderer>().material.GetColor("_Color");

                cor.a = transparencia;

                ((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).
                            GetComponent<MeshRenderer>().material.SetColor("_Color", cor);
            }
        }
    }

    void MudarTransparenciaDeObjetosDeEspacoDeTempoEspecifico(int tempo_minimo, int tempo_maximo, float transparencia)
    {

        int limit = GetComponent<NovoLeitor2>().lista_de_objetos.Count;

        for (int i = 0; i < limit; i++)
        {
            Color cor;
            int tempo = ((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).GetComponent<Dados>().tempo;
            if ((tempo_minimo <= tempo) && (tempo <= tempo_maximo))
            {
                cor = ((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).
                            GetComponent<MeshRenderer>().material.GetColor("_Color");

                cor.a = transparencia;

                ((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).
                            GetComponent<MeshRenderer>().material.SetColor("_Color", cor);
            }
        }
    }

    void SetInteracaoObjetosDeEspacoDeTempo(int tempo_minimo, int tempo_maximo, bool e_interagivel)
    {
        int limit = GetComponent<NovoLeitor2>().lista_de_objetos.Count;

        for (int i = 0; i < limit; i++)
        {
            int tempo = ((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).GetComponent<Dados>().tempo;
            if ((tempo_minimo <= tempo) && (tempo <= tempo_maximo))
            {
                if (e_interagivel) ((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).layer = 0;
                else ((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).layer = 2;
            }
        }
    }

    void SetInteracaoComTiposDeObjetos(string nome, bool e_interagivel)
    {
        int limit = GetComponent<NovoLeitor2>().lista_de_objetos.Count;

        for (int i = 0; i < limit; i++)
        {
            string nome_do_objeto = ((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).GetComponent<Dados>().nome_do_objeto;
            if (nome_do_objeto == nome)
            {
                if (e_interagivel) ((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).layer = 0;
                else ((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).layer = 2;
            }
        }
    }

    public void DeixarTipoDeObjetoInvisivelEIninteragivel(string nome)
    {
        MudarTransparenciaDeTipoEspecificoDeObjetos(nome, 0.0f);
        SetInteracaoComTiposDeObjetos(nome, false);
    }

    public void DeixarTipoDeObjetoVisivelEInteragivel(string nome)
    {
        MudarTransparenciaDeTipoEspecificoDeObjetos(nome, 1.0f);
        SetInteracaoComTiposDeObjetos(nome, true);
    }

    public void DeixarObjetosEmEspacoDeTempoInvisiveisEIninteragiveis(int tempo_minimo, int tempo_maximo)
    {
        MudarTransparenciaDeObjetosDeEspacoDeTempoEspecifico(tempo_minimo, tempo_maximo, 0.0f);
        SetInteracaoObjetosDeEspacoDeTempo(tempo_minimo, tempo_maximo, false);
    }

    public void DeixarObjetosEmEspacoDeTempoVisiveisEInteragiveis(int tempo_minimo, int tempo_maximo)
    {
        MudarTransparenciaDeObjetosDeEspacoDeTempoEspecifico(tempo_minimo, tempo_maximo, 1.0f);
        SetInteracaoObjetosDeEspacoDeTempo(tempo_minimo, tempo_maximo, true);
    }

    void TransparenciaDoBackground(float trans)
    {
        Color cor = ((GameObject)GetComponent<NovoLeitor2>().lista_de_backgrounds[count - 1]).
                GetComponent<MeshRenderer>().material.GetColor("_Color");

        cor.a = trans;
        ((GameObject)GetComponent<NovoLeitor2>().lista_de_backgrounds[count - 1]).
            GetComponent<MeshRenderer>().material.SetColor("_Color", cor);
    }

    void AumentarVermelhidao(float change)
    {

        MudancaDeCor(false, false, true, true, change);

    }

    public void PontoFoiClicado(Transform click)
    {
        if (objeto_clicado == null)
        {
            objeto_clicado = click;
            click.GetComponent<MeshRenderer>().material.mainTexture = GetComponent<NovoLeitor2>().
                texturas_selecionadas.Get(click.GetComponent<Dados>().nome_do_objeto.ToString());
        } else if (objeto_clicado != click) {
            objeto_clicado.GetComponent<MeshRenderer>().material.mainTexture = GetComponent<NovoLeitor2>().
                texturas.Get(objeto_clicado.GetComponent<Dados>().nome_do_objeto.ToString());
            objeto_clicado = click;
            click.GetComponent<MeshRenderer>().material.mainTexture = GetComponent<NovoLeitor2>().
                texturas_selecionadas.Get(click.GetComponent<Dados>().nome_do_objeto.ToString());
        }
              
    }

    public void NenhumPontoFoiClicado()
    {
        if (objeto_clicado != null) {
            objeto_clicado.GetComponent<MeshRenderer>().material.mainTexture = GetComponent<NovoLeitor2>().
                    texturas.Get(objeto_clicado.GetComponent<Dados>().nome_do_objeto.ToString());
            objeto_clicado = null;
        }
    }

    private void MudancaDeCor(bool a, bool r, bool g, bool b, float change)
    {
        for (int i = 0; i < GetComponent<NovoLeitor2>().lista_de_objetos.Count; i++)
        {
            Color cor = ((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).GetComponent<MeshRenderer>().
                material.GetColor("_Color");

            if (a) cor.a += change;
            if (r) cor.r += change;
            if (g) cor.g += change;
            if (b) cor.b += change;

            ((GameObject)GetComponent<NovoLeitor2>().lista_de_objetos[i]).GetComponent<MeshRenderer>().
                material.SetColor("_Color", cor);

        }
    }

    public int QualHeatmapMostra()
    {
        return qual_heatmap_mostrar;
    }

    public void AlterarQualHeatmapMostra(int qual)
    {
        if ((qual >= 0) && (qual < GetComponent<NovoLeitor2>().GetQuantHeatmaps()))
        {
            qual_heatmap_mostrar = qual;
        }
    }

    public void MostrarHeatmapAnterior()
    {
        qual_heatmap_mostrar--;
        if (qual_heatmap_mostrar < 0) qual_heatmap_mostrar = GetComponent<NovoLeitor2>().GetQuantHeatmaps() - 1;
    }

    public void MostrarHeatmapPosterior()
    {
        qual_heatmap_mostrar++;
        if (qual_heatmap_mostrar >= GetComponent<NovoLeitor2>().GetQuantHeatmaps()) qual_heatmap_mostrar = 0;
    }

    public bool GetAutoMode()
    {
        return auto_mode;
    }

    public bool GetAutoModeCustom()
    {
        return auto_mode_custom;
    }

    public void InicializacaoBolhas()
    {
        instrucoes_genericas = "Instrucoes:\n" +
                                 "1 - Muda para 'Todos De Uma Vez em 2D'\n" +
                                 "2 - Muda para 'Um Frame De Cada Vez em 3D'\n" +
                                 "3 - Muda para 'Um Frame De Cada Vez em 2D'\n" +
                                 "4 - Muda para 'Todos De Uma Vez em 3D'\n" +
                                 "5 - Muda para Heatmap\n";
        tipo = "Bolhas";
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
        tipo = "Fit";
        Inicializacao();
    }

    public void Inicializacao()
    {

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

        GameObject background = (GameObject)(GetComponent<NovoLeitor2>().lista_de_backgrounds[0]);

        AtualizarValoresDePosicionamento(background.transform.position.y);

        //gambiarra a ser corrigida posteriormente
        Vector3 pos = FindObjectOfType<Camera>().transform.position;
        pos.y = modos.GetCameraInitY(modo_de_visualizacao);
        FindObjectOfType<Camera>().transform.position = pos;
        //Daqui pra baixo, parte do FIT
        count = GetComponent<NovoLeitor2>().lista_de_backgrounds.Count;

        TransparenciaDoBackground(1f);
        GetComponent<NovoLeitor2>().ConectarTodos();
        Mudanca_De_Modo_De_Visualizacao("Todos De Uma Vez em 2D", true);
        GetComponent<NovoLeitor2>().PosicionarBackgrounds(20f);
        GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
        GetComponent<NovoLeitor2>().DesconectarTodos();
        GetComponent<GuiModo>().MudarInstrucoes(instrucoes_genericas + modos.GetInstrucao(modo_de_visualizacao));
        GetComponent<GuiModo>().RevelarGui();

        usuario_pode_fazer_input = true;

    }
}