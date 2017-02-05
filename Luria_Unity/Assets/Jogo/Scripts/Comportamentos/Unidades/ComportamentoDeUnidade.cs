using UnityEngine;
using System.Collections;

public class ComportamentoDeUnidade : MonoBehaviour
{
	private GameObject areaDeAtaque = null;	//Area de ataque da unidade
	public GameObject alvoAtual = null;		//Alvo que os soldados estao defendendo/indo/atacando
	public GameObject lider = null;			//Lider atual do grupo
	public GameObject inimigo = null;		//Inimigo atual da unidade
	public int tipoTropa = 1;               //Tipo da tropa: 1 = aliado; 2 = inimigo;
	public int rotacao = 0;

	private Vector3[] formacao = new Vector3[5];//Array de posiçao dos soldados na formaçao
	private float velocidadeMinima = 100;       //Velocidade minima da unidade

	public void Start()
	{
		//Defino um lider para o grupo
		if(gameObject.transform.childCount > 1)
			redefinirLider();

		//Pegar Area De Ataque
		areaDeAtaque = gameObject.transform.FindChild("AreaDeAtaque").gameObject;

		//Gerar as posiçoes relativas para a formaçao
		formacao[0] = new Vector3(0,0,0);	//Central
		formacao[1] = new Vector3(-2,0,-2);	//Inferior Esquerdo
		formacao[2] = new Vector3(2,0,-2);	//Inferior Direito
		formacao[3] = new Vector3(2,0,2);	//Superior Direito
		formacao[4] = new Vector3(-2,0,2);	//Superior Esquerdo
	}

	public void Update()
	{
		//A Area de Ataque tera como centro o lider da unidade
		if(lider != null)
			areaDeAtaque.transform.position = lider.transform.position;
	}

	public void selecionarSoldados()
	{
		//Pego a referencia para o controlador de jogo
		Transform mapaTransform = gameObject.transform.parent.parent;
		//Digo que essa unidade esta selecionada
		mapaTransform.GetComponent<ControladorDeJogo>().setObjetoSelecionado(gameObject);
	}

	public void moverSoldados(GameObject destino)
	{
		int i;
		//Atualizo o destino do grupo
		alvoAtual = destino;
		//Verifico qual eh o soldado mais lento para pegar a velocidade dele
		velocidadeMinima = 100;
		foreach (Transform soldadoTransform in gameObject.transform)
			if (soldadoTransform.tag == "Soldado" && soldadoTransform.GetComponent<ComportamentoDeSoldado>().atributos.velocidadeDeMovimento < velocidadeMinima)
				velocidadeMinima = soldadoTransform.GetComponent<ComportamentoDeSoldado>().atributos.velocidadeDeMovimento;
		//Percorro todos os soldados
		foreach (Transform soldadoTransform in gameObject.transform)
		{
			if (soldadoTransform.tag == "Soldado")
			{
				////Se houver algum inimigo, digo que estou recuando
				//soldadoTransform.GetComponent<ComportamentoDeSoldado>().recuando = (inimigo != null);
				//Digo que ele esta andando
				soldadoTransform.GetComponent<ComportamentoDeSoldado>().animator.SetBool("correndo", true);
				//Todos os soldados devem andar na mesma velocidade
				soldadoTransform.GetComponent<NavMeshAgent>().speed = velocidadeMinima;
				//calculo a posicao na formacao
				i = soldadoTransform.GetComponent<ComportamentoDeSoldado>().indiceNaUnidade;
				if (i > 0) i = (rotacao < i) ? i - rotacao : 4 + i - rotacao;
				//Seto o novo destino para o NavMeshAgent
				soldadoTransform.GetComponent<NavMeshAgent>().destination = destino.transform.position + formacao[i];
				//Seto a distancia minima para 1
				soldadoTransform.GetComponent<NavMeshAgent>().stoppingDistance = 1F;
			}
		}
	}

	public void moverLider(GameObject destino)
	{
		if (inimigo == null)
		{
			//Digo que ele esta andando
			lider.GetComponent<ComportamentoDeSoldado>().animator.SetBool("correndo", true);
			//Todos os soldados devem andar na mesma velocidade
			lider.GetComponent<NavMeshAgent>().speed = lider.GetComponent<ComportamentoDeSoldado>().atributos.velocidadeDeMovimento;
			//Seto o novo destino para o NavMeshAgent
			lider.GetComponent<NavMeshAgent>().destination = destino.transform.position;
			//Seto a distancia minima para 1
			lider.GetComponent<NavMeshAgent>().stoppingDistance = 1F;
		}
	}

	public void definirInimigoParaUnidade(GameObject novoInimigo)
	{
		//pego o novo inimigo
		inimigo = novoInimigo;

		//Percorro todos os soldados
		foreach(Transform soldadoTransform in gameObject.transform)
		{
			//se for um soldado, defino pra ele o inimigo mais proximo
			if(soldadoTransform.tag == "Soldado")
			{
				soldadoTransform.GetComponent<ComportamentoDeSoldado>().definirHabilidade(0);
			}
		}
	}
	
	public void redefinirLider()
	{
		//se tiver pelo menos 1 soldado (vai ter pelo menos 1 filho, que e a area de ataque)
		if(gameObject.transform.childCount > 1)
		{
			//Percorro todos os soldados
			foreach(Transform soldadoTransform in gameObject.transform)
			{
				if(soldadoTransform.tag == "Soldado")
				{
					//O primeiro que aparecer sera o lider do grupo
					lider = soldadoTransform.gameObject;
					break;
				}
			}
		}
		else
		{//Se todos os soldados ja estiverem mortos
			//digo para o destino que nao havera mais nenhum soldado la
			if(alvoAtual != null)
				alvoAtual.GetComponent<AcoesJogadorAlvo>().limparTropa(tipoTropa);
			//destruo a unidade
			Destroy(gameObject);
		}
	}
}
