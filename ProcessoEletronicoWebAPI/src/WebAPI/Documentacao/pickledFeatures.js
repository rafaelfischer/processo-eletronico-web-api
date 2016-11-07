jsonPWrapper ({
  "Features": [
    {
      "RelativeFolder": "Administrativo\\PlanoClassificacao.feature",
      "Feature": {
        "Name": "Plano de Classificação",
        "Description": "Como um administrador do Processo Eletrônico,\r\ndesejo cadastrar os planos de classificação (inclusive os itens que os compõem),\r\npara que seja possível a autuação de processos eletrônicos com diversos tipos de planos de classificação.",
        "FeatureElements": [
          {
            "Name": "Inserir um plano de classificação",
            "Slug": "inserir-um-plano-de-classificacao",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Dado ",
                "Name": "que eu tenho o link de acesso ao serviço de incluir um novo plano de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "And",
                "NativeKeyword": "E ",
                "Name": "que eu defina a descrição",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "And",
                "NativeKeyword": "E ",
                "Name": "que eu defina os itens de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "And",
                "NativeKeyword": "E ",
                "Name": "que eu defina a descrição de cada item de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "When",
                "NativeKeyword": "Quando ",
                "Name": "eu requisitar o serviço",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Então ",
                "Name": "devo receber a resposta \"200 OK (A requisição teve sucesso)\" com o plano de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          },
          {
            "Name": "Pesquisar um plano de classificação",
            "Slug": "pesquisar-um-plano-de-classificacao",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Dado ",
                "Name": "que eu tenho o link de acesso ao serviço de pesquisar um plano de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "And",
                "NativeKeyword": "E ",
                "Name": "que eu tenho o identificador de um plano de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "When",
                "NativeKeyword": "Quando ",
                "Name": "eu requisitar o serviço",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Então ",
                "Name": "devo receber a resposta \"200 OK (A requisição teve sucesso)\" com o plano de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          },
          {
            "Name": "Pesquisar planos de classificação",
            "Slug": "pesquisar-planos-de-classificacao",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Dado ",
                "Name": "que eu tenho o link de acesso ao serviço de pesquisar um plano de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "When",
                "NativeKeyword": "Quando ",
                "Name": "eu requisitar o serviço",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Então ",
                "Name": "devo receber a resposta \"200 OK (A requisição teve sucesso)\" com uma lista de planos de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          },
          {
            "Name": "Editar um plano de classificação",
            "Slug": "editar-um-plano-de-classificacao",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Dado ",
                "Name": "que eu tenho o link de acesso ao serviço de editar um plano de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "And",
                "NativeKeyword": "E ",
                "Name": "que eu tenho as informações de um plano de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "And",
                "NativeKeyword": "E ",
                "Name": "que eu defina a descrição",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "And",
                "NativeKeyword": "E ",
                "Name": "que eu defina os itens de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "And",
                "NativeKeyword": "E ",
                "Name": "que eu defina a descrição de cada item de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "When",
                "NativeKeyword": "Quando ",
                "Name": "eu requisitar o serviço",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Então ",
                "Name": "devo receber a resposta \"200 OK (A requisição teve sucesso)\"",
                "StepComments": [],
                "AfterLastStepComments": []
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          },
          {
            "Name": "Excluir um ambiente",
            "Slug": "excluir-um-ambiente",
            "Description": "",
            "Steps": [
              {
                "Keyword": "Given",
                "NativeKeyword": "Dado ",
                "Name": "que eu tenho o link de acesso ao serviço de excluir um plano de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "And",
                "NativeKeyword": "E ",
                "Name": "que eu tenho o identificador de um plano de classificação",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "When",
                "NativeKeyword": "Quando ",
                "Name": "eu requisitar o serviço",
                "StepComments": [],
                "AfterLastStepComments": []
              },
              {
                "Keyword": "Then",
                "NativeKeyword": "Então ",
                "Name": "devo receber a resposta \"200 OK (A requisição teve sucesso)\"",
                "StepComments": [],
                "AfterLastStepComments": []
              }
            ],
            "Tags": [],
            "Result": {
              "WasExecuted": false,
              "WasSuccessful": false
            }
          }
        ],
        "Background": {
          "Name": "Logado como administrador",
          "Description": "",
          "Steps": [
            {
              "Keyword": "Given",
              "NativeKeyword": "Dado ",
              "Name": "que eu logue como administrador",
              "StepComments": [],
              "AfterLastStepComments": []
            }
          ],
          "Tags": [],
          "Result": {
            "WasExecuted": false,
            "WasSuccessful": false
          }
        },
        "Result": {
          "WasExecuted": false,
          "WasSuccessful": false
        },
        "Tags": [
          "@PlanoClassificacao"
        ]
      },
      "Result": {
        "WasExecuted": false,
        "WasSuccessful": false
      }
    }
  ],
  "Summary": {
    "Tags": [
      {
        "Tag": "@PlanoClassificacao",
        "Total": 5,
        "Passing": 0,
        "Failing": 0,
        "Inconclusive": 5
      }
    ],
    "Folders": [
      {
        "Folder": "Administrativo",
        "Total": 5,
        "Passing": 0,
        "Failing": 0,
        "Inconclusive": 5
      }
    ],
    "NotTestedFolders": [
      {
        "Folder": "Administrativo",
        "Total": 0,
        "Passing": 0,
        "Failing": 0,
        "Inconclusive": 0
      }
    ],
    "Scenarios": {
      "Total": 5,
      "Passing": 0,
      "Failing": 0,
      "Inconclusive": 5
    },
    "Features": {
      "Total": 1,
      "Passing": 0,
      "Failing": 0,
      "Inconclusive": 1
    }
  },
  "Configuration": {
    "GeneratedOn": "2 setembro 2016 15:34:42"
  }
});