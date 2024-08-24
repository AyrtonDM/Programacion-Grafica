using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea2
{
    internal class DibujarFormas
    {
        Cara cara1; 
        Cara cara2;
        Cara cara3;
        Cara cara4;
        Cara cara5;
        Cara cara6;
        public DibujarFormas() 
        { 
            
        }

        public void cuadradoCubico(Relativo inicio, float ancho, float largo, float profundidad)
        {
            inicio.Actualizar();
            cara1 = new Cara( //frontal
                new Relativo(inicio.newPoint, inicio.X, inicio.Y, inicio.Z),
                new Relativo(inicio.newPoint, inicio.X + ancho, inicio.Y, inicio.Z),
                new Relativo(inicio.newPoint, inicio.X + ancho, inicio.Y + largo, inicio.Z),
                new Relativo(inicio.newPoint, inicio.X, inicio.Y + largo, inicio.Z)
                );
            cara2 = new Cara( //trasera
                new Relativo(inicio.newPoint, inicio.X, inicio.Y, inicio.Z - profundidad),
                new Relativo(inicio.newPoint, inicio.X + ancho, inicio.Y, inicio.Z - profundidad),
                new Relativo(inicio.newPoint, inicio.X + ancho, inicio.Y + largo, inicio.Z - profundidad),
                new Relativo(inicio.newPoint, inicio.X, inicio.Y + largo, inicio.Z - profundidad)
                );
            cara3 = new Cara( //superior
                new Relativo(inicio.newPoint, inicio.X, inicio.Y + largo, inicio.Z),
                new Relativo(inicio.newPoint, inicio.X + ancho, inicio.Y + largo, inicio.Z),
                new Relativo(inicio.newPoint, inicio.X + ancho, inicio.Y + largo, inicio.Z - profundidad),
                new Relativo(inicio.newPoint, inicio.X, inicio.Y + largo, inicio.Z - profundidad)
                );
            cara4 = new Cara( //inferior
                new Relativo(inicio.newPoint, inicio.X, inicio.Y, inicio.Z),
                new Relativo(inicio.newPoint, inicio.X + ancho, inicio.Y, inicio.Z),
                new Relativo(inicio.newPoint, inicio.X + ancho, inicio.Y, inicio.Z - profundidad),
                new Relativo(inicio.newPoint, inicio.X, inicio.Y, inicio.Z - profundidad)
                );
            cara5 = new Cara( //lateral izquierdo
                new Relativo(inicio.newPoint, inicio.X, inicio.Y, inicio.Z),
                new Relativo(inicio.newPoint, inicio.X, inicio.Y, inicio.Z - profundidad),
                new Relativo(inicio.newPoint, inicio.X, inicio.Y + largo, inicio.Z - profundidad),
                new Relativo(inicio.newPoint, inicio.X, inicio.Y + largo, inicio.Z)
                );
            cara6 = new Cara( //lateral derecho
                new Relativo(inicio.newPoint, inicio.X + ancho, inicio.Y, inicio.Z),
                new Relativo(inicio.newPoint, inicio.X + ancho, inicio.Y, inicio.Z - profundidad),
                new Relativo(inicio.newPoint, inicio.X + ancho, inicio.Y + largo, inicio.Z - profundidad),
                new Relativo(inicio.newPoint, inicio.X + ancho, inicio.Y + largo, inicio.Z)
                );

        }

        public void DibujarCuadrado()
        {
            cara1.Dibujar(255,0,0);
            cara2.Dibujar(0,255,0);
            cara3.Dibujar(255,255,255);
            cara4.Dibujar(0,0,0);
            cara5.Dibujar(0,0,255);
            cara6.Dibujar(0,255,255);
        }
    }
}
