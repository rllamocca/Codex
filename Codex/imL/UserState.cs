namespace Codex
{
    public class UserState
    {
        public int Distancia { get; set; } = 0;
        public int Posicion { get; set; } = 0;
        public string Mensaje { get; set; }

        public decimal Porcentaje()
        {
            if (this.Distancia != 0.0m) return 1.0m * this.Posicion / this.Distancia;
            return 0.0m;
        }
        public string Proporcion()
        {
            return string.Format("{0} / {1}", this.Posicion, this.Distancia);
        }
    }
}
