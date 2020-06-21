partial class GameForm
{
   /// <summary>
   /// Required designer variable.
   /// </summary>
   private System.ComponentModel.IContainer components = null;

   /// <summary>
   /// Clean up any resources being used.
   /// </summary>
   /// <param name="disposing">true if managed resources should be disposed; otherwise,false.</param>
   protected override void Dispose(bool disposing)
   {
      if(disposing && (components != null))
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
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.joinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // timer1
      // 
      this.timer1.Enabled = true;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0,0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(539,24);
      this.menuStrip1.TabIndex = 24;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // menuToolStripMenuItem
      // 
      this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cToolStripMenuItem,
            this.joinToolStripMenuItem});
      this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
      this.menuToolStripMenuItem.Size = new System.Drawing.Size(37,20);
      this.menuToolStripMenuItem.Text = "File";
      // 
      // cToolStripMenuItem
      // 
      this.cToolStripMenuItem.Name = "cToolStripMenuItem";
      this.cToolStripMenuItem.Size = new System.Drawing.Size(108,22);
      this.cToolStripMenuItem.Text = "Create";
      this.cToolStripMenuItem.Click += new System.EventHandler(this.cToolStripMenuItem_Click);
      // 
      // joinToolStripMenuItem
      // 
      this.joinToolStripMenuItem.Name = "joinToolStripMenuItem";
      this.joinToolStripMenuItem.Size = new System.Drawing.Size(108,22);
      this.joinToolStripMenuItem.Text = "Join";
      this.joinToolStripMenuItem.Click += new System.EventHandler(this.joinToolStripMenuItem_Click);
      // 
      // GameForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(539,453);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "GameForm";
      this.Text = "Cloud Boxing";
      this.Load += new System.EventHandler(this.GameForm_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

   }

   #endregion

   private System.Windows.Forms.Timer timer1;
   private System.Windows.Forms.MenuStrip menuStrip1;
   private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
   private System.Windows.Forms.ToolStripMenuItem cToolStripMenuItem;
   private System.Windows.Forms.ToolStripMenuItem joinToolStripMenuItem;
}

