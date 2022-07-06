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
    class freedys
    {
        /*Lawnmower lawnmower1 = new Lawnmower();
        Lawnmower lawnmower2 = new Lawnmower();
        Lawnmower lawnmower3 = new Lawnmower();*/

        //string path_fr = "../../../freedy/source/Freddy.obj";

        // Head
        string path_head = "../../../freedyfix/kepala.obj";
        string path_eye = "../../../freedyfix/mata.obj";
        string path_Lalis = "../../../freedyfix/alis_kiri.obj";
        string path_Ralis = "../../../freedyfix/alis_kanan.obj";
        string path_hidung = "../../../freedyfix/hidung.obj";
        string path_gigi = "../../../freedyfix/gigi.obj";
        string path_totol = "../../../freedyfix/totol.obj";
        string path_topi = "../../../freedyfix/topi.obj";


        // Body
        // Torso
        string path_upTorso = "../../../freedyfix/torso_atas.obj";
        string path_lowTorso = "../../../freedyfix/torso_bawah.obj";
        string path_tie = "../../../freedyfix/dasi.obj";

        // Arm
        string path_upArm = "../../../freedyfix/lengan_atas.obj";
        string path_lowArm = "../../../freedyfix/lengan_bawah.obj";
        string path_hand = "../../../freedyfix/tangan.obj";

        string path_rArm = "../../../freedyfix/lengan_kanan.obj";
        string path_Rhand = "../../../freedyfix/tangan_kanan.obj";

        // Leg
        string path_LupLeg = "../../../freedyfix/paha_kiri.obj";
        string path_LlowLeg = "../../../freedyfix/betis_kiri.obj";
        string path_Lfoot = "../../../freedyfix/kaki_kiri.obj";

        string path_RupLeg = "../../../freedyfix/paha_kanan.obj";
        string path_RlowLeg = "../../../freedyfix/betis_kanan.obj";
        string path_Rfoot = "../../../freedyfix/kaki_kanan.obj";

        //mesh fr = new mesh();

        mesh head = new mesh();
        mesh eye = new mesh();
        mesh Lalis = new mesh();
        mesh Ralis = new mesh();
        mesh hidung = new mesh();
        mesh gigi = new mesh();
        mesh totol = new mesh();
        mesh topi = new mesh();

        mesh upTorso = new mesh();
        mesh lowTorso = new mesh();
        mesh tie = new mesh();

        mesh upArm = new mesh();
        mesh lowArm = new mesh();
        mesh hand = new mesh();

        mesh rArm = new mesh();
        mesh Rhand = new mesh();

        mesh LupLeg = new mesh();
        mesh LlowLeg = new mesh();
        mesh Lfoot = new mesh();

        mesh RupLeg = new mesh();
        mesh RlowLeg = new mesh();
        mesh Rfoot = new mesh();


        public freedys()
        {

        }


        public void load()
        {
            //fr.initialize(path_fr);



            head.initialize(path_head);
            eye.initialize(path_eye);
            Lalis.initialize(path_Lalis);
            Ralis.initialize(path_Ralis);
            hidung.initialize(path_hidung);
            gigi.initialize(path_gigi);
            totol.initialize(path_totol);
            topi.initialize(path_topi);

            upTorso.initialize(path_upTorso);
            lowTorso.initialize(path_lowTorso);
            tie.initialize(path_tie);

            upArm.initialize(path_upArm);
            lowArm.initialize(path_lowArm);
            hand.initialize(path_hand);

            rArm.initialize(path_rArm);
            Rhand.initialize(path_Rhand);

            LupLeg.initialize(path_LupLeg);
            LlowLeg.initialize(path_LlowLeg);
            Lfoot.initialize(path_Lfoot);

            RupLeg.initialize(path_RupLeg);
            RlowLeg.initialize(path_RlowLeg);
            Rfoot.initialize(path_Rfoot);


        }
        public void move(Vector3 _pos)
        {
            //fr.translate(_pos.X, _pos.Y, _pos.Z);

            head.translate(_pos.X, _pos.Y, _pos.Z);
            eye.translate(_pos.X, _pos.Y, _pos.Z);
            Lalis.translate(_pos.X, _pos.Y, _pos.Z);
            Ralis.translate(_pos.X, _pos.Y, _pos.Z);
            hidung.translate(_pos.X, _pos.Y, _pos.Z);
            gigi.translate(_pos.X, _pos.Y, _pos.Z);
            totol.translate(_pos.X, _pos.Y, _pos.Z);
            topi.translate(_pos.X, _pos.Y, _pos.Z);

            upTorso.translate(_pos.X, _pos.Y, _pos.Z);
            lowTorso.translate(_pos.X, _pos.Y, _pos.Z);
            tie.translate(_pos.X, _pos.Y, _pos.Z);

            upArm.translate(_pos.X, _pos.Y, _pos.Z);
            lowArm.translate(_pos.X, _pos.Y, _pos.Z);
            hand.translate(_pos.X, _pos.Y, _pos.Z);

            rArm.translate(_pos.X, _pos.Y, _pos.Z);
            Rhand.translate(_pos.X, _pos.Y, _pos.Z);

            LupLeg.translate(_pos.X, _pos.Y, _pos.Z);
            LlowLeg.translate(_pos.X, _pos.Y, _pos.Z);
            Lfoot.translate(_pos.X, _pos.Y, _pos.Z);

            RupLeg.translate(_pos.X, _pos.Y, _pos.Z);
            RlowLeg.translate(_pos.X, _pos.Y, _pos.Z);
            Rfoot.translate(_pos.X, _pos.Y, _pos.Z);


        }


        public void rotate(float dr)
        {
            head.rotate(dr);
            eye.rotate(dr);
            Lalis.rotate(dr);
            Ralis.rotate(dr);
            hidung.rotate(dr);
            gigi.rotate(dr);
            totol.rotate(dr);
            topi.rotate(dr);

            upTorso.rotate(dr);
            lowTorso.rotate(dr);
            tie.rotate(dr);

            upArm.rotate(dr);
            lowArm.rotate(dr);
            hand.rotate(dr);

            rArm.rotate(dr);
            Rhand.rotate(dr);

            LupLeg.rotate(dr);
            LlowLeg.rotate(dr);
            Lfoot.rotate(dr);

            RupLeg.rotate(dr);
            RlowLeg.rotate(dr);
            Rfoot.rotate(dr);
        }


        public void Render_object(Camera _camera, List<List<mesh>> all_lights)
        {

            //fr.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);

            head.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);
            eye.Render(_camera, new Vector3(1.0f, 1.0f, 1.0f), all_lights);
            Lalis.Render(_camera, new Vector3(0.150f, 0.128f, 0.096f), all_lights);
            Ralis.Render(_camera, new Vector3(0.150f, 0.128f, 0.096f), all_lights);
            hidung.Render(_camera, new Vector3(0.0f, 0.0f, 0.0f), all_lights);
            gigi.Render(_camera, new Vector3(1.0f, 1.0f, 1.0f), all_lights);
            totol.Render(_camera, new Vector3(0.150f, 0.128f, 0.096f), all_lights);
            topi.Render(_camera, new Vector3(0.0f, 0.0f, 0.0f), all_lights);

            upTorso.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);
            lowTorso.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);
            tie.Render(_camera, new Vector3(0.0f, 0.0f, 0.0f), all_lights);

            upArm.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);
            lowArm.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);
            hand.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);

            rArm.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);
            Rhand.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);

            LupLeg.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);
            LlowLeg.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);
            Lfoot.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);

            RupLeg.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);
            RlowLeg.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);
            Rfoot.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);


        }

    }
}
