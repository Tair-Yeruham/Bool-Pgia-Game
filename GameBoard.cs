using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoolPgiaLogic;

namespace BoolPgiaGameUI
{
    public partial class GameBoard : Form
    {
        #region Events

        public static EventHandler UserChangeGuessPick;
        public static EventHandler CheckGuessClicked;

        #endregion

        #region Members

        private const string k_ButtonPickColorGuessNameFormat = "buttonGuess{0}Pick{1}";
        private const string k_ButtonCheckGuessNameFormat = "buttonCheckGuess{0}";
        private const string k_ButtonGuessResultNameFormat = "buttonGuess{0}Result{1}";
        private readonly BoolPgiaLogic.BoolPgiaGame r_BoolPgiaGame;
        private readonly List<Button> r_HiddenResultsButtons;
        private readonly Dictionary<int, List<Button>> r_ResultButtons;
        private readonly Dictionary<int, Button> r_CheckGuessButtons;
        private readonly Color r_CorrectInPlaceColorResult = Color.Black;
        private readonly Color r_CorrectNotInPlaceColorResult = Color.Yellow;
        // $G$ DSN-999 (-4) members should be readonly
        private Color[] m_CurrentGuess;
        private GuessColorPlate m_PickColorGuessDialog;

        #endregion

        public GameBoard(int i_AmountOfChances)
        {
            initializeComponent();
            m_PickColorGuessDialog = new GuessColorPlate();
            r_BoolPgiaGame = new BoolPgiaLogic.BoolPgiaGame(i_AmountOfChances, m_PickColorGuessDialog.AllColorsTypes);
            this.Size = new Size(this.Size.Width, 140 + (i_AmountOfChances * 50));
            m_CurrentGuess = new Color[r_BoolPgiaGame.AmountOfColorsToGuess];
            r_ResultButtons = new Dictionary<int, List<Button>>();
            r_CheckGuessButtons = new Dictionary<int, Button>();
            r_HiddenResultsButtons = new List<Button>();  
            createHiddenResultsButtonsList(); 
            createButtonsForCurrentGame(i_AmountOfChances);
        }

        private void createHiddenResultsButtonsList()
        {
            foreach(Button button in this.Controls)
            {
                r_HiddenResultsButtons.Add(button);
            }
        }

        #region Buttons Creation

        private void createButtonsForCurrentGame(int i_AmountOfChances)
        {
            createHiddenResultsButtons();
            for (int i = 1; i <= i_AmountOfChances; i++)
            {
                createGuessButtonsSet(i);
                createCheckGuessButton(i);
                createGuessResultButtonsSet(i);
            }
        }

        private void createGuessButtonsSet(int i_GuessNumber)
        {
            Button buttonPickAGuess;
            bool isFirstGuess = i_GuessNumber == 1 ? true : false;

            for (int i = 1; i <= r_BoolPgiaGame.AmountOfColorsToGuess; i++)
            {
                buttonPickAGuess = new PickGuessButton(i_GuessNumber, i);
                buttonPickAGuess.Name = string.Format(k_ButtonPickColorGuessNameFormat, i_GuessNumber, i);
                buttonPickAGuess.Size = new Size(40, 40);
                buttonPickAGuess.Enabled = isFirstGuess;
                buttonPickAGuess.Location = new Point(r_HiddenResultsButtons[0].Location.X + ((i - 1) * 46), 100 + ((i_GuessNumber - 1) * 50));
                buttonPickAGuess.Click += buttonPickAGuess_Click;
                this.Controls.Add(buttonPickAGuess);
            }
        }

        private void createCheckGuessButton(int i_GuessNumber)
        {
            Button buttonCheckGuess = new Button();

            buttonCheckGuess.Name = string.Format(k_ButtonCheckGuessNameFormat, i_GuessNumber);
            buttonCheckGuess.Size = new Size(40, 20);
            buttonCheckGuess.Location = new Point(r_HiddenResultsButtons[r_HiddenResultsButtons.Count - 1].Location.X + 46, 110 + ((i_GuessNumber - 1) * 50));
            buttonCheckGuess.Text = "->>";
            buttonCheckGuess.Enabled = false;
            buttonCheckGuess.Click += ButtonCheckGuess_Click;
            r_CheckGuessButtons.Add(i_GuessNumber, buttonCheckGuess);
            Controls.Add(buttonCheckGuess);
        }

        private void createGuessResultButtonsSet(int i_GuessNumber)
        {
            Button buttonGuessResult;
            int locationOfButtonY, locationOfButtonX;
            List<Button> listOfResultButtonsSet = new List<Button>();

            for (int i = 1; i <= r_BoolPgiaGame.AmountOfColorsToGuess; i++)
            {
                locationOfButtonX = i % 2 == 1 ? 0 : 20;
                locationOfButtonY = i > r_BoolPgiaGame.AmountOfColorsToGuess / 2 ? 120 : 100;
                buttonGuessResult = new Button();
                buttonGuessResult.Name = string.Format(k_ButtonGuessResultNameFormat, i_GuessNumber, i);
                buttonGuessResult.Size = new Size(20, 20);
                buttonGuessResult.Location = new Point(r_HiddenResultsButtons[r_HiddenResultsButtons.Count - 1].Location.X + 90 + locationOfButtonX, locationOfButtonY + ((i_GuessNumber - 1) * 50));
                buttonGuessResult.Enabled = false;
                Controls.Add(buttonGuessResult);
                listOfResultButtonsSet.Add(buttonGuessResult);
            }

            r_ResultButtons.Add(i_GuessNumber, listOfResultButtonsSet);
        }

