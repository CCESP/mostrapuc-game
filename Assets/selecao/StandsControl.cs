using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;

// esse objeto é o scene controller da parte selecao do jogo
public class StandsControl : MonoBehaviour {

	// atribuído no editor; DialogoModalStand
	public GameObject nextScene;

    public GameObject macaco;

    private Hashtable dadosEmpresas = new Hashtable();

    private const int NUM_EMPRESAS = 12;

    void parseRefEmpresa(string data)
    {
        var jsonData = JSON.Parse(data);
        for(int i = 0; i < jsonData.Count; i++)
        {
            var vaga = jsonData[i];
            Hashtable infoVaga = new Hashtable();
            infoVaga["nome"] = vaga["empresa"].Value.ToUpper();
            infoVaga["logo"] = vaga["type"].Value.ToUpper();
            infoVaga["texto"] = "";

            infoVaga["texto"] += "\n<b>Empresa:</b> " + vaga["empresa"].Value;

            infoVaga["texto"] += "\n<b>Tipo:</b> " + vaga["type"].Value;

            infoVaga["texto"] += "\n<b>Número de vagas:</b> " + vaga["job_openings"].Value;

            infoVaga["texto"] += "\n<b>Horário:</b> " + vaga["office_hours"].Value;

            infoVaga["texto"] += "\n<b>Remuneração:</b> R$ " + vaga["remuneration"].Value;

            infoVaga["texto"] += "\n<b>Cursos:</b>\n";

            foreach(var curso in vaga["cursos"])
            {
                infoVaga["texto"] += curso.Value + "\n";
            }

            infoVaga["texto"] += "<b>Período:</b> Entre " + vaga["periodo_beg"].Value + "º e o " + vaga["periodo_end"].Value + "º";

            infoVaga["texto"] += "\n<b>Sexo:</b> " + vaga["gender"].Value;

            infoVaga["texto"] += "\n<b>Local da atividade</b>\n<b>Cidade:</b> " + vaga["city"].Value + "\n<b>Bairro:</b> " + vaga["neighbourhood"].Value;

            infoVaga["texto"] += "\n<b>Atividades:</b>\n" + vaga["activities"].Value;

            infoVaga["texto"] += "\n<b>Benefícios:</b>\n" + vaga["benefits"].Value;

            infoVaga["texto"] += "\n<b>Pré-requisitos:\n</b> " + vaga["requirements"].Value.ToString().Trim();

            infoVaga["texto"] += "\n<b>Procedimentos para se inscrever na vaga:</b>\n" + vaga["procedures"].Value;

            dadosEmpresas[i + ""] = infoVaga;
        }
    }

    IEnumerator fetchData()
    {
        string url = "http://www.ccesp.puc-rio.br/vagasonline-tools/vagas-rng";
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null || www.error == "")
        {
            // se há menos de N empresas, busca as N últimas através de outro acesso
            if (JSON.Parse(www.text).Count < NUM_EMPRESAS)
            {
                url = "http://www.ccesp.puc-rio.br/vagasonline-tools/vagas-last";
                www = new WWW(url);
                yield return www;
            }
            parseRefEmpresa(www.text);
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
    }

	void Start(){

        StartCoroutine(fetchData());

        // cria referencia para os dados das empresas a partir dos IDs
        //criarRefEmpresas();

		nextScene.SetActive(false);
        macaco.SetActive(false);

        // atribui um id a cada stand
        popularStands();
    }

    private void popularStands()
    {
        Transform wrapStands = transform.Find("StandsColisao");

        for (int i = 0; i < wrapStands.childCount; i++)
        {
            wrapStands.GetChild(i).GetComponent<TriggerDialog>().nomeStand = i + "";
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
        //Image goLogoEmpresa = transform.Find("DialogoModalStand/Canvas/selecaoStandLogoEmpresa").GetComponent<Image>();
        Text goLogoEmpresa = transform.Find("DialogoModalStand/Canvas/selecaoStandLogoEmpresa").GetComponent<Text>();

        Hashtable dados = ((Hashtable) dadosEmpresas[id]);
        
        goTextoEmpresa.text = dados["texto"].ToString();
        goNomeEmpresa.text = dados["nome"].ToString();
        //goLogoEmpresa.sprite = Resources.LoadAll<Sprite>((string)dados["logo"])[0] as Sprite;
        goLogoEmpresa.text = dados["logo"].ToString();
    }
}
