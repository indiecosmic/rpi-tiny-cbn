using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ThingM.Blink1.ColorProcessor;

namespace TinyCbn
{
    public class CbnWrapper
    {
        public async Task<List<Rgb>> GetColours()
        {
            List<Rgb> colors = new List<Rgb>();
            using (var stream = await new HttpClient().GetStreamAsync("http://api.colourbynumbers.org/cbn-live/getColours"))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        dynamic d = new JsonSerializer().Deserialize(jsonReader);
                        for (int i = 0; i <= 9; i++)
                        {
                            JProperty color = ((JObject)d.colours).Property(i.ToString());
                            JArray arr = color.Value as JArray;
                            Rgb c = new Rgb((ushort)arr[0], (ushort)arr[1], (ushort)arr[2]);
                            colors.Add(c);
                        }
                    }
                }

                return colors;
            }
        }
    }
}
