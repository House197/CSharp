# Concepts

## Numbers
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

## If Statements
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

## Expression-bodies Method
- Consiste en una única expresión que devuelve un valor cuyo tipo coincide con el tipo de retorno del método o, en el caso de los métodos que devuelven void, que realiza alguna operación.
- Es el equivalente a Arrow Functions con una sola línea de código en JS.

``` C#
public override string ToString() => $"{fname} {lname}".Trim();
```

- Se tiene el ejemplo en Extensions Methods para el método Message().

## Extensions Methods

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

# Método de String
## String.Substring()

``` C#
public string Substring (int startIndex, int length);
```

- Permite obtener un substring a partir de un string.
- El primer argumento indica en donde inica el corte.
- El segundo argumento, si no se coloca, indica que se va a cortar lo sobrante.
    - Si se indica su valor entonces, entonces éste representa la longitud por cortar.

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