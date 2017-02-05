using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusDeUnidade : MonoBehaviour
{
	public int indice = 0;
	public ComportamentoDeUnidade unidade;
	public bool ativado { get; private set; }
	private GameObject unidades;
	private float delay = 1;

	void Start ()
	{
		ativado = true;
		unidades = GameObject.Find("Unidades").gameObject;
		if (indice >= unidades.transform.childCount)
			ativar(false);
		else
			unidade = unidades.transform.GetChild(indice).GetComponent<ComportamentoDeUnidade>();
	}
	
	void Update ()
	{
		if (ativado)
		{
			if (unidade == null)
				ativar(false);
			else if ((delay += Time.deltaTime) > 0.5F)
			{
				atualizarVidas();
				delay = 0;
			}
		}
	}

	public void ativar(bool ativado = true)
	{
		if (this.ativado != ativado)
		{
			this.ativado = ativado;
			if (ativado)
				gameObject.transform.position = new Vector2(gameObject.transform.position.x + 150, gameObject.transform.position.y);
			else
			{
				gameObject.transform.position = new Vector2(gameObject.transform.position.x - 150, gameObject.transform.position.y);
				unidade = null;
			}
		}
	}

	private void atualizarVidas()
	{
		int j = 0;
		for (int i = 0; i < unidade.transform.childCount; i++)
		{
			if (unidade.transform.GetChild(i).tag == "Soldado")
			{
				gameObject.transform.GetChild(j++).GetComponent<Image>().fillAmount = unidade.transform.GetChild(i).GetComponent<ComportamentoDeSoldado>().getVidaAtual();
			}
		}

		for (; j < 5; j++)
			gameObject.transform.GetChild(j).GetComponent<Image>().fillAmount = 0;
    }

	public void selecionarUnidade()
	{
		if (ativado)
			unidade.GetComponent<ComportamentoDeUnidade>().selecionarSoldados();
		else
			GameObject.Find("Hud").GetComponent<ControladorDeInterface>().escreverMensagem("Unidade Não Encontrada");
	}
}
