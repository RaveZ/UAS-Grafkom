using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System;
using OpenTK.Mathematics;
using LearnOpenTK.Common;

namespace LightFast
{
    class Window : GameWindow
    {
        private readonly string path = "../../../";
        //string path_sun = "../../../Assets_environment/sun.obj";
        //mesh sun = new mesh();

        freedys frs = new freedys();
        Character _chara = new Character(0);

        Character _budi = new Character(1);

        private int selection = 0;

        Lights lights = new Lights();
        List<List<mesh>> all_lights = new List<List<mesh>>();
        List<mesh> objectList = new List<mesh>();
        List<Asset3d> directLightList = new List<Asset3d>();
        List<Asset3d> pointLightList = new List<Asset3d>();
        List<Asset3d> spotLightList = new List<Asset3d>();


        Asset3d chara = new Asset3d("shader.vert", "shader.frag", new Vector3(1, 1, 1), 1);
        Asset3d budi = new Asset3d("shader.vert", "shader.frag", new Vector3(1, 1, 1), 1);


        Asset3dInstanced instancedCubes;

        //Inisialisasi Environment
        environment _environment = new environment();

        private Vector3 _lightPos = new Vector3(0f, 6.0f, -10.0f);

        //Asset3d light;

        //private Cubemap cubemap;

        Camera camera; //camera yang aktif
        Camera camera2;
        Camera camera1;
        Camera fps;
        ThirdPersonCamera tps = new ThirdPersonCamera();
        ThirdPersonCamera tpsBudi = new ThirdPersonCamera();

        private bool isThirdPerson = true;

        // CCTV
        private int checkers = 0;
        float xInput = 0;
        float yInput = 0;
        float yTotal = 0;
        public float xTotal = 0;
        float topAngle = 5;
        float BottomAngle = -5;
        float leftAngle = -30;
        float rightAngle = 30;
        Camera cctv1;
        Camera cctv2;
        Camera cctv3;
        Camera cctv4;
        Camera cctvFreddy;

        private bool firstMove = true;

        private int renderSetting = 1;

        private float cameraSpeed = 12.0f;
        private float sensitivity = 0.2f;
        private Vector2 lastPos;

        private float totalTime;

        private List<float> moveOffset = new List<float>();




        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();

            camera1 = new Camera(new Vector3(0, -1, 3), Size.X / (float)Size.Y);
            camera = camera1;
            fps = new Camera(new Vector3(0, -1, 3), Size.X / (float)Size.Y);
            camera2 = new Camera(new Vector3(0, -1, 3), Size.X / (float)Size.Y);

            cctv1 = new Camera(new Vector3(25, 20, -30), Size.X / (float)Size.Y);
            cctv1.Pitch -= 30f;
            cctv1.Yaw += 90f;

            cctv2 = new Camera(new Vector3(15, 20, 60), Size.X / (float)Size.Y);
            cctv2.Pitch -= 30f;
            cctv3 = new Camera(new Vector3(-100, 20, 45), Size.X / (float)Size.Y);
            cctv3.Pitch -= 30f;
            cctv3.Yaw += 40f;
            cctv4 = new Camera(new Vector3(-100, 20, -8), Size.X / (float)Size.Y);
            cctv4.Pitch -= 30f;
            cctv4.Yaw -= 260f;
            cctvFreddy = new Camera(new Vector3(42f, 5.8f, 22f), Size.X / (float)Size.Y);
            cctvFreddy.Yaw += 270f;

