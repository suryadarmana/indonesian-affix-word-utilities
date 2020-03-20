using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AffixWordUtilities {
    static class Word {

        private static string[] prefix      = {"ber", "di", "ke", "me", "pe", "se", "ter"};
        private static string[] sufix       = {"an", "i", "kan", "lah", "wan", "wati"};
        private static string[] partikel    = {"kah", "lah", "pun"};
        private static string[] kepunyaan   = {"ku", "mu", "nya"};

        private const string DISALLOWED_COMBINATION = "disallowed combination!";
        
        /// <summary>
        ///     Menghasilkan daftar seluruh kombinasi imbuhan dari sebuah kata dasar
        ///     <param name="root"> kata dasar </param>
        /// </summary>
        /// <returns>
        ///     daftar seluruh kombinasi imbuhan dari sebuah kata dasar dalam bentuk List of String
        /// </returns>
        public static List<string> TransformRoot(string root) {
            List<string> combined = new List<string>();
            
            for (int i = 0; i < prefix.Length; i++) {
                combined.Add( CombineAwalan(prefix[i], root) );
            }

            for (int i = 0; i < sufix.Length; i++) {
                combined.Add( CombineAkhiran(sufix[i], root) );
            }

            for (int i = 0; i < prefix.Length; i++) {
                for (int j = 0; j < sufix.Length; j++) {
                    string tempComb = CombineAwalanAkhiran(prefix[i], root, sufix[j]);
                    if (tempComb != DISALLOWED_COMBINATION) {
                        combined.Add( tempComb );
                        /*
                        for (int k = 0; k < partikel.Length; k++) {
                            combined.Add( CombineAkhiran(partikel[k], tempComb) );
                        }
                        */
                    }
                }
            }
            
            return combined;
        }

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

                if (root[0] == 'p' && root != "punya" && ((
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
                root[0] == 'e' || 
                root[0] == 'l' || 
                root[0] == 'o' || 
                root[0] == 'u' || 
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

            #region Rules 3 = ber
            else if (prefix == "ber" && root[0] == 'r') {
                prefix = "be";
            } else if (prefix == "ber" && root == "ajar") {
                prefix = "bel";
            }
            #endregion

            #region Rules 4 = ter
            else if (prefix == "ter" && root[0] == 'r') {
                prefix = "te";
            }
            #endregion
            
            //TODO: ADD THE OTHER RULES FOR PREFIX

            string result = prefix + root;
            return result;
        }

        //TODO: Combine Multi Awalan

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

        // TODO: Combine Multi Akhiran

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
    }
}