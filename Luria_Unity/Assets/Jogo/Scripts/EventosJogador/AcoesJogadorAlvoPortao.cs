using UnityEngine;
using System.Collections;

public class AcoesJogadorAlvoPortao : AcoesJogadorAlvo
{
	public bool trancado = false;
	//portao1 <- \/ ; portao2 -> /\
	public GameObject[] portoes = new GameObject[2];
	//0 e 1 = Indo para Norte/Leste; 0 e 2 = posicoes da unidade; 1 e 3 = posicoes do pivo
	public GameObject[] alvosAuxiliares = new GameObject[4];

	public bool horizontal;
	public bool aberto { get; private set; }
	private bool abrindo;
	private float distancia;

	void Start()
	{
		aberto = false;
		abrindo = false;
		distancia = 0;

		gameObject.GetComponent<MeshRenderer>().enabled = false;
		alvosAuxiliares[0].GetComponent<MeshRenderer>().enabled = false;
		alvosAuxiliares[2].GetComponent<MeshRenderer>().enabled = false;
		if (horizontal)
		{
			alvosAuxiliares[0].transform.position -= new Vector3(0, 0, 1.5F);
			alvosAuxiliares[2].transform.position += new Vector3(0, 0, 1.5F);
		}
		else
		{
			alvosAuxiliares[0].transform.position -= new Vector3(1.5F, 0, 0);
			alvosAuxiliares[2].transform.position += new Vector3(1.5F, 0, 0);
		}
		alvosAuxiliares[1].AddComponent<AbrirPortao>();
		alvosAuxiliares[3].AddComponent<AbrirPortao>();
	}

	void Update()
	{
		if (abrindo)
		{
			if (distancia < 3)
			{
				float d = Time.deltaTime * 2F;
                distancia = Mathf.Clamp(distancia + d, 0, 3);
				if (horizontal)
				{
					portoes[0].transform.position -= new Vector3(d, 0, 0);
					portoes[1].transform.position += new Vector3(d, 0, 0);
				}
				else
				{
					portoes[0].transform.position -= new Vector3(0, 0, d);
					portoes[1].transform.position += new Vector3(0, 0, d);
				}
			}
			else
			{
				abrindo = false;
				aberto = true;
				for (int i = 0; i < 4; i++)
					Destroy(alvosAuxiliares[i]);

				if (tropaJogador != null)
				{
					GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeJogo>().hud.GetComponent<Navegacao>().setAlvo(gameObject);
					tropaJogador.GetComponent<ComportamentoDeUnidade>().moverSoldados(gameObject);
                }
			}
		}
	}

	public void abrir()
	{
		abrindo = true;
		alvosAuxiliares[1].GetComponent<SphereCollider>().enabled = false;
		alvosAuxiliares[1].GetComponent<MeshRenderer>().enabled = false;
		alvosAuxiliares[3].GetComponent<SphereCollider>().enabled = false;
		alvosAuxiliares[3].GetComponent<MeshRenderer>().enabled = false;
	}

	public GameObject alvoAuxiliarUnidade(int direcao)
	{
		//se estiver indo para Norte ou Leste
		if (direcao > 2)
			return alvosAuxiliares[2];
		//se estiver indo para Sul ou Oeste
		return alvosAuxiliares[0];
	}

	public GameObject alvoAuxiliarLider(int direcao)
	{
		//se estiver indo para Norte ou Leste
		if (direcao > 2)
			return alvosAuxiliares[3];
		//se estiver indo para Sul ou Oeste
		return alvosAuxiliares[1];
	}
}
