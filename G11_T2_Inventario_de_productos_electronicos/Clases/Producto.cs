using G11_SistemaInventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_SistemaInventario.Models
{
    public class G11_Product
    {
        public string G11_Nombre { get; set; }
        public G11_Category G11_Categoria { get; }
        public G11_Category G5_Categoria { get; set; } // Usamos la clase Category
        public decimal G11_Precio { get; set; }
        public int G11_Cantidad { get; set; }
        public string G11_Codigo { get; }
        public string G5_Codigo { get; set; } // Puede ser string si contiene letras/numeros

        public G11_Product(string nombre, G11_Category categoria, decimal precio, int cantidad, string codigo, G11_Category g11_Categoria)
        {
            G11_Nombre = nombre;
            G11_Categoria = categoria;
            G11_Precio = precio;
            G11_Cantidad = cantidad;
            G11_Codigo = codigo;
            G11_Categoria = g11_Categoria;
        }

        // Este método ToString() es útil para depuración o si quisieras mostrar el producto como string
        public override string ToString()
        {
            return $"Nombre: {G11_Nombre}, Categoría: {G5_Categoria.G5_Name}, Precio: {G11_Precio}, Cantidad: {G11_Cantidad}, Código: {G11_Codigo}";
        }
    }
}