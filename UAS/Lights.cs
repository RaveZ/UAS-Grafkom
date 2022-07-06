using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnOpenTK.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
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
    internal class Lights
    {
        List<mesh> pointL = new List<mesh>(); //point light
        List<mesh> directL = new List<mesh>(); //directional light
        List<mesh> spotL = new List<mesh>(); //spot light
        List<mesh> spotLnC = new List<mesh>(); //spot light non Camera

        //Lights
        string path_sun = "../../../freedyMap/sun.obj";
        mesh sun = new mesh();
        mesh stageL = new mesh();
        mesh stageR = new mesh();
        mesh officeL = new mesh();
        mesh leftL1 = new mesh();
        mesh leftL2 = new mesh();
        mesh rightL1 = new mesh();
        mesh rightL2 = new mesh();
        mesh gudangL = new mesh();
        mesh room1L = new mesh();
        mesh room2L = new mesh();
        bool lamp_stat = true;

        mesh flashlight = new mesh();
        bool flashlight_stat = true;

        mesh stagelight = new mesh();
        mesh stagelightR = new mesh();
        mesh stagelightB = new mesh();




        public Lights() { }

        public void load()
        {
            float ceilingY = 25;

            sun.initialize_lamp(path_sun, new Vector3(10f, ceilingY, 22.0f), new Vector3(1, 1, 1));
            sun.setPointLight(0.3f, 0.5f, 50);
            pointL.Add(sun);

            officeL.initialize_lamp(path_sun, new Vector3(-90f, ceilingY, 22.0f), new Vector3(1, 1, 1));
            officeL.setPointLight(0, 0.5f, 40);
            pointL.Add(officeL);

            leftL1.initialize_lamp(path_sun, new Vector3(-40f, ceilingY, -2.0f), new Vector3(1, 1, 1));
            leftL1.setPointLight(0.1f, 0.5f, 35);
            pointL.Add(leftL1);

            leftL2.initialize_lamp(path_sun, new Vector3(-70f, ceilingY, -2.0f), new Vector3(1, 1, 1));
            leftL2.setPointLight(0.1f, 0.5f, 35);
            pointL.Add(leftL2);

            rightL1.initialize_lamp(path_sun, new Vector3(-40f, ceilingY, 41.0f), new Vector3(1, 1, 1));
            rightL1.setPointLight(0.1f, 0.5f, 35);
            pointL.Add(rightL1);

            rightL2.initialize_lamp(path_sun, new Vector3(-70f, ceilingY, 41.0f), new Vector3(1, 1, 1));
            rightL2.setPointLight(0.1f, 0.5f, 35);
            pointL.Add(rightL2);

            gudangL.initialize_lamp(path_sun, new Vector3(38f, ceilingY, -30.0f), new Vector3(1, 1, 1));
            gudangL.setPointLight(0.1f, 0.5f, 40);
            pointL.Add(gudangL);

            room1L.initialize_lamp(path_sun, new Vector3(-10f, ceilingY, -30.0f), new Vector3(1, 1, 1));
            room1L.setPointLight(0.1f, 0.5f, 40);
            pointL.Add(room1L);

            room2L.initialize_lamp(path_sun, new Vector3(-50f, ceilingY, -23.0f), new Vector3(1, 1, 1));
            room2L.setPointLight(0.1f, 0.5f, 40);
            pointL.Add(room2L);

            stageL.initialize_lamp(path_sun, new Vector3(43f, ceilingY, 5f), new Vector3(0.5f, 0, 0));
            stageL.setPointLight(0.1f, 0.2f, 25);
            pointL.Add(stageL);

            stageR.initialize_lamp(path_sun, new Vector3(43f, ceilingY, 40f), new Vector3(0.5f, 0, 0));
            stageR.setPointLight(0.1f, 0.2f, 25);
            pointL.Add(stageR);

            //sun.initialize_lamp(path_sun, new Vector3(0f, 6.0f, -10.0f), new Vector3(1,1,1));
            //sun.setPointLight(0.3f, 0.5f, 50);
            //pointL.Add(sun);



            flashlight.initialize_lamp(path_sun, new Vector3(), new Vector3(255 / 255f, 227 / 255f, 82 / 255f));
            flashlight.setSpotLight(0, 0.5f, new Vector3(0, -1, 0), 17, 60);
            spotL.Add(flashlight);

            stagelight.initialize_lamp(path_sun, new Vector3(43f, 25.0f, 22.0f), new Vector3(1, 1, 1));
            stagelight.setSpotLight(0, 1, new Vector3(0, -1, 0), 7, 80f);
            spotLnC.Add(stagelight);

            stagelightR.initialize_lamp(path_sun, new Vector3(43f, 25.0f, 18.0f), new Vector3(0.98f, 0.187f, 0.525f));
            stagelightR.setSpotLight(0, 1, new Vector3(0, -1, 0), 7, 80f);
            spotLnC.Add(stagelightR);

            stagelightB.initialize_lamp(path_sun, new Vector3(43f, 25.0f, 26.0f), new Vector3(0.091f, 0.473f, 0.9801f));
            stagelightB.setSpotLight(0, 1, new Vector3(0, -1, 0), 7, 80f);
            spotLnC.Add(stagelightB);






        }

        public void render(Camera camera)
        {
            if (lamp_stat)
            {
                sun.color = new Vector3(1, 1, 1);
                officeL.color = new Vector3(1, 1, 1);
                leftL1.color = new Vector3(1, 1, 1);
                leftL2.color = new Vector3(1, 1, 1);
                rightL1.color = new Vector3(1, 1, 1);
                rightL2.color = new Vector3(1, 1, 1);
                gudangL.color = new Vector3(1, 1, 1);
                room1L.color = new Vector3(1, 1, 1);
                room2L.color = new Vector3(1, 1, 1);
                stageL.color = new Vector3(0.5f, 0, 0);
                stageR.color = new Vector3(0.5f, 0, 0);
                stagelight.color = new Vector3(1, 1, 1);
                stagelightR.color = new Vector3(0.98f, 0.187f, 0.525f);
                stagelightB.color = new Vector3(0.091f, 0.473f, 0.9801f);
                flashlight.amb_stre = 0;
                foreach (mesh light in pointL)
                {
                    light.Render_lamp(camera, light.light_pos, light.color);
                }
                foreach (mesh light in spotLnC)
                {
                    light.Render_lamp(camera, light.light_pos, light.color);
                }

            }
            else
            {
                flashlight.amb_stre = 0.05f;
                foreach (mesh light in pointL)
                {
                    light.color = new Vector3(0, 0, 0);
                }
                foreach (mesh light in spotLnC)
                {
                    light.color = new Vector3(0, 0, 0);
                }
            }


            if (flashlight_stat)
            {
                flashlight.color = new Vector3(255 / 255f, 227 / 255f, 82 / 255f);
            }
            else
            {
                flashlight.color = new Vector3(0, 0, 0);
            }
            //flashlight.Render_lamp(camera, flashlight.light_pos, flashlight.color);

        }

        public List<List<mesh>> out_light()
        {
            List<List<mesh>> all_light = new List<List<mesh>>();
            all_light.Add(directL);
            all_light.Add(pointL);
            all_light.Add(spotL);
            all_light.Add(spotLnC);

            return all_light;
        }

        public void keyboard_down(KeyboardState input)
        {
            //untuk mengubah posisi lampu berdasarkan sumbu X
            if (input.IsKeyDown(Keys.Z))
            {
                sun.light_pos.X += 0.75f;
            }
            if (input.IsKeyDown(Keys.X))
            {
                sun.light_pos.X -= 0.75f;
            }

            //untuk mengubah posisi lampu berdasarkan sumbu Y
            if (input.IsKeyDown(Keys.C))
            {
                sun.light_pos.Y += 0.75f;
            }
            if (input.IsKeyDown(Keys.V))
            {
                sun.light_pos.Y -= 0.75f;
            }

            //untuk mengubah posisi lampu berdasarkan sumbu Z
            if (input.IsKeyDown(Keys.B))
            {
                sun.light_pos.Z += 0.75f;
            }
            if (input.IsKeyDown(Keys.N))
            {
                sun.light_pos.Z -= 0.75f;
            }


        }

        public void keyboard_up(KeyboardState input)
        {

            if (input.IsKeyReleased(Keys.P))
            {
                flashlight_stat = !flashlight_stat;
            }
            if (input.IsKeyReleased(Keys.O))
            {
                lamp_stat = !lamp_stat;
            }



        }


    }
}