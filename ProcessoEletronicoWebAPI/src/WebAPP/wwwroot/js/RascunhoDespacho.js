/*Incialização da página*/
$(document).ready(function () {
    $('#form-dados-basicos select').select2({ width: '100%' });
});

//CARREGAR UNIDADES POR ORGANIZACAO
$("#form-dados-basicos").on("select2:select", "#GuidOrganizacaoDestino", function () {

    var formData = new FormData();
    var orgao = $(this).val();
    var url = "/Suporte/GetUnidadesPorOrganizacao";

    $.get(url, { guidOrganizacao: orgao })
        .done(function (data) {
            if (data.length > 0) {                
                data.unshift({ id: '0', text: 'Selecione uma unidade' });
                $('#GuidUnidadeDestino').select2({ width: '100%', data: data })
            }            
        });
});


/********************************************************************************************************************************/
/*ANEXOS DE RASCUNHO DE DESPACHO*/


/*Evento Change Files. Lista arquivos selecionados.*/
$('#formanexo').on('change', '#files', function () {
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
$('#formanexo').on('click', '#btnSalvarAnexo', function () {
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


/*Evento click para o botão excluir da lista de anexos*/
$("#listaanexos").on("click", ".btnEditarAnexos", function () {

    var formData = new FormData();

    formData.append("idRascunho", $("#formanexo").find("#Id").val());
    formData.append("idAtividade", $('#Atividade_Id').val());

    $.ajax(
        {
            url: "/RascunhoAnexo/EditarAnexosForm",
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

/*Evento click para o botão excluir da lista de anexos*/
$("#listaanexos").on("click", ".btnEditarAnexo", function () {

    var elemento = $(this);
    //$(this).parent('div').find('.btn').toggleClass('disabled');
    var formData = new FormData();

    formData.append("idRascunho", $("#formanexo").find("#Id").val());
    formData.append("idAnexo", $(this).attr("data-id"));
    formData.append("idAtividade", $('#Atividade_Id').val());

    $.ajax(
        {
            url: "/RascunhoAnexo/EditarAnexosForm",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                elemento.parents('tr').find('td:nth-child(2)').html(data);
                //$(this).parent('tr td:nth-child(2)').html(data);
            }
        }
    );
});