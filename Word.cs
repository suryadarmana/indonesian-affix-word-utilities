using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AffixWordUtilities {
    static class Word {

        private static string rootword;
        private static string[] prefix = {"ber", "di", "ke", "me", "pe", "se", "ter"};
        private static string[] sufix = {"an", "i", "kan", "lah", "wan", "wati"};
        private static string[] partikel = {"kah", "lah", "pun"};

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
                    combined.Add( tempComb );
                    for (int k = 0; k < partikel.Length; k++) {
                        combined.Add( CombineAkhiran(partikel[k], tempComb) );
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
            // Rules 1 = me(N)-
            if (prefix == "me" && root.Length <= 3) {
                prefix = prefix + "nge";
            } else if (prefix == "me" && root[0] == 's') {
                root = root.Remove(0, 1);
                root = "ny" + root;
            } else if (prefix == "me" && (root[0] == 'b' || root[0] == 'p') ) {
                prefix = prefix + "m";
                if (root[0] == 'p')
                    root = root.Remove(0, 1);
            } else if (prefix == "me" && (root[0] == 'c' || root[0] == 'd' || root[0] == 'j' || root[0] == 't') ) {
                prefix = prefix + "n";
                if (root[0] == 't')
                    root = root.Remove(0, 1);
            } else if (prefix == "me" && (root[0] == 'g' || root[0] == 'k' || (root[0] == 'k' && root[1] == 'h')) ) {
                prefix = prefix + "ng";
                if (root[0] == 'k' && root[1] != 'h')
                    root = root.Remove(0, 1);
            }

            //TODO: ADD THE OTHER RULES FOR PREFIX

            string result = prefix + root;
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
        ///     Menambahkan imbuhan awalan dan akhiran pada sebuah kata dasar
        ///     <param name="prefix">awalan yang ingin ditambahkan</param>
        ///     <param name="root">kata dasar</param>
        ///     <param name="sufix">akhiran yang ingin ditambahkan</param>
        /// </summary>
        /// <returns>
        ///     kata yang telah ditambahkan akhiran
        /// </returns>
        /// <example>
        ///     "nama" + "kan" = "namakan"
        /// </example>
        public static string CombineAwalanAkhiran(string prefix, string root, string sufix) {
            string result = CombineAwalan(prefix, root) + sufix;
            return result;
        }
    }
}