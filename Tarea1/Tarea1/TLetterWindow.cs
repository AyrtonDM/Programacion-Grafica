using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Tarea1
{
    public class TLetterWindow : GameWindow
    {
        private int _vertexArrayObject;
        private int _vertexBufferObject;
        private int _elementBufferObject;
        private int _shaderProgram;

        // Define vertices para estilizar la "T" 3D
        private readonly float[] _vertices =
        {
        // Cara frontal de la T (parte horizontal de arriba)
        -0.5f,  0.8f,  0.5f, 1.0f, 0.752f, 0.796f, // Superior izquierda
         0.5f,  0.8f,  0.5f, 1.0f, 0.752f, 0.796f, // Superior derecha
         0.5f,  0.4f,  0.5f, 1.0f, 0.752f, 0.796f, // fondo derecha
        -0.5f,  0.4f,  0.5f, 1.0f, 0.752f, 0.796f, // fondo izquiera

        // Cara trasera de la T (parte horizontal de arriba)
        -0.5f,  0.8f, 0.1f, 0.8f, 0.5f, 0.6f, 
         0.5f,  0.8f, 0.1f, 0.8f, 0.5f, 0.6f, 
         0.5f,  0.4f, 0.1f, 0.8f, 0.5f, 0.6f, 
        -0.5f,  0.4f, 0.1f, 0.8f, 0.5f, 0.6f, 

        // Cara frontal de la T (parte vertical)
        -0.2f,  0.4f,  0.5f, 1.0f, 0.752f, 0.796f, 
         0.2f,  0.4f,  0.5f, 1.0f, 0.752f, 0.796f, 
         0.2f, -0.6f,  0.5f, 1.0f, 0.752f, 0.796f, 
        -0.2f, -0.6f,  0.5f, 1.0f, 0.752f, 0.796f, 
        
        // Cara trasera de la T (parte vertical)
        -0.2f,  0.4f, 0.1f, 0.8f, 0.5f, 0.6f,
         0.2f,  0.4f, 0.1f, 0.8f, 0.5f, 0.6f, 
         0.2f, -0.6f, 0.1f, 0.8f, 0.5f, 0.6f, 
        -0.2f, -0.6f, 0.1f, 0.8f, 0.5f, 0.6f,
        
        

        // Techo de la T
        //-0.5f,  0.8f, 0.1f, 1.0f, 1.0f, 0.0f,
        // 0.5f,  0.8f, 0.1f, 1.0f, 1.0f, 0.0f,
        // 0.5f,  0.8f,  0.5f, 0.4f, 0.0f, 0.0f,
        //-0.5f,  0.8f,  0.5f, 0.5f, 0.0f, 0.0f,

        ////Suelo de la T
        //-0.2f, -0.6f, 0.1f, 1.0f, 1.0f, 0.0f,
        // 0.2f, -0.6f, 0.1f, 1.0f, 1.0f, 0.0f,
        // 0.2f, -0.6f,  0.5f, 0.4f, 0.0f, 0.0f,
        //-0.2f, -0.6f,  0.5f, 0.5f, 0.0f, 0.0f,

        
    };

        private readonly uint[] _indices = //Orden y forma de conectar las esquinas con triangulos
        {
        // Parte horizaontales
        0, 1, 2, 2, 3, 0, 
        4, 5, 6, 6, 7, 4, 

        // Parte verticales
        8, 9, 10, 10, 11, 8, 
        12, 13, 14, 14, 15, 12, 

        0, 4, 5, 5, 1,0,
        11, 10, 14, 14, 15, 11,

        4, 0, 3, 3, 7, 4,
        1, 5, 2, 2, 6, 5,

        12, 8, 11, 11, 15, 12,
        9, 13, 14, 14, 10, 9,

        9, 2, 6, 6, 13, 9,
        3, 8, 12, 12, 7, 3, 

        //Techo y suelo
        //16, 17, 18, 18, 19, 16,
        //20, 21, 22, 22, 23, 20,

 
         
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

        public TLetterWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            // Create and compile shaders
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, _vertexShaderSource);
            GL.CompileShader(vertexShader);
            CheckShaderCompilation(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, _fragmentShaderSource);
            GL.CompileShader(fragmentShader);
            CheckShaderCompilation(fragmentShader);

            // Create shader program and link shaders
            _shaderProgram = GL.CreateProgram();
            GL.AttachShader(_shaderProgram, vertexShader);
            GL.AttachShader(_shaderProgram, fragmentShader);
            GL.LinkProgram(_shaderProgram);
            GL.DetachShader(_shaderProgram, vertexShader);
            GL.DetachShader(_shaderProgram, fragmentShader);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            // Set up Vertex Array Object and Vertex Buffer Object
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

            // Set up element buffer
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

            // Transformation matrices
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

            if (KeyboardState.IsKeyDown(Keys.W))
                _angleX += _rotationSpeed * (float)args.Time;
            if (KeyboardState.IsKeyDown(Keys.S))
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
