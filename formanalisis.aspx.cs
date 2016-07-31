using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;

namespace TestASPNETv0._0
{
    public partial class formanalisis : System.Web.UI.Page
    {
        /** 
         * Atribut algoritma KMP
         */
        private List<int> data = new List<int>();   // data prefix KMP      
        
        /**
         * Atribut algoritma Boyer-Moore
         */
        private char[] teks;                        // teks tweet 
        private char[] cek;                         // pola yang akan dicari dalam teks tweet
        private char[] simpan;

        private int panjangteks;                    // panjang teks tweet
        private int panjangcek;                     // panjang pola


        /**
         * Atribut lain
         */

        // list yang menyimpan jumlah tweet terdisposisi untuk setiap dinas
        public static List<int> ListOfJUMLAHTWEET_DISPOSISI = new List<int>();
        // list yang menyimpan indeks minimum ditemukannya keyword untuk suatu dinas
        public static List<int> IDX_DISPOStweet_PDAM = new List<int>();
        public static List<int> IDX_DISPOStweet_SOSIAL = new List<int>();
        public static List<int> IDX_DISPOStweet_KEBERSIHAN = new List<int>();
        public static List<int> IDX_DISPOStweet_PERTAMANAN = new List<int>();
        public static List<int> IDX_DISPOStweet_DINKOMINFO = new List<int>();
        public static List<int> IDX_DISPOStweet_UNKNOWN = new List<int>();
        // list yang menyimpan daftar keyword dalam semua tweet yang didisposisikan untuk suatu tweet
        public static List<string> KEYWORD_DISPOStweet_PDAM = new List<string>();
        public static List<string> KEYWORD_DISPOStweet_SOSIAL = new List<string>();
        public static List<string> KEYWORD_DISPOStweet_KEBERSIHAN = new List<string>();
        public static List<string> KEYWORD_DISPOStweet_PERTAMANAN = new List<string>();
        public static List<string> KEYWORD_DISPOStweet_DINKOMINFO = new List<string>();
        public static List<string> KEYWORD_DISPOStweet_UNKNOWN = new List<string>();

        // variabel yang menyimpan jumlah tweet untuk setiap dinas saat proses pemanggilan KMP atau BM
        private int jumlahTWEET_PDAM;
        private int jumlahTWEET_SOSIAL;
        private int jumlahTWEET_KEBERSIHAN;
        private int jumlahTWEET_PERTAMANAN;
        private int jumlahTWEET_DINKOMINFO;
        private int jumlahTWEET_UNKNOWN;

        protected void Page_Load(object sender, EventArgs e)
        {

            LabelJenisAlgoAnalisis.Text = formpreanalisis.jenisAlgo;
            
            // inisiasi list
            ListOfJUMLAHTWEET_DISPOSISI.Clear();
            IDX_DISPOStweet_PDAM.Clear();
            IDX_DISPOStweet_SOSIAL.Clear();
            IDX_DISPOStweet_KEBERSIHAN.Clear();
            IDX_DISPOStweet_PERTAMANAN.Clear();
            IDX_DISPOStweet_DINKOMINFO.Clear();
            IDX_DISPOStweet_UNKNOWN.Clear();
            KEYWORD_DISPOStweet_PDAM.Clear();
            KEYWORD_DISPOStweet_SOSIAL.Clear();
            KEYWORD_DISPOStweet_KEBERSIHAN.Clear();
            KEYWORD_DISPOStweet_PERTAMANAN.Clear();
            KEYWORD_DISPOStweet_DINKOMINFO.Clear();
            KEYWORD_DISPOStweet_UNKNOWN.Clear();

            /**
             * inisiasi atribut
             */
            jumlahTWEET_PDAM = 0;
            jumlahTWEET_SOSIAL = 0;
            jumlahTWEET_KEBERSIHAN = 0;
            jumlahTWEET_PERTAMANAN = 0;
            jumlahTWEET_DINKOMINFO = 0;
            jumlahTWEET_UNKNOWN = 0;

            /**
             * mulai proses pencocokan keyword
             * setiap tweet akan diproses sebanyak 5 kali untuk mencari semua keyword
             * dinas yang ada
             */
            START_MATCHING_MASTER(formpreanalisis.jenisAlgo);

        }

