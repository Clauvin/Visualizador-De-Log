using UnityEngine;
using Basicas;
using System.Collections;
using System;

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
public class Controlador : MonoBehaviour
{

    Vector3 posicao_da_camera;
    int get_tempo_anterior;
    BancoDeDadosModos modos = new BancoDeDadosModos();
    public string modo_de_visualizacao = "Todos De Uma Vez em 3D";
    string instrucoes_genericas = "Instrucoes:\n" +
                                 "2 - 'Um Frame De Cada Vez em 3D'\n" +
                                 "3 - 'Um Frame De Cada Vez em 2D'\n" +
                                 "4 - 'Todos De Uma Vez em 3D'\n" +
                                 "5 - Heatmap\n";
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

    private ArrayList lista_de_objetos_a_ligar_ou_desligar;

    string parte_da_transparencia_dos_objetos;

    // Update is called once per frame
    void Update()
    {

        if (usuario_pode_fazer_input)
        {

            posicao_da_camera = transform.position;

            if (modo_de_visualizacao == "Um Frame De Cada Vez em 2D")
            {

                if (Input.GetKeyUp("r"))
                {
                    if (auto_mode) auto_mode = false;
                    else auto_mode = true;
                }

                if (auto_mode)
                {
                    float y = modos.GetVelocidadeDeMovimentacao(modo_de_visualizacao);

                    posicao_da_camera.y -= y;

                    if (posicao_da_camera.y <= ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[
                        GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 1]).transform.position.y)
                    {
                        posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[0]).
                            transform.position.y + 20.0f;
                    }

                    FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                }

                if (auto_mode_custom)
                {
                    float y = modos.GetVelocidadeDeMovimentacao(modo_de_visualizacao);

                    posicao_da_camera.y -= y;

                    if (posicao_da_camera.y <= ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[
                        posicao_temporal_2]).transform.position.y)
                    {
                        posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[
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
                            posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[0]).transform.position.y +
                                              20.0f - 20.0f * posicao_temporal_;
                        }
                        else
                        {
                            GetComponent<GuiTempo>().SetStringEditavel("Instante " + tempo + " não existe.");
                        }
                    }

