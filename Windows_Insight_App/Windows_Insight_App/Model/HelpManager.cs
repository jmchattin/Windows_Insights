using System;

namespace Windows_Insight_App.Model
{
    /// <summary>
    /// Handle misc. functions that don't fit well anywhere else.
    /// </summary>
    class HelpManager
    {
        /// <summary>
        /// Determines is a string value is >0 and an integer.
        /// </summary>
        /// <param name="text">The text to test for validity.</param>
        /// <returns>Whether or not the text is a valid value.</returns>
        public static bool IsValidPositiveInt(string text)
        {
            if (text != "")
            {
                int result;
                if (!int.TryParse(text, out result) || result < 0)
                {
                    ErrorManager.ManageError(ErrorManager.PossibleErrors.Invalid_Int);
                    //System.Windows.Message Box.Show("Invalid positive integer.");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Take kilobytes and convert them into MB w/ specified decimal places to show.
        /// </summary>
        /// <param name="kbytes">The kilobytes to convert to megabytes.</param>
        /// <param name="deciPlaces">How many decimals to show.</param>
        /// <returns>The converted MB result.</returns>
        public static decimal TurnKBIntoMB(long kbytes, int deciPlaces)
        {
            decimal mbDecimal = (decimal)kbytes / 1000;
            return Math.Round(mbDecimal, deciPlaces);
        }

        /// <summary>
        /// Take bytes and convert them into MB w/ specified decimal places to show.
        /// </summary>
        /// <param name="bytes">The bytes to convert to megabytes.</param>
        /// <param name="deciPlaces">How many decimals to show.</param>
        /// <returns>The converted MB result.</returns>
        public static decimal TurnByteIntoMB(long bytes, int deciPlaces)
        {
            decimal mbDecimal = (decimal)bytes / 1000000;
            return Math.Round(mbDecimal, deciPlaces);
        }
    }
}
