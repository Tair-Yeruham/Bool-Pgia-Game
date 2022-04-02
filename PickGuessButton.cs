using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoolPgiaGameUI
{
    public class PickGuessButton : Button
    {
        #region Members

        private readonly int m_GuessNumber;
        private readonly int m_GuessPickNumber;
        private bool m_IsGuessPicked;

        #endregion

        #region Properties

        public bool IsGuessPicked
        {
            get
            {
                return m_IsGuessPicked;
            }
        }

        public int GuessPickNumber
        {
            get
            {
                return m_GuessPickNumber;
            }
        }

        public int GuessNumber
        {
            get
            {
                return m_GuessNumber;
            }
        }

        #endregion

        #region Constructor

        public PickGuessButton(int i_GuessNumber, int i_GuessPickNumber)
        {
            m_IsGuessPicked = false;
            m_GuessNumber = i_GuessNumber;
            m_GuessPickNumber = i_GuessPickNumber;
        }

        #endregion

        public void GuessPicked()
        {
            m_IsGuessPicked = true;
        }
    }
}