                    FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                }

                if (Input.GetKeyUp("t"))
                {
                    if (auto_mode_custom == false)
                    {
                        string tempo1 = GetComponent<GuiTempo>().GetAutoPassagemDeTempoCustomComecoEditavel();
                        string tempo2 = GetComponent<GuiTempo>().GetAutoPassagemDeTempoCustomFinalEditavel();

                        bool resultado1 = int.TryParse(tempo1, out posicao_temporal_1);
                        bool resultado2 = int.TryParse(tempo2, out posicao_temporal_2);

                        if (resultado1 && resultado2)
                        {

                            if (posicao_temporal_1 <= GetComponent<NovoLeitor2>().GetUltimoTempo() &&
                                posicao_temporal_2 <= GetComponent<NovoLeitor2>().GetUltimoTempo())
                            {
                                if (posicao_temporal_1 <= GetComponent<NovoLeitor2>().GetUltimoTempo())
                                {
                                    posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[0]).transform.position.y +
                                                        20.0f - 20.0f * posicao_temporal_1;
                                }

                                FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                                auto_mode_custom = true;
                            }
                        }
                        else
                        {
                            if (!resultado1) GetComponent<GuiTempo>().SetAutoPassagemDeTempoCustomComecoEditavel("Instante " + tempo1 + " não existe.");
                            if (!resultado2) GetComponent<GuiTempo>().SetAutoPassagemDeTempoCustomFinalEditavel("Instante " + tempo2 + " não existe.");
                        }

                        FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                    }
                    else { auto_mode_custom = false; }
                }
            }

            //Necessário para esse modo específico por conta da movimentação livre dele
            if (modo_de_visualizacao == "Todos De Uma Vez em 3D")
            {

                if (Input.GetAxis("Horizontal") != 0)
                {

                    AlterarPosicaoDeCamera('x');

                    FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                }

                //Esse código movimenta a câmera para frente e para trás.
                if (Input.GetAxis("Vertical") != 0)
                {
                    AlterarPosicaoDeCamera('y', false, "", "", false, 0, -1.0f);

                    FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                }

                //Esse código movimenta a câmera para cima e para baixo.
                if (Input.GetAxis("Zertical") != 0)
                {
                    AlterarPosicaoDeCamera('z');

                    FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                }

                //Esse código movimenta a câmera para esquerda e para direita mais rápido.
                if ((Input.GetButton("a")) || (Input.GetButton("d")))
                {

                    AlterarPosicaoDeCamera('x', false, "", "", false, 0, 7, true, "a", "d");

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

            }
            else if (modo_de_visualizacao == "Heatmap")
            {

                if ((Input.GetKeyUp("left")) || ((Input.GetKeyUp("right"))))
                {
                    if ((Input.GetKeyUp("left"))) { MostrarHeatmapAnterior(); }
                    if ((Input.GetKeyUp("right"))) { MostrarHeatmapPosterior(); }
                    for (int i = 0; i < GetComponent<NovoLeitor2>().objs_jogadores_fit.QuantosJogadores(); i++)
                    {
                        GetComponent<NovoLeitor2>().qual_jogador = i;
                        GetComponent<NovoLeitor2>().ChangeTexturaHeatmap(qual_heatmap_mostrar);
                    }
                    
                }


            }
            else
            {

                //Esse código movimenta a câmera para frente e para trás.
                if (Input.GetAxis("Horizontal") != 0)
                {

                    if ((modo_de_visualizacao == "Um Frame De Cada Vez em 2D") && (auto_mode)) { }
                    else
                    {

                        AlterarPosicaoDeCamera('y', true, "Horizontal", "Vertical", false, 0, -1.0f);

                        if (posicao_da_camera.y > ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[0]).
                            transform.position.y + 20.0f)
                        {
                            posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[0]).
                                transform.position.y + 20.0f;
                        }

                        float limitacao = 5.0f;
                        if (modo_de_visualizacao == "Um Frame De Cada Vez em 3D") limitacao = -15.0f;

                        if (GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count > 1)
                        {
                            if (posicao_da_camera.y < ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[
                                                        GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 2
                                                        ]).transform.position.y + limitacao - 5.0f)
                            {
                                posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[
                                                        GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 2
                                                        ]).transform.position.y + limitacao - 5.0f;
                            }
                        } else
                        {
                            if (posicao_da_camera.y < ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[
                                                        GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 1
                                                        ]).transform.position.y + 10.0f)
                            {
                                posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[
                                                        GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 1
                                                        ]).transform.position.y + 10.0f;
                            }
                        }



                        FindObjectOfType<Camera>().transform.position = posicao_da_camera;

                    }
                }

                //Esse código movimenta a câmera para frente e para trás mais rápido.
                if ((Input.GetButton("a")) || (Input.GetButton("d")))
                {
                    if ((modo_de_visualizacao == "Um Frame De Cada Vez em 2D") && (auto_mode)) { }
                    else
                    {
                        AlterarPosicaoDeCamera('y', false, "", "", false, 0, -7.0f, true, "a", "d");

                        if (posicao_da_camera.y > ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[0]).transform.position.y + 20.0f)
                        {
                            posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[0]).transform.position.y + 20.0f;
                        }

                        float limitacao = 5.0f;
                        if (modo_de_visualizacao == "Um Frame De Cada Vez em 3D") limitacao = -15.0f;

                        if (GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 1 > 1)
                        {
                            if (posicao_da_camera.y < ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[
                                                    GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 2
                                                    ]).transform.position.y + limitacao - 5.0f)
                            {
                                posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[
                                                        GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 2
                                                        ]).transform.position.y + limitacao - 5.0f;
                            }
                        } else
                        {
                            if (posicao_da_camera.y < ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[
                                                    GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 1
                                                    ]).transform.position.y + 10.0f)
                            {
                                posicao_da_camera.y = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[
                                                        GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 1
                                                        ]).transform.position.y + 10.0f;
                            }
                        }

                        

                        FindObjectOfType<Camera>().transform.position = posicao_da_camera;
                    }
                }

                if (modo_de_visualizacao == "Um Frame De Cada Vez em 2D")
                {
                    if (GetComponent<GuiTempo>().GetTempo() != get_tempo_anterior)
                    {
                        int tempo_anterior = get_tempo_anterior;

                        for (int i = 0; i < GetComponent<NovoLeitor2>().objs_jogadores_fit.QuantosJogadores(); i++)
                        {
                            GetComponent<NovoLeitor2>().qual_jogador = i;
                            get_tempo_anterior = tempo_anterior;
                            if ((get_tempo_anterior - 1 >= 0) && (get_tempo_anterior <=
                                    GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 1))
                            {
                                ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[get_tempo_anterior - 1]).
                                    GetComponent<LigaDesliga>().Desligar();
                            }

                            if (get_tempo_anterior <= GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 1)
                            {
                                ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[get_tempo_anterior]).
                                GetComponent<LigaDesliga>().Desligar();
                            }

                            if (get_tempo_anterior + 1 <= GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 1)
                            {
                                ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[get_tempo_anterior + 1]).
                                    GetComponent<LigaDesliga>().Desligar();
                            }

                            get_tempo_anterior = GetComponent<GuiTempo>().GetTempo();

                            if ((get_tempo_anterior - 1 >= 0) && (get_tempo_anterior <=
                                    GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 1))
                            {
                                ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[get_tempo_anterior - 1]).
                                    GetComponent<LigaDesliga>().Ligar();
                            }

                            if (get_tempo_anterior <= GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 1)
                            {
                                ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[get_tempo_anterior]).
                                GetComponent<LigaDesliga>().Ligar();
                            }

                            if (get_tempo_anterior + 1 <= GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count - 1)
                            {
                                ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[get_tempo_anterior + 1]).
                                    GetComponent<LigaDesliga>().Ligar();
                            }
                        }
                    }
                }

            }

            if ((Input.GetButtonDown("2")) && (modo_de_visualizacao != "Um Frame De Cada Vez em 3D"))
            {
                
                Mudanca_De_Modo_De_Visualizacao("Um Frame De Cada Vez em 3D");

                for (int i = 0; i < GetComponent<NovoLeitor2>().objs_jogadores_fit.QuantosJogadores(); i++)
                {
                    GetComponent<NovoLeitor2>().qual_jogador = i;
                    GetComponent<NovoLeitor2>().ConectarTodos();
                    GetComponent<NovoLeitor2>().PosicionarBackgrounds(20f);
                    //GetComponent<NovoLeitor2>().DesconectarTodos();
                }

                GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
            }

            if ((Input.GetButtonDown("3")) && (modo_de_visualizacao != "Um Frame De Cada Vez em 2D"))
            {
                
                Mudanca_De_Modo_De_Visualizacao("Um Frame De Cada Vez em 2D");

                for (int i = 0; i < GetComponent<NovoLeitor2>().objs_jogadores_fit.QuantosJogadores(); i++)
                {
                    GetComponent<NovoLeitor2>().qual_jogador = i;
                    GetComponent<NovoLeitor2>().ConectarTodos();
                    GetComponent<NovoLeitor2>().PosicionarBackgrounds(20f);
                    //GetComponent<NovoLeitor2>().DesconectarTodos();
                }

                GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
            }

            if ((Input.GetButtonDown("4")) && (modo_de_visualizacao != "Todos De Uma Vez em 3D"))
            {
                
                Mudanca_De_Modo_De_Visualizacao("Todos De Uma Vez em 3D");

                // Gambiarra temporária para evitar problemas de posicionamento errado do primeiro jogador,
                // em caso de dupla.

                GetComponent<Camera>().backgroundColor = Color.black;

                for (int i = 0; i < GetComponent<NovoLeitor2>().objs_jogadores_fit.QuantosJogadores(); i++)
                {
                    GetComponent<NovoLeitor2>().qual_jogador = i;
                    GetComponent<NovoLeitor2>().ConectarTodos();
                    GetComponent<NovoLeitor2>().PosicionarBackgrounds(1f);
                    
                    if (GetComponent<NovoLeitor2>().Ancora.activeSelf) GetComponent<NovoLeitor2>().DesconectarTodos();

                    ArrayList lista_de_backs = GetComponent<NovoLeitor2>().Lista_de_backgrounds;
                    int contagem = lista_de_backs.Count;
                    int desligar = 0;
                    for (int j = desligar; j < contagem; j++)
                    {
                        ((GameObject)lista_de_backs[j]).GetComponent<LigaDesliga>().Desligar();
                    }
                }

                GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
            }

            if ((Input.GetButtonDown("5")) && (modo_de_visualizacao != "Heatmap"))
            {
                Mudanca_De_Modo_De_Visualizacao("Heatmap");

                GetComponent<Camera>().backgroundColor = Color.black;

                /*for (int i = 0; i < GetComponent<NovoLeitor2>().objs_jogadores_fit.QuantosJogadores(); i++)
                {
                    GetComponent<NovoLeitor2>().ConectarTodos();
                    GetComponent<NovoLeitor2>().PosicionarBackgrounds(1f);
                    
                    GetComponent<NovoLeitor2>().DesconectarTodos();
                }*/

                GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
            }

            if (tipo == "Bolhas")
            {
                if (Input.GetButtonDown("8")) MudarTransparenciaDosObjetos(-0.2f);

                if (Input.GetButtonDown("9")) MudarTransparenciaDosObjetos(0.2f);
            }

            if (Input.GetButtonDown("Q"))
            {
                MudaCenas.Fechar_Aplicacao();
            }

            //Esse código serve para fazer a Gui dos pontos funcionar.
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit acerto = new RaycastHit();
                Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(raio, out acerto))
                {
                    if (acerto.collider.name.Contains("Background"))
                    {
                        GetComponent<GuiInfoObjeto>().EsconderGui();
                        NenhumPontoFoiClicado();
                    }
                }
                else
                {
                    GetComponent<GuiInfoObjeto>().EsconderGui();
                    NenhumPontoFoiClicado();
                }
            }
        }
    }

    public void AtualizarValoresDePosicionamento(float posicaoy0)
    {
        modos.SetCameraInitY("Um Frame De Cada Vez em 3D", posicaoy0 + 20f);
        modos.SetCameraInitY("Um Frame De Cada Vez em 2D", posicaoy0 + 20f);
        modos.SetCameraInitY("Todos De Uma Vez em 3D", posicaoy0 + 20f);

        modos.AddCameraInitX("Todos De Uma Vez em 3D", 0f);

        modos.SetCameraInitY("Heatmap", posicaoy0 + 1f);
    }

    public void Mudanca_De_Modo_De_Visualizacao(string modonovo, bool forcar = false)
    {
        if ((modo_de_visualizacao != modonovo) || (forcar == true))
        {

            Vector3 posicaonova = GetComponent<Camera>().transform.position;
            posicaonova.x = modos.GetCameraX(modonovo);

            modos.SetCameraInitY(modo_de_visualizacao, posicaonova.y);
            posicaonova.y = modos.GetCameraInitY(modonovo);

            if ((modo_de_visualizacao == "Todos De Uma Vez em 3D") || (modo_de_visualizacao == "Um Frame De Cada Vez em 2D"))
            {
                modos.SetCameraInitZ(modo_de_visualizacao, posicaonova.z);

                for (int i = 0; i < GetComponent<NovoLeitor2>().objs_jogadores_fit.QuantosJogadores(); i++)
                {
                    GetComponent<NovoLeitor2>().qual_jogador = i;
                    ArrayList lista_de_backs = GetComponent<NovoLeitor2>().Lista_de_backgrounds;
                    int contagem = lista_de_backs.Count;
                    if (modo_de_visualizacao == "Um Frame De Cada Vez em 2D")
                    {
                        //GetComponent<NovoLeitor2>().DesconectarTodos();
                    }
                    else if (modo_de_visualizacao == "Todos De Uma Vez em 3D")
                    {
                        GetComponent<NovoLeitor2>().ConectarTodos();
                    }

                    for (int j = 0; j < contagem; j++)
                    {
                        ((GameObject)lista_de_backs[j]).GetComponent<LigaDesliga>().Ligar();
                    }
                }

            }
            if ((modonovo == "Todos De Uma Vez em 3D") || (modonovo == "Um Frame De Cada Vez em 2D"))
            {
                posicaonova.z = modos.GetCameraInitZ(modonovo);

                for (int i = 0; i < GetComponent<NovoLeitor2>().objs_jogadores_fit.QuantosJogadores(); i++)
                {
                    GetComponent<NovoLeitor2>().qual_jogador = i;
                    ArrayList lista_de_backs = GetComponent<NovoLeitor2>().Lista_de_backgrounds;
                    int contagem = lista_de_backs.Count;
                    int desligar = 0;
                    if (modonovo == "Todos De Uma Vez em 3D") desligar = 0;
                    else if (modonovo == "Um Frame De Cada Vez em 2D")
                    {
                        //GetComponent<NovoLeitor2>().ConectarTodos();
                        desligar = 1;
                    }
                    for (int j = desligar; j < contagem; j++)
                    {
                        ((GameObject)lista_de_backs[j]).GetComponent<LigaDesliga>().Desligar();
                    }
                }

            }
            if (modonovo != "Todos De Uma Vez em 3D") posicaonova.z = 0.0f;

            GetComponent<Camera>().transform.position = posicaonova;
            GetComponent<Camera>().orthographic = modos.GetOrthographic(modonovo);

            for (int i = 0; i < GetComponent<NovoLeitor2>().objs_jogadores_fit.QuantosJogadores(); i++)
            {
                GetComponent<NovoLeitor2>().qual_jogador = i;
                GetComponent<NovoLeitor2>().ControlarAlpha(modos.GetAlpha(modonovo));
                GetComponent<NovoLeitor2>().GirarBackgrounds(modos.GetRotationChange(modonovo));
                GetComponent<NovoLeitor2>().AlterarLayer(modos.GetLayer(modonovo));
                TransparenciaDoBackground(modos.GetVisibleBackgroundAlpha(modonovo));
            }

            GetComponent<NovoLeitor2>().GirarCamera(modos.GetRotationChangeCamera(modonovo));
            
            if (modonovo == "Um Frame De Cada Vez em 2D")
            {
                get_tempo_anterior = GetComponent<GuiTempo>().GetTempo();

            }

            modo_de_visualizacao = modonovo;

            GetComponent<GuiModo>().MudarTexto(modo_de_visualizacao);
            GetComponent<GuiModo>().MudarInstrucoes(instrucoes_genericas + modos.GetInstrucao(modo_de_visualizacao));

            if ((modo_de_visualizacao == "Um Frame De Cada Vez em 3D") || (modo_de_visualizacao == "Um Frame De Cada Vez em 2D"))
            {
                if (modo_de_visualizacao == "Um Frame De Cada Vez em 3D") GetComponent<GuiTempo>().DesativarGuiDoAutoMode();
                else if (modo_de_visualizacao == "Um Frame De Cada Vez em 2D") GetComponent<GuiTempo>().AtivarGuiDoAutoMode();
                GetComponent<GuiTempo>().RevelarGui();
            }
            else { GetComponent<GuiTempo>().EsconderGui(); }

            if (modo_de_visualizacao == "Heatmap")
            {
                GetComponent<GuiVisualizarTipos>().EsconderGui();
                GetComponent<GuiVisualizarPorTempo>().EsconderGui();
                GetComponent<GuiHeatmap>().RevelarGui();
            }
            else
            {
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

            AlteracaoDePosicionamentoDeJogadores();

        }

    }

    void AlterarPosicaoDeCamera(char coordenada, bool usar_eixo_custom = false, string o_eixo_das_teclas = "", string o_eixo = "",
                                bool usar_valor_custom = false, float valor = 0.0f, float acelerador = 1.0f,
                                bool usa_teclas_custom = false, string tecla_menos = "", string tecla_mais = "")
    {

        float valor_final;
        string eixo = "";
        int eixo_int = 0;
        if (usar_valor_custom) valor_final = valor;
        else valor_final = modos.GetVelocidadeDeMovimentacao(modo_de_visualizacao);

        if (usar_eixo_custom == false)
        {
            if (coordenada == 'x') { eixo = "Horizontal"; eixo_int = 0; }
            else if (coordenada == 'y') { eixo = "Vertical"; eixo_int = 1; }
            else if (coordenada == 'z') { eixo = "Zertical"; eixo_int = 2; }
        } else
        {
            eixo = o_eixo_das_teclas;
            if (o_eixo == "Horizontal") eixo_int = 0;
            if (o_eixo == "Vertical") eixo_int = 1;
            if (o_eixo == "Zertical") eixo_int = 2;
        }

        if (usa_teclas_custom == false)
        {
            if (Input.GetAxis(eixo) > 0) posicao_da_camera[eixo_int] += valor_final * acelerador;
            else if (Input.GetAxis(eixo) < 0) posicao_da_camera[eixo_int] -= valor_final * acelerador;
        } else
        {
            if (Input.GetButton(tecla_menos)) posicao_da_camera[eixo_int] -= valor_final * acelerador;
            else if (Input.GetButton(tecla_mais)) posicao_da_camera[eixo_int] += valor_final * acelerador;
        }
        

    }

    Vector3 AlterarPosicaoDeCamera(Vector3 posicao, char coordenada, bool usar_eixo_custom = false, string o_eixo_das_teclas = "",
                                string o_eixo = "", bool usar_valor_custom = false, float valor = 0.0f, float acelerador = 1.0f,
                                bool usa_teclas_custom = false, string tecla_menos = "", string tecla_mais = "")
    {

        float valor_final;
        string eixo = "";
        int eixo_int = 0;
        if (usar_valor_custom) valor_final = valor;
        else valor_final = modos.GetVelocidadeDeMovimentacao(modo_de_visualizacao);

        if (usar_eixo_custom == false)
        {
            if (coordenada == 'x') { eixo = "Horizontal"; eixo_int = 0; }
            else if (coordenada == 'y') { eixo = "Vertical"; eixo_int = 1; }
            else if (coordenada == 'z') { eixo = "Zertical"; eixo_int = 2; }
        }
        else
        {
            eixo = o_eixo_das_teclas;
            if (o_eixo == "Horizontal") eixo_int = 0;
            if (o_eixo == "Vertical") eixo_int = 1;
            if (o_eixo == "Zertical") eixo_int = 2;
        }

        if (usa_teclas_custom == false)
        {
            if (Input.GetAxis(eixo) > 0) posicao[eixo_int] += valor_final * acelerador;
            else if (Input.GetAxis(eixo) < 0) posicao[eixo_int] -= valor_final * acelerador;
        }
        else
        {
            if (Input.GetButton(tecla_menos)) posicao[eixo_int] -= valor_final * acelerador;
            else if (Input.GetButton(tecla_mais)) posicao[eixo_int] += valor_final * acelerador;
        }

        return posicao;
    }

    int QualTempoEVisto()
    {
        float pos_tempo_float = (GetComponent<GuiTempo>().GetPosicaoInicialDaCamera() - GetComponent<Camera>().transform.position.y);
        pos_tempo_float /= 20;
        pos_tempo_float +=  GetComponent<NovoLeitor2>().GetPrimeiroTempo();
        if (GetComponent<Controlador>().modo_de_visualizacao == "Um Frame De Cada Vez em 2D")
        {
            pos_tempo_float += 0.25f;
        }
        int pos_tempo = (int)pos_tempo_float;
        if (pos_tempo < 0) pos_tempo = 0;
        return pos_tempo;
    }

    int QualTempoEVisto(float coordenada)
    {
        float pos_tempo_float = coordenada;
        pos_tempo_float /= 20;
        pos_tempo_float += GetComponent<NovoLeitor2>().GetPrimeiroTempo();
        if (GetComponent<Controlador>().modo_de_visualizacao == "Um Frame De Cada Vez em 2D")
        {
            pos_tempo_float += 0.25f;
        }
        int pos_tempo = (int)pos_tempo_float;
        if (pos_tempo < 0) pos_tempo = 0;
        return pos_tempo;
    }

    public void MudarTransparenciaDosObjetos(float change)
    {

        MudancaDeCor(true, false, false, false, change);

    }

    private void LigarOuDesligarObjetos(bool ligar, bool tipo, string qual_tipo, bool tempo, int tempo_minimo, int tempo_maximo)
    {
        int limit = GetComponent<NovoLeitor2>().Lista_de_objetos.Count;
        lista_de_objetos_a_ligar_ou_desligar = GetComponent<NovoLeitor2>().Lista_de_objetos;

        for (int i = 0; i < limit; i++)
        {
            // Explicação sobre o código abaixo:
            // modificar é a variável que indica no fim das comparações se o objeto dentro do for é aquele
            // o qual se quer ligar ou desligar.
            // A linha modificar && ... está ali para simultaneamente permitir que modificar se mantenha como true
            // a cada condição passada e que em caso de false em uma dessas condições, o false também se propague e não
            // seja sobreescrito por próximos trues.
            bool modificar = true;
            if (tipo)
            {
                modificar = (modificar &&
                            ((GameObject)lista_de_objetos_a_ligar_ou_desligar[i]).GetComponent<Dados>().nome_do_objeto == qual_tipo);
            }
            if (tempo)
            {
                int qual_tempo = ((GameObject)lista_de_objetos_a_ligar_ou_desligar[i]).GetComponent<Dados>().tempo;
                modificar = (modificar &&
                            ((tempo_minimo <= qual_tempo) && (qual_tempo <= tempo_maximo)));
            }

            if (modificar)
            {
                if (ligar) ((GameObject)lista_de_objetos_a_ligar_ou_desligar[i]).GetComponent<LigaDesliga>().Ligar();
                else ((GameObject)lista_de_objetos_a_ligar_ou_desligar[i]).GetComponent<LigaDesliga>().Desligar();
            }
        }
    }

    private void LigarOuDesligarObjetosConsiderandoSobreposicao
                 (bool ligar, bool tipo, string qual_tipo, bool tempo, int tempo_minimo, int tempo_maximo)
    {
        int limit = GetComponent<NovoLeitor2>().Lista_de_objetos.Count;
        int qual_tempo;

        // variáveis usadas para evitar objetos ativos ignorando desligamento por tempo/tipo, quando ambos estão sendo
        // usados e um deles é desfeito.
        int t_minimo; int t_maximo;
        int posicao_de_checagem_de_tipo_de_objeto;

        lista_de_objetos_a_ligar_ou_desligar = GetComponent<NovoLeitor2>().Lista_de_objetos;

        for (int i = 0; i < limit; i++)
        {
            // Explicação sobre o código abaixo:
            // modificar é a variável que indica no fim das comparações se o objeto dentro do for é aquele
            // o qual se quer ligar ou desligar.
            // A linha modificar && ... está ali para simultaneamente permitir que modificar se mantenha como true
            // a cada condição passada e que em caso de false em uma dessas condições, o false também se propague e não
            // seja sobreescrito por próximos trues.
            bool modificar = true;
            if (tipo)
            {
                modificar = (modificar &&
                            ((GameObject)lista_de_objetos_a_ligar_ou_desligar[i]).GetComponent<Dados>().nome_do_objeto == qual_tipo);
            }
            if (tempo)
            {
                qual_tempo = ((GameObject)lista_de_objetos_a_ligar_ou_desligar[i]).GetComponent<Dados>().tempo;
                modificar = (modificar &&
                            ((tempo_minimo <= qual_tempo) && (qual_tempo <= tempo_maximo)));
            }

            if (modificar)
            {
                if (ligar)
                {
                    if (tipo && !GetComponent<GuiVisualizarPorTempo>().GetTrueFalsePorVariavelVisivelOuInvisivel())
                    {
                        qual_tempo = ((GameObject)lista_de_objetos_a_ligar_ou_desligar[i]).GetComponent<Dados>().tempo;
                        t_minimo = Int32.Parse(GetComponent<GuiVisualizarPorTempo>().GetTempoMinimo());
                        t_maximo = Int32.Parse(GetComponent<GuiVisualizarPorTempo>().GetTempoMaximo());
                        modificar = modificar && !((t_minimo <= qual_tempo) && (qual_tempo <= t_maximo));
                    }
                    if (tempo && GetComponent<GuiVisualizarTipos>().AlgumTipoDeObjetoInvisivel())
                    {
                        // ou faco isso ou o compilador acusa erro de não declaração de variável.
                        posicao_de_checagem_de_tipo_de_objeto = 0;

                        // Lembrando: this serve para fazer referência à classe em que o código se encontra.
                        // Uso aqui porquê a variável interna da função é idêntica à variável da classe.
                        // Seria legal se alguém deixasse as duas variáveis com nomes diferentes depois...
                        if (this.tipo == "Fit")
                        {
                            posicao_de_checagem_de_tipo_de_objeto = GetComponent<NovoLeitor2>().
                                           nomes_e_numeros_de_objetos_do_FIT[
                                            ((GameObject)lista_de_objetos_a_ligar_ou_desligar[i]).GetComponent<Dados>().nome_do_objeto
                                            ];
                        }

                        else if (this.tipo == "Bolhas")
                        {
                            posicao_de_checagem_de_tipo_de_objeto = GetComponent<NovoLeitor2>().
                                           nomes_e_numeros_de_objetos_do_bolhas[
                                            ((GameObject)lista_de_objetos_a_ligar_ou_desligar[i]).GetComponent<Dados>().nome_do_objeto
                                            ];
                        }

                        if (GetComponent<GuiVisualizarTipos>().EstaInvisivel(posicao_de_checagem_de_tipo_de_objeto))
                        {
                            modificar = false;
                        }

                    }
                    if (modificar) ((GameObject)lista_de_objetos_a_ligar_ou_desligar[i]).GetComponent<LigaDesliga>().Ligar();
                }
                else ((GameObject)lista_de_objetos_a_ligar_ou_desligar[i]).GetComponent<LigaDesliga>().Desligar();
            }
        }
    }

    void MudarTransparenciaDeTipoEspecificoDeObjetos(string nome, float transparencia)
    {

        int limit = GetComponent<NovoLeitor2>().Lista_de_objetos.Count;

        for (int i = 0; i < limit; i++)
        {
            Color cor;
            if (((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).GetComponent<Dados>().nome_do_objeto == nome)
            {
                cor = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).
                            GetComponent<MeshRenderer>().material.GetColor("_Color");

                cor.a = transparencia;

                ((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).
                            GetComponent<MeshRenderer>().material.SetColor("_Color", cor);
            }
        }
    }

    void MudarTransparenciaDeObjetosDeEspacoDeTempoEspecifico(int tempo_minimo, int tempo_maximo, float transparencia)
    {

        int limit = GetComponent<NovoLeitor2>().Lista_de_objetos.Count;

        for (int i = 0; i < limit; i++)
        {
            Color cor;
            int tempo = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).GetComponent<Dados>().tempo;
            if ((tempo_minimo <= tempo) && (tempo <= tempo_maximo))
            {
                cor = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).
                            GetComponent<MeshRenderer>().material.GetColor("_Color");

                cor.a = transparencia;

                ((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).
                            GetComponent<MeshRenderer>().material.SetColor("_Color", cor);
            }
        }
    }

    void SetInteracaoObjetosDeEspacoDeTempo(int tempo_minimo, int tempo_maximo, bool e_interagivel)
    {
        int limit = GetComponent<NovoLeitor2>().Lista_de_objetos.Count;

        for (int i = 0; i < limit; i++)
        {
            int tempo = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).GetComponent<Dados>().tempo;
            if ((tempo_minimo <= tempo) && (tempo <= tempo_maximo))
            {
                if (e_interagivel) ((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).layer = 0;
                else ((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).layer = 2;
            }
        }
    }

    void SetInteracaoComTiposDeObjetos(string nome, bool e_interagivel)
    {
        int limit = GetComponent<NovoLeitor2>().Lista_de_objetos.Count;

        for (int i = 0; i < limit; i++)
        {
            string nome_do_objeto = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).GetComponent<Dados>().nome_do_objeto;
            if (nome_do_objeto == nome)
            {
                if (e_interagivel) ((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).layer = 0;
                else ((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).layer = 2;
            }
        }
    }

    public void DeixarTipoDeObjetoInvisivelEIninteragivel(string nome)
    {
        LigarOuDesligarObjetosConsiderandoSobreposicao(false, true, nome, false, 0, 0);
    }

    public void DeixarTipoDeObjetoVisivelEInteragivel(string nome)
    {
        LigarOuDesligarObjetosConsiderandoSobreposicao(true, true, nome, false, 0, 0);
    }

    public void DeixarObjetosEmEspacoDeTempoInvisiveisEIninteragiveis(int tempo_minimo, int tempo_maximo)
    {
        LigarOuDesligarObjetosConsiderandoSobreposicao(false, false, null, true, tempo_minimo, tempo_maximo);
    }

    public void DeixarObjetosEmEspacoDeTempoVisiveisEInteragiveis(int tempo_minimo, int tempo_maximo)
    {
        LigarOuDesligarObjetosConsiderandoSobreposicao(true, false, null, true, tempo_minimo, tempo_maximo);
    }

    void TransparenciaDoBackground(float trans)
    {
        int count = GetComponent<NovoLeitor2>().Lista_de_backgrounds.Count;
        Color cor = ((GameObject)GetComponent<NovoLeitor2>().
            Lista_de_backgrounds[count - 1]).
                GetComponent<MeshRenderer>().material.GetColor("_Color");

        cor.a = trans;
        ((GameObject)GetComponent<NovoLeitor2>().Lista_de_backgrounds[count - 1]).
            GetComponent<MeshRenderer>().material.SetColor("_Color", cor);
    }

    public void PontoFoiClicado(Transform click)
    {
        if (objeto_clicado == null)
        {
            objeto_clicado = click;
            click.GetComponent<MeshRenderer>().material.mainTexture = GetComponent<NovoLeitor2>().
                texturas_selecionadas.Get(click.GetComponent<Dados>().nome_do_objeto.ToString());
        }
        else if (objeto_clicado != click)
        {
            objeto_clicado.GetComponent<MeshRenderer>().material.mainTexture = GetComponent<NovoLeitor2>().
                texturas.Get(objeto_clicado.GetComponent<Dados>().nome_do_objeto.ToString());
            objeto_clicado = click;
            click.GetComponent<MeshRenderer>().material.mainTexture = GetComponent<NovoLeitor2>().
                texturas_selecionadas.Get(click.GetComponent<Dados>().nome_do_objeto.ToString());
        }

    }

    public void NenhumPontoFoiClicado()
    {
        if (objeto_clicado != null)
        {
            objeto_clicado.GetComponent<MeshRenderer>().material.mainTexture = GetComponent<NovoLeitor2>().
                    texturas.Get(objeto_clicado.GetComponent<Dados>().nome_do_objeto.ToString());
            objeto_clicado = null;
        }
    }

    private void MudancaDeCor(bool a, bool r, bool g, bool b, float change)
    {
        for (int i = 0; i < GetComponent<NovoLeitor2>().Lista_de_objetos.Count; i++)
        {
            Color cor = ((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).GetComponent<MeshRenderer>().
                material.GetColor("_Color");

            if (a) cor.a += change;
            if (r) cor.r += change;
            if (g) cor.g += change;
            if (b) cor.b += change;

            ((GameObject)GetComponent<NovoLeitor2>().Lista_de_objetos[i]).GetComponent<MeshRenderer>().
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

    public void AlteracaoDePosicionamentoDeJogadores()
    {
        bool[] ativos = GetComponent<NovoLeitor2>().GetQuaisJogadoresEstaoAtivos();
        
        if (modo_de_visualizacao == "Um Frame De Cada Vez em 3D")
        {

            if (ativos[0] && !ativos[1])
            {
                GetComponent<NovoLeitor2>().qual_jogador = 0;
                //GetComponent<NovoLeitor2>().Ancora.SetActive(true);
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesCentroUmDeCadaVez3D();
                //GetComponent<NovoLeitor2>().Ancora.SetActive(false);
                GetComponent<NovoLeitor2>().qual_jogador = 1;
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesDireitaUmDeCadaVez3D();
            } else if (ativos[0] && ativos[1])
            {
                GetComponent<NovoLeitor2>().qual_jogador = 0;
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesEsquerdaUmDeCadaVez3D();
                GetComponent<NovoLeitor2>().qual_jogador = 1;
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesDireitaUmDeCadaVez3D();
            } else if (!ativos[0] && ativos[1])
            {
                GetComponent<NovoLeitor2>().qual_jogador = 0;
                GetComponent<NovoLeitor2>().Ancora.SetActive(true);
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesEsquerdaUmDeCadaVez3D();
                GetComponent<NovoLeitor2>().Ancora.SetActive(false);
                GetComponent<NovoLeitor2>().qual_jogador = 1;
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesCentroUmDeCadaVez3D();
            }

        } else if (modo_de_visualizacao == "Um Frame De Cada Vez em 2D")
        {

            if (ativos[0] && !ativos[1])
            {
                GetComponent<NovoLeitor2>().qual_jogador = 0;
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesCentroUmDeCadaVez2D();
            }
            else if (ativos[0] && ativos[1])
            {
                GetComponent<NovoLeitor2>().qual_jogador = 0;
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesEsquerdaUmDeCadaVez2D();
                GetComponent<NovoLeitor2>().qual_jogador = 1;
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesDireitaUmDeCadaVez2D();
            }
            else if (!ativos[0] && ativos[1]){
                GetComponent<NovoLeitor2>().qual_jogador = 1;
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesCentroUmDeCadaVez2D();
            }

        } else if (modo_de_visualizacao == "Todos De Uma Vez em 3D")
        {

            if (ativos[0] && !ativos[1])
            {
                GetComponent<NovoLeitor2>().qual_jogador = 0;
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesCentroTodosDeUmaVez3D();
            }
            else if (ativos[0] && ativos[1])
            {
                GetComponent<NovoLeitor2>().qual_jogador = 0;
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesEsquerdaTodosDeUmaVez3D();
                GetComponent<NovoLeitor2>().qual_jogador = 1;
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesDireitaTodosDeUmaVez3D();
            }
            else if (!ativos[0] && ativos[1]){
                GetComponent<NovoLeitor2>().qual_jogador = 1;
                GetComponent<NovoLeitor2>().ReposicionandoPosicoesCentroTodosDeUmaVez3D();
            }

        } else if (modo_de_visualizacao == "Heatmap")
        {

            if (ativos[0] && !ativos[1])
            {
                GetComponent<NovoLeitor2>().qual_jogador = 0;
                GetComponent<NovoLeitor2>().ReposicionandoHeatmapCentro();
            }
            else if (ativos[0] && ativos[1])
            {
                GetComponent<NovoLeitor2>().qual_jogador = 0;
                GetComponent<NovoLeitor2>().ReposicionandoHeatmapEsquerda();
                GetComponent<NovoLeitor2>().qual_jogador = 1;
                GetComponent<NovoLeitor2>().ReposicionandoHeatmapDireita();
            }
            else if (!ativos[0] && ativos[1])
            {
                GetComponent<NovoLeitor2>().qual_jogador = 1;
                GetComponent<NovoLeitor2>().ReposicionandoHeatmapCentro();
            }

        }
    }

    public bool GetAutoMode()
    {
        return auto_mode;
    }

    public bool GetAutoModeCustom()
    {
        return auto_mode_custom;
    }

    public BancoDeDadosModos GetBancoDeDadosModos()
    {
        return modos;
    }

    public void InicializacaoBolhas()
    {

        tipo = "Bolhas";

        parte_da_transparencia_dos_objetos += "8 - Diminui detalhes \n" +
                                             "9 - Aumenta detalhes \n";

        Inicializacao();
    }

    public void InicializacaoFIT()
    {
        tipo = "Fit";

        parte_da_transparencia_dos_objetos = "";

        Inicializacao();
    }

    public void Inicializacao()
    {

        modos.Add("Um Frame De Cada Vez em 3D", 0.5f, 5f, 30f, 0f, 0f, false, 1f, new Vector3(0f, 0f, 330f),
            new Vector3(90f, 0f, 0f), 1f, 2, -8.0f, 0.0f, 12.0f,
            new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f),
            new Vector3(0, 0f, 30f), new Vector3(0f, 0f, 330f), parte_da_transparencia_dos_objetos +
                                             "<- - Câmera recua\n" +
                                             "-> - Câmera avança(pode \n" +
                                             "atravessar grids)\n" +
                                             "A - Câmera recua mais rápido\n" +
                                             "D - Câmera avança mais rápido\n" +
                                             "Q - Voltar à tela inicial");

        modos.Add("Um Frame De Cada Vez em 2D", 2f, 0f, 10f, 0f, 0f, true, 1f, new Vector3(0f, 0f, 0f),
            new Vector3(90f, 0f, 0f), 1f, 0, -4f, 0.0f, 4f,
            new Vector3(0.5f, 1f, 0.5f), new Vector3(1f, 1f, 1f),
            new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), parte_da_transparencia_dos_objetos +
                                             "<- - Posição anterior\n" +
                                             "-> - Posição seguinte\n" +
                                             "A - Anterior mais rápido\n" +
                                             "D - Seguinte mais rápido\n" +
                                             "E - Posição escolhida\n" +
                                             "em 'Pular Para Posição'\n" +
                                             "R - Liga/Desliga\n" +
                                             "automático\n" +
                                             "T - Liga/Desliga\n" +
                                             "automático customizado\n" +
                                             "Q - Voltar à tela inicial");

        modos.Add("Todos De Uma Vez em 3D", 0.5f, 5f, 2f, 0f, 0f, false, 0f, new Vector3(0f, 0f, 0f),
            new Vector3(90f, 0f, 0f), 0f, 2, -12.0f, 0.0f, 12.0f,
            new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f),
            new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), parte_da_transparencia_dos_objetos +
                                             "<- - Câmera recua\n" +
                                             "-> - Câmera avança\n" +
                                             "A - Recua mais rápido\n" +
                                             "D - Avança mais rápido\n" +
                                             "R - Reseta posição e rotação\n" +
                                             "S - Reseta rotação\n" +
                                             "Q - Tela inicial");

        modos.Add("Heatmap", 0f, 200f, 15f, 0f, 0f, true, 0f, new Vector3(0f, 0f, 0f),
            new Vector3(90f, 0f, 0f), 1f, 2, -4f, 0f, 4f,
            new Vector3(0.67f, 1f, 0.5f), new Vector3(1.333333f, 1f, 1f),
            new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), "<- - Avança na lista de heatmaps\n" +
                                             "-> - Retorna na lista de heatmaps\n" +
                                             "Q - Tela inicial");

        for (int i = 0; i < GetComponent<NovoLeitor2>().objs_jogadores_fit.QuantosJogadores(); i++)
        {
            GetComponent<NovoLeitor2>().qual_jogador = i;

            TransparenciaDoBackground(1f);
            GetComponent<NovoLeitor2>().ConectarTodos();
            
        }

        Mudanca_De_Modo_De_Visualizacao("Um Frame De Cada Vez em 3D", true);

        for (int i = 0; i < GetComponent<NovoLeitor2>().objs_jogadores_fit.QuantosJogadores(); i++)
        {
            GetComponent<NovoLeitor2>().qual_jogador = i;
            GetComponent<NovoLeitor2>().PosicionarBackgrounds(20f);
            GetComponent<NovoLeitor2>().DesconectarTodos();
        }

        GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;

        GetComponent<GuiModo>().MudarInstrucoes(instrucoes_genericas + modos.GetInstrucao(modo_de_visualizacao));
        GetComponent<GuiModo>().RevelarGui();

        AlteracaoDePosicionamentoDeJogadores();

        GameObject background = (GameObject)(GetComponent<NovoLeitor2>().Lista_de_backgrounds[0]);

        AtualizarValoresDePosicionamento(background.transform.position.y);

        //gambiarra a ser corrigida posteriormente
        Vector3 pos = FindObjectOfType<Camera>().transform.position;
        pos.y = modos.GetCameraInitY(modo_de_visualizacao);

        FindObjectOfType<Camera>().transform.position = pos;

        usuario_pode_fazer_input = true;

    }
}