            GL.ClearColor(0f, 0f, 0f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            //GL.Enable(EnableCap.Blend);
            //GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            var random = new Random();
            /*
                        sun.initialize(path_sun);
                        sun.set_VAO_lamp();
                        sun.set_shader_lamp();*/
            lights.load();
            all_lights = lights.out_light();
            /*foreach (var i in sun.vertices)
            {
                Console.WriteLine(i);
            }*/

            //environment
            _environment.load();
            objectList = _environment.all_mesh();
            _environment.move(new Vector3(0f, 0f, 0f), objectList);
            //chara.createCuboid(0, 0, -13, 2f, false, false);

            frs.load();
            frs.move(new Vector3(15f, 5.8f, -42.5f));
            frs.rotate(-90.0f);

            _chara.load();
            _chara.move(new Vector3(0, -4, -6), objectList);
            _chara.rotate(_chara.chara.objectCenter, _chara.chara._euler[1], 180f);

            chara.load();

            _budi.load();
            _budi.move(new Vector3(-10, -2, -6), objectList);
            //_budi.move(new Vector3(-30, 10, -6), objectList);
            _budi.rotate(_budi.chara.objectCenter, _budi.chara._euler[1], 180f);

            budi.load();
            List<mesh> characterList = new List<mesh>();
            characterList.Add(_chara.chara);
            characterList.Add(_budi.chara);
            all_lights.Add(characterList); //index 4

            CursorGrabbed = true;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            float time = (float)args.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (checkers == 0)
            {
                lights.render(camera);
                //environment
                _environment.Render_object(camera, all_lights);


                frs.Render_object(camera, all_lights);

                if (selection == 0)
                {
                    _chara.Render_object(camera, all_lights);
                    chara.render(renderSetting, camera.GetViewMatrix(), camera.GetProjectionMatrix(), camera.Position, directLightList, pointLightList, spotLightList);
                }
                else if (selection == 1)
                {
                    _budi.Render_object(camera, all_lights);
                    budi.render(renderSetting, camera.GetViewMatrix(), camera.GetProjectionMatrix(), camera.Position, directLightList, pointLightList, spotLightList);
                }


            }

            else if (checkers == 1)
            {
                lights.render(cctv1);
                //environment
                _environment.Render_object(cctv1, all_lights);


                frs.Render_object(cctv1, all_lights);

                if (selection == 0)
                {
                    _chara.Render_object(cctv1, all_lights);
                    chara.render(renderSetting, cctv1.GetViewMatrix(), cctv1.GetProjectionMatrix(), cctv1.Position, directLightList, pointLightList, spotLightList);
                }
                else if (selection == 1)
                {
                    _budi.Render_object(cctv1, all_lights);
                    budi.render(renderSetting, cctv1.GetViewMatrix(), cctv1.GetProjectionMatrix(), cctv1.Position, directLightList, pointLightList, spotLightList);
                }

            }

            else if (checkers == 2)
            {
                lights.render(cctv2);
                //environment
                _environment.Render_object(cctv2, all_lights);


                frs.Render_object(cctv2, all_lights);

                if (selection == 0)
                {
                    _chara.Render_object(cctv2, all_lights);
                    chara.render(renderSetting, cctv2.GetViewMatrix(), cctv2.GetProjectionMatrix(), cctv2.Position, directLightList, pointLightList, spotLightList);
                }
                else if (selection == 1)
                {
                    _budi.Render_object(cctv2, all_lights);
                    budi.render(renderSetting, cctv2.GetViewMatrix(), cctv2.GetProjectionMatrix(), cctv2.Position, directLightList, pointLightList, spotLightList);
                }
            }

            else if (checkers == 3)
            {
                lights.render(cctv3);
                //environment
                _environment.Render_object(cctv3, all_lights);


                frs.Render_object(cctv3, all_lights);

                if (selection == 0)
                {
                    _chara.Render_object(cctv3, all_lights);
                    chara.render(renderSetting, cctv3.GetViewMatrix(), cctv3.GetProjectionMatrix(), cctv3.Position, directLightList, pointLightList, spotLightList);
                }
                else if (selection == 1)
                {
                    _budi.Render_object(cctv3, all_lights);
                    budi.render(renderSetting, cctv3.GetViewMatrix(), cctv3.GetProjectionMatrix(), cctv3.Position, directLightList, pointLightList, spotLightList);
                }
            }

            else if (checkers == 4)
            {
                lights.render(cctv4);
                //environment
                _environment.Render_object(cctv4, all_lights);


                frs.Render_object(cctv4, all_lights);

                if (selection == 0)
                {
                    _chara.Render_object(cctv4, all_lights);
                    chara.render(renderSetting, cctv4.GetViewMatrix(), cctv4.GetProjectionMatrix(), cctv4.Position, directLightList, pointLightList, spotLightList);
                }
                else if (selection == 1)
                {
                    _budi.Render_object(cctv4, all_lights);
                    budi.render(renderSetting, cctv4.GetViewMatrix(), cctv4.GetProjectionMatrix(), cctv4.Position, directLightList, pointLightList, spotLightList);
                }
            }

            else if (checkers == 5)
            {
                lights.render(cctvFreddy);
                //environment
                _environment.Render_object(cctvFreddy, all_lights);


                frs.Render_object(cctvFreddy, all_lights);

                if (selection == 0)
                {
                    _chara.Render_object(cctvFreddy, all_lights);
                    chara.render(renderSetting, cctvFreddy.GetViewMatrix(), cctvFreddy.GetProjectionMatrix(), cctvFreddy.Position, directLightList, pointLightList, spotLightList);
                }
                else if (selection == 1)
                {
                    _budi.Render_object(cctvFreddy, all_lights);
                    budi.render(renderSetting, cctvFreddy.GetViewMatrix(), cctvFreddy.GetProjectionMatrix(), cctvFreddy.Position, directLightList, pointLightList, spotLightList);
                }
            }



            var inputss = KeyboardState;

            if (inputss.IsKeyDown(Keys.H))
            {
                checkers = 1;
            }
            else if (inputss.IsKeyReleased(Keys.H))
            {
                checkers = 0;
            }

            if (inputss.IsKeyDown(Keys.J))
            {
                checkers = 2;
            }
            else if (inputss.IsKeyReleased(Keys.J))
            {
                checkers = 0;
            }

            if (inputss.IsKeyDown(Keys.K))
            {
                checkers = 3;
            }
            else if (inputss.IsKeyReleased(Keys.K))
            {
                checkers = 0;
            }

            if (inputss.IsKeyDown(Keys.L))
            {
                checkers = 4;
            }
            else if (inputss.IsKeyReleased(Keys.L))
            {
                checkers = 0;
            }

            if (inputss.IsKeyDown(Keys.D0))
            {
                checkers = 5;
            }
            else if (inputss.IsKeyReleased(Keys.D0))
            {
                checkers = 0;
            }

            if (inputss.IsKeyDown(Keys.U))
            {
                selection = 0;
                camera = camera1;
                all_lights[4][0] = _chara.chara;

            }
            else if (inputss.IsKeyDown(Keys.I))
            {
                selection = 1;
                camera = camera2;
                all_lights[4][0] = _budi.chara;
            }


            GL.Disable(EnableCap.CullFace);
            GL.DepthFunc(DepthFunction.Lequal);
            //cubemap.render(camera.GetViewMatrix(), camera.GetProjectionMatrix());
            GL.DepthFunc(DepthFunction.Less);
            GL.Enable(EnableCap.CullFace);

            totalTime += time;

            SwapBuffers();
        }

