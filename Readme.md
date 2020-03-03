# Indonesian Affixes - Word Utilities

Collections of method for transforming a root word into an affix-word in Bahasa Indonesia.
I created this utilities to help me in a process of building SIBI (Sign System for Bahasa Indonesia) Translation Apps to Translate from Text to SIBI Gestures Animation, which currently in the works at Faculty of Computer Science Universitas Indonesia.


This utilities are part of the text-parser module for SIBI Gestures Generation

## Status
**Incomplete [ON-GOING]**

Currently this utilities only capable of adding one prefix and one suffix to a root-word.
The final goal of this utilities is to add multiple prefixes (max. 3) and multiple suffixes (max. 3)
as cited from [(Adriani et.al., 2007)](https://www.researchgate.net/profile/Jelita_Asian/publication/220316701_Stemming_Indonesian_A_confix-stripping_approach/links/5badcaff299bf13e6051ef4b/Stemming-Indonesian-A-confix-stripping-approach.pdf) below:
```
[[[DP+]DP+]DP+] Root-Word [[+DS][+PP][+P]]
```

Contributors are open :)

## Prequisites
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
            Console.WriteLine(Word.CombineAwalan("me", "baca"));
        }
    }
}
```
**Output**
```
membaca
```

## Built With

* **Language:** *C#*
* **IDE:** *Visual Studio Code*

## Author

* **Surya Darmana** - *Initial Work* - igm.surya@ui.ac.id