        public void START_MATCHING_MASTER(string jnsALGO)
        {
            // KAMUS
            List<string> LIST_OF_SUB_keywords = new List<string>();
            List<int> LIST_OF_IDX_FOUND = new List<int>();
            
            int IDXFOUND_PDAM = -1;
            int IDXFOUND_SOSIAL = -1;
            int IDXFOUND_KEBERSIHAN = -1;
            int IDXFOUND_PERTAMANAN = -1;
            int IDXFOUND_DINKOMINFO = -1;
            int tweetCounter = -1;
            int minIDXFOUND, minIDX;

            string keywordPDAM = "null";
            string keywordSOSIAL = "null";
            string keywordKEBERSIHAN = "null";
            string keywordPERTAMANAN = "null";
            string keywordDINKOMINFO = "null";

            // ALGORITMA
            
            /**
             * method yang akan memproses pencocokan string berdasarkan
             * opsi jenis algoritma yang dipilih user
             */
            foreach (var tweet in formtarget.currentTweets)
            {
                //memproses sebuah tweet dari list currentTweets
                tweetCounter++;

                minIDXFOUND = -1;

                IDXFOUND_PDAM = -1;
                IDXFOUND_SOSIAL = -1;
                IDXFOUND_KEBERSIHAN = -1;
                IDXFOUND_PERTAMANAN = -1;
                IDXFOUND_DINKOMINFO = -1;

                /**
                 * pencarian keyword milik PDAM
                 */

                foreach (var keyword in formpreanalisis.LIST_OF_PDAM_keywords)
                {
                    //mengambil token dari sebuah keyword PDAM dengan delimiternya adalah " "
                    LIST_OF_SUB_keywords.Clear();
                    LIST_OF_SUB_keywords = ExtractSUBToken(keyword);

                    if (jnsALGO == "KMP")
                    {
                        //memanggil algoritma KMP untuk pencocokan string
                        IDXFOUND_PDAM = AlgoritmaKMP(tweet.Text, LIST_OF_SUB_keywords);
                    }
                    else
                    {
                        //memanggil algoritma BM untuk pencocokan string
                        IDXFOUND_PDAM = AlgoritmaBM(tweet.Text, LIST_OF_SUB_keywords);
                    }
                    
                    if (IDXFOUND_PDAM != -1)
                    {
                        //tampilkan sebagai hasil
                        if (minIDXFOUND == -1) {
                            minIDXFOUND = IDXFOUND_PDAM;
                        }
                        keywordPDAM = keyword;      
                        break;
                    }
                }

                /**
                 * pencarian keyword milik SOSIAL
                 */

                foreach (var keyword in formpreanalisis.LIST_OF_SOSIAL_keywords)
                {
                    //mengambil token dari sebuah keyword SOSIAL dengan delimiternya adalah " "
                    LIST_OF_SUB_keywords.Clear();
                    LIST_OF_SUB_keywords = ExtractSUBToken(keyword);
                    //memanggil algoritma KMP untuk pencocokan string
                    if (jnsALGO == "KMP")
                    {
                        IDXFOUND_SOSIAL = AlgoritmaKMP(tweet.Text, LIST_OF_SUB_keywords);
                    }
                    else
                    {
                        //memanggil algoritma BM untuk pencocokan string
                        IDXFOUND_SOSIAL = AlgoritmaBM(tweet.Text, LIST_OF_SUB_keywords);
                    }
                    
                    if (IDXFOUND_SOSIAL != -1)
                    {
                        //tampilkan sebagai hasil
                        if (minIDXFOUND == -1) {
                            minIDXFOUND = IDXFOUND_SOSIAL;
                        }
                        keywordSOSIAL = keyword;
                        break;
                    }
                }

                /**
                * pencarian keyword milik KEBERSIHAN
                */

                foreach (var keyword in formpreanalisis.LIST_OF_KEBERSIHAN_keywords)
                {
                    //mengambil token dari sebuah keyword KEBERSIHAN dengan delimiternya adalah " "
                    LIST_OF_SUB_keywords.Clear();
                    LIST_OF_SUB_keywords = ExtractSUBToken(keyword);
                    
                    if (jnsALGO == "KMP")
                    {
                        //memanggil algoritma KMP untuk pencocokan string
                        IDXFOUND_KEBERSIHAN = AlgoritmaKMP(tweet.Text, LIST_OF_SUB_keywords);
                    }
                    else
                    {
                        //memanggil algoritma BM untuk pencocokan string
                        IDXFOUND_KEBERSIHAN = AlgoritmaBM(tweet.Text, LIST_OF_SUB_keywords);
                    }
                    
                    if (IDXFOUND_KEBERSIHAN != -1)
                    {
                        //tampilkan sebagai hasil
                        if (minIDXFOUND == -1) {
                            minIDXFOUND = IDXFOUND_KEBERSIHAN;
                        }
                        keywordKEBERSIHAN = keyword;
                        break;
                    }
                }

                /**
                * pencarian keyword milik PERTAMANAN
                */

                foreach (var keyword in formpreanalisis.LIST_OF_PERTAMANAN_keywords)
                {
                    //mengambil token dari sebuah keyword PERTAMANAN dengan delimiternya adalah " "
                    LIST_OF_SUB_keywords.Clear();
                    LIST_OF_SUB_keywords = ExtractSUBToken(keyword);

                    if (jnsALGO == "KMP")
                    {
                        //memanggil algoritma KMP untuk pencocokan string
                        IDXFOUND_PERTAMANAN = AlgoritmaKMP(tweet.Text, LIST_OF_SUB_keywords);
                    }
                    else
                    {
                        //memanggil algoritma BM untuk pencocokan string
                        IDXFOUND_PERTAMANAN = AlgoritmaBM(tweet.Text, LIST_OF_SUB_keywords);
                    }
                        
                    if (IDXFOUND_PERTAMANAN != -1)
                    {
                        //tampilkan sebagai hasil
                        if (minIDXFOUND == -1) {
                            minIDXFOUND = IDXFOUND_PERTAMANAN;
                        }
                        keywordPERTAMANAN = keyword;
                        break;
                    }
                }

                /**
                * pencarian keyword milik DINKOMINFO
                */

                foreach (var keyword in formpreanalisis.LIST_OF_DINKOMINFO_keywords)
                {
                    //mengambil token dari sebuah keyword SOSIAL dengan delimiternya adalah " "
                    LIST_OF_SUB_keywords.Clear();
                    LIST_OF_SUB_keywords = ExtractSUBToken(keyword);

                    if (jnsALGO == "KMP")
                    {
                        //memanggil algoritma KMP untuk pencocokan string
                        IDXFOUND_DINKOMINFO = AlgoritmaKMP(tweet.Text, LIST_OF_SUB_keywords);
                    }
                    else
                    {
                        //memanggil algoritma BM untuk pencocokan string
                        IDXFOUND_DINKOMINFO = AlgoritmaBM(tweet.Text, LIST_OF_SUB_keywords);
                    }
                        
                    if (IDXFOUND_DINKOMINFO != -1)
                    {
                        //tampilkan sebagai hasil
                        if (minIDXFOUND == -1) {
                            minIDXFOUND = IDXFOUND_DINKOMINFO;
                        }
                        keywordDINKOMINFO = keyword;
                        break;
                    }
                }

                /**
                 * DISPOSISI ke dinas yang sesuai
                 */
                if (IDXFOUND_PDAM == -1 && IDXFOUND_SOSIAL == -1 && IDXFOUND_KEBERSIHAN == -1 && IDXFOUND_PERTAMANAN == -1 && IDXFOUND_DINKOMINFO == -1)
                {
                    // UNKNOWN
                    jumlahTWEET_UNKNOWN++;
                    IDX_DISPOStweet_UNKNOWN.Add(tweetCounter);
                    KEYWORD_DISPOStweet_UNKNOWN.Add(" ");
                }
                else 
                {
                    LIST_OF_IDX_FOUND.Clear();
                    LIST_OF_IDX_FOUND.Add(IDXFOUND_PDAM);
                    LIST_OF_IDX_FOUND.Add(IDXFOUND_SOSIAL);
                    LIST_OF_IDX_FOUND.Add(IDXFOUND_KEBERSIHAN);
                    LIST_OF_IDX_FOUND.Add(IDXFOUND_PERTAMANAN);
                    LIST_OF_IDX_FOUND.Add(IDXFOUND_DINKOMINFO);
                
                    // mencari keyword dengan indeks ditemukan paling kecil
                    minIDX = FIND_MINIMUM_IDX_FOR_DISPOSITION(minIDXFOUND, LIST_OF_IDX_FOUND);

                    // menambahkan jumlah tweet yang didisposisikan ke dinas tertentu
                    if (minIDX == IDXFOUND_PDAM)
                    {
                        //disposisi ke PDAM
                        jumlahTWEET_PDAM++;
                        IDX_DISPOStweet_PDAM.Add(tweetCounter);
                        KEYWORD_DISPOStweet_PDAM.Add(keywordPDAM);
                    }
                    else if (minIDX == IDXFOUND_SOSIAL)
                    {
                        //disposisi ke SOSIAL
                        jumlahTWEET_SOSIAL++;
                        IDX_DISPOStweet_SOSIAL.Add(tweetCounter);
                        KEYWORD_DISPOStweet_SOSIAL.Add(keywordSOSIAL);
                    }
                    else if (minIDX == IDXFOUND_KEBERSIHAN)
                    {
                        //disposisi ke KEBERSIHAN
                        jumlahTWEET_KEBERSIHAN++;
                        IDX_DISPOStweet_KEBERSIHAN.Add(tweetCounter);
                        KEYWORD_DISPOStweet_KEBERSIHAN.Add(keywordKEBERSIHAN);
                    }
                    else if (minIDX == IDXFOUND_PERTAMANAN)
                    {
                        //disposisi ke PERTAMANAN
                        jumlahTWEET_PERTAMANAN++;
                        IDX_DISPOStweet_PERTAMANAN.Add(tweetCounter);
                        KEYWORD_DISPOStweet_PERTAMANAN.Add(keywordPERTAMANAN);
                    }
                    else if (minIDX == IDXFOUND_DINKOMINFO)
                    {
                        //disposisi ke DINKOMINFO
                        jumlahTWEET_DINKOMINFO++;
                        IDX_DISPOStweet_DINKOMINFO.Add(tweetCounter);
                        KEYWORD_DISPOStweet_DINKOMINFO.Add(keywordDINKOMINFO);
                    }
                }
               
                
            }
            
            /**
             * DISPOSISI KE DINAS TERKAIT
             */
            ListOfJUMLAHTWEET_DISPOSISI.Clear();
            ListOfJUMLAHTWEET_DISPOSISI.Add(jumlahTWEET_PDAM);
            ListOfJUMLAHTWEET_DISPOSISI.Add(jumlahTWEET_SOSIAL);
            ListOfJUMLAHTWEET_DISPOSISI.Add(jumlahTWEET_KEBERSIHAN);
            ListOfJUMLAHTWEET_DISPOSISI.Add(jumlahTWEET_PERTAMANAN);
            ListOfJUMLAHTWEET_DISPOSISI.Add(jumlahTWEET_DINKOMINFO);
            ListOfJUMLAHTWEET_DISPOSISI.Add(jumlahTWEET_UNKNOWN);

            printHasil(ListOfJUMLAHTWEET_DISPOSISI);
            
        }

