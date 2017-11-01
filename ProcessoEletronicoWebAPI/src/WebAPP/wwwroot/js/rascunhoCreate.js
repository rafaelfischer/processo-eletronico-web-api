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

/*Inicialização do componente tipo documental*/
$eSTipoDocumental.select2(
    {
        width: '100%',
        placeholder: 'Selecione um tipo documental',
        language: {
            noResults: function () {
                return "Informe uma atividade para o processo.";
            }
        }
    }
);

/*Evento change do componente select para carregamento dos dados da consulta de tipo documental*/
$eSAtividade.on('select2:selecting', function (e) {    
    carregaModalDefault(        
        "Alterar Atividade",
        "Os tipos documentais dos anexos serão removidos caso confirme esta alteração. Deseja alterar a atividade do processo?",
        "",
        "",
        "alterarAtividade",
        "manterAtividade"        
    );
});

$('body').on('click', '.alterarAtividade', function (e) {
    alert('Confirmou!');
});

$('body').on('click', '.manterAtividade', function (e) {    
    $eSAtividade.val($atividadeDefault).trigger("change");
});

/*Carrega tipos documentais com base no idAtividade */
function getTiposDocumentais() {
    $idAtividade = $('#Atividade_Id').val();
    $.get(
        "/Suporte/GetTiposDocumentais?idAtividade=" + $idAtividade, function (data) {
            $eSTipoDocumental.empty();
            if (data.length > 0) {
                data.unshift({ id: '-1', text: 'Selecione um tipo documental' });
            }
            $eSTipoDocumental.select2({
                width: '100%',
                data: data
            });
        }).fail(function () {
            $eSTipoDocumental.select2({
                width: '100%',
                language: {
                    noResults: function () {
                        return "Não foi possível carregar tipos documentais.";
                    }
                }
            });
        });
}

/*Evento abrindo do componente tipo documental para carregamento dos dados da consulta de tipo documental*/
$eSTipoDocumental.on('select2:opening', function (e) {
    if ($idAtividade == 0) {
        $idAtividade = $('#Atividade_Id').val();
        $.get(
            "/Suporte/GetTiposDocumentais?idAtividade=" + $idAtividade, function (data) {
                CarregaTiposDocumentais(data);
            }).fail(function () {
                ErrorTiposDocumentais();
            });
    }
});

/**
 * Carrega dados da consulta ajax para o componente select Tipo Documental
 * @param {any} data
 */
function CarregaTiposDocumentais(data) {
    $eSTipoDocumental.empty();
    if (data.length > 0) {
        data.unshift({ id: '-1', text: 'Selecione um tipo documental' });
    }
    $eSTipoDocumental.select2({
        width: '100%',
        data: data
    });
}

/**
 * Carrega mensagem de erro para o componente select Tipo Documental
 */
function ErrorTiposDocumentais() {
    $eSTipoDocumental.select2({
        width: '100%',
        language: {
            noResults: function () {
                return "Não foi possível carregar tipos documentais.";
            }
        }
    });
}

/*Inicialização do selectbox de municipio e uf com o plugin select2*/
$eSUf.select2({ width: '100%' });
$eSMunicipio.select2(
    {
        width: '100%',
        placeholder: 'Selecione um Município',
        language: {
            noResults: function () {
                return "Nenhum município encontrado.";
            }
        }
    }
);

/*Evento Change para o campo UF que dispara a consulta de municípios por UF*/
$eSUf.on('change', function (e) {
    $.get(
        "/Suporte/GetMunicipiosPorUF?uf=" + $eSUf.val(), function (data) {
            $eSMunicipio.empty();
            if (data.length > 0) {
                data.unshift({ id: 'all', text: 'Todos' });
                data.unshift({ id: '-1', text: 'Selecione um Município' });
                $eSMunicipio.select2({ width: '100%', data: data });
            }
        });
});

/*Evento select para o campo municipio que cria checkbox a partir da seleção*/
$eSMunicipio.on('select2:select', function (e) {
    var idSelecionado = $('#municipiosajax').val();
    var exist = false;

    if (idSelecionado == -1)
        return false;

    //Seleciona todos as opções ou apenas uma opção
    if (idSelecionado == 'all') {
        $.each($("#municipiosajax option"), function () {
            if ($(this).val() != 'all' && $(this).val() != -1 && !JaExiste($(this).val())) {
                IncluirSelecionado($(this).text(), $(this).val());
            }
        });
    } else {

        if (JaExiste(idSelecionado))
            return false;

        /*Cria novo checkbox*/
        IncluirSelecionado($("#municipiosajax option:selected").text(), $('#municipiosajax').val());
    }

    $('input').iCheck({
        checkboxClass: 'icheckbox_square-red',
        radioClass: 'iradio_square-red',
        increaseArea: '20%' // optional
    });

    $eSMunicipio.val(-1).trigger("change");
});

