using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace LightFast
{
    internal class ThirdPersonCamera
    {
        float yTotal = 0;
        public float xTotal = 0;
        float mouseSensitivity = 4f;

        float topAngle = 5;
        float BottomAngle = 0;

        public List<Vector3> _euler = new List<Vector3>();

        public ThirdPersonCamera()
        {
            resetEuler();
        }

        public Matrix4 generateArbRotationMatrix(Vector3 axis, Vector3 center, float degree, bool isEuler = false)
        {
            var rads = MathHelper.DegreesToRadians(degree);

            var secretFormula = new float[4, 4] {
                { (float)Math.Cos(rads) + (float)Math.Pow(axis.X, 2) * (1 - (float)Math.Cos(rads)), axis.X* axis.Y * (1 - (float)Math.Cos(rads)) - axis.Z * (float)Math.Sin(rads),    axis.X * axis.Z * (1 - (float)Math.Cos(rads)) + axis.Y * (float)Math.Sin(rads),   0 },
                { axis.Y * axis.X * (1 - (float)Math.Cos(rads)) + axis.Z * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Y, 2) * (1 - (float)Math.Cos(rads)), axis.Y * axis.Z * (1 - (float)Math.Cos(rads)) - axis.X * (float)Math.Sin(rads),   0 },
                { axis.Z * axis.X * (1 - (float)Math.Cos(rads)) - axis.Y * (float)Math.Sin(rads),   axis.Z * axis.Y * (1 - (float)Math.Cos(rads)) + axis.X * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Z, 2) * (1 - (float)Math.Cos(rads)), 0 },
                { 0, 0, 0, 1}
            };
            var secretFormulaMatix = new Matrix4
            (
                new Vector4(secretFormula[0, 0], secretFormula[0, 1], secretFormula[0, 2], secretFormula[0, 3]),
                new Vector4(secretFormula[1, 0], secretFormula[1, 1], secretFormula[1, 2], secretFormula[1, 3]),
                new Vector4(secretFormula[2, 0], secretFormula[2, 1], secretFormula[2, 2], secretFormula[2, 3]),
                new Vector4(secretFormula[3, 0], secretFormula[3, 1], secretFormula[3, 2], secretFormula[3, 3])
            );
            if (isEuler)
            {
                for (int i = 0; i < 3; i++)
                {
                    _euler[i] = Vector3.Normalize(getRotationResult(center, axis, rads, _euler[i], true));
                }
            }

            return secretFormulaMatix;
        }

        /*  public void rotate(Vector3 pivot, Vector3 vector, float angle)
          {
              var radAngle = MathHelper.DegreesToRadians(angle);

              var arbRotationMatrix = new Matrix4
                  (
                  new Vector4((float)(Math.Cos(radAngle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(radAngle))), (float)(vector.X * vector.Y * (1.0f - Math.Cos(radAngle)) + vector.Z * Math.Sin(radAngle)), (float)(vector.X * vector.Z * (1.0f - Math.Cos(radAngle)) - vector.Y * Math.Sin(radAngle)), 0),
                  new Vector4((float)(vector.X * vector.Y * (1.0f - Math.Cos(radAngle)) - vector.Z * Math.Sin(radAngle)), (float)(Math.Cos(radAngle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(radAngle))), (float)(vector.Y * vector.Z * (1.0f - Math.Cos(radAngle)) + vector.X * Math.Sin(radAngle)), 0),
                  new Vector4((float)(vector.X * vector.Z * (1.0f - Math.Cos(radAngle)) + vector.Y * Math.Sin(radAngle)), (float)(vector.Y * vector.Z * (1.0f - Math.Cos(radAngle)) - vector.X * Math.Sin(radAngle)), (float)(Math.Cos(radAngle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(radAngle))), 0),
                  Vector4.UnitW
                  );

              model *= Matrix4.CreateTranslation(-pivot);
              model *= arbRotationMatrix;
              model *= Matrix4.CreateTranslation(pivot);

              normalMat = Matrix4.Transpose(Matrix4.Invert(model));

              for (int i = 0; i < 3; i++)
              {
                  _euler[i] = Vector3.Normalize(getRotationResult(pivot, vector, radAngle, _euler[i], true));
              }

              objectCenter = getRotationResult(pivot, vector, radAngle, objectCenter);

              foreach (var i in child)
              {
                  i.rotate(pivot, vector, angle);
              }
          }*/

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

        public void HandleCameraRotation(Camera _camera, float dX, float dY, Vector3 ObjectCenter, Vector3 dir, float charaSpeed, bool _canMove, FrameEventArgs args)
        {
            float xInput = 0;
            float yInput = 0;
            var deltaX = dX * mouseSensitivity * (float)args.Time;
            var deltaY = dY * mouseSensitivity * (float)args.Time;
            //_camera.Yaw += deltaX * sensitivity;
            //_camera.Pitch -= deltaY * sensitivity;

            xInput = deltaX;
            xTotal += xInput;

            xTotal %= 360.0f;

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


            //rotate untuk radius x
            _camera.Position -= ObjectCenter;
            //_camera.Yaw += deltaX * sensitivity;
            _camera.Position = Vector3.Transform(_camera.Position,
                generateArbRotationMatrix(_euler[1], ObjectCenter, deltaX, true).ExtractRotation());
            _camera.Position += _camera.Up * (yInput / 2);
            _camera.Position += Vector3.Normalize(Vector3.Cross(_camera.Up, _camera.Right)) * (float)args.Time * (yInput);

            if (_canMove)
            {
                _camera.Position += dir * charaSpeed * (float)args.Time;
            }

            _camera.Position += ObjectCenter;





            _camera._front = -Vector3.Normalize(_camera.Position - ObjectCenter);
        }
        public void resetEuler()
        {
            _euler.Clear();
            _euler.Add(Vector3.UnitX);
            _euler.Add(Vector3.UnitY);
            _euler.Add(Vector3.UnitZ);
        }

    }
}