        /**
         * menentukan posisi indeks terkecil ditemukannya keyword tertentu.
         * Hal ini digunakan jika dalam satu tweet terdapat lebih dari 1 keyword dari dinas
         * berbeda
         */
        public int FIND_MINIMUM_IDX_FOR_DISPOSITION(int minIDXFOUND, List<int> ListOfIDX)
        {
            int minidx = minIDXFOUND;

            foreach (var idxfound in ListOfIDX)
            {
                if (idxfound != -1)
                {
                    if (minidx <= idxfound)
                    {
                        minidx = minidx;
                    }
                    else
                    {
                        minidx = idxfound;
                    }
                }
            }
            return minidx;
        }

        /**
         * DISPOSITION CONTROLLER
         */

        public void printHasil(List<int> ListOfJMLH_TWEET_DISPOSISI)
        {
            int cellCtr;
           
            foreach (var dinas in formpreanalisis.LIST_OF_DINAS)
            {
                // Create new row and add it to the table.
                TableRow tRow = new TableRow();
                DispositionTable.Rows.Add(tRow);

                for (cellCtr = 0; cellCtr < 3; cellCtr++)
                {
                    // Create a new cell and add it to the row.
                    TableCell tCell = new TableCell();
                    if (cellCtr == 0)
                    {
                        //nama dinas
                        tCell.Text = @"<b>" + dinas + @"</b>";
                    }
                    else if (cellCtr == 1)
                    {
                        //jumlah tweet terdisposisi untuk dinas tersebut
                        if (dinas == "PDAM")
                        {
                            tCell.Text = @"<b>" + ListOfJUMLAHTWEET_DISPOSISI[0].ToString() + @"</b>";
                        }
                        else if (dinas == "SOSIAL")
                        {
                            tCell.Text = @"<b>" + ListOfJUMLAHTWEET_DISPOSISI[1].ToString() + @"</b>";
                        }
                        else if (dinas == "KEBERSIHAN")
                        {
                            tCell.Text = @"<b>" + ListOfJUMLAHTWEET_DISPOSISI[2].ToString() + @"</b>";
                        }
                        else if (dinas == "PERTAMANAN")
                        {
                            tCell.Text = @"<b>" + ListOfJUMLAHTWEET_DISPOSISI[3].ToString() + @"</b>";
                        }
                        else if (dinas == "DINKOMINFO")
                        {
                            tCell.Text = @"<b>" + ListOfJUMLAHTWEET_DISPOSISI[4].ToString() + @"</b>";
                        }
                        else if (dinas == "UNKNOWN")
                        {
                            tCell.Text = @"<b>" + ListOfJUMLAHTWEET_DISPOSISI[5].ToString() + @"</b>";
                        }
                    }
                    else if (cellCtr == 2)
                    {
                        //link menuju informasi detail
                        System.Web.UI.WebControls.HyperLink h = new HyperLink();
                        h.Text = "Info Tweet " + dinas;
                        if (dinas == "PDAM")
                        {
                            h.NavigateUrl = "~/formdisposisiPDAM.aspx";
                        }
                        else if (dinas == "SOSIAL")
                        {
                            h.NavigateUrl = "~/formdisposisiSOSIAL.aspx";
                        }
                        else if (dinas == "KEBERSIHAN")
                        {
                            h.NavigateUrl = "~/formdisposisiKEBERSIHAN.aspx";
                        }
                        else if (dinas == "PERTAMANAN")
                        {
                            h.NavigateUrl = "~/formdisposisiPERTAMANAN.aspx";
                        }
                        else if (dinas == "DINKOMINFO")
                        {
                            h.NavigateUrl = "~/formdisposisiDINKOMINFO.aspx";
                        }
                        else if (dinas == "UNKNOWN")
                        {
                            h.NavigateUrl = "~/formdisposisiUNKNOWN.aspx";
                        }
                        tCell.Controls.Add(h);
                    }
                    tRow.Cells.Add(tCell);
                }
            }
        }

