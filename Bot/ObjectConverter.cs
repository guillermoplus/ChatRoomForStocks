using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Bot
{
    public class ObjectConverter
    {
        public static byte[] getBytes(object obj)
        {
            try
            {
                if (obj == null)
                    return null;
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, obj);
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo realizar la conversión debido a: {0}", ex.Message);
                return null;
            }
        }

        public static T getObject<T>(byte[] byteArray)
        {
            try
            {
                if (byteArray == null) return default(T);

                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream(byteArray))
                {
                    object obj = bf.Deserialize(ms);
                    return (T)obj;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo realizar la conversión debido a: {0}", ex.Message);
                return default(T);
            }
        }
    }
}
