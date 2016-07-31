using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestASPNETv0._0
{
    public partial class formpreanalisis : System.Web.UI.Page
    {

        /**
         * atribut
         */
        public static List<string> LIST_OF_DINAS = new List<string>();
        public static List<string> LIST_OF_PDAM_keywords = new List<string>();    
        public static List<string> LIST_OF_SOSIAL_keywords = new List<string>();  
        public static List<string> LIST_OF_KEBERSIHAN_keywords = new List<string>();
        public static List<string> LIST_OF_PERTAMANAN_keywords = new List<string>();
        public static List<string> LIST_OF_DINKOMINFO_keywords = new List<string>();

        public static string jenisAlgo;
        public static int maxTokenAmount;

        /**
         * masuk ke prosedur ini setelah button Analisis diklik
         */
        protected void Page_Load(object sender, EventArgs e)
        {

            RadioButtonList radAlgo = ((RadioButtonList)Page.PreviousPage.FindControl("optAlgoritma"));
            LabelJenisAlgo.Text = radAlgo.SelectedValue.ToString();
            jenisAlgo = LabelJenisAlgo.Text;

            // inisiasi
            LIST_OF_PDAM_keywords.Clear();
            LIST_OF_SOSIAL_keywords.Clear();
            LIST_OF_KEBERSIHAN_keywords.Clear();
            LIST_OF_PERTAMANAN_keywords.Clear();
            LIST_OF_DINKOMINFO_keywords.Clear();

            // menyimpan nama dinas ke dalam list
            LIST_OF_DINAS.Clear();
            LIST_OF_DINAS.Add("PDAM");
            LIST_OF_DINAS.Add("SOSIAL");
            LIST_OF_DINAS.Add("KEBERSIHAN");
            LIST_OF_DINAS.Add("PERTAMANAN");
            LIST_OF_DINAS.Add("DINKOMINFO");
            LIST_OF_DINAS.Add("UNKNOWN");

            // memulai pengambilan keyword setiap dinas
            // menyimpannya ke dalam list yang sesuai
            ExtractToken_MASTER();

            // print hasil ekstrak token ke layar
            PrintListToken_MASTER();

            // sinkronisasi data dari code behind dengan aspx page
            Page.DataBind();

        }

        /**
         * METHODS
         */
        public void ExtractToken_MASTER()
        {
            // ambil keyword (token) dari dinas PDAM yang dipisahkan dengan tanda ;
            ExtractToken("keywordPDAM");

            // ambil keyword (token) dari dinas SOSIAL yang dipisahkan dengan tanda ;
            ExtractToken("keywordSOSIAL");

            // ambil keyword (token) dari dinas KEBERSIHAN yang dipisahkan dengan tanda ;
            ExtractToken("keywordKEBERSIHAN");

            // ambil keyword (token) dari dinas PERTAMANAN yang dipisahkan dengan tanda ;
            ExtractToken("keywordPERTAMANAN");

            // ambil keyword (token) dari dinas DINKOMINFO yang dipisahkan dengan tanda ;
            ExtractToken("keywordKOMINFO");
        }

        public void ExtractToken(string namaDinas)
        {
           
            //KAMUS
            string isiTextBox = ((TextBox)Page.PreviousPage.FindControl(namaDinas)).Text;
            string[] arrOfToken;
            char[] delimiters = { ';' };
           
            //ALGORITMA
            try
            {
                //mengambil token 
                arrOfToken = isiTextBox.Split(delimiters);

                //menyimpan token ke dalam list yang sesuai
                foreach (string s in arrOfToken)
                {
                    if (namaDinas == "keywordPDAM")
                    {
                        LIST_OF_PDAM_keywords.Add(s);
                    }
                    else if (namaDinas == "keywordSOSIAL")
                    {
                        LIST_OF_SOSIAL_keywords.Add(s);
                    }
                    else if (namaDinas == "keywordKEBERSIHAN")
                    {
                        LIST_OF_KEBERSIHAN_keywords.Add(s);
                    }
                    else if (namaDinas == "keywordPERTAMANAN")
                    {
                        LIST_OF_PERTAMANAN_keywords.Add(s);
                    }
                    else if (namaDinas == "keywordKOMINFO")
                    {
                        LIST_OF_DINKOMINFO_keywords.Add(s);
                    }    
                }
            }
            catch (NullReferenceException ex)
            {
                
            }

        }

        public void PrintListToken_MASTER()
        {
            int maxNBToken;

            //menampilkan daftar token setiap dinas
            PrintListToken("PDAM");
            PrintListToken("SOSIAL");
            PrintListToken("KEBERSIHAN");
            PrintListToken("PERTAMANAN");
            PrintListToken("DINKOMINFO");

            //merge column
            maxNBToken = getMaxNumberOfToken();
            maxTokenAmount = maxNBToken;
            for (int i = 1; i <= 5; i++) {

                if (i == 1)
                {
                    TokenTable.Rows[i].Cells[LIST_OF_PDAM_keywords.Count()].ColumnSpan = maxNBToken - LIST_OF_PDAM_keywords.Count() + 1;
                }
                else if (i == 2)
                {
                    TokenTable.Rows[i].Cells[LIST_OF_SOSIAL_keywords.Count()].ColumnSpan = maxNBToken - LIST_OF_SOSIAL_keywords.Count() + 1;
                }
                else if (i == 3)
                {
                    TokenTable.Rows[i].Cells[LIST_OF_KEBERSIHAN_keywords.Count()].ColumnSpan = maxNBToken - LIST_OF_KEBERSIHAN_keywords.Count() + 1;
                }
                else if (i == 4)
                {
                    TokenTable.Rows[i].Cells[LIST_OF_PERTAMANAN_keywords.Count()].ColumnSpan = maxNBToken - LIST_OF_PERTAMANAN_keywords.Count() + 1;
                }
                else if (i == 5)
                {
                    TokenTable.Rows[i].Cells[LIST_OF_DINKOMINFO_keywords.Count()].ColumnSpan = maxNBToken - LIST_OF_DINKOMINFO_keywords.Count() + 1;
                }
                //TokenTable.Rows[i].Cells.RemoveAt(i + 1);
            }
        }

        public void PrintListToken(string namaDinas)
        {

            //KAMUS
            List<string> tempBUFFER = new List<string>();

            //ALGORITMA
            if (namaDinas == "PDAM")
            {
                tempBUFFER = LIST_OF_PDAM_keywords;
            }
            else if (namaDinas == "SOSIAL")
            {
                tempBUFFER = LIST_OF_SOSIAL_keywords;
            }
            else if (namaDinas == "KEBERSIHAN")
            {
                tempBUFFER = LIST_OF_KEBERSIHAN_keywords;
            }
            else if (namaDinas == "PERTAMANAN")
            {
                tempBUFFER = LIST_OF_PERTAMANAN_keywords;
            }
            else if (namaDinas == "DINKOMINFO")
            {
                tempBUFFER = LIST_OF_DINKOMINFO_keywords;
            }
            
            // Create new row and add it to the table.
            TableRow tRow = new TableRow();
            TokenTable.Rows.Add(tRow);

            TableCell tCell = new TableCell();

            tCell = new TableCell();
            tCell.Text = @"<b>" + namaDinas + @"</b>";

            tRow.Cells.Add(tCell);

            foreach (var token in tempBUFFER)
            {
                tCell = new TableCell();
                tCell.Text = @"<b>" + token + @"</b>";
                tRow.Cells.Add(tCell);
            }
            
        }

        public int getMaxNumberOfToken()
        {
            //KAMUS
            List<int> list_NUMBER_OF_TOKEN = new List<int>();
            int max;

            //ALGORITMA
            list_NUMBER_OF_TOKEN.Add(LIST_OF_PDAM_keywords.Count());
            list_NUMBER_OF_TOKEN.Add(LIST_OF_SOSIAL_keywords.Count());
            list_NUMBER_OF_TOKEN.Add(LIST_OF_KEBERSIHAN_keywords.Count());
            list_NUMBER_OF_TOKEN.Add(LIST_OF_PERTAMANAN_keywords.Count());
            list_NUMBER_OF_TOKEN.Add(LIST_OF_DINKOMINFO_keywords.Count());

            max = list_NUMBER_OF_TOKEN[0];
            foreach (var maxValue in list_NUMBER_OF_TOKEN)
            {
                if (max >= maxValue)
                {
                    max = max;
                }
                else
                {
                    max = maxValue;
                }
            }
            return max;
        }

        protected void StartAnalisis_Click(object sender, EventArgs e)
        {
            //Response.Redirect("formanalisis.aspx");
            Page.Server.Transfer("formanalisis.aspx");
        }

    }
}