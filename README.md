# XLocalizationService
SimplePCL for localization, can be used in any Xamarin app as well on WPF

## Installation ##

Use nuget to import the reference in your project

       PM> Install-Package XlocalizationService 

After installation create a `myVocabulary.csv` file inside the project. You can choose if save as content or resource.

The file have to follow these rules:

 1. Use standard csv formart: comma to separate columns, "\n" to separate rows 
 2. First row start with "Key, " and then contain the Language Culture Names ("it-IT", "en-US")
 2. All the keys start with "\#" and are lowercase
 3. After the Key, follow the list of localizations in the order defined by first row
 4. File contains also Language Culture Names localization
 5. If the localization contain a comma or special char you have to use the quotation mark
 6. Emplty row will be ignored

**Example**

﻿﻿﻿Key,it-IT,en-US
﻿﻿﻿\#it-it,Italiano,Italian
\#en-en,Inglese,English
\#,,
\#set,Imposta,Set
\#error,Errore,Error
\#example, Questo e' un esempio di stringa, This is a sample
\#example2, "Questo e' un esempio, con anche una virgola2", "This is a sample, with comma"
﻿



## How to use ##
Load vocabulary

```csharp
StreamReader sr = new StreamReader("Vocabulary.csv"); 
LocalizationService.LS.ChangeVocabulary(sr.BaseStream); 

```
  

Example of use in code behind:
```csharp
 String localizedString = LocalizationService.LS["#example"];
```
or
```csharp
 String localizedString = LocalizationService.LS.GetValue("#example", "fallBackValue)";
```

Example of use in XAML (at the moment supported only on WPF):
```XML
 xmlns:ls="clr-namespace:XLocalizationService;assembly=XLocalizationService.Win"
 
<Window.Resources> 
         <ls:LanguageConverter x:Key="LangConverter" /> 
</Window.Resources> 

 <TextBlock Text="{Binding Converter={StaticResource LangConverter}, ConverterParameter=#key}"/>
 
 <TextBlock Text="{Binding Converter={StaticResource LangConverter}, ConverterParameter=#key|fallbackValue}"/>
```
If the fallBack value is not set and key is not found, the return value is  `String.Empty`

### Language Management ###

To obtain the list of available languages
```csharp
LocalizationService.LS.SupportedLanguages
```

To set a new language us the standard language convection
```csharp
 LocalizationService.LS.SetLanguage("en-US")
```


> Written with [StackEdit](https://stackedit.io/).

