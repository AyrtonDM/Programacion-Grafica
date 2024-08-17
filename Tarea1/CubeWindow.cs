using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Tarea1
{

    public class CubeWindow : GameWindow
    {
        private int _vertexArrayObject;
        private int _vertexBufferObject;
        private int _elementBufferObject;
        private int _shaderProgram;

        private readonly float[] _vertices =
        {
        // Posiciones            // Colores
        // Frente
        -0.5f, -0.5f,  0.5f, 1.0f, 0.0f, 0.0f, 
         0.5f, -0.5f,  0.5f, 1.0f, 0.0f, 0.0f, 
         0.5f,  0.5f,  0.5f, 1.0f, 0.0f, 0.0f, 
        -0.5f,  0.5f,  0.5f, 1.0f, 0.0f, 0.0f, 

        // Atrás
        -0.5f, -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 
         0.5f, -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 
         0.5f,  0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 
        -0.5f,  0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 

        // Izquierda
        -0.5f, -0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 
        -0.5f, -0.5f,  0.5f, 0.0f, 0.0f, 1.0f, 
        -0.5f,  0.5f,  0.5f, 0.0f, 0.0f, 1.0f, 
        -0.5f,  0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 

        // Derecha
         0.5f, -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 
         0.5f, -0.5f,  0.5f, 0.0f, 0.0f, 0.0f, 
         0.5f,  0.5f,  0.5f, 0.0f, 0.0f, 0.0f, 
         0.5f,  0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 

        // Arriba
        -0.5f,  0.5f, -0.5f, 1.0f, 1.0f, 0.0f, 
         0.5f,  0.5f, -0.5f, 1.0f, 1.0f, 0.0f, 
         0.5f,  0.5f,  0.5f, 1.0f, 1.0f, 0.0f, 
        -0.5f,  0.5f,  0.5f, 1.0f, 1.0f, 0.0f, 

        // Abajo
        -0.5f, -0.5f, -0.5f, 0.0f, 1.0f, 1.0f, 
         0.5f, -0.5f, -0.5f, 0.0f, 1.0f, 1.0f, 
         0.5f, -0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 
        -0.5f, -0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 
    };

        private readonly uint[] _indices =
        {
        0, 1, 2, 2, 3, 0, // Frente
        4, 5, 6, 6, 7, 4, // Atrás
        8, 9, 10, 10, 11, 8, // Izquierda
        12, 13, 14, 14, 15, 12, // Derecha
        16, 17, 18, 18, 19, 16, // Arriba
        20, 21, 22, 22, 23, 20  // Abajo
    };

        private readonly string _vertexShaderSource = @"
        #version 400
        in vec3 position;
        in vec3 color;
        out vec3 fragColor;
        uniform mat4 model;
        uniform mat4 view;
        uniform mat4 projection;
        void main()
        {
            gl_Position = projection * view * model * vec4(position, 1.0);
            fragColor = color;
        }
    ";

        private readonly string _fragmentShaderSource = @"
        #version 400
        in vec3 fragColor;
        out vec4 color;
        void main()
        {
            color = vec4(fragColor, 1.0);
        }
    ";

        private float _angleX;
        private float _angleY;
        private float _rotationSpeed = 1.0f;

        public CubeWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            // Crear y compilar shaders
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, _vertexShaderSource);
            GL.CompileShader(vertexShader);
            CheckShaderCompilation(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, _fragmentShaderSource);
            GL.CompileShader(fragmentShader);
            CheckShaderCompilation(fragmentShader);

            // Crear programa de shader y linkear shaders
            _shaderProgram = GL.CreateProgram();
            GL.AttachShader(_shaderProgram, vertexShader);
            GL.AttachShader(_shaderProgram, fragmentShader);
            GL.LinkProgram(_shaderProgram);
            GL.DetachShader(_shaderProgram, vertexShader);
            GL.DetachShader(_shaderProgram, fragmentShader);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            // Configurar Vertex Array Object y Vertex Buffer Object
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            int positionLocation = GL.GetAttribLocation(_shaderProgram, "position");
            int colorLocation = GL.GetAttribLocation(_shaderProgram, "color");

            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(positionLocation);

            GL.VertexAttribPointer(colorLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(colorLocation);

            // Configurar índice de elementos
            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        private void CheckShaderCompilation(int shader)
        {
            GL.GetShader(shader, ShaderParameter.CompileStatus, out int status);
            if (status == 0)
            {
                string infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Shader compilation failed: {infoLog}");
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(_shaderProgram);
            GL.BindVertexArray(_vertexArrayObject);

            // Matrices de transformación
            Matrix4 modelMatrix = Matrix4.CreateRotationX(_angleX) * Matrix4.CreateRotationY(_angleY);
            Matrix4 viewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, -5.0f);
            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)Size.X / Size.Y, 0.1f, 100.0f);

            int modelLocation = GL.GetUniformLocation(_shaderProgram, "model");
            int viewLocation = GL.GetUniformLocation(_shaderProgram, "view");
            int projectionLocation = GL.GetUniformLocation(_shaderProgram, "projection");

            GL.UniformMatrix4(modelLocation, false, ref modelMatrix);
            GL.UniformMatrix4(viewLocation, false, ref viewMatrix);
            GL.UniformMatrix4(projectionLocation, false, ref projectionMatrix);

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.S))
                _angleX += _rotationSpeed * (float)args.Time;
            if (KeyboardState.IsKeyDown(Keys.W))
                _angleX -= _rotationSpeed * (float)args.Time;
            if (KeyboardState.IsKeyDown(Keys.A))
                _angleY -= _rotationSpeed * (float)args.Time;
            if (KeyboardState.IsKeyDown(Keys.D))
                _angleY += _rotationSpeed * (float)args.Time;
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            GL.DeleteVertexArray(_vertexArrayObject);
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteBuffer(_elementBufferObject);
            GL.DeleteProgram(_shaderProgram);
        }
    }

}
