using UnityEngine;
using System.Collections;

public class AcoesJogadorAlvoEspecial : MonoBehaviour
{
	public float carregamento = 2;
	private float progressoCarregamento = 0;
	private GameObject soldado;

	void Start ()
	{
		gameObject.layer = LayerMask.NameToLayer("Default");
		gameObject.AddComponent<SphereCollider>().isTrigger = true;
	}
	
	void Update ()
	{
		if (soldado != null)
		{
			progressoCarregamento += Time.deltaTime;
			gameObject.transform.Rotate(0, 0, Time.deltaTime * 50);
			if (progressoCarregamento > carregamento)
			{
				AcaoEngatilhada acao = gameObject.GetComponent<AcaoEngatilhada>();
				if (acao != null)
					acao.ativarGatilho();

				gameObject.GetComponent<MeshRenderer>().enabled = false;
				this.enabled = false;

				ComportamentoDeUnidade unidade = soldado.transform.GetComponentInParent<ComportamentoDeUnidade>();
				unidade.moverLider(unidade.alvoAtual);

				ControladorDeJogo controladorDeJogo = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeJogo>();
				if (controladorDeJogo.objetoSelecionado == unidade.gameObject)
					controladorDeJogo.hud.gameObject.GetComponent<Navegacao>().setAlvo(unidade.alvoAtual);
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Soldado")
			soldado = collider.gameObject;
    }

	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Soldado")
		{
			progressoCarregamento = 0;
			soldado = null;
		}
	}
}
