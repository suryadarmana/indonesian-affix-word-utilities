using System.Collections.Generic;
using System.Linq;

namespace AffixWordUtilities {
    /*
    
    Basic Rules from [Sobariah, 2016] : 
    https://play.google.com/store/books/details?id=fVRxCwAAQBAJ&pcampaignid=books_web_aboutlink

    --------------------------------------------------------
    awalan:     vokal/konsonan:    bentuk:        peluruhan:
    --------------------------------------------------------
    se          [*]                se             false
    me          [a,i,u,e,o]        meng           false
    me          [b]                mem            false
    me          [c,d,j]            men            false
    me          [g,h]              meng           false
    me          [k]                meng           true
    me          [p]                mem            true
    me          [s]                meny           true
    me          [t]                men            true
    me          [l,m,n,r,w]        me             false
    ke          [*]                ke             false
    di          [*]                di             false
    ber         [~r]               ber            false
    ber         [r]                ber            true
    ber         [*|er|*]           be             false
    ber         [ajar]             bel            false
    ter         [~r]               ter            false
    ter         [r]                ter            true
    pe          [a,i,u,e,o]        peng           false
    pe          [g,h]              peng           false
    pe          [k,h,~kh]          peng           true
    pe          [kh]               peng           false
    pe          [j,d,c]            pen            false
    pe          [t]                pen            true
    pe          [b,f]              pem            false
    pe          [p]                pem            true
    pe          [s]                peny           true
    pe          [l,m,n,r,y,z]      pe             false
    per         [*]                per            false

    ---------------------------------------------------------
    akhiran:     vokal/konsonan:    bentuk:        peluruhan:
    ---------------------------------------------------------
    -kan         [*]                -kan           false
    -an          [*]                -an            false
    -i           [*]                -i             false
    -wan         [a]                -wan           false
    -wan         [ah]               -wan           true
    -wati        [a]                -wati          false
    -wati        [ah]               -wati          true
    -man         [i]                -man           false
    -is          [*]                -is            false
    -isme        [*]                -isme          false

    */
    
    static class Word {

        public static string[] prefix      = { "se", "me", "ke", "di", "ber", "ter", "pe", "per" };
        public static string[] sufix       = { "kan","an", "i", "wan", "wati", "man", "is", "isme" };
        public static string[] partikel    = { "kah", "lah", "pun" };
        public static string[] kepunyaan   = { "ku", "mu", "nya" };
        private const string DISALLOWED_COMBINATION = "disallowed combination!";

