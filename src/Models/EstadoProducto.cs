namespace InventarioApp.Models;

public enum EstadoProducto
{
    // <summary> Disponible para la venta </summary>
    Activo,
    // <summary> Temporalmente no disponible </summary>
    Inactivo,
    // <summary> Retirado permanentemente del catalogo </summary>
    Descontinuado
}