        /**
         * METHOD EKSTRAKSI SUBKEYWORD
         */
        public List<string> ExtractSUBToken(string keyword)
        {

            //KAMUS
            string[] arrOfToken;
            char[] delimiters = { ' ' };
            List<string> buffTemp = new List<string>();

            //ALGORITMA
            try
            {
                //mengambil token 
                arrOfToken = keyword.Split(delimiters);

                //menyimpan token ke dalam list yang sesuai
                foreach (string s in arrOfToken)
                {
                    buffTemp.Add(s);
                }
            }
            catch (NullReferenceException ex)
            {

            }

            return buffTemp;
        }

        /**
         * ---------------------------------------------------------------------------------
         * FUNGSI PENCOCOKAN STRING DENGAN ALGORITMA KMP
         * ---------------------------------------------------------------------------------
         */
        public void TABEL_PREFIX_POLA(string pola)
        {
            /* edited code
            data.Clear();
            //data.Add(0);
            for (int indeks = 0; indeks < pola.Length; ++indeks)
            {
                data.Add(0);
            }
            */

            data.Clear();
            data.Add(0);
            for (int indeks = 1; indeks < (pola.Length)+1; ++indeks)
            {
                data.Add(0);
            }
        }

        public void SET_PREFIX_POLA(string pola)
        {
            /* edited code
            //ALGORITMA MENCARI PREFIX POLA
            int acuan = 0;
            for (int indeks = 1; indeks < pola.Length; ++indeks)
            {
                if (pola[indeks].ToString().ToUpper() == pola[acuan].ToString().ToUpper())
                {
                    data[indeks] = acuan + 1;
                    acuan++;
                }
                else
                {
                    acuan = 0;
                }
            }
            */

            //ALGORITMA MENCARI PREFIX POLA
            pola = "?" + pola;
            int acuan = 1;
            for (int indeks = 2; indeks < (pola.Length)+1; ++indeks)
            {
                if (indeks < (pola.Length))
                {
                    if (pola[indeks].ToString().ToUpper() == pola[acuan].ToString().ToUpper())
                    {
                        data[indeks] = acuan + 1;
                        acuan++;
                    }
                    else
                    {
                        acuan = 1;
                    }
                }
            }
        }

