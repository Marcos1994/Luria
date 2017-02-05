using UnityEngine;
using System.Collections;

public class ComportamentoItem : MonoBehaviour
{
	public string nome;
	public string descricao;
	public Sprite icone;
	public bool utilizavel;
	public int quantidade = 1;
	public int taxaDeDrop = 10; //1 a 20

	public bool coletado { get; set; }
	private int sentido = -1;
	private float limite = 1;
	
	void Start ()
	{
		coletado = false;
		gameObject.tag = "Item";
		gameObject.AddComponent<Rigidbody>();
		gameObject.GetComponent<Rigidbody>().useGravity = false;
		gameObject.GetComponent<Rigidbody>().isKinematic = true;
		gameObject.AddComponent<SphereCollider>().isTrigger = true;
	}

	public Item Item()
	{
		return new Item(nome, descricao, icone, utilizavel, quantidade);
	}
	
	void Update ()
	{
		float t = Time.deltaTime * sentido;
		limite -= t;
        gameObject.transform.position += new Vector3(0, t * 0.5F, 0);
		gameObject.transform.Rotate(0, 1, 0);

		if (coletado)
		{
			if (limite < 0)
				Destroy(gameObject);
		}
		else
		{
			if (limite > 2)
				sentido = 1;
			else if (limite < 1)
				sentido = -1;
		}
    }

	public void coletar()
	{
		if(!coletado)
		{
			coletado = true;
			sentido = 1;

			if (utilizavel)
			{
				GameObject unidade = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeJogo>().objetoSelecionado;
				if (unidade != null)
				{
					if (nome == "Quite Medico")
					{
						foreach (Transform soldadoTransform in unidade.transform)
							if (soldadoTransform.tag == "Soldado")
								soldadoTransform.GetComponent<ComportamentoDeSoldado>().atributos.receberCura(quantidade);
					}
					else if (nome == "Carregador de Energia")
					{
						foreach (Transform soldadoTransform in unidade.transform)
							if (soldadoTransform.tag == "Soldado")
								soldadoTransform.GetComponent<ComportamentoDeSoldado>().atributos.regenerarEnergia(quantidade);
					}
				}
			}
			else
				GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDeInventario>().adicionarItem(Item());
        }
	}
}
