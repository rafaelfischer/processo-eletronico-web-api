using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class MensagemViewModel
    {
        public TipoMensagem Tipo { get; set; }

        private string _titulo;
        public string Titulo
        {
            get
            {
                if (_titulo == null)
                {
                    switch (Tipo)
                    {
                        case TipoMensagem.Sucesso:
                            _titulo = "Sucesso";
                            break;
                        case TipoMensagem.Informacao:
                            _titulo = "Informação";
                            break;
                        case TipoMensagem.Atencao:
                            _titulo = "Atenção";
                            break;
                        case TipoMensagem.Erro:
                            _titulo = "Erro";
                            break;
                    }
                }

                return _titulo;
            }
            set
            {
                _titulo = value;
            }
        }

        public string Texto { get; set; }

        public string TipoToastr
        {
            get
            {
                switch (Tipo)
                {
                    case TipoMensagem.Sucesso:
                        return "success";
                    case TipoMensagem.Informacao:
                        return "info";
                    case TipoMensagem.Atencao:
                        return "warning";
                    case TipoMensagem.Erro:
                        return "error";
                    default:
                        return "info";
                }
            }
        }
    }   
}
