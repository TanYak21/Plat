﻿using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
//using OpenTK.Mathematics;
//using OpenTK.Windowing.Common;
//using OpenTK.Windowing.GraphicsLibraryFramework;
//using OpenTK.Windowing.Desktop;

using PlatonBodies;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;
using OpenTK.Input;
using OpenTK;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Windowing.Desktop;
using OpenTK.Platform;

namespace LearnOpenTK
{
    public class Window : OpenTK.GameWindow
    {
        PlatonBodies.PlatonBodies pb = new PlatonBodies.PlatonBodies();

        private readonly Vector3 _lightPos = new Vector3(1.2f, 1.0f, 2.0f);

        private int _vertexBufferObject; // piramide

        private int _vertexBufferObject2; // octaedr

        private int _vertexBufferObject3; // cube

        private int _vertexBufferObject4; // dodecahedron

        private int _vertexBufferObject5; // icosahedron

        private int _vaoModel;

        private int _vaoModel2;

        private int _vaoModel3;

        private int _vaoModel4;

        private int _vaoModel5;

        private int _vaoLamp;

        private Shader _lampShader;

        private Shader _lightingShader;

        private Camera _camera;

        private bool _firstMove = true;

        private Vector2 _lastPos;

        private double _time;

        private Matrix4 _view;

        private Matrix4 _projection;

        bool forward = true;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            //: base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //pb.findNormal();

            //pb.findIcosahedron();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, pb.piramide_vertices.Length * sizeof(float), pb.piramide_vertices, BufferUsageHint.StaticDraw);

            _vertexBufferObject2 = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject2);
            GL.BufferData(BufferTarget.ArrayBuffer, pb.octaedr_vertices.Length * sizeof(float), pb.octaedr_vertices, BufferUsageHint.StaticDraw);

