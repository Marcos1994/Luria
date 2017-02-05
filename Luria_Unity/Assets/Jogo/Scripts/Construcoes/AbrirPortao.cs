using UnityEngine;
using System.Collections;

public class AbrirPortao : MonoBehaviour
{
	public bool abrivel;
	public float tempoParaAbrir;
	private bool abrindo;

	void Start ()
	{
		abrivel = !gameObject.transform.GetComponentInParent<AcoesJogadorAlvoPortao>().trancado;
		tempoParaAbrir = 2;
		//redefino o layer para que o alvo possa colidir com os soldados
		gameObject.layer = LayerMask.NameToLayer("Default");
		//seto um collider com trigger para que o alvo possa colidir com os soldados
		gameObject.AddComponent<SphereCollider>().isTrigger = true;
	}

	void Update()
	{
		if (abrindo)
		{
			tempoParaAbrir -= Time.deltaTime;
			gameObject.transform.Rotate(0, 0, Time.deltaTime * 50);
			if (tempoParaAbrir < 0)
			{
				abrindo = false;
				gameObject.transform.GetComponentInParent<AcoesJogadorAlvoPortao>().abrir();
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Soldado")
		{
			if (abrivel)
				abrindo = true;
			else
				GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeJogo>().escreverMensagem("Essa porta está trancada!");
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Soldado")
		{
			abrindo = false;
			tempoParaAbrir = 2;
			gameObject.transform.rotation.Set(0, 0, 0, 0);
        }
	}
}
