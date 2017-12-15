$('body').on('click', '.btn-visualizar-despacho', function () {
    var url = $(this).attr("href");

    if (isNullOrEmpty(url)) {
        return false;
    }
    else {

        $.ajax(
            {
                url: url,
                processData: false,
                contentType: false,
                type: "GET",
                success: function (data) {
                    CarregaModelDetalhe('Detalhes do Despacho', data);
                },
                error: function () {
                    toastr['error']('Não foi possível obter o despacho. Atualize a página e tente novamente.');
                }
            }
        );
    }
    return false;
});