# Indonesian Affixes - Word Utilities

This utility is a collection of methods for transforming a root word into an affix-word in Bahasa Indonesia. I created this utility to help me in the process of building SIBI (Sign System for Bahasa Indonesia) Translation Apps. The main goal of the apps is to **translate a sentence from Text Input to SIBI Gestures Animation**, which currently in the works at the Faculty of Computer Science Universitas Indonesia.


The initial purpose of this utility is for data-collection automation only. Hopefully, by open-sourcing this utility, it will grow into something more with help from other researchers related to Indonesian Language and Text Processing who wants to contribute. Currently, this utility is made in C# language because the SIBI Gestures Generation Apps built using Unity. In the future, I have a plan to migrate this utility to becoming an API.

## Status
**Incomplete [ON-GOING]**

Currently, this utility only capable of adding one prefix and one suffix to a root-word.
The final goal of this utility is to add multiple prefixes (max. 3) and multiple suffixes (max. 3)
as cited from [(Adriani et.al., 2007)](https://www.researchgate.net/profile/Jelita_Asian/publication/220316701_Stemming_Indonesian_A_confix-stripping_approach/links/5badcaff299bf13e6051ef4b/Stemming-Indonesian-A-confix-stripping-approach.pdf) below:
```
[[[DP+]DP+]DP+] Root-Word [[+DS][+PP][+P]]
```

Contributors are open :)

## Minimum Prequisites
```
.NET Core SDK v2.2.301
```

## List of Methods
```csharp
TransformRoot(string root) 
//return List<string>

CombineAwalan(string prefix, string root) 
//return string

CombineAkhiran(string sufix, string root) 
//return string

CombineAwalanAkhiran(string prefix, string root, string sufix) 
//return string
```
## Examples
Retrieve prefix + root-word
```csharp
using AffixWordUtilities;

namespace Example {
    class Program {
        static void Main(string[] args) {
            //Prefix Only
            Console.WriteLine(Word.CombineAwalan("me", "baca"));
            
            //Suffix Only
            Console.WriteLine(Word.CombineAwalan("makan", "an"));

            // Prefix + Suffix
            Console.WriteLine(Word.CombineAwalanAkhiran("me", "nama", "i"));
        }
    }
}
```
**Output**
```
membaca
makanan
menamai
```

## Built With

* **Language:** *C#*
* **IDE:** *Visual Studio Code*

## Author

* **Surya Darmana** - *Initial Work* - igm.surya@ui.ac.id


