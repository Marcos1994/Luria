using UnityEngine;
using System.Collections;

public class OcultarTeto : MonoBehaviour
{
	private bool emEspera;
	private Navegacao navegacao;

	void Start()
	{
		emEspera = false;
		navegacao = GameObject.Find("Hud").GetComponent<Navegacao>();
	}

	void Update()
	{
		if (emEspera)
		{
			if (navegacao.alvo.tag == "AlvoPortao" && navegacao.alvo.gameObject.GetComponent<AcoesJogadorAlvoPortao>().aberto)
			{
				gameObject.GetComponent<MeshRenderer>().enabled = false;
				emEspera = true;
			}
		}
	}

	public void OnTriggerEnter(Collider colisor)
	{
		//Se o alvo da camera ficar abaixo do teto, oculte o teto
		if (colisor.transform.name == "AlvoCamera")
		{
			if (navegacao.alvo != null &&
				navegacao.alvo.tag == "AlvoPortao" &&
				!navegacao.alvo.gameObject.GetComponent<AcoesJogadorAlvoPortao>().aberto)
				emEspera = true;
			else
				gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}

	public void OnTriggerExit(Collider colisor)
	{
		//Se o alvo da camera sair de baixo do teto, desoculte o teto
		if (colisor.transform.name == "AlvoCamera")
		{
			gameObject.GetComponent<MeshRenderer>().enabled = true;
			emEspera = false;
		}
	}
}
