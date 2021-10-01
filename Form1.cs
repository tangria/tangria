using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;

using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

/** Solution:   WorkStudyUI.sln
 *  Date:       Monday, December 28, 2015
 *  Purpose:    A UI version of UCSF_WS_Parsing.sln.
 *              Takes an MS Excel file (.xls or .xlsx) 
 *              and parases the records into a textfile
 *              that meets the specifications that the
 *              UCSF work study software requires.
 *              
 *              v.1.0 - 12/23/15 initial launch
 *
 *              v.1.1 - 12/28/15 fixed the input file check
 *              original expression was: 
 *              if ( inputFile.LastIndexOf(".xls", lengthInput) == -1 || inputFile.LastIndexOf(".xlsx", lengthInput) == -1 )
 *              So inevitably the expression would return true, since
 *              one of the criteria would return -1, preventing the
 *              user from moving forward even though they might have
 *              entered a valid file type.
 *
 *              v.1.2 - 1/4/16  MS Excel input has no SSN, Last Name,
 *              First Name, MI fields.  Removed these cases.
 *              TODO: make program robust to allow for customization of
 *              textfile output based on the fields in the input file.
 *
 *              v.1.3 - 1/5/16  Provide UI tools to select certain fields
 *              based on the input file at hand.      
 *
 *              v.1.4 - 1/10/16 Expanded to allow CSV files as input files.
 *              TODO: need to find a way to determine if cell is truly blank,
 *              searching to see if it is "" does not work.
 *              COMPLETED - implemented string.IsNullOrWhiteSpace(record[i])
 *              but, might want to consider using ClearFormats: http://stackoverflow.com/questions/1284388/how-to-get-the-range-of-occupied-cells-in-excel-sheet
 *
 *              v.1.5 - 1/11/16 Added StatusStrip to update user on parsing process.  
 *              
 *              v.1.6 - 1/15/16 Scaled down version that no longer includes
 *              RadioButtons or CheckBox options.  Streamlines input file to only 
 *              three (3) fields:
 *              1 - Award Year
 *              2 - Alternate ID
 *              3 - Federal Work-Study YTD Earnings
 *              Allowable file types are still .xls, .xlsx, and .csv.
 *              All parsing done similarly as before, except for only 3 columns.
 *              
 *              v.1.7 - 1/21/16 Spaced out the YTD Earnings to start at column 52
 *              instead of creating a single string concatenating the award year, 
 *              alternate ID, and YTD earnings without any spacing.
 *
 *              v.1.8 - 1/24/16 Revised method AmountConversion so that it accounts
 *              for negative YTD earnings.  Also cleaned up the method to avoid
 *              redundant code.
 */

