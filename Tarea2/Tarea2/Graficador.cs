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

namespace Tarea2
{
    public class Graficador : GameWindow
    {
        private float anguloX;
        private float anguloY;
        private float velocidadRotacion = 1.0f;
        DibujarFormas cuboV;
        DibujarFormas cuboH;

        public Graficador()
         : base(DisplayDevice.Default.Width/2, DisplayDevice.Default.Height/2, GraphicsMode.Default, "T relativa", GameWindowFlags.Default)
        {
            float[] nuevoPunto = { 0.4f, 0.4f, 0.0f };
            Relativo inicio = new Relativo(nuevoPunto, -0.2f, -0.1f, 0.0f);
            cuboV = new DibujarFormas();
            cuboV.cuadradoCubico(inicio, 0.4f, 0.2f, 0.2f);

            float[] nuevoPuntoH = { 0.4f, 0.1f, 0.0f };
            Relativo inicioH = new Relativo(nuevoPuntoH, -0.1f, -0.2f, 0.0f);
            cuboH = new DibujarFormas();
            cuboH.cuadradoCubico(inicioH, 0.2f, 0.4f, 0.2f);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.8f, 0.5f, 0.6f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)Size.Width / Size.Height, 0.1f, 100.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projectionMatrix);

            // Configuración de la matriz de vista
            Matrix4 viewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, -5.0f);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref viewMatrix);

            // Rotaciones
            Matrix4 modelMatrix = Matrix4.CreateRotationX(this.anguloX) * Matrix4.CreateRotationY(this.anguloY);
            GL.MultMatrix(ref modelMatrix);

            //DrawCube();
            cuboV.DibujarCuadrado();
            cuboH.DibujarCuadrado();



            Context.SwapBuffers();
        }

        //private void DrawCube()
        //{
        //    GL.Begin(PrimitiveType.Polygon);
        //    GL.Color3(Color.FromArgb(255,0,0)); // Rojo
        //    GL.Vertex3(-1.0f, -1.0f, 1.0f);
        //    GL.Vertex3(1.0f, -1.0f, 1.0f);
        //    GL.Vertex3(1.0f, 1.0f, 1.0f);
        //    GL.Vertex3(-1.0f, 1.0f, 1.0f);

        //    GL.End();
        //}

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard.GetState().IsKeyDown(Key.Escape))
            {
                Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Key.S))
                anguloX += velocidadRotacion * (float)e.Time;
            if (Keyboard.GetState().IsKeyDown(Key.W))
                anguloX -= velocidadRotacion * (float)e.Time;
            if (Keyboard.GetState().IsKeyDown(Key.A))
                anguloY -= velocidadRotacion * (float)e.Time;
            if (Keyboard.GetState().IsKeyDown(Key.D))
                anguloY += velocidadRotacion * (float)e.Time;

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(ClientRectangle);

        }

    }
}
