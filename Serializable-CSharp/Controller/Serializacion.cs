using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Serializable_CSharp.Controller
{
    class Serializacion
    {
        private Stream stream;
        private BinaryFormatter Formateador = new BinaryFormatter();

        public bool Serializar(object array)
        {
            stream = new FileStream("Carros.dat", FileMode.Create, FileAccess.Write, FileShare.None);
            Formateador.Serialize(stream, array);
            stream.Close();
            return true;
        }

        public object Deserializar()
        {
            object v = null;
            stream = new FileStream("Carros.dat", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
            if (stream.Length > 0)
                v = Formateador.Deserialize(stream);
            stream.Close();
            return v;
        }
    }
}
