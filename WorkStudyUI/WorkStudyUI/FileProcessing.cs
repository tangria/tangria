using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace WorkStudyUI
{
    class FileProcessing
    {
        string yearString = "";
        string ssn = "";
        string altIDString = "";
        string last = "";
        string first = "";
        string mid = "";
        string workStudy1 = "";
        string workStudy2 = "";
        string workStudy3 = "";
        string workStudy4 = "";
        string floatString = "";
        string cr = "\r";
        string lf = "\n";

        string tempString = ""; // not sure if it is needed
        string rowConcat = "";

        public bool SwitchProcessing(string entity, string writeToFile, bool tf, int iteration)
        {
            FieldElementEval fieldElementEval = new FieldElementEval();

            switch (iteration)
            {
                case 0: // calendar year
                    yearString = fieldElementEval.DataElementEval(entity, 4);
                    break;
                case 1: // alternate ID
                    altIDString = fieldElementEval.AltIDEval(entity, 10);
                    break;
                case 2: // Federal Work-Study YTD Earnings
                    workStudy1 = fieldElementEval.AmountConversion(entity, 7);
                    ssn = fieldElementEval.BlankSpaces(9);              // blank SSN field
                    last = fieldElementEval.BlankSpaces(16);            // blank last name field
                    first = fieldElementEval.BlankSpaces(11);           // blank first name field
                    mid = fieldElementEval.BlankSpaces(1);              // blank middle initial field
                    workStudy2 = fieldElementEval.BlankSpaces(7);       // blank YTD earnings field #1
                    workStudy3 = fieldElementEval.BlankSpaces(7);       // blank YTD earnings field #2
                    workStudy4 = fieldElementEval.BlankSpaces(7);       // blank YTD earnings field #3
                    floatString = fieldElementEval.BlankSpaces(821);    // filler field

                    //tempString = fieldElementEval.RecordCompleteString(yearString, floatString1, altIDString, floatString2, floatString3, floatString4, workStudyString, 
                    //    );
                    break;
            } // end switch

            // create a list of UCSFRecord, a collection of financial aid award records 
            //List<UCSFRecord> FinancialAidEntry = new List<UCSFRecord>();

            //// define and set all attributes for the UCSFRecord object
            //UCSFRecord field = new UCSFRecord();

            //field.Year = yearString;
            //field.SocialSecurity = ssn;
            //field.AltID = altIDString;
            //field.LastName = last;
            //field.FirstName = first;
            //field.MiddleInitial = mid;
            //field.FedWorkStudy = workStudy1;
            //field.StateWorkStudy = workStudy2;
            //field.InstWorkStudy = workStudy3;
            //field.OtherWorkStudy = workStudy4;
            //field.Filler = floatString;
            //field.CarriageReturn = cr;
            //field.LineFeed = lf;

            //// gather all elements of the UCSFRecord object & place into the UCSFRecord list (named FinancialAidEntry)
            //FinancialAidEntry.Add(field);

            List<string> awardRecord = new List<string>();
            awardRecord.Add(yearString);
            awardRecord.Add(ssn);
            awardRecord.Add(altIDString);
            awardRecord.Add(last);
            awardRecord.Add(first);
            awardRecord.Add(mid);
            awardRecord.Add(workStudy1);
            awardRecord.Add(workStudy2);
            awardRecord.Add(workStudy3);
            awardRecord.Add(workStudy4);
            awardRecord.Add(floatString);
            awardRecord.Add(cr);
            awardRecord.Add(lf);

            // TODO: may not need the final two lines if textBox3 is eliminated
            //rowConcat += tempString;
            
            /* textBox3.Text = rowConcat; */  // show user data being written to file

            rowConcat = string.Join("", awardRecord.ToArray());

            System.IO.File.WriteAllText(writeToFile, rowConcat);
            tf = true;

            return tf;

        } // end method SwitchProcessing

        public bool CSVProcessing(string sourceFile, string destinationFile)
        {
            bool success = false;

            StreamReader reader = new StreamReader(sourceFile);
            try
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] record = line.Split(',');

                    FieldElementEval fieldElementEval = new FieldElementEval();

                    // read each record (line) and parse the data
                    for (int i = 0; i < 3; i++)
                    {

                        tempString = record[i];
                        SwitchProcessing(tempString, destinationFile, success, i);

                        /*
                        //switch (i)
                        //{
                        //    case 0: // calendar year
                        //        yearString = fieldElementEval.DataElementEval(record[i], 4);
                        //        break;
                        //    case 1: // alternate ID
                        //        altIDString = fieldElementEval.AltIDEval(record[i], 10);
                        //        break;
                        //    case 2: // Federal Work-Study YTD Earnings
                        //        workStudy1 = fieldElementEval.AmountConversion(record[i], 7);
                        //        ssn = fieldElementEval.BlankSpaces(9);              // blank SSN field
                        //        last = fieldElementEval.BlankSpaces(16);            // blank last name field
                        //        first = fieldElementEval.BlankSpaces(11);           // blank first name field
                        //        mid = fieldElementEval.BlankSpaces(1);              // blank middle initial field
                        //        workStudy2 = fieldElementEval.BlankSpaces(7);       // blank YTD earnings field #1
                        //        workStudy3 = fieldElementEval.BlankSpaces(7);       // blank YTD earnings field #2
                        //        workStudy4 = fieldElementEval.BlankSpaces(7);       // blank YTD earnings field #3
                        //        floatString = fieldElementEval.BlankSpaces(821);    // filler field

                        //        //tempString = fieldElementEval.RecordCompleteString(yearString, floatString1, altIDString, floatString2, floatString3, floatString4, workStudyString, 
                        //        //    );
                        //        break;
                        //} // end switch
                        */

                    } // end for

                    /*
                    //// create a list of UCSFRecord, a collection of financial aid award records 
                    //List<UCSFRecord> FinancialAidEntry = new List<UCSFRecord>();

                    //// define and set all attributes for the UCSFRecord object
                    //UCSFRecord field = new UCSFRecord();

                    //field.Year = yearString;
                    //field.SocialSecurity = ssn;
                    //field.AltID = altIDString;
                    //field.LastName = last;
                    //field.FirstName = first;
                    //field.MiddleInitial = mid;
                    //field.FedWorkStudy = workStudy1;
                    //field.StateWorkStudy = workStudy2;
                    //field.InstWorkStudy = workStudy3;
                    //field.OtherWorkStudy = workStudy4;
                    //field.Filler = floatString;
                    //field.CarriageReturn = cr;
                    //field.LineFeed = lf;

                    //// gather all elements of the UCSFRecord instance into the UCSFRecord list (named FinancialAidEntry)
                    //FinancialAidEntry.Add(field);

                    //// TODO: may not need the final two lines if textBox3 is eliminated
                    //rowConcat += tempString;
                    //textBox3.Text = rowConcat;  // show user data being written to file

                    //System.IO.File.WriteAllText(outputFile, rowConcat);
                    */

                } // end while
                //toolStripStatusLabel1.Text = Convert.ToString("CSV parsing complete!");
            } // end try

            catch (Exception ex)
            {
                //toolStripStatusLabel1.Text = Convert.ToString("PROBLEM - see exception box"); // TODO: need to look at this error
                MessageBox.Show(ex.Message, "Program Execution Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                success = false;
            }

            finally
            {
                reader.Close();
                reader.Dispose();
            }

            return success;

        } // end method CSVProcessing

        public bool ExcelProcessing(string sourceFile, string destinationFile)
        {
            bool success = false;

            Microsoft.Office.Interop.Excel.Application test;
            Workbook book = null;
            Worksheet wkSheet = null;

            test = new Microsoft.Office.Interop.Excel.Application();
            test.Visible = false;

            try
            {
                book = test.Workbooks.Open(sourceFile);
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

                        // offset in j required since this loop starts @ 1, whereas the switch in method SwitchProcessing initilaizes @ 0
                        SwitchProcessing(tempString, destinationFile, success, --j);

                    } // end for (cols)
                } // end for (rows)
            } // end try

            catch (Exception ex)
            {
                //toolStripStatusLabel1.Text = Convert.ToString("PROBLEM - see exception box");
                MessageBox.Show(ex.Message, "Program Execution Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                success = false;
            }

            finally
            {
                test.Workbooks.Close();
            }

            return success;

        } // end method ExcelProcessing
    }
}