/*Evento de uncheck para os checkbox que utilizam o plugin iCheck*/
$('form').on('ifUnchecked', 'input[type="checkbox"]', function (event) {
    $('#' + this.value).remove();
});

/**
 * Inclui municipio selecionado passando como parâmetros text e val
 * @param {any} text
 * @param {any} val
 */
function IncluirSelecionado(text, val) {
    $('#grupomunicipios').append('<div class="checkbox municipio col-lg-3 col-md-4 col-xs-6" id="' + val + '"><label><input type="checkbox" checked name="MunicipiosRascunhoProcesso[' + index + '].GuidMunicipio" value="' + val + '" /> ' + text + '</label></div>');
    index++;
}

/**
 * Verifica se ID seletor existe no DOM
 * @param {any} idSelecionado
 */
function JaExiste(idSelecionado) {
    var exist = false;
    if ($('#' + idSelecionado).length > 0)
        exist = true;
    return exist;
}

/**
 * Reseta o formulário de abrangência
 */
function LimparAbrangencia() {
    $("#grupomunicipios div.municipio").remove();
    $('form#formabrangencia')[0].reset();
    $eSMunicipio.val(-1).trigger("change");
    $eSUf.val(0).trigger("change");
    index = 0;
}

/**
 * Reseta o formulário de informações básica de rascunho
 */
function LimparFormBasico() {
    $('#Atividade_Id').val($('#Atividade_Id option:first').val()).trigger('change')
    $('#GuidUnidade').index[0].trigger("change");
    $('form#formbasico')[0].reset();
}

/**
 * Reseta os campos de municipio com o plugin iCheck
 */
function ResetMunicipioLista() {
    $('#formabrangencia input').iCheck({
        checkboxClass: 'icheckbox_square-red',
        radioClass: 'iradio_square-red',
        increaseArea: '5%' // optional
    });
}

/**
 * Inicializa os campos de sinalização com o plugin iCheck
 */
function ResetSinalizacoesLista() {
    $('#formbasico input').iCheck({
        checkboxClass: 'icheckbox_square-red',
        radioClass: 'iradio_square-red',
        increaseArea: '5%' // optional
    });
}

/**
 * Upload de Anexos
 * @param {any} inputId
 */
function uploadFiles(inputId) {
    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();

    if (files.length == 0)
        return false;

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
        formData.append("idRascunho", $("#formanexo").find("#Id").val());
        if (!isNullOrEmpty($("#idTipoDocumental").val()))
            formData.append("idTipoDocumental", $("#formanexo").find("#idTipoDocumental").val());
    }

    $('#textLoad').toggleClass('disabled');
    $('#textLoad').text('Carregando...');
    $('#progressAnexo').toggleClass('hide');

    $.ajax(
        {
            url: "/rascunho/UploadAnexo",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#textLoad").text('Selecionar arquivos');
                $('#textLoad').toggleClass('disabled');
                $("#progressAnexo").toggleClass('hide');
                $("#grupoanexos").html(data)
            },
            error: function (erro) {
                console.log(erro);
                $("#progressAnexo").toggleClass('hide');
            }
        }
    );
}

/*Evento click para o botão excluir da lista de anexos*/
$("#grupoanexos").on("click", ".btnExcluirAnexo", function () {

    $(this).parent('div').find('.btn').toggleClass('disabled');
    var formData = new FormData();
    formData.append("idRascunho", $("#formanexo").find("#Id").val());
    formData.append("idAnexo", $(this).attr("data-id"));

    $.ajax(
        {
            url: "/rascunho/ExcluirAnexo",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#grupoanexos").html(data)
            }
        }
    );
});

//CARREGAR ORGANIZACOES
$("#interessadoTipo").on("change", function () {

    var formData = new FormData();
    var tipoInteressado = $(this).val();
    var url = "";
    formData.append("tipoInteressado", tipoInteressado);

    if (tipoInteressado > 0) {
        url = "/rascunho/FormInteressado";
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
    var url = "/rascunho/GetUnidadesPorOrganizacao";
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
    var url = "/rascunho/IncluirInteressadoPJ";
    var guidOrganizacao = $("#interessadoOrgao").val();
    var guidUnidade = $("#interessadoOrgaoUnidade").val();

    formData.append("idRascunho", $("#formanexo").find("#Id").val());

    switch (tipoInteressado) {
        case "1":
            if (isNullOrEmpty(guidUnidade)) {
                formData.append("guidOrganizacao", guidOrganizacao);
            }
            else {
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
                $("#interessadoUnidade").html(data)
                $('#interessadoUnidade select').select2({ width: '100%' })
            }
        }
    );

    return false;
});

//btnEditarAnexo