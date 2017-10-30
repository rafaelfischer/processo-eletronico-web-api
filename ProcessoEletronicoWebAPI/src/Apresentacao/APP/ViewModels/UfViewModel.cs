using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class UfViewModel
    {
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public string Id { get { return this.Sigla; } }
        public string Text { get { return this.Nome; } }

        public List<UfViewModel> GetUFs()
        {
            string ufs = string.Empty;
            FileStream fileStream = new FileStream("Json/uf.json", FileMode.Open);

            using (StreamReader reader = new StreamReader(fileStream))
            {
                ufs = reader.ReadToEnd();
            }

            List<UfViewModel> listaUfs = JsonConvert.DeserializeObject<List<UfViewModel>>(ufs);

            return listaUfs;
        }
    }
}
