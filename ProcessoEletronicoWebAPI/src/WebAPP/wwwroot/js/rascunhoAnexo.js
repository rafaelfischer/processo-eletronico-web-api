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
        if (!isNullOrEmpty($("#idTipoDocumental").val()))
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
                $("#grupoanexos").html(dados)
            },
            error: function (erro) {
                AtualizaFormAnexo(files);
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
$("#grupoanexos").on("click", ".btnExcluirAnexo", function () {

    $(this).parent('div').find('.btn').toggleClass('disabled');
    var formData = new FormData();
    formData.append("idRascunho", $("#formanexo").find("#Id").val());
    formData.append("idAnexo", $(this).attr("data-id"));

    $.ajax(
        {
            url: "/RascunhoAnexo/ExcluirAnexo",
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