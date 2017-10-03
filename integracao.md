# Documento de Integração do Processo Eletrônico

Este documento tem como objetivo detalhar como deve ser feita a integração de outros sistemas com esta API do processo eletrônico.
Por integração, entende-se como o passo-a-passo para o envio de processos para os recursos de (i) autuação de processos e de (ii) despachos de processos. Além disso, também prevê a leitura de todas as informações relacionadas a autuações e despachos de processos no processo eletrônico.

## 1. Autenticação

Todas as requisições à API do processo eletrônico necessitam de autenticação. Em outras palavras, todas as requisições devem ser acompanhadas de um Access Token.
Para obtenção do Access Token, há um sistema chamado **Acesso Cidadão**, que gerencia o cadastro de pessoas e sistemas e também o envio de access tokens para integração com outros sistemas como o Processo Eletrônico.

A documentação informando detalhes dos procedimentos que devem ser realizados para cadastro e obtenção de Access Tokens no Acesso cidadão está *em construção*. Enquanto esta documentação não estiver disponível, é necessário enviar um e-mail para o setor responsável informando da intenção de utilizar o Acesso Cidadão para integração com outros sistemas (como é caso do Processo Eletrônico). No caso, deve ser enviado para o e-mail é [caio.barbosa@prodest.es.gov.br]().

**Toda requisição** feita às APIs do Processo Eletrônico e do Organograma **deve ser acompanhada de um Access Token** (enviado no `request.header.Authorization` no formato `Bearer {access-token}`), seguindo o [RFC 6750](https://tools.ietf.org/html/rfc6750#section-6.1.1)


## 2. Integração com o Sistema de Organograma

Além do sistema Acesso Cidadão, utilizado para controle de autenticação e autorização de sistemas e usuários, o Processo Eletrônico é integrado ao sistema Organograma, que realiza o controle de organizações, setores (ou unidades) e municípios. Essa integração é feita seguinte forma: qualquer informação enviada à API do Processo Eletrônico relacionado a organização, unidade ou município deve ser enviado como um GUID (Global Unique IDentifier). O formato do GUID é avaliado (exemplo: `3ca6ea0e-ca14-46fa-a911-22e616303722`) e, caso o formato seja válido, esse GUID será enviado ao Organograma para avaliar se de fato a informação existe.

Para possibilitar que o sistema consulte a API do Organograma para obter informações, o sistema deve ter o escopo `ApiOrganograma` no seu cadastro no Acesso Cidadão. Dessa forma, quando for gerado um Access Token (que deve ser enviado em toda requisição), ele estará apto para consultar informações do Organograma.

Para obter a lista de organizações relacionadas ao Governo do Estado do Espírito Santo, é necessário enviar uma requisição HTTP GET para `https://sistemas.es.gov.br/prodest/organograma/api/organizacoes/3ca6ea0e-ca14-46fa-a911-22e616303722/filhas`

Para obter a lista de setores (ou unidades) de uma organização, é necessário enviar uma requisição HTTP GET para `https://sistemas.es.gov.br/prodest/organograma/api/unidades/organizacao/{GUID da organização}`

Para obter a lista de municípios, envie uma requisição HTTP GET para `https://sistemas.es.gov.br/prodest/organograma/api/municipios`

Nessas requisições informadas, dentro de cada elemento da resposta (no caso, será uma lista de informações) tem um campo `guid`. Esse `guid` que deve ser utilizado quando for necessário enviar informação de organização, unidade ou município para a API do Processo Eletrônico.

A API do sistema Organograma está totalmente documentada, ou seja, todas as requisições e respostas estão detalhadas. Essa documentação está disponível em [https://sistemas.es.gov.br/prodest/organograma/api/documentation/index.html]()


## Considerações Iniciais

 - O Processo Eletrônico é um sistema utilizado para autuação e despacho de processos administrativos genéricos. Não há qualquer cadastro de tipos ou categorias de processos. 
Isso significa dizer que todas as validações realizadas nesse sistema se limitam a validações de **integridade** das informações.

 - Para simplificar o endereço da API do Processo Eletrônico, considere que todas os endereços informados devem ser precedidos de `https://sistemas.es.gov.br/prodest/processoeletronico/`. Em outras palavras, quando for explicado que deve ser enviada uma requisição para `/api/processos`, considere que a URL é `https://sistemas.es.gov.br/prodest/processoeletronico/api/processos`.

 - Caso haja algum erro de validação, tais como informação inválida ou falta de preenchimento de um campo obrigatório, será gerado um erro http com status code `4xx` informando o motivo do erro. No entanto, caso seja gerado um erro http com status code `5xx`, é necessário enviar um email para [atendimento@prodest.es.gov.br](mailto:atendimeto@prodest.es.gov.br), pois trata-se de um erro de sistema, o que pode afetar a disponibilidade do serviço e não há qualquer ação por parte de outros clientes que possa resolver o problema. Outra alternativa é cadastrar uma [issue](https://github.com/prodest/processo-eletronico-web-api/issues) nesse repositório do GitHub.

- Caso o procedimento a ser realizado seja feito com sucesso será gerado um código de sucesso, ou seja, http status code `2xx`.

## Envio de um processo para Autuação

Após a realização da autenticação, o sistema que necessita de integração está pronto para consumir todos os serviços disponíveis no Processo Eletrônico.

Para enviar um processo para autuação, deve ser realizada uma requisição HTTP POST para `sistemas.es.gov.br/prodest/processoeletronico/api/processos` (juntamente com o Access Token) com um documento JSON no corpo da requisição. A estrutura desse documento poderá sofrer atualizações e a cada atualização, uma nova especificação é publicada automaticamente. A especificação do documento JSON que precisa ser enviada pode ser encontrado em [https://sistemas.dchm.es.gov.br/prodest/processoeletronico/api/documentation/index.html].

A estrutura do documento JSON estará disponível na seção **PROCESSOS** em POST `/api/processos`.

### Detalhamento das informações contidas no documento JSON

Alguns campos contidos nesse documento JSON devem ter uma atenção especial. Os demais não citados são campos livres, o que significa dizer que não há validação a não ser de obrigatoriedade de preenchimento (quando necessário) ou tamanho máximo. 

Segue abaixo detalhes dos campos:

### idAtividade

Trata-se um identificador numérico. Ao receber esse identificador, o sistema de Processo Eletrônico irá conferir se essa atividade existe dentro do escopo do Governo do Estado.
Para consultar todas as atividades disponíveis, é necessário enviar um HTTP GET para `api/atividades`. No corpo da resposta estará um documento JSON contendo todas as atividades disponíveis.

Lembrando que a única validação realizada nesse campo ao enviar um processo para a autuação é se a atividade existe. O sistema não faz relação do conteúdo do processo com o identificador da atividade. 

### interessadosPessoaFisica e intessadosPessoaJuridica

Interessados Pessoa Física e Jurídica são estruturas quase semelhantes. Como regra de validação, o processo necessita ter pelo menos um interessado (seja ele interessado pessoa física ou pessoa jurídica).

####  guidMunicipio

GUID do munícipio do interessado. Essa informação terá sua formatação validada. Além disso, será feita uma requisição ao sistema Organograma para verificar se este município existe.

#### idTipoContato e telefone (contatos)

Dentro de contatos (que é uma informação opcional), há a informação de idTipoContato e telefone. Todos os tipos de contato podem ser obtidos através de uma requisição HTTP GET para `api/tipos-contato`. Exemplo de tipos de contato são celular e telefone fixo, que possuem um número determinado de dígitos. A quantidade de dígitos do telefone será verificada de acordo com o tipo. No caso do telefone, este deve ser informado junto com o DDD e conter apenas números.

#### endereco (emails)

Dentro de emails (que é uma informação opcional), há o campo endereco, que é o e-mail do interessado. Será realizada uma verificação de formato nesse campo. Exemplos de emails válidos são `atendimento@prodest.es.gov.br`, `suporte@gmail.com`.

#### cpf (interessado pessoa física)

Deve ser enviado sem formatação, ou seja, apenas os caracteres numéricos. Esse campo terá seu formato validado de acordo com a validação de CPF. A existência desse CPF junto à base da receita federal não é verificada.

#### cnpj (interessado pessoa jurídica)

Deve ser enviado sem formatação, ou seja, apenas os caracteres numéricos. Esse campo terá seu formato validado de acordo com a validação de CNPJ. A existência desse CNPJ junto à base da receita federal não é verificada.

### municipios

O processo deve conter pelo menos um município. No caso desse campo, é uma lista contendo uma estrutura com `guidMunicipio`. Esse GUID terá seu formato validado, bem como a existência de um município com esse GUID será verificado no sistema Organograma.

### anexos

O processo pode possuir anexos. Não há qualquer restrição para tipos de anexos, o que significa dizer que o Processo Eletrônico trabalha com todos os tipos de arquivo, sejam eles áudios, vídeos ou documentos (pdf, docx, txt, entre outros).

Para efeito de integração, é **altamente recomendável** que seja enviado um arquivo de interoperabilidade no formato JSON. Caso outros sistemas possuam informações diferenciadas conforme o tipo de processo, esse arquivo torna-se muito útil para futura leitura do processo por parte de outros sistemas e também por parte do Processo Eletrônico.

Como o processo eletrônico não trabalha com tipos de processo, ele não possui campos específicos que fazem sentido apenas em determinado tipos de processos. Exemplo: no caso de um processo de recurso de multa de trânsito, informações como número da CNH do motorista e a placa do carro envolvido devem existir. Esse arquivo de interoperabilidade terá todas essas informações num formato que seja legível por parte de vários sistemas, no caso o formato recomendado é o JSON.

Outra recomendação é enviar como anexo o PDF contendo todas as informações do processo. Nesse caso, esse documento será lido por outros usuários e não por outros sistemas. Dessa forma, um documento do formato PDF é mais recomendado.

Em relação aos campos existentes no anexo, alguns merecem destaque. São eles:

#### nome

O nome do arquivo enviado. Exemplos: `documento.docx`, `processo.pdf`, `audio.mp3`.

#### conteudo

O conteúdo do anexo deve ser enviado através de uma string Base64. Esse formato será verificado no Processo Eletrônico. No caso dos arquivos de interoperabilidade e o PDF contendo dados do processo devem ter seus conteúdos enviados através desse campo.

#### mimeType

Esse campo é o tipo do anexo. Exemplos: `application/json`, `application/pdf`. Esse campo é meramente informativo e é usado para efeitos de download. Não é feita qualquer validação do mimeType com o conteúdo do anexo.

#### idTipoDocumental

`idTipoDocumental` é um campo **opcional** que, caso seja informado, terá sua existência verificada de acordo com a atividade informada no processo. Tipos documentais estão associados a uma determinada atividade. Em outras palavras, uma atividade possui uma série de tipos documentais.

### idSinalizacoes

O processo pode possuir sinalizações, que são informados através de uma lista de identificadores. Exemplos de sinalizações são "Projeto estruturante do Governo do Estado" ou "Preferencial". Para o Processo Eletrônico, esse campo é meramente informativo. Não há qualquer relação com o conteúdo do processo.

Para obter a lista de sinalizações, basta enviar uma requisição HTTP GET para `/api/sinalizacoes/organizacao-patriarca/{guidOrganizacaoPatriarca}`. Cada sinalização possui um campo `id`, este deve ser informado na lista de sinalizações do processo.

### guidOrganizacaoAutuadora

Esse campo tem uma verificação diferenciada. Quando um sistema ou usuário possui permissão de autuar um processo, estes possuem permissão em uma determinada organização. Não há uma permissão total. Essa permissão deve ser configurada por organização no **Acesso Cidadão**.
O `guidOrganizacaoAutuadora` sofrerá algumas validações, são elas:

- Seu formato será validado
- Será verificado se existe uma organização no Organograma com este GUID
- Será verificado nas permissões do usuário ou sistema se essa organização está cadastrada.

### guidUnidadeAutuadora

O `guidUnidadeAutuadora` sofrerá algumas validações:

- Seu formato será validado
- Será verificado se existe uma unidade no Organograma com este GUID
- Será verificado se esta unidade pertence à organização informada no processo (`guidOrganizacaoAutuadora`)

## Leitura de processos

Ao final do envio de um processo para autuação, será enviada um documento JSON no corpo da resposta. O formato desse documento pode ser encontrado em [https://sistemas.es.gov.br/prodest/processoeletronico/api/documentation/index.html]() em `Response Class`.

Para efeitos de leitura, o Processo Eletrônico disponibiliza os processos de diversas formas:

1. Consulta por ID

Todo processo possui um `id`, que é o identificador único do processo. Esse `id` é informado na resposta do envio do processo para Autuação. Para realizar essa consulta, envie uma requisição HTTP GET para `/api/processos/{id}`. Caso o processo exista, suas informações estarão no corpo da resposta. Caso contrário, será gerado um erro http com status code `404`.

2. Consulta por Número

Todo processo possui um número, que também tem como objetivo identificar o processo de forma única. Neste caso, este número é uma `string`. Esse campo também é informado como resultado do envio do processo para autuação no corpo da resposta em `numero`.

Para consultar o processo por número, envie uma requisição HTTP GET para `/api/processos/numero/{numero}`. Caso o processo exista, suas informações estarão no corpo da resposta. Caso contrário, será gerado um erro http com status code `404`

3. Consulta de processo por Organização

Para obter os processos que se encontram uma determinada organização, em outras palavras, processos que foram autuados em uma organização ou que o último despacho foi realizado para essa organização, é necessário enviar uma requisição HTTP GET para `/api/processos/organizacao/{guidOrganizacao}`.
Caso não haja processos na organização informada, será enviada uma lista vazia no corpo da resposta.

### O arquivo de interoperabilidade (em JSON)

Para todas as consultas acima, o arquivo de interoperabilidade (no formato JSON) será útil para outros sistemas realizaram a leitura do processo de forma mais específica. Exemplo, caso um determinado sistema envie um arquivo de interoperabilidade relacionado a um processo de recurso de multa, todas as informações específicas pertinentes a esse tipo de processo poderão ser lidas facilmente por parte deste sistema ou de qualquer outro que consulte este processo.

## Despachos de Processos

Um processo possui uma série de despachos em sequência, ou seja, o processo é despachado de um setor para outro dentro uma mesma organização ou para outra organização. O sistema de Processo Eletrônico não suporta despachos para mais de um setor. Isso significa dizer que o despacho deve possuir um, e apenas um, setor de destino.

Para realizar um despacho de processo através da API do Processo Eletrônico, é necessário enviar uma requisição HTTP POST para `/api/despachos` com um documento JSON no corpo da requisição. Os detalhes desse documento estão disponíveis na documentação do Processo Eletrônico em [https://sistemas.es.gov.br/prodest/processoeletronico/api/documentation]() na seção **DESPACHOS**.

Seguindo a mesma forma de explicar o documento para autuação de processo, será detalhado alguns campos específicos do documento JSON que deve ser enviado na requisição de despachos de processos bem como algumas regras de validação diferenciadas.

### idProcesso

Identificador do Processo que sofrerá um despacho. Esse identificador é o campo `id` do processo. 

### anexos

A estrtura de anexos é exatamente igual à que deve ser enviada para autuação de processos. Para verificar detalhes dos campos, acesse a seção de autuação de processos.
Em relação ao arquivo de interoperabilidade (no formato JSON), ele é **altamente recomendável** também para despachos de processos.

### guidOrganizacaoDestino e guidUnidadeDestino

O par `guidOrganizacaoDestino` e `guidUnidadeDestino` representa o setor (ou unidade) que o processo será despachado. Esses campos terão seu formato validado bem como serão enviados para o Organograma para verificar se existe a unidade na organização informada.

Para consultar todas as organizações do Governo do Estado, a requisição foi citada acima (na seção de autuação de processos). Quanto às unidades, para obtê-las é necessário conhecer primeira a organização e, em seguida, enviar uma requisição HTTP GET para à API do Organograma em `/api/unidades/organizacao/{guid da organização}`

Antes de gravar o despacho do processo, são feitas algumas validações em relação a origem e destino do processo:

1. É verificado, de acordo com as permissões do usuário ou sistema, se o processo se encontra na organização em que o usuário ou sistema possui permissão

Para saber a localização do processo, o sistema de Processo Eletrônico irá procurar o destino do último despacho realizado do processo ou, se o processo não possuir despachos, irá procurar a organização autuadora do processo.

2. Caso a condição 1 seja verdadeira, ou seja, o local onde o processo se encontra confere com as permissões do usuário ou sistema, será verificado se o par organização e unidade existe no Organograma.

3. Caso as condições 1 e 2 sejam verdadeiras, o despacho será realizado. Caso contrário, será gerado um erro http com status code `4xx` informando o motivo do erro.

Como observação final, o processo eletrônico **aceita** que sejam realizados despachos para a **mesmo unidade** em que o processo se encontra, ou seja, a origem (obtida automaticamente conforme explicado) e o destino do despacho do processo **podem ser iguais**.

## Leitura de despachos

Ao final da gravação do despacho de um processo, será enviado um documento JSON no corpo da resposta contendo detalhes do despacho. A estrutura desse documento JSON também pode ser encontrada na documentação do Processo Eletrônico em [https://sistemas.es.gov.br/prodest/processoeletronico/api/documentation]() na seção **DESPACHOS**.

Para consultar um despacho ou uma lista de despacho de um processo, há consultas disponíveis. Todas elas podem ser encontradas na documentação do Processo Eletrônico.

1. Consulta por identificador

Essa consulta obtém apenas um despacho ou retorna um erro http com status code `404`. O identificador utilizado nessa consulta está no campo `id` que é retornado ao final da gravação do despacho. Para realizar essa consulta, envie uma requisição HTTP GET para `/api/despachos/{id}`.

2. Consulta de despachos por processo.

Essa consulta traz a lista de despachos de um determinado processo. Caso não haja qualquer despacho, é retornada uma lista vazia. Para realizar essa consulta, envie uma requisição HTTP GET pra `/api/processos/{idProcesso}`. Dentro do documento JSON há a lista de despachos (em `despachos`), caso existam despachos, realizados no processo. 

## Observações finais

- Esse documento tem como objetivo detalhar os procedimentos pertinentes a autuação e despachos de processos.
- Caso haja alguma dúvida ou seja identificado a necessidade de detalhar outros procedimentos, entre em contato com os administradores do sistema ou cadastre uma [issue](https://github.com/prodest/processo-eletronico-web-api/issues) neste repositório do GitHub.
