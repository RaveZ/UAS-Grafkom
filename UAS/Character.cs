using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using LearnOpenTK.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
namespace LightFast
{
    internal class Character
    {

        string path_chara = "../../../Player/Player.obj";
        string path_free = "../../../freedy/Freddy-Fazbear.obj";

        public mesh chara = new mesh();
        private int counter = 0;

        public Character(int test)
        {
            this.counter = test;
        }

        public void load()
        {
            if(counter == 0)
            {
                chara.initialize2(path_chara, chara);
            }
            else if (counter == 1)
            {
                chara.initialize2(path_free, chara);
            }
            
        }


        public void move(Vector3 _pos, List<mesh> all_object)
        {
            chara.translate2(_pos, all_object, chara);


        }
        public void rotate(Vector3 pivot, Vector3 vector, float angle)
        {
            chara.rotate(pivot, vector, angle);
        }
        public void Render_object(Camera _camera, List<List<mesh>> all_lights)
        {
            chara.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);
        }

        public bool check_Boundary(Vector3 _pos, List<mesh> all_object)
        {
            Vector2 l1 = new Vector2(chara.Xmin + _pos.X, chara.Zmax + _pos.Z);
            Vector2 r1 = new Vector2(chara.Xmax + _pos.X, chara.Zmin + _pos.Z);

            foreach (mesh obj in all_object)
            {
                Vector2 l2 = new Vector2(obj.Xmin, obj.Zmax);
                Vector2 r2 = new Vector2(obj.Xmax, obj.Zmin);
                bool overlap = check_overlap(l1, r1, l2, r2);
                if (overlap)
                {
                    //Console.WriteLine("NOT PASS");
                    return false;

                }


            }
            //Console.WriteLine("MOVE");
            return true;

        }


        public bool check_overlap(Vector2 l1, Vector2 r1, Vector2 l2, Vector2 r2)
        {
            if (l1.X == r1.X || l1.Y == r1.Y || r2.X == l2.X || l2.Y == r2.Y)
            {
                return false;

            }

            // If one rectangle is on left side of other
            if (l1.X > r2.X || l2.X > r1.X)
            {
                return false;

            }

            // If one rectangle is above other
            if (r1.Y > l2.Y || r2.Y > l1.Y)
            {
                return false;
            }

            return true;
        }

    }
}