namespace WorkStudyUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            /* allow user to execute the program by pressing the "Enter" key
             * "Enter" will trigger button parsefile
             * cf. http://stackoverflow.com/questions/12492515/textbox-to-accept-enter-return
             */
            this.AcceptButton = btnParseFile; 
        }
        
        public string inputFile;
        public string outputFile;
        public string createPath;

        
        static void CheckFileExists(string checkFile)
        {
            // http://stackoverflow.com/questions/7135058/c-sharp-message-box-variable-usage
            if (!File.Exists(checkFile))
            {
                DialogResult result2 = MessageBox.Show(string.Format("File cannot be found: {0}", checkFile), "Input Filename Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result2 == DialogResult.OK)
                    return;
            }
        } // end method CheckFileExists
         
        static string SSNEval(string value, int length)
        {
            // determine the number of characters in the string
            int result = 0;

            foreach (char c in value)
            {
                if (!char.IsWhiteSpace(c))
                    result++;
            }

            // if no entry in the SSN field, pad with spaces
            if (result == 0)
                value = BlankSpaces(length);

            else if (result < length)
            {
                int extraZeros = length - result;
                for (int i = 1; i <= extraZeros; i++)
                    value = '0' + value;
            }

            return value;
        } // end method IDEval (SSNEval?)
         

        static string AltIDEval(string value, int length)
        {
            int result = 0;
            foreach (char c in value)
            {
                if (!char.IsWhiteSpace(c))
                    result++;
            }

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
        }  // end method AltIDEval

        static string BlankSpaces(int length)
        {
            string value = "";
            for (int i = 1; i <= length; i++)
            {
                value += ' ';
            }

            return value;
        }

        static string DataElementEval(string value, int length)
        {

            // cf. http://www.dotnetperls.com/count-characters
            int result = 0;
            foreach (char c in value)
            {
                if (!char.IsWhiteSpace(c))
                    result++;
            }

            // pad the string with extra sapces on the right side of the string
            if (result < length)
                value = value.PadRight(length);

            return value;
        } // end method DataElementEval

        static string AmountConversion(string value, int length)
        {
            double numvalue;
            string newstringamount;
            int remainingspaces;

            numvalue = Convert.ToDouble(value);
            numvalue = Math.Round(numvalue, 2);             // round the ytd earning amount to 2 decimal places
            numvalue = numvalue * 100;                      // multiply out the decimal
            newstringamount = Convert.ToString(numvalue);

            remainingspaces = length - newstringamount.Length;

            if (value.Contains('-'))
            {
                for (int i = 1; i <= remainingspaces; i++)
                    newstringamount = ' ' + newstringamount;
            }

            else
            {
                for (int i = 1; i <= remainingspaces; i++)
                    newstringamount = '0' + newstringamount;
            }

            return newstringamount;

        } // end method amountconversion

        static string RecordCompleteString(string a, string b, string c, string d, string e, string f)
        {
            /** append a carriage return ("\r") & line feed ("\n") on the rowconcat string - per program requirements
             *  cf. http://www.dotnetperls.com/newline, http://stackoverflow.com/questions/10096100/add-carriage-return-to-a-string
             *  rowconcat += "\r\n"; <-- most trustworthy & reliable
             */
            //string rowcomplete = a + b + c + d + e + f + g + h + i + j + k + "\r\n";
            string rowcomplete = a + b + c + d + e + f + "\r\n";
            return rowcomplete;
        }

        private void btnFilePathIn_Click(object sender, EventArgs e)
        {
            /* browse file button
             * cf. http://stackoverflow.com/questions/4999734/how-to-add-browse-file-button-to-windows-form-using-c-sharp
             */
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtInputFile.Text = openFileDialog1.FileName;
            }
        } // end AE method filePath_Click (btnFilePathIn_Click?)

        // TODO: need to work on this
        // http://www.dotnetperls.com/savefiledialog
        private void btnFilePathOut_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        } // end AE method btnFilePathOut_Click

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtOutputFile.Text = saveFileDialog1.FileName;
        } // end method saveFileDialog1_FileOk


        private void parseFile_Click(object sender, EventArgs e)
        {
            // -----try
            // -----{
                
                // verify that the string is a valid MS Excel document
                inputFile = "";   // clear variable contents
                inputFile = txtInputFile.Text;
                
            /*
                ValidateFile validateFilePaths = new ValidateFile();

                string inputFileExtension = inputFile.Substring(inputFile.LastIndexOf('.') + 1, inputFile.Length - (inputFile.LastIndexOf('.') + 1));

                validateFilePaths.InputFileStringCheck(inputFile);
                validateFilePaths.ValidateInputFile(inputFileExtension);
                validateFilePaths.CheckInputFileExists(inputFile);

                outputFile = txtOutputFile.Text;

                validateFilePaths.OuputFileStringCheck(outputFile);
                validateFilePaths.ValidateOutputFile(outputFile);
                validateFilePaths.CheckOutputFileExists(outputFile); // <--- don't really need 
                */

                // original code - can uncomment lines 291-339
                
                int lengthInput = inputFile.Length;
                //textBox3.Text = string.Format("length of input file is ", inputFile);

                if (lengthInput == 0)
                {
                    DialogResult result = MessageBox.Show("No input file entered.", "Input Filename Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                        return;
                }

                /** use Substring method to pick off the extension of the input file name
                 *  this will be used to determine if it is a valid file type
                 */
                string inputFileExtension = inputFile.Substring(inputFile.LastIndexOf('.') + 1, inputFile.Length - (inputFile.LastIndexOf('.') + 1));

                if (inputFileExtension == "xls" || inputFileExtension == "xlsx" || inputFileExtension == "csv")
                    CheckFileExists(inputFile);

                else
                {
                    DialogResult result1 = MessageBox.Show("Incorrect input file format.\nFile must be either .xls, .xlsx, or .csv", "Input Filename Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (result1 == DialogResult.OK)
                        return;
                }

                outputFile = txtOutputFile.Text;

                 //if there is a string in the output file text field, validate

                int lengthOutput = outputFile.Length;

                if (lengthOutput == 0)
                {
                    DialogResult result5 = MessageBox.Show("No outoing text file entered.", "Output Filename Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (result5 == DialogResult.OK)
                        return;
                }

                else if (outputFile.LastIndexOf(".txt", lengthOutput) == -1)
                {
                    DialogResult result6 = MessageBox.Show("Incorrect output file format.\nFile must have a .txt extension.", "Output Filename Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (result6 == DialogResult.OK)
                        return;
                }

                // no longer needed with SaveFileDialog - can delete it
                /*
                if (File.Exists(outputFile))
                {
                    DialogResult result7 = MessageBox.Show(string.Format("WARNING - output text file {0} exists.\nProceeding will result in loss of data.\nAre you SURE you want to continue?", outputFile), "Potential File Override", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result7 == DialogResult.No)
                        return;
                }
                */
                 //end comment section - can uncomment

                textBox3.Clear();
                toolStripStatusLabel1.Text = "";
                statusStrip1.Refresh();

            
                string rowConcat = null;
                string tempString = null;
                string yearString = null;
                string altIDString = null;

                string workStudyString = null;
                string floatString1 = null;
                string floatString2 = null;
                string floatString3 = null;
              
                //string extraPaddingString1 = null;
                
            /*
                FileProcessing inputFileType = new FileProcessing();

                switch (inputFileExtension)
                {
                    case "csv":
                        inputFileType.CSVProcessing(inputFile, outputFile);

                        if (inputFileType.CSVProcessing(inputFile, outputFile) == true)
                            toolStripStatusLabel1.Text = Convert.ToString("CSV parsing complete!");
                        else
                            toolStripStatusLabel1.Text = Convert.ToString("PROBLEM - see exception box");
                        break;
                    case "xls": case "xlsx":
                        inputFileType.ExcelProcessing(inputFile, outputFile);

                        if (inputFileType.ExcelProcessing(inputFile, outputFile) == true)
                            toolStripStatusLabel1.Text = Convert.ToString("CSV parsing complete!");
                        else if (inputFileType.ExcelProcessing(inputFile, outputFile) == false)
                            toolStripStatusLabel1.Text = Convert.ToString("PROBLEM - see exception box");
                        break;
                }
             */


                
                if (inputFileExtension == "csv")
                {
                    StreamReader reader = new StreamReader(inputFile);
                    try
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                                string[] record = line.Split(',');

                                // read each record (line) and parse the data
                                for (int i = 0; i < 3; i++)
                                {
                                    switch (i)
                                    {
                                        case 0: // calendar year
                                            yearString = DataElementEval(record[i], 4);
                                            break;
                                        case 1: // alternate ID
                                            altIDString = AltIDEval(record[i], 10);
                                            break;
                                        case 2: // Federal Work-Study YTD Earnings
                                            workStudyString = AmountConversion(record[i], 7);
                                            floatString1 = BlankSpaces(9);
                                            floatString2 = BlankSpaces(28);
                                            floatString3 = BlankSpaces(842);
                                            tempString = RecordCompleteString(yearString, floatString1, altIDString, floatString2, workStudyString, floatString3);
                                            break;
                                    } // end switch
                                } // end for

                                rowConcat += tempString;
                                textBox3.Text = rowConcat;  // show user data being written to file
                                System.IO.File.WriteAllText(outputFile, rowConcat);

                        } // end while
                        toolStripStatusLabel1.Text = Convert.ToString("CSV parsing complete!");
                    } // end try #2a (for CSV StreamReader object)

                    catch (Exception ex)
                    {
                        toolStripStatusLabel1.Text = Convert.ToString("PROBLEM - see exception box");
                        MessageBox.Show(ex.Message, "Program Execution Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    finally
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                } // end if
                 

            
                else
                {
                    Microsoft.Office.Interop.Excel.Application test;
                    Workbook book = null;
                    Worksheet wkSheet = null;

                    test = new Microsoft.Office.Interop.Excel.Application();
                    test.Visible = false;

                    try
                    {
                        book = test.Workbooks.Open(inputFile);
                        wkSheet = book.Sheets[1];

                        // http://stackoverflow.com/questions/9621687/what-type-does-excel-worksheet-usedrange-return
                        // (string)xlSheet.Cells[row,col].Value2 or xlSheet.Cells[row,col].Value2.ToString();

                        Range excelRange = wkSheet.UsedRange;

                        // instantiate rows & cols for Range object excelRange
                        int rows = excelRange.Rows.Count;
                        int cols = 3;

                        for (int i = 1; i <= rows; i++)
                        {
                            if (excelRange.Cells[i, 1].Value == null)
                                break;

                            for (int j = 1; j <= cols; j++)
                            {
                                if (excelRange.Cells[i, j].Value == null)
                                    tempString = "";
                                else
                                    tempString = excelRange.Cells[i, j].Value2.ToString();

                                switch (j)
                                {
                                    case 1: // calendar year
                                        yearString = DataElementEval(tempString, 4);
                                        break;
                                    case 2: // alternate ID
                                        altIDString = AltIDEval(tempString, 10);
                                        break;
                                    case 3: // Federal Work-Study YTD Earnings
                                        workStudyString = AmountConversion(tempString, 7);
                                        floatString1 = BlankSpaces(9);
                                        floatString2 = BlankSpaces(28);
                                        floatString3 = BlankSpaces(842);
                                        tempString = RecordCompleteString(yearString, floatString1, altIDString, floatString2, workStudyString, floatString3);
                                        break;
                                } // end switch
                            } // end for

                            rowConcat += tempString;
                            // https://msdn.microsoft.com/en-us/library/8bh11f1k.aspx
                            System.IO.File.WriteAllText(outputFile, rowConcat);
                            textBox3.Text = rowConcat;  // show user data being written to file on UI
                            
                        } // end for (rows)
                        
                        toolStripStatusLabel1.Text = Convert.ToString("MS Excel parsing complete!");
                        
                    } // end try #2b (for MS Excel Workbook object)

                    catch (Exception ex)
                    {
                        toolStripStatusLabel1.Text = Convert.ToString("PROBLEM - see exception box");
                        MessageBox.Show(ex.Message, "Program Execution Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    finally
                    {
                        test.Workbooks.Close();
                    }
                } // end else .xls/.xlsx
            
            // -----} // end try #1 (for the input & output files that the user enters into the TextBoxes)

            // cf. http://www.codeproject.com/Articles/154121/Using-Try-Catch-Finally
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}


        } // end AE method parseFile_Click

    } // end class Form1

} // end namespace WorkStudyUI