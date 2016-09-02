﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Documentacao.Funcionalidades.Administrativo
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [TechTalk.SpecRun.FeatureAttribute("Plano de Classificação", new string[] {
            "PlanoClassificacao"}, Description="Como um administrador do Processo Eletrônico,\r\ndesejo cadastrar os planos de clas" +
        "sificação (inclusive os itens que os compõem),\r\npara que seja possível a autuaçã" +
        "o de processos eletrônicos com diversos tipos de planos de classificação.", SourceFile="Funcionalidades\\Administrativo\\PlanoClassificacao.feature", SourceLine=4)]
    public partial class PlanoDeClassificacaoFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "PlanoClassificacao.feature"
#line hidden
        
        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt"), "Plano de Classificação", "Como um administrador do Processo Eletrônico,\r\ndesejo cadastrar os planos de clas" +
                    "sificação (inclusive os itens que os compõem),\r\npara que seja possível a autuaçã" +
                    "o de processos eletrônicos com diversos tipos de planos de classificação.", ProgrammingLanguage.CSharp, new string[] {
                        "PlanoClassificacao"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [TechTalk.SpecRun.FeatureCleanup()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        [TechTalk.SpecRun.ScenarioCleanup()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 10
#line 11
 testRunner.Given("que eu logue como administrador", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Inserir um plano de classificação", SourceLine=12)]
        public virtual void InserirUmPlanoDeClassificacao()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Inserir um plano de classificação", ((string[])(null)));
#line 13
this.ScenarioSetup(scenarioInfo);
#line 10
this.FeatureBackground();
#line 14
    testRunner.Given("que eu tenho o link de acesso ao serviço de incluir um novo plano de classificaçã" +
                    "o", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line 15
    testRunner.And("que eu defina a descrição", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line 16
    testRunner.And("que eu defina os itens de classificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line 17
    testRunner.And("que eu defina a descrição de cada item de classificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line 18
  testRunner.When("eu requisitar o serviço", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 19
   testRunner.Then("devo receber a resposta \"200 OK (A requisição teve sucesso)\" com o plano de class" +
                    "ificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Pesquisar um plano de classificação", SourceLine=20)]
        public virtual void PesquisarUmPlanoDeClassificacao()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Pesquisar um plano de classificação", ((string[])(null)));
#line 21
this.ScenarioSetup(scenarioInfo);
#line 10
this.FeatureBackground();
#line 22
    testRunner.Given("que eu tenho o link de acesso ao serviço de pesquisar um plano de classificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line 23
    testRunner.And("que eu tenho o identificador de um plano de classificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line 24
  testRunner.When("eu requisitar o serviço", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 25
   testRunner.Then("devo receber a resposta \"200 OK (A requisição teve sucesso)\" com o plano de class" +
                    "ificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Pesquisar planos de classificação", SourceLine=26)]
        public virtual void PesquisarPlanosDeClassificacao()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Pesquisar planos de classificação", ((string[])(null)));
#line 27
this.ScenarioSetup(scenarioInfo);
#line 10
this.FeatureBackground();
#line 28
    testRunner.Given("que eu tenho o link de acesso ao serviço de pesquisar um plano de classificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line 29
  testRunner.When("eu requisitar o serviço", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 30
   testRunner.Then("devo receber a resposta \"200 OK (A requisição teve sucesso)\" com uma lista de pla" +
                    "nos de classificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Editar um plano de classificação", SourceLine=31)]
        public virtual void EditarUmPlanoDeClassificacao()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Editar um plano de classificação", ((string[])(null)));
#line 32
this.ScenarioSetup(scenarioInfo);
#line 10
this.FeatureBackground();
#line 33
    testRunner.Given("que eu tenho o link de acesso ao serviço de editar um plano de classificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line 34
    testRunner.And("que eu tenho as informações de um plano de classificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line 35
    testRunner.And("que eu defina a descrição", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line 36
    testRunner.And("que eu defina os itens de classificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line 37
    testRunner.And("que eu defina a descrição de cada item de classificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line 38
  testRunner.When("eu requisitar o serviço", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 39
   testRunner.Then("devo receber a resposta \"200 OK (A requisição teve sucesso)\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Excluir um ambiente", SourceLine=40)]
        public virtual void ExcluirUmAmbiente()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Excluir um ambiente", ((string[])(null)));
#line 41
this.ScenarioSetup(scenarioInfo);
#line 10
this.FeatureBackground();
#line 42
    testRunner.Given("que eu tenho o link de acesso ao serviço de excluir um plano de classificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line 43
    testRunner.And("que eu tenho o identificador de um plano de classificação", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line 44
  testRunner.When("eu requisitar o serviço", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 45
   testRunner.Then("devo receber a resposta \"200 OK (A requisição teve sucesso)\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.TestRunCleanup()]
        public virtual void TestRunCleanup()
        {
            TechTalk.SpecFlow.TestRunnerManager.GetTestRunner().OnTestRunEnd();
        }
    }
}
#pragma warning restore
#endregion
