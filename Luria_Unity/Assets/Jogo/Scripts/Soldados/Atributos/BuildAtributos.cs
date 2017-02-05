using System.Collections;

public class BuildAtributos
{
	public static Atributos atributosDe(NivelSoldado nivel, FaccaoSoldado faccao, PapelSoldado papel)
	{
		int statusInicial = 5;

		int atributoBase = 0, forca = 0, agilidade = 0, habilidade = 0, resistencia = 0;

		int armadura = 0;
		float distanciaDeAtaque = 2.5F;
		float velocidadeDeMovimento = 7F; //5..10

		switch (faccao)
		{
			case FaccaoSoldado.Selvagem:
				agilidade += 2;
				resistencia += 1;
				velocidadeDeMovimento += 1;
				atributoBase += 2;
				break;
			case FaccaoSoldado.Diktar:
				armadura += 2;
				atributoBase += 3;
				break;
			case FaccaoSoldado.Rebelde:
				agilidade += 2;
				velocidadeDeMovimento += 1;
				atributoBase += 3;
				break;
			case FaccaoSoldado.Mutante:
				resistencia += 3;
				armadura += 1;
				atributoBase += 2;
				break;
			case FaccaoSoldado.Cangadar:
				resistencia += 4;
				atributoBase += 1;
				break;
			case FaccaoSoldado.CavaleiroDoTempo:
				agilidade += 4;
				velocidadeDeMovimento += 1;
				atributoBase += 1;
				break;
			case FaccaoSoldado.BruxoDaFloresta:
				habilidade += 5;
				break;
			case FaccaoSoldado.Wonkata:
				agilidade += 1;
				atributoBase += 4;
				break;
		}

		switch (nivel)
		{
			case NivelSoldado.General:
				statusInicial += 2;
				atributoBase += 3;
				armadura += 2;
                break;
			case NivelSoldado.Elite:
				statusInicial += 1;
				atributoBase += 3;
				armadura += 2;
				break;
			case NivelSoldado.Soldado:
				statusInicial += 1;
				atributoBase += 1;
				armadura += 1;
				break;
			case NivelSoldado.Basico:
				statusInicial += 1;
				atributoBase += 1;
				armadura += 0;
				break;
			//case NivelSoldado.Peao:
			//	break;
			case NivelSoldado.Desafio:
				statusInicial += 6;
				atributoBase += 7;
				armadura += 5;
				resistencia += 25;
				break;
		}

		switch (papel)
		{
			case PapelSoldado.Neutro:
				forca += 5 + atributoBase;
				agilidade += 5;
				resistencia += 5;
				armadura += 3;
				break;
			case PapelSoldado.Infantaria:
				forca += 3 + atributoBase;
				resistencia += 12;
				armadura += 5;
				velocidadeDeMovimento -= 1;
				break;
			case PapelSoldado.Suporte:
				habilidade += 6 + atributoBase;
				resistencia += 9;
				armadura += 5;
				velocidadeDeMovimento -= 2;
				distanciaDeAtaque += 3.5F;
				break;
			case PapelSoldado.Curandeiro:
				agilidade += 4;
				habilidade += 11 + atributoBase;
				armadura += 1;
				velocidadeDeMovimento += 2;
				distanciaDeAtaque += 2.5F;
				break;
			case PapelSoldado.Asassino:
				forca += 6 + atributoBase;
				agilidade += 9;
				armadura += 1;
				velocidadeDeMovimento += 1;
				break;
			case PapelSoldado.Guerreiro:
				forca += 10 + atributoBase;
				agilidade += 2;
				resistencia += 3;
				armadura += 3;
				break;
			case PapelSoldado.Atirador:
				agilidade += 5;
				habilidade += 10 + atributoBase;
				armadura += 1;
				velocidadeDeMovimento += 0;
				distanciaDeAtaque += 5.5F;
				break;
		}

		forca		+= statusInicial;
		agilidade	+= statusInicial;
		habilidade	+= statusInicial;
		resistencia	+= statusInicial;

		return new Atributos(forca, agilidade, habilidade, resistencia, armadura, distanciaDeAtaque, velocidadeDeMovimento);
	}
}

public enum FaccaoSoldado
{                       //+5*						+1    +7    +5*
	SemFaccao,          //for+0 agi+0 hab+0 res+0	arm+0 vel+0 ATB+0
	Selvagem,           //for+0 agi+2 hab+0 res+1	arm+0 vel+1 ATB+2
	Diktar,             //for+0 agi+0 hab+0 res+0	arm+2 vel+0 ATB+3
	Rebelde,            //for+0 agi+2 hab+0 res+0	arm+0 vel+1 ATB+3
	Mutante,            //for+0 agi+0 hab+0 res+3	arm+1 vel+0 ATB+2
	Cangadar,           //for+0 agi+0 hab+0 res+4	arm+0 vel+0 ATB+1
	CavaleiroDoTempo,   //for+0 agi+4 hab+0 res+0	arm+0 vel+1 ATB+1
	BruxoDaFloresta,    //for+0 agi+0 hab+5 res+0	arm+0 vel+0 ATB+0
	Wonkata             //for+0 agi+1 hab+0 res+0	arm+0 vel+0 ATB+4
}

public enum NivelSoldado
{               //AtrGe	ATB	Arm
	General,    //+2	+3	+2
	Elite,      //+1	+3	+2
	Soldado,    //+1	+1	+1
	Basico,     //+1	+1	+0
	Peao,		//+0	+0	+0
	Desafio		//+6	+7	+5	Res += 25
}

public enum PapelSoldado
{               //Atr(stsIni+15) Arm(0..6) Vel(7 - 5..10) Dist(2.5..8)
				//	for	agi	hab	res		arm	vel	dist
	Neutro,     //	+05	+05	+00	+05		+3	+0	+0.0	
	Infantaria, //	+03	+00	+00	+12		+5	-1	+0.0	Tanker Melee
	Suporte,    //	+00	+00	+06	+09		+5	-2	+3.5	Tanker Ranged
	Curandeiro, //	+00	+04	+11	+00		+1	+2	+2.5	Healer
	Asassino,   //	+06	+09	+00	+00		+1	+1	+0.0	DPS de Agilidade
	Guerreiro,  //	+10	+02	+00	+03		+3	+0	+0.0	DPS de Força
	Atirador,   //	+00	+05	+10	+00		+1	+0	+5.5	DPS de Longo Alcance

}
