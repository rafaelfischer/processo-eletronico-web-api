using Negocio.Notificacoes.Base;
using System;
using System.Collections.Generic;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Dominio.Base;
using System.Linq;
using MimeKit;
using MailKit.Net.Smtp;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Negocio.Notificacoes
{
    public class NotificacoesService : INotificacoesService
    {
        private readonly string FromEmail = "noreply@prodest.es.gov.br";
        private readonly string EmailSubject = "Notificação automática do Processo Eletrônico";
        //Environment Variable (host + port)
        private readonly string mailServerAddress = "sistemas.mail.dchm.es.gov.br";
        private readonly int mailServerPort = 25;

        private IRepositorioGenerico<Notificacao> _repositorioNotificacoes;

        private IUnitOfWork _unityOfWork;

        public NotificacoesService(IProcessoEletronicoRepositorios repositorios)
        {
            _repositorioNotificacoes = repositorios.Notificacoes;
            _unityOfWork = repositorios.UnitOfWork;
        }

        public IList<Notificacao> Get()
        {
            return _repositorioNotificacoes.Where(n => n.DataNotificacao == null).Include(p => p.Processo).Include(d => d.Despacho).ToList();
        }

        public void Post(int idProcesso, int idDespacho, string email)
        {
            Notificacao notificacao = new Notificacao { IdProcesso = idProcesso, IdDespacho = idDespacho, Email = email };
            _repositorioNotificacoes.Add(notificacao);
        }

        public void Run()
        {
            IList<Notificacao> notificacoes = Get();

            if (notificacoes.Count > 0)
            {
                SmtpClient client = new SmtpClient();
                client.Connect(mailServerAddress, mailServerPort, false);

                foreach (Notificacao notificacao in notificacoes)
                {
                    MimeMessage mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress(FromEmail));
                    mimeMessage.Subject = EmailSubject;
                    mimeMessage.To.Add(new MailboxAddress(notificacao.Email));

                    //Notificar autuação
                    if (!notificacao.IdDespacho.HasValue)
                    {
                        mimeMessage.Body = new TextPart("plain")
                        {
                            Text = GenerateAutuacaoMailBody(notificacao)
                        };

                        client.Send(mimeMessage);
                        notificacao.DataNotificacao = DateTime.Now;
                    }
                    //Notificar despacho
                    else
                    {
                        mimeMessage.Body = new TextPart("plain")
                        {
                            Text = GenerateDespachoMailBody(notificacao)
                        };

                        client.Send(mimeMessage);
                        notificacao.DataNotificacao = DateTime.Now;
                    }
                }
                client.Disconnect(true);
                _unityOfWork.Save();
            }
        }
        
        private string GenerateAutuacaoMailBody(Notificacao notificacao)
        {
            string numeroProcesso = GetNumeroProcesso(notificacao);
            string EmailBody;

            EmailBody = $"Sistema de Processo Eletrônico do Governo do Estado do Espírito Santo\n\n";
            EmailBody += $"Um processo de número {numeroProcesso} foi autuado e você foi cadastrado como interessado.\n";
            EmailBody += $"Para consultar detalhes do processo, utilize o Sistema de Processo Eletrônico clicando no link abaixo:\n\n";
            EmailBody += $"https://www.processoeletronico.es.gov.br \n\n";
            EmailBody += $"Essa é uma notificação automática. Não é necessário responder a esse e-mail.\n\n";
            EmailBody += $"(A qualidade da mensagem será melhorada. Essa é uma versão de testes ainda)";

            return EmailBody;
        }

        private string GenerateDespachoMailBody(Notificacao notificacao)
        {
            string numeroProcesso = GetNumeroProcesso(notificacao);
            string EmailBody;

            EmailBody = $"Sistema de Processo Eletrônico do Governo do Estado do Espírito Santo\n\n";
            EmailBody += $"Um processo de número {numeroProcesso} teve um trâmite registrado.\n";
            EmailBody += $"Para consultar detalhes do processo, utilize o Sistema de Processo Eletrônico clicando no link abaixo:\n\n";
            EmailBody += $"https://www.processoeletronico.es.gov.br \n\n";
            EmailBody += $"Essa é uma notificação automática. Não é necessário responder a esse e-mail.\n\n";
            EmailBody += $"(A qualidade da mensagem será melhorada. Essa é uma versão de testes ainda)";

            return EmailBody;
        }

        private string GetNumeroProcesso(Notificacao notificacao)
        {
            return notificacao.Processo.Sequencial + "-" + notificacao.Processo.DigitoVerificador.ToString().PadLeft(2, '0')
                            + "." + notificacao.Processo.Ano + "." + notificacao.Processo.DigitoPoder + "." + notificacao.Processo.DigitoEsfera
                            + "." + notificacao.Processo.DigitoOrganizacao.ToString().PadLeft(4, '0');
        }
    }
}
