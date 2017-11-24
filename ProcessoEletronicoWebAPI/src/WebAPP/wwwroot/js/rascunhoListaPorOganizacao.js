$('body').on('click', '.btnExcluirRascunho', function () {
    var id = $(this).attr('data-id');
    var url = "/Rascunho/Excluir/" + id;
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

    var url = $(this).attr('data-acaoconfirmar');

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