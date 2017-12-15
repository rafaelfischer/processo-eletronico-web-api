using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public class MensagemService
    {
        protected void SetMensagemAlerta(ICollection<MensagemViewModel> mensagens, string textoMensagem)
        {
            MensagemViewModel mensagem = new MensagemViewModel();
            mensagem.Texto = textoMensagem;
            mensagem.Tipo = TipoMensagem.Atencao;
            mensagens.Add(mensagem);
        }

        protected void SetMensagemErro(ICollection<MensagemViewModel> mensagens, Exception e)
        {
            MensagemViewModel mensagem = new MensagemViewModel();
            mensagem.Texto = e.Message;
            mensagem.Tipo = TipoMensagem.Erro;
            mensagens.Add(mensagem);
        }
        protected void SetMensagemInformacao(ICollection<MensagemViewModel> mensagens, string textoMensagem)
        {
            MensagemViewModel mensagem = new MensagemViewModel();
            mensagem.Texto = textoMensagem;
            mensagem.Tipo = TipoMensagem.Informacao;
            mensagens.Add(mensagem);
        }

        protected void SetMensagemSucesso(ICollection<MensagemViewModel> mensagens, string textoMensagem)
        {
            MensagemViewModel mensagem = new MensagemViewModel();
            mensagem.Texto = textoMensagem;
            mensagem.Tipo = TipoMensagem.Sucesso;
            mensagens.Add(mensagem);
        }
    }
}
