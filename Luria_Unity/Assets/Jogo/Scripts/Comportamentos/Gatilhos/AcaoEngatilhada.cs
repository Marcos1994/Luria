using UnityEngine;
using System.Collections;

public abstract class AcaoEngatilhada : MonoBehaviour
{
	public GameObject[] objetosAlvo = new GameObject[1];
	public string mensagem = "Ação realizada com sucesso!";

	protected abstract void acaoGatilho();

	public void ativarGatilho()
	{
		acaoGatilho();
		this.enabled = false;
		if(mensagem != "")
			GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeJogo>().escreverMensagem(mensagem);
	}
}
