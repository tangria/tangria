using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkStudyUI
{
    class FieldElementEval
    {
        private int result = 0;
        public int CharacterCount(string field)
        {
            // cf. http://www.dotnetperls.com/count-characters
            int nonWhiteSpace = 0; // counter
            foreach (char c in field)
            {
                if (!char.IsWhiteSpace(c))
                    nonWhiteSpace++;
            }

            return nonWhiteSpace;
        } // end method CharacterCount

        public string BlankSpaces(int length)
        {
            string value = "";
            for (int i = 1; i <= length; i++)
            {
                value += ' ';
            }

            return value;
        } // end method BlankSpaces

        static string SpacePadding(string value, int length)    // probably need to delete this method because its redundancy to method BlankSpaces
        {
            for (int i = 1; i <= length; i++)
                value += ' ';

            return value;
        } // end SpacePadding

        //public string RecordCompleteString(string a, string b, string c, string d, string e, string f)
        //{
        //    /** append a carriage return ("\r") & line feed ("\n") on the rowConcat string - per program requirements
        //     *  cf. http://www.dotnetperls.com/newline, http://stackoverflow.com/questions/10096100/add-carriage-return-to-a-string
        //     *  rowConcat += "\r\n"; <-- most trustworthy & reliable
        //     */
        //    //string rowComplete = a + b + c + d + e + f + g + h + i + j + k + "\r\n";
        //    string rowComplete = a + b + c + d + e + f + "\r\n";
        //    return rowComplete;
        //}

        public string DataElementEval(string value, int length)
        {
            result = CharacterCount(value);

            // pad the string with extra sapces on the right side of the string
            if (result < length)
                value = value.PadRight(length);

            return value;
        } // end method DataElementEval

        public string AltIDEval(string value, int length)
        {
            result = CharacterCount(value);

            if (result < length)
            {
                int extraZeros = length - result;
                string charValue = value.Substring(0, 2);
                string numericValue = value.Substring(2, value.Length - 2);
                for (int i = 1; i <= extraZeros; i++)
                    numericValue = numericValue + ' ';

                value = string.Concat(charValue, numericValue);
            }
            return value;
        } // end method AltIDEval

        public string AmountConversion(string value, int length)
        {
            double numValue;
            string newStringAmount;
            int remainingSpaces;

            /* Conversion Strategy:
             * 
             * 1) cast value to double
             * 2) round to two decimals
             * 3) multiply by 100 to remove the decimal
             * 4) re-cast the amount back to string
             * 
             */ 
            numValue = Convert.ToDouble(value);
            numValue = Math.Round(numValue, 2);             // round the YTD earning amount to 2 decimal places
            numValue = numValue * 100;                      // multiply out the decimal
            newStringAmount = Convert.ToString(numValue);

            // evaluate the new string newStringAmount to see if it meets length requirements (as passed into the method)
            remainingSpaces = length - newStringAmount.Length;

            // account for the case if there is a negative value (unlikely situation)
            if (value.Contains('-'))
            {
                for (int i = 1; i <= remainingSpaces; i++)
                    newStringAmount = ' ' + newStringAmount;
            }

            else if (remainingSpaces > 0)
            {
                for (int i = 1; i <= remainingSpaces; i++)
                    newStringAmount = '0' + newStringAmount;
            }

            return newStringAmount;
        } // end method AmountConversion

    }
}
