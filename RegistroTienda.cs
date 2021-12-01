using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TALLER_2_1000897727_1007111554_1214738496
{
    public class RegistroTienda
    {
        private string nombreLibro;
        private string autor;
        private int cantidad;
        private string genero;
        private decimal precio;
        //constructor por defecto
        public RegistroTienda()
        {
           
        }
        //constructor con los 5 parametros
        public RegistroTienda(string nombreLibro, string autor, int cantidad, string genero, decimal precio)
        {
            this.nombreLibro = nombreLibro;
            this.autor = autor;
            this.cantidad = cantidad;
            this.genero = genero;
            this.precio = precio;
        }
        //encapsulamiento de paramentros (precedimiento de propiedad o auto propiedad)
        public string Nombre_Libro { get => nombreLibro; set => nombreLibro = value; }
        public string Autor { get => autor; set => autor = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public string Genero { get => genero; set => genero = value; }
        public decimal Precio { get => precio; set => precio = value; }
        //metodo estatico
        public static int NumeroDeRegistro = 0;
    }
}