        public int minIDXFOUND(List<int> LOK)
        {
            int min = LOK[0];
            foreach (var subk in LOK)
            {
                if (min <= subk)
                {
                    min = min;
                }
                else
                {
                    min = subk;
                }
            }
            return min;
        }

        /*
        public int AlgoritmaKMP(string teksSource, List<string> listOfKeywords)
        {
            //KAMUS
            string pola_fix;        //string yang mau dicari
            string teks_fix;        //teks asal
            int indeks, acuan, k, isFound = 0;
            
            List<int> listOfIDXFOUND = new List<int>();

            //ALGORITMA
          
            // inisiasi list
            listOfIDXFOUND.Clear();

            /**
             * mencocokkan subkeyword dalam teks tweet
             */
        /*
            foreach (var subkeyword in listOfKeywords) {
                pola_fix = String.Join(" ", subkeyword);    
                teks_fix = String.Join(" ", teksSource);

                //inisiasi tabel prefix pola
                TABEL_PREFIX_POLA(subkeyword);

                //mencari prefix pola
                SET_PREFIX_POLA(subkeyword);

                //algoritma utama
                indeks = 0; 
                acuan = 0; 
                k = 0;
                while ((teksSource.Length - k) >= subkeyword.Length)
                {
                    while ((acuan < subkeyword.Length) && teks_fix[indeks].ToString().ToUpper() == pola_fix[acuan].ToString().ToUpper())
                    {
                        acuan++;
                        indeks++;
                    }
                    if (acuan >= subkeyword.Length)
                    {
                        isFound++;
                        listOfIDXFOUND.Add(k);
                        break;
                    }
                    if (data[acuan] > 0)
                    {
                        k = indeks - data[acuan];
                    }
                    else
                    {
                        if (indeks == k)
                        {
                            indeks++;
                        }
                        k = indeks;
                    }
                    if (acuan > 0)
                    {
                        acuan = data[acuan] + 1;
                    }
                }
            }
        */
        
