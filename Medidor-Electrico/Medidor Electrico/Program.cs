using Medidor_Electrico.Comunicacion;
using MedidorModel.DAL;
using MedidorModel.DTO;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedidorElectrico
{
    class Program
    {
        private static IMedidorDAL medidorDAL = MedidorDALArchivo.GetInstancia();
        static void Main(string[] args)
        {
            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.IsBackground = true;
            t.Start();
            while (Menu()) ;
        }
        private static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("Bienvenido");
            Console.WriteLine("1. Ingresar");
            Console.WriteLine("2. Mostrar");
            Console.WriteLine("OK Salir");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Elija una opcion");
                    break;
            }
            return continuar;
        }
        private static void Ingresar()
        {
            try
            {
                Console.WriteLine("Ingrese los datos de la factura: ");
                string datos = Console.ReadLine().Trim();

                string[] data = datos.Split('|', '|', '|');

                int num = Convert.ToInt32(data[0]);
                string fecha = Convert.ToString(data[1]);
                double valor = Convert.ToDouble(data[2]);

                Medidor medidor = new Medidor()
                {
                    IdMedidor = num,
                    Fecha = fecha,
                    ValorConsumo = valor
                };

                lock (medidorDAL)
                {
                    medidorDAL.AgregarMedidor(medidor);
                }
            }
            catch
            {
                Console.WriteLine("Datos ingresados erroneos");
            }
        }
        private static void Mostrar()
        {
            List<Medidor> medidors = null;
            lock (medidorDAL)
            {
                medidors = medidorDAL.ObtenerMedidor();
            }
            foreach (Medidor medidor in medidors)
            {
                Console.WriteLine(medidor);
            }
        }
    }
}

