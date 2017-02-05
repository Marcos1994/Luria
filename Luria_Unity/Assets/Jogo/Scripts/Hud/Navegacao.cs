using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Navegacao : MonoBehaviour
{
	public GameObject alvoCamera = null;

	public Sprite setaAzul = null;
	public Sprite setaVermelha = null;
	public Sprite alvoEspecial = null;

	public AcoesJogadorAlvo alvo { get; private set; }
	private GameObject unidadeSelecionada;

	//public Button norte, sul, leste, oeste, especial;
	public Button[] botoes = new Button[5];
	public Button itens;

	void Start()
	{
		for (int i = 0; i < 5; i++)
		{
			botoes[i].image.sprite = setaAzul;
			habilitarBotao(botoes[i]);
		}
		botoes[0].image.sprite = alvoEspecial;
	}

	void Update()
	{
		//Se tiver alguma unidade selecionada, siga-a
		if (unidadeSelecionada != null)
		{
			alvoCamera.transform.position = unidadeSelecionada.GetComponent<ComportamentoDeUnidade>().lider.transform.position;

			//se tiver algum item, exibo o botao de itens
			ativarColetaDeItens(unidadeSelecionada.GetComponent<ComportamentoDeUnidade>().inimigo == null &&
								unidadeSelecionada.GetComponentInChildren<AreaDeAtaque>().itens.Count > 0);
		}
	}

	public void setAlvo(GameObject alvoGO)
	{
		if (alvoGO == null)
			return;
		alvo = alvoGO.GetComponent<AcoesJogadorAlvo>();
		unidadeSelecionada = alvo.tropaJogador;

		habilitarBotao(botoes[0], alvo.alvoEspecial);
		habilitarBotao(botoes[1], alvo.alvoNorte);
		habilitarBotao(botoes[2], alvo.alvoLeste);
		habilitarBotao(botoes[3], alvo.alvoSul);
		habilitarBotao(botoes[4], alvo.alvoOeste);
	}

	private void habilitarBotao(Button botao, GameObject alvoRef = null)
	{
		if (alvoRef == null ||
			(alvoRef.tag == "AlvoEspecial" && !alvoRef.GetComponent<AcoesJogadorAlvoEspecial>().enabled))
		{
			botao.enabled = false;
			botao.image.enabled = false;
			return;
		}
		botao.enabled = true;
		botao.image.enabled = true;
		if (alvoRef.tag == "Alvo" || alvoRef.tag == "AlvoPortao")
			botao.image.sprite = (alvoRef.GetComponent<AcoesJogadorAlvo>().tropaInimiga == null) ? setaAzul : setaVermelha;
	}

	private GameObject selecionarDestino(int destino)
	{
		switch (destino)
		{
			case 1:
				return alvo.alvoNorte.gameObject;
			case 2:
				return alvo.alvoLeste.gameObject;
			case 3:
				return alvo.alvoSul.gameObject;
			case 4:
				return alvo.alvoOeste.gameObject;
		}
		return null;
	}

	public void irPara(int direcao)
	{
		if (unidadeSelecionada.GetComponent<ComportamentoDeUnidade>().inimigo == null)
		{
			GameObject alvoDestino = selecionarDestino(direcao);
			AcoesJogadorAlvo alvoDestinoScript = alvoDestino.GetComponent<AcoesJogadorAlvo>();
			//pego a unidade selecionada
			unidadeSelecionada = alvo.tropaJogador;
			//Se tiver algum objeto selecionado, mando a unidade selecionada se mover para o objeto clicado
			if (unidadeSelecionada != null)
			{//Se tiver alguma unidade selecionada
				if (alvoDestinoScript.tropaJogador == null)
				{//E se nao tiver nem uma unidade nesse alvo (unidade do jogador)
				 //Defino que tem uma unidade vindo pra ca
					alvoDestinoScript.tropaJogador = unidadeSelecionada;
					//limpo o alvo antigo
					alvo.tropaJogador = null;
					//defino a rotacao (se n for para um alvo especial) da formacao
					if (direcao > 0) unidadeSelecionada.GetComponent<ComportamentoDeUnidade>().rotacao = direcao - 1;
					//E digo que esse alvo e o novo destino dele
					unidadeSelecionada.GetComponent<ComportamentoDeUnidade>().alvoAtual = alvoDestino;
					if (alvoDestino.tag != "AlvoPortao" || alvoDestino.GetComponent<AcoesJogadorAlvoPortao>().aberto)
					{//não é um alvo de portão ou é um alvo de portão mas está aberto
					 //Mando a unidade se mover para o alvo atual
						unidadeSelecionada.GetComponent<ComportamentoDeUnidade>().moverSoldados(alvoDestino);
						//reseto o alvo atual da navegação
						setAlvo(alvoDestino);
					}
					else
					{//é um alvo de portão e está fechado
					 //Mando a unidade se mover para o alvo auxiliar de onde ele veio
						unidadeSelecionada.GetComponent<ComportamentoDeUnidade>().moverSoldados(alvoDestino.GetComponent<AcoesJogadorAlvoPortao>().alvoAuxiliarUnidade(direcao));
						//mando o lider da unidade ir para o alvo auxiliar p abrir o portão
						unidadeSelecionada.GetComponent<ComportamentoDeUnidade>().moverLider(alvoDestino.GetComponent<AcoesJogadorAlvoPortao>().alvoAuxiliarLider(direcao));

						//oculto todas as setas
						for (int i = 0; i < 5; i++)
							habilitarBotao(botoes[i]);
						//exibo apenas a seta de voltar
						int direcaoOposta = (direcao > 2) ? direcao - 2 : direcao + 2;
						habilitarBotao(botoes[direcaoOposta], alvo.gameObject);

						//reseto o alvo atual da navegação
						alvo = alvoDestinoScript;
						unidadeSelecionada = alvo.tropaJogador;
					}
				}
				else
					GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeJogo>().escreverMensagem("Nao pode haver dois grupos em um mesmo alvo");
			}
		}
	}

	public void irParaAlvoEspecial()
	{
		//pego a unidade selecionada
		GameObject unidadeSelecionada = alvo.tropaJogador;
		//Se tiver algum objeto selecionado, mando a unidade selecionada se mover para o objeto clicado
		if (unidadeSelecionada != null && alvo.alvoEspecial != null)
		{//Se tiver alguma unidade selecionada
		 //Mando a unidade se mover para o alvo atual
			unidadeSelecionada.GetComponent<ComportamentoDeUnidade>().moverLider(alvo.alvoEspecial);
		}
	}

	public void ativarColetaDeItens(bool ativar = true)
	{
		itens.enabled = ativar;
		itens.image.enabled = ativar;
    }

	public void coletarItens()
	{
		AreaDeAtaque area = unidadeSelecionada.GetComponentInChildren<AreaDeAtaque>();
		foreach (GameObject item in area.itens)
			item.GetComponent<ComportamentoItem>().coletar();
		area.itens = new ArrayList();
	}
}