        Vector2 InputAxis;
        Vector3 MoveDir;
        Vector3 MoveDirBudi;
        float charaSpeed = 10f;
        float lastAngle = 0;
        float lastAngleBudi = 0;
        Vector3 cameraOffset = new Vector3(0.0f, 3.0f, 0.0f);
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            float time = (float)args.Time;

            if (!IsFocused)
            {
                return;
            }

            var input = KeyboardState;
            //Console.WriteLine(input.ToString());
            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            float freddy_speed = 0.1f;
            if (input.IsKeyDown(Keys.D1))
            {
                frs.move(new Vector3(-freddy_speed, 0, 0));
            }
            if (input.IsKeyDown(Keys.D2))
            {
                frs.move(new Vector3(freddy_speed, 0, 0));
            }
            if (input.IsKeyDown(Keys.D3))
            {
                frs.move(new Vector3(0, 0, -freddy_speed));
            }
            if (input.IsKeyDown(Keys.D4))
            {
                frs.move(new Vector3(0, 0, freddy_speed));
            }


            if (isThirdPerson)
            {
                if (input.IsKeyDown(Keys.W))
                {
                    InputAxis.Y = 1;
                }
                else if (input.IsKeyDown(Keys.S))
                {
                    InputAxis.Y = -1;
                }
                else
                {
                    InputAxis.Y = 0;
                }

                if (input.IsKeyDown(Keys.A))
                {
                    InputAxis.X = 1;
                }
                else if (input.IsKeyDown(Keys.D))
                {
                    InputAxis.X = -1;
                }
                else
                {
                    InputAxis.X = 0;
                }
            }
            else
            {
                if (input.IsKeyDown(Keys.W))
                {
                    camera.Position += Vector3.Normalize(Vector3.Cross(camera.Up, camera.Right)) * cameraSpeed * time;
                }
                if (input.IsKeyDown(Keys.S))
                {
                    camera.Position -= Vector3.Normalize(Vector3.Cross(camera.Up, camera.Right)) * cameraSpeed * time;
                }
                if (input.IsKeyDown(Keys.A))
                {
                    camera.Position -= camera.Right * cameraSpeed * time;
                }
                if (input.IsKeyDown(Keys.D))
                {
                    camera.Position += camera.Right * cameraSpeed * time;
                }
            }


