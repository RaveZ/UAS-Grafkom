using OpenTK;
using OpenTK.Graphics;
using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace LightFast
{
    /*internal class boundingBox
    {
        mesh _a = new mesh();
        string _path;

        public boundingBox(mesh _a, string _path)
        {
            this._a = _a;
            this._path = _path;
        }
        public void load()
        {
            _a.initialize(_path);
            foreach (Vector3 point in _a.vertices)
            {
                //Console.WriteLine(point.X + " " + point.Y + " " + point.Z);
                if (_a.Xmax < point.X)
                {
                    _a.Xmax = point.X;
                }
                else if (_a.Xmin > point.X)
                {
                    _a.Xmin = point.X;
                }

                if (_a.Zmax < point.Z)
                {
                    _a.Zmax = point.Z;
                }
                else if (_a.Zmin > point.Z)
                {
                    _a.Zmin = point.Z;
                }

            }
        }
        public void move(Vector3 _pos, List<mesh> all_object)
        {
            if (check_Boundary(_pos, all_object))
            {
                _a.translate(_pos.X, _pos.Y, _pos.Z);
                Console.WriteLine(_pos.X + " " + _pos.Y + " " + _pos.Z);
                _a.Xmax += _pos.X;
                _a.Xmin += _pos.X;
                _a.Zmax += _pos.Z;
                _a.Zmin += _pos.Z;
                Console.WriteLine(_a.Xmax);
                Console.WriteLine(_a.Xmin);
                Console.WriteLine(_a.Zmax);
                Console.WriteLine(_a.Zmin);

            }


        }
        public void rotate(Vector3 pivot, Vector3 vector, float angle)
        {
            _a.rotate(pivot, vector, angle);
        }
        public void Render_object(Camera _camera, List<List<mesh>> all_lights)
        {
            _a.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);
        }

        public bool check_Boundary(Vector3 _pos, List<mesh> all_object)
        {
            Vector2 l1 = new Vector2(_a.Xmin + _pos.X, _a.Zmax + _pos.Z);
            Vector2 r1 = new Vector2(_a.Xmax + _pos.X, _a.Zmin + _pos.Z);

            foreach (mesh obj in all_object)
            {
                Vector2 l2 = new Vector2(obj.Xmin, obj.Zmax);
                Vector2 r2 = new Vector2(obj.Xmax, obj.Zmin);
                bool overlap = check_overlap(l1, r1, l2, r2);
                if (overlap)
                {
                    Console.WriteLine("NOT PASS");
                    return false;

                }


            }
            Console.WriteLine("MOVE");
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
    }*/

    internal class boundingBox
    {
        Vector3 min = Vector3.Zero, max = Vector3.Zero;

        public Vector3 Min
        {
            set { min = value; }
            get { return min; }
        }

        public Vector3 Max
        {
            set { max = value; }
            get { return max; }
        }

        public Vector3 Center
        {
            set
            {
                Vector3 dist = value - Center;
                min += dist;
                max += dist;
            }
            get { return (min + max) / 2.0f; }
        }

        public Vector3 HalfSize
        {
            set
            {
                Vector3 cent = Center;
                max = cent + value;
                min = cent - value;
            }
            get { return (max - min) / 2.0f; }
        }

        public void Scale(Vector3 scale)
        {
            Vector3 halfSize = this.HalfSize;

            halfSize.X *= scale.X;
            halfSize.Y *= scale.Y;
            halfSize.Z *= scale.Z;

            this.HalfSize = halfSize;
        }

        public boundingBox()
        {
        }

        public boundingBox(boundingBox box)
        {
            this.min = box.Min;
            this.max = box.Max;
        }

        public boundingBox(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public boundingBox(mesh mesh)
        {
            min = new Vector3(9999.0f, 9999.0f, 9999.0f);
            max = -min;

            foreach (Vector3 vec in mesh.vertices)
            {
                if (max.X < vec.X)
                    max.X = vec.X;
                if (max.Y < vec.Y)
                    max.Y = vec.Y;
                if (max.Z < vec.Z)
                    max.Z = vec.Z;

                if (min.X > vec.X)
                    min.X = vec.X;
                if (min.Y > vec.Y)
                    min.Y = vec.Y;
                if (min.Z > vec.Z)
                    min.Z = vec.Z;
            }
        }
    }
}