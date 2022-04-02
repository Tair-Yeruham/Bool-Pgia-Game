namespace BoolPgiaGameUI
{
    public partial class GameConfiguration
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void initializeComponent()
        {
            this.buttonAmountOfGuesses = new System.Windows.Forms.Button();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonAmountOfGuesses
            // 
            this.buttonAmountOfGuesses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left))));
            this.buttonAmountOfGuesses.BackColor = System.Drawing.SystemColors.Control;
            this.buttonAmountOfGuesses.Location = new System.Drawing.Point(69, 73);
            this.buttonAmountOfGuesses.Name = "buttonAmountOfGuesses";
            this.buttonAmountOfGuesses.Size = new System.Drawing.Size(210, 23);
            this.buttonAmountOfGuesses.TabIndex = 0;
            this.buttonAmountOfGuesses.UseVisualStyleBackColor = false;
            this.buttonAmountOfGuesses.Click += new System.EventHandler(this.buttonAmountOfGuesses_Click);
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartGame.Location = new System.Drawing.Point(257, 128);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(75, 23);
            this.buttonStartGame.TabIndex = 1;
            this.buttonStartGame.Text = "Start";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // GameConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 188);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.buttonAmountOfGuesses);
            this.MinimumSize = new System.Drawing.Size(381, 227);
            this.Name = "GameConfiguration";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bool Pgia";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAmountOfGuesses;
        private System.Windows.Forms.Button buttonStartGame;
    }
}