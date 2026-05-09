using InventarioApp.Models;
using InventarioApp.Repositories;
using InventarioApp.Infrastructure;
using InventarioApp.Factories;

namespace InventarioApp.Services;

public class InventarioServices
{
    private readonly InMemoryProductoRepository _repository;
    private readonly JsonInventarioStorage _storage;
    private readonly string _rutaInventario;

    public InventarioServices(string rutaInventario = "inventario.json")
    {
        _repository = new InMemoryProductoRepository();
        _storage = new JsonInventarioStorage();
        _rutaInventario = rutaInventario;

        CargarInventario();
    }

    public void CargarInventario()
    {
        if (File.Exists(_rutaInventario))
        {
            var productos = _storage.Cargar(_rutaInventario);
            foreach (var producto in productos)
            {
                _repository.Agregar(producto);
            }
        }
    }

    public void AgregarProducto(string nombre, decimal precio, int cantidad, CategoriaProducto categoria)
    {
        var producto = ProductFactory.Crear(nombre, precio, cantidad, categoria);
        _repository.Agregar(producto);
        _Persitir();
    }

    public IEnumerable<Producto> ObtenerProductos()
    {
        return _repository.ObtenerTodos();
    }

    public Producto? ObtenerProductoPorId(int id)
    {
        return _repository.ObtenerPorId(id);
    }

    public void ActualizarProducto(int id, string nombre, decimal precio, int cantidad, CategoriaProducto categoria)
    {
        var producto = _repository.ObtenerPorId(id);
        if (producto != null)
        {
            producto.Nombre = nombre;
            producto.Precio = precio;
            producto.Cantidad = cantidad;
            producto.Categoria = categoria;
            _repository.Actualizar(producto);
            _Persitir();
        }
    }

    public void EliminarProducto(int id)
    {
        _repository.Eliminar(id);
        _Persitir();
    }

    public IEnumerable<Producto> BuscarPorCategoria(CategoriaProducto categoria)
    {
        return _repository.BuscarPorCategoria(categoria);
    }

    public IEnumerable<Producto> BuscarPorNombre(string nombre)
    {
        return _repository.BuscarPorNombre(nombre);
    }

    public IEnumerable<Producto> ObtenerPorductoBajoStock(int minimo = 5)
    {
        return _repository.ObtenerStockBajo(minimo);
    }

    public decimal ObtenerValorTotalInventario()
    {
        return _repository.ObtenerValorTotalInventario();
    }

    public decimal ObtenerPrecioPromedio()
    {
        return _repository.ObtenerPrecioPromedio();
    }

    public Producto? ObtenerProductoMasCaro()
    {
        return _repository.ObtenerProductoMasCaro();
    }


    // ============ Métodos de reportes =============

    public string GenerarResumen()
    {
        var producto = _repository.ObtenerTodos();
        var generador = new GeneradorReportes(producto);
        return generador.GenerarResumen();
    }

    public string GenerarReporteStockBajo(int minimo = 5)
    {
        var producto = _repository.ObtenerTodos();
        var generador = new GeneradorReportes(producto);
        return generador.GenerarReporteStockBajo(minimo);
    }

    public string GenerarTopProductos(int cantidad = 5)
    {
        var producto = _repository.ObtenerTodos();
        var generador = new GeneradorReportes(producto);
        return generador.GenerarTopProductos(cantidad);
    }

    public string ExportarCsv()
    {
        var producto = _repository.ObtenerTodos();
        var generador = new GeneradorReportes(producto);
        return generador.ExportarCsv();
    }

    // ============ Métodos privados =============

    private void _Persitir()
    {
        _storage.CrearBackup(_rutaInventario);
        var productos = _repository.ObtenerTodos().ToList();
        _storage.Guardar(productos, _rutaInventario);
    }
}