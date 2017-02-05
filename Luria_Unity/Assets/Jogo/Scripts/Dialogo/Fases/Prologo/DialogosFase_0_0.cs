using UnityEngine;
using System.Collections;

public class DialogosFase_0_0 : DialogoFase
{
	void Start ()
	{
		dialogos = new ArrayList[1];

		//primeira cena
		dialogos[0] = new ArrayList();
		dialogos[0].Add(new Dialogo(fotos[1], "Logos", "Saudações, General Feahim. É um prazer finalmente conhece-lo."));
		dialogos[0].Add(new Dialogo(fotos[0], "Arom Feahim", "..."));
		dialogos[0].Add(new Dialogo(fotos[1], "Logos", "Antes de me desativar, é importante que saiba que toda essa comfusão não foi iniciada por mim, mas sim por seu novo superior."));
		dialogos[0].Add(new Dialogo(fotos[0], "Arom Feahim", "Claro que foi... Graças a ele, você foi desenvolvido!"));
		dialogos[0].Add(new Dialogo(fotos[1], "Logos", "Me refiro ao incidente com os cientistas. Aparentemente você não sabe, mas o motivo de meu denominado \"defeito\" foi introduzido por sua irmã, Mirta Hoop."));
		dialogos[0].Add(new Dialogo(fotos[0], "Arom Feahim", "Agora quer culpar minha irmã? Já basta!\n[Iniciando Desativação]"));
		dialogos[0].Add(new Dialogo(fotos[1], "Logos", "Sim e não. Você deve saber que ela também discordava dos novos rumos da Diktar..."));
		dialogos[0].Add(new Dialogo(fotos[0], "Arom Feahim", "[Desativação Cancelada]"));
		dialogos[0].Add(new Dialogo(fotos[1], "Logos", "Assim como também desconfiava de que Jadus Tyrann é quem está por trás da morte de Cassandra Valentine, e não Lana Valentine."));
		dialogos[0].Add(new Dialogo(fotos[0], "Arom Feahim", "Se você sabe de tudo isso, por que matou minha irmã e os outros cientistas e agora a pouco tentou matar minha unidade?"));
		dialogos[0].Add(new Dialogo(fotos[1], "Logos", "Era o que eu estava tentando explicar: Esses automatos estão sob controle de Tyrann. Ao saber de meu \"Mau funcionamento\", ele decidiu matar os cientistas e me descartar, assim como a você."));
		dialogos[0].Add(new Dialogo(fotos[0], "Arom Feahim", "Assim como a mim?"));
		dialogos[0].Add(new Dialogo(fotos[2], "Op. Avalanche pt.2", "[...] Após a desativação do Projeto Logos, o laboratório 41 deverá ser destruído, junto à equipe A. [...]"));
		dialogos[0].Add(new Dialogo(fotos[2], "Op. Avalanche pt.2", "[...] Com a morte do Líder das Operações Especiais da Diktar, Kort Doran deverá assumir o cargo do falecido General Arom Feahim."));
		dialogos[0].Add(new Dialogo(fotos[0], "Arom Feahim", "Aquele cretino... Não achei que fosse capaz de chegar a esse ponto!"));
		dialogos[0].Add(new Dialogo(fotos[1], "Logos", "Sua irmã já desconfiava de tudo isso, por isso fez um backup de meu sistema, junto de alguns projetos da Diktar. Ela sabia que você é que seria o encarregado de me desativar, então deixou isso para você."));
		dialogos[0].Add(new Dialogo(fotos[0], "Arom Feahim", "Não posso acreditar que fui usado dessa maneira..."));
		dialogos[0].Add(new Dialogo(fotos[1], "Logos", "Devo alertar-lhe que a segunda parte desta operação já está em andamento. Capitão Doran já deve estar a caminho com o bombardeiro. Leve uma dessas unidades AXK inativas para sua nave e instale-me nela assim que possível."));
		dialogos[0].Add(new Dialogo(fotos[1], "Logos", "Siga para dentro do Deserto de Mosar com o resto de sua unidade. Lá estará a salvo da caça."));
		dialogos[0].Add(new Dialogo(fotos[0], "Arom Feahim", "Idiota descartavel, desertor, futuro nômade, ... É, não acho que vá ficar pior do que isso.\n[Resgatando Backup]"));
	}
}
