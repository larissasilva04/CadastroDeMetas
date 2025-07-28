using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CadastroDeMetas
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Configurações do Form
            this.Size = new Size(1200, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(1000, 500);
            this.KeyPreview = true;
            this.KeyDown += FormPrincipal_KeyDown;
            this.BackColor = Color.FromArgb(142, 142, 142);
            this.Name = "FormPrincipal";
            this.Text = "Cadastro de Metas - Sistema de Vendas";

            // Inicializar controles
            InitializeControls();
            ConfigureDataGridView();
            ConfigureLayout();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeControls()
        {
            // Panel superior
            panelTop = new Panel();
            panelTop.BackColor = Color.FromArgb(51, 67, 85);
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 80;

            // Título
            lblTitulo = new Label();
            lblTitulo.Text = " 📊 CADASTRO DE METAS";
            lblTitulo.Font = new Font("Montserrat", 9, FontStyle.Regular);
            lblTitulo.ForeColor = Color.FromArgb(255, 197, 36);
            lblTitulo.Location = new Point(10, 6);
            lblTitulo.Size = new Size(200, 30);
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;

            // Campo de filtro
            lblFiltro = new Label();
            lblFiltro.Text = "🔍 Filtro";
            lblFiltro.ForeColor = Color.White;
            lblFiltro.Location = new Point(10, 48);
            lblFiltro.Size = new Size(70, 20);
            lblFiltro.TextAlign = ContentAlignment.MiddleLeft;

            txtFiltro = new TextBox();
            txtFiltro.PlaceholderText = " Nome do vendedor ou Valor da Meta ";   
            txtFiltro.BackColor = Color.FromArgb(255, 255, 255);
            txtFiltro.ForeColor = Color.FromArgb(164, 164, 164);
            txtFiltro.Location = new Point(83, 48);
            txtFiltro.Size = new Size(210, 60);
            txtFiltro.Font = new Font("Montserrat", 9, FontStyle.Regular);
            txtFiltro.BorderStyle = BorderStyle.FixedSingle;
            txtFiltro.ForeColor = Color.FromArgb(64, 64, 67);
            txtFiltro.TextChanged += (s, e) => AplicarFiltros();

            // Checkbox filtrar inativas
            chkFiltrarInativas = new CheckBox();
            chkFiltrarInativas.Text = "Limpar Filtros";
            chkFiltrarInativas.ForeColor = Color.White;
            chkFiltrarInativas.Location = new Point(310, 45);
            chkFiltrarInativas.Size = new Size(150, 30);
            chkFiltrarInativas.Checked = true;
            chkFiltrarInativas.CheckedChanged += ChkFiltrarInativas_CheckedChanged;

            // Botão Buscar filtro
            btnBuscar = CriarBotaoEstilizado("Buscar (F11)",
                Color.FromArgb(23, 162, 184),
                Color.FromArgb(13, 152, 174),
                Color.White);
            btnBuscar.Location = new Point(1020, 35);
            btnBuscar.Size = new Size(130, 40);
            btnBuscar.Click += BtnBuscar_Click;

            // Adicionar controles ao panel superior
            panelTop.Controls.Add(lblTitulo);
            panelTop.Controls.Add(lblFiltro);
            panelTop.Controls.Add(txtFiltro);
            panelTop.Controls.Add(chkFiltrarInativas);
            panelTop.Controls.Add(btnBuscar);

            // Panel inferior (botões)
            panelBottom = new Panel();
            panelBottom.BackColor = Color.FromArgb(51, 67, 85);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Height = 80;

            // Botões de ação
            btnNovaMeta = CriarBotaoEstilizado("➕ Adicionar (F2)",
                Color.FromArgb(0, 100, 0),
                Color.FromArgb(23, 181, 28),
                Color.White);
            btnNovaMeta.Location = new Point(20, 20);
            btnNovaMeta.Size = new Size(130, 40);
            btnNovaMeta.Click += BtnNovaMeta_Click;

            btnEditarMeta = CriarBotaoEstilizado("✏️ Editar (F4)",
                Color.FromArgb(51, 67, 85),
                Color.FromArgb(31, 47, 65),
                Color.White);
            btnEditarMeta.Location = new Point(180, 20);
            btnEditarMeta.Size = new Size(130, 40);
            btnEditarMeta.Click += BtnEditarMeta_Click;

            btnDuplicarMeta = CriarBotaoEstilizado("📋 Duplicar (F6)",
                Color.FromArgb(51, 67, 85),
                Color.FromArgb(31, 47, 65),
                Color.White);
            btnDuplicarMeta.Location = new Point(320, 20);
            btnDuplicarMeta.Size = new Size(130, 40);
            btnDuplicarMeta.Click += BtnDuplicarMeta_Click;

            btnExcluirMeta = CriarBotaoEstilizado("🗑️ Excluir (Del)",
                Color.FromArgb(199, 0, 14),
                Color.FromArgb(255, 72, 72),
                Color.White);
            btnExcluirMeta.Location = new Point(470, 20);
            btnExcluirMeta.Size = new Size(130, 40);
            btnExcluirMeta.Click += BtnExcluirMeta_Click;

            // Label contador
            lblContador = new Label();
            lblContador.Name = "lblRodape";
            lblContador.Text = "0 metas encontradas";
            lblContador.ForeColor = Color.FromArgb(164, 164, 164);
            lblContador.Font = new Font("Montserrat", 9, FontStyle.Regular);
            lblContador.Location = new Point(620, 35);
            lblContador.Size = new Size(200, 20);
            lblContador.TextAlign = ContentAlignment.MiddleLeft;

            // Adicionar botões ao panel inferior
            panelBottom.Controls.Add(btnNovaMeta);
            panelBottom.Controls.Add(btnEditarMeta);
            panelBottom.Controls.Add(btnDuplicarMeta);
            panelBottom.Controls.Add(btnExcluirMeta);
            panelBottom.Controls.Add(lblContador);
        }

        private void ConfigureDataGridView()
        {
            dgvMetas = new DataGridView();
            dgvMetas.Dock = DockStyle.Fill;
            dgvMetas.BackgroundColor = Color.FromArgb(64, 64, 67);
            dgvMetas.GridColor = Color.FromArgb(100, 100, 100);
            dgvMetas.BorderStyle = BorderStyle.None;
            dgvMetas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMetas.MultiSelect = false;
            dgvMetas.ReadOnly = true;
            dgvMetas.AllowUserToAddRows = false;
            dgvMetas.AllowUserToDeleteRows = false;
            dgvMetas.AllowUserToResizeRows = false;
            dgvMetas.RowHeadersVisible = false;
            dgvMetas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Estilo das células
            dgvMetas.DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 67);
            dgvMetas.DefaultCellStyle.ForeColor = Color.White;
            dgvMetas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 197, 36);
            dgvMetas.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvMetas.DefaultCellStyle.Font = new Font("Montserrat", 9, FontStyle.Regular);

            // Estilo do cabeçalho
            dgvMetas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 67, 85);
            dgvMetas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMetas.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 9, FontStyle.Regular);
            dgvMetas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMetas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvMetas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvMetas.ColumnHeadersHeight = 35;

            // Eventos
            dgvMetas.DoubleClick += DataGridView_DoubleClick;
            dgvMetas.DataBindingComplete += DgvMetas_DataBindingComplete;
        }

        private void DgvMetas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Configurar colunas após o binding
            if (dgvMetas.Columns.Count > 0)
            {
                // Ocultar colunas desnecessárias
                if (dgvMetas.Columns["Id"] != null)
                    dgvMetas.Columns["Id"].Visible = false;

                if (dgvMetas.Columns["NumeroVinculados"] != null)
                    dgvMetas.Columns["NumeroVinculados"].Visible = false;

                if (dgvMetas.Columns["TipoVinculados"] != null)
                    dgvMetas.Columns["TipoVinculados"].Visible = false;

                if (dgvMetas.Columns["StatusVinculados"] != null)
                    dgvMetas.Columns["StatusVinculados"].Visible = false;

                if (dgvMetas.Columns["DataCriacao"] != null)
                    dgvMetas.Columns["DataCriacao"].Visible = false;

                if (dgvMetas.Columns["ValorFormatado"] != null)
                    dgvMetas.Columns["ValorFormatado"].Visible = false;

                // Configurar headers e larguras
                if (dgvMetas.Columns["Vendedor"] != null)
                {
                    dgvMetas.Columns["Vendedor"].HeaderText = "Vendedor";
                    dgvMetas.Columns["Vendedor"].FillWeight = 25;
                }

                if (dgvMetas.Columns["Valor"] != null)
                {
                    dgvMetas.Columns["Valor"].HeaderText = "Valor";
                    dgvMetas.Columns["Valor"].DefaultCellStyle.Format = "C2";
                    dgvMetas.Columns["Valor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvMetas.Columns["Valor"].FillWeight = 15;
                }

                if (dgvMetas.Columns["Tipo"] != null)
                {
                    dgvMetas.Columns["Tipo"].HeaderText = "Tipo";
                    dgvMetas.Columns["Tipo"].FillWeight = 20;
                }

                if (dgvMetas.Columns["Periodicidade"] != null)
                {
                    dgvMetas.Columns["Periodicidade"].HeaderText = "Periodicidade";
                    dgvMetas.Columns["Periodicidade"].FillWeight = 15;
                }

                if (dgvMetas.Columns["Produto"] != null)
                {
                    dgvMetas.Columns["Produto"].HeaderText = "Produto";
                    dgvMetas.Columns["Produto"].FillWeight = 20;
                }

                if (dgvMetas.Columns["Ativa"] != null)
                {
                    dgvMetas.Columns["Ativa"].HeaderText = "Status";
                    dgvMetas.Columns["Ativa"].FillWeight = 10;
                }
            }
        }

        private void ConfigureLayout()
        {
            // Adicionar controles ao formulário na ordem correta
            this.Controls.Add(dgvMetas);
            this.Controls.Add(panelTop);
            this.Controls.Add(panelBottom);
        }

        private Button CriarBotaoEstilizado(string texto, Color corInicio, Color corFim, Color corTexto)
        {
            Button btn = new Button()
            {
                Text = texto,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Montserrat", 9, FontStyle.Regular),
                ForeColor = corTexto,
                UseVisualStyleBackColor = false,
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;

            bool isPressed = false;
            bool isHovered = false;

            // Paint event para desenhar o gradiente
            btn.Paint += (sender, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle rect = new Rectangle(0, 0, btn.Width, btn.Height);

                Color cor1, cor2;

                if (isPressed)
                {
                    cor1 = Color.FromArgb(Math.Max(0, corInicio.R - 30),
                                         Math.Max(0, corInicio.G - 30),
                                         Math.Max(0, corInicio.B - 30));
                    cor2 = Color.FromArgb(Math.Max(0, corFim.R - 30),
                                         Math.Max(0, corFim.G - 30),
                                         Math.Max(0, corFim.B - 30));
                }
                else if (isHovered)
                {
                    cor1 = Color.FromArgb(Math.Min(255, corInicio.R + 20),
                                         Math.Min(255, corInicio.G + 20),
                                         Math.Min(255, corInicio.B + 20));
                    cor2 = Color.FromArgb(Math.Min(255, corFim.R + 20),
                                         Math.Min(255, corFim.G + 20),
                                         Math.Min(255, corFim.B + 20));
                }
                else
                {
                    cor1 = corInicio;
                    cor2 = corFim;
                }

                using (LinearGradientBrush brush = new LinearGradientBrush(rect, cor1, cor2, 45f))
                {
                    using (GraphicsPath path = CriarRetanguloArredondado(rect, 4))
                    {
                        g.FillPath(brush, path);
                    }
                }

                if (!isPressed)
                {
                    Rectangle shadowRect = new Rectangle(1, 2, btn.Width, btn.Height);
                    using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
                    {
                        using (GraphicsPath shadowPath = CriarRetanguloArredondado(shadowRect, 4))
                        {
                            g.FillPath(shadowBrush, shadowPath);
                        }
                    }
                }

                TextRenderer.DrawText(g, btn.Text, btn.Font, rect, btn.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            btn.MouseEnter += (sender, e) =>
            {
                isHovered = true;
                btn.Cursor = Cursors.Hand;
                btn.Invalidate();
            };

            btn.MouseLeave += (sender, e) =>
            {
                isHovered = false;
                isPressed = false;
                btn.Cursor = Cursors.Default;
                btn.Invalidate();
            };

            btn.MouseDown += (sender, e) =>
            {
                isPressed = true;
                btn.Invalidate();
            };

            btn.MouseUp += (sender, e) =>
            {
                isPressed = false;
                btn.Invalidate();
            };

            return btn;
        }

        private GraphicsPath CriarRetanguloArredondado(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
        }

        #endregion
    }
}