        /// <summary>
        ///     Menambahkan imbuhan awalan pada sebuah kata dasar
        ///     <param name="prefix">awalan yang ingin ditambahkan</param>
        ///     <param name="root">kata dasar</param>
        /// </summary>
        /// <returns>
        ///     kata yang telah ditambahkan awalan
        /// </returns>
        /// <example>
        ///     "me" + "sapu" = "menyapu"
        /// </example>      
        public static string CombineAwalan(string prefix, string root) {
            #region Rules 1 = me(N)-
            if (prefix == "me" && root.Length <= 3) {
                prefix = prefix + "nge";
            } else if (prefix == "me" && root[0] == 's') {
                if (root[1] == 'a' || 
                    root[1] == 'i' || 
                    root[1] == 'u' || 
                    root[1] == 'e' || 
                    root[1] == 'o') {
                    
                    prefix = prefix + "ny";
                    root = root.Remove(0, 1);
                } else {
                    prefix = prefix + "n";
                }
            } else if (prefix == "me" && (
                root[0] == 'b' || 
                root[0] == 'f' || 
                root[0] == 'p' || 
                root[0] == 'v') ) {
                
                prefix = prefix + "m";

                if (root[0] == 'p' && root != "punya" && (root[0] != 'p' && root[1] != 'e' && root[2] != 'r') && ((
                    root[1] == 'a' || 
                    root[1] == 'i' || 
                    root[1] == 'u' || 
                    root[1] == 'e' || 
                    root[1] == 'o'))) {

                    root = root.Remove(0, 1);
                }
            } else if (prefix == "me" && (
                root[0] == 'c' || 
                root[0] == 'd' || 
                root[0] == 'j' || 
                root[0] == 't' || 
                root[0] == 'z') ) {
                
                prefix = prefix + "n";
                
                if (root[0] == 't' && (
                    root[1] == 'a' || 
                    root[1] == 'i' || 
                    root[1] == 'u' || 
                    root[1] == 'e' || 
                    root[1] == 'o')) {

                    root = root.Remove(0, 1);
                }
            } else if (prefix == "me" && (
                root[0] == 'a' || 
                root[0] == 'e' || 
                root[0] == 'g' || 
                root[0] == 'h' || 
                root[0] == 'k' || 
                (root[0] == 'k' && root[1] == 'h') || 
                root[0] == 'o' || 
                root[0] == 'q' || 
                root[0] == 'u' || 
                root[0] == 'x') ) {
                
                prefix = prefix + "ng";
                
                if (root[0] == 'k' && root[1] != 'h' && root != "kaji" && (
                    root[1] == 'a' || 
                    root[1] == 'i' || 
                    root[1] == 'u' || 
                    root[1] == 'e' || 
                    root[1] == 'o')) {
                    
                    root = root.Remove(0, 1);
                }
            }
            #endregion

            #region Rules 2 = pe(N)-
            else if (prefix == "pe" && root[0] == 's') { //peny-
                if (root[1] == 'a' || 
                    root[1] == 'i' || 
                    root[1] == 'u' || 
                    root[1] == 'e' || 
                    root[1] == 'o') {
                    
                    prefix = prefix + "ny";
                    root = root.Remove(0, 1);
                } else {
                    prefix = prefix + "n";
                }
            } else if (prefix == "pe" && (
                root[0] == 'a' || 
                root[0] == 'i' || 
                root[0] == 'u' || 
                root[0] == 'e' || 
                root[0] == 'o' || 
                root[0] == 'g' || 
                root[0] == 'h' || 
                root[0] == 'k') ) { //peng-
                
                prefix = prefix + "ng";
                
                if (root[0] == 'k' && root[1] != 'h' && root != "kaji" && (
                    root[1] == 'a' || 
                    root[1] == 'i' || 
                    root[1] == 'u' || 
                    root[1] == 'e' || 
                    root[1] == 'o')) {

                    root = root.Remove(0, 1);
                }
            } else if (prefix == "pe" && (
                root[0] == 'c' || 
                root[0] == 'd' || 
                root[0] == 'j' || 
                root[0] == 't') ) { //pen-

                prefix = prefix + "n";
                
                if (root[0] == 't' && (
                    root[1] == 'a' || 
                    root[1] == 'i' || 
                    root[1] == 'u' || 
                    root[1] == 'e' || 
                    root[1] == 'o')) {
                    
                    root = root.Remove(0, 1);
                }
            } else if (prefix == "pe" && (root[0] == 'b' || root[0] == 'f' || root[0] == 'p') ) { //pem-
                prefix = prefix + "m";
                if (root[0] == 'p')
                    root = root.Remove(0, 1);
            }
            #endregion

            #region Rules 3 = per
            else if (prefix == "per") {
                prefix = "per";
                if (root[0] == 'r') {
                    root = root.Remove(0, 1);
                }
            }
            #endregion

            #region Rules 4 = ber
            else if ((prefix == "ber" && root[0] == 'r') ||
                     (prefix == "ber" && root[1] == 'e' && root[2] == 'r')) {
                prefix = "be";
            } else if (prefix == "ber" && root == "ajar") {
                prefix = "bel";
            }
            #endregion

            #region Rules 5 = ter
            else if (prefix == "ter" && root[0] == 'r') {
                prefix = "te";
            }
            #endregion
            
            // the remaining prefix not included in rules because
            // it doesn't have any special case 

            string result = prefix + root;
            return result;
        }

        //TODO: Combine Multi Awalan
        public static string CombineAwalan(string[] prefix, string root) {
            string result = "";
            for (int i = prefix.Length-1; i >= 0; i--) {
                if (i != prefix.Length-1) {
                    result = CombineAwalan(prefix[i], result);
                } else {
                    result = CombineAwalan(prefix[i], root);
                }
            }
            
            return result;
        }

