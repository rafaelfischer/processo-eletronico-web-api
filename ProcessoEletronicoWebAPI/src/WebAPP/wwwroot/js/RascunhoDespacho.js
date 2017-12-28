/*Variaveis globais*/
var $dataId;

/*Incialização da página*/
$(document).ready(function () {        
    $('#form-dados-basicos select').select2({ width: '100%' });    
});

function InicializacaoComponentes() {
    $('#form-dados-basicos select').select2({ width: '100%' });    
    CKEDITOR.replace('Texto');
}

//CARREGAR UNIDADES POR ORGANIZACAO
$("body").on("select2:select", "#GuidOrganizacaoDestino", function () {

    var formData = new FormData();
    var orgao = $(this).val();
    var url = "/Suporte/GetUnidadesPorOrganizacao";

    if (isNullOrEmpty(orgao) || orgao == 0) {
        $('#GuidUnidadeDestino').children().remove();
        var data = [{ id: '0', text: 'Selecione uma unidade' }];
        $('#GuidUnidadeDestino').select2({ width: '100%', data: data });
        return false;
    }   

    $.get(url, { guidOrganizacao: orgao })
        .done(function (data) {
            if (data.length > 0) {                
                $('#GuidUnidadeDestino').children().remove();
                data.unshift({ id: '0', text: 'Selecione uma unidade' });
                $('#GuidUnidadeDestino').select2({ width: '100%', data: data });
            }            
        });
});


/********************************************************************************************************************************/
/*ANEXOS DE RASCUNHO DE DESPACHO*/


/*Evento Change Files. Lista arquivos selecionados.*/
$('body').on('change', '#files', function () {
    var local = $('#arquivosselecionados');

    LimpaArquivosSelecionados();
    local.append('<table class="table table-condensed table-bordered table-striped table-hover"><thead><tr><th>Arquivos Selecionados</th></tr></thead><tbody></tbody></table>')

    $.each($(this)[0].files, function (i) {
        local.find('.table tbody').append('<tr><td>' + this.name + '</td></tr>');
    });

});

/**
 * /Limpar lista de arquivos Selecionados
 */
function LimpaArquivosSelecionados() {
    var local = $('#arquivosselecionados');
    local.find('.table').remove();
}

/**
 * /Limpar formulário de anexos
 */
function LimpaFormAnexos() {
    $('#formanexo')[0].reset();
    LimpaArquivosSelecionados();    
}

/*Evento click botão salvar anexos*/
$('body').on('click', '#btnSalvarAnexo', function () {
    uploadFiles('files');
});

/**
 * Upload de Anexos
 * @param {any} inputId
 */
function uploadFiles(campo) {
    var inputFiles = document.getElementById(campo);
    var files = inputFiles.files;
    var formData = new FormData();

    if (files.length == 0)
        return false;

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
        formData.append("idRascunhoDespacho", $("#formanexo").find("#Id").val());
        formData.append("descricaoAnexos", $("#formanexo").find("#DescricaoAnexos").val());

        if (!isNullOrEmpty($("#idTipoDocumental").val()) && $("#idTipoDocumental").val() > 0)
            formData.append("idTipoDocumental", $("#formanexo").find("#idTipoDocumental").val());
    }

    $('#textLoad').toggleClass('disabled');
    $('#textLoad').text('Carregando...');
    $('#progressAnexo').toggleClass('hide');

    $.ajax(
        {
            url: "/RascunhoAnexoDespacho/UploadAnexo",
            data: formData,
            crossDomain: true,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (dados) {
                AtualizaFormAnexo(inputFiles);
                LimpaArquivosSelecionados();
                $("#listaanexos").html(dados);
                LimpaFormAnexos();
            },
            error: function (erro) {
                AtualizaFormAnexo(files);
                LimpaArquivosSelecionados();
            }
        }
    );
}

function AtualizaFormAnexo(inputFiles) {
    $("#textLoad").text('Selecionar arquivos');
    $('#textLoad').toggleClass('disabled');
    $("#progressAnexo").toggleClass('hide');
    $(inputFiles).val('');

}

/*Evento click para o botão excluir da lista de anexos*/
$("#listaanexos").on("click", ".btnExcluirAnexo", function () {

    var nome = $(this).attr("data-nome");
    var id = $(this).attr("data-id");

    carregaModalDefault(
        "Excluir Anexo",
        "Deseja confirmar a exclusão do anexo <strong>" + nome + "</strong>?",
        "Excluir",
        "Cancelar",
        "btnConfirmarExclusaoAnexo",
        "btnCancelarExclusao",
        id);
});

