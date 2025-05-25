namespace ServicosGlobais.Model
{

    public class DiaDeExecucao
    {
        public int Ordem { get; set; }
        public string DiaDaSemana { get; set; }
        public bool isExecutar { get; set; }

        public override string ToString()
        {
            return DiaDaSemana;
        }
    }
}
