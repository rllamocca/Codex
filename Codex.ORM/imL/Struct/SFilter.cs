using System;

using Codex.ORM.Enum;

namespace Codex.ORM.Struct
{
    public struct SFilter
    {
        public ECheck Comprobacion;
        public String Personalizado;

        public SFilter(ECheck _c)
        {
            this.Comprobacion = _c;
            this.Personalizado = "";
        }
        public SFilter(String _p)
        {
            this.Comprobacion = ECheck.Si;
            this.Personalizado = _p;
        }

        public String Formato()
        {
            if (this.Personalizado != null && this.Personalizado.Length > 0) return this.Personalizado;
            switch (this.Comprobacion)
            {
                case ECheck.Igual:
                    return "[{0}] = {1}";
                case ECheck.Diferente:
                    return "[{0}] != {1}";
                case ECheck.Menor:
                    return "[{0}] < {1}";
                case ECheck.MenorIgual:
                    return "[{0}] <= {1}";
                case ECheck.Mayor:
                    return "[{0}] > {1}";
                case ECheck.MayorIgual:
                    return "[{0}] >= {1}";
                case ECheck.Contiene:
                    return "[{0}] LIKE '%'+{1}+'%'";
                case ECheck.NoContiene:
                    return "[{0}] NOT LIKE '%'+{1}+'%'";
                case ECheck.Comienza:
                    return "[{0}] LIKE {1}+'%'";
                case ECheck.NoComienza:
                    return "[{0}] NOT LIKE {1}+'%'";
                case ECheck.Termina:
                    return "[{0}] LIKE '%'+{1}";
                case ECheck.NoTermina:
                    return "[{0}] NOT LIKE '%'+{1}";
                case ECheck.Entre:
                    return "[{0}] BETWEEN {1} AND {2}";
                case ECheck.NoEntre:
                    return "[{0}] NOT BETWEEN {1} AND {2}";
                case ECheck.Contenido:
                    return "[{0}] IN ({1})";
                case ECheck.NoContenido:
                    return "[{0}] NOT IN ({1})";
                case ECheck.Vacio:
                    return "[{0}] IS NULL";
                case ECheck.NoVacio:
                    return "[{0}] NOT IS NULL";
                default:
                    return "";
            }
        }
    }
}
