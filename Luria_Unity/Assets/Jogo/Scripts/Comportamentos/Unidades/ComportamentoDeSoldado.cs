using UnityEngine;
using System.Collections;

public class ComportamentoDeSoldado : MonoBehaviour
{
	public bool alvoInimigo { get; set; }
	public GameObject alvoHabilidade { get; set; }
	public Vector3 alvo { get; set; }

	/*Caracteristicas*/
	public NivelSoldado nivel;
	public FaccaoSoldado faccao;
	public PapelSoldado papel;
	public Sprite foto = null;
	public string nome = "";

	/*Status*/
	public int indiceNaUnidade = 0;
	public Atributos atributos { get; private set; }
	public Hashtable efeitos { get; private set; }
	public Habilidade[] habilidades = new Habilidade[3];
	public int habilidadeAtual { get; private set; }
	public float esperaProximoAtaque = 0;

	/*Variaveis Auxiliares*/
	public Animator animator { get; private set; }
	public NavMeshAgent navMeshAgent { get; private set; }
	private float delayDesaparecerMorto = 3;
	private float delayRegeneracaoDeVida = 0;
	private float delayRegeneracaoDeEnergia = 0;
	public bool recuando = false;
	
	public void Start()
	{
		//Referenciando componentes
		animator = gameObject.GetComponent<Animator>();
		navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

		//Inicio os atributos do personagem
		atributos = BuildAtributos.atributosDe(nivel, faccao, papel);
		atributos.soldado = this;
		efeitos = new Hashtable();

		//configurar navmesh
		navMeshAgent.destination = gameObject.transform.position;
		navMeshAgent.speed = atributos.velocidadeDeMovimento;
        navMeshAgent.radius = 0.5F;
		navMeshAgent.angularSpeed = 300;
		navMeshAgent.acceleration = 20;
		navMeshAgent.stoppingDistance = 1.5F;

		//Carregar Habilidades
		habilidades = gameObject.GetComponents<Habilidade>();
	}
	
