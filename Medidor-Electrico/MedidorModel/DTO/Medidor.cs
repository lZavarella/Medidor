using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel.DTO
{
    public class Medidor
    {
        private int idMedidor;
        private string fecha;
        private double valorConsumo;

        public int IdMedidor { get => idMedidor; set => idMedidor = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public double ValorConsumo { get => valorConsumo; set => valorConsumo = value; }

        public override string ToString()
        {

            return idMedidor + "| " + Fecha + "| " + valorConsumo;
        }
    }
}
