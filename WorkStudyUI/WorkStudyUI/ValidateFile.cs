using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.IO;

namespace WorkStudyUI
{
    class ValidateFile
    {
        public void InputFileStringCheck(string textBoxString)
        {
            int lengthInput = textBoxString.Length;
            if (lengthInput == 0)
            {
                DialogResult result = MessageBox.Show("No input file entered.", "Input Filename Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                    return;
            }
        }  // end method CheckInputFileField

        public void ValidateInputFile(string inputExtension)
        {
            string inputFileExtension = inputExtension.Substring(inputExtension.LastIndexOf('.') + 1, inputExtension.Length - (inputExtension.LastIndexOf('.') + 1));
            if (inputFileExtension != "xls" || inputFileExtension != "xlsx" || inputFileExtension != "csv")
            {
                DialogResult result = MessageBox.Show("Incorrect input file format.\nFile must be either .xls, .xlsx, or .csv", "Input Filename Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                    return;
            }
        }  // end method InputFileFormat

        public void CheckInputFileExists(string sourceFile)
        {
            // http://stackoverflow.com/questions/7135058/c-sharp-message-box-variable-usage
            if (!File.Exists(sourceFile))
            {
                DialogResult result = MessageBox.Show(string.Format("File cannot be found: {0}", sourceFile), "Input Filename Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                    return;
            }
        }  // end method CheckFileExists

        public void OuputFileStringCheck(string textBoxString)
        {
            int lengthOutput = textBoxString.Length;
            if (lengthOutput == 0)
            {
                DialogResult result = MessageBox.Show("No output text file entered.", "Output Filename Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                    return;
            }
        }  // end method CheckOutputFileField

        public void ValidateOutputFile(string destinationFile)
        {
            int lengthOutput = destinationFile.Length;
            if (destinationFile.LastIndexOf(".txt", lengthOutput) == -1)
            {
                DialogResult result = MessageBox.Show("Incorrect output file format.\nFile must have a .txt extension.", "Output Filename Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                    return;
            }
        }  // end method OutputFileFormat

        public void CheckOutputFileExists(string destinationFile)
        {
            if (File.Exists(destinationFile))
            {
                DialogResult result = MessageBox.Show(string.Format("WARNING - output text file {0} exists.\nProceeding will result in loss of data.\nAre you SURE you want to continue?", destinationFile),
                    "Potential File Override", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                    return;
            }
        }  // end method OutputFileExists

    }  // end class ValidateFile

}  // end namespace WorkStudyUI
