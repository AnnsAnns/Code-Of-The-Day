namespace ElasticityCalculator {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtPEQ1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPEQ2 = new System.Windows.Forms.TextBox();
            this.txtPEP1 = new System.Windows.Forms.TextBox();
            this.txtPEP2 = new System.Windows.Forms.TextBox();
            this.txtPEE = new System.Windows.Forms.TextBox();
            this.btnCalcPriceElasticity = new System.Windows.Forms.Button();
            this.btnClearPriceElasticity = new System.Windows.Forms.Button();
            this.txtPEdQ = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPEdP = new System.Windows.Forms.TextBox();
            this.lblPEOut = new System.Windows.Forms.Label();
            this.lblExpenditure = new System.Windows.Forms.Label();
            this.lblCrossElasticity = new System.Windows.Forms.Label();
            this.lblIncomeElasticity = new System.Windows.Forms.Label();
            this.lblOriginalRevenue = new System.Windows.Forms.Label();
            this.lblNewRevenue = new System.Windows.Forms.Label();
            this.lblRevenueChange = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPEQ1
            // 
            this.txtPEQ1.Location = new System.Drawing.Point(12, 12);
            this.txtPEQ1.Name = "txtPEQ1";
            this.txtPEQ1.Size = new System.Drawing.Size(100, 20);
            this.txtPEQ1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Initial Quanity Demanded or Supplied (Q1)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "New Quantity Demanded or Supplied (Q2)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Initial Price or Income (P1 or M1)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "New Price or Income (P2 or M2)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(118, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Elasticity (E)";
            // 
            // txtPEQ2
            // 
            this.txtPEQ2.Location = new System.Drawing.Point(12, 38);
            this.txtPEQ2.Name = "txtPEQ2";
            this.txtPEQ2.Size = new System.Drawing.Size(100, 20);
            this.txtPEQ2.TabIndex = 1;
            // 
            // txtPEP1
            // 
            this.txtPEP1.Location = new System.Drawing.Point(12, 64);
            this.txtPEP1.Name = "txtPEP1";
            this.txtPEP1.Size = new System.Drawing.Size(100, 20);
            this.txtPEP1.TabIndex = 2;
            // 
            // txtPEP2
            // 
            this.txtPEP2.Location = new System.Drawing.Point(12, 90);
            this.txtPEP2.Name = "txtPEP2";
            this.txtPEP2.Size = new System.Drawing.Size(100, 20);
            this.txtPEP2.TabIndex = 3;
            // 
            // txtPEE
            // 
            this.txtPEE.Location = new System.Drawing.Point(12, 208);
            this.txtPEE.Name = "txtPEE";
            this.txtPEE.Size = new System.Drawing.Size(100, 20);
            this.txtPEE.TabIndex = 6;
            // 
            // btnCalcPriceElasticity
            // 
            this.btnCalcPriceElasticity.Location = new System.Drawing.Point(12, 234);
            this.btnCalcPriceElasticity.Name = "btnCalcPriceElasticity";
            this.btnCalcPriceElasticity.Size = new System.Drawing.Size(311, 23);
            this.btnCalcPriceElasticity.TabIndex = 7;
            this.btnCalcPriceElasticity.Text = "Calculate";
            this.btnCalcPriceElasticity.UseVisualStyleBackColor = true;
            this.btnCalcPriceElasticity.Click += new System.EventHandler(this.btnCalcPriceElasticity_Click);
            // 
            // btnClearPriceElasticity
            // 
            this.btnClearPriceElasticity.Location = new System.Drawing.Point(12, 263);
            this.btnClearPriceElasticity.Name = "btnClearPriceElasticity";
            this.btnClearPriceElasticity.Size = new System.Drawing.Size(311, 23);
            this.btnClearPriceElasticity.TabIndex = 8;
            this.btnClearPriceElasticity.Text = "Clear Fields";
            this.btnClearPriceElasticity.UseVisualStyleBackColor = true;
            this.btnClearPriceElasticity.Click += new System.EventHandler(this.btnClearPriceElasticity_Click);
            // 
            // txtPEdQ
            // 
            this.txtPEdQ.Location = new System.Drawing.Point(12, 138);
            this.txtPEdQ.Name = "txtPEdQ";
            this.txtPEdQ.Size = new System.Drawing.Size(100, 20);
            this.txtPEdQ.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(118, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Change in Qty Demand (%dQ)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(118, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(200, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Change in Price or Income (%dP or %dM)";
            // 
            // txtPEdP
            // 
            this.txtPEdP.Location = new System.Drawing.Point(12, 164);
            this.txtPEdP.Name = "txtPEdP";
            this.txtPEdP.Size = new System.Drawing.Size(100, 20);
            this.txtPEdP.TabIndex = 5;
            // 
            // lblPEOut
            // 
            this.lblPEOut.AutoSize = true;
            this.lblPEOut.Location = new System.Drawing.Point(9, 301);
            this.lblPEOut.Name = "lblPEOut";
            this.lblPEOut.Size = new System.Drawing.Size(99, 13);
            this.lblPEOut.TabIndex = 17;
            this.lblPEOut.Text = "Elasticity: <waiting>";
            // 
            // lblExpenditure
            // 
            this.lblExpenditure.AutoSize = true;
            this.lblExpenditure.Location = new System.Drawing.Point(9, 314);
            this.lblExpenditure.Name = "lblExpenditure";
            this.lblExpenditure.Size = new System.Drawing.Size(114, 13);
            this.lblExpenditure.TabIndex = 18;
            this.lblExpenditure.Text = "Expenditure: <waiting>";
            // 
            // lblCrossElasticity
            // 
            this.lblCrossElasticity.AutoSize = true;
            this.lblCrossElasticity.Location = new System.Drawing.Point(9, 327);
            this.lblCrossElasticity.Name = "lblCrossElasticity";
            this.lblCrossElasticity.Size = new System.Drawing.Size(128, 13);
            this.lblCrossElasticity.TabIndex = 19;
            this.lblCrossElasticity.Text = "Cross Elasticity: <waiting>";
            // 
            // lblIncomeElasticity
            // 
            this.lblIncomeElasticity.AutoSize = true;
            this.lblIncomeElasticity.Location = new System.Drawing.Point(9, 340);
            this.lblIncomeElasticity.Name = "lblIncomeElasticity";
            this.lblIncomeElasticity.Size = new System.Drawing.Size(137, 13);
            this.lblIncomeElasticity.TabIndex = 20;
            this.lblIncomeElasticity.Text = "Income Elasticity: <waiting>";
            // 
            // lblOriginalRevenue
            // 
            this.lblOriginalRevenue.AutoSize = true;
            this.lblOriginalRevenue.Location = new System.Drawing.Point(9, 353);
            this.lblOriginalRevenue.Name = "lblOriginalRevenue";
            this.lblOriginalRevenue.Size = new System.Drawing.Size(140, 13);
            this.lblOriginalRevenue.TabIndex = 21;
            this.lblOriginalRevenue.Text = "Original Revenue: <waiting>";
            // 
            // lblNewRevenue
            // 
            this.lblNewRevenue.AutoSize = true;
            this.lblNewRevenue.Location = new System.Drawing.Point(9, 366);
            this.lblNewRevenue.Name = "lblNewRevenue";
            this.lblNewRevenue.Size = new System.Drawing.Size(127, 13);
            this.lblNewRevenue.TabIndex = 22;
            this.lblNewRevenue.Text = "New Revenue: <waiting>";
            // 
            // lblRevenueChange
            // 
            this.lblRevenueChange.AutoSize = true;
            this.lblRevenueChange.Location = new System.Drawing.Point(9, 379);
            this.lblRevenueChange.Name = "lblRevenueChange";
            this.lblRevenueChange.Size = new System.Drawing.Size(142, 13);
            this.lblRevenueChange.TabIndex = 23;
            this.lblRevenueChange.Text = "Revenue Change: <waiting>";
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(9, 405);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(309, 39);
            this.label6.TabIndex = 24;
            this.label6.Text = "WARNING: Price elasticity of supply/demand should always be given as an absolute " +
    "value (no negative sign), while income and cross elasticity should include the n" +
    "egative sign if it\'s negative.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 453);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblRevenueChange);
            this.Controls.Add(this.lblNewRevenue);
            this.Controls.Add(this.txtPEQ1);
            this.Controls.Add(this.lblOriginalRevenue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblIncomeElasticity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCrossElasticity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblExpenditure);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblPEOut);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPEdP);
            this.Controls.Add(this.txtPEQ2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPEP1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPEP2);
            this.Controls.Add(this.txtPEdQ);
            this.Controls.Add(this.txtPEE);
            this.Controls.Add(this.btnCalcPriceElasticity);
            this.Controls.Add(this.btnClearPriceElasticity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Elasticity Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtPEQ1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPEQ2;
        private System.Windows.Forms.TextBox txtPEP1;
        private System.Windows.Forms.TextBox txtPEP2;
        private System.Windows.Forms.TextBox txtPEE;
        private System.Windows.Forms.Button btnCalcPriceElasticity;
        private System.Windows.Forms.Button btnClearPriceElasticity;
        private System.Windows.Forms.TextBox txtPEdQ;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPEdP;
        private System.Windows.Forms.Label lblPEOut;
        private System.Windows.Forms.Label lblExpenditure;
        private System.Windows.Forms.Label lblCrossElasticity;
        private System.Windows.Forms.Label lblIncomeElasticity;
        private System.Windows.Forms.Label lblOriginalRevenue;
        private System.Windows.Forms.Label lblNewRevenue;
        private System.Windows.Forms.Label lblRevenueChange;
        private System.Windows.Forms.Label label6;
    }
}

