namespace logr
{
    partial class Form_theory
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Поняття інтерполяції");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Лінійна інтерполяція");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Квадратична інтерполяція");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Многочлен Лагранжа");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Многочлен Ньютона");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Інтерполяція", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Поняття апроксимації");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Лінійна апроксимація");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Квадратична апроксимація");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Апроксімація", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Криві Безьє");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_theory));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(300, 18);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(5);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(33, 31);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(611, 494);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Visible = false;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.Info;
            this.treeView1.Location = new System.Drawing.Point(1, 72);
            this.treeView1.Margin = new System.Windows.Forms.Padding(5);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Узел3";
            treeNode1.Text = "Поняття інтерполяції";
            treeNode2.Name = "Узел4";
            treeNode2.Text = "Лінійна інтерполяція";
            treeNode3.Name = "Узел5";
            treeNode3.Text = "Квадратична інтерполяція";
            treeNode4.Name = "Узел6";
            treeNode4.Text = "Многочлен Лагранжа";
            treeNode5.Name = "Узел7";
            treeNode5.Text = "Многочлен Ньютона";
            treeNode6.Name = "Узел0";
            treeNode6.Text = "Інтерполяція";
            treeNode7.Name = "Узел8";
            treeNode7.Text = "Поняття апроксимації";
            treeNode8.Name = "Узел9";
            treeNode8.Text = "Лінійна апроксимація";
            treeNode9.Name = "Узел10";
            treeNode9.Text = "Квадратична апроксимація";
            treeNode10.Name = "Узел1";
            treeNode10.Text = "Апроксімація";
            treeNode11.Name = "Узел2";
            treeNode11.Text = "Криві Безьє";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode10,
            treeNode11});
            this.treeView1.Size = new System.Drawing.Size(289, 262);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Location = new System.Drawing.Point(21, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 53);
            this.button2.TabIndex = 56;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form_theory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(953, 526);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.webBrowser1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form_theory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_theory_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button2;
    }
}