        /// <summary>
        ///     Menambahkan imbuhan akhiran pada sebuah kata dasar
        ///     <param name="sufix">akhiran yang ingin ditambahkan</param>
        ///     <param name="root">kata dasar</param>
        /// </summary>
        /// <returns>
        ///     kata yang telah ditambahkan akhiran
        /// </returns>
        /// <example>
        ///     "nama" + "kan" = "namakan"
        /// </example>
        public static string CombineAkhiran(string sufix, string root) {
            string result = root + sufix;
            return result;
        }

        /// <summary>
        ///     Menambahkan beberapa imbuhan akhiran pada sebuah kata dasar
        ///     <param name="sufix">kumpulan akhiran yang ingin ditambahkan</param>
        ///     <param name="root">kata dasar</param>
        /// </summary>
        /// <returns>
        ///     kata yang telah ditambahkan beberapa akhiran
        /// </returns>
        /// <example>
        ///     "nama" + ["kan", "lah"]  = "namakanlah"
        /// </example>
        public static string CombineAkhiran(string[] sufix, string root) {
            string sufixes = "";
            foreach (var akhiran in sufix) {
                sufixes += akhiran;
            }
            string result = root + sufixes;
            return result;
        }

        /// <summary>
        ///     Menambahkan imbuhan awalan dan akhiran pada sebuah kata dasar
        ///     <param name="prefix">awalan yang ingin ditambahkan</param>
        ///     <param name="root">kata dasar</param>
        ///     <param name="sufix">akhiran yang ingin ditambahkan</param>
        ///     
        ///     Disallowed Prefix - Suffix Combination
        ///     be-i, di-an, ke-i, ke-kan, me-an, se-i, se-kan, te-an
        ///
        /// </summary>
        /// <returns>
        ///     kata yang telah ditambahkan awalan dan akhiran
        /// </returns>
        /// <example>
        ///     "me" + "nama" + "kan" = "menamakan"
        /// </example>
        public static string CombineAwalanAkhiran(string prefix, string root, string sufix) {
            string result = "";
            if ((prefix == "ber" && sufix == "i") ||
                (prefix == "di" && sufix == "an") ||
                (prefix == "ke" && sufix == "i") ||
                (prefix == "ke" && sufix == "kan") ||
                (prefix == "me" && sufix == "an") ||
                (prefix == "se" && sufix == "i") || 
                (prefix == "se" && sufix == "kan") ||
                (prefix == "ter" && sufix == "an")) {
                result = DISALLOWED_COMBINATION;
            } else {
                result = CombineAwalan(prefix, root) + sufix;
            }
            return result;
        }

        /// <summary>
        ///     Menambahkan beberapa imbuhan awalan dan akhiran pada sebuah kata dasar
        ///     <param name="prefix">beberapa awalan yang ingin ditambahkan</param>
        ///     <param name="root">kata dasar</param>
        ///     <param name="sufix">beberapa akhiran yang ingin ditambahkan</param>
        ///     
        ///     Disallowed Prefix - Suffix Combination
        ///     be-i, di-an, ke-i, ke-kan, me-an, se-i, se-kan, te-an
        ///
        /// </summary>
        /// <returns>
        ///     kata yang telah ditambahkan awalan dan akhiran
        /// </returns>
        /// <example>
        ///     ["me", "per"] + "masalah" + ["kan"] = "mempermasalahkan"
        /// </example>
        public static string CombineAwalanAkhiran(string[] prefix, string root, string[] suffix) {
            string result = "";
            if ((prefix.First() == "ber" && sufix.Last() == "i") ||
                (prefix.First() == "di" && sufix.Last() == "an") ||
                (prefix.First() == "ke" && sufix.Last() == "i") ||
                (prefix.First() == "ke" && sufix.Last() == "kan") ||
                (prefix.First() == "me" && sufix.Last() == "an") ||
                (prefix.First() == "se" && sufix.Last() == "i") || 
                (prefix.First() == "se" && sufix.Last() == "kan") ||
                (prefix.First() == "ter" && sufix.Last() == "an")) {
                result = DISALLOWED_COMBINATION;
            } else {
                string suffixes = "";
                foreach(var akhiran in suffix) {
                    suffixes += akhiran;
                }
                result = CombineAwalan(prefix, root) + suffixes;
            }     
            return result;
        }
    }
}