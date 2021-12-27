using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusDeSoldado : MonoBehaviour
{
	//soldado o qual esse script esta representando
	public GameObject soldado = null;
	private ComportamentoDeSoldado comportamentoDeSoldado = null;

	//Gameobjects Filhos
	private Image fotoSoldado = null;
	private Image vidaSoldado = null;
	private Image energiaSoldado = null;
	private Image acaoA = null;
	private Image acaoB = null;
	private bool ativado = true;

	void Start()
	{
		//pego os GameObjects do hud
		fotoSoldado = gameObject.transform.Find("Foto").gameObject.GetComponent<Image>();
		vidaSoldado = gameObject.transform.Find("Vida").gameObject.GetComponent<Image>();
		energiaSoldado = gameObject.transform.Find("Energia").gameObject.GetComponent<Image>();
		acaoA = gameObject.transform.Find("Acao_a").GetChild(0).GetComponent<Image>();
		acaoB = gameObject.transform.Find("Acao_b").GetChild(0).GetComponent<Image>();
	}

	public void atualizarSoldado(GameObject novoSoldado)
	{
		//defino o novo soldado
		soldado = novoSoldado;
		//pego o script de comportamento dele
		comportamentoDeSoldado = soldado.GetComponent<ComportamentoDeSoldado>();
		//defino a nova foto
		fotoSoldado.sprite = comportamentoDeSoldado.foto;
		//definir habilidades
		acaoA.sprite = comportamentoDeSoldado.habilidades[1].icone;
		acaoB.sprite = comportamentoDeSoldado.habilidades[2].icone;
	}

	void LateUpdate ()
	{
		if (soldado != null)
		{
			if (!ativado)
				ativar();
			vidaSoldado.fillAmount = comportamentoDeSoldado.getVidaAtual();
			energiaSoldado.fillAmount = comportamentoDeSoldado.getEnergiaAtual();
			acaoA.fillAmount = comportamentoDeSoldado.habilidades[1].porcentagemRecarga();
			acaoB.fillAmount = comportamentoDeSoldado.habilidades[2].porcentagemRecarga();
			if (comportamentoDeSoldado.atributos.vida <= 0)
			{
				soldado = null;
				comportamentoDeSoldado = null;
				fotoSoldado.sprite = null;
				acaoA.sprite = null;
				acaoB.sprite = null;
			}
		}
		else if (ativado)
			ativar(false);

	}

	public void ativar(bool ativado = true)
	{
		this.ativado = ativado;
		if (ativado)
			gameObject.transform.position = new Vector2(gameObject.transform.position.x - 100, gameObject.transform.position.y);
		else
			gameObject.transform.position = new Vector2(gameObject.transform.position.x + 100, gameObject.transform.position.y);
	}

	public void UtilizarAcao(int acao)
	{
		comportamentoDeSoldado.definirHabilidade(acao);
    }
}
