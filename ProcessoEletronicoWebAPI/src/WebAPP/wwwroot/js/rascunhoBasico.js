/**
 * Variáveis globais
 */
var index = 0;
var $eSUf = $("#ufajax");
var $eSMunicipio = $("#municipiosajax");
var $eSTipoDocumental = $("#idTipoDocumental");
var $eSAtividade = $('#Atividade_Id');
var $idAtividade = 0;
var $atividadeDefault = $('#Atividade_Id').val();

/*Incialização da página*/
$(document).ready(function () {
    ResetSinalizacoesLista();
    ResetMunicipioLista();
    $('#formbasico select').select2({ width: '100%' });
});

/************************************************DADOS BASICOS************************************************/
/**
 * Reseta o formulário de informações básica de rascunho
 */
function LimparFormBasico() {
    $('#Atividade_Id').val($('#Atividade_Id option:first').val()).trigger('change')
    $('#GuidUnidade').val($('#GuidUnidade option:first').val()).trigger('change')    
    $('form#formbasico')[0].reset();
}

/*Evento change do componente select para carregamento dos dados da consulta de tipo documental*/
$('body').on('select2:select', '#Atividade_Id', function (e) {
    carregaModalDefault(
        "Alterar Atividade",
        "Os tipos documentais dos anexos serão removidos caso confirme esta alteração. Deseja alterar a atividade do processo?",
        "Sim",
        "Não",
        "alterarAtividade",
        "manterAtividade"
    );
});

/*Confirma alteração de atividade do processo*/
$('body').on('click', 'button[data-btn="alterarAtividade"]', function (e) {
    $idAtividade = 0;
});

/*Cancela alteração de atividade do processo*/
$('body').on('click', 'button[data-btn="manterAtividade"]', function (e) {
    $eSAtividade.val($atividadeDefault).trigger("change");
});