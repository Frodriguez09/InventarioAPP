namespace InventarioApp.Repositories;

using InventarioApp.Models;

///<summary>
/// Contrato para almacenamiento de productos.
/// Define las operaciones basicas CRUD.
/// </summary>

public interface IProductoRepository
{
    /// <summary>
    /// Agrega un nuevo producto al repositorio.
    /// </summary>
    
    void Agregar(Producto producto);

    /// <summary>
    /// Obtiene un producto por su ID.
    /// Retorna null si no se encuentra.
    /// </summary>
    Producto? ObtenerPorId(int id);

    /// <summary>
    /// Obtiene todos los productos almacenados.
    /// </summary>
    IEnumerable<Producto> ObtenerTodos();

    /// <summary>
    /// Actualiza un producto existente.
    /// <summary>
    bool Actualizar(Producto producto);

    /// <summary>
    /// Elimina un producto por su ID.
    /// Retorna true si se elimino correctamente, false si no se encontro.
    /// </summary>
    bool Eliminar(int id);

    /// <summary>
    /// Cantidad total de productos en el repositorio.
    /// </summary>
    int Cantidad { get; }
    
}