using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroDeMetas
{
    public partial class FormPrincipal : Form
    {
        private List<Meta> metas = new List<Meta>();
        private List<Meta> metasFiltradas = new List<Meta>();
        private BindingSource bindingSource = new BindingSource();
        private DataGridView dataGridView = new DataGridView();
        private Button btnNovaMeta = new Button();
        private Button btnEditarMeta = new Button();
        private Button btnDuplicarMeta = new Button();
        private Button btnExcluirMeta = new Button();
        private Button btnVoltar = new Button();
        private Label lblTitulo = new Label();
        private Label lblFiltro = new Label();  
        private DataGridView dgvMetas = new DataGridView();
        private CheckBox chkFiltrarInativas = new CheckBox();
        private TextBox txtFiltro = new TextBox();
        private Button btnBuscar = new Button();
        private Label lblContador = new Label();
        private Panel panelTop = new Panel();
        private Panel panelBottom = new Panel();
        private int proximoId = 1;
        private string colunaOrdenacaoAtual = "";
        private bool ordemCrescente = true;
        private Meta? ultimaMetaAlterada = null;
        private bool ultimoStatusAnterior = false;
        private System.Windows.Forms.Timer? timerDesfazer = null;

        public FormPrincipal()
        {
            InitializeComponent();

            metas = new List<Meta>();
            metasFiltradas = new List<Meta>();
            bindingSource = new BindingSource();

            InicializarSistemaDesfazer();

            CarregarDadosExemplo();
        }

        private void CarregarDadosExemplo()
        {
            metas.Add(new Meta
            {
                Id = proximoId++,
                Vendedor = "João Silva",
                Valor = 500,
                Tipo = "Unidades de Produto (UN)",
                Periodicidade = "Semanal",
                Produto = "Barris",
                Ativa = true,
            });

            metas.Add(new Meta
            {
                Id = proximoId++,
                Vendedor = "Maria Santos",
                Valor = 75000,
                Tipo = "Monetário (R$)",
                Periodicidade = "Mensal",
                Produto = "Acessórios e Produtos",
                Ativa = true,
            });

            metas.Add(new Meta
            {
                Id = proximoId++,
                Vendedor = "Larissa Silva",
                Valor = 1000,
                Tipo = "Litros (L)",
                Periodicidade = "Diária",
                Produto = "Garrafas e Latas",
                Ativa = false,
            });

            AtualizarLista();
        }

        private void AtualizarLista()
        {
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            string filtroNome = txtFiltro?.Text?.Trim().ToLower() ?? "";
            bool filtrarInativas = chkFiltrarInativas.Checked;

            metasFiltradas = metas
                .Where(m => (string.IsNullOrEmpty(filtroNome) || m.Vendedor?.ToLower().Contains(filtroNome) == true) &&
                            (!filtrarInativas || m.Ativa))
                .ToList();

            dgvMetas.DataSource = null;
            dgvMetas.DataSource = metasFiltradas;

            AtualizarRodape();
        }

        private void AtualizarRodape()
        {
            if (lblContador != null)
            {
                int total = metasFiltradas.Count;
                int ativas = metasFiltradas.Count(m => m.Ativa);
                int inativas = metasFiltradas.Count(m => !m.Ativa);

                lblContador.Text = $"{total} metas encontradas ({ativas} ativas, {inativas} inativas)";
                lblContador.ForeColor = SystemColors.ControlText; // Volta à cor padrão
            }
        }

        private void TxtFiltro(object sender, EventArgs e)
        {
            {
                AplicarFiltros();
                
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void ChkFiltrarInativas_CheckedChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void DataGridView_DoubleClick(object sender, EventArgs e)
        {
            BtnEditarMeta_Click(sender, e);
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvMetas.Columns.Contains("Acoes"))
            {
                if (e.ColumnIndex == dgvMetas.Columns["Acoes"].Index)
                {
                    BtnExcluirMeta_Click(sender, e);
                }
            }
        }

        // Evento de clique no cabeçalho da coluna
        private void DataGridViewMetas_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (sender is not DataGridView dgv) return;

            string nomeColuna = dgv.Columns[e.ColumnIndex].Name;

            // Se clicou na mesma coluna, inverte a ordem
            if (colunaOrdenacaoAtual == nomeColuna)
            {
                ordemCrescente = !ordemCrescente;
            }
            else
            {
                // Nova coluna, sempre começa crescente
                colunaOrdenacaoAtual = nomeColuna;
                ordemCrescente = true;
            }

            // Aplica a ordenação
            OrdenarDataGridView(dgv, nomeColuna, ordemCrescente);

            // Atualiza o indicador visual no cabeçalho
            AtualizarIndicadorOrdenacao(dgv, e.ColumnIndex, ordemCrescente);
        }

        // Método para ordenar o DataGridView
        private void OrdenarDataGridView(DataGridView dgv, string nomeColuna, bool crescente)
        {
            try
            {
                ListSortDirection direcao = crescente ? ListSortDirection.Ascending : ListSortDirection.Descending;

                // Ordenação específica por coluna
                switch (nomeColuna.ToLower())
                {
                    case "vendedor":
                    case "nomevendedor":
                        dgv.Sort(dgv.Columns[nomeColuna], direcao);
                        break;

                    case "valor":
                    case "valormeta":
                        OrdenarPorValor(dgv, nomeColuna, crescente);
                        break;

                    case "tipo":
                    case "tipometa":
                        dgv.Sort(dgv.Columns[nomeColuna], direcao);
                        break;

                    case "periodicidade":
                        OrdenarPorPeriodicidade(dgv, nomeColuna, crescente);
                        break;

                    case "produto":
                        dgv.Sort(dgv.Columns[nomeColuna], direcao);
                        break;

                    case "status":
                        OrdenarPorStatus(dgv, nomeColuna, crescente);
                        break;

                    default:
                        dgv.Sort(dgv.Columns[nomeColuna], direcao);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao ordenar: {ex.Message}", "Erro",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ordenação específica para valores monetários
          private void OrdenarPorValor(DataGridView dgv, string nomeColuna, bool crescente)
        {
            var linhasOrdenadas = dgv.Rows.Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .OrderBy(r =>
                {
                    string valorTexto = r.Cells[nomeColuna].Value?.ToString() ?? "0";
                    // Remove R$, espaços e converte para decimal
                    valorTexto = valorTexto.Replace("R$", "").Replace(" ", "").Replace(".", "").Replace(",", ".");

                    if (decimal.TryParse(valorTexto, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out decimal valor))
                    {
                        return valor;
                    }
                    return 0m;
                })
                .ToArray();

            if (!crescente)
                linhasOrdenadas = linhasOrdenadas.Reverse().ToArray();

            ReorganizarLinhas(dgv, linhasOrdenadas);
        }

        // Ordenação específica para periodicidade (Diária, Semanal, Mensal)
        private void OrdenarPorPeriodicidade(DataGridView dgv, string nomeColuna, bool crescente)
        {
            var ordemPeriodicidade = new Dictionary<string, int>
            {
                { "DIÁRIA", 1 },
                { "SEMANAL", 2 },
                { "MENSAL", 3 }
            };

            var linhasOrdenadas = dgv.Rows.Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .OrderBy(r =>
                {
                    string periodicidade = r.Cells[nomeColuna].Value?.ToString()?.ToUpper() ?? "";
                    return ordemPeriodicidade.ContainsKey(periodicidade) ? ordemPeriodicidade[periodicidade] : 999;
                })
                .ToArray();

            if (!crescente)
                linhasOrdenadas = linhasOrdenadas.Reverse().ToArray();

            ReorganizarLinhas(dgv, linhasOrdenadas);
        }

        // Ordenação específica para status (Ativo primeiro, depois Inativo)
        private void OrdenarPorStatus(DataGridView dgv, string nomeColuna, bool crescente)
        {
            var linhasOrdenadas = dgv.Rows.Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .OrderBy(r =>
                {
                    // Assumindo que é um checkbox ou texto "Ativo"/"Inativo"
                    var celula = r.Cells[nomeColuna];

                    if (celula is DataGridViewCheckBoxCell checkbox)
                    {
                        bool ativo = Convert.ToBoolean(checkbox.Value ?? false);
                        return crescente ? (ativo ? 0 : 1) : (ativo ? 1 : 0);
                    }
                    else
                    {
                        string status = celula.Value?.ToString()?.ToUpper() ?? "";
                        if (status.Contains("ATIVO") || status == "✓")
                            return crescente ? 0 : 1;
                        else
                            return crescente ? 1 : 0;
                    }
                })
                .ToArray();

            ReorganizarLinhas(dgv, linhasOrdenadas);
        }

        // Método para reorganizar as linhas no DataGridView
        private void ReorganizarLinhas(DataGridView dgv, DataGridViewRow[] linhasOrdenadas)
        {
            dgv.Rows.Clear();
            foreach (var linha in linhasOrdenadas)
            {
                dgv.Rows.Add(linha);
            }
        }

        // Atualiza o indicador visual de ordenação no cabeçalho
        private void AtualizarIndicadorOrdenacao(DataGridView dgv, int colunaIndex, bool crescente)
        {
            // Remove indicadores de todas as colunas
            foreach (DataGridViewColumn coluna in dgv.Columns)
            {
                string textoOriginal = coluna.HeaderText.Replace(" ▲", "").Replace(" ▼", "");
                coluna.HeaderText = textoOriginal;
            }

            // Adiciona indicador na coluna atual
            string indicador = crescente ? " ▲" : " ▼";
            dgv.Columns[colunaIndex].HeaderText += indicador;
        }

        // Método para configurar o DataGridView 
        private void ConfigurarDataGridView()
        {
            // Habilita a ordenação por clique no cabeçalho
            foreach (DataGridViewColumn coluna in dgvMetas.Columns)
            {
                coluna.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            // Adiciona o evento de clique no cabeçalho
            dgvMetas.ColumnHeaderMouseClick += DataGridViewMetas_ColumnHeaderMouseClick;

            // Configura o cursor para indicar que é clicável
            dgvMetas.EnableHeadersVisualStyles = false;
            dgvMetas.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvMetas.ColumnHeadersDefaultCellStyle.BackColor;
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            AtualizarLista();
        }

        // Método para resetar a ordenação
        private void ResetarOrdenacao()
        {
            colunaOrdenacaoAtual = "";
            ordemCrescente = true;

            // Remove indicadores visuais
            foreach (DataGridViewColumn coluna in dgvMetas.Columns)
            {
                string textoOriginal = coluna.HeaderText.Replace(" ▲", "").Replace(" ▼", "");
                coluna.HeaderText = textoOriginal;
            }

            // Recarrega os dados na ordem original
            AtualizarLista();
        }

        private void BtnNovaMeta_Click(object sender, EventArgs e)
        {
            using (FormCadastroMeta formCadastro = new FormCadastroMeta())
            {
                if (formCadastro.ShowDialog() == DialogResult.OK)
                {
                    Meta novaMeta = formCadastro.Meta;
                    novaMeta.Id = proximoId++;
                    metas.Add(novaMeta);
                    AtualizarLista();

                    MessageBox.Show($"Meta para '{novaMeta.Vendedor}' criada com sucesso!",
                        "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnEditarMeta_Click(object sender, EventArgs e)
        {
            if (dgvMetas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma meta para editar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Meta metaSelecionada = (Meta)dgvMetas.SelectedRows[0].DataBoundItem;
            using (FormCadastroMeta formCadastro = new FormCadastroMeta(metaSelecionada))
            {
                if (formCadastro.ShowDialog() == DialogResult.OK)
                {
                    AtualizarLista();
                }
            }
        }

        private void BtnDuplicarMeta_Click(object sender, EventArgs e)
        {
            if (dgvMetas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma meta para duplicar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Meta metaOriginal = (Meta)dgvMetas.SelectedRows[0].DataBoundItem;
            Meta metaDuplicada = new Meta
            {
                Id = proximoId++,
                Vendedor = metaOriginal.Vendedor + " (Cópia)",
                Valor = metaOriginal.Valor,
                Tipo = metaOriginal.Tipo,
                Periodicidade = metaOriginal.Periodicidade,
                Produto = metaOriginal.Produto,
                Ativa = true,
            };

            metas.Add(metaDuplicada);
            AtualizarLista();
            MessageBox.Show($"Meta duplicada com sucesso!\nNova meta criada para: {metaDuplicada.Vendedor}",
                "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExcluirMeta_Click(object sender, EventArgs e)
        {
            if (dgvMetas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma meta para alterar o status.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Meta metaSelecionada = (Meta)dgvMetas.SelectedRows[0].DataBoundItem;
            string acao = metaSelecionada.Ativa ? "desativar" : "ativar";
            string status = metaSelecionada.Ativa ? "inativa" : "ativa";

            DialogResult resultado = MessageBox.Show(
                $"Tem certeza que deseja {acao} a meta do vendedor '{metaSelecionada.Vendedor}'?\n\n" +
                $"A meta ficará {status}.",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Salva o estado para possível desfazer
                ultimaMetaAlterada = metaSelecionada;
                ultimoStatusAnterior = metaSelecionada.Ativa;

                // Altera o status
                metaSelecionada.Ativa = !metaSelecionada.Ativa;
                AtualizarLista();

                string acaoRealizada = metaSelecionada.Ativa ? "ativada" : "desativada";

                MostrarMensagemComDesfazer($"Meta {acaoRealizada} com sucesso!", metaSelecionada.Vendedor);
            }
        }

        private void FormPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    BtnNovaMeta_Click(sender, e);
                    break;
                case Keys.F4:
                    BtnEditarMeta_Click(sender, e);
                    break;
                case Keys.F6:
                    BtnDuplicarMeta_Click(sender, e);
                    break;
                case Keys.Delete:
                    BtnExcluirMeta_Click(sender, e);
                    break;
                case Keys.F11:
                    BtnBuscar_Click(sender, e);
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Z when e.Control:
                    DesfazerUltimaAlteracao();
                    break;
            }
        }

        #region Sistema de Desfazer

        private void InicializarSistemaDesfazer()
        {
            // Inicializa o timer com 10 segundos
            timerDesfazer = new System.Windows.Forms.Timer();
            timerDesfazer.Interval = 10000; // 10 segundos
            timerDesfazer.Tick += TimerDesfazer_Tick;
        }

        private void TimerDesfazer_Tick(object? sender, EventArgs e)
        {
            // Para o timer e limpa a opção de desfazer
            timerDesfazer?.Stop();
            ultimaMetaAlterada = null;
            AtualizarStatusDesfazer("");
        }

        private void MostrarMensagemComDesfazer(string mensagem, string nomeVendedor)
        {
            // Para qualquer timer anterior
            timerDesfazer?.Stop();

            // Mostra a mensagem normal primeiro
            MessageBox.Show(mensagem, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Atualiza o status com opção de desfazer
            AtualizarStatusDesfazer($"Ação realizada para '{nomeVendedor}' - Pressione Ctrl+Z para desfazer (10s)");

            // Inicia o timer
            timerDesfazer?.Start();
        }

        private void AtualizarStatusDesfazer(string texto)
        {
            // Se não existe a label de status, cria uma simples na tela
            if (lblContador != null)
            {
                if (string.IsNullOrEmpty(texto))
                {
                    // Volta ao texto normal do contador
                    AtualizarRodape();
                }
                else
                {
                    // Mostra a mensagem de desfazer
                    lblContador.Text = texto;
                    lblContador.ForeColor = Color.Blue;
                }
            }
        }

        private void DesfazerUltimaAlteracao()
        {
            if (ultimaMetaAlterada == null)
            {
                MessageBox.Show("Não há ação para desfazer.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Restaura o status anterior
            ultimaMetaAlterada.Ativa = ultimoStatusAnterior;
            AtualizarLista();

            // Para o timer
            timerDesfazer?.Stop();

            string statusRestaurado = ultimaMetaAlterada.Ativa ? "ativa" : "inativa";
            MessageBox.Show($"Ação desfeita! Meta de '{ultimaMetaAlterada.Vendedor}' está {statusRestaurado} novamente.",
                "Desfazer", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpa as variáveis
            ultimaMetaAlterada = null;
            AtualizarStatusDesfazer("");
        }

        #endregion
    }
}