        /*
            if (isFound != listOfKeywords.Count())
            {
                return -1;
            }
            else
            {
                return minIDXFOUND(listOfIDXFOUND);
            }

        }
        */

        public int AlgoritmaKMP(string teksSource, List<string> listOfKeywords)
        {
            //KAMUS
            string pola_fix;        //string yang mau dicari
            string teks_fix;        //teks asal
            int indeks, acuan, k, isFound = 0;

            List<int> listOfIDXFOUND = new List<int>();

            //ALGORITMA

            // inisiasi list
            listOfIDXFOUND.Clear();

            teksSource = teksSource + "\0";

            /**
             * mencocokkan subkeyword dalam teks tweet
             */
            foreach (var subkeyword in listOfKeywords)
            {
                pola_fix = "?" + subkeyword;
                teks_fix = "?" + teksSource;
                //pola_fix = String.Join(" ", subkeyword);
                //teks_fix = String.Join(" ", teksSource);

                //inisiasi tabel prefix pola
                TABEL_PREFIX_POLA(subkeyword);

                //mencari prefix pola
                SET_PREFIX_POLA(subkeyword);

                //algoritma utama
                indeks = 1;
                acuan = 1;
                k = 1;
                while ((teksSource.Length - k) >= subkeyword.Length)
                {
                    while ((acuan <= subkeyword.Length) && teks_fix[indeks].ToString().ToUpper() == pola_fix[acuan].ToString().ToUpper())
                    {
                        acuan++;
                        indeks++;
                    }
                    if (acuan > subkeyword.Length)
                    {
                        isFound++;
                        listOfIDXFOUND.Add(k);
                        break;
                    }
                    if (data[acuan-1] > 0)
                    {
                        k = indeks - data[acuan-1];
                    }
                    else
                    {
                        if (indeks == k)
                        {
                            indeks++;
                        }
                        k = indeks;
                    }
                    if (acuan > 1)
                    {
                        acuan = data[acuan-1] + 1;
                    }
                }
            }

            /**
             * menghitung jumlah keyword yang berada dalam tweet
             * jika jumlahnya kurang dari jumlah subkeyword, maka
             * keyword secara keseluruhan tidak terdapat dalam tweet -> return -1
             * jika jumlahnya sama dengan jumlah subkeyword, maka
             * keyword secara keseluruhan terdapat dalam tweet -> return idxfound terkecil
             */
            if (isFound != listOfKeywords.Count())
            {
                return -1;
            }
            else
            {
                return minIDXFOUND(listOfIDXFOUND);
            }

        }


