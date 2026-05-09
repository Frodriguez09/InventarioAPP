using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using InventarioApp.Models;

namespace InventarioApp.Infrastructure;

public class GeneradorReportes
{
    private readonly IEnumerable<Producto> _productos;
    public GeneradorReportes(IEnumerable<Producto> productos)
    {
        _productos = productos;
    }

    public string GenerarResumen()
    {
        var sb = new StringBuilder();
        sb.AppendLine("=== Resumen del Inventario ===");
        sb.AppendLine($"Total de productos: {_productos.Count()}");
        sb.AppendLine($"Valor total del inventario: ${_productos.Sum(p => p.ValorTotal):F2}");

        var productosPorCategoria = _productos.GroupBy(p => p.Categoria).Select(g => new { Categoria = g.Key, Cantidad = g.Count() });
        sb.AppendLine("\nProductos por categoría:");
        foreach (var categoria in productosPorCategoria)
        {
            sb.AppendLine($"- {categoria.Categoria}: {categoria.Cantidad}");
        }

        return sb.ToString();
    }

    public string GenerarReporteStockBajo(int minimo = 5)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"=== Reporte de Stock Bajo (mínimo {minimo}) ===");

        var stockBajo = _productos.Where(p => p.Cantidad < minimo).OrderBy(p => p.Cantidad);
        if (!stockBajo.Any())
        {
            sb.AppendLine("No hay productos con stock bajo.");
            return sb.ToString();
        }
        foreach (var producto in stockBajo)
        {
            sb.AppendLine($"ID: {producto.Id}, Nombre: {producto.Nombre}, Stock: {producto.Cantidad} | ${producto.Precio:F2}");
        }

        return sb.ToString();
    }

    public string GenerarTopProductos(int cantidad = 5)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"=== Top {cantidad} Productos por Valor Total ===");

        var top = _productos.OrderByDescending(p => p.ValorTotal).Take(cantidad);
        
        if (!top.Any())
        {
            sb.AppendLine("No hay productos disponibles.");
            return sb.ToString();
        }
        int posicion = 1;
        foreach (var producto in top)
        {
            sb.AppendLine($"{posicion}. {producto.Nombre} | Cantidad: {producto.Cantidad} | Valor: ${producto.ValorTotal:F2}");
            posicion++;
        }

        return sb.ToString();
    }

    public string ExportarCsv()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id,Nombre,Precio,Cantidad,Categoria,ValorTotal");

        foreach (var producto in _productos.OrderBy(p => p.Id))
        {
            sb.AppendLine($"{producto.Id},{producto.Nombre},{producto.Precio:F2},{producto.Cantidad},{producto.Categoria},{producto.ValorTotal:F2}");
        }

        return sb.ToString();
    }
}
