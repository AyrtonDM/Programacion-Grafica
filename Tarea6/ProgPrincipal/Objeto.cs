using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Timers;
using Microsoft.VisualBasic;

namespace ProgPrincipal
{
    [Serializable]
    public class Objeto
    {
        public Punto centro;
        public Dictionary<string, Parte> partes;

        public Objeto()
        {
            centro = new Punto();
            partes = new Dictionary<string, Parte>();
        }

        public Objeto(Dictionary<string, Parte> parte, Punto np)
        {
            partes = parte;
            centro = np;
        }

        public Objeto(Punto nc)
        {
            centro = nc;
            partes = new Dictionary<string, Parte>();
        }

        public void agregar(string clave, Parte p)
        {
            p.Centro.x = centro.x + p.Centro.x;
            p.Centro.y = centro.y + p.Centro.y;
            p.Centro.z = centro.z + p.Centro.z;
            partes.Add(clave, p);
        }

        public void remover(string clave)
        {
            partes.Remove(clave);
        }

        public Parte obtenerparte(string clave)
        {
            return partes[clave];
        }

        public void Escalar(float factor)
        {   
            foreach (Parte valor in partes.Values)
            {
                valor.Escalar(factor);
            }
        }

        public void trasladar(Punto nc)
        {
            centro.x = centro.x + nc.x;
            centro.y = centro.y + nc.y;
            centro.z = centro.z + nc.z;
            foreach (Parte kvp in partes.Values)
            {
                kvp.trasladar(nc);

            }
        }

        public void Rotar(float angulox, float anguloy, float anguloz)
        {
            //foreach (Parte valor in partes.Values)
            //{
            //    valor.Rotar(angulox, anguloy, anguloz);
            //}

            // Trasladar las partes al origen (usando el centro del objeto como referencia)
            Matrix4 trasladarOrigen = Matrix4.CreateTranslation(-centro.x, -centro.y, -centro.z);

            // Crear matrices de rotación para cada eje
            Matrix4 rotacionX = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angulox));
            Matrix4 rotacionY = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(anguloy));
            Matrix4 rotacionZ = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(anguloz));

            // Combinación de las rotaciones: Z * Y * X
            Matrix4 rotacionTotal = rotacionZ * rotacionY * rotacionX;

            // Trasladar las partes de vuelta a su posición original
            Matrix4 trasladarDeVuelta = Matrix4.CreateTranslation(centro.x, centro.y, centro.z);

            foreach (Parte parte in partes.Values)
            {
                // Trasladar el centro de la parte al origen
                Vector4 parteCentro = new Vector4(parte.Centro.x, parte.Centro.y, parte.Centro.z, 1.0f);
                parteCentro = Vector4.Transform(parteCentro, trasladarOrigen);

                // Aplicar la rotación
                parteCentro = Vector4.Transform(parteCentro, rotacionTotal);

                // Trasladar el centro de vuelta a su posición original
                parteCentro = Vector4.Transform(parteCentro, trasladarDeVuelta);

                // Actualizar el centro de la parte
                parte.Centro.x = parteCentro.X;
                parte.Centro.y = parteCentro.Y;
                parte.Centro.z = parteCentro.Z;

                // Rotar los polígonos dentro de la parte
                parte.Rotar(angulox, anguloy, anguloz);
            }
        }

        public void Dibujar()
        {
            foreach (Parte valor in partes.Values)
            {
                valor.Dibujar();
            }
        }
    }
}
