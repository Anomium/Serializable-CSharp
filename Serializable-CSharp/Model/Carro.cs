using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializable_CSharp.Model
{
    class Carro
    {
        public String Marca { get; set; }
        public String Color { get; set; }
        public double Precio { get; set; }

        public Carro(string marca, string color, double precio)
        {
            Marca = marca;
            Color = color;
            Precio = precio;
        }

        public Carro()
        {

        }
    }
}