        private void createHiddenResultsButtons()
        {
            Button buttonHiddenResult;

            for (int i = 1; i <= r_BoolPgiaGame.AmountOfColorsToGuess; i++)
            {
                buttonHiddenResult = new Button();
                buttonHiddenResult.BackColor = Color.Black;
                buttonHiddenResult.Size = new Size(40, 40);
                buttonHiddenResult.Location = new Point(18 + ((i - 1) * 46), 23);
                buttonHiddenResult.Enabled = false;
                r_HiddenResultsButtons.Add(buttonHiddenResult);
                this.Controls.Add(buttonHiddenResult);
            }
        }

        #endregion

        #region Buttons Click Methods

        private void buttonPickAGuess_Click(object sender, EventArgs e)
        {
            DialogResult pickColorDialogResult;
            PickGuessButton pickGuessButton = sender as PickGuessButton;

            if (pickGuessButton.IsGuessPicked)
            {
                onUserChangeGuessPick(sender, e);
            }

            pickColorDialogResult = m_PickColorGuessDialog.ShowDialog();
            if (pickColorDialogResult == DialogResult.OK && sender is Button)
            {
                (sender as Button).BackColor = m_PickColorGuessDialog.PickedGuessColor;
                pickGuessButton.GuessPicked();
            }

            m_CurrentGuess[pickGuessButton.GuessPickNumber - 1] = pickGuessButton.BackColor;
            checkIfUserPickAGuessToEnableTheCheckGuessButton();
        }

        private void ButtonCheckGuess_Click(object sender, EventArgs e)
        {
            int correctInPlace, correctNotInPlace;
            List<Color> correctAnswer;

            r_BoolPgiaGame.CheckGuess(m_CurrentGuess, out correctInPlace, out correctNotInPlace, out correctAnswer);
            paintTheGuessResultButtonsSet(correctInPlace, correctNotInPlace);
            if (correctAnswer != null)
            {
                disableThePrevGuessButtonsSet();
                exposeTheCorrectAnswerOfTheGame(correctAnswer);
            }
            else
            {
                disableThePrevGuessButtonsSet();
                r_BoolPgiaGame.NextGuess();
                enableTheNextGuessButtonsSet();
            }

            onCheckGuessClicked(sender, e);
            if (sender is Button)
            {
                (sender as Button).Enabled = false;
            }
        }

        #endregion

        private void disableThePrevGuessButtonsSet()
        {
            PickGuessButton guessButton;

            foreach (Button button in this.Controls)
            {
                if (button is PickGuessButton)
                {
                    guessButton = button as PickGuessButton;
                    if (guessButton.GuessNumber == r_BoolPgiaGame.CurrentGuessNumber)
                    {
                        guessButton.Enabled = false;
                    }
                }
            }
        }

        private void enableTheNextGuessButtonsSet()
        {
            PickGuessButton guessButton;

            foreach (Button button in this.Controls)
            {
                if (button is PickGuessButton)
                {
                    guessButton = button as PickGuessButton;
                    if (guessButton.GuessNumber == r_BoolPgiaGame.CurrentGuessNumber)
                    {
                        guessButton.Enabled = true;
                    }
                }
            }
        }

        private void checkIfUserPickAGuessToEnableTheCheckGuessButton()
        {
            bool isUserPickedGuess = true;
            PickGuessButton button;

            foreach (Button buttonGuess in this.Controls)
            {
                if (buttonGuess is PickGuessButton)
                {
                    button = buttonGuess as PickGuessButton;
                    if (button.GuessNumber == r_BoolPgiaGame.CurrentGuessNumber && !button.IsGuessPicked)
                    {
                        isUserPickedGuess = false;
                        break;
                    }
                }
            }

            if (isUserPickedGuess)
            {
                r_CheckGuessButtons[r_BoolPgiaGame.CurrentGuessNumber].Enabled = true;
            }
        }

        private void onUserChangeGuessPick(object sender, EventArgs e)
        {
            if (UserChangeGuessPick != null)
            {
                UserChangeGuessPick(sender, e);
            }
        }

        private void onCheckGuessClicked(object sender, EventArgs e)
        {
            if (CheckGuessClicked != null)
            {
                CheckGuessClicked(sender, e);
            }
        }

        private void paintTheGuessResultButtonsSet(int i_CorrectInPlace, int i_CorrectNotInPlace)
        {
            int i = 0;

            for (; i < i_CorrectInPlace; i++)
            {
                r_ResultButtons[r_BoolPgiaGame.CurrentGuessNumber][i].BackColor = Color.Black;
            }

            if(i_CorrectInPlace == r_BoolPgiaGame.AmountOfColorsToGuess)
            {
                exposeTheCorrectAnswerOfTheGame(m_CurrentGuess.ToList());
            }

            for (int j = 0; j < i_CorrectNotInPlace; j++)
            {
                r_ResultButtons[r_BoolPgiaGame.CurrentGuessNumber][i++].BackColor = Color.Yellow;
            }
        }

        private void exposeTheCorrectAnswerOfTheGame(List<Color> i_CorrectAnswer)
        {
            for (int i = 0; i < i_CorrectAnswer.Count; i++)
            {
                r_HiddenResultsButtons[i].BackColor = i_CorrectAnswer[i];
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(278, 244);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GameBoard";
            this.ResumeLayout(false);
        }
    }
}