using UnityEngine;
using System.Collections;


/// <summary>
/// Classe LidaComErros. Criada com base incompleta das classes para detectar erros no sistema, e uma vez encontrados,
/// avisar isso e gerar mensagens de erro adequadas para que apareçam na GUI.
/// <para>ATENÇÃO: ESTA CLASSE NÃO DEVE SER USADA. Seus filhos, claro, mas ela é incompleta e por si só, não basta
/// para poder lidar com erros.</para>
/// </summary>
public class LidaComErros : MonoBehaviour {

    public int posicao_da_mensagem_de_erro_y;
    public int quantidade_de_mudanca_de_posicao_y;

    public virtual void PossiveisMensagensDeErro() { }

    public virtual bool NaoTemosErrosDeInput() { return false; }

}
