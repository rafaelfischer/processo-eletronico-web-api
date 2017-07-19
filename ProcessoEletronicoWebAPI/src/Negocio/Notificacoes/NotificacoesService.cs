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

        private IRepositorioGenerico<Notificacao> _repositorioNotificacoes;
        private IRepositorioGenerico<Processo> _repositorioProcessos;

        private IUnitOfWork _unityOfWork;

        public NotificacoesService(IProcessoEletronicoRepositorios repositorios)
        {
            _repositorioNotificacoes = repositorios.Notificacoes;
            _repositorioProcessos = repositorios.Processos;
            _unityOfWork = repositorios.UnitOfWork;
        }

        public IList<Notificacao> Get()
        {
            return _repositorioNotificacoes.Where(n => n.DataNotificacao == null).Include(p => p.Processo).Include(d => d.Despacho).ToList();
        }

        public void Insert(Processo processo)
        {
            InsertInteressadosPessoaFisica(processo);
            InsertInteressadosPessoaJuridica(processo);
            _unityOfWork.Save();
        }

        public void Insert(Despacho despacho)
        {
            Processo processo = _repositorioProcessos.Where(p => p.Id == despacho.IdProcesso)
                .Include(i => i.InteressadosPessoaFisica).ThenInclude(e => e.Emails)
                .Include(i => i.InteressadosPessoaJuridica).ThenInclude(e => e.Emails)
                .Single();

            InsertInteressadosPessoaFisica(processo, despacho.Id);
            InsertInteressadosPessoaJuridica(processo, despacho.Id);
            _unityOfWork.Save();
        }

        private void InsertInteressadosPessoaFisica(Processo processo, int? idDespacho = null)
        {
            //Inserir notificações para interessados pessoa física
            if (processo.InteressadosPessoaFisica != null)
            {
                foreach (InteressadoPessoaFisica interessadoPessoaFisica in processo.InteressadosPessoaFisica)
                {
                    if (interessadoPessoaFisica.Emails != null)
                    {
                        foreach (Email email in interessadoPessoaFisica.Emails)
                        {
                            Notificacao notificacao = new Notificacao { IdProcesso = processo.Id, IdDespacho = idDespacho, Email = email.Endereco };
                            _repositorioNotificacoes.Add(notificacao);
                        }
                    }
                }
            }
        }

        private void InsertInteressadosPessoaJuridica(Processo processo, int? idDespacho = null)
        {
            if (processo.InteressadosPessoaJuridica != null)
            {
                foreach (InteressadoPessoaJuridica interessadoPessoaJuridica in processo.InteressadosPessoaJuridica)
                {
                    if (interessadoPessoaJuridica.Emails != null)
                    {
                        foreach (Email email in interessadoPessoaJuridica.Emails)
                        {
                            Notificacao notificacao = new Notificacao { IdProcesso = processo.Id, IdDespacho = idDespacho, Email = email.Endereco };
                            _repositorioNotificacoes.Add(notificacao);
                        }
                    }
                }
            }
        }

        public void Run()
        {
            //Obtém lista de notificações que devem ser feitas
            IList<Notificacao> notificacoes = Get();

            if (notificacoes.Count > 0)
            {
                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect(Environment.GetEnvironmentVariable("MailServerHost"), int.Parse(Environment.GetEnvironmentVariable("MailServerPort")), false);

                    MimeMessage mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress(FromEmail));
                    mimeMessage.Subject = EmailSubject;
                    
                    foreach (Notificacao notificacao in notificacoes)
                    {
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

                        mimeMessage.To.Clear();
                    }
                    client.Disconnect(true);
                    _unityOfWork.Save();
                }
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
            EmailBody += $"(A qualidade da mensagem será revisada. Essa é uma versão de testes ainda)";

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
            EmailBody += $"(A qualidade da mensagem será revisada. Essa é uma versão de testes ainda)";

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