        ////////////////////////////////////////////////////////////////////////////////////


        /**
         * ---------------------------------------------------------------------------------
         * FUNGSI PENCOCOKAN STRING DENGAN ALGORITMA BOYER-MOORE
         * ---------------------------------------------------------------------------------
         */
        public void INITIATE_BM(string teksSource, string teksPola)
        {
            /**
             * prosedur yang akan menginisiasi semua atribut milik algoritma Boyer-Moore
             */

            // inisiasi atribut teks tweet
            var chars = teksSource.ToCharArray();
            teks = new char[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                teks[i] = chars[i];
            }
            panjangteks = teksSource.Length;

            // inisiasi atribut teks pola
            var chars2 = teksPola.ToCharArray();
            cek = new char[chars2.Length];
            for (int i = 0; i < chars2.Length; i++)
            {
                cek[i] = chars2[i];
            }
           
            panjangcek = teksPola.Length;

            // inisiasi array simpan
            simpan = new char[teksPola.Length];
        }

        public int SOLUTION_BM()
        {
            bool stop = false;
            bool stop2 = false;
            int pointer = panjangcek - 1;

            // menyimpan panjangcek karakter pertama dari teks ke dalam variabel simpan
            for (int i = 0; i < panjangcek; i++)
            {
                simpan[i] = teks[i];
            }
            
            while ((pointer < panjangteks - 1) && (!stop) && (!stop2))
            {
                if (hitungsamaakhir(cek, simpan) == 0)
                {
                    //Kasus sama akhir samadengan nol 
                    char cx = teks[pointer];
                    pointer += geser2(cx);
                
                    if (panjangteks > pointer)
                    {
                        int j = 0;
                        for (int i = pointer - panjangcek + 1; i <= pointer; i++)
                        {
                            simpan[j] = teks[i];
                            j++;
                        }
                    }
                    else
                    {
                        stop2 = true;
                    }

                }
                else if (hitungsamaakhir(cek, simpan) == panjangcek)
                {
                    //Kasus telah sama
                    stop = true;
                }
                else
                {
                    //Kasus sama akhir lebih besar dari nol
                    int panjangcc = hitungsamaakhir(cek, simpan);
                    pointer += geser(panjangcc);

                    if (panjangteks > pointer)
                    {
                        int j = 0;  
                        for (int i = pointer - panjangcek + 1; i <= pointer; i++)
                        {
                            simpan[j] = teks[i];
                            j++;
                        }
                    }
                    else
                    {
                        stop2 = true;
                    }
                }
                
            }

            if (stop)
            {
                return pointer - panjangcek + 1;
            }
            else
            {
                return -1;  
            }

        }