            //Player Movement
            Vector3 Dir = new Vector3(InputAxis.X, 0, InputAxis.Y);

            if (Dir.Length >= 0.01f)
            {

                if (selection == 0)
                {
                    var targetAngle = MathHelper.RadiansToDegrees(MathF.Atan2(Dir.X, Dir.Z)) - tps.xTotal;
                    if (targetAngle != lastAngle)
                    {
                        var HasilAngle = targetAngle - lastAngle;
                        //chara.rotate(chara.objectCenter,chara._euler[1], HasilAngle);
                        _chara.rotate(_chara.chara.objectCenter, _chara.chara._euler[1], HasilAngle);
                    }
                    lastAngle = targetAngle;
                    //Console.WriteLine(camera.Yaw);
                    MoveDir = Quaternion.FromEulerAngles(0, MathHelper.DegreesToRadians(targetAngle), 0) * -Vector3.UnitZ;

                    _chara.move(MoveDir * charaSpeed * (float)args.Time, objectList);
                }
                else if (selection == 1)
                {
                    var targetAngle = MathHelper.RadiansToDegrees(MathF.Atan2(Dir.X, Dir.Z)) - tpsBudi.xTotal + 45f;
                    if (targetAngle != lastAngleBudi)
                    {
                        var HasilAngle = targetAngle - lastAngleBudi;
                        //chara.rotate(chara.objectCenter,chara._euler[1], HasilAngle);
                        _budi.rotate(_budi.chara.objectCenter, _budi.chara._euler[1], HasilAngle);
                    }
                    lastAngleBudi = targetAngle;
                    //Console.WriteLine(camera.Yaw);
                    MoveDirBudi = Quaternion.FromEulerAngles(0, MathHelper.DegreesToRadians(targetAngle), 0) * -Vector3.UnitZ;

                    _budi.move(MoveDirBudi * charaSpeed * (float)args.Time, objectList);
                }



            }
            else
            {
                MoveDir = Vector3.Zero;
                MoveDirBudi = Vector3.Zero;
            }




            if (input.IsKeyDown(Keys.Space))
            {
                camera.Position += camera.Up * cameraSpeed * time;
            }

            if (input.IsKeyDown(Keys.LeftShift))
            {
                camera.Position -= camera.Up * cameraSpeed * time;
            }

            if (input.IsKeyPressed(Keys.LeftControl))
            {
                cameraSpeed += 5;
                camera.Fov += 10;
            }

            if (input.IsKeyReleased(Keys.LeftControl))
            {
                cameraSpeed -= 5;
                camera.Fov -= 10;
            }

            if (input.IsKeyPressed(Keys.GraveAccent))
            {
                renderSetting *= -1;
            }

            //untuk mengubah posisi lampu berdasarkan sumbu X
            if (KeyboardState.IsKeyDown(Keys.Z))
            {
                _lightPos.X += 0.1f;
            }
            if (KeyboardState.IsKeyDown(Keys.X))
            {
                _lightPos.X -= 0.1f;
            }

            //untuk mengubah posisi lampu berdasarkan sumbu Y
            if (KeyboardState.IsKeyDown(Keys.C))
            {
                _lightPos.Y += 0.1f;
            }
            if (KeyboardState.IsKeyDown(Keys.V))
            {
                _lightPos.Y -= 0.1f;
            }

