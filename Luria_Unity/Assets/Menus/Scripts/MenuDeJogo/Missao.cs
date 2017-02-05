using System.Collections;

public class Missao
{
	public int id;
	public string titulo;
	public string cena;
	public bool obrigatoria, final;
	public int estado;
	public int[] desbloqueia;

	public Missao(int id, string titulo, string cena, bool obrigatoria, int[] desbloqueia = null, int extremidades = 0)
	{
		this.id=id;
		this.titulo=titulo;
		this.cena=cena;
		this.obrigatoria=obrigatoria;
		this.desbloqueia=desbloqueia;
		final = extremidades == 2;
		estado = (extremidades == 1) ? 1 : 0; //se extremidade for 1, é a primeira fase (logo, desbloqueada)
	}
}
