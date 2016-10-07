using Basicas;
using UnityEngine;

/// <summary>
/// Responsável por usar as funções de NovoLeitor2 de forma a ler e usar os dados do log do FIT.
/// </summary>
public class NovoLeitorFIT : NovoLeitor2 {

    // Use this for initialization
    void Start()
    {
        NovoLeitor2InitFIT();

        string endereco;

#if UNITY_EDITOR

        endereco = Application.dataPath + "/Arquivos de Teste de Log";

#else

        endereco = System.IO.Directory.GetCurrentDirectory();

#endif

        PassadorDeDados pd = FindObjectOfType<PassadorDeDados>();

        pegar_endereco_de_log.endereco_de_arquivo[0] = pd.endereco_do_arquivo;

        StartFIT();
        
        for (int i = 0; i < pd.bd_fits.Count; i++)
        {
            qual_jogador = i;
            objs_jogadores_fit.Add(new ObjetosDeUmJogadorFIT());
            objs_jogadores_fit.GetObjetosDeUmJogadorFIT(qual_jogador).bd_fit = (BancoDeDadosFIT)(pd.bd_fits[qual_jogador]);
            Heatmaps = pd.heatmaps;
            InicializarHeatmaps();

            

        }

        for (int i = 0; i < pd.bd_fits.Count; i++)
        {
            qual_jogador = i;
            PreenchedorDeHeatmapsFIT();
            CreateStuffFIT();
        }


        qual_jogador = 0;

        GetComponent<Controlador>().InicializacaoFIT();
        
        pd.Destruir();

    }

}
