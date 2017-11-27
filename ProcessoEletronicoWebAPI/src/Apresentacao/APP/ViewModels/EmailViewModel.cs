using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class EmailViewModel
    {
        public int Id { get; set; }
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Endereco { get; set; }
    }
}
