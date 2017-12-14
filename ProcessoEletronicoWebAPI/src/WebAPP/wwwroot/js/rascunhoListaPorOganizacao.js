$('body').on('click', '.btnExcluirRascunho', function () {
    var id = $(this).attr('data-id');
    var url = "/Rascunho/Delete/" + id;
    carregaModalDefault(
        "Excluir Rascunho de Processo",
        "Deseja realmente excluir o rascunho ID "+id+"?",
        "Excluir",
        "Cancelar",
        "btnConfirmarExclusao",
        "btnCancelarExclusao",
        url);
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
                    $("#listaRascunhos").html(data)
                }
            }
        );
    }
});


/*Exclusão na tela de edição de rascunho de despacho*/
$('body').on('click', '.btnExcluirRascunhoForm', function () {
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