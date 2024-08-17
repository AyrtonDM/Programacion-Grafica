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
    public class LineSquareWindow : GameWindow
    {
        private int _vertexArrayObject;
        private int _vertexBufferObject;
        private int _elementBufferObject;
        private int _shaderProgram;

        private float _angleX = 0.0f;
        private float _angleY = 0.0f;
        private float _rotationSpeed = 1.5f;

        // Vertices for a cube
        private readonly float[] _vertices =
        {
        // Front face
        -0.5f, -0.5f,  0.5f,  // Bottom-left
         0.5f, -0.5f,  0.5f,  // Bottom-right
         0.5f,  0.5f,  0.5f,  // Top-right
        -0.5f,  0.5f,  0.5f,  // Top-left

        // Back face
        -0.5f, -0.5f, -0.5f,  // Bottom-left
         0.5f, -0.5f, -0.5f,  // Bottom-right
         0.5f,  0.5f, -0.5f,  // Top-right
        -0.5f,  0.5f, -0.5f,  // Top-left
    };

        // Indices for drawing lines
        private readonly uint[] _indices =
        {
        // Front face
        0, 1,
        1, 2,
        2, 3,
        3, 0,

        // Back face
        4, 5,
        5, 6,
        6, 7,
        7, 4,

        // Connecting edges
        0, 4,
        1, 5,
        2, 6,
        3, 7
    };

        private readonly string _vertexShaderSource = @"
        #version 330 core
        layout(location = 0) in vec3 aPosition;

        uniform mat4 model;
        uniform mat4 view;
        uniform mat4 projection;

        void main()
        {
            gl_Position = projection * view * model * vec4(aPosition, 1.0);
        }
    ";

        private readonly string _fragmentShaderSource = @"
        #version 330 core
        out vec4 FragColor;

        void main()
        {
            FragColor = vec4(0.8f, 0.5f, 0.6f, 1.0f); // Darker pink color
        }
    ";

        private Matrix4 _modelMatrix;
        private Matrix4 _viewMatrix;
        private Matrix4 _projectionMatrix;

        public LineSquareWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            // Compile shaders
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

            // Create and bind VAO
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            // Create and bind VBO
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            // Set vertex attributes
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Create and bind EBO
            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

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

            // Apply rotation
            _modelMatrix = Matrix4.CreateRotationX(_angleX) * Matrix4.CreateRotationY(_angleY);
            _viewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Size.X / (float)Size.Y, 0.1f, 100.0f);

            int modelLocation = GL.GetUniformLocation(_shaderProgram, "model");
            int viewLocation = GL.GetUniformLocation(_shaderProgram, "view");
            int projectionLocation = GL.GetUniformLocation(_shaderProgram, "projection");

            GL.UniformMatrix4(modelLocation, false, ref _modelMatrix);
            GL.UniformMatrix4(viewLocation, false, ref _viewMatrix);
            GL.UniformMatrix4(projectionLocation, false, ref _projectionMatrix);

            // Draw the cube as lines
            GL.DrawElements(PrimitiveType.Lines, _indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Rotate with W, S, A, D keys
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
