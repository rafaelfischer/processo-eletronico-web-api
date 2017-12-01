/******ANEXOS*******/

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

/*Carrega tipos documentais com base no idAtividade */
function getTiposDocumentais() {
    $idAtividade = $('#Atividade_Id').val();
    $.get(
        "/Suporte/GetTiposDocumentais?idAtividade=" + $idAtividade, function (data) {
            $eSTipoDocumental.empty();
            if (data.length > 0) {
                data.unshift({ id: '0', text: 'Selecione um tipo documental' });
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
        data.unshift({ id: '0', text: 'Selecione um tipo documental' });
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
    $eSTipoDocumental.val('0').trigger("change");
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
        formData.append("idRascunho", $("#formanexo").find("#Id").val());
        formData.append("descricaoAnexos", $("#formanexo").find("#DescricaoAnexos").val());
        
        if (!isNullOrEmpty($("#idTipoDocumental").val()) && $("#idTipoDocumental").val() > 0)
            formData.append("idTipoDocumental", $("#formanexo").find("#idTipoDocumental").val());
    }

    $('#textLoad').toggleClass('disabled');
    $('#textLoad').text('Carregando...');
    $('#progressAnexo').toggleClass('hide');

    $.ajax(
        {
            url: "/RascunhoAnexo/UploadAnexo",            
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
    var url = "/RascunhoAnexo/ExcluirAnexo";

    var formData = new FormData();
    formData.append("idRascunho", $("#formanexo").find("#Id").val());
    formData.append("idAnexo", id);    

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