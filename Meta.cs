using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CadastroDeMetas
{
    public class Meta
    {
        public int Id { get; set; }
        public string Vendedor { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Periodicidade { get; set; } = string.Empty;
        public string Produto { get; set; } = string.Empty;
        public bool Ativa { get; set; }
        public int NumeroVinculados { get; set; }
        public string TipoVinculados { get; set; } = string.Empty;
        public string StatusVinculados { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public string ValorFormatado => $"R$ {Valor:N2}";

        public override string ToString()
        {
            return $"{Vendedor} - R$ {Valor:N2}";
        }
    }
}
