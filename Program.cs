// ===============================================================
// SISTEMA DE INVENTARIO - APLICACIÓN DE GESTIÓN DE PRODUCTOS
// Estado: Estructura profesional configurada
// ===============================================================

using System.Reflection;

var assembly = System.Reflection.Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;

Console.WriteLine("||==============================================||");
Console.WriteLine("||  SISTEMA DE GESTION DE INVENTARIO            ||");
Console.WriteLine("||==============================================||");
Console.WriteLine("");
Console.WriteLine($"Plataforma: {Environment.OSVersion}");
Console.WriteLine($"Versión de .NET: {Environment.Version}");
Console.WriteLine($"Versión: {version}");
Console.WriteLine("");
Console.WriteLine("Estrutura del proyecto");
Console.WriteLine("Configuracion .csproj");
Console.WriteLine("Carpeta src/ creada");
Console.WriteLine("Metadatos configurados");
Console.WriteLine("");
Console.WriteLine("Proximo paso: Agregar argumentos CLI y configuracion de repositorio en GitHub");
