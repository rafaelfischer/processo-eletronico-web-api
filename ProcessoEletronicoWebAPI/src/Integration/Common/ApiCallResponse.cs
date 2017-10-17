using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.ProcessoEletronico.Integration.Common
{
    public class ApiCallResponse<T>
    {
        public int StatusCode { get; set; }
        public T ResponseObject {get; set;}
    }
}
