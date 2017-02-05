using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImagemCutscene : MonoBehaviour
{
	public float velTransicao = 2;
	public float esperaCarregamento = 1;
	public int indiceBloco;
	//Cada bloco é definido pela centena e milhar do indice
	//A ordem das imagens nos blocos é definida pelo restante do indice

	public void desativar(float velocidadeSumir = 0)
	{
		gameObject.GetComponent<Image>().CrossFadeAlpha(0, velocidadeSumir, true);
	}

	public void ativar()
	{
		gameObject.GetComponent<Image>().CrossFadeAlpha(1, velTransicao, true);
	}
}
