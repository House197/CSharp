# C#

# Foundations
## Lenguaje de programación
- Permiten escribir instrucciones que se desea que la computadora ejecute.
- Su función es permitir a los usuarios expresar la intención de una forma entendible humana hacia una computadora.
- Las instrucciones que se escriben en un lenguaje de programación se les denomina **source code** o **code**.
- El código debe ser compilado en un formato que la computadora pueda entender.

## Compilación
- Un compilador convierte el código fuente en un formato diferente entendible para la inidad de procesamiento central de una computadora (computer's central processing unit CPU).

## Syntax
- Son las reglas para la escritura de código en un lenguaje de programación.
- La sintaxos (reglas) definen las palabras clave y operadores, así como cómo se usan en conjunto para crear el programa.

## Literals

### Literal Values
- Es un valor constante.
- El tipo de dato **string** cuando se tienen palabras alfanuméricas, frases o datos para presentación, no para cálculos.

### Character Literals
- Si se desea imprimir un solo caracter alfanumerico se puede crear una **char lateral** al encerrar un caracter alfanumérico en comillas simples.
- **char** es una abreviación para character.
- Las comillas simples se usan para crear character literal.
    - En otras palabras, al usar comillas simples el compilador de C# espera un solo caracter.
- El uso de comillas dobles es para la creación de tipo de dato **string**.

``` C#
Console.WriteLine('b');
```

- Si se escribiera una frase entre comillas simples se obtiene el error 'Too many characters in character literal'.

``` C#
Console.WriteLine('Hello World!');

Output: (1,19): error CS1012: Too many characters in character literal
```

### Integer Literals
- Se puede usar para imprimir numeros enteros en la consola.
- Se definen con **int**.
- No requiere de otro operador como **string** o **char**.

``` C#
Console.WriteLine(123);
```

### Floating-point literals
- Números de punto flotante son aquellos que contienen un decimal.
    - Por ejemplo: 3.14159.
- C# cuenta con tres tipos de daos para representar números decimales:
    - float
    - double
    - decimal.
- Los anteriores tipos de dato se diferencian por su grado de precisión.

```
Float Type    Precision
----------------------------
float         ~6-9 digits
double        ~15-17 digits
decimal        28-29 digits
```

- La precisión representa el número de dígitos después del decimal que son exactos.
- Se crea un **float** literal al agregar la letra **F** después del número.
    - En este contexto, F se llama **literal suffix**.
    - Los literal suffix le indican al compilador que se desea trabajar con un valor de tipo float.
    - Se puede usar tanto **f** o **F** como el sufijo para un float.

``` C#
Console.WriteLine(0.25F);

Output: 0.25
```

- Se crea un **double** literal para simplemente ingresar un número decimal, ya que el compilador por defecto usa **double literal** cuando un número decimal se ingresa sin un literal suffix.

``` C#
Console.WriteLine(2.625);

Output: 2.625
```

- Se crea un **decimal literal** al agregar la letra **m** después del número.
- Se puede usar tanto **m** o **M**.

``` C#
Console.WriteLine(12.39816m);

Output: 12.39816
```

### Boolean literals
- Se usan las palabras reservadas: **true** y **false**.

``` C#
Console.WriteLine(true);
Console.WriteLine(false);

Output:
True
False
```

### Recap
The main takeaway is that there are many data types, but you'll focus on just a few for now:

- string for words, phrases, or any alphanumeric data for presentation, not calculation
- char for a single alphanumeric character
- int for a whole number
- decimal for a number with a fractional component
- bool for a true/false value

## Variables
- Es un contenedor para el almacenamiento de un tipo de valor.
- El nombre de una variable es una etiqueta que el compilador asigna a una dirección de memoria.
- Se declara al primero especificar su tipo de dato.
- Se puede cambiar el valor de una variable las veces que se deseen.

``` C#
string firstName;
firstName = "Bob";
Console.WriteLine(firstName);

char userOption;

int gameScore;

decimal particlesPerMillion;

bool processedCustomer;
```

- La técnica de initializing una variable permite inicializar una variable apenas de declara.

``` C#
string firstName = "Bob";
Console.WriteLine(firstName);
```

### Convenciones para el nombrado de variables

Here's a few important considerations about variable names:

- Variable names can contain alphanumeric characters and the underscore character. Special characters like the hash symbol # (also known as the number symbol or pound symbol) or dollar symbol $ are not allowed.
- Variable names must begin with an alphabetical letter or an underscore, not a number.
- Variable names are case-sensitive, meaning that string Value; and string value; are two different variables.
- Variable names must not be a C# keyword. For example, you cannot use the following variable declarations: decimal decimal; or string string;.
- There are coding conventions that help keep variables readable and easy to identify. As you develop larger applications, these coding conventions can help you keep track of variables among other text.

Here are some coding conventions for variables:

- Variable names should use camel case, which is a style of writing that uses a lower-case letter at the beginning of the first word and an upper-case letter at the beginning of each subsequent word. For example, string thisIsCamelCase;.
- Variable names should begin with an alphabetical letter. Developers use the underscore for a special purpose, so try to not use that for now.
- Variable names should be descriptive and meaningful in your app. Choose a name for your variable that represents the kind of data it will hold.
- Variable names should be one or more entire words appended together. Don't use contractions or abbreviations because the name of the variable (and therefore, its purpose) may be unclear to others who are reading your code.
- Variable names shouldn't include the data type of the variable. You might see some advice to use a style like string strValue;. That advice is no longer current.

### Declaración de implicitly typed local variables
- Un variable local de tipado implícitp se crea usando la palabra reservada **var** seguida por una inicialización de variable.

``` C#
var message = "Hello world!";
```

- **var** le indica al compilador de C# que el tipo de dato está implícito por el valor asignado.
- **var** se usa para ahorrar keystrokes cuando los typos son extensos o cuando el tipo es obvio por el contexto.
- En el ejemplo anterior, el compilador de C# va a tratar cada instancia de message como una instancia de tipo **string**.
    - El tipado implícito nunca va a cambiar, por lo que message siempre va a ser string.
- Las variables que usan **var** siempre deben ser inicializadas.
-  When you begin developing code for a task, you may not immediately know what data type to use. Using var can help you develop your solution more dynamically.

### Recap
- Variables are temporary values you store in the computer's memory.
- Before you can use a variable, you have to declare it.
- To declare a variable, you first select a data type for the kind of data you want to store, and then give the variable a name that follows the rules.

## String Formatting
### Character escape sequences
- Un Escape Character Sequence es una instrucción al runtime de insertar un caracter especial que afectará a la salida del string.
- El escale character sequence empieza con un backlash \ seguido del caracter al que se le desea hace escape.
    - Por ejemplo:
        - La \n secuencia agrega una nueva línea.
        - La \t secuencia agrega un tab.
``` C#
Console.WriteLine("Hello\nWorld!");
Console.WriteLine("Hello\tWorld!");

Output:
Hello
World!
Hello   World!
```

### Verbatim string literal
- Un verbatim string literal mantiene todos los espacios en blanco y los caracteres sin la necesidad de escapar el backlash.
- Se crea un verbatim string con la directiva **@** antes del literal string.
- Permite usar el unescaped \ character.

``` C#
Console.WriteLine(@"    c:\source\repos    
        (this is where your code goes)");

Output:
    c:\source\repos    
        (this is where your code goes)
```

### Unicode Escape Characters
- Se pueden agregar encoded characters in literal string usando la secuancia de escape **\u**, luego un código de 4 caracteres representando algun caracter en Unicode (UTF-16).

``` C#
// Kon'nichiwa World
Console.WriteLine("\u3053\u3093\u306B\u3061\u306F World!");

```
- Caracteres Unicode pueden no imprimit correctamente dependiendo de la aplicación.

### String concatenation para combinar strings
- Permite combinar dos o más valores **string** en un solo valor **string**.
- A diferencia de la adición, el segundo valor es appended al final del primer valor.
- Se utiliza el operador de concatenación de string **+**.

``` C#
string firstName = "Bob";
string greeting = "Hello";
// string message = greeting + " " + firstName + "!"; Se recomienda no usar variables intermedias.
Console.WriteLine(greeting + " " + firstName + "!");
```

### String Interpolation
- Se usa en situaciones en donde se deben combinar varias variables strings y variables en un solo mensaje formateado.
- Reduce el número de caracteres requeridos que se usarían en String Concatenation.
- String Interpolation combina múltiples valores en un solo literal string al usar una "plantilla" y una o más **interpolation expressions**.
- Una **Expresión de Interpolación** se indica por corchetes **{ }**.
- El literal string se convierte en una plantilla cuando se prefija por el caracter **$**.

``` C#
string message = $"{greeting} {firstName}!";
```

#### Combinar verbatim literals y string interpolation
``` C#
string projectName = "First-Project";
Console.WriteLine($@"C:\Output\{projectName}\Data");
```

- El caracter **$** se coloca antes de la directiva **@**.

## Operaciones básicas en números
### Suma
- Se usa el operados de suma **+**.
- El uso de un símbolo para varios propósitos se le llama **overloading the operator**.
- El compilador ve que el operador de suma está rodeado por dos valores numéricos (operandos), por lo que entiende que se desea sumar valores.
- Si se trata de usar el símbolo + con valores **int** y **string** values el compilador entiende que se desea concatenar los dos operandos.
    - Lo deduce ya que el símbolo + estpa rodeado por operandos de tipo de dato string e int, por lo que intenta convertir implícitamente la variable int 'widgetsSold' en un string de forma temporal para poder concatenarla.

``` C#
string firstName = "Bob";
int widgetsSold = 7;
Console.WriteLine(firstName + " sold " + widgetsSold + " widgets.");
```

- El compilador trata a todo como un string y lo concatena todo cuando los operandos no son completamente int.

``` C#
string firstName = "Bob";
int widgetsSold = 7;
Console.WriteLine(firstName + " sold " + widgetsSold + 7 + " widgets.");

Output:
Bob sold 77 widgets.

```

- Lo anterior se puede evitar con el uso de parentesis.
    - El símbolo de () se convierte en otro operador sobrecargado (overloaded operator).
        - El parentesis se puede usar para:
            - Método de invocación.
            - Orden de operaciones.
            - Casting.
    - En este caso se usa como operador para el orden de operaciones.

``` C#
string firstName = "Bob";
int widgetsSold = 7;
Console.WriteLine(firstName + " sold " + (widgetsSold + 7) + " widgets.");

Output:
Bob sold 14 widgets.
```

### Quotient (División)
- El resultado está truncado a int.
- Si se desea manejar decimales entonces se deben usar estos tipo de dato.
    - El quotient (izquierdo del operador de asignación) debe ser de tipo decimal y al menos unos de los números debe ser decimal.

``` C#
decimal decimalQuotient = 7.0m / 5;
Console.WriteLine($"Decimal quotient: {decimalQuotient}");

decimal decimalQuotient = 7 / 5.0m;
decimal decimalQuotient = 7.0m / 5.0m;
```

- Las siguientes líneas de código no funcionan (o dan resultados inexactos), ya que se usa de forma incorrecta el tipo de dato.

``` C#
int decimalQuotient = 7 / 5.0m;
int decimalQuotient = 7.0m / 5;
int decimalQuotient = 7.0m / 5.0m;
decimal decimalQuotient = 7 / 5;
```
#### División usando literal decimal data
- Se usa para evitar que el resultado sea truncado.
- Se realiza un cast de tipo de dato de **int** a **decimal**.
    - El operador de cast se agrega antes del valor.
    - Se usa el nombre de tipo de dato encerrado por parentesis en frente del valor que se le hará cast. Por ejemplo (decimal)VariableName.

``` C#
int first = 7;
int second = 5;
decimal quotient = (decimal)first / (decimal)second;
Console.WriteLine(quotient);

Output: 1.4
```

#### Módulo (modulus)
- Indica el remainder de la división.
- Ayuda a asaber si un número es divisible por otro.

``` C#
Console.WriteLine($"Modulus of 200 / 5 : {200 % 5}");
Console.WriteLine($"Modulus of 7 / 5 : {7 % 5}");

Output
Modulus of 200 / 5 : 0
Modulus of 7 / 5 : 2
```

#### Recap
- Use operators like +, -, *, and / to perform basic mathematical operations.
- The division of two int values will result in the truncation of any values after the decimal point. To retain values after the decimal point, you need to cast the divisor or  dividend (or both) from int into a floating point number like decimal first, then the quotient must be of the same floating point type as well in order to avoid truncation.
- Perform a cast operation to temporarily treat a value as if it were a different data type.
- Use the % operator to capture the remainder after division.
- The order of operations will follow the rules of the acronym PEMDAS.

### Orden de operaciones
- PEMDAS
    1. Parentheses (whatever is inside the parenthesis is performed first)
    2. Exponents
    3. Multiplication and Division (from left to right)
    4. Addition and Subtraction (from left to right)
- C# se guia de PEMDAS a exepción de los exponentes.
    - Aunque no hay operador de exponente en C#, se puede usar el método System.Math.Pow

### Incremento decremento de valores
``` C#
int value = 0;     // value is now 0.
value = value + 5; // value is now 5.
value += 5;        // value is now 10.
value ++;          // value is now 11.
```

- Estas técnicas también aplican para la resta, multiplicación y más.
- Los siguientes operadores se les conoce como **compound assignment operator** ya que realizan alguna operación además de asignar el resultado a la variable.
    - +=
    - -=
    - ++
    - *=
    - --
- El operador += se le denomina también **addition assignment operator**.

#### Posición de operadores de incremento y decremento
- Según la posición estos operadores ejecútan la operación antes o después de recuperar su valor.
    - Por ejemplo:
        - ++value el incremento ocurre antes de que el valor sea recuperado.
        - value++ incrementa el valor antes de que sea recuperado.

``` C#
int value = 1;
value++;
Console.WriteLine("First: " + value);
Console.WriteLine($"Second: {value++}"); // Retorna el valor actual de value y luego hace el incremento.
Console.WriteLine("Third: " + value);
Console.WriteLine("Fourth: " + (++value));

Output

First: 2
Second: 2
Third: 3
Fourth: 4
```

# Visual Studio Code
## Instalar .NET SDK
- Un IDE es un suite de herramientas que supports todo el ciclo de desarrollo de software.
- .NET es una plataforma de desarrollo multiplataforma y de código abierto que puede utilizarse para desarrollar distintos tipos de aplicaciones. Incluye los lenguajes de software y las bibliotecas de código que se utilizan para desarrollar aplicaciones .NET. Puede escribir aplicaciones .NET en C#, F# o Visual Basic.
- El Runtime de .NET es la librería de código requerida para correr aplicaciones C#.
- Se puede ver también que el runtime de .NET se le refiere como The Common Language Runtime, o CLR.
- No se requiere el runtime de .NET para escribir el código, pero sí para correr las aplicaciones.
- Se descarga .NET en https://dotnet.microsoft.com/es-es/download.
- Se descargan las extensiones de VS descritas en la siguiente página https://learn.microsoft.com/en-us/training/modules/install-configure-visual-studio-code/6-exercise-configure-visual-studio-code.
- El SDK incluye una interfaz de línea de comando.
- Se crean pryectos con el comando:

``` C#
dotnet new console -o ./CsharpProjects/TestProject
```

- La estructura del comando es:
    - The Driver: dotnet en este ejemplo.
    - The command: new console en este ejemplo.
    - The command arguments: -o ./CsharpProjects/TestProject
- Si se omite el argumento entonces el proyecto se crea en el path actual.
- Este comando .NET CLI utiliza una plantilla de programa .NET para crear un nuevo proyecto de aplicación de consola C# en la ubicación de carpeta especificada. El comando crea las carpetas CsharpProjects y TestProject para usted, y utiliza TestProject como el nombre de su archivo .csproj.
    - El archivo con el nombre Program.cs contiene el código C#.
- El archivo Program.cs se compila con el siguiente comando en el path en donde está el archivo.
    - Este comando bulds el proyecto y sus dependencias en un conjunto de binarios.
    - Los binarios incluyen el código del proyecto en archivos Intermediate Language (IL) con una extensión .dll.
    - Estos archvos se pueden encontrar en el Root, en las carpetas bin\Debug\net7.0
    - Este comando se puede usar gracias al .NET SDK.

``` C#
dotnet build
```

- Para correr la aplicación se ejecuta el siguiente comando.

``` C#
dotnet run
```

- Este comando corre código fuente sin ninguna compilacion explícita o comandos launch.
    - Procee de la opción para correr la aplicación desde el código fuente con un comando.
    - Es útil para un desarrollo iterativo rápido desde la línea de comando


# .NET Libraries
-  El .NET Runtime, que aloja y gestiona su código mientras se ejecuta en el ordenador del usuario final. 
-  La librería -NET Class es una colección de clases que contienen miles de métodos.
    - Por ejemplo, la librería .NET Class incluye la clase Console para desarrolladores trabajando en una aplicación de consola.
    - La clase de Console incluye métodos para ingreso y salida de operaciones tales como: Write(), WriteLine(), Read(), ReadLine(), entre otros.
    - Los métodos que pueden enviar y recibir información desde una ventana de consola son recolectados en la clase System.Console en la librería .NET.
- Incluso tipos de datos son parte de la librería .NET CLSS.
    - Tipos de datos de C# tales como string e int están disponibles gracias a las clases en la librería clase de .NET.
        - El lenguaje C# enmascara la conexión entre los tipos de dato y las clases .NET para simplificar el trabajo.
- Al . se le refiere como **operador de acceso a miembro**, mientras que () se le refiere como **operador de invocación de método**
- Ejemplos System.Random para generación aleatoria de números:
    - En la tercera línea se tiene una referencia a la clase Console y se llama al método WriteLine de forma directa.
    - Para Random se tuvo que insanciar la clase para poder usar los métodos.
    - Esto se debe a que algunos métodos son Stateful y otros Stateless.

``` C#
Random dice = new Random();
int roll = dice.Next(1, 7);
Console.WriteLine(roll);
```    

## Stateless y Stateful methods
- El término state se usa para decsribir la condición del entorno de ejecución en un momento en específico en el tiempo.
- Los métodos stateless se implementa de modo que puedan trabajar sin referenciar o cambiar cualquier valor ya almacenado en memoria.
    - También se les denomina **static methods**.
    - Por ejemplo, Console.WriteLine() no necesita de ningún gaurdado en memoria. Realiza su función y termina sin impactar el estado de la aplicación de cualquier manera.
- Otros métodos deben tener acceso al estado de la aplicación para trabajar de forma correcta.
    - Los métodos stateful están construidos de forma necesitan de valores almacenados en memoria por líneas de código previas que han sido ejecutadas.
    - Por otro lado, también pueden modificar el estado de la aplicación al actualizar valores o almacenar nuevos valores en memoria.
    - También se les denomina como **instancia de métodos**.
- Stateful (instace) methods mantienen registro de su estado en **fields**, los cuales son variables definidas en la clase.
    - Cada nueva instancia de la clase tiene una coppia propia de esos campos en donde el estado se almacena.
- Una sola clase puede tener tanto stateful y stateless métodos. Sin embargo, cuando se desea invocar stateful methods se deben instanciar primero la clases para que el método pueda acceder al estado.

## Creación de instancia de clase
- La instancia de una clase se le llama objeto.
- Se usa el operado **new**.
- En el siguiente ejemplo se crea una instancia de la clase Random para crear al objeto nuevo llamado dice:

``` C#
Random dice = new Random();
```

- El operador **new** hace lo siguiente:
    - Primero solicita una dirección en la memoria de la computadora lo suficientemente grande para almacenar un nuevo objeto basado en la clase Random.
    - Crea un nuevo objeto y lo guarda en la dirección de memoria.
    - Retorna la dirección de memoria para que pueda ser almacenada en la variable dice.
- Entonces, cuando la variable dice es referenciada el runtime .NET realizar un lookup detrás de escenas para dar la ilusión de que se trabaja con el objeto mismo.
- La última versión del Runtime .NET habilita poder instanciar un objeto sin tener que repetir el nombre del tipo (target-typed constructor invocation). Por ejemplo, en el siguiente código se crea una instancia de la clase Random.
    - Siempre se usa parentesis cuando se escribe una **target-typed new expression**.

``` C#
Random dice = new();
```

## Return Values
- Los métodos que no retornan valor se les denomina **void methods**.

## Input parameters
- La información consumida por un parámetro se le llama parámetro.
    - Parámetro se refiere a la variable que se usa dentro del método.
    - Argumento es el valor que se le pasa cuando el método se invoca.
- Los métodos usan un **method signature** para definir el número de parámetros que el método va a aceptar, así como el tipo de cada de cada parámetro.
- El argumento debe ser compatible con el tipo del parámetro.
    - La sentencia de código que invoca al método debe aderirse a los requerimientos especificados por el method signature.
    - Algunos métodos proveen d eopciones para el número y tipo de parámetros que el método acepta.
- Cuando un invocador invoca el método, proporciona valores concretos, llamados argumentos, para cada parámetro. Los argumentos deben ser compatibles con el tipo de parámetro. Sin embargo, el nombre del argumento, si se utiliza uno en el código de llamada, no tiene por qué ser el mismo que el nombre del parámetro definido en el método.
- Type checking es una manera que C# y .NET usan para prevenir que los usuarios finales tengan errores en el runtime.

## Overloaded methods
- Un overladed method es un método que supports varias implementaciones del método, cada uno con un method signature único.
- Varios métodos tienen **overloaded method signatures**.
    - Esto habilita poder invocar al método con o sin argumentos especificos en el enunciado de invocación.
- Un overloaded method se define con múltiples method signatures.
    - Overloaded Methods proveen de diferentes maneras para invocar al método o de proveer de diferentes tipos de datos.
- En algunos casos, las versiones overloaded de un método se usan para definir un parámetro de entrada usando diferentes tipos de datos.
    - Por ejemplo, Console.WriteLine() tiene 19 diferentes overloaded versions.
    - La mayoría de esos overloads permite que el método acepte diferentes tipos y luego escriva la información especificada en la consola.

``` C#
int number = 7;
string text = "seven";

Console.WriteLine(number);
Console.WriteLine();
Console.WriteLine(text);
```

- En este ejemplo se usan tres versiones overloaded del método WriteLine().
    - El primero usa un method signature que define un parámetro int.
    - El segundo usa un method signature que define cero parámetros de entrada.
    - El terceri usa un method signature que define un parametro string.
- En otros casos, overloaded versiones de un método definen un número diferente de parámetros de entrada.
    - La alternativa de parámetros de entrada pueden ser usados para proveer más control sobre el resultado deseado.
    - Por ejemplo, el método Random.Next() tiene overloaded versiones que habilitan varios conjuntos de niveles de restricciones sobre el número generado de forma aleatoria.
- Se puede leer más acerca de determinados Métodos en https://learn.microsoft.com.

## Recap
- Methods might accept no parameters or multiple parameters, depending on how they were designed and implemented. When passing in multiple input parameters, separate them with a , symbol.
- Methods might return a value when they complete their task, or they might return nothing (void).
- Overloaded methods support several implementations of the method, each with a unique method signature (the number of input parameters and the data type of each input parameter).
- IntelliSense can help write code more quickly. It provides a quick reference to methods, their return values, their overloaded versions, and the types of their input parameters.
- learn.microsoft.com is the "source of truth" when you want to learn how methods in the .NET Class Library work.

# Lógica de decisión
- Los desarrolladores se refieren al código que implementa diferentes paths de ejecuciones como **code branches**.

## IF
- Su estructura es la misma que en JavaScript.
- Se usan paréntesis para encerrar a la evaluación (Boolean expression), y de {} para encerrar el bloque de código por ejecutar.

``` C#
if (total >= 15)
{
    Console.WriteLine("You win!");
}
else if (total >= 10)
{
    Console.WriteLine("You win a new laptop!");
}
else 
{
    Console.WriteLine("Sorry, you lose.");
}
```

- Una Boolean expression es cualquier código que retorna un valor Booleano.
- Las expresiones booleanas pueden ser creadas por medio de operadores que comparan dos valores:
    - ==, operador 'equals'.
    - >, operador 'greater than'.
    - >, operador 'less than'.
    - >=, operador 'greater than or equal to'.
    - <=, operador 'less than or equal to'.
- Un Block Code es una colección de una o más líneas de código que están definindas dentro de curly braces {}.
    - Representa una unidad completa de código que tiene un función en el sistema.
    - En el Runtime todas las líneas de código en el bloque de código se ejecutan si la expresión booleana es verdadera.
- Una **Compound Condition** se da cuando se evalúan varias condiciones al mismo tiempo.

``` C#
if ((roll1 == roll2) || (roll2 == roll3) || (roll1 == roll3))
{
    Console.WriteLine("You rolled doubles! +2 bonus to total!");
    total += 2;
}
```

- Los operadores lógicos OR y AND son los mismos que con JavaScript (|| y && respectivamente).

## Recap
- Use an if statement to branch your code logic. The if decision statement will execute code in its code block if its Boolean expression equates to true. Otherwise, the runtime will skip over the code block and continue to the next line of code after the code block.
- A Boolean expression is any expression that returns a Boolean value.
- Boolean operators will compare the two values on its left and right for equality, comparison, and more.
- A code block is defined by curly braces { }. It collects lines of code that should be treated as a single unit.
- The logical AND operator && aggregates two expressions so that both subexpressions must be true in order for the entire expression to be true.
- The logical OR operator || aggregates two expressions so that if either subexpression is true, the entire expression is true.

## Arrays
- Se usan para almacenar múltiples valores del mismo tipo en una sola variable.
- Es una secuecia de elementos de datos individuales a través de una sola variable.
- Los arreglos manejan índice 0.
- Permiten recoletar datos similares que comparten un proósito comun o características en una sola estructura de datos para su preocesamiento.
- Es un tipo especial de variable.
- Para su declaración se debe especificar el tipo de dato y el tamaño del arreglo.

``` C#
string[] fraudulentOrderIDs = new string[3];
```

- El operador new crea una nueva instancia de un arreglo en la memoria de la computadora que puede contener tres valores string.
    - El primer conjunto de square brackets [] le indica al compulador que la variable es un arreglo.
    - El segundo conjunto de square brackets [3] le indica el número de elementos que el array puede contener.
- Se asignan o reasignan valores a los elementos de un arreglo por medio del uso de índices y el operador de asinación.

``` C#
string[] fraudulentOrderIDs = new string[3];

fraudulentOrderIDs[0] = "A123";
fraudulentOrderIDs[1] = "B456";
fraudulentOrderIDs[2] = "C789";
```

- Se recuperan valores del arreglo por medio de índices.

``` C#
Console.WriteLine($"First: {fraudulentOrderIDs[0]}");
Console.WriteLine($"Second: {fraudulentOrderIDs[1]}");
Console.WriteLine($"Third: {fraudulentOrderIDs[2]}");
```

- Inicialización de un arreglo
    - Se puede inicializar en su declaración.
    - Se utiliza una sintaxis que involucra curly braces {}.

``` C#
string[] fraudulentOrderIDs = { "A123", "B456", "C789" };
```

### Propiedades de array
#### Length
- No es zero-based.
``` C#
Console.WriteLine($"There are {fraudulentOrderIDs.Length} fraudulent orders to process.");
```

### Recap
- An array is a special variable that holds a sequence of related data elements.
- You should memorize the basic format of an array variable declaration.
- Access each element of an array to set or get its values using a zero-based index inside of square brackets.
- If you attempt to access an index outside of the boundary of the array, you get a run time exception.
- The Length property gives you a programmatic way to determine the number of elements in an array.

# Foreach
- Permite iterar los elementos de un arreglo.
- El enunciado foreach procesa los elementos del arreglo en un orden de índice incremental empezando ocn el índice 0 y terminando con el índice Length - 1.
- Usa una varaible temporal para mantener el valor del arreglo asociado con la iteración actual.
- Cada iteración ejecuta el bloque de código localizado debajo de la declaración foreach.

``` C#
string[] names = { "Rowena", "Robin", "Bao" };
foreach (string name in names)
{
    Console.WriteLine(name);
}
```

- La variable name es la variable temporal.

## Recap
- Use the foreach statement to iterate through each element in an array, executing the associated code block once for each element in the array.
- The foreach statement sets the value of the current element in the array to a temporary variable, which you can use in the body of the code block.
- Use the ++ increment operator to add 1 to the current value of a variable.

# Code Blocks y Variable Scope
- Es importante hacer scope a una variale en su nivel más bajo para mantener recursos de aplicación y decurity footprint pequeños.
- Un code block es uno o más enunciados C# que definen un path de ejecución.
- Las sentencias fuera de un bloque de código afectan a cuándo, si y con qué frecuencia se ejecuta ese bloque de código en tiempo de ejecución. Los límites de un bloque de código están típicamente definidos por llaves, {}.
- Además de su efecto en la ruta de ejecución, los bloques de código también pueden afectar al ámbito de las variables.
- Variable scope se refiere a la visibilidad de la variable hacia el otro código en la aplicación.
    - Una variable localmente scoped solo es accesible dentro del bloque de código en donde está definida.
- To make a variable visible inside and outside of a code block, you must define the variable outside of the code block.
- Don't forget to initialize any variables whose value is set in a code block, such as an if statement.

## Quitar Code Blocks de enunciados if
- Si un code block necesita solo de una línea de código se pueden quitar los curly braces.
- Quitar curly braces no afecta el hecho de que en este caso Console es el bloque de código del enunciado if.

``` C#
bool flag = true;
if (flag)
    Console.WriteLine(flag);
```

- Microsoft recomienda las siguinetes convenciones cuando se implementa un enunciado IF que incluye un solo enunciado de bloque de código.
    - No usar una sola línea (tal como se hace en JavaScript:  if (flag) Console.WriteLine(flag);), esto es por propósitos de redibilidad.
    - Using braces is always accepted, and required if any block of an if/else if/.../else compound statement uses braces or if a single statement body spans multiple lines.
    - Los braces pueden ser omitidos si cada block of an if/else if/.../else compound statement se coloca en una sola línea.

``` C#
string name = "steve";

if (name == "bob")
    Console.WriteLine("Found Bob");
else if (name == "steve") 
    Console.WriteLine("Found Steve");
else
    Console.WriteLine("Found Chuck");
```

# Switch statement
- Es un enunciado de selección en C# que provee una alternativa a un if-elseif-else.
- Provee de ventajas sobre if-elseif-else al evaluat un solo valor contra una lista de opciones.
- Switch escoje una sección de código para ejecutar de una lista de posibles secciones.
    - La sección de conmutación seleccionada se elige basándose en una coincidencia de patrón con la expresión de coincidencia de la sentencia.

``` C#
switch (fruit)
{
    case "apple":
        Console.WriteLine($"App will display information for apple.");
        break;

    case "banana":
        Console.WriteLine($"App will display information for banana.");
        break;

    case "cherry":
        Console.WriteLine($"App will display information for cherry.");
        break;
    default:
        title = "Associate";
        break;
}
```

- La match expression (switch expression) es el valor sigiuendo a la keyword switch.
- Cada switch section se define por un case pattern.
- Case patterns se consturyen con la keyword case seguido de un valor.
    - Case patterns son expresoines Booleanas.
- Switch se usa cuando:
    - Un solo valor (variable o expresión) se quiere comparar contra una lista de posibles valores.
    - Praa un match dado, se requiere ejecutar un par de líneas de código a lo mucho.
- La opción default puede aparecer en cualquier posición dentro de la lista de secciones de switch.
    - Sin importar la posición, default siempre se evalúa hasta el final.
- Solo una sección switch se puede evaluar.
    - No se tiene permitido pasar a la siguiente switch section si ya se ejecutó 1.
    - La keyword break es una de las formas de terminar una section switch antes de que prosiga a la siguiente.
    - Se genera un error si se omite break o return.
- Se pueden combinar cases.

``` C#
int employeeLevel = 100;
string employeeName = "John Smith";

string title = "";

switch (employeeLevel)
{
    case 100:
    case 200:
        title = "Senior Associate";
        break;
    case 300:
        title = "Manager";
        break;
    case 400:
        title = "Senior Manager";
        break;
    default:
        title = "Associate";
        break;
}

Console.WriteLine($"{employeeName}, {title}");
```

## Recap
- Use the switch statement when you have one value with many possible matches, each match requiring a branch in your code logic.
- A single switch section containing code logic can be matched using one or more labels defined by the case keyword.
- Use the optional default keyword to create a label and a switch section that will be used when no other case labels match.

# Enunciado for
- Itera a lo largo de un bloque de código un determinado número de veces.
- El enunciado foreach itera un bloque de código una vez por cada item en una secuencia de datos como un arreglo o colección.
- El enunciado while itera un bloque de código hasta que una condición se cumple.
- El enunciado for da más control sobre el proceso de iteración al exponer las condiciones para la iteración.

``` C#
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i);
}
```

- All three sections (initializer, condition, and iterator) are optional. However, in practice, typically all three sections are used.
- Se puede usar break para salir del enunciado de iteración antes según una condición.

``` C#
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i);
    if (i == 7) break;
}
```

- El uso del enunciado foreach no permite mutar al arreglo sobre el cual se itera, pero con el enunciado for sí es posible.

``` C#
string[] names = { "Alex", "Eddie", "David", "Michael" };
for (int i = 0; i < names.Length; i++)
    if (names[i] == "David") names[i] = "Sammy";

foreach (var name in names) Console.WriteLine(name); 
```

## Recap
- The for iteration statement allows you to iterate through a block of code a specific number of times.
- The for iteration statement allows you to control every aspect of the iteration's mechanics by altering the three conditions inside the parenthesis: the initializer, condition, and iterator.
- It's common to use the for statement when you need to control how you want to iterate through each item in an array.
- If your code block has only one line of code, you can eliminate the curly braces and white space if you wish.

# do-while y while
- Permiten iterar un bloque de código hasta que una condición se cumpla.
- El enunciado do-while ejecuta el código al menos una vez, luego la expresión Booleana es evaluada.
``` C#
do
{
    // This code executes at least one time
} while (true);
```
- La sintaxis de While es la siguiente:
``` C#
Random random = new Random();
int current = random.Next(1, 11);

while (current >= 3)
{
    Console.WriteLine(current);
    current = random.Next(1, 11);
}
```
- Se puede usar el enunciado continue para pasar directamente a la expresión booleana.
    - Continue transfiere el control al final del bloque de código.
        - En otras palabras, continue trasfiere la ejecución al final de la iteración actual.
        - Se diferencia con Break en que break termina la iteración o switch y trasfiere el control al enunciado que sigue el enunciado terminado. Si no hay enunciado después del enunciado terminado, el control se transfiere al final del archivo o método.

``` C#
Random random = new Random();
int current = random.Next(1, 11);

do
{
    current = random.Next(1, 11);

    if (current >= 8) continue;

    Console.WriteLine(current);
} while (current != 7);
```

# Data type
- Data es un valor almacenado en la memoria de la computadora como una ser de bit..
- Un bit es un switch binario simple representado como 0 o 1, o off y on.
- Byte es la combinación de 8 bits en una secuencia.
- La computadora usa un sistema como ASCII para usar un single byte para representar upper y lowercase letters, números, tab, ackspace, newline y varios símbolos matemáticos.
- Data type es una forma del lenguaje de programación que define cuánta memoria se salva para un valor.

## Value Types vs reference types
- Reference types incluyen arreglos, clase y strings. Reference types son tratados diferente que value types con respecto a la manera en que almacenan los valores cuando la aplicación se ejecuta.
- Las variables de tipo referencia almacenan referencias a sus datos (objetos), es decir, apuntan a valores de datos almacenados en otro lugar.
- Las variables de tipo valor (variables of value types) contienen directamente sus datos. 
- Un Value type variable almacena su valor directamente en un área de almacenamiento llamado el **stack**.
    - Una variable de tipo valor (value type variable) almacena sus valores directamente en un área de almacenamiento llamada pila (stack). La pila es la memoria asignada al código que se está ejecutando actualmente en la CPU (también conocida como marco de pila o marco de activación). Cuando el marco de pila ha terminado de ejecutarse, los valores de la pila se eliminan.
    - Una variable de tipo referencia (refrence type variable) almacena sus valores en una región de memoria separada llamada **heap**. El **heap** es una zona de memoria compartida por muchas aplicaciones que se ejecutan simultáneamente en el sistema operativo. El .NET Runtime se comunica con el sistema operativo para determinar qué direcciones de memoria están disponibles y solicita una dirección en la que pueda almacenar el valor. El .NET Runtime almacena el valor y, a continuación, devuelve la dirección de memoria a la variable. Cuando su código utiliza la variable, el .NET Runtime busca sin problemas la dirección almacenada en la variable, y recupera el valor que está almacenado allí.
- Por ejemplo:

``` C#
int[] data;
```
- En este punto, data solamente es una variale que puede hold a reference, o mejor dicho, una dirección de memoria de un valor en el heap. Ya que no está apuntando a una dirección de memoria se le llama **null reference**.
- Luego, al usar la palabra reservada new para crear una instancia de un arreglo int.
    - new le informa al .NET crar una instancia de arreglo int,  and then coordinate with the operating system to store the array sized for three int values in memory. The .NET Runtime complies, and returns a memory address of the new int array. Finally, the memory address is stored in the variable data. The int array's elements default to the value 0, because that is the default value of an int.

``` C#
int[] data;
data = new int[3];
```

- Para el caso del tipo de dato de string, también es un reference type. Ya que este tipo de data se usa frecuentemente se diseñó por conveniencie para no tener que usar el operador NEW cada que se declara un nuevo string.
    - Sin embargo, detrás de escenas se crea una nueva instancia de System.String se crea e inicializa con ''

``` C#
string shortenedString = "Hello World!";
Console.WriteLine(shortenedString);
```

#### Recap
- Value types can hold smaller values and are stored in the stack. Reference types can hold large values, and a new instance of a reference type is created using the new operator. Reference type variables hold a reference (the memory address) to the actual value stored in the heap.
- Reference types include arrays, strings, and classes.

### Simple value types
- Simple value types are a set of predefined types provided by C# as keywords.
- Estos keywords son aliases para los tipos predefinidios definidos en la librería de clase .NET.
- Por ejemplo, la palabra reservada C# int es un alias de un tipo de valor deifnido en la librería de clase .NET System.Int32.
- Los tipos de valor simple incluyen varios tipos de datos char o bool. También hay varios tipos de dato enteros y de puntos flotante que representan un amplio rango de número enteros y fracionales.

#### Integral Types
- La categoría más popular es el tipo de dato int.
- Dos subcategorias de tipos integrales son: signed y unsigned integral values.
- Un signed value usa sus bytes para representar un número igual de números positivos y negativos.
- Un unsiged type usa sus bytes para representar solo números positivos.


## Choose specialty complex types for special situations
https://learn.microsoft.com/en-us/training/modules/csharp-choose-data-type/6-choose-right-data-type
Don't reinvent data types if one or more data type already exists for 
a given purpose. The following examples identify where a specific .NET data types can be useful:

- byte: working with encoded data that comes from other computer systems or using different character sets.
- double: working with geometric or scientific purposes. double is used frequently when building games involving motion.
- System.DateTime for a specific date and time value.
- System.TimeSpan for a span of years / months / days / hours / minutes / seconds / milliseconds.

## Recap
- Values are stored as bits, which are simple on / off switches. Combining enough of these switches allows you to store just about any possible value.
- There are two basic categories of data types: value and reference types. The difference is in how and where the values are stored by the computer as your program executes.
- Simple value types use a keyword alias to represent formal names of types in the .NET Library.
- An integral type is a simple value data type that can hold whole numbers.
- There are signed and unsigned numeric data types. Signed integral types use 1 bit to store whether the value is positive or negative.
- You can use the MaxValue and MinValue properties of numeric data types to evaluate whether a number can fit in a given data type.

# Conversión de datos usando casting y técnicas de conversión en C#
- La técnica a utilizar dependente de las preguntas:
    - Is it possible, depending on the value, that attempting to change the value's data type would throw an exception at run time?
    - Is it possible, depending on the value, that attempting to change the value's data type would result in a loss of information?

## Pregunta 1
- El compilador de C# intenta acomodar el código, pero no compila operaciones que puedan resultar en exception.
- Los compiladores hacen conversiones seguras.
- En el siguiente ejemplo, si el compilador C# intentara convertir el string a un número causaría una exepción en runtime, por lo que para evitar esa posibilidad el compilador C# no realiza la conversión implícita de String a int, sino que usa la operación más segura, la cual es convertir int en un string y realizar una concatenación.

``` C#
int first = 2;
string second = "4";
int result = first + second;
Console.WriteLine(result);
```

- Si se intentara hacer una suma usando un string, se debe cambiar el tipo de dato y evitar que se produzca una exepción en runtime (data conversion).
- Se tienen varias técnicas para data conversion:
    - Usar helper method en el tipo de dato.
    - Usar métodos de clase Convert.

## Pregunta 2.
- Se tiene el término **widening conversion**, el cual significa que se intenta convertir un valor desde un tipo de dato que podría contener menos información a un tipo de dato que puede contener más información. Por ejemplo, convertir un int a un decimal no hace que se pierda información, ya que int cabe perfectamente en un decimal.

``` C#
int myInt = 3;
Console.WriteLine($"int: {myInt}");

decimal myDecimal = myInt;
Console.WriteLine($"decimal: {myDecimal}");
```

## Perform Cast
- Se realiza por medio del operador de casting (), en donde se encierra un tipo de datos y colocarlo a lado de la variable que se desea convertir (int)myDecimal.
- Se ejecuta una explicit conversion al defined cast data type (int).

``` C#
decimal myDecimal = 3.14m;
Console.WriteLine($"decimal: {myDecimal}");

int myInt = (int)myDecimal;
Console.WriteLine($"int: {myInt}");
```

- La variable myDecimal mentiene un valor que tiene una precision después del punto decimal. Al hacer casting a int se le dice al compilador C# que se entiende que se pierde precisión. Entonces, se le indica al compilador que es una intentional convertion, an explicit conversion

### Narrowing conversion y widening conversion
- Narrowing Conversion significa que se intenta convertir un valor de un tipo de dato que puede tener más información a un tipo de dato que contiene menos información.
    - Se pierde precisión (el número de valores después del punto decimal).
    - Pro ejemplo, convertir de decimal a int.
    - Cuando se realiza narrowing conversion se debe hacer cast. Casting es una instrucción para el compilador de C# de que se sabe que la precisión se puede perder, pero se acepta.

## Data Conversiones
- Se tienen 3 técnicas:
    - Helper method en la variable.
    - Helper method en el tipo de dato.
    - Métodos de la clase Convert

### ToString para convertir número a string
- En los tipos más primitivos realiza un widening conversion.

``` C#
C#

Copy
int first = 5;
int second = 7;
string message = first.ToString() + second.ToString();
Console.WriteLine(message);
```

### Convertir string a int usando Parse() helper method
``` C#
string first = "5";
string second = "7";
int sum = int.Parse(first) + int.Parse(second);
Console.WriteLine(sum);
```

- Se recomienda usa TryParse() para mitigar exceptions.
    - Intenta parsear un string en el tipo de dato numérico dado.
    - Si tiene éxito, almacena el valor convertido en un out parameter.
    - Retorna un booleanos para indicar si la acción fue éxitosa o no.

#### Out Parameters
- Los métodos, además de poder retornar un valor o void, pueden retornar valores a través de parametros out, los cuales son definidos como input parameter pero incluyen la palabra reservada out.

``` C#
string value = "102";
int result = 0;
if (int.TryParse(value, out result))
{
    Console.WriteLine($"Measurement: {result}");
}
else
{
    Console.WriteLine("Unable to report the measurement.");
}

Console.WriteLine($"Measurement (w/ offset): {50 + result}");
```

- El parámetro out es asignado la variable result en el código. Se puede usar el valor de out parameter en el resto del código usando la variable result.
- La palabra reservada out indica al compilador que el método TryParse no va a retornar un valor de la forma tradicional (as a return value), sino que también va a comunicar una salida a través de este two-way parameter.

#### Recap
- Use TryParse() when converting a string into a numeric data type.
- TryParse() returns true if the conversion is successful, false if it's unsuccessful.
- Out parameters provide a secondary means of a method returning a value. In this case, the out parameter returns the converted value.
- Use the keyword out when passing in an argument to a method that has defined an out parameter.

### Convertir string a int usando Convert class
``` C#
string value1 = "5";
string value2 = "7";
int result = Convert.ToInt32(value1) * Convert.ToInt32(value2);
Console.WriteLine(result);
```
- Why is the method name ToInt32()? Why not ToInt()? System.Int32 is the name of the underlying data type in the .NET Class Library that the C# programming language maps to the keyword int. Because the Convert class is also part of the .NET Class Library, it is called by its full name, not its C# name. By defining data types as part of the .NET Class Library, multiple .NET languages like Visual Basic, F#, IronPython, and others can share the same data types and the same classes in the .NET Class Library.

### Casting y conveting a decimal into an int
``` C#
int value = (int)1.5m; // casting truncates
Console.WriteLine(value); // 1

int value2 = Convert.ToInt32(1.5m); // converting rounds up
Console.WriteLine(value2); // 2
```

- When you're casting int value = (int)1.5m;, the value of the float is truncated so the result is 1, meaning the value after the decimal is ignored completely. you could change the literal float to 1.999m and the result of casting would be the same.
- When you're converting using Convert.ToInt32(), the literal float value is properly rounded up to 2. If you changed the literal value to 1.499m, it would be rounded down to 1.

### Recap
- Prevent a runtime error while performing a data conversion
- Perform an explicit cast to tell the compiler you understand the risk of losing data
- Rely on the compiler to perform an implicit cast when performing an expanding conversion
- Use the () cast operator and the data type to perform a cast (for example, (int)myDecimal)
- Use the Convert class when you want to perform a narrowing conversion, but want to perform rounding, not a truncation of information
- CAST es el mejor para convertir de Decimal a INT.

# Operaciones en arreglos con helper methods
## Sort() y Reverse()
- Sort ordena ya sea alfanumericamente o por valores numéricos.

``` C#
string[] pallets = { "B14", "A11", "B12", "A13" };

Console.WriteLine("Sorted...");
Array.Sort(pallets);
foreach (var pallet in pallets)
{
    Console.WriteLine($"-- {pallet}");
}


Sorted...
-- A11
-- A13
-- B12
-- B14

```

``` C#
string[] pallets = { "B14", "A11", "B12", "A13" };

Console.WriteLine("Sorted...");
Array.Sort(pallets);
foreach (var pallet in pallets)
{
    Console.WriteLine($"-- {pallet}");
}

Console.WriteLine("");
Console.WriteLine("Reversed...");
Array.Reverse(pallets);
foreach (var pallet in pallets)
{
    Console.WriteLine($"-- {pallet}");
}

Sorted...
-- A11
-- A13
-- B12
-- B14

Reversed...
-- B14
-- B12
-- A13
-- A11

```

- The Array class has methods that can manipulate the size and contents of an array.
- Use the Sort() method to manipulate the order based on the given data type of the array.
- Use the Reverse() method to flip the order of the elements in the array.

## Clear and Resize
- Array.Clear() permite quitar conteido de elementos especificos en arreglo y reemplazarlos con el valor de arreglo por defecto.
    - Para un arreglo string el valor por defecto es null, mientras que para int es 0.
    - Los elementos limpiados ya no hacen referencia a un string en memoria. El elemento apunta a nothing.
    - Se pasa el arreglo, el índice de inicio y la cantidad de elementos a limpiar.
- Array.Resize() agrega o quita elementos del arreglo.

``` C#
string[] pallets = { "B14", "A11", "B12", "A13" };
Console.WriteLine("");

Array.Clear(pallets, 0, 2);
Console.WriteLine($"Clearing 2 ... count: {pallets.Length}");
foreach (var pallet in pallets)
{
    Console.WriteLine($"-- {pallet}");
}

Clearing 2 ... count: 4
-- 
-- 
-- B12
-- A13

```

- Si se intenta acceder a un elemento limpio el compilador C# implicitamente conviete el vlaor null en un string vacío para su representación.

``` C#
string[] pallets = { "B14", "A11", "B12", "A13" };
Console.WriteLine("");

Console.WriteLine($"Before: {pallets[0]}");
Array.Clear(pallets, 0, 2);
Console.WriteLine($"After: {pallets[0]}");

Console.WriteLine($"Clearing 2 ... count: {pallets.Length}");
foreach (var pallet in pallets)
{
    Console.WriteLine($"-- {pallet}");
}


Before: B14
After:
Clearing 2 ... count: 4
--
--
-- B12
-- A13
```

# Glosario
## Expresion
- Es cualquier combinación de valores (literal o variable), operadores y métodos que retornan un solo valor.
- Un enunciado es una instrucción completa en C#.
    - Los enunciados están compuestos de una o más expresiones.

## Logical Negation
- Se refiere al operador unario de negación !.
- Se le puede referir también como not operator.

## Conditional operator
- El operador condicional **?:** evalua una expresión Boolena y retorna uno de los dos resultados dependendiendo si la expresiones booleana evalua falso o verdadero.
- También se lre refiere como el operadot condicional ternario (ternary conditoinal operator).
- Es el operador ternario en JavaScript.

``` C#
<evaluate this condition> ? <if condition is true, return this value> : <if condition is false, return this value>
```

## Console
### WriteLine.
- Coloca un salto de línea al final del output.

### Write
- No coloca salto de linea al final del output.
- Si se escriben varios Console.Write entonces los resultados aparecen en una sola línea.

``` C#
Console.WriteLine("This is the first line.");

Console.Write("This is ");
Console.Write("the second ");
Console.Write("line.");
```

## System.Globalization
- Permite cambiar el formato de lo números (coma en lugar de punto).
https://learn.microsoft.com/en-us/training/modules/csharp-convert-cast/4-challenge