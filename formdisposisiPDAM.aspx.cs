﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;

namespace TestASPNETv0._0
{
    public partial class formdisposisiPDAM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelJumlahTweetPDAM.Text = formanalisis.ListOfJUMLAHTWEET_DISPOSISI[0].ToString();
            printHasilDisposisi();
        }

        /**
         * METHODS
         */

        public void highlightTheFoundKey(int idxctr)
        {
            //KAMUS
            int idxFound;
            string[] arrOfKeyWord;
            string tmpBUFF;
            string formattedSTR;
            string cartBUFF;
            char[] delimiters = { ' ' };

          
            //ALGORITMA
            try
            {
                //mengambil token 
                arrOfKeyWord = (formanalisis.KEYWORD_DISPOStweet_PDAM[idxctr]).Split(delimiters);

                //tweet dalam format plain text
                tmpBUFF = formtarget.currentTweets_STRINGFORMATTED[formanalisis.IDX_DISPOStweet_PDAM[idxctr]];

                //tmpBUFF = tmpBUFF + "\0";

                foreach (string ss in arrOfKeyWord)
                {
                    idxFound = tmpBUFF.IndexOf(ss, StringComparison.CurrentCultureIgnoreCase);
                    cartBUFF = ss;
                    /*
                    cartBUFF = "";
                    if (idxFound + (ss.Length) - 1 >= formtarget.currentTweets_STRINGFORMATTED[formanalisis.IDX_DISPOStweet_PDAM[idxctr]].Length)
                    {
                        cartBUFF = ss;
                    }
                    else
                    {
                        for (int i = idxFound; i <= idxFound + (ss.Length) - 1; i++)
                        {
                            cartBUFF = cartBUFF + tmpBUFF[i];
                        }
                    }
                    */
                    formtarget.currentTweets_STRINGFORMATTED[formanalisis.IDX_DISPOStweet_PDAM[idxctr]] =
                    Regex.Replace(tmpBUFF, ss, @"<font color='red'>" + cartBUFF + @"</font>", RegexOptions.IgnoreCase);
                    tmpBUFF = formtarget.currentTweets_STRINGFORMATTED[formanalisis.IDX_DISPOStweet_PDAM[idxctr]];
                    //tmpBUFF = tmpBUFF + "\0";
                }
                
            }
            catch (NullReferenceException ex)
            {

            }
        }

        public void printHasilDisposisi()
        {
            //KAMUS
            int cellCtr;
            int idxCOUNTER = 0;

            //ALGORITMA
            for (int idx = 0; idx < formanalisis.ListOfJUMLAHTWEET_DISPOSISI[0]; idx++)
            {
                // Create new row and add it to the table.
                TableRow tRow = new TableRow();
                DispositionPDAMTable.Rows.Add(tRow);
                for (cellCtr = 0; cellCtr < 3; cellCtr++)
                {
                    // Create a new cell and add it to the row.
                    TableCell tCell = new TableCell();
                    if (cellCtr == 0)
                    {
                        tCell.Text = formanalisis.IDX_DISPOStweet_PDAM[idxCOUNTER].ToString();
                    }
                    else if (cellCtr == 1)
                    {
                        if (formtarget.jenisTAMPILAN_TWEET == "Plain text")
                        {
                            highlightTheFoundKey(idxCOUNTER);
                            tCell.Text = formtarget.currentTweets_STRINGFORMATTED[formanalisis.IDX_DISPOStweet_PDAM[idxCOUNTER]];
                        }
                        else
                        {
                            tCell.Text = formtarget.arrTweetsHTML[formanalisis.IDX_DISPOStweet_PDAM[idxCOUNTER]].Html;
                        }
                    }
                    else if (cellCtr == 2)
                    {
                        tCell.Text = formanalisis.KEYWORD_DISPOStweet_PDAM[idxCOUNTER];
                    }
                    tRow.Cells.Add(tCell);
                }
                idxCOUNTER++;
            }
        }
    }
}