/*confirmar Exclusão de anexo*/
$('body').on('click', 'button[data-btn="btnConfirmarExclusaoAnexo"]', function () {

    var id = $(this).attr('data-acaoconfirmar');
    var url = "/RascunhoAnexoDespacho/Delete";

    var formData = new FormData();
    formData.append("idRascunhoDespacho", $("#formanexo").find("#Id").val());
    formData.append("id", id);

    if (isNullOrEmpty(id)) {
        return false;
    }
    else {

        $.ajax(
            {
                url: url,
                data: formData,
                processData: false,
                contentType: false,
                type: "POST",
                success: function (data) {
                    $("#listaanexos").html(data);
                }
            }
        );
    }
});


/*Evento click para o botão editar da lista de anexos*/
$("body").on("click", ".btnEditarAnexos", function () {

    var formData = new FormData();

    formData.append("idRascunhoDespacho", $("#formanexo").find("#Id").val());    

    $.ajax(
        {
            url: "/RascunhoAnexoDespacho/EditList",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $('#listaanexos').html(data);
                //$(this).parent('tr td:nth-child(2)').html(data);
            }
        }
    );
});

/***************************************************************************************************************************/
/*EXCLUSAO DE RASCUNHO DE DESPACHO*/

$('body').on('submit', '#formExcluirRascunhoDespachoAjax', function () {
    var id = $(this).find('#Id').val();
    var url = "/RascunhoDespacho/Delete/" + id;

    $.ajax(
        {
            url: url,
            processData: false,
            contentType: false,
            type: "GET"
        }
    );
});

/*Exclusão na tela de edição de rascunho de despacho*/
$('body').on('click', '.btnExcluirRascunhoDespachoForm', function () {
    var id = $(this).attr('data-id');
    var url = $(this).attr('href');

    carregaModalDefault(
        "Excluir Rascunho de Despacho",
        "Deseja realmente excluir o rascunho de despacho ID " + id + "?",
        "Excluir",
        "Cancelar",
        "btnConfirmarExclusaoForm",
        "btnCancelarExclusao",
        url);

    return false;
});

$('body').on('click', 'button[data-btn="btnConfirmarExclusaoForm"]', function () {

    var url = $(this).attr('data-acaoconfirmar');

    if (isNullOrEmpty(url)) {
        return false;
    }
    else {
        window.location.assign(url);
    }
});


/*Exclusão na tela de consulta de rascunhos de despachos*/
$('body').on('click', '.btnExcluirRascunhoDespacho', function () {
    var id = $(this).attr('data-id');
    var url = $(this).attr('href');

    carregaModalDefault(
        "Excluir Rascunho de Despacho",
        "Deseja realmente excluir o rascunho de despacho ID " + id + "?",
        "Excluir",
        "Cancelar",
        "btnConfirmarExclusao",
        "btnCancelarExclusao",
        url);

    return false;
});

$('body').on('click', 'button[data-btn="btnConfirmarExclusao"]', function () {

    var url = $(this).attr('data-acaoconfirmar') + "?ajax=true";

    if (isNullOrEmpty(url)) {
        return false;
    }
    else {
        $.ajax(
            {
                url: url,
                processData: false,
                contentType: false,
                type: "POST",
                success: function (data) {
                    $("#listaRascunhosDespacho").html(data)
                },
                complete: function () {
                    DatatablePlugin($('#table-rascunhos-despacho'));
                }
            }
        );
    }
});

/*Retorno Despacho*/

function RetornoDespacho(data) {
    var local = $('#opcoes-carregamento');

    local.siblings('.alert').remove();

    if (data.mensagens!=null) {        
        $.each(data.mensagens, function (i, v) {
            local.before('<div class="alert alert-' + v.tipoToastr + '">' + v.texto + '</div>')
        });

        if (data.success) {
            local.remove();
        }
    }
}

$('body').on('blur', '#form-dados-basicos input, #form-dados-basicos select, #form-dados-basicos textarea ', function () {

});

$("body").on("select2:select", "#form-dados-basicos select", function () {
    $('#form-dados-basicos').submit();
});