using UnityEngine;

public abstract class Habilidade : MonoBehaviour
{
	public Sprite icone;
	public string nome { get; set; }
	public int energia { get; set; }
	public float recargaTotal { get; set; }
	public float recarga { get; set; }
	public bool alvoInimigo { get; set; }

	void Start()
	{
		recarga = 0;
	}

	void Update()
	{
		if (recarga > 0)
		{
			recarga -= Time.deltaTime;
			if (recarga < 0) recarga = 0;
		}
	}

	//quando o jogador selecionar uma habilidade, a habilidade vai selecionar o possivel alvo
	public void selecionarAlvo(ComportamentoDeSoldado soldado)
	{
		GameObject soldadoPassivo;
        if (alvoInimigo)
		{
			ComportamentoDeUnidade unidade = soldado.gameObject.GetComponentInParent<ComportamentoDeUnidade>();
			if (unidade.inimigo != null && unidade.inimigo.transform.childCount > 1)
				soldadoPassivo = definirAlvo(soldado);
			else
			{
				//mando os soldados seguirem para o destino, caso tenham
				if (unidade.alvoAtual != null)
					unidade.moverSoldados(unidade.alvoAtual);
				else //Se nao, mando entrarem em formaçao para onde o lider ta
					unidade.moverSoldados(unidade.lider);
				return;
			}
		}
		else
		{
			soldadoPassivo = definirAlvo(soldado);
			if (soldadoPassivo == null)
				return;
		}
		soldado.alvoHabilidade = soldadoPassivo;
		soldado.alvo = soldadoPassivo.transform.position;
		soldado.navMeshAgent.destination = soldadoPassivo.transform.position;
		soldado.navMeshAgent.stoppingDistance = soldado.atributos.distanciaDeAtaque;
		soldado.alvoInimigo = alvoInimigo;
	}

	public float porcentagemRecarga()
	{
		return (recargaTotal - recarga) / recargaTotal;
	}
	
	protected abstract GameObject definirAlvo(ComportamentoDeSoldado soldado);

	//Com o gameobject do soldado, tenho acesso ao inimigo e à unidade à qual ele percente
	//Caso a habilidade gere algum efeito progressivo, deve ser instanciado dentro do Ativar
	public abstract void ativar(GameObject soldado);
}
