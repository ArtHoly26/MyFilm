using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFilm
{
    public class SearchResult 
    {
            [JsonProperty("results")]
            public List<Film> Results {  get; set; }      
    }
}
