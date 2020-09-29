using System;

namespace Codex.Generic
{
    public class UserState
    {
        public Int32 Distancia { get; set; } = 0;
        public Int32 Posicion { get; set; } = 0;
        public String Mensaje { get; set; }

        public Decimal Porcentaje()
        {
            if (this.Distancia != 0.0m) return 1.0m * this.Posicion / this.Distancia;
            return 0.0m;
        }
        public String Proporcion()
        {
            return String.Format("{0} / {1}", this.Posicion, this.Distancia);
        }
    }
}
