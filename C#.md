# C#

# Table of Contents
1. [Notas](#Notas)
2. [Concepts](#Concepts)
3. [Booelans](#Booleans)
4. [Strings](#Strings)
     1. [Verbatim](#Verbatim)
     2. [Métodos String](#MetodosString)
         1. [Substring](#Substring)
         2. [IndexOf](#IndexOf)
         3. [Split](#Split)
         4. [Trim](#Trim)
         5. [TrimEnd](#TrimEnd)
         6. [TrimStart](#TrimStart)
         7. [ToLower](#ToLower)
5. [Regex](#Regex)
     1. [Métodos Regex](#MetodosRegex)
         1. [Match](#RegexMatch)
6. [Numbers](#Numbers)
7. [If Statement](#If)
8. [Expression-bodies Methods](#ExpressionBodies)
9. [Extensions Methods](#ExtensionsMethods)

# Notas <a id="Notas"></a>
- Siempre terminar líneas con punto y coma ;.

# [Concepts](https://exercism.org/tracks/csharp/concepts/basics) <a id="Concepts"></a>

- C# es una lenguaje de tipado estático, lo que significa que todo tiene un tipo en tiempo de compilación.
- Una variable puede definirse ya sea explícitamente especificando su tipo, o dejando que el compilador C# infiera su tipo a partir del valor que se le asignó (**type inference**), lo cual no aplica para los parámetros de una función.

``` C#
int explicitVar = 10; // Explicitly typed
var implicitVar = 10; // Implicitly typed
```

- Se actualiza el valor de una variable por medio del operador **=**.
    - Una vez definida una variable, su tipo da dato nunca puede cambiar.

``` C#
var count = 1; // Assign initial value
count = 2;     // Update to new value

// Compiler error when assigning different type
// count = false;
```

- C# es un lenguaje orientado a objetos y requiere que todas las funciones se definan en una **class**.
    - Los objetos (o instancias) se crean usando la palabra reservada **new**.

``` C#
class Calculator
{
    // ...
}

var calculator = new Calculator();
```

- Una función dentro de una clase se le refiere como **método**.
    - Un método puede tener 0 o más parámetros, los cuales deben ser explícitamente tipados debido a que no aplica **type inference**.
    - Lo tipado de lo que retorna una función debe estar definido explícitamente.
    - Para permitir que un método sea invocado por código en otros archivos se tiene que utilizar el modificador de acceso **public**.

``` C#
class Calculator
{
    public int Add(int x, int y)
    {
        return x + y;
    }
}
```

- Una función estática (**static**), a diferencia de una función regular (instancia), no está asociada con la instancia de una clase.
    - Entonces, un método estático es un método de clase en lugar de un método de instancia.
    - Una clase **static** es una clase que solo puede contener miembros estáticos, y por lo tanto no puede ser instanciada.

``` C#
class SomeClass {
    public int InstanceMethod() { return 1; }
    public static int StaticMethod() { return 42; }
}
```

```
SomeClass instance = new SomeClass();
instance.InstanceMethod();   //Fine
instance.StaticMethod();     //Won't compile

SomeClass.InstanceMethod();  //Won't compile
SomeClass.StaticMethod();    //Fine
```

- Ejemplo de clase estática.

``` C#
static class QuestLogic
{
    public static bool CanFastAttack(bool knightIsAwake)
    {
        return !knightIsAwake;
    }

    public static bool CanSpy(bool knightIsAwake, bool archerIsAwake, bool prisonerIsAwake)
    {
        return knightIsAwake || archerIsAwake || prisonerIsAwake;
    }

    public static bool CanSignalPrisoner(bool archerIsAwake, bool prisonerIsAwake)
    {
        return !archerIsAwake && prisonerIsAwake;
    }

    public static bool CanFreePrisoner(bool knightIsAwake, bool archerIsAwake, bool prisonerIsAwake, bool petDogIsPresent)
    {
        return !archerIsAwake && petDogIsPresent || !archerIsAwake && !knightIsAwake && prisonerIsAwake;
    }
}
```

- Los métodos se invocan usando la sintaxis de punto en una instancia, especificando el nomre del método y pasando los argumentos que se requieren.
    - Se puede especificar el nombre del parámetros correspondiente, lo cual es opcional.

``` C#
var calculator = new Calculator();
var sum_v1 = calculator.Add(1, 2);
var sum_v2 = calculator.Add(x: 1, y: 2);
```

- El **Scope** en C# se define entre llaves {} y los caracteres.
- Los comentarios en C# se colocan por medio de dos opciones:
    - Una sola línea: usando //.
    - Múltiples líneas: /**/

# Booleans <a id="Booleans"></a>
- Están representados por el tipo de dato **bool**.
- C# maneja tres tipos de operadores booleanos:
    - NOT **!**.
    - AND **&&**.
    - OR **||**.

# Strings <a id="Strings"></a>
- Un String en C# es un objeto que representa un texto inmutable como una sencuancia de caracteres Unicode (letras, dígitos, puntuación, etc.).
- Se utilizan comillas dobles para definir una instancia de **string**.

``` C#
string fruit = "Apple";
```

- Los strings se manipulan por medio de métodos de string.
- Una vez que se ha construido un string, su valor nunca vuelve a cambiar.
    - Cualquier métodos que aparente modificar el string en realidad está retornando un nuevo string.
- Se pueden concatenar varios strings, siendo la forma más fácil de conseguirlo con el operador **+**.

``` C#
string name = "Jane";
"Hello " + name + "!";
// => "Hello Jane!"
```

- Se prefiere la **interpolación** en lugar de concatenación simple para el formateo de strings más complejos.
    - Se usar la interpolación en un string se debe usar el símbolo de dolar **$** y llaves para acceder a cualquier variable dentro del string.

``` C#
string name = "Jane";
$"Hello {name}!";
// => "Hello Jane!"
```

## Verbatim string <a id="Verbatim"></a>
- Algunos caracteres requieren el uso del símbolo de escape \.
- Los Strings también pueden usan el símbolo @ para ignorar cualquier símbolo de escape.

``` C#
string escaped = "c:\\test.txt";
string verbatim = @"c:\test.txt";
escaped == verbatim;
// => true
```

## Métodos <a id="MetodosString"></a>

## String.Substring() <a id="Substring"></a>

``` C#
public string Substring (int startIndex, int length);
```

- Permite obtener un substring a partir de un string.
- El primer argumento indica en donde inica el corte.
- El segundo argumento, si no se coloca, indica que se va a cortar lo sobrante.
    - Si se indica su valor entonces, entonces éste representa la longitud por cortar.

``` C#
string sentence = "Frank chases the bus.";
string name = sentence.Substring(0, 5);
// => "Frank"
```

### Ejemplo.
https://exercism.org/tracks/csharp/exercises/log-analysis
- En este ejemplo se aplica el concepto de Extensions Methods.

``` C#
using System;
using System.Text.RegularExpressions;

public static class LogAnalysis 
{
    public static string SubstringAfter(this string sentence, string delimiter) {
        int indexOfDelimiter = sentence.IndexOf(delimiter);

        // Se suma el length de delimitador ya que el delimitador puede tener más un caracter.
        // Por otro lado, se hace para excluir el delimitador inicial a la hora de imprimir el resultado.
        // IndexOf retorna el índica del primer caracter del string dado.
        if(indexOfDelimiter != -1){
            return sentence.Substring(indexOfDelimiter + delimiter.Length);
        } 
    
        return sentence;
    }

    public static string SubstringBetween(this string sentence, string startDelimiter, string endDelimiter)
    {
        int startDelimiterIndex = sentence.IndexOf(startDelimiter);
        // Se busca el índice del delimitador final a partir de donde termina el delimitador inicial.
        int endDelimiterIndex = sentence.IndexOf(endDelimiter, startDelimiterIndex + startDelimiter.Length);

        if(startDelimiterIndex != -1 && endDelimiterIndex != -1){
            return sentence.Substring(startDelimiterIndex + startDelimiter.Length, endDelimiterIndex - startDelimiterIndex - startDelimiter.Length);
        } 

        return sentence;
    }

    public static string Message(this string input) => input.SubstringAfter(": ");
    

    public static string LogLevel(this string input) => input.SubstringBetween("[", "]");

}
```

### String.IndexOf() <a id="IndexOf"></a> <a id="IndexOf"></a>
- Se usa para hallar el índice de la primera ocurrencia de una string dentro de un string. 
- Retorna -1 si no se encontró el valor especificado.

``` C#
"continuous-integration".IndexOf("integration")
// => 11

"continuous-integration".IndexOf("deployment")
// => -1
```

### String.Split() <a id="Split"></a> <a id="Split"></a>
``` C#
static class LogLine
{
    public static string Message(string logLine)
    {
        return logLine.Split(":")[1].Trim();
    }
}
```
### String.Trim() <a id="Trim"></a> <a id="Trim"></a>
### String.TrimEnd() <a id="TrimEnd"></a> <a id="TrimEnd"></a>
### String.TrimStart() <a id="TrimStart"></a> <a id="TrimStart"></a>

### String.ToLower() <a id="ToLower"></a> <a id="ToLower"></a>

# Regex <a id="Regex"></a> <a id="Regex"></a>
- C# cuenta con la clase de Regex para el uso de expresiones regulares.
- Se puede empezar a usar al importar la librería.

``` C#
using System.Text.RegularExpressions;
```

## Métodos <a id="MetodosRegex"></a> <a id="MetodosRegex"></a>

### Regex.Match <a id="RegexMatch"></a> <a id="RegexMatch"></a>
- Se utiliza para buscar la primera coincidencia de una expresión regular.

#### Ejemplo
- Obtener el texto encerrado entre []. P/E: "[ERROR]: Invalid operation"

``` C#
using System;
using System.Text.RegularExpressions;

static class LogLine
{
    public static string LogLevel(string logLine)
    {
        string patron = @"\[(.*?)\]";

        Match match = Regex.Match(logLine, patron);

        return match.Groups[1].Value.ToLower();
    }
}
```

- Al llamar a Regex.Match se obtiene un objeto Match que contiene información sobre la primera coincidencia encontrada en la cadena de texto.
- Se puede acceder a la información a través de las propiedades y métodos del objeto Match.
    - match.Success: Devuelve verdadero si se encontró una coincidencia.
    - match.Value: Devuelve la cadena que coincide con el patrón de la expresión regular.
- Si la expresión regular contiene grupos (definidos por paréntesis), se puede acceder a ellos con la propiedad **Groups** del objeto **Match**.
    - Los grupos de captura se almacenan en la propiedad de Groups como una colección, accediendo a ellos por medio de índices.
    - En el índice 0 se guarda el grupo de captura que representa la coincidencia completa, y a los grupos de captura adicionales se numean secuencialmente a partir de 1.
    - En este ejemplo se tendrían los siguientes escenarios:
        - match.Groups[0] representaría toda la coincidencia "[ERROR]".
        - match.Groups[1] representa el contenido entre corchetes, que es "ERROR" en este caso.
    - En el ejemplo se tiene la expresión regular **@"\[(.*?)\]"**.
        - Los paréntesis (.*?) se utilizan para definir un grupo de captura. Este grupo capturará el texto que se encuentra entre los corchetes. En este caso, el grupo de captura es el contenido dentro de los corchetes.
        - Esta expresión regular se usa para encontrar el texto dentro de los corchetes.
            - Ya que '[' es un carácter especial en las expresoines regulares se utiliza \ para escaparlo.
            - La parte de '(.*?)' define un grupo de captura (encerrado entre paréntesis) que captura cualquier cosa '.*?' entre los corchetes.
                - '.*?' significa 'cualquier carácter ('.')' repetido cero o más veces ('*') de manera no codiciosa ('?'), lo que significa que capturara la menor cantidad posible de caracteres entre los corchetes.

# Numbers <a id="Numbers"></a> <a id="Numbers"></a>
- Existen dos tipos de números en C#:
    - Integers.
        - Númeron sin dígitos detas del separador decimal. P/e: -1, 0, 32, 500000.
    - Floating-point numbers.
        - Número con 0 o más dígitos detrás del separador decimal. P/e: -4.2, 0.1, 16.984925, 1024.0.
- Los tipos de números más comunes en C# son:
    - int.
        - Es un entero de 32 bits.
    - double.
        - Es un double con 64 bits de número de coma flotante.
- Las operasciones aritméticas se llevan a cabo con los operadores aritméticos estandar.
- Los números se pueden comparar con los operadores de == o !=.
- C# tiene dos tipos de conversiones numéricas.
    - Conversiones implícitas.
        - No se pierde data y no se requiere sintaxis adicional.
    - Conversiones explícitas.
        - La data puede perderse y se require de sintaxis adicional en la forma de un **cast**.
            - Cuando se realiza una conversión se debe indicar la variable deseada con () para poder indicar al compilador explícitamente la conversión a realizar.
            - C# es fuertemente tipado, y requiere que las conversiones de tipo sean explícitas en ciertos casos para evitar pérdida de datos o comportamientos inesperados. Cuando conviertes un double a int, es posible que pierdas la parte fraccional del número, y C# quiere que seas consciente de esto.

            - Colocar (int) delante de la variable double le dice al compilador que estás consciente de que se puede perder la parte decimal del número y que deseas realizar la conversión de todos modos. Esto es una indicación clara de tu intención.

            - Sin la notación (int), el compilador podría generar un error o advertencia debido a la posible pérdida de datos. Por lo tanto, la notación (int) es una forma de decirle al compilador que estás tomando una decisión consciente al realizar la conversión.

``` C#
double valorDouble = 3.6;
int valorIntFloor = (int)Math.Floor(valorDouble);   // ValorIntFloor será 3
int valorIntCeiling = (int)Math.Ceiling(valorDouble); // ValorIntCeiling será 4
```

- Ya que un int tiene menos precisión que un double, convertir un int a double es seguros y por lo tanto se usa una conversión implícita.
    - Convertir de double a inbt puede llevar a perder data, por lo que se requiere de una conversión explícita.

# If Statements <a id="If"></a>
- Se definen al igual que en JS.
- C# no maneja el concepto de **truthy values**.
- Al igual que en JS, se puede omitir {} si el if retorna inmediatamente.

``` C#
int x = 6;

if (x == 5)
{
    // Execute logic if x equals 5
}
else if (x > 7)
{
    // Execute logic if x greater than 7
}
else
{
    // Execute logic in all other cases
}
```

- Estructura si If retorna inmediatamente.

``` C#
int x = 6;

if (x == 5) return;
else if (x > 7) return 'd'
```

# Expression-bodies Method <a id="ExpressionBodies"></a>
- Consiste en una única expresión que devuelve un valor cuyo tipo coincide con el tipo de retorno del método o, en el caso de los métodos que devuelven void, que realiza alguna operación.
- Es el equivalente a Arrow Functions con una sola línea de código en JS.

``` C#
public override string ToString() => $"{fname} {lname}".Trim();
```

- Se tiene el ejemplo en Extensions Methods para el método Message().

# Extensions Methods <a id="ExtensionsMethods"></a>

- Permiten agregar método a tipos existentes sin tener que crear un nuevo tipo derivado, recompilar o modificar el tipo original.
- Se definen como métodos estáticos, pero se invocan usando la sintaxis de instancia de métodos.
    - En otras palabras, se invocan como si fueran insntancias de métodos en los tipos extendidos.
    - Consigue esto utilizando **this** antes del tipo, indicando que la instancia en la que ponemos el . se pasa como primer parámetro. 
- Su primer parámetro es precedido por el modificador **this**, y especifica el tipo sobre el cual el método opera, y se introducen en el scope al nivel del namespace.
    - Esto significa que si estás en un espacio de nombres diferente al que está definido el método de extensión, su espacio de nombres debe estar primero en una directiva **using**. 
    - Por ejemplo, en el siguiente ejemplo se necesitaría primero una directiva **using MyExtensions**. Si se está en el mismo espacio de nombres en el que está definido el método de extensión, se pueden usar los métodos de extensión sin una directiva **using**.
    - Un ejemplo bien conocido de métodos de extensión son los operadores de consulta estándar [LINQ](https://learn.microsoft.com/en-us/dotnet/csharp/linq/) que añaden funcionalidad de consulta a los tipos IEnumerable existentes. Para incluirlos en el ámbito de aplicación necesitamos una directiva **using System.Linq;**.
- En código de cliente no hay diferencia aparente entre invocar una extensión de metodo y los métodos definidos en el tipo.

``` C#
public static int WordCount(this string str)
{
    return str.Split().Length;
}

"Hello World".WordCount();
// => 2
```