            _vertexBufferObject3 = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject3);
            GL.BufferData(BufferTarget.ArrayBuffer, pb.cube_vertices.Length * sizeof(float), pb.cube_vertices, BufferUsageHint.StaticDraw);

            _vertexBufferObject4 = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject4);
            GL.BufferData(BufferTarget.ArrayBuffer, pb.dodecahedron_vertices_normals.Length * sizeof(float), pb.dodecahedron_vertices_normals, BufferUsageHint.StaticDraw);

            _vertexBufferObject5 = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject5);
            GL.BufferData(BufferTarget.ArrayBuffer, pb.icosahedron_vertices_normals.Length * sizeof(float), pb.icosahedron_vertices_normals, BufferUsageHint.StaticDraw);

            _lightingShader = new Shader("../../../Shader/shader.vert", "../../../Shader/lighting.frag");
            _lampShader = new Shader("../../../Shader/shader.vert", "../../../Shader/shader.frag");

            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
                _vaoModel = GL.GenVertexArray();
                GL.BindVertexArray(_vaoModel);

                var positionLocation = _lightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);

                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);


                var normalLocation = _lightingShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation);
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            }

            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject2);
                _vaoModel2 = GL.GenVertexArray();
                GL.BindVertexArray(_vaoModel2);

                var positionLocation = _lightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);

                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);


                var normalLocation = _lightingShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation);
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            }

            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject3);
                _vaoModel3 = GL.GenVertexArray();
                GL.BindVertexArray(_vaoModel3);

                var positionLocation = _lightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);

                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);


                var normalLocation = _lightingShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation);
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            }

            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject4);
                _vaoModel4 = GL.GenVertexArray();
                GL.BindVertexArray(_vaoModel4);

                var positionLocation = _lightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);

                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

                var normalLocation = _lightingShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation);
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            }

            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject5);
                _vaoModel5 = GL.GenVertexArray();
                GL.BindVertexArray(_vaoModel5);

                var positionLocation = _lightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);

                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

                var normalLocation = _lightingShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation);
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            }

            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject3);
                _vaoLamp = GL.GenVertexArray();
                GL.BindVertexArray(_vaoLamp);

                var positionLocation = _lampShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            }



            _camera = new Camera(Vector3.UnitZ * 3, Size.Width / (float)Size.Height);

            //CursorState = CursorState.Grabbed;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);



            if (forward)
                _time += 8.5 * e.Time;
            if (!forward)
                _time -= 8.5 * e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(_vaoModel); // piramide

            _lightingShader.Use();
            var model = Matrix4.Identity * Matrix4.CreateTranslation((float)_time, 2.0f, 0);

            if (model.ExtractTranslation().X > 2.0f)
            {
                forward = false;
            }
            else if (model.ExtractTranslation().X < -2.0f)
            {
                forward = true;
            }


            //_lightingShader.SetMatrix4("model", Matrix4.Identity);
            _lightingShader.SetMatrix4("model", model);
            _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            _lightingShader.SetVector3("objectColor", new Vector3(1.0f, 0.5f, 0.31f));
            _lightingShader.SetVector3("lightColor", new Vector3(1.0f, 1.0f, 1.0f));
            _lightingShader.SetVector3("lightPos", _lightPos);
            _lightingShader.SetVector3("viewPos", _camera.Position);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 18);

            GL.BindVertexArray(_vaoModel2); // octaedr

            _lightingShader.Use();

            _lightingShader.SetMatrix4("model", Matrix4.Identity * Matrix4.CreateTranslation(0, 1.0f, 0));
            _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            //_lightingShader.SetVector3("objectColor", new Vector3(0.4f, 0.6f, 0.31f));
            _lightingShader.SetVector3("objectColor", new Vector3(153.0f / 255.0f, 51f / 255f, 153f / 255f)); // Можно брать значения RGB 256
            _lightingShader.SetVector3("lightColor", new Vector3(1.0f, 1.0f, 1.0f));
            _lightingShader.SetVector3("lightPos", _lightPos);
            _lightingShader.SetVector3("viewPos", _camera.Position);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 24);

            GL.BindVertexArray(_vaoModel3); // cube

            _lightingShader.Use();

            _lightingShader.SetMatrix4("model", Matrix4.Identity);
            _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            //_lightingShader.SetVector3("objectColor", new Vector3(0.4f, 0.6f, 0.31f));
            _lightingShader.SetVector3("objectColor", new Vector3(51f / 255f, 153f / 255f, 255f / 255f));
            _lightingShader.SetVector3("lightColor", new Vector3(1.0f, 1.0f, 1.0f));
            _lightingShader.SetVector3("lightPos", _lightPos);
            _lightingShader.SetVector3("viewPos", _camera.Position);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            GL.BindVertexArray(_vaoModel4); // dodecahedron

            _lightingShader.Use();

            _lightingShader.SetMatrix4("model", Matrix4.Identity * Matrix4.CreateTranslation(-3.0f, -2.0f, 2.0f));
            _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            _lightingShader.SetVector3("objectColor", new Vector3(0.4f, 0.6f, 0.31f));
            _lightingShader.SetVector3("lightColor", new Vector3(1.0f, 1.0f, 1.0f));
            _lightingShader.SetVector3("lightPos", _lightPos);
            _lightingShader.SetVector3("viewPos", _camera.Position);

            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 108);

            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            GL.BindVertexArray(_vaoModel5); // isocahedron

            _lightingShader.Use();

            _lightingShader.SetMatrix4("model", Matrix4.Identity * Matrix4.CreateTranslation(3.0f, -2.0f, 2.0f));
            _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            //_lightingShader.SetVector3("objectColor", new Vector3(0.4f, 0.6f, 0.31f));
            _lightingShader.SetVector3("objectColor", new Vector3(173f / 255f, 173f / 255f, 173f / 255f));
            _lightingShader.SetVector3("lightColor", new Vector3(1.0f, 1.0f, 1.0f));
            _lightingShader.SetVector3("lightPos", _lightPos);
            _lightingShader.SetVector3("viewPos", _camera.Position);

            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 60);

            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            GL.BindVertexArray(_vaoLamp);

            _lampShader.Use();

            Matrix4 lampMatrix = Matrix4.CreateScale(0.2f);
            lampMatrix = lampMatrix * Matrix4.CreateTranslation(_lightPos);

            _lampShader.SetMatrix4("model", lampMatrix);
            _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (!Focused)
            {
                return;
            }

            //var input = KeyboardState; old

            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown((Key)Keys.Escape))
            {
                Close();
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown((Key)Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
            }
            if (input.IsKeyDown((Key)Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
            }
            if (input.IsKeyDown((Key)Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
            }
            if (input.IsKeyDown((Key)Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
            }
            if (input.IsKeyDown((Key)Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
            }
            if (input.IsKeyDown((Key)Keys.LShiftKey))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
            }

            //var mouse = MouseState;
            MouseState mouse = Mouse.GetState();

            if (_firstMove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }
        }

        //protected override void OnMouseWheel(MouseWheelEventArgs e)
        //{
        //    base.OnMouseWheel(e);

        //    _camera.Fov -= e.OffsetY;
        //}

        //protected override void OnResize(ResizeEventArgs e)
        //{
        //    base.OnResize(e);

        //    GL.Viewport(0, 0, Size.X, Size.Y);
        //    _camera.AspectRatio = Size.X / (float)Size.Y;
        //}
    }
}