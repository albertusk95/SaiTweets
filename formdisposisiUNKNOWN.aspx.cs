using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestASPNETv0._0
{
    public partial class formdisposisiUNKNOWN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelJumlahTweetUNKNOWN.Text = formanalisis.ListOfJUMLAHTWEET_DISPOSISI[5].ToString();
            printHasilDisposisi();
        }

        /**
       * METHODS
       */
        public void printHasilDisposisi()
        {
            int cellCtr;
            int idxCOUNTER = 0;

            for (int idx = 0; idx < formanalisis.ListOfJUMLAHTWEET_DISPOSISI[5]; idx++)
            {
                // Create new row and add it to the table.
                TableRow tRow = new TableRow();
                DispositionUNKNOWNTable.Rows.Add(tRow);
                for (cellCtr = 0; cellCtr < 3; cellCtr++)
                {
                    // Create a new cell and add it to the row.
                    TableCell tCell = new TableCell();
                    if (cellCtr == 0)
                    {
                        tCell.Text = formanalisis.IDX_DISPOStweet_UNKNOWN[idxCOUNTER].ToString();
                    }
                    else if (cellCtr == 1)
                    {
                        if (formtarget.jenisTAMPILAN_TWEET == "Plain text")
                        {
                            tCell.Text = formtarget.currentTweets_STRINGFORMATTED[formanalisis.IDX_DISPOStweet_UNKNOWN[idxCOUNTER]];
                        }
                        else
                        {
                            tCell.Text = formtarget.arrTweetsHTML[formanalisis.IDX_DISPOStweet_UNKNOWN[idxCOUNTER]].Html;
                        }
                    }
                    else if (cellCtr == 2)
                    {
                        tCell.Text = formanalisis.KEYWORD_DISPOStweet_UNKNOWN[idxCOUNTER];
                    }
                    tRow.Cells.Add(tCell);
                }
                idxCOUNTER++;
            }
        }

    }
}