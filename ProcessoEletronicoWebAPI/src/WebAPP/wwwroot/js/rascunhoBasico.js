/*Incialização da página*/
$(document).ready(function () {
    ResetSinalizacoesLista();
    ResetMunicipioLista();
    $('#formbasico select').select2({ width: '100%' });
});

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

/************************************************DADOS BASICOS************************************************/
/**
 * Reseta o formulário de informações básica de rascunho
 */
function LimparFormBasico() {
    $('#Atividade_Id').val($('#Atividade_Id option:first').val()).trigger('change')
    $('#GuidUnidade').index[0].trigger("change");
    $('form#formbasico')[0].reset();
}

/************************************************VISUALIZACAO************************************************/