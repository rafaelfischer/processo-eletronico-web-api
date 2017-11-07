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

/************************************************INTERESSADOS************************************************/
//CARREGAR ORGANIZACOES
$("#interessadoTipo").on("change", function () {

    var formData = new FormData();
    var tipoInteressado = $(this).val();
    var url = "";
    formData.append("tipoInteressado", tipoInteressado);

    if (tipoInteressado > 0) {
        url = "/RascunhoInteressado/FormInteressado";
    } else {
        return false;
    }

    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#formInteressado").html(data)
                $('#formInteressado select').select2({ width: '100%' })
            }
        }
    );
});

//CARREGAR UNIDADES POR ORGANIZACAO
$("#forminteressados").on("select2:select", "#interessadoOrgao", function () {

    var formData = new FormData();
    var orgao = $(this).val();
    var url = "/Suporte/GetUnidadesPorOrganizacao";
    formData.append("guidOrganizacao", orgao);

    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#interessadoUnidade").html(data)
                $('#interessadoUnidade select').select2({ width: '100%' })
            }
        }
    );
});

//SALVAR INTERESSADO ORGANIZACAO/UNIDADE
$("#forminteressados").on("click", "#btnAddInteressado", function () {

    var tipoInteressado = $("#interessadoTipo").val();
    var formData = new FormData();
    var url = "/RascunhoInteressado/IncluirInteressadoPJ";
    var guidOrganizacao = $("#interessadoOrgao").val();
    var guidUnidade = $("#interessadoOrgaoUnidade").val();

    formData.append("idRascunho", $("#formanexo").find("#Id").val());

    switch (tipoInteressado) {
        case "1":
            if (isNullOrEmpty(guidUnidade)) {
                formData.append("guidOrganizacao", guidOrganizacao);
            }
            else {
                formData.append("guidOrganizacao", guidOrganizacao);
                formData.append("guidUnidade", guidUnidade);
            }
            break;
        case "2":
            break;
        case "3":
            break;
        default:
            return false;
    }

    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#listainteressados").html(data)
                //$('#interessadoUnidade select').select2({ width: '100%' })
            }
        }
    );

    return false;
});

//EXCLUIR INTERESSADO PESSOA JURÍDICA
$("#listainteressados").on("click", ".btn-excluir-interessado-pj", function () {

    var idRascunho = $("#forminteressados").find("#Id").val();
    var formData = new FormData();
    var url = "/RascunhoInteressado/ExcluirInteressadoPJ";

    formData.append("idRascunho", idRascunho);
    formData.append("idInteressadoPJ", $(this).attr('data-id'));

    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#listainteressados").html(data)
                //$('#interessadoUnidade select').select2({ width: '100%' })
            }
        }
    );

    return false;
});





/************************************************VISUALIZACAO************************************************/