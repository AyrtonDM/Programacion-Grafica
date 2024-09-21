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
using ProgPrincipal;


public class Graficador : GameWindow
{
    //GLControl 

    private float velocidadRotacion = 0.5f;

    private float velocidadMovimiento = 2.0f;
    private Vector3 posicionCamara = new Vector3(0.0f, 0.0f, 5.0f); // Posición inicial de la cámara
    private Vector3 direccionCamara = -Vector3.UnitZ; // Dirección inicial (hacia adelante)
    private Vector3 up = Vector3.UnitY; // Vector arriba

    private PlanoCartesiano plano;
    private Escenario escenario;
    private Serializa archivar;
    private Objeto T;
    private Parte cuboh;
    private Parte cubov;
    private Poligono carafrontal;
    public void CrearObjeto()
    {
        Punto centroh = new Punto(0.0f, 0.2f, 0.0f);
        Punto centrov = new Punto(0.0f, -0.1f, 0.0f);

        Punto p1h = new Punto(-0.2f, -0.1f, 0.1f);
        Punto p2h = new Punto(0.2f, -0.1f, 0.1f);
        Punto p3h = new Punto(0.2f, 0.1f, 0.1f);
        Punto p4h = new Punto(-0.2f, 0.1f, 0.1f);

        Punto p5h = new Punto(-0.2f, -0.1f, -0.1f);
        Punto p6h = new Punto(0.2f, -0.1f, -0.1f);
        Punto p7h = new Punto(0.2f, 0.1f, -0.1f);
        Punto p8h = new Punto(-0.2f, 0.1f, -0.1f);

        Punto p1v = new Punto(-0.1f, -0.2f, 0.1f);
        Punto p2v = new Punto(0.1f, -0.2f, 0.1f);
        Punto p3v = new Punto(0.1f, 0.2f, 0.1f);
        Punto p4v = new Punto(-0.1f, 0.2f, 0.1f);

        Punto p5v = new Punto(-0.1f, -0.2f, -0.1f);
        Punto p6v = new Punto(0.1f, -0.2f, -0.1f);
        Punto p7v = new Punto(0.1f, 0.2f, -0.1f);
        Punto p8v = new Punto(-0.1f, 0.2f, -0.1f);

        carafrontal = new Poligono();
        carafrontal.agregar("p1h",p1h);
        carafrontal.agregar("p2h",p2h);
        carafrontal.agregar("p3h", p3h);
        carafrontal.agregar("p4h", p4h);

        Poligono caratrasera = new Poligono();
        caratrasera.agregar("p5h", p5h);
        caratrasera.agregar("p6h", p6h);
        caratrasera.agregar("p7h", p7h);
        caratrasera.agregar("p8h", p8h);

        Poligono caraizquierda = new Poligono();
        caraizquierda.agregar("p9h", p5h);
        caraizquierda.agregar("p10h", p1h);
        caraizquierda.agregar("p11h", p4h);
        caraizquierda.agregar("p12h", p8h);

        Poligono caraderecha = new Poligono();
        caraderecha.agregar("p13h", p2h);
        caraderecha.agregar("p14h", p6h);
        caraderecha.agregar("p15h", p7h);
        caraderecha.agregar("p16h", p3h);

        Poligono carasuperior = new Poligono();
        carasuperior.agregar("p17h", p4h);
        carasuperior.agregar("p18h", p3h);
        carasuperior.agregar("p19h", p7h);
        carasuperior.agregar("p20h", p8h);

        Poligono carainferior = new Poligono();
        carainferior.agregar("p21h", p5h);
        carainferior.agregar("p22h", p6h);
        carainferior.agregar("p23h", p2h);
        carainferior.agregar("p24h", p1h);

        cuboh = new Parte(centroh);
        cuboh.agregar("caradel", carafrontal);
        cuboh.agregar("caratra", caratrasera);
        cuboh.agregar("caraizq", caraizquierda);
        cuboh.agregar("carader", caraderecha);
        cuboh.agregar("carasup", carasuperior);
        cuboh.agregar("carainf", carainferior);

        Poligono carafrontalv = new Poligono();
        carafrontalv.agregar("p1v", p1v);
        carafrontalv.agregar("p2v", p2v);
        carafrontalv.agregar("p3v", p3v);
        carafrontalv.agregar("p4v", p4v);

        Poligono caratraserav = new Poligono();
        caratraserav.agregar("p5v", p5v);
        caratraserav.agregar("p6v", p6v);
        caratraserav.agregar("p7v", p7v);
        caratraserav.agregar("p8v", p8v);

        Poligono caraizquierdav = new Poligono();
        caraizquierdav.agregar("p9v", p5v);
        caraizquierdav.agregar("p10v", p1v);
        caraizquierdav.agregar("p11v", p4v);
        caraizquierdav.agregar("p12v", p8v);

        Poligono caraderechav = new Poligono();
        caraderechav.agregar("p13v", p2v);
        caraderechav.agregar("p14v", p6v);
        caraderechav.agregar("p15v", p7v);
        caraderechav.agregar("p16v", p3v);

        Poligono carasuperiorv = new Poligono();
        carasuperiorv.agregar("p17v", p4v);
        carasuperiorv.agregar("p18v", p3v);
        carasuperiorv.agregar("p19v", p7v);
        carasuperiorv.agregar("p20v", p8v);

        Poligono carainferiorv = new Poligono();
        carainferiorv.agregar("p21v", p5v);
        carainferiorv.agregar("p22v", p6v);
        carainferiorv.agregar("p23v", p2v);
        carainferiorv.agregar("p24v", p1v);

        cubov = new Parte(centrov);
        cubov.agregar("caradel", carafrontalv);
        cubov.agregar("caratra", caratraserav);
        cubov.agregar("caraizq", caraizquierdav);
        cubov.agregar("carader", caraderechav);
        cubov.agregar("carasup", carasuperiorv);
        cubov.agregar("carainf", carainferiorv);

        Punto centroob = new Punto(0.5f, 0.5f, 0.0f);
        T = new Objeto();
        T.agregar("cuboh", cuboh);
        T.agregar("cubov", cubov);


        //Objeto T = archivar.deserializarobjeto("T");
        escenario.agregar("T3D", T);
        escenario.Escalar(2);
        escenario.Trasladar(centroob);
        escenario.Rotar(90f, 0f, 0f);
        //escenario.trasladar(new Punto(0.1f, 0.1f, 0.0f));
    }
    public Graficador()
     : base(DisplayDevice.Default.Width, DisplayDevice.Default.Height, GraphicsMode.Default, "T relativa", GameWindowFlags.Default)
    {
        plano = new PlanoCartesiano(0.1f, 10);
        escenario = new Escenario();
        archivar = new Serializa();
        CrearObjeto();
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

        Matrix4 viewMatrix = Matrix4.LookAt(posicionCamara, posicionCamara + direccionCamara, up);
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadMatrix(ref viewMatrix);

        //DrawCube();
        plano.Dibujar();
        //escenario.Dibujar();
        //escenario.Rotar(1f, 0f, 0f);
        T.Dibujar();
        //carafrontal.Rotar(1f, 0f, 0f);
        //carafrontal.Dibujar();
        //cuboh.Dibujar();
        //cuboh.Rotar(1f, 0f, 0f);

        Context.SwapBuffers();
    }

    private void DrawCube()
    {
        //GL.Scale(0.4f, 0.2f, 0.0f);
        //GL.Translate(0.0f,0.4f,0.0f);
        //GL.Rotate(-90.0f,4.0f,2.0f,0.0f);
        GL.LineWidth(3.0f);
        GL.Begin(PrimitiveType.LineLoop);
        GL.Color3(Color.FromArgb(0, 0, 0));
        GL.Vertex3(-0.1f, -0.1f, 0.1f);
        GL.Vertex3(0.1f, -0.1f, 0.1f);
        GL.Vertex3(0.1f, 0.1f, 0.1f);
        GL.Vertex3(-0.1f, 0.1f, 0.1f);
        GL.End();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        if (Keyboard.GetState().IsKeyDown(Key.Escape))
        {
            Exit();
        }
        if (Keyboard.GetState().IsKeyDown(Key.Right))
            direccionCamara = Vector3.Transform(direccionCamara, Quaternion.FromAxisAngle(up, MathHelper.DegreesToRadians(-velocidadRotacion)));
        if (Keyboard.GetState().IsKeyDown(Key.Left))
            direccionCamara = Vector3.Transform(direccionCamara, Quaternion.FromAxisAngle(up, MathHelper.DegreesToRadians(velocidadRotacion)));
        if (Keyboard.GetState().IsKeyDown(Key.Down))
        {
            Vector3 right = Vector3.Cross(direccionCamara, up);
            direccionCamara = Vector3.Transform(direccionCamara, Quaternion.FromAxisAngle(right, MathHelper.DegreesToRadians(-velocidadRotacion)));
        }
        if (Keyboard.GetState().IsKeyDown(Key.Up))
        {
            Vector3 right = Vector3.Cross(direccionCamara, up);
            direccionCamara = Vector3.Transform(direccionCamara, Quaternion.FromAxisAngle(right, MathHelper.DegreesToRadians(velocidadRotacion)));
        }

        // Movimiento de la cámara
        if (Keyboard.GetState().IsKeyDown(Key.W))
            posicionCamara += direccionCamara * velocidadMovimiento * (float)e.Time;
        if (Keyboard.GetState().IsKeyDown(Key.S))
            posicionCamara -= direccionCamara * velocidadMovimiento * (float)e.Time;
        if (Keyboard.GetState().IsKeyDown(Key.A))
        {
            Vector3 left = Vector3.Cross(up, direccionCamara);
            posicionCamara += left * velocidadMovimiento * (float)e.Time;
        }
        if (Keyboard.GetState().IsKeyDown(Key.D))
        {
            Vector3 right = Vector3.Cross(direccionCamara, up);
            posicionCamara += right * velocidadMovimiento * (float)e.Time;
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(ClientRectangle);
    }
}