namespace DimmingContol
{
    partial class FormConn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConn));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.titleLabel = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel5 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.ipMetroTextbox = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.subMaskMetroTextbox = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.gateWayMetroTextbox = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.portMetroTextbox = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.connButton = new Bunifu.Framework.UI.BunifuThinButton2();
            this.closeButton = new Bunifu.Framework.UI.BunifuThinButton2();
            this.cancelButton = new Bunifu.Framework.UI.BunifuThinButton2();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.panel1.Controls.Add(this.titleLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(678, 51);
            this.panel1.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Malgun Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(111, 6);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(442, 40);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "정릉 터널등 제어기 IP 접속 설정";
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Malgun Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(82, 119);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(205, 63);
            this.bunifuCustomLabel2.TabIndex = 1;
            this.bunifuCustomLabel2.Text = "IP";
            this.bunifuCustomLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bunifuCustomLabel3
            // 
            this.bunifuCustomLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.bunifuCustomLabel3.Font = new System.Drawing.Font("Malgun Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bunifuCustomLabel3.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel3.Location = new System.Drawing.Point(82, 201);
            this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
            this.bunifuCustomLabel3.Size = new System.Drawing.Size(205, 63);
            this.bunifuCustomLabel3.TabIndex = 2;
            this.bunifuCustomLabel3.Text = "SUB Mask";
            this.bunifuCustomLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bunifuCustomLabel4
            // 
            this.bunifuCustomLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.bunifuCustomLabel4.Font = new System.Drawing.Font("Malgun Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bunifuCustomLabel4.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel4.Location = new System.Drawing.Point(82, 283);
            this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
            this.bunifuCustomLabel4.Size = new System.Drawing.Size(205, 63);
            this.bunifuCustomLabel4.TabIndex = 3;
            this.bunifuCustomLabel4.Text = "Gateway";
            this.bunifuCustomLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bunifuCustomLabel5
            // 
            this.bunifuCustomLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.bunifuCustomLabel5.Font = new System.Drawing.Font("Malgun Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bunifuCustomLabel5.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel5.Location = new System.Drawing.Point(82, 365);
            this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
            this.bunifuCustomLabel5.Size = new System.Drawing.Size(205, 63);
            this.bunifuCustomLabel5.TabIndex = 4;
            this.bunifuCustomLabel5.Text = "Port";
            this.bunifuCustomLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ipMetroTextbox
            // 
            this.ipMetroTextbox.BorderColorFocused = System.Drawing.Color.Blue;
            this.ipMetroTextbox.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ipMetroTextbox.BorderColorMouseHover = System.Drawing.Color.Blue;
            this.ipMetroTextbox.BorderThickness = 3;
            this.ipMetroTextbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipMetroTextbox.Font = new System.Drawing.Font("Malgun Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipMetroTextbox.ForeColor = System.Drawing.Color.Gainsboro;
            this.ipMetroTextbox.isPassword = false;
            this.ipMetroTextbox.Location = new System.Drawing.Point(309, 119);
            this.ipMetroTextbox.Margin = new System.Windows.Forms.Padding(0);
            this.ipMetroTextbox.Name = "ipMetroTextbox";
            this.ipMetroTextbox.Size = new System.Drawing.Size(255, 63);
            this.ipMetroTextbox.TabIndex = 6;
            this.ipMetroTextbox.Text = "192.168.0.100";
            this.ipMetroTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // subMaskMetroTextbox
            // 
            this.subMaskMetroTextbox.BorderColorFocused = System.Drawing.Color.Blue;
            this.subMaskMetroTextbox.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.subMaskMetroTextbox.BorderColorMouseHover = System.Drawing.Color.Blue;
            this.subMaskMetroTextbox.BorderThickness = 3;
            this.subMaskMetroTextbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.subMaskMetroTextbox.Font = new System.Drawing.Font("Malgun Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subMaskMetroTextbox.ForeColor = System.Drawing.Color.Gainsboro;
            this.subMaskMetroTextbox.isPassword = false;
            this.subMaskMetroTextbox.Location = new System.Drawing.Point(309, 201);
            this.subMaskMetroTextbox.Margin = new System.Windows.Forms.Padding(0);
            this.subMaskMetroTextbox.Name = "subMaskMetroTextbox";
            this.subMaskMetroTextbox.Size = new System.Drawing.Size(255, 63);
            this.subMaskMetroTextbox.TabIndex = 8;
            this.subMaskMetroTextbox.Text = "255.255.255.0";
            this.subMaskMetroTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gateWayMetroTextbox
            // 
            this.gateWayMetroTextbox.BorderColorFocused = System.Drawing.Color.Blue;
            this.gateWayMetroTextbox.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gateWayMetroTextbox.BorderColorMouseHover = System.Drawing.Color.Blue;
            this.gateWayMetroTextbox.BorderThickness = 3;
            this.gateWayMetroTextbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.gateWayMetroTextbox.Font = new System.Drawing.Font("Malgun Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gateWayMetroTextbox.ForeColor = System.Drawing.Color.Gainsboro;
            this.gateWayMetroTextbox.isPassword = false;
            this.gateWayMetroTextbox.Location = new System.Drawing.Point(309, 283);
            this.gateWayMetroTextbox.Margin = new System.Windows.Forms.Padding(0);
            this.gateWayMetroTextbox.Name = "gateWayMetroTextbox";
            this.gateWayMetroTextbox.Size = new System.Drawing.Size(255, 63);
            this.gateWayMetroTextbox.TabIndex = 9;
            this.gateWayMetroTextbox.Text = "192.168.0.1";
            this.gateWayMetroTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // portMetroTextbox
            // 
            this.portMetroTextbox.BorderColorFocused = System.Drawing.Color.Blue;
            this.portMetroTextbox.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.portMetroTextbox.BorderColorMouseHover = System.Drawing.Color.Blue;
            this.portMetroTextbox.BorderThickness = 3;
            this.portMetroTextbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.portMetroTextbox.Font = new System.Drawing.Font("Malgun Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portMetroTextbox.ForeColor = System.Drawing.Color.Gainsboro;
            this.portMetroTextbox.isPassword = false;
            this.portMetroTextbox.Location = new System.Drawing.Point(309, 365);
            this.portMetroTextbox.Margin = new System.Windows.Forms.Padding(0);
            this.portMetroTextbox.Name = "portMetroTextbox";
            this.portMetroTextbox.Size = new System.Drawing.Size(255, 63);
            this.portMetroTextbox.TabIndex = 10;
            this.portMetroTextbox.Text = "5000";
            this.portMetroTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // connButton
            // 
            this.connButton.ActiveBorderThickness = 1;
            this.connButton.ActiveCornerRadius = 20;
            this.connButton.ActiveFillColor = System.Drawing.Color.SeaGreen;
            this.connButton.ActiveForecolor = System.Drawing.Color.White;
            this.connButton.ActiveLineColor = System.Drawing.Color.SeaGreen;
            this.connButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.connButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("connButton.BackgroundImage")));
            this.connButton.ButtonText = "연결";
            this.connButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.connButton.Font = new System.Drawing.Font("Malgun Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.connButton.ForeColor = System.Drawing.Color.SeaGreen;
            this.connButton.IdleBorderThickness = 1;
            this.connButton.IdleCornerRadius = 20;
            this.connButton.IdleFillColor = System.Drawing.Color.White;
            this.connButton.IdleForecolor = System.Drawing.Color.SeaGreen;
            this.connButton.IdleLineColor = System.Drawing.Color.SeaGreen;
            this.connButton.Location = new System.Drawing.Point(73, 485);
            this.connButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.connButton.Name = "connButton";
            this.connButton.Size = new System.Drawing.Size(126, 52);
            this.connButton.TabIndex = 11;
            this.connButton.TabStop = false;
            this.connButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.connButton.Click += new System.EventHandler(this.Button_Click);
            // 
            // closeButton
            // 
            this.closeButton.ActiveBorderThickness = 1;
            this.closeButton.ActiveCornerRadius = 20;
            this.closeButton.ActiveFillColor = System.Drawing.Color.SeaGreen;
            this.closeButton.ActiveForecolor = System.Drawing.Color.White;
            this.closeButton.ActiveLineColor = System.Drawing.Color.SeaGreen;
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.closeButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closeButton.BackgroundImage")));
            this.closeButton.ButtonText = "끊기";
            this.closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeButton.Font = new System.Drawing.Font("Malgun Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.closeButton.ForeColor = System.Drawing.Color.SeaGreen;
            this.closeButton.IdleBorderThickness = 1;
            this.closeButton.IdleCornerRadius = 20;
            this.closeButton.IdleFillColor = System.Drawing.Color.White;
            this.closeButton.IdleForecolor = System.Drawing.Color.SeaGreen;
            this.closeButton.IdleLineColor = System.Drawing.Color.SeaGreen;
            this.closeButton.Location = new System.Drawing.Point(266, 485);
            this.closeButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(126, 52);
            this.closeButton.TabIndex = 12;
            this.closeButton.TabStop = false;
            this.closeButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.closeButton.Click += new System.EventHandler(this.Button_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.ActiveBorderThickness = 1;
            this.cancelButton.ActiveCornerRadius = 20;
            this.cancelButton.ActiveFillColor = System.Drawing.Color.SeaGreen;
            this.cancelButton.ActiveForecolor = System.Drawing.Color.White;
            this.cancelButton.ActiveLineColor = System.Drawing.Color.SeaGreen;
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.cancelButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cancelButton.BackgroundImage")));
            this.cancelButton.ButtonText = "닫기";
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.Font = new System.Drawing.Font("Malgun Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cancelButton.ForeColor = System.Drawing.Color.SeaGreen;
            this.cancelButton.IdleBorderThickness = 1;
            this.cancelButton.IdleCornerRadius = 20;
            this.cancelButton.IdleFillColor = System.Drawing.Color.White;
            this.cancelButton.IdleForecolor = System.Drawing.Color.SeaGreen;
            this.cancelButton.IdleLineColor = System.Drawing.Color.SeaGreen;
            this.cancelButton.Location = new System.Drawing.Point(459, 485);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(126, 52);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.TabStop = false;
            this.cancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cancelButton.Click += new System.EventHandler(this.Button_Click);
            // 
            // FormConnDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.ClientSize = new System.Drawing.Size(678, 576);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.connButton);
            this.Controls.Add(this.portMetroTextbox);
            this.Controls.Add(this.gateWayMetroTextbox);
            this.Controls.Add(this.subMaskMetroTextbox);
            this.Controls.Add(this.ipMetroTextbox);
            this.Controls.Add(this.bunifuCustomLabel5);
            this.Controls.Add(this.bunifuCustomLabel4);
            this.Controls.Add(this.bunifuCustomLabel3);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormConnDialog";
            this.Text = "FormConnDialog";
            this.Load += new System.EventHandler(this.FormConnDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuCustomLabel titleLabel;
        private Bunifu.Framework.UI.BunifuMetroTextbox ipMetroTextbox;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel5;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private Bunifu.Framework.UI.BunifuMetroTextbox portMetroTextbox;
        private Bunifu.Framework.UI.BunifuMetroTextbox gateWayMetroTextbox;
        private Bunifu.Framework.UI.BunifuMetroTextbox subMaskMetroTextbox;
        private Bunifu.Framework.UI.BunifuThinButton2 cancelButton;
        private Bunifu.Framework.UI.BunifuThinButton2 closeButton;
        private Bunifu.Framework.UI.BunifuThinButton2 connButton;
    }
}