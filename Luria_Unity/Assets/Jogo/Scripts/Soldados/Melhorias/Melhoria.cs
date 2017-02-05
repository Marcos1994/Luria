using UnityEngine;
using System.Collections;

/*/
 * Cada habilidade deverá ser implementada em uma classe filha de Melhoria
/*/
public abstract class Melhoria
{
	public string nome;
	public Sprite icone;

	public Hashtable preRequisito;

	public bool adiquirida { get; private set; }
	public bool ativada { get; private set; }

	public Hashtable atributos;
	public Hashtable habilidadePrimaria;
	public Hashtable habilidadeSecundaria;

	public Melhoria melhoriaPai;
	public ArrayList especializacoes;

	public Melhoria()
	{
		//icone = Resources.Load("melhoriaIco", typeof(Sprite)) as Sprite;
		atributos = new Hashtable();
		habilidadePrimaria = new Hashtable();
		habilidadeSecundaria = new Hashtable();
		especializacoes = new ArrayList();
	}

	public Melhoria(Melhoria melhoriaPai)
	{
		this.melhoriaPai = melhoriaPai;
		//icone = Resources.Load("melhoriaIco", typeof(Sprite)) as Sprite;
		atributos = new Hashtable();
		habilidadePrimaria = new Hashtable();
		habilidadeSecundaria = new Hashtable();
		especializacoes = new ArrayList();
	}

	public Melhoria(Melhoria melhoriaPai, Hashtable preRequisito)
	{
		this.melhoriaPai = melhoriaPai;
		this.preRequisito = preRequisito;
		//icone = Resources.Load("melhoriaIco", typeof(Sprite)) as Sprite;
		atributos = new Hashtable();
		habilidadePrimaria = new Hashtable();
		habilidadeSecundaria = new Hashtable();
		especializacoes = new ArrayList();
	}

	public Melhoria proximaMelhoria()
	{
		foreach(Melhoria proxima in especializacoes)
			if (proxima.ativada)
				return proxima;

		if(especializacoes.Count > 0)
			return (Melhoria) especializacoes[0];

		return null;
	}

	public ArrayList pegarRamo()
	{
		ArrayList melhorias = new ArrayList();
		Melhoria melhoria = this;

		do
		{
			melhorias.Add(melhoria);
			if (!melhoria.adiquirida)
				break;
			melhoria = melhoria.proximaMelhoria();
		}
		while (melhoria != null);

		return melhorias;
	}

	public void adquirirMelhoria()
	{
		if (preRequisito != null)
			preRequisito = null;

		if (melhoriaPai != null)
			foreach (Melhoria melhoriaIrma in melhoriaPai.especializacoes)
				melhoriaIrma.ativada = false;

		ativada = adiquirida = true;
	}

	public void ativarMelhoria()
	{
		if (melhoriaPai != null)
			foreach (Melhoria melhoriaIrma in melhoriaPai.especializacoes)
				melhoriaIrma.ativada = false;

		ativada = true;
	}
}
