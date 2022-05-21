using MedidorModel.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel.DAL
{
    public class MedidorDALArchivo : IMedidorDAL
    {
        private MedidorDALArchivo()
        {

        }

        private static MedidorDALArchivo instancia;

        public static IMedidorDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new MedidorDALArchivo();
            }
            return instancia;

        }

        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/lecturas.txt"; 


        public void AgregarMedidor(Medidor medidor)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(archivo, true))
                {
                    writer.WriteLine(medidor.IdMedidor + " | " + medidor.Fecha + " | " + medidor.ValorConsumo);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public List<Medidor> ObtenerMedidor()
        {
            List<Medidor> lista = new List<Medidor>();
            try
            {
                using (StreamReader reader = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split('|');
                            int num = Convert.ToInt32(arr[0]);
                            string fecha = Convert.ToString(arr[1]);
                            double valor = Convert.ToDouble(arr[2]); //cambio esto

                            Medidor medidor = new Medidor()
                            {
                                IdMedidor = num,
                                Fecha = fecha,
                                ValorConsumo = valor
                            };

                            lista.Add(medidor);
                        }
                    } while (texto != null);
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }
    }
}
