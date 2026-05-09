namespace InvetarioApp.Models;

// <summary> 
// Records inmutables para representar proveedores.
// </summary>

public record Proveedor
(
    int Id,
    string Nombre,
    string Email,
    string Telefono
);


