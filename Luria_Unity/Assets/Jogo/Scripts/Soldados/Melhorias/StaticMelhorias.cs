using UnityEngine;
using System.Collections;

public static class StaticMelhorias
{
	private static Hashtable faccoes;
	private static Hashtable papeis;
	
	static StaticMelhorias()
	{
		faccoes = new Hashtable();
		papeis = new Hashtable();

		//Instanciar as melhorias aqui
		//Cada raiz de melhoria deverá ter o nome ("raizMelhorias" + [PapelSoldado/FaccaoSoldado])



		//Inicializar os Hashtables aqui

		/*Melhorias de Facções*/
		//faccoes.Add(FaccaoSoldado.BruxoDaFloresta, raizMelhorias);
		//faccoes.Add(FaccaoSoldado.Cangadar, raizMelhoriasCangadar);
		//faccoes.Add(FaccaoSoldado.CavaleiroDoTempo, raizMelhoriasCavaleiroDoTempo);
		//faccoes.Add(FaccaoSoldado.Diktar, raizMelhoriasDiktar);
		//faccoes.Add(FaccaoSoldado.Mutante, raizMelhoriasMutante);
		//faccoes.Add(FaccaoSoldado.Rebelde, raizMelhoriasRebelde);
		//faccoes.Add(FaccaoSoldado.Selvagem, raizMelhoriasSelvagem);
		//faccoes.Add(FaccaoSoldado.Wonkata, raizMelhoriasWonkata);
		//faccoes.Add(FaccaoSoldado.SemFaccao, raizMelhoriasSemFaccao);
		/*Melhorias de Papeis*/
		//papeis.Add(PapelSoldado.Asassino, raizMelhoriasAssassino);
		//papeis.Add(PapelSoldado.Atirador, raizMelhoriasAtirador);
		//papeis.Add(PapelSoldado.Curandeiro, raizMelhoriasCurandeiro);
		//papeis.Add(PapelSoldado.Guerreiro, raizMelhoriasGuerreiro);
		//papeis.Add(PapelSoldado.Infantaria, raizMelhoriasInfantaria);
		//papeis.Add(PapelSoldado.Suporte, raizMelhoriasSuporte);
		//papeis.Add(PapelSoldado.Neutro, raizMelhoriasNeutro);



		//Adicionar as instancias das melhorias aqui

	}

	public static ArrayList listarMelhoriasDe(FaccaoSoldado faccao)
	{
		return ((Melhoria)faccoes[faccao]).pegarRamo();
	}

	public static ArrayList listarMelhoriasDe(PapelSoldado papel)
	{
		return ((Melhoria)papeis[papel]).pegarRamo();
	}
}
