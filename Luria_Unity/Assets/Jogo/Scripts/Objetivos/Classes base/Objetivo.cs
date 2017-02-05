using System.Collections;

public class Objetivo
{
	public string titulo { get; set; }		//Titulo do objetivo
	public string descricao { get; set; }	//Descriçao do objetivo
	public bool obrigatorio { get; set; }	//Obrigatoriedade do objetivo
	public int estado { get; set; }			//0: em aberto; 1: sucesso; -1: falha;
	public int progressoAtual { get; set; }		//0: ja pode ser concluido
	public int progressoFinal { get; set; }		//0: ja pode ser concluido
	public bool possuiCinematic { get; set; }   //informa se o objetivo possui uma cinematic relacionada
	
	public Objetivo(string titulo, string descricao, bool obrigatorio = true, int progressoFinal = 0, bool possuiCinematic = false)
	{
		this.titulo = titulo;
		this.descricao = descricao;
		this.obrigatorio = obrigatorio;
		this.progressoAtual = 0;
		this.progressoFinal = progressoFinal;
		this.estado = 0;
		this.possuiCinematic = possuiCinematic;
	}
}
