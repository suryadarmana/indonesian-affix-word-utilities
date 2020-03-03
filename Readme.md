# Indonesian Affixes - Word Utilities

English:
Collections of method for transforming a root word into an affix-word in Bahasa Indonesia.

Indonesia:
Kumpulan metode untuk transformasi kata dasar menjadi kata berimbuhan dalam Bahasa Indonesia

## Status
Incomplete [ON-GOING]
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


