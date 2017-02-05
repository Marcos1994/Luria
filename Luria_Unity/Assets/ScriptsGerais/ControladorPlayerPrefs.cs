using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class ControladorPlayerPrefs
{
	//CampanhaAtual: String da campanha atual
	//Prologo, Deserto, Refugio, Capital, Norte, Resgate;

	//ProgressoCampanha: ID da fase e se esta bloqueada(0), desbloqueada(1) ou finalizada(2).
	//idDaFase:estado,idDaFase:estado

	//UnidadesDisponiveis: nome da unidade e quantidade disponivel; se quantidade for 0, a quantidade é ilimitada
	//Nome da unidade:quantidade, nome da unidade:quantidade

	//Inventario: inventario;
	//item:quantidade#nome do outro item:quantidade

	//int MissaoAtual;
	//int SucessoMissao;

	//Melhorias: Nomes das Melhorias se foram realizadas, estão ativas ou n foram realizadas;
	//?

	//Experiencia: PapelDoSoldado + Experiencia
	//?

	public static Hashtable inventario = new Hashtable();
	public static Missao[] missoes;

	public static Hashtable inventarioFase = new Hashtable();
	public static ArrayList unidadesFase = new ArrayList();
	public static Objetivo[] objetivosFase;

	static ControladorPlayerPrefs()
	{
		carregarInventario();
		PlayerPrefs.SetInt("SucessoMissao", 0);

		ArrayList[] teste = new ArrayList[1];
		teste[0] = new ArrayList();
		teste[0].Add("Alan Hoop:0");
		teste[0].Add("Arom Feahim:4");
		teste[0].Add("Iana Amara:2");
		setarUnidades(teste);

		Debug.Log("PlyerPrefs Carregado!");
	}


	/*-----------------------------------------------------------------------*/
	/*------------------------------ Progresso ------------------------------*/
	/*-----------------------------------------------------------------------*/

	public static void definirCampanha(string campanha)
	{
		PlayerPrefs.SetString("CampanhaAtual", campanha);

		switch(campanha)
		{
			case "Prologo":
				missoes = new Missao[1];
				missoes[0] = new Missao(0, "Projeto Logos", "Fase_0_0", true, null, 2);
				break;
			case "Deserto":
				missoes = new Missao[1];
				missoes[0] = new Missao(0, "Iniciação", "Fase_1_1", true, null, 1);
				break;
		}
	}

	public static void salvarProgresso()
	{
		string progresso = "";
		for (int i = 0; i < missoes.Length; i++)
			progresso += "," + missoes[i].id + ":" + missoes[i].estado;
		PlayerPrefs.SetString("ProgressoCampanha", progresso.Substring(1,progresso.Length-1));
	}

	public static void carregarProgresso()
	{
		definirCampanha(PlayerPrefs.GetString("CampanhaAtual"));
	}

	public static void concluirMissao(bool sucesso = true)
	{
		inventarioFase = new Hashtable();
		unidadesFase = new ArrayList();
		objetivosFase = null;

		//se a missão foi finalizada com sucesso...
		if(PlayerPrefs.GetInt("SucessoMissao") == 1)
		{//Se era uma missão final
			if(missoes[PlayerPrefs.GetInt("MissaoAtual")].final)
			{//passo para a proxima campanha
				switch(PlayerPrefs.GetString("CampanhaAtual"))
				{
					case "Prologo":
						SceneManager.LoadScene("IntroducaoDeserto");
						break;
					case "Deserto":
						SceneManager.LoadScene("IntroducaoRefugio");
						break;
					case "Refugio":
						SceneManager.LoadScene("IntroducaoCapital");
						break;
					case "Capital":
						SceneManager.LoadScene("IntroducaoNorte");
						break;
					case "Norte":
						SceneManager.LoadScene("IntroducaoResgate");
						break;
					case "Resgate":
						SceneManager.LoadScene("FinalJogo");
						break;
				}
			}
			else
			{//desbloqueio as missões e sigo para o menu principal
				missoes[PlayerPrefs.GetInt("MissaoAtual")].estado = 2;
				if(missoes[PlayerPrefs.GetInt("MissaoAtual")].desbloqueia != null)
				{
					int j = 0;
					for (int i = 0; i < missoes[PlayerPrefs.GetInt("MissaoAtual")].desbloqueia.Length; i++)
					{
						j = missoes[PlayerPrefs.GetInt("MissaoAtual")].desbloqueia[i];
						if(missoes[j].estado == 0)
							missoes[j].estado = 1;
					}
				}
				irParaMenu();
			}
		}
		else
			irParaMenu();

		PlayerPrefs.SetInt("SucessoMissao", 0);
		salvarProgresso();
	}

	public static void irParaMenu()
	{
		if(PlayerPrefs.GetString("CampanhaAtual") == "Prologo")
			SceneManager.LoadScene("MenuPrincipal");
		else
			SceneManager.LoadScene("MenuDeJogo" + PlayerPrefs.GetString("CampanhaAtual"));
	}



	/*-----------------------------------------------------------------------*/
	/*------------------------- UnidadesDisponiveis -------------------------*/
	/*-----------------------------------------------------------------------*/

	public static void setarUnidadesDisponiveis(string[] unidadesDisponiveis)
	{//no formato "nome do soldado:quantidade"
		string unidadesSerializadas = "";
		foreach(string unidade in unidadesDisponiveis)
			unidadesSerializadas += "," + unidade;
		unidadesSerializadas = unidadesSerializadas.Substring(1, unidadesSerializadas.Length-1);
		PlayerPrefs.SetString("UnidadesDisponiveis", unidadesSerializadas);
	}

	public static Hashtable listarUnidadesDisponiveis()
	{
		if (!PlayerPrefs.HasKey("UnidadesDisponiveis"))
			return null;

		Hashtable unidadesDisponiveis = new Hashtable();

		string[] unidadesSerializadas = PlayerPrefs.GetString("UnidadesDisponiveis").Split(',');
		string[] unidade;
		foreach(string unidadeSerializada in unidadesSerializadas)
		{
			unidade = unidadeSerializada.Split(':');
			if(unidade.Length == 2)
				unidadesDisponiveis.Add(unidade[0], unidade[1]);
		}

		return unidadesDisponiveis;
	}

	public static void atualizarQuantidadesUnidadeDisponivel(Hashtable unidadesDisponiveis)
	{
		
	}



	/*-----------------------------------------------------------------------*/
	/*--------------------------- UnidadesMissao ----------------------------*/
	/*-----------------------------------------------------------------------*/

	public static void setarUnidades(ArrayList[] unidades)
	{
		//Cada arraylist de unidade vai receber uma string no formato "nome do soldado:indiceNaUnidade"
		string unidadeSerializada = "";
		for (int i = 0; i < unidades.Length; i++)
		{
			unidadeSerializada += "#";
			for (int j = 0; j < unidades[i].Count; j++)
				unidadeSerializada += unidades[i][j].ToString() + ",";
			//pra tirar a ultima virgula
			unidadeSerializada = unidadeSerializada.Substring(0, unidadeSerializada.Length-1);
		}
		//pra tirar o primeiro jogo da velha
		PlayerPrefs.SetString("UnidadesMissao", unidadeSerializada.Substring(1, unidadeSerializada.Length-1));
	}

	public static void carregarUnidades(Transform[] posicoesUnidades)
	{
		if (PlayerPrefs.HasKey("UnidadesMissao"))
		{
			string[] unidadesSerializadas = PlayerPrefs.GetString("UnidadesMissao").Split('#');

			if (unidadesSerializadas.Length > 0)
			{
				int indiceUnidade = 0;

				foreach (string unidadeSerializada in unidadesSerializadas)
				{
					if(indiceUnidade < posicoesUnidades.Length)
						CarregarUnidades.instanciarUnidade(unidadeSerializada.Split(','), (Transform) posicoesUnidades[indiceUnidade++]);
				}
			}
		}
	}


	/*-----------------------------------------------------------------------*/
	/*----------------------------- Inventario ------------------------------*/
	/*-----------------------------------------------------------------------*/

	public static void resetarInventarioFase()
	{
		inventarioFase = new Hashtable();
	}

	public static void carregarInventario()
	{
		inventario = new Hashtable();
		string inventarioSerializado = PlayerPrefs.GetString("Inventario");
		
		if (inventarioSerializado.Length > 0)
		{
			//nome do item:quantidade
			string[] itensSerializados = inventarioSerializado.Substring(1, inventarioSerializado.Length-1).Split('#');
			string[] item;
			foreach (string itemSerializado in itensSerializados)
			{
				item = itemSerializado.Split(':');
				if (item.Length == 2)
				{
					inventario.Add(item[0], CarregarItens.carregarItem(item[0]));
					((Item)inventario[item[0]]).quantidade += int.Parse(item[1]);
				}
			}
		}
	}

	public static void adicionarAoInventario(Item item)
	{
		if (inventarioFase.ContainsKey(item.nome))
			((Item)inventarioFase[item.nome]).quantidade += item.quantidade;
		else
			inventarioFase.Add(item.nome, item);
	}

	public static void salvarInventario()
	{
		ArrayList novosItens = new ArrayList();
		foreach(string novoItem in inventarioFase.Keys)
			novosItens.Add(novoItem);

		foreach(string novoItem in novosItens)
		{
			if (inventario.ContainsKey(novoItem))
				((Item)inventario[novoItem]).quantidade += ((Item)inventarioFase[novoItem]).quantidade;
			else
				inventario.Add(novoItem, ((Item)inventarioFase[novoItem]));
		}

		//salvo o inventario total
		string inventarioSerializado = "";
		foreach (Item item in inventario.Values)
			inventarioSerializado += "#" + item.nome + ":" + item.quantidade;
		PlayerPrefs.SetString("Inventario", inventarioSerializado);
	}
}
