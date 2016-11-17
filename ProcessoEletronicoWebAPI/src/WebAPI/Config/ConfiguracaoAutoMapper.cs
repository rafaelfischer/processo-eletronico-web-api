using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Config
{
    public static class ConfiguracaoAutoMapper
    {
        public static void CriarMapeamento()
        {
            Apresentacao.Config.ConfiguracaoAutoMapper.ExecutaMapeamento();
        }
    }
}