	public void Update()
	{
		//Se o soldado estiver morto
		if(atributos.vida <= 0)
		{
			//Mas ainda nao foi dado como morto
			if(animator.GetBool("vivo"))
			{//Mato ele
				//se não for uma tropa aliada
				if (gameObject.GetComponentInParent<ComportamentoDeUnidade>().tipoTropa != 1)
				{
					//dropo algum item, caso ele possa
					GameObject drop = CarregarItens.droparItem(nome);
					if (drop != null)
					{
						drop = Instantiate(drop);
						drop.transform.position = gameObject.transform.position;
						drop.transform.position += new Vector3(0, gameObject.transform.position.y + 1.5F, 0);
					}
				}

				//Coloco a animaçao de morto
				animator.SetBool("vivo", false);
				//Pego o pai dele
				GameObject pai = gameObject.transform.parent.gameObject;
				//Removo ele da unidade
				gameObject.transform.SetParent(gameObject.transform.parent.parent);
				//digo para o pai procurar um novo lider
				pai.GetComponent<ComportamentoDeUnidade>().redefinirLider();
				//desativo o navMesh
				navMeshAgent.radius = 0.1F;
				//removo a tag de soldado
				gameObject.tag = "SoldadoMorto";
			}//Se ja tiver morto, apenas verifico se ja deu tempo de apodrecer
			else if((delayDesaparecerMorto -= Time.deltaTime) <= 0)
				Destroy(gameObject);
		}
		else
		{
			//espera para atacar novamente
			if(esperaProximoAtaque > 0)
				esperaProximoAtaque -= Time.deltaTime;

			//se eu estiver recuando, vou apenas recuar
			if(recuando)
			{//ate chegar ao meu destino
				if(navMeshAgent.remainingDistance <= 2)
					recuando = false;
			}//Se nao estiver recuando, o soldado esta atacando algum inimigo?
			else if(alvoHabilidade != null)
			{
				//Se ele nao tiver na distancia minima movo ele para mais perto
				if(Vector3.Distance(gameObject.transform.position, alvoHabilidade.transform.position) > atributos.distanciaDeAtaque)
				{
					//Digo para o soldado ir em direçao ao inimigo
					navMeshAgent.destination = alvoHabilidade.transform.position;
				}
				else
				{
					//Rotaciono o soldado para o inimigo
					Vector3 posicaoAlvo = alvo - gameObject.transform.position;
					Vector3 novaDirecao = Vector3.RotateTowards(gameObject.transform.position, posicaoAlvo, 12, 0.0F);
					gameObject.transform.rotation = Quaternion.LookRotation(novaDirecao);
					gameObject.transform.rotation.Set(0, gameObject.transform.rotation.y, 0, 0);
					//Digo para ele ficar parado
					animator.SetBool("correndo", false);
					//Digo para ele ficar onde esta
					navMeshAgent.destination = gameObject.transform.position;
					//e eu ja posso atacar?
					if (esperaProximoAtaque <= 0)
						usarHabilidade();
                }

				//Verifico se ele ainda esta vivo... Se nao estiver, peço outro alvo aa unidade
				if (alvoHabilidade == null || alvoHabilidade.GetComponent<ComportamentoDeSoldado>().atributos.vida <= 0)
				{
					alvoHabilidade = null;
					definirHabilidade(0);
				}
			}

			//Se ele estiver vivo e o estado estiver andando, mas a distancia minima para o destino ja foi alcançada, faça-o parar
			if(navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
				animator.SetBool("correndo", true);
			else if(animator.GetBool("correndo"))
				animator.SetBool("correndo", false);
		}
	}

	//Regeneraçao de energia e vida
	void LateUpdate ()
	{
		if (atributos.vida > 0)
		{//a energia pode ser regenerada em combate

			if (alvoHabilidade == null)
			{//se eu nao estiver em combate, posso recuperar minha vida e energia
				//Minha vida nao esta cheia?
				if (atributos.vida < atributos.vidaTotal)
				{
					//Eu ja posso recuperar vida?
					if (delayRegeneracaoDeVida >= 1)
					{
						atributos.receberCura(atributos.regeneracaoDeVida);
						delayRegeneracaoDeVida = 0;
					}
					else //incremento o deley de regeneraçao de vida
						delayRegeneracaoDeVida += Time.deltaTime;
				}
			}

			//Minha energia nao esta cheia?
			if (atributos.energia < atributos.energiaTotal)
			{
				//Eu ja posso recuperar energia?
				if (delayRegeneracaoDeEnergia >= 1)
				{
					atributos.regenerarEnergia(atributos.regeneracaoDeEnergia);
					delayRegeneracaoDeEnergia = 0;
				}
				else //incremento o deley de regeneraçao de energia
					delayRegeneracaoDeEnergia += Time.deltaTime;
			}

			foreach (Efeito efeito in efeitos.Values)
			{
				if(efeito.efeitoPeriodico)
					efeito.ativacaoPeriodica();
				if (efeito.cargas <= 0)
					efeitos.Remove(efeito.nome);
			}
		}
	}

	public void definirHabilidade(int indiceHabilidade)
	{
		//se o jogador selecionou uma habilidade que não pode utilizar, seto a habilidade basica
		if (habilidades[indiceHabilidade].recarga > 0 ||
			atributos.energia < habilidades[indiceHabilidade].energia)
			indiceHabilidade = 0;

		//se a habilidade que o jogador selecionou for uma habilidade já selecionada, não reseto o alvo
		if(indiceHabilidade != habilidadeAtual || alvoHabilidade == null)
			habilidades[indiceHabilidade].selecionarAlvo(this);

		habilidadeAtual = indiceHabilidade;
	}
	
	public void usarHabilidade()
	{
		if (alvoHabilidade != null)
		{
			animator.Play("Atacando");
			//redefino quem o soldado ira atacar
			if (alvo != alvoHabilidade.transform.position)
				alvo = alvoHabilidade.transform.position;
			//Depois que o jogador utilizar a habilidade, volto ela para o auto ataque
			habilidades[habilidadeAtual].ativar(gameObject);
			definirHabilidade(0);
			//Reseto a espera para atacar
			esperaProximoAtaque = atributos.velocidadeDeAtaque;
		}
	}

	public void adicionarEfeito(Efeito novoEfeito)
	{
		novoEfeito.alvo = this;
		if (efeitos.ContainsKey(novoEfeito.nome))
			efeitos.Remove(novoEfeito.nome);
		efeitos.Add(novoEfeito.nome, novoEfeito);
	}
	
	public float getVidaAtual()
	{
		if(atributos == null)
			return 0;
		return atributos.vida/((float) atributos.vidaTotal);
	}
	
	public float getEnergiaAtual()
	{ return atributos.energia/((float) atributos.energiaTotal); }
}