        public int hitungsamaakhir(char[] c1, char[] c2)
        {
            int banyak = 0;
            int i = panjangcek - 1;
            while ((i >= 0) && (c1[i].ToString().ToUpper() == c2[i].ToString().ToUpper()))
            {
                banyak++;
                i--;
            }
            return banyak;
        }

        public int geser(int panjang)
        {
            int i = panjangcek - panjang - 1;
            int j = panjangcek - 1;
            bool stop = false;

            while ((i >= 0) && (!stop))
            {
                if (cek[i] == cek[j])
                {
                    int ii = i - 1;
                    j--;
                    while ((j >= panjangcek - panjang) && (cek[ii] == cek[j]))
                    {
                        ii--;
                        j--;
                    }
                    if (j == panjangcek - panjang - 1)
                    {
                        stop = true;
                    }
                    else
                    {
                        j = panjangcek - 1;
                        i--;
                    }
                }
                else
                {
                    i--;
                }
            }

            if (stop)
            {
                return (panjangcek - i - 1);
            }
            else
            {
                return panjangcek;
            }
        }

        public int geser2(char c)
        {
            int i = panjangcek - 2;
            bool stop = false;

            while ((i >= 0) && (!stop))
            {
                if (cek[i].ToString().ToUpper() == c.ToString().ToUpper())
                {
                    stop = true;
                }
                else
                {
                    i--;
                }
            }

            if (stop)
            {
                return (panjangcek - i - 1);
            }
            else
            {
                return panjangcek;
            }
        }

        public int AlgoritmaBM(string teksSource, List<string> listOfKeywords)
        {
            // KAMUS
            int idx_SOLUTION_BM;
            List<int> listOfIDXFOUND = new List<int>();

            // ALGORITMA
            teksSource = teksSource + "\0";
            listOfIDXFOUND.Clear();
            foreach (var subkey in listOfKeywords)
            {
                // inisiasi atribut algoritma Boyer-Moore
                INITIATE_BM(teksSource, subkey);
                // mencari indeks ditemukannya pola
                idx_SOLUTION_BM = SOLUTION_BM();
                // menambahkan indeks tersebut ke dalam list
                listOfIDXFOUND.Add(idx_SOLUTION_BM);
            }

            /*
             * PROSES_ISI_LIST
             * keyword dinyatakan berada dalam teks tweet jika isi listOfIDXFOUND tidak ada yang
             * bernilai -1
             */
            foreach (var idxfound in listOfIDXFOUND) 
            {
                if (idxfound == -1)
                {
                    return -1;
                }
            }

            /*
             * STATE: tidak ada elemen listOfIDXFOUND yang bernilai -1
             * keyword dinyatakan berada dalam teks tweet
             * NEXT PROCESS: mencari indeks minimum dari indeks ditemukannya pola dalam teks tweet
             */
            return minIDXFOUND(listOfIDXFOUND);

        }


    }
}


