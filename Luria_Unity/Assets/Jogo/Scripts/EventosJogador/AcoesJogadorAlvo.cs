using UnityEngine;
using System.Collections;

public class AcoesJogadorAlvo : MonoBehaviour
{
	public GameObject tropaJogador = null;	//Unidade do jogador que esta no alvo
	public GameObject tropaInimiga = null;  //Unidade inimiga que esta no alvo

	public GameObject alvoNorte;
	public GameObject alvoSul;
	public GameObject alvoLeste;
	public GameObject alvoOeste;
	public GameObject alvoEspecial;

	void Start()
	{
		gameObject.GetComponent<MeshRenderer>().enabled = false;
	}

	public void limparTropa(int tipoTropa)
	{
		if(tipoTropa == 1)
			tropaJogador = null;
		else
			tropaInimiga = null;
	}
}
