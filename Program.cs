using InventarioApp.Models;
using InventarioApp.Services;

var servicios = new InventarioServices();
bool activo = true;

while (activo)
{
    MostrarMenu();
    string opcion = Console.ReadLine() ?? "";

    switch (opcion)
    {
        case "1":
            AgregarProducto();
            break;
        case "2":
            ListarProductos();
            break;
        case "3":
            BuscarPorId();
            break;
        case "4":
            EliminarProducto();
            break;
        case "5":
            BuscarPorCategoria();
            break;
        case "6":
            MostrarResumen();
            break;
        case "7":
            MostrarStockBajo();
            break;
        case "8":
            MostrarEstadisticas();
            break;
        case "9":
            ExportarCsv();
            break;
        case "10":
            activo = false;
            Console.WriteLine("\nHasta luego!");
            break;
        default:
            Console.WriteLine("\nOpción no válida. Intente nuevamente.");
            break;
    }

    void MostrarMenu()
    {
        Console.WriteLine("\n=== Menú de Inventario ===");
        Console.WriteLine("1. Agregar producto");
        Console.WriteLine("2. Listar productos");
        Console.WriteLine("3. Buscar producto por ID");
        Console.WriteLine("4. Eliminar producto");
        Console.WriteLine("5. Buscar productos por categoría");
        Console.WriteLine("6. Mostrar resumen del inventario");
        Console.WriteLine("7. Mostrar productos con stock bajo");
        Console.WriteLine("8. Mostrar estadísticas del inventario");
        Console.WriteLine("9. Exportar inventario a CSV");
        Console.WriteLine("10. Salir");
        Console.Write("Seleccione una opción: ");
    }

    void AgregarProducto()
    {
        Console.Write("\nNombre: ");
        string nombre = Console.ReadLine() ?? "";
        Console.Write("Precio: ");
        decimal precio = decimal.TryParse(Console.ReadLine(), out decimal p) ? p : 0;
        Console.Write("Cantidad: ");
        int cantidad = int.TryParse(Console.ReadLine(), out int c) ? c : 0;
        Console.WriteLine("\nCategoría : Electronica, Ropa, Alimentos, Hogar, Otros");
        Console.Write("Categoría: ");
        string categoriaStr = Console.ReadLine() ?? "Otros";

        if (Enum.TryParse<CategoriaProducto>(categoriaStr, true, out var categoria))
        {
            servicios.AgregarProducto(nombre, precio, cantidad, categoria);
            Console.WriteLine("\nProducto agregado exitosamente.");
        }
        else
        {
            Console.WriteLine("\nCategoría no válida. Producto no agregado.");
        }
        
    }

    void ListarProductos()
    {
        var productos = servicios.ObtenerProductos();
        if (!productos.Any())
        {
            Console.WriteLine("\nNo hay productos en el inventario.");
            return;
        }

        Console.WriteLine("\n=== Lista de Productos ===");
        foreach (var producto in productos)
        {
            Console.WriteLine($"ID: {producto.Id} | Nombre: {producto.Nombre} | Precio: ${producto.Precio:F2} | Cantidad: {producto.Cantidad} | Total: {producto.ValorTotal:F2} | Categoría: {producto.Categoria}");
        }
    }

    void BuscarPorId()
    {
        Console.Write("\nIngrese el ID del producto: ");
        int id = int.TryParse(Console.ReadLine(), out int i) ? i : 0;

        var producto = servicios.ObtenerProductoPorId(id);
        if(producto != null)
        {
            Console.WriteLine($"\nID: {producto.Id}");
            Console.WriteLine($"Nombre: {producto.Nombre}");
            Console.WriteLine($"Precio: ${producto.Precio:F2}");
            Console.WriteLine($"Cantidad: {producto.Cantidad}");
            Console.WriteLine($"Valor Total: ${producto.ValorTotal:F2}");
            Console.WriteLine($"Categoría: {producto.Categoria}");
        }
        else
        {
            Console.WriteLine("\nProducto no encontrado.");
        }
    }

    void EliminarProducto()
    {
        Console.Write("\nIngrese el ID del producto a eliminar: ");
        int id = int.TryParse(Console.ReadLine(), out int i) ? i : 0;

        var producto = servicios.ObtenerProductoPorId(id);
        if (producto != null)
        {
            servicios.EliminarProducto(id);
            Console.WriteLine("\nProducto eliminado exitosamente.");
        }
        else
        {
            Console.WriteLine("\nProducto no encontrado.");
        }
    }

    void BuscarPorCategoria(){
        Console.WriteLine("\nCategorias: Electronica, Ropa, Alimentos, Hogar, Otros");
        Console.Write("Ingrese la categoría a buscar: ");
        string categoriaStr = Console.ReadLine() ?? "Otros";

        if (Enum.TryParse<CategoriaProducto>(categoriaStr, true, out var categoria))
        {
            var productos = servicios.BuscarPorCategoria(categoria);
            if (!productos.Any())
            {
                Console.WriteLine("\nNo hay productos en esta categoria.");
                return;
            }
            Console.WriteLine($"\n=== Productos en categoría {categoria} ===");
            foreach (var producto in productos)
            {
                Console.WriteLine($"ID: {producto.Id} | Nombre: {producto.Nombre} | Precio: ${producto.Precio:F2} | Cantidad: {producto.Cantidad}");
            }
        }
        else
        {
            Console.WriteLine("\nCategoría no válida.");
        }
    }

    void MostrarResumen()
    {
        string reporte = servicios.GenerarResumen();
        Console.WriteLine($"\n{reporte}");
    }

    void MostrarStockBajo()
    {
        var reporte = servicios.GenerarReporteStockBajo(minimo: 5);
        Console.WriteLine($"\n{reporte}");
    }

    void MostrarEstadisticas()
    {
        Console.WriteLine("\n=== Estadísticas del Inventario ===");
        Console.WriteLine($"Valor total del inventario: ${servicios.ObtenerValorTotalInventario():F2}");
        Console.WriteLine($"Precio promedio: ${servicios.ObtenerPrecioPromedio():F2}");

        var masCaro = servicios.ObtenerProductoMasCaro();
        if (masCaro != null)
        {
            Console.WriteLine($"Producto más caro: {masCaro.Nombre} | Precio: ${masCaro.Precio:F2}");
        }
        else
        {
            Console.WriteLine("No hay productos en el inventario.");
        }
    }

    void ExportarCsv()
    {
        string csv = servicios.ExportarCsv();
        Console.WriteLine($"\n{csv}");
    }


}