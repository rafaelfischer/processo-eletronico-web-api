#language: pt

@PlanoClassificacao

Funcionalidade: Plano de Classificação
Como um administrador do Processo Eletrônico,
desejo cadastrar os planos de classificação (inclusive os itens que os compõem),
para que seja possível a autuação de processos eletrônicos com diversos tipos de planos de classificação.

Contexto: Logado como administrador
	Dado que eu logue como administrador

Cenario: Inserir um plano de classificação
    Dado que eu tenho o link de acesso ao serviço de incluir um novo plano de classificação
	   E que eu defina a descrição
	   E que eu defina os itens de classificação
	   E que eu defina a descrição de cada item de classificação
  Quando eu requisitar o serviço
   Então devo receber a resposta "200 OK (A requisição teve sucesso)" com o plano de classificação

Cenario: Pesquisar um plano de classificação
    Dado que eu tenho o link de acesso ao serviço de pesquisar um plano de classificação
	   E que eu tenho o identificador de um plano de classificação
  Quando eu requisitar o serviço
   Então devo receber a resposta "200 OK (A requisição teve sucesso)" com o plano de classificação

Cenario: Pesquisar planos de classificação
    Dado que eu tenho o link de acesso ao serviço de pesquisar um plano de classificação
  Quando eu requisitar o serviço
   Então devo receber a resposta "200 OK (A requisição teve sucesso)" com uma lista de planos de classificação

Cenario: Editar um plano de classificação
    Dado que eu tenho o link de acesso ao serviço de editar um plano de classificação
	   E que eu tenho as informações de um plano de classificação
	   E que eu defina a descrição
	   E que eu defina os itens de classificação
	   E que eu defina a descrição de cada item de classificação
  Quando eu requisitar o serviço
   Então devo receber a resposta "200 OK (A requisição teve sucesso)"

Cenario: Excluir um ambiente
    Dado que eu tenho o link de acesso ao serviço de excluir um plano de classificação
	   E que eu tenho o identificador de um plano de classificação
  Quando eu requisitar o serviço
   Então devo receber a resposta "200 OK (A requisição teve sucesso)"
