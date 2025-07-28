using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroDeMetas
{
    public partial class FormCadastroMeta : Form
    {
        private TextBox txtNomeVendedor = new TextBox();
        private TextBox txtValorMeta = new TextBox();
        private ComboBox cmbTipoMeta = new ComboBox();
        private ComboBox cmbPeriodicidade = new ComboBox();
        private ComboBox cmbProduto = new ComboBox();
        private CheckBox chkAtivo = new CheckBox();
        private Button btnSalvar = new Button();
        private Button btnVoltar = new Button();
        private Button btnExcluir = new Button();
        private Meta meta = new Meta();
        private bool modoEdicao = false;
        private int idVendedorSendoEditado = 0; 
        private bool isEdicao = false;

        // Método para resetar a cor dos campos para o normal
        private void ResetarCoresCampos()
        {
            txtNomeVendedor.BackColor = corCampoNormal;
            txtValorMeta.BackColor = corCampoNormal;
        }

        // Método para validar campos obrigatórios
        private bool ValidarCamposObrigatorios()
        {
            bool todosPreenchidos = true;

            // Resetar cores primeiro
            ResetarCoresCampos();

            // Validar Nome do Vendedor
            if (string.IsNullOrWhiteSpace(txtNomeVendedor.Text))
            {
                txtNomeVendedor.BackColor = corCampoErro;
                todosPreenchidos = false;
            }

            // Validar Valor da Meta (campo preenchido E valor válido)
            if (!ValidarValorMeta())
            {
                txtValorMeta.BackColor = corCampoErro;
                todosPreenchidos = false;
            }

            return todosPreenchidos;
        }

        // Evento TextChanged para resetar cor do nome do vendedor
        private void TxtNomeVendedor_TextChanged(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNomeVendedor.Text))
            {
                txtNomeVendedor.BackColor = corCampoNormal;
            }
        }

        private readonly Color corCampoNormal = Color.White;
        private readonly Color corCampoErro = Color.FromArgb(252, 199, 194);

        public Meta Meta { get { return meta; } }

        public FormCadastroMeta()
        {
            meta = new Meta { Ativa = true };
            isEdicao = false;

            InitializeComponent();

            this.Text = "Nova Meta";

            AdicionarEventos();
        }

        public FormCadastroMeta(Meta metaExistente)
        {
            meta = metaExistente;
            isEdicao = true;

            InitializeComponent();

            this.Text = "Editar Meta";

            AdicionarEventos();

            CarregarDados();
        }

        private void AdicionarEventos()
        {
            // Eventos que não são configurados no Designer
            txtNomeVendedor.TextChanged += TxtNomeVendedor_TextChanged;
            txtValorMeta.TextChanged += TxtValorMeta_TextChanged;
            txtValorMeta.KeyPress += TxtValorMeta_KeyPress;
            txtValorMeta.KeyDown += TxtValorMeta_KeyDown;
            txtValorMeta.Leave += TxtValorMeta_Leave;
        }

        private void CarregarDados()
        {
            txtNomeVendedor.Text = meta.Vendedor;
            txtValorMeta.Text = meta.Valor.ToString("F2");
            chkAtivo.Checked = meta.Ativa;
            cmbTipoMeta.SelectedItem = meta.Tipo;
            cmbPeriodicidade.SelectedItem = meta.Periodicidade;
            cmbProduto.SelectedItem = meta.Produto;

            if (cmbTipoMeta.Items.Contains(meta.Tipo))
                cmbTipoMeta.SelectedItem = meta.Tipo;

            if (cmbPeriodicidade.Items.Contains(meta.Periodicidade))
                cmbPeriodicidade.SelectedItem = meta.Periodicidade;

            if (cmbProduto.Items.Contains(meta.Produto))
                cmbProduto.SelectedItem = meta.Produto;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNomeVendedor.Text))
            {
                MessageBox.Show("O nome do vendedor é obrigatório.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNomeVendedor.Focus();
                return false;
            }

            if (!decimal.TryParse(txtValorMeta.Text, out decimal valor) || valor <= 0)
            {
                MessageBox.Show("Digite um valor válido para a meta.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValorMeta.Focus();
                return false;
            }

            if (cmbTipoMeta.SelectedItem == null)
            {
                MessageBox.Show("Selecione o tipo de meta.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoMeta.Focus();
                return false;
            }

            if (cmbPeriodicidade.SelectedItem == null)
            {
                MessageBox.Show("Selecione a periodicidade.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPeriodicidade.Focus();
                return false;
            }

            if (cmbProduto.SelectedItem == null)
            {
                MessageBox.Show("Selecione o produto.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProduto.Focus();
                return false;
            }

            return true;
        }

        private void BtnSalvar_Click(object? sender, EventArgs e)
        {
            // Primeiro valida os campos obrigatórios
            if (!ValidarCamposObrigatorios())
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios (destacados em vermelho).",
                               "Campos Obrigatórios", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Foca no primeiro campo vazio
                if (string.IsNullOrWhiteSpace(txtNomeVendedor.Text))
                {
                    txtNomeVendedor.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtValorMeta.Text))
                {
                    txtValorMeta.Focus();
                }
                return;
            }

            // Verificar se o vendedor já existe
            int? idAtual = null;
            if (modoEdicao)
            {
                idAtual = idVendedorSendoEditado;
            }
 
            if (!ValidarCampos()) return;

            try
            {
                meta.Vendedor = txtNomeVendedor.Text.Trim();
                meta.Valor = decimal.Parse(txtValorMeta.Text);
                meta.Tipo = cmbTipoMeta.SelectedItem?.ToString() ?? string.Empty;
                meta.Periodicidade = cmbPeriodicidade.SelectedItem?.ToString() ?? string.Empty;
                meta.Produto = cmbProduto.SelectedItem?.ToString() ?? string.Empty;
                meta.Ativa = chkAtivo.Checked;

                string acao = isEdicao ? "atualizada" : "salva";
                MessageBox.Show($"Meta {acao} com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtValorMeta_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Permite apenas números, vírgula, ponto, backspace e delete
            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != ',' &&
                e.KeyChar != '.' &&
                e.KeyChar != (char)Keys.Back &&
                e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true; // Bloqueia a tecla
                return;
            }

            // Permite apenas um separador decimal (vírgula OU ponto)
            if ((e.KeyChar == ',' || e.KeyChar == '.') && txtValorMeta.Text.Length == 0)
            {
                e.Handled = true; // Bloqueia se já tiver separador decimal
                return;
            }

            // Não permite separador decimal no início
            if ((e.KeyChar == ',' || e.KeyChar == '.') && txtValorMeta.Text.Length == 0)
            {
                e.Handled = true;
                return;
            }
        }

        private void TxtValorMeta_KeyDown(object? sender, KeyEventArgs e)
        {
            // Permite Ctrl+A (Selecionar Tudo)
            if (e.Control && e.KeyCode == Keys.A)
            {
                txtValorMeta.SelectAll();
                e.Handled = true;
            }

            // Permite Ctrl+C (Copiar)
            if (e.Control && e.KeyCode == Keys.C)
            {
                e.Handled = false;
            }

            // Permite Ctrl+V (Colar) - mas vamos validar o conteúdo
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.Handled = true; // Bloqueia o Ctrl+V padrão
                ColarApenasNumeros();
            }

            // Permite setas de navegação
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Home || e.KeyCode == Keys.End)
            {
                e.Handled = false;
            }
        }

        // Método para colar apenas números
        private void ColarApenasNumeros()
        {
            try
            {
                if (Clipboard.ContainsText())
                {
                    string textoColado = Clipboard.GetText();
                    string textoLimpo = "";
                    bool jaTemSeparador = txtValorMeta.Text.Contains(",") || txtValorMeta.Text.Contains(".");

                    foreach (char c in textoColado)
                    {
                        if (char.IsDigit(c))
                        {
                            textoLimpo += c;
                        }
                        else if ((c == ',' || c == '.') && !jaTemSeparador && textoLimpo.Length > 0)
                        {
                            textoLimpo += c;
                            jaTemSeparador = true;
                        }
                    }

                    if (!string.IsNullOrEmpty(textoLimpo))
                    {
                        // Inserir na posição do cursor
                        int posicaoCursor = txtValorMeta.SelectionStart;
                        string textoAtual = txtValorMeta.Text;

                        txtValorMeta.Text = textoAtual.Insert(posicaoCursor, textoLimpo);
                        txtValorMeta.SelectionStart = posicaoCursor + textoLimpo.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtValorMeta_Leave(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtValorMeta.Text))
            {
                // Remove espaços
                string valor = txtValorMeta.Text.Trim();

                // Tenta converter para decimal para validar
                if (decimal.TryParse(valor, out decimal valorDecimal))
                {

                }
                else
                {
                    MessageBox.Show("Valor inválido! Por favor, digite apenas números.",
                                   "Valor Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtValorMeta.Focus();
                    txtValorMeta.SelectAll();
                }
            }
        }

        private bool ValidarValorMeta()
        {
            if (string.IsNullOrWhiteSpace(txtValorMeta.Text))
            {
                return false;
            }

            // Padroniza o separador decimal para vírgula
            string valor = txtValorMeta.Text.Replace(".", ",");

            if (!decimal.TryParse(valor, out decimal valorDecimal))
            {
                return false;
            }

            if (valorDecimal <= 0)
            {
                return false;
            }

            return true;
        }

        // Evento TextChanged atualizado para resetar cor
        private void TxtValorMeta_TextChanged(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtValorMeta.Text))
            {
                txtValorMeta.BackColor = corCampoNormal;
            }
        }

        private void BtnVoltar_Click(object? sender, EventArgs e)
        {
            if (TemAlteracoesPendentes())
            {
                DialogResult resultado = MessageBox.Show(
                    "Existem alterações não salvas. Deseja realmente sair?",
                    "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                    return;
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnExcluir_Click(object? sender, EventArgs e)
        {
            if (!isEdicao)
            {
                MessageBox.Show("Não é possível excluir uma meta que ainda não foi salva.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult resultado = MessageBox.Show($"Tem certeza que deseja excluir a meta do vendedor '{meta.Vendedor}' ?\n\n" +
                "Esta ação não pode ser desfeita.",
                "Confirmação de Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);// Botão "Não" como padrão

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    meta.Ativa = false;

                    MessageBox.Show("Meta excluída com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir: " + ex.Message, "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FormCadastroMeta_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F10:
                    BtnSalvar_Click(this, EventArgs.Empty); // Pass 'this' and 'EventArgs.Empty' instead of null
                    break;
                case Keys.Escape:
                    BtnVoltar_Click(this, EventArgs.Empty); // Pass 'this' and 'EventArgs.Empty' instead of null
                    break;
            }
        }

        // MÉTODO AUXILIAR: Verificar se existem alterações pendentes
        private bool TemAlteracoesPendentes()
        {
            if (!isEdicao) // Se é uma nova meta
            {
                return !string.IsNullOrWhiteSpace(txtNomeVendedor.Text) ||
                       !string.IsNullOrWhiteSpace(txtValorMeta.Text) ||
                       cmbTipoMeta.SelectedItem != null ||
                       cmbPeriodicidade.SelectedItem != null ||
                       cmbProduto.SelectedItem != null;
            }
            else // Se está editando
            {
                return txtNomeVendedor.Text.Trim() != meta.Vendedor ||
                       txtValorMeta.Text != meta.Valor.ToString("F2") ||
                       cmbTipoMeta.SelectedItem?.ToString() != meta.Tipo ||
                       cmbPeriodicidade.SelectedItem?.ToString() != meta.Periodicidade ||
                       cmbProduto.SelectedItem?.ToString() != meta.Produto ||
                       chkAtivo.Checked != meta.Ativa;
            }
        }

        // MÉTODO AUXILIAR: Limpar formulário (se necessário)
        private void LimparFormulario()
        {
            txtNomeVendedor.Clear();
            txtValorMeta.Clear();
            cmbTipoMeta.SelectedIndex = -1;
            cmbPeriodicidade.SelectedIndex = -1;
            cmbProduto.SelectedIndex = -1;
            chkAtivo.Checked = true;
            txtNomeVendedor.Focus();

            ResetarCoresCampos();
        }

        // SOBRESCRITA: Evento de fechamento do formulário
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                if (TemAlteracoesPendentes())
                {
                    DialogResult resultado = MessageBox.Show(
                        "Existem alterações não salvas. Deseja realmente sair?",
                        "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            base.OnFormClosing(e);
        }
    }
}
           
