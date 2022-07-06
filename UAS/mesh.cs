using System;
using System.Collections.Generic;
using System.Text;
using LearnOpenTK.Common;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LightFast
{
    class mesh
    {

        private readonly string path = "../../../Shaders/";

        public boundingBox bound;

        public List<Vector3> vertices = new List<Vector3>();
        protected List<Vector3> texture = new List<Vector3>();
        protected List<Vector3> normals = new List<Vector3>();
        protected List<float> _combine = new List<float>();
        private float[] _realVertices;
        protected List<uint> indeces = new List<uint>();
        protected List<uint> indeces_n = new List<uint>();

        protected int _VBO;
        protected int _VAO;
        protected int _VAO_lamp;
        protected int _EBO;

        private Shader _lightingShader;
        private Shader _lampShader;

        protected Matrix4 _transform;
        protected Matrix4 _transform_tmp;

        //Light
        public Vector3 light_pos;
        public float amb_stre;
        public float spec_stre;
        public float spot_angle;
        public Vector3 light_dir;
        public Vector3 color;

        //Bounding Box
        public float Xmax = float.MinValue;
        public float Xmin = float.MaxValue;
        public float Ymax = float.MinValue;
        public float Ymin = float.MaxValue;
        public float Zmax = float.MinValue;
        public float Zmin = float.MaxValue;

        public Vector3 objectCenter = Vector3.Zero;

        public List<Vector3> _euler = new List<Vector3>();

        //chara

        public bool canMove = false;

        public float light_maxDis;

        public mesh()
        {
            resetEuler();

        }

        /*public mesh(List<Vector3> vertices)
        {
            this.vertices = vertices;

            Vector3 min = vertices[0];
            Vector3 max = vertices[0];

            foreach (Vector3 v in vertices)
            {
                if (v.X <= min.X)
                {
                    min.X = v.X;
                }
                else if (v.X >= max.X)
                {
                    max.X = v.X;
                }

                if (v.Y <= min.Y)
                {
                    min.Y = v.Y;
                }
                else if (v.Y >= max.Y)
                {
                    max.Y = v.Y;
                }

                if (v.Z <= min.Z)
                {
                    min.Z = v.Z;
                }
                else if (v.Z >= max.Z)
                {
                    max.Z = v.Z;
                }
            }

            //bound = new boundingBox(min, max);


        }*/


        public void set_transform()
        {
            _transform = Matrix4.Identity;
        }

        public void set_VAO()
        {
            _VAO = GL.GenVertexArray();
            GL.BindVertexArray(_VAO);
        }
        public void set_VAO_lamp()
        {
            _VAO_lamp = GL.GenVertexArray();
            GL.BindVertexArray(_VAO_lamp);
        }
        public void set_VBO()
        {
            combine();
            _VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, _realVertices.Length * sizeof(float), _realVertices, BufferUsageHint.StaticDraw);
        }

        public void enable_vertex()
        {

            var vertexLocation = _lightingShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);


            var normalLocation = _lightingShader.GetAttribLocation("aNormal"); // Karena ada normalisasi, jadi buat baru. Jika tidak ada normalisasi, bisa dihapus
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

        }

        public void set_EBO()
        {
            _EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indeces.Count * sizeof(uint), indeces.ToArray(), BufferUsageHint.StaticDraw);

        }

        public void set_shader()
        {
            _lightingShader = new Shader(path + "shader2.vert", path + "lighting.frag");
            enable_vertex();
        }

        public void set_shader_lamp()
        {
            _lampShader = new Shader(path + "shader2.vert", path + "shader2.frag");

            var vertexLocation = _lampShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

        }

        public void set_light_posCol(Vector3 lightPos, Vector3 lightColor)
        {
            this.light_pos = lightPos;
            this.color = lightColor;
            //Console.WriteLine(this.color);
        }

        public void setPointLight(float amb_stre, float spec_stre, float maxDis)
        {
            this.amb_stre = amb_stre;
            this.spec_stre = spec_stre;
            this.light_maxDis = maxDis;
        }

        public void setDirectLight(float amb_stre, float spec_stre, Vector3 lightDir)
        {
            /*setPointLight(amb_stre, spec_stre);*/
            this.amb_stre = amb_stre;
            this.spec_stre = spec_stre;
            /*            this.light_maxDis = maxDis;*/
            this.light_dir = lightDir;
        }

        public void setSpotLight(float amb_stre, float spec_stre, Vector3 spotDir, float spotAngle, float maxDis)
        {
            setDirectLight(amb_stre, spec_stre, spotDir);
            this.light_maxDis = maxDis;
            this.spot_angle = spotAngle;
        }

        public void initialize(string p1)
        {
            set_transform();
            LoadObjFile(p1);
            set_VBO();
            set_VAO();
            set_shader();

            foreach (Vector3 point in vertices)
            {
                //Console.WriteLine(point.X + " " + point.Y + " " + point.Z);
                if (Xmax < point.X)
                {
                    Xmax = point.X;
                }
                else if (Xmin > point.X)
                {
                    Xmin = point.X;
                }

                if (Ymax < point.Y)
                {
                    Ymax = point.Y;
                }
                else if (Ymin > point.Y)
                {
                    Ymin = point.Y;
                }

                if (Zmax < point.Z)
                {
                    Zmax = point.Z;
                }
                else if (Zmin > point.Z)
                {
                    Zmin = point.Z;
                }

            }
        }


        public void initialize2(string p1, mesh chara)
        {
            set_transform();
            LoadObjFile(p1);
            set_VBO();
            set_VAO();
            set_shader();

            /*foreach (Vector3 point in vertices)
            {
                Console.WriteLine(point.X + " " + point.Y + " " + point.Z);
                if (Xmax < point.X)
                {
                    Xmax = point.X;
                }
                else if (Xmin > point.X)
                {
                    Xmin = point.X;
                }

                if (Ymax < point.Y)
                {
                    Ymax = point.Y;
                }
                else if (Ymin > point.Y)
                {
                    Ymin = point.Y;
                }

                if (Zmax < point.Z)
                {
                    Zmax = point.Z;
                }
                else if (Zmin > point.Z)
                {
                    Zmin = point.Z;
                }

            }*/

            foreach (Vector3 point in chara.vertices)
            {
                //Console.WriteLine(point.X + " " + point.Y + " " + point.Z);
                if (chara.Xmax < point.X)
                {
                    chara.Xmax = point.X;
                }
                else if (chara.Xmin > point.X)
                {
                    chara.Xmin = point.X;
                }

                if (chara.Zmax < point.Z)
                {
                    chara.Zmax = point.Z;
                }
                else if (chara.Zmin > point.Z)
                {
                    chara.Zmin = point.Z;
                }

            }
        }

        public bool check_Boundary(Vector3 _pos, List<mesh> all_object, mesh chara)
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

        public void translate2(Vector3 _pos, List<mesh> all_object, mesh chara)
        {

            if (check_Boundary(_pos, all_object, chara))
            {
                chara.translate(_pos.X, _pos.Y, _pos.Z);
                chara.Xmax += _pos.X;
                chara.Xmin += _pos.X;
                chara.Zmax += _pos.Z;
                chara.Zmin += _pos.Z;
                canMove = true;
            }
            else
            {
                canMove = false;
            }

        }



        public void initialize_lamp(string p1, Vector3 lightPos, Vector3 lightColor)
        {
            initialize(p1);
            set_VAO_lamp();
            set_shader_lamp();
            set_light_posCol(lightPos, lightColor);
        }

        public void Render(Camera _camera, Vector3 _color, List<List<mesh>> all_light)
        {
            //Console.WriteLine(all_light.Count);
            GL.BindVertexArray(_VAO);
            _lightingShader.Use();

            _lightingShader.SetMatrix4("transform", _transform);
            _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            _lightingShader.SetVector3("objectColor", _color);
            //Console.WriteLine(_color);
            _lightingShader.SetVector3("viewPos", _camera.Position);

            for (int i = 0; i < all_light[0].Count; i++)
            {
                //_shader.SetVector3($"directList[{i}].lightPos", dirlightList[i].objectCenter);
                _lightingShader.SetVector3($"directList[{i}].lightColor", all_light[0][i].color);
                _lightingShader.SetFloat($"directList[{i}].ambientStre", all_light[0][i].amb_stre);
                _lightingShader.SetFloat($"directList[{i}].specStre", all_light[0][i].spec_stre);

                _lightingShader.SetVector3($"directList[{i}].lightDir", -all_light[0][i].light_dir);
            }
            for (int i = 0; i < all_light[1].Count; i++)
            {
                _lightingShader.SetVector3($"pointList[{i}].lightPos", all_light[1][i].light_pos);
                _lightingShader.SetVector3($"pointList[{i}].lightColor", all_light[1][i].color);
                _lightingShader.SetFloat($"pointList[{i}].ambientStre", all_light[1][i].amb_stre);
                _lightingShader.SetFloat($"pointList[{i}].specStre", all_light[1][i].spec_stre);
                _lightingShader.SetFloat($"pointList[{i}].lightMaxDist", all_light[1][i].light_maxDis);

            }
            for (int i = 0; i < all_light[2].Count; i++)
            {
                //_shader.SetVector3($"spotList[{i}].lightPos", spotlightList[i].objectCenter);
                //_lightingShader.SetVector3($"spotList[{i}].lightPos", all_light[2][i].light_pos);
                if (all_light[4].Count > 0)
                {
                    Vector3 offset = new Vector3(0, 2f, 0);
                    _lightingShader.SetVector3($"spotList[{i}].lightPos", all_light[4][0].objectCenter + offset);
                }
                else
                {
                    _lightingShader.SetVector3($"spotList[{i}].lightPos", _camera.Position);
                }

                _lightingShader.SetVector3($"spotList[{i}].lightColor", all_light[2][i].color);
                _lightingShader.SetFloat($"spotList[{i}].ambientStre", all_light[2][i].amb_stre);
                _lightingShader.SetFloat($"spotList[{i}].specStre", all_light[2][i].spec_stre);

                //_lightingShader.SetVector3($"spotList[{i}].spotDir", -all_light[2][i].light_dir);

                if (all_light[4].Count > 0)
                {
                    _lightingShader.SetVector3($"spotList[{i}].spotDir", -all_light[4][0]._euler[2]);
                }
                else
                {
                    _lightingShader.SetVector3($"spotList[{i}].spotDir", -_camera.Front);
                }
                //_lightingShader.SetVector3($"spotList[{i}].spotDir", -_camera.Front);
                //_lightingShader.SetVector3($"spotList[{i}].spotDir", -all_light[4][0]._euler[2]);

                _lightingShader.SetFloat($"spotList[{i}].spotAngleCos", (float)MathHelper.Cos(MathHelper.DegreesToRadians(all_light[2][i].spot_angle - 1)));
                //_lightingShader.SetFloat($"spotNCList[{i}].softSpotAngleCos", (float)MathHelper.Cos(MathHelper.DegreesToRadians(all_light[2][i].spot_angle)));
                _lightingShader.SetFloat($"spotList[{i}].lightMaxDist", all_light[2][i].light_maxDis);
            }
            for (int i = 0; i < all_light[3].Count; i++)
            {
                //_shader.SetVector3($"spotNCList[{i}].lightPos", spotlightList[i].objectCenter);
                _lightingShader.SetVector3($"spotNCList[{i}].lightPos", all_light[3][i].light_pos);
                //_lightingShader.SetVector3($"spotNCList[{i}].lightPos", _camera.Position);
                _lightingShader.SetVector3($"spotNCList[{i}].lightColor", all_light[3][i].color);
                _lightingShader.SetFloat($"spotNCList[{i}].ambientStre", all_light[3][i].amb_stre);
                _lightingShader.SetFloat($"spotNCList[{i}].specStre", all_light[3][i].spec_stre);
                //all_light[3][i].light_dir = new Vector3(1,0,0);
                _lightingShader.SetVector3($"spotNCList[{i}].spotDir", -all_light[3][i].light_dir);
                //Console.WriteLine(all_light[3][i].light_dir);
                //_lightingShader.SetVector3($"spotNCList[{i}].spotDir", -_camera.Front);

                _lightingShader.SetFloat($"spotNCList[{i}].spotAngleCos", (float)MathHelper.Cos(MathHelper.DegreesToRadians(all_light[3][i].spot_angle - 1)));
                //_lightingShader.SetFloat($"spotNCList[{i}].softSpotAngleCos", (float)MathHelper.Cos(MathHelper.DegreesToRadians(all_light[3][i].spot_angle)));
                _lightingShader.SetFloat($"spotNCList[{i}].lightMaxDist", all_light[3][i].light_maxDis);


            }

            // _lightingShader.SetVector3("lightColor", new Vector3(1.0f, 1.0f, 1.0f));
            // _lightingShader.SetVector3("lightPos", _lightPos);



            GL.DrawArrays(PrimitiveType.Triangles, 0, _realVertices.Length);
        }

        public void Render_lamp(Camera _camera, Vector3 _lightPos, Vector3 _color)
        {
            /*
            GL.BindVertexArray(_VAO);
            _lightingShader.Use();

            _lightingShader.SetMatrix4("transform", _transform);
            _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            _lightingShader.SetVector3("objectColor", _color);
            _lightingShader.SetVector3("lightColor", new Vector3(1.0f, 1.0f, 0.0f));
            _lightingShader.SetVector3("lightPos", _lightPos);
            _lightingShader.SetVector3("viewPos", _camera.Position);
            */

            GL.BindVertexArray(_VAO_lamp);
            _lampShader.Use();

            Matrix4 lampMatrix = Matrix4.CreateScale(2f);
            lampMatrix = lampMatrix * Matrix4.CreateTranslation(this.light_pos);

            _lampShader.SetMatrix4("transform", lampMatrix);
            _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            GL.DrawArrays(PrimitiveType.Triangles, 0, _realVertices.Length);

        }

        private void combine()
        {
            int n = 0;
            int cnt = 0;
            foreach (int i in indeces)
            {

                n = (int)indeces_n[cnt];

                _combine.Add(vertices[i].X);
                _combine.Add(vertices[i].Y);
                _combine.Add(vertices[i].Z);
                _combine.Add(normals[n].X);
                _combine.Add(normals[n].Y);
                _combine.Add(normals[n].Z);

                cnt++;
            }
            _realVertices = _combine.ToArray();
        }

        public void save()
        {
            _transform_tmp = _transform;
        }
        public void reset()
        {
            _transform = _transform_tmp;
        }

        public void rotate(float dr)
        {
            _transform = _transform * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(dr));

        }
        public void resetEuler()
        {
            _euler.Clear();
            _euler.Add(Vector3.UnitX);
            _euler.Add(Vector3.UnitY);
            _euler.Add(Vector3.UnitZ);
        }
        public void rotate(Vector3 pivot, Vector3 vector, float angle)
        {
            var radAngle = MathHelper.DegreesToRadians(angle);

            var arbRotationMatrix = new Matrix4
                (
                new Vector4((float)(Math.Cos(radAngle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(radAngle))), (float)(vector.X * vector.Y * (1.0f - Math.Cos(radAngle)) + vector.Z * Math.Sin(radAngle)), (float)(vector.X * vector.Z * (1.0f - Math.Cos(radAngle)) - vector.Y * Math.Sin(radAngle)), 0),
                new Vector4((float)(vector.X * vector.Y * (1.0f - Math.Cos(radAngle)) - vector.Z * Math.Sin(radAngle)), (float)(Math.Cos(radAngle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(radAngle))), (float)(vector.Y * vector.Z * (1.0f - Math.Cos(radAngle)) + vector.X * Math.Sin(radAngle)), 0),
                new Vector4((float)(vector.X * vector.Z * (1.0f - Math.Cos(radAngle)) + vector.Y * Math.Sin(radAngle)), (float)(vector.Y * vector.Z * (1.0f - Math.Cos(radAngle)) - vector.X * Math.Sin(radAngle)), (float)(Math.Cos(radAngle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(radAngle))), 0),
                Vector4.UnitW
                );

            _transform *= Matrix4.CreateTranslation(-pivot);
            _transform *= arbRotationMatrix;
            _transform *= Matrix4.CreateTranslation(pivot);


            for (int i = 0; i < 3; i++)
            {
                _euler[i] = Vector3.Normalize(getRotationResult(pivot, vector, radAngle, _euler[i], true));
            }

            objectCenter = getRotationResult(pivot, vector, radAngle, objectCenter);


        }

        public Vector3 getRotationResult(Vector3 pivot, Vector3 vector, float angle, Vector3 point, bool isEuler = false)
        {
            Vector3 temp, newPosition;

            if (isEuler)
            {
                temp = point;
            }
            else
            {
                temp = point - pivot;
            }

            newPosition.X =
                temp.X * (float)(Math.Cos(angle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(angle))) +
                temp.Y * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) - vector.Z * Math.Sin(angle)) +
                temp.Z * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) + vector.Y * Math.Sin(angle));

            newPosition.Y =
                temp.X * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) + vector.Z * Math.Sin(angle)) +
                temp.Y * (float)(Math.Cos(angle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(angle))) +
                temp.Z * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) - vector.X * Math.Sin(angle));

            newPosition.Z =
                temp.X * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) - vector.Y * Math.Sin(angle)) +
                temp.Y * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) + vector.X * Math.Sin(angle)) +
                temp.Z * (float)(Math.Cos(angle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(angle)));

            if (isEuler)
            {
                temp = newPosition;
            }
            else
            {
                temp = newPosition + pivot;
            }
            return temp;
        }
        public void scale(float r)
        {
            _transform = _transform * Matrix4.CreateScale(r);
        }

        public void translate(float dx, float dy, float dz)
        {

            _transform = _transform * Matrix4.CreateTranslation(dx, dy, dz);

            objectCenter.X += dx;
            objectCenter.Y += dy;
            objectCenter.Z += dz;

        }


        public void LoadObjFile(string path)
        {

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to open \"" + path + "\", does not exist.");
            }

            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {

                    List<string> words = new List<string>(streamReader.ReadLine().ToLower().Split(' '));

                    words.RemoveAll(s => s == string.Empty);

                    if (words.Count == 0)
                    {
                        continue;
                    }

                    string type = words[0];
                    words.RemoveAt(0);

                    switch (type)
                    {

                        case "v":
                            vertices.Add(new Vector3(float.Parse(words[0]) / 1000000, float.Parse(words[1]) / 1000000, float.Parse(words[2]) / 1000000));
                            break;

                        case "vt":
                            texture.Add(new Vector3(float.Parse(words[0]), float.Parse(words[1]),
                                                            words.Count < 3 ? 0 : float.Parse(words[2])));
                            break;

                        case "vn":
                            normals.Add(new Vector3(float.Parse(words[0]), float.Parse(words[1]), float.Parse(words[2])));
                            break;

                        case "f":
                            foreach (string w in words)
                            {
                                if (w.Length == 0)
                                    continue;

                                string[] comps = w.Split('/');

                                indeces.Add(uint.Parse(comps[0]) - 1);
                                indeces_n.Add(uint.Parse(comps[2]) - 1);
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}
