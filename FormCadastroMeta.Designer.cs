using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CadastroDeMetas
{
    partial class FormCadastroMeta
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

            //        
            // Configurações do Form
            // 
            this.Size = new Size(470, 390);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.KeyPreview = true;
            this.KeyDown += FormCadastroMeta_KeyDown;
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.Name = "FormCadastroMeta";

            // Inicializar controles
            lblNomeVendedor = new System.Windows.Forms.Label();
            txtNomeVendedor = new TextBox();
            lblValorMeta = new System.Windows.Forms.Label();
            txtValorMeta = new TextBox();
            lblTipoMeta = new System.Windows.Forms.Label();
            cmbTipoMeta = new ComboBox();
            lblPeriodicidade = new System.Windows.Forms.Label();
            cmbPeriodicidade = new ComboBox();
            lblProduto = new System.Windows.Forms.Label();
            cmbProduto = new ComboBox();
            lblAtivo = new System.Windows.Forms.Label();
            chkAtivo = new CheckBox();

            // 
            // lblNomeVendedor 
            //
            lblNomeVendedor.Location = new Point(20, 20);
            lblNomeVendedor.Size = new Size (145, 20);
            lblNomeVendedor.ForeColor = Color.Yellow;
            lblNomeVendedor.TabIndex = 0;
            lblNomeVendedor.Text = "Nome do Vendedor:";
            // 
            // txtNomeVendedor
            // 
            txtNomeVendedor.BackColor = Color.FromArgb(255, 255, 255);
            txtNomeVendedor.ForeColor = Color.Black;
            txtNomeVendedor.Location = new Point(165, 20);
            txtNomeVendedor.Size = new Size(235, 200);
            txtNomeVendedor.TabIndex = 1;
            // 
            // lblValorMeta
            // 
            lblValorMeta.Location = new Point(230, 50);
            lblValorMeta.Name = "lblValorMeta";
            lblValorMeta.Size = new Size(105, 20);
            lblValorMeta.TabIndex = 2;
            lblValorMeta.Text = "Valor da Meta:";
            lblValorMeta.ForeColor = Color.Yellow;
            // 
            // txtValorMeta
            // 
            txtValorMeta.BackColor = Color.FromArgb(255, 255, 255);
            txtValorMeta.ForeColor = Color.Black;
            txtValorMeta.Location = new Point(340, 49);
            txtValorMeta.Name = "txtValorMeta";
            txtValorMeta.Size = new Size(60, 25);
            txtValorMeta.TabIndex = 3;
            // 
            // lblTipoMeta
            // 
            lblTipoMeta.Location = new Point(20, 80);
            lblTipoMeta.Name = "lblTipoMeta";
            lblTipoMeta.Size = new Size(120, 20);
            lblTipoMeta.TabIndex = 4;
            lblTipoMeta.Text = "Tipo de Meta:";
            lblTipoMeta.ForeColor = Color.White;
            // 
            // cmbTipoMeta
            // 
            cmbTipoMeta.BackColor = Color.FromArgb(255, 255, 255);
            cmbTipoMeta.ForeColor = Color.Black;
            cmbTipoMeta.Items.AddRange(new object[] { "Monetário (R$)", "Litros (L)", "Unidades de Produto (UN)" });
            cmbTipoMeta.Location = new Point(150, 78);
            cmbTipoMeta.Name = "cmbTipoMeta";
            cmbTipoMeta.Size = new Size(250, 28);
            cmbTipoMeta.TabIndex = 5;
            cmbTipoMeta.DropDownStyle = ComboBoxStyle.DropDownList;
            // 
            // lblPeriodicidade
            // 
            lblPeriodicidade.Location = new Point(20, 109);
            lblPeriodicidade.Name = "lblPeriodicidade";
            lblPeriodicidade.Size = new Size(120, 20);
            lblPeriodicidade.TabIndex = 6;
            lblPeriodicidade.Text = "Periodicidade:";
            lblPeriodicidade.ForeColor = Color.White;
            // 
            // cmbPeriodicidade
            // 
            cmbPeriodicidade.BackColor = Color.FromArgb(255, 255, 255);
            cmbPeriodicidade.ForeColor = Color.Black;
            cmbPeriodicidade.Items.AddRange(new object[] { "Diária", "Semanal", "Mensal" });
            cmbPeriodicidade.Location = new Point(150, 107);
            cmbPeriodicidade.Name = "cmbPeriodicidade";
            cmbPeriodicidade.Size = new Size(250, 28);
            cmbPeriodicidade.TabIndex = 7;
            cmbPeriodicidade.DropDownStyle = ComboBoxStyle.DropDownList;
            // 
            // lblProduto
            // 
            lblProduto.Location = new Point(20, 138);
            lblProduto.Name = "lblProduto";
            lblProduto.Size = new Size(120, 20);
            lblProduto.TabIndex = 8;
            lblProduto.Text = "Produto:";
            lblProduto.ForeColor = Color.White;
            // 
            // cmbProduto
            // 
            cmbProduto.BackColor = Color.FromArgb(255, 255, 255);
            cmbProduto.ForeColor = Color.Black;
            cmbProduto.Items.AddRange(new object[] { "Barris", "Garrafas e Latas", "Acessórios e Produtos" });
            cmbProduto.Location = new Point(150, 136);
            cmbProduto.Name = "cmbProduto";
            cmbProduto.Size = new Size(250, 28);
            cmbProduto.TabIndex = 9;
            cmbProduto.DropDownStyle = ComboBoxStyle.DropDownList;
            // 
            // lblAtivo
            // 
            lblAtivo.Location = new Point(20, 167);
            lblAtivo.Name = "lblAtivo";
            lblAtivo.Size = new Size(120, 20);
            lblAtivo.TabIndex = 10;
            lblAtivo.Text = "Ativo?";
            lblAtivo.ForeColor = Color.White;
            // 
            // chkAtivo
            // 
            chkAtivo.Checked = true;
            chkAtivo.CheckState = CheckState.Checked;
            chkAtivo.Location = new Point(150, 165);
            chkAtivo.Name = "chkAtivo";
            chkAtivo.Size = new Size(200, 20);
            chkAtivo.TabIndex = 11;
            chkAtivo.ForeColor = Color.White;
            // 
            // btnSalvar
            // 
            btnSalvar = CriarBotaoEstilizado("✓ Salvar (F10)",
                Color.FromArgb(255, 197, 36), 
                Color.FromArgb(235, 177, 26), 
                Color.FromArgb(51, 51, 51));   
            btnSalvar.Location = new Point(190, 280);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(120, 35);
            btnSalvar.TabIndex = 12;
            btnSalvar.Click += BtnSalvar_Click;
            // 
            // btnVoltar
            // 
            btnVoltar = CriarBotaoEstilizado("← Voltar (Esc)",
                Color.FromArgb(51, 67, 85),   
                Color.FromArgb(31, 47, 65),   
                Color.White);                 
            btnVoltar.Location = new Point(320, 280);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(120, 35);
            btnVoltar.TabIndex = 13;
            btnVoltar.Click += BtnVoltar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir = CriarBotaoEstilizado("🗑️ Excluir",
                Color.FromArgb(199, 0, 14), 
                Color.FromArgb(255, 72, 72),  
                Color.White);                
            btnExcluir.Location = new Point(20, 280);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(120, 35);
            btnExcluir.TabIndex = 14;
            btnExcluir.Click += BtnExcluir_Click;
            // 
            // FormCadastroMeta
            // 
            this.Controls.Add(lblNomeVendedor);
            this.Controls.Add(txtNomeVendedor);
            this.Controls.Add(lblValorMeta);
            this.Controls.Add(txtValorMeta);
            this.Controls.Add(lblTipoMeta);
            this.Controls.Add(cmbTipoMeta);
            this.Controls.Add(lblPeriodicidade);
            this.Controls.Add(cmbPeriodicidade);
            this.Controls.Add(lblProduto);
            this.Controls.Add(cmbProduto);
            this.Controls.Add(lblAtivo);
            this.Controls.Add(chkAtivo);
            this.Controls.Add(btnSalvar);
            this.Controls.Add(btnVoltar);
            this.Controls.Add(btnExcluir);

            this.ResumeLayout(false);
            this.PerformLayout();
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
                Cursor = Cursors.Hand // CURSOR DE MÃOZINHA
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
                    // Cores quando pressionado (mais escuras)
                    cor1 = Color.FromArgb(Math.Max(0, corInicio.R - 30),
                                         Math.Max(0, corInicio.G - 30),
                                         Math.Max(0, corInicio.B - 30));
                    cor2 = Color.FromArgb(Math.Max(0, corFim.R - 30),
                                         Math.Max(0, corFim.G - 30),
                                         Math.Max(0, corFim.B - 30));
                }
                else if (isHovered)
                {
                    // Cores quando hover (mais claras)
                    cor1 = Color.FromArgb(Math.Min(255, corInicio.R + 20),
                                         Math.Min(255, corInicio.G + 20),
                                         Math.Min(255, corInicio.B + 20));
                    cor2 = Color.FromArgb(Math.Min(255, corFim.R + 20),
                                         Math.Min(255, corFim.G + 20),
                                         Math.Min(255, corFim.B + 20));
                }
                else
                {
                    // Cores normais
                    cor1 = corInicio;
                    cor2 = corFim;
                }

                // Desenhar gradiente
                using (LinearGradientBrush brush = new LinearGradientBrush(rect, cor1, cor2, 45f))
                {
                    using (GraphicsPath path = CriarRetanguloArredondado(rect, 4))
                    {
                        g.FillPath(brush, path);
                    }
                }

                // Desenhar sombra sutil
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

                // Desenhar texto
                TextRenderer.DrawText(g, btn.Text, btn.Font, rect, btn.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            // Eventos de mouse para hover e pressed
            btn.MouseEnter += (sender, e) =>
            {
                isHovered = true;
                btn.Cursor = Cursors.Hand; // Garantir cursor de mãozinha
                btn.Invalidate();
            };

            btn.MouseLeave += (sender, e) =>
            {
                isHovered = false;
                isPressed = false;
                btn.Cursor = Cursors.Default; // Voltar cursor padrão
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

        // MÉTODO AUXILIAR PARA CRIAR RETÂNGULOS ARREDONDADOS
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

        private System.Windows.Forms.Label lblNomeVendedor;
        private System.Windows.Forms.Label lblValorMeta;
        private System.Windows.Forms.Label lblTipoMeta;
        private System.Windows.Forms.Label lblPeriodicidade;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.Label lblAtivo;
    }
}


