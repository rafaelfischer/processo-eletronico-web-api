using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.ProcessoEletronico.Integration.Common.Base
{
    public interface IApiHandler
    {
        ApiCallResponse<T> DownloadJsonDataFromApi<T>(string url);
    }
}