            //untuk mengubah posisi lampu berdasarkan sumbu Z
            if (KeyboardState.IsKeyDown(Keys.B))
            {
                _lightPos.Z += 0.1f;
            }
            if (KeyboardState.IsKeyDown(Keys.N))
            {
                _lightPos.Z -= 0.1f;
            }
            if (KeyboardState.IsKeyDown(Keys.T))
            {
                isThirdPerson = false;
                camera = fps;
                all_lights[4].Clear();
            }
            if (KeyboardState.IsKeyDown(Keys.Y))
            {
                isThirdPerson = true;
                camera = camera1;
            }
            if (!IsFocused)
            {
                return;
            }

            var mouse = MouseState;

            if (firstMove)
            {
                lastPos = new Vector2(mouse.X, mouse.Y);
                firstMove = false;
            }
            else
            {
                if (isThirdPerson)
                {
                    var deltaX = (mouse.X - lastPos.X);
                    var deltaY = (mouse.Y - lastPos.Y);
                    lastPos = new Vector2(mouse.X, mouse.Y);

                    if (selection == 0)
                    {
                        tps.HandleCameraRotation(camera, deltaX, deltaY, _chara.chara.objectCenter + cameraOffset, MoveDir, charaSpeed, _chara.chara.canMove, args);
                    }
                    else if (selection == 1)
                    {
                        tpsBudi.HandleCameraRotation(camera, deltaX, deltaY, _budi.chara.objectCenter + cameraOffset, MoveDirBudi, charaSpeed, _budi.chara.canMove, args);
                    }



                    if (xTotal >= rightAngle)
                    {
                        if (deltaX > 0)
                            xInput = 0;
                        else if (deltaX < 0)
                            xInput = deltaX;
                    }
                    else if (xTotal <= leftAngle)
                    {
                        if (deltaX < 0)
                            xInput = 0;
                        else if (deltaX > 0)
                            xInput = deltaX;
                    }
                    else //if (yTotal > -clampAngle && yTotal < clampAngle)
                    {
                        xInput = deltaX;
                    }

                    xTotal += xInput;

                    if (yTotal >= topAngle)
                    {
                        if (deltaY > 0)
                            yInput = 0;
                        else if (deltaY < 0)
                            yInput = deltaY;
                    }
                    else if (yTotal <= BottomAngle)
                    {
                        if (deltaY < 0)
                            yInput = 0;
                        else if (deltaY > 0)
                            yInput = deltaY;
                    }
                    else //if (yTotal > -clampAngle && yTotal < clampAngle)
                    {
                        yInput = deltaY;
                    }


                    yTotal += yInput;



                    cctv1.Yaw += xInput * sensitivity;
                    cctv1.Pitch -= yInput * sensitivity;

                    cctv2.Yaw += xInput * sensitivity;
                    cctv2.Pitch -= yInput * sensitivity;

                    cctv3.Yaw += xInput * sensitivity;
                    cctv3.Pitch -= yInput * sensitivity;

                    cctv4.Yaw += xInput * sensitivity;
                    cctv4.Pitch -= yInput * sensitivity;

                    cctvFreddy.Yaw += xInput * sensitivity;
                    cctvFreddy.Pitch -= yInput * sensitivity;
                }
                else
                {

                    var deltaX = mouse.X - lastPos.X;
                    var deltaY = mouse.Y - lastPos.Y;
                    lastPos = new Vector2(mouse.X, mouse.Y);

                    camera.Yaw += deltaX * sensitivity;
                    camera.Pitch -= deltaY * sensitivity;

                    cctv1.Yaw += deltaX * sensitivity;
                    cctv1.Pitch -= deltaY * sensitivity;

                    cctv2.Yaw += deltaX * sensitivity;
                    cctv2.Pitch -= deltaY * sensitivity;

                    cctv3.Yaw += deltaX * sensitivity;
                    cctv3.Pitch -= deltaY * sensitivity;

                    cctv4.Yaw += deltaX * sensitivity;
                    cctv4.Pitch -= deltaY * sensitivity;

                    cctvFreddy.Yaw += xInput * sensitivity;
                    cctvFreddy.Pitch -= yInput * sensitivity;

                }
            }

            lights.keyboard_up(input);

            lights.keyboard_down(input);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);

            camera.AspectRatio = Size.X / (float)Size.Y;
        }
    }
}
