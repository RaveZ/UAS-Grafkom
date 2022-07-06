using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFast
{
    class Asset3dInstanced
    {
        private readonly string path = "../../../";

        private List<Vector3> vertices = new List<Vector3>();
        private List<uint> indices = new List<uint>();
        private List<Vector3> normals = new List<Vector3>();
        private List<Vector3> texCoords = new List<Vector3>();

        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;
        private int _shaderStorageBufferObjectModel;
        private int _shaderStorageBufferObjectNormal;

        private Shader _shader;
        private Texture _texture;

        private List<Matrix4> model;

        private Vector4 color;

        private string vertName;
        private string fragName;

        public List<Vector3> _euler = new List<Vector3>();
        public Vector3 objectCenter = Vector3.Zero;

        public Asset3dInstanced(string vertName, string fragName, List<Matrix4> model, Vector3 color, float alpha = 1)
        {
            this.color = new Vector4(color, alpha);
            this.vertName = vertName;
            this.fragName = fragName;
            this.model = model;

            _euler.Add(Vector3.UnitX);
            _euler.Add(Vector3.UnitY);
            _euler.Add(Vector3.UnitZ);
        }

        public void load()
        {
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _shaderStorageBufferObjectModel = GL.GenBuffer();

            var transposedModel = new List<Matrix4>(model);

            for (int i = 0; i < transposedModel.Count; i++)
            {
                transposedModel[i] = Matrix4.Transpose(transposedModel[i]);
            }

            GL.BindBuffer(BufferTarget.ShaderStorageBuffer, _shaderStorageBufferObjectModel);

            GL.BufferData(BufferTarget.ShaderStorageBuffer, transposedModel.Count * Vector4.SizeInBytes * 4, transposedModel.ToArray(), BufferUsageHint.StaticDraw);
            GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, _shaderStorageBufferObjectModel);

            if (texCoords.Count == 0 && normals.Count == 0)
            {
                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);

                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);
            }
            else if (texCoords.Count > 0)
            {
                var combinedData = new List<Vector3>();
                for (int i = 0; i < vertices.Count; i++)
                {
                    combinedData.Add(vertices[i]);
                    combinedData.Add(texCoords[i]);
                }

                GL.BufferData(BufferTarget.ArrayBuffer, combinedData.Count * Vector3.SizeInBytes, combinedData.ToArray(), BufferUsageHint.StaticDraw);

                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);

                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
                GL.EnableVertexAttribArray(1);
            }
            else if (normals.Count > 0)
            {
                var combinedData = new List<Vector3>();
                for (int i = 0; i < vertices.Count; i++)
                {
                    combinedData.Add(vertices[i]);
                    combinedData.Add(normals[i]);
                }

                GL.BufferData(BufferTarget.ArrayBuffer, combinedData.Count * Vector3.SizeInBytes, combinedData.ToArray(), BufferUsageHint.StaticDraw);

                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);

                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
                GL.EnableVertexAttribArray(1);

                _shaderStorageBufferObjectNormal = GL.GenBuffer();

                var normalMats = new List<Matrix4>();

                for (int i = 0; i < transposedModel.Count; i++)
                {
                    normalMats.Add(Matrix4.Transpose(Matrix4.Invert(transposedModel[i])));
                }

                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, _shaderStorageBufferObjectNormal);

                GL.BufferData(BufferTarget.ShaderStorageBuffer, normalMats.Count * Vector4.SizeInBytes * 4, normalMats.ToArray(), BufferUsageHint.StaticDraw);
                GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 1, _shaderStorageBufferObjectNormal);
            }

            GL.BindBuffer(BufferTarget.ShaderStorageBuffer, 0);

            if (indices.Count != 0)
            {
                _elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(uint), indices.ToArray(), BufferUsageHint.StaticDraw);
            }

            _shader = new Shader(path + "Shaders/" + vertName, path + "Shaders/" + fragName);
            _shader.Use();

            if (texCoords.Count > 0)
            {
                _texture = Texture.LoadFromFile(path + "textures/top.png");
                _texture.Use(TextureUnit.Texture0);
            }
        }

        public void render(int line, Matrix4 camera_view, Matrix4 camera_projection, Vector3 cameraPos, List<Asset3d> dirlightList, List<Asset3d> pointlightList, List<Asset3d> spotlightList)
        {
            _shader.Use();

            _shader.SetMatrix4("view", camera_view);
            _shader.SetMatrix4("projection", camera_projection);

            GL.BindVertexArray(_vertexArrayObject);

            if (texCoords.Count > 0)
            {
                _texture.Use(TextureUnit.Texture0);
            }
            else
            {
                _shader.SetVector4("objColor", color);
            }

            if (normals.Count > 0)
            {
                //_shader.SetVector3("lightPos", light.objectCenter);
               // _shader.SetVector3("lightColor", light.color.Xyz);


                _shader.SetVector3("viewPos", cameraPos);

                //_shader.SetVector3("lightPos", light.objectCenter);
                //_shader.SetVector3("lightColor", light.color.Xyz);
                //for (int i = 0; i < lightList.Count; i++)
                //{
                //   _shader.SetVector3($"pointList[{i}].lightPos", lightList[i].objectCenter);
                //   _shader.SetVector3($"pointList[{i}].lightColor", new Vector3(lightList[i].color));
                ///   _shader.SetFloat($"pointList[{i}].ambientStre", 0.1f);
                //    _shader.SetFloat($"pointList[{i}].specStre", 1f);
                //}
                for (int i = 0; i < dirlightList.Count; i++)
                {
                    //_shader.SetVector3($"directList[{i}].lightPos", dirlightList[i].objectCenter);
                    _shader.SetVector3($"directList[{i}].lightColor", dirlightList[i].color.Xyz);
                    _shader.SetFloat($"directList[{i}].ambientStre", dirlightList[i].ambientStrength);
                    _shader.SetFloat($"directList[{i}].specStre", dirlightList[i].specStrength);

                    _shader.SetVector3($"directList[{i}].lightDir", -dirlightList[i].lightDirection);
                }
                for (int i = 0; i < pointlightList.Count; i++)
                {
                    _shader.SetVector3($"pointList[{i}].lightPos", pointlightList[i].objectCenter);
                    _shader.SetVector3($"pointList[{i}].lightColor", pointlightList[i].color.Xyz);
                    _shader.SetFloat($"pointList[{i}].ambientStre", pointlightList[i].ambientStrength);
                    _shader.SetFloat($"pointList[{i}].specStre", pointlightList[i].specStrength);
                }
                for (int i = 0; i < spotlightList.Count; i++)
                {
                    //_shader.SetVector3($"spotList[{i}].lightPos", spotlightList[i].objectCenter);
                    _shader.SetVector3($"spotList[{i}].lightPos", cameraPos);
                    _shader.SetVector3($"spotList[{i}].lightColor", spotlightList[i].color.Xyz);
                    _shader.SetFloat($"spotList[{i}].ambientStre", spotlightList[i].ambientStrength);
                    _shader.SetFloat($"spotList[{i}].specStre", spotlightList[i].specStrength);

                    _shader.SetVector3($"spotList[{i}].spotDir", -spotlightList[i].lightDirection);
                    _shader.SetFloat($"spotList[{i}].spotAngleCos", (float)MathHelper.Cos(MathHelper.DegreesToRadians(spotlightList[i].spotAngle)));
                }
            }

            if (indices.Count != 0)
            {
                switch (line)
                {
                    case 1:
                        GL.DrawElementsInstanced(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, (IntPtr)0, model.Count);
                        break;
                    case -1:
                        GL.DrawElementsInstanced(PrimitiveType.LineStrip, indices.Count, DrawElementsType.UnsignedInt, (IntPtr)0, model.Count);
                        break;
                }
            }
            else
            {
                switch (line)
                {
                    case 1:
                        GL.DrawArraysInstanced(PrimitiveType.Triangles, 0, vertices.Count, model.Count);
                        break;
                    case -1:
                        GL.DrawArraysInstanced(PrimitiveType.LineStrip, 0, vertices.Count, model.Count);
                        break;
                }
            }
        }


        #region solidObjects

        public void createCuboid(float x_, float y_, float z_, float length, bool useNormals, bool useTextures)
        {
            objectCenter = new Vector3(x_, y_, z_);

            var tempVertices = new List<Vector3>();
            Vector3 temp_vector;

            //Titik 1
            temp_vector.X = x_ - length / 2.0f;
            temp_vector.Y = y_ + length / 2.0f;
            temp_vector.Z = z_ - length / 2.0f;
            tempVertices.Add(temp_vector);

            //Titik 2
            temp_vector.X = x_ + length / 2.0f;
            temp_vector.Y = y_ + length / 2.0f;
            temp_vector.Z = z_ - length / 2.0f;
            tempVertices.Add(temp_vector);

            //Titik 3
            temp_vector.X = x_ - length / 2.0f;
            temp_vector.Y = y_ - length / 2.0f;
            temp_vector.Z = z_ - length / 2.0f;
            tempVertices.Add(temp_vector);

            //Titik 4
            temp_vector.X = x_ + length / 2.0f;
            temp_vector.Y = y_ - length / 2.0f;
            temp_vector.Z = z_ - length / 2.0f;
            tempVertices.Add(temp_vector);

            //Titik 5
            temp_vector.X = x_ - length / 2.0f;
            temp_vector.Y = y_ + length / 2.0f;
            temp_vector.Z = z_ + length / 2.0f;
            tempVertices.Add(temp_vector);

            //Titik 6
            temp_vector.X = x_ + length / 2.0f;
            temp_vector.Y = y_ + length / 2.0f;
            temp_vector.Z = z_ + length / 2.0f;
            tempVertices.Add(temp_vector);

            //Titik 7
            temp_vector.X = x_ - length / 2.0f;
            temp_vector.Y = y_ - length / 2.0f;
            temp_vector.Z = z_ + length / 2.0f;
            tempVertices.Add(temp_vector);

            //Titik 8
            temp_vector.X = x_ + length / 2.0f;
            temp_vector.Y = y_ - length / 2.0f;
            temp_vector.Z = z_ + length / 2.0f;
            tempVertices.Add(temp_vector);

            var tempIndices = new List<uint>
            {
				//Back
				1, 2, 0,
                2, 1, 3,
				
				//Top
				5, 0, 4,
                0, 5, 1,

				//Right
				5, 3, 1,
                3, 5, 7,

				//Left
				0, 6, 4,
                6, 0, 2,

				//Front
				4, 7, 5,
                7, 4, 6,

				//Bottom
				3, 6, 2,
                6, 3, 7
            };

            if (useNormals)
            {
                for (int i = 0; i < 6; i++)
                {
                    normals.Add(-Vector3.UnitZ);
                }

                for (int i = 0; i < 6; i++)
                {
                    normals.Add(Vector3.UnitY);
                }

                for (int i = 0; i < 6; i++)
                {
                    normals.Add(Vector3.UnitX);
                }

                for (int i = 0; i < 6; i++)
                {
                    normals.Add(-Vector3.UnitX);
                }

                for (int i = 0; i < 6; i++)
                {
                    normals.Add(Vector3.UnitZ);
                }

                for (int i = 0; i < 6; i++)
                {
                    normals.Add(-Vector3.UnitY);
                }
            }

            if (useTextures)
            {
                texCoords = new List<Vector3>()
                {
                    (0, 1.0f, 0),
                    (1.0f, 0, 0),
                    (1.0f, 1.0f, 0),
                    (1.0f, 0, 0),
                    (0, 1.0f, 0),
                    (0, 0, 0),

                    (1.0f, 0, 0),
                    (0, 1.0f, 0),
                    (0, 0, 0),
                    (0, 1.0f, 0),
                    (1.0f, 0, 0),
                    (1.0f, 1.0f, 0),

                    (0, 1.0f, 0),
                    (1.0f, 0, 0),
                    (1.0f, 1.0f, 0),
                    (1.0f, 0, 0),
                    (0, 1.0f, 0),
                    (0, 0, 0),

                    (0, 1.0f, 0),
                    (1.0f, 0, 0),
                    (1.0f, 1.0f, 0),
                    (1.0f, 0, 0),
                    (0, 1.0f, 0),
                    (0, 0, 0),

                    (0, 1.0f, 0),
                    (1.0f, 0, 0),
                    (1.0f, 1.0f, 0),
                    (1.0f, 0, 0),
                    (0, 1.0f, 0),
                    (0, 0, 0),

                    (1.0f, 0, 0),
                    (0, 1.0f, 0),
                    (0, 0, 0),
                    (0, 1.0f, 0),
                    (1.0f, 0, 0),
                    (1.0f, 1.0f, 0)
                };
            }

            if (useNormals || useTextures)
            {
                for (int i = 0; i < tempIndices.Count; i++)
                {
                    vertices.Add(tempVertices[(int)tempIndices[i]]);
                }
            }
            else
            {
                vertices = tempVertices;
                indices = tempIndices;
            }
        }

        #endregion

        #region transforms
        public void rotate(Vector3 pivot, Vector3 vector, float angle, int index)
        {
            var radAngle = MathHelper.DegreesToRadians(angle);

            var arbRotationMatrix = new Matrix4
                (
                new Vector4((float)(Math.Cos(radAngle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(radAngle))), (float)(vector.X * vector.Y * (1.0f - Math.Cos(radAngle)) + vector.Z * Math.Sin(radAngle)), (float)(vector.X * vector.Z * (1.0f - Math.Cos(radAngle)) - vector.Y * Math.Sin(radAngle)), 0),
                new Vector4((float)(vector.X * vector.Y * (1.0f - Math.Cos(radAngle)) - vector.Z * Math.Sin(radAngle)), (float)(Math.Cos(radAngle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(radAngle))), (float)(vector.Y * vector.Z * (1.0f - Math.Cos(radAngle)) + vector.X * Math.Sin(radAngle)), 0),
                new Vector4((float)(vector.X * vector.Z * (1.0f - Math.Cos(radAngle)) + vector.Y * Math.Sin(radAngle)), (float)(vector.Y * vector.Z * (1.0f - Math.Cos(radAngle)) - vector.X * Math.Sin(radAngle)), (float)(Math.Cos(radAngle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(radAngle))), 0),
                Vector4.UnitW
                );

            model[index] *= Matrix4.CreateTranslation(-pivot);
            model[index] *= arbRotationMatrix;
            model[index] *= Matrix4.CreateTranslation(pivot);

            var newModel = Matrix4.Transpose(model[index]);
            GL.BindBuffer(BufferTarget.ShaderStorageBuffer, _shaderStorageBufferObjectModel);
            GL.BufferSubData(BufferTarget.ShaderStorageBuffer, (IntPtr)(index * Vector4.SizeInBytes * 4), Vector4.SizeInBytes * 4, ref newModel);

            if (normals.Count > 0)
            {
                var newNormalMat = Matrix4.Transpose(Matrix4.Invert(newModel));
                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, _shaderStorageBufferObjectNormal);
                GL.BufferSubData(BufferTarget.ShaderStorageBuffer, (IntPtr)(index * Vector4.SizeInBytes * 4), Vector4.SizeInBytes * 4, ref newNormalMat);
            }
            Console.WriteLine(GL.GetError());

            GL.BindBuffer(BufferTarget.ShaderStorageBuffer, 0);

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

        public void translate(float x, float y, float z, int index)
        {
            model[index] *= Matrix4.CreateTranslation(x, y, z);

            var newModel = Matrix4.Transpose(model[index]);
            GL.BindBuffer(BufferTarget.ShaderStorageBuffer, _shaderStorageBufferObjectModel);
            GL.BufferSubData(BufferTarget.ShaderStorageBuffer, (IntPtr)(index * Vector4.SizeInBytes * 4), Vector4.SizeInBytes * 4, ref newModel);

            if (normals.Count > 0)
            {
                var newNormalMat = Matrix4.Transpose(Matrix4.Invert(newModel));
                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, _shaderStorageBufferObjectNormal);
                GL.BufferSubData(BufferTarget.ShaderStorageBuffer, (IntPtr)(index * Vector4.SizeInBytes * 4), Vector4.SizeInBytes * 4, ref newNormalMat);
            }

            GL.BindBuffer(BufferTarget.ShaderStorageBuffer, 0);

            objectCenter.X += x;
            objectCenter.Y += y;
            objectCenter.Z += z;
        }

        public void scale(float scaleX, float scaleY, float scaleZ, Vector3 scaleCenter, int index)
        {
            model[index] *= Matrix4.CreateTranslation(-scaleCenter);
            model[index] *= Matrix4.CreateScale(scaleX, scaleY, scaleZ);
            model[index] *= Matrix4.CreateTranslation(scaleCenter);
        }
        #endregion
    }
}
