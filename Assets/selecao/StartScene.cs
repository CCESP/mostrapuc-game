using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// esse objeto é o scene controller da parte selecao do jogo
public class StartScene : MonoBehaviour {

	// atribuído no editor; DialogoModalStand
	public GameObject nextScene;

    public GameObject macaco;

    private Hashtable dadosEmpresas = new Hashtable(); 

	void Start(){
        // cria referencia para os dados das empresas a partir dos IDs
        criarRefEmpresas();

		nextScene.SetActive(false);
        macaco.SetActive(false);

        // atribui um id a cada stand
        popularStands();
    }

    private void popularStands()
    {
        Transform wrapStands = transform.Find("StandsColisao");

        ArrayList keyList = new ArrayList(dadosEmpresas.Keys);

        for (int i = 0; i < wrapStands.childCount; i++)
        {
            string idx = (string) keyList[i];
            wrapStands.GetChild(i).GetComponent<TriggerDialog>().nomeStand = idx;
        }
            
    }

    private void criarRefEmpresas()
    {
        string[] nomeEmpresas = {"adidas",
                                "ccesp",
                                "costarica",
                                "ford",
                                "ibm",
                                "mcdonalds",
                                "microsoft",
                                "partners",
                                "target",
                                "tata",
                                "yellowoak",
                                "puc"};

        int maxEmpresas = 12;
        ArrayList listaEmpresas = new ArrayList(nomeEmpresas);

        while (dadosEmpresas.Count < maxEmpresas) {
            int popIdx = Random.Range(0, listaEmpresas.Count - 1);
            Hashtable infoEmpresa = new Hashtable();
            string idxEmpresa = (string) listaEmpresas[popIdx];
            listaEmpresas.RemoveAt(popIdx);
            infoEmpresa["texto"] = "DADO TESTE: " + idxEmpresa + @"	
Responsável por estabelecer, com instituições públicas e privadas, convênios e termos de compromissos para estágios, a Coordenação Central de Estágios e Serviços Profissionais – CCESP – intermedia as relações de estágio curricular e extracurricular dos alunos da PUC-Rio.

A CCESP mantém uma rica base de dados das empresas, das instituições conveniadas e dos alunos, o que inclui um banco de currículos. Assim, propicia aos estudantes da PUC-Rio uma maior, mais ágil e atualizada interface com o mercado de trabalho, sendo este site uma das formas de conexão entre a Universidade e o setor produtivo.

Além de intermediar os Estágios e Programas de Trainee, a CCESP atua como promotora de serviços voluntários a partir do UNICOM e é a realizadora da Mostra PUC. Dispõe também, sob demanda, de cursos preparatórios para processos seletivos e serviços de desenvolvimento de carreira.";
            infoEmpresa["logo"] = "imgs/logos/logo" + idxEmpresa;
            infoEmpresa["nome"] = idxEmpresa.ToUpper();
            dadosEmpresas[idxEmpresa] = infoEmpresa;
        }
    }

    public void ExibeStand (string id)
    {
        Text goTextoEmpresa = transform.Find("DialogoModalStand/Canvas/ScrollTextControll/ScrollBackGround/selecaoStandTextoEmpresa").GetComponent<Text>();
        Text goNomeEmpresa = transform.Find("DialogoModalStand/Canvas/selecaoStandNomeEmpresa").GetComponent<Text>();
        Image goLogoEmpresa = transform.Find("DialogoModalStand/Canvas/selecaoStandLogoEmpresa").GetComponent<Image>();

        Hashtable dados = ((Hashtable) dadosEmpresas[id]);
        
        goTextoEmpresa.text = (string) dados["texto"];
        goNomeEmpresa.text = (string) dados["nome"];
        goLogoEmpresa.sprite = Resources.LoadAll<Sprite>((string)dados["logo"])[0] as Sprite;
    }
}
