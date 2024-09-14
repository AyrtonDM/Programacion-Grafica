using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProgPrincipal
{
    public class Serializa
    {
        private string direccion;

        public Serializa()
        {
            direccion = "../../../../Objetos/";
        }

        public Serializa(string direccion)
        {
            this.direccion = direccion;
        }
        public void serializarobjeto(string name,Objeto valor)
        {
            name += ".json";
            string datos = JsonConvert.SerializeObject(valor, Formatting.Indented);
            File.WriteAllText(direccion + name, datos);
        }

        public Objeto deserializarobjeto(string name)
        {
            try
            {
                name += ".json";
                string datos = File.ReadAllText(direccion + name);
                Objeto valor = JsonConvert.DeserializeObject<Objeto>(datos);
                return valor;
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrió un error inesperado.");
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
