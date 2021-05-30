using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Helpers
{
    public class CopyHelper
    {
        public static T DeepCopy<T>(T obj)
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
            return ret;
        }
    }
}
