using UnityEngine;
using System.Collections;

public class Atributos
{
	public ComportamentoDeSoldado soldado { get; set; }
	public int vidaTotal { get; private set; }
	public int energiaTotal { get; private set; }

	public int vida { get; set; }
	public int energia { get; set; }
	public float velocidadeDeAtaque { get; private set; }
	public int regeneracaoDeVida { get; private set; }
	public int regeneracaoDeEnergia { get; private set; }
	
	public int forca { get; private set; }
	public int agilidade { get; private set; }
	public int habilidade { get; private set; }
	public int resistencia { get; private set; }
	public int armadura { get; private set; }
	public int escudo { get; set; }
	private float redDano;
	public float distanciaDeAtaque { get; private set; } //distancia > 3, o dano e calculado com habilidade
	public float velocidadeDeMovimento { get; private set; }

	public Atributos()
	{
		forca = 10;
		agilidade = 10;
		habilidade = 10;
		resistencia = 10;
		armadura = 3;
		distanciaDeAtaque = 2;
		velocidadeDeMovimento = 7;
		calcularAtributosSecundarios();
	}

	public Atributos(int forca, int agilidade, int habilidade, int resistencia, int armadura, float distanciaDeAtaque, float velocidadeDeMovimento)
	{
		this.forca = forca;
		this.agilidade = agilidade;
		this.habilidade = habilidade;
		this.resistencia = resistencia;
		this.armadura = armadura;
		this.distanciaDeAtaque = distanciaDeAtaque;
		this.velocidadeDeMovimento = velocidadeDeMovimento;
		calcularAtributosSecundarios();
    }

	private void calcularAtributosSecundarios()
	{
		redDano = (armadura + ((forca + habilidade) / 10) + (resistencia / 3));
		vida = vidaTotal = (resistencia * 15) + (75 - resistencia);
		energia = energiaTotal = (habilidade * 5) + (resistencia * 2);
		velocidadeDeAtaque = 1 + (15 / agilidade);
		regeneracaoDeVida = (int)((resistencia + habilidade) / 8) + 1;
		regeneracaoDeEnergia = (int)((resistencia + habilidade * 3) / 12) + 1;
		escudo = 0;
	}

	public void receberDano(int danoInimigo)
	{
		int dano = 0;

		//se o soldado tem escudo
		if (escudo > 0)
		{
			if (escudo > danoInimigo)
				escudo -= danoInimigo;
			else
			{
				danoInimigo -= escudo;
				escudo = 0;
			}
		}
		else
			dano = danoInimigo - (int) (redDano * taxaCritico());

		dano = Mathf.Clamp(dano, 0, 500);
		//se o dano for positivo
		if (dano > 0)
		{//o alvo e atingido
			vida -= dano;
			if(!soldado.animator.GetCurrentAnimatorStateInfo(0).IsName("Atacando"))
				soldado.animator.Play("Atingido");
		}
		else if (!soldado.animator.GetCurrentAnimatorStateInfo(0).IsName("Atacando"))
			soldado.animator.Play("Defendendo");
	}

	public void receberCura(int cura)
	{
		vida += cura;
		if (vida > vidaTotal)
			vida = vidaTotal;
	}

	public void regenerarEnergia(int cura)
	{
		energia += cura;
		if (energia > energiaTotal)
			energia = energiaTotal;
	}

	public int atributoBase()
	{
		if(distanciaDeAtaque > 3)
			return habilidade;
		return forca;
	}

	public float taxaCritico()
	{
		return 1 + (Random.Range(0, agilidade/3)/10);
	}
}
