using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serializable_CSharp.Model;

namespace Serializable_CSharp.Controller
{
    class CarroController
    {
        Carro carro = new Carro();
        private List<Carro> carros = new List<Carro>();

        internal List<Carro> Carros { get => carros; set => carros = value; }

        public List<Carro> Read()
        {
            return Carros;

        }

        public void Create(Carro carro)
        {
            carros.Add(carro);
        }

        public List<String[]> ReadAll()
        {
            List<String[]> get = new List<String[]>();
            for (int i = 0; i < carros.Count; i++)
            {
                
                get.Add(new String[]{carros[i].Marca, carros[i].Color,
                    Convert.ToString(carros[i].Precio)});
            }
            return get;
        }

        public void Delete(int index)
        {
            carros.RemoveAt(index);
        }
    }
}
