using System;
using System.Collections.Generic;
using System.Drawing;

namespace BoolPgiaLogic
{
    public class BoolPgiaGame
    {
        #region Members

        private const int k_AmountOfColorsToGuess = 4;
        // $G$ CSS-999 (-5) Bad readonly members variable name (should be in the form of r_PamelCase).
        private readonly int k_AmountOfGuesses;
        private readonly List<Color> k_ListOfAllGuessOptions;
        private readonly List<Color> r_CorrectAnswerOfTheGame = new List<Color>();
        private readonly Random r_RandomForCorrectGuess = new Random();
        private int m_CurrentGuessNumber;

        #endregion

        #region Properties

        public int CurrentGuessNumber
        {
            get
            {
                return m_CurrentGuessNumber;
            }
        }

        public int AmountOfColorsToGuess
        {
            get
            {
                return k_AmountOfColorsToGuess;
            }
        }

        #endregion

        #region Constructor

        public BoolPgiaGame(int i_AmountOfGuesses, List<Color> i_AllGuessOption)
        {
            k_AmountOfGuesses = i_AmountOfGuesses;
            m_CurrentGuessNumber = 1;
            k_ListOfAllGuessOptions = i_AllGuessOption;
            randomCorrectAnswerOfGame();
        }

        #endregion

        #region Methods

        private void randomCorrectAnswerOfGame()
        {
            List<Color> copyOfListOfAllGuessOptions = k_ListOfAllGuessOptions;
            int randomIndexThatWasRandomed;

            for (int i = 0; i < k_AmountOfColorsToGuess; i++)
            {
                randomIndexThatWasRandomed = r_RandomForCorrectGuess.Next(copyOfListOfAllGuessOptions.Count);
                r_CorrectAnswerOfTheGame.Add(copyOfListOfAllGuessOptions[randomIndexThatWasRandomed]);
                copyOfListOfAllGuessOptions.RemoveAt(randomIndexThatWasRandomed);
            }
        }

        public void CheckGuess(Color[] i_Guess, out int io_CorrectInPlace, out int io_CorrectNotInPlace, out List<Color> o_CorrectAnswer)
        {
            io_CorrectInPlace = io_CorrectNotInPlace = 0;

            for (int i = 0; i < k_AmountOfColorsToGuess; i++)
            {
                if (i_Guess[i] == r_CorrectAnswerOfTheGame[i])
                {
                    io_CorrectInPlace++;
                }
                else if (r_CorrectAnswerOfTheGame.Contains(i_Guess[i]))
                {
                    io_CorrectNotInPlace++;
                }
            }

            o_CorrectAnswer = io_CorrectInPlace == k_AmountOfColorsToGuess || m_CurrentGuessNumber == k_AmountOfGuesses ? r_CorrectAnswerOfTheGame : null;
        }

        public void NextGuess()
        {
            m_CurrentGuessNumber++;
        }

        #endregion
    }
}
