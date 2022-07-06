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
    class environment
    {

        string path_tembok1 = "../../../freedyMap/tembok1.obj";

        string path_tembok2 = "../../../freedyMap/tembok2.obj";
        string path_tembok3 = "../../../freedyMap/tembok3.obj";
        string path_tembok4 = "../../../freedyMap/tembok4.obj";
        string path_tembok5 = "../../../freedyMap/tembok5.obj";
        string path_tembok6 = "../../../freedyMap/tembok6.obj";
        string path_tembok7 = "../../../freedyMap/tembok7.obj";
        string path_tembok8 = "../../../freedyMap/tembok8.obj";
        string path_tembok9 = "../../../freedyMap/tembok9.obj";
        string path_tembok10 = "../../../freedyMap/tembok10.obj";
        string path_tembok11 = "../../../freedyMap/tembok11.obj";
        string path_tembok12 = "../../../freedyMap/tembok12.obj";
        string path_tembok13 = "../../../freedyMap/tembok13.obj";
        string path_tembok14 = "../../../freedyMap/tembok14.obj";
        string path_tembok15 = "../../../freedyMap/tembok15.obj";
        string path_tembok16 = "../../../freedyMap/tembok16.obj";
        string path_tembok17 = "../../../freedyMap/tembok17.obj";
        string path_tembok18 = "../../../freedyMap/tembok18.obj";
        string path_tembok19 = "../../../freedyMap/tembok19.obj";
        string path_tembok20 = "../../../freedyMap/tembok20.obj";
        string path_tembok21 = "../../../freedyMap/tembok21.obj";
        string path_tembok22 = "../../../freedyMap/tembok22.obj";
        string path_tembok23 = "../../../freedyMap/tembok23.obj";
        string path_tembok24 = "../../../freedyMap/tembok24.obj";
        string path_tembok25 = "../../../freedyMap/tembok25.obj";
        string path_tembok26 = "../../../freedyMap/tembok26.obj";
        string path_tembok27 = "../../../freedyMap/tembok27.obj";
        string path_tembok28 = "../../../freedyMap/tembok28.obj";
        string path_tembok29 = "../../../freedyMap/tembok29.obj";
        string path_tembok30 = "../../../freedyMap/tembok30.obj";
        string path_tembok31 = "../../../freedyMap/tembok31.obj";
        string path_tembok32 = "../../../freedyMap/tembok32.obj";
        string path_tembok33 = "../../../freedyMap/tembok33.obj";

        string path_floor = "../../../freedyMap/lantai1.obj";
        string path_floor2 = "../../../freedyMap/lantai2.obj";
        string path_floor3 = "../../../freedyMap/lantai3.obj";

        string path_atap = "../../../freedyMap/atap.obj";
        mesh tembok1 = new mesh();
        mesh tembok2 = new mesh();
        mesh tembok3 = new mesh();
        mesh tembok4 = new mesh();
        mesh tembok5 = new mesh();
        mesh tembok6 = new mesh();
        mesh tembok7 = new mesh();
        mesh tembok8 = new mesh();
        mesh tembok9 = new mesh();
        mesh tembok10 = new mesh();
        mesh tembok11 = new mesh();
        mesh tembok12 = new mesh();
        mesh tembok13 = new mesh();
        mesh tembok14 = new mesh();
        mesh tembok15 = new mesh();
        mesh tembok16 = new mesh();
        mesh tembok17 = new mesh();
        mesh tembok18 = new mesh();
        mesh tembok19 = new mesh();
        mesh tembok20 = new mesh();
        mesh tembok21 = new mesh();
        mesh tembok22 = new mesh();
        mesh tembok23 = new mesh();
        mesh tembok24 = new mesh();
        mesh tembok25 = new mesh();
        mesh tembok26 = new mesh();
        mesh tembok27 = new mesh();
        mesh tembok28 = new mesh();
        mesh tembok29 = new mesh();
        mesh tembok30 = new mesh();
        mesh tembok31 = new mesh();
        mesh tembok32 = new mesh();
        mesh tembok33 = new mesh();

        mesh floor = new mesh();
        mesh floor2 = new mesh();
        mesh floor3 = new mesh();

        mesh atap = new mesh();

        // Stage - Panggung
        string path_SunStage = "../../../freedyMap/stage/matahari.obj";
        string path_Cloud = "../../../freedyMap/stage/awan.obj";
        string path_curtain = "../../../freedyMap/stage/tirai.obj";
        string path_panggung = "../../../freedyMap/stage/panggung.obj";
        mesh sunStage = new mesh();
        mesh cloud = new mesh();
        mesh curtain = new mesh();
        mesh panggung = new mesh();


        // Office
        string path_kertas = "../../../freedyMap/office/kertas.obj";
        mesh kertas = new mesh();

        // Office - Kue
        string path_kue = "../../../freedyMap/office/kue.obj";
        string path_lilin = "../../../freedyMap/office/lilin.obj";
        string path_badanKue = "../../../freedyMap/office/badan_kue.obj";
        string path_mataKue = "../../../freedyMap/office/mata_kue.obj";
        string path_mejaoffice = "../../../freedyMap/office/mejaoffice.obj";
        mesh kue = new mesh();
        mesh lilin = new mesh();
        mesh badanKue = new mesh();
        mesh mataKue = new mesh();
        mesh mejaOffice = new mesh();



        // Main Hall
        string path_mejaHall1 = "../../../freedyMap/mainHall/meja1.obj";
        string path_mejaHall2 = "../../../freedyMap/mainHall/meja2.obj";
        string path_mejaHall3 = "../../../freedyMap/mainHall/meja3.obj";
        string path_mejaHall4 = "../../../freedyMap/mainHall/meja4.obj";
        string path_mejaHall5 = "../../../freedyMap/mainHall/meja5.obj";
        string path_mejaHall6 = "../../../freedyMap/mainHall/meja6.obj";
        string path_mejaHall7 = "../../../freedyMap/mainHall/meja7.obj";
        string path_mejaHall8 = "../../../freedyMap/mainHall/meja8.obj";
        string path_mejaHall9 = "../../../freedyMap/mainHall/meja9.obj";
        string path_papanHall = "../../../freedyMap/mainHall/Papan.obj";
        mesh mejaHall1 = new mesh();
        mesh mejaHall2 = new mesh();
        mesh mejaHall3 = new mesh();
        mesh mejaHall4 = new mesh();
        mesh mejaHall5 = new mesh();
        mesh mejaHall6 = new mesh();
        mesh mejaHall7 = new mesh();
        mesh mejaHall8 = new mesh();
        mesh mejaHall9 = new mesh();
        mesh papanHall = new mesh();


        // Near Office
        string path_Lemari = "../../../freedyMap/nearOffStuff.obj";
        mesh lemari_near = new mesh();


        // Storage
        string path_lemariGudang = "../../../freedyMap/storage/lemariGudang.obj";
        string path_lemariGudang1 = "../../../freedyMap/storage/lemariGudang1.obj";
        string path_mejaGudang = "../../../freedyMap/storage/mejaGudang.obj";
        mesh lemari_gudang = new mesh();
        mesh lemari_gudang1 = new mesh();
        mesh meja_gudang = new mesh();

        // Storage Item
        string path_endoHead = "../../../freedyMap/storage/Endoskeleton/mata_gigi.obj";
        string path_endoBody = "../../../freedyMap/storage/Endoskeleton/badan.obj";
        mesh endoHead = new mesh();
        mesh endoBody = new mesh();

        string path_freedy = "../../../freedyMap/storage/Kepala_Freddy/freddy_kepala.obj";
        string path_Fbl = "../../../freedyMap/storage/Kepala_Freddy/freddy_hitam.obj";
        string path_Wbl = "../../../freedyMap/storage/Kepala_Freddy/freddy_putih.obj";
        mesh freedy = new mesh();
        mesh Fbl = new mesh();
        mesh Wbl = new mesh();

        string path_chica_kepala = "../../../freedyMap/storage/Chica/kepala.obj";
        string path_chica_mata = "../../../freedyMap/storage/Chica/mata.obj";
        string path_chica_paruh = "../../../freedyMap/storage/Chica/patuh.obj";
        string path_chica_alis = "../../../freedyMap/storage/Chica/alis_kelopak.obj";
        mesh chica = new mesh();
        mesh chicaMata = new mesh();
        mesh chicaParuh = new mesh();
        mesh chicaALis = new mesh();

        string path_bonnie_kepala = "../../../freedyMap/storage/Bonnie/kepala.obj";
        string path_bonnie_kelopak = "../../../freedyMap/storage/Bonnie/kelopak_hidung.obj";
        string path_bonnie_gigi = "../../../freedyMap/storage/Bonnie/gigi_mata.obj";
        mesh bonnie = new mesh();
        mesh bonnieKe = new mesh();
        mesh bonnieGi = new mesh();


        public environment()
        {

        }
        public List<mesh> all_mesh()
        {
            List<mesh> list_mesh = new List<mesh>();
            list_mesh.Add(tembok1);
            list_mesh.Add(tembok2);
            list_mesh.Add(tembok3);
            list_mesh.Add(tembok4);
            list_mesh.Add(tembok5);
            list_mesh.Add(tembok6);
            //list_mesh.Add(tembok7);
            list_mesh.Add(tembok8);
            list_mesh.Add(tembok9);
            list_mesh.Add(tembok10);
            list_mesh.Add(tembok11);
            list_mesh.Add(tembok12);
            list_mesh.Add(tembok13);
            list_mesh.Add(tembok14);
            list_mesh.Add(tembok15);
            list_mesh.Add(tembok16);
            list_mesh.Add(tembok17);
            list_mesh.Add(tembok18);
            list_mesh.Add(tembok19);
            //list_mesh.Add(tembok20);
            list_mesh.Add(tembok21);
            list_mesh.Add(tembok22);
            list_mesh.Add(tembok23);
            list_mesh.Add(tembok24);
            //list_mesh.Add(tembok25);
            list_mesh.Add(tembok26);
            list_mesh.Add(tembok27);
            list_mesh.Add(tembok28);
            list_mesh.Add(tembok29);
            list_mesh.Add(tembok30);
            list_mesh.Add(tembok31);
            list_mesh.Add(tembok32);
            list_mesh.Add(tembok33);
            //list_mesh.Add(floor);
            //list_mesh.Add(atap);

            list_mesh.Add(sunStage);
            list_mesh.Add(cloud);
            list_mesh.Add(curtain);
            list_mesh.Add(panggung);

            list_mesh.Add(mejaHall1);
            list_mesh.Add(mejaHall2);
            list_mesh.Add(mejaHall3);
            list_mesh.Add(mejaHall4);
            list_mesh.Add(mejaHall5);
            list_mesh.Add(mejaHall6);
            list_mesh.Add(mejaHall7);
            list_mesh.Add(mejaHall8);
            list_mesh.Add(mejaHall9);

            list_mesh.Add(mejaOffice);
            list_mesh.Add(kertas);
            list_mesh.Add(kue);
            list_mesh.Add(badanKue);
            list_mesh.Add(lilin);
            list_mesh.Add(mataKue);

            list_mesh.Add(lemari_near);
            //list_mesh.Add(mejaHall);
            list_mesh.Add(papanHall);
            list_mesh.Add(mataKue);

            list_mesh.Add(lemari_gudang);
            //list_mesh.Add(lemari_gudang1);
            list_mesh.Add(meja_gudang);

            /* list_mesh.Add(endoBody);
             list_mesh.Add(endoHead);*/

            /*list_mesh.Add(freedy);
            list_mesh.Add(Fbl);
            list_mesh.Add(Wbl);

            list_mesh.Add(chica);
            list_mesh.Add(chicaALis);
            list_mesh.Add(chicaMata);
            list_mesh.Add(chicaParuh);

            list_mesh.Add(bonnie);
            list_mesh.Add(bonnieKe);
            list_mesh.Add(bonnieGi);*/

            return list_mesh;

        }


        public void load()
        {

            tembok1.initialize(path_tembok1);
            tembok2.initialize(path_tembok2);
            tembok3.initialize(path_tembok3);
            tembok4.initialize(path_tembok4);
            tembok5.initialize(path_tembok5);
            tembok6.initialize(path_tembok6);
            tembok7.initialize(path_tembok7);
            tembok8.initialize(path_tembok8);
            tembok9.initialize(path_tembok9);
            tembok10.initialize(path_tembok10);
            tembok11.initialize(path_tembok11);
            tembok12.initialize(path_tembok12);
            tembok13.initialize(path_tembok13);
            tembok14.initialize(path_tembok14);
            tembok15.initialize(path_tembok15);
            tembok16.initialize(path_tembok16);
            tembok17.initialize(path_tembok17);
            tembok18.initialize(path_tembok18);
            tembok19.initialize(path_tembok19);
            tembok20.initialize(path_tembok20);
            tembok21.initialize(path_tembok21);
            tembok22.initialize(path_tembok22);
            tembok23.initialize(path_tembok23);
            tembok24.initialize(path_tembok24);
            tembok25.initialize(path_tembok25);
            tembok26.initialize(path_tembok26);
            tembok27.initialize(path_tembok27);
            tembok28.initialize(path_tembok28);
            tembok29.initialize(path_tembok29);
            tembok30.initialize(path_tembok30);
            tembok31.initialize(path_tembok31);
            tembok32.initialize(path_tembok32);
            tembok33.initialize(path_tembok33);


            floor.initialize(path_floor);
            floor2.initialize(path_floor2);
            floor3.initialize(path_floor3);


            atap.initialize(path_atap);


            // Stage - Panggung
            sunStage.initialize(path_SunStage);
            cloud.initialize(path_Cloud);
            curtain.initialize(path_curtain);
            panggung.initialize(path_panggung);


            // Office
            kertas.initialize(path_kertas);
            mejaOffice.initialize(path_mejaoffice);

            // Office  - Kue
            kue.initialize(path_kue);
            badanKue.initialize(path_badanKue);
            lilin.initialize(path_lilin);
            mataKue.initialize(path_mataKue);


            // Near Office
            lemari_near.initialize(path_Lemari);


            // Main Hall
            mejaHall1.initialize(path_mejaHall1);
            mejaHall2.initialize(path_mejaHall2);
            mejaHall3.initialize(path_mejaHall3);
            mejaHall4.initialize(path_mejaHall4);
            mejaHall5.initialize(path_mejaHall5);
            mejaHall6.initialize(path_mejaHall6);
            mejaHall7.initialize(path_mejaHall7);
            mejaHall8.initialize(path_mejaHall8);
            mejaHall9.initialize(path_mejaHall9);
            papanHall.initialize(path_papanHall);


            // Storage
            lemari_gudang.initialize(path_lemariGudang);
            lemari_gudang1.initialize(path_lemariGudang1);
            meja_gudang.initialize(path_mejaGudang);

            // Storage Item
            endoHead.initialize(path_endoHead);
            endoBody.initialize(path_endoBody);

            freedy.initialize(path_freedy);
            Fbl.initialize(path_Fbl);
            Wbl.initialize(path_Wbl);

            chica.initialize(path_chica_kepala);
            chicaALis.initialize(path_chica_alis);
            chicaMata.initialize(path_chica_mata);
            chicaParuh.initialize(path_chica_paruh);

            bonnie.initialize(path_bonnie_kepala);
            bonnieKe.initialize(path_bonnie_kelopak);
            bonnieGi.initialize(path_bonnie_gigi);


        }

        public void move(Vector3 _pos, List<mesh> all_object)
        {
            tembok1.translate(_pos.X, _pos.Y, _pos.Z);

            tembok2.translate(_pos.X, _pos.Y, _pos.Z);

            tembok3.translate(_pos.X, _pos.Y, _pos.Z);

            tembok4.translate(_pos.X, _pos.Y, _pos.Z);
            tembok5.translate(_pos.X, _pos.Y, _pos.Z);
            tembok6.translate(_pos.X, _pos.Y, _pos.Z);
            tembok7.translate(_pos.X, _pos.Y, _pos.Z);
            tembok8.translate(_pos.X, _pos.Y, _pos.Z);
            tembok9.translate(_pos.X, _pos.Y, _pos.Z);
            tembok10.translate(_pos.X, _pos.Y, _pos.Z);
            tembok11.translate(_pos.X, _pos.Y, _pos.Z);
            tembok12.translate(_pos.X, _pos.Y, _pos.Z);
            tembok13.translate(_pos.X, _pos.Y, _pos.Z);
            tembok14.translate(_pos.X, _pos.Y, _pos.Z);
            tembok15.translate(_pos.X, _pos.Y, _pos.Z);
            tembok16.translate(_pos.X, _pos.Y, _pos.Z);
            tembok17.translate(_pos.X, _pos.Y, _pos.Z);
            tembok18.translate(_pos.X, _pos.Y, _pos.Z);
            tembok19.translate(_pos.X, _pos.Y, _pos.Z);
            tembok20.translate(_pos.X, _pos.Y, _pos.Z);
            tembok21.translate(_pos.X, _pos.Y, _pos.Z);
            tembok22.translate(_pos.X, _pos.Y, _pos.Z);
            tembok23.translate(_pos.X, _pos.Y, _pos.Z);
            tembok24.translate(_pos.X, _pos.Y, _pos.Z);
            tembok25.translate(_pos.X, _pos.Y, _pos.Z);
            tembok26.translate(_pos.X, _pos.Y, _pos.Z);
            tembok27.translate(_pos.X, _pos.Y, _pos.Z);
            tembok28.translate(_pos.X, _pos.Y, _pos.Z);
            tembok29.translate(_pos.X, _pos.Y, _pos.Z);
            tembok30.translate(_pos.X, _pos.Y, _pos.Z);
            tembok31.translate(_pos.X, _pos.Y, _pos.Z);
            tembok32.translate(_pos.X, _pos.Y, _pos.Z);
            tembok33.translate(_pos.X, _pos.Y, _pos.Z);


            floor.translate(_pos.X, _pos.Y, _pos.Z);
            floor2.translate(_pos.X, _pos.Y, _pos.Z);
            floor3.translate(_pos.X, _pos.Y, _pos.Z);


            atap.translate(_pos.X, _pos.Y, _pos.Z);

            // Stage - Panggung
            sunStage.translate(_pos.X, _pos.Y, _pos.Z);
            cloud.translate(_pos.X, _pos.Y, _pos.Z);
            curtain.translate(_pos.X, _pos.Y, _pos.Z);
            panggung.translate(_pos.X, _pos.Y, _pos.Z);


            // Office
            kertas.translate(_pos.X, _pos.Y, _pos.Z);
            mejaOffice.translate(_pos.X, _pos.Y, _pos.Z);

            // Office - Kue
            kue.translate(_pos.X, _pos.Y, _pos.Z);
            badanKue.translate(_pos.X, _pos.Y, _pos.Z);
            mataKue.translate(_pos.X, _pos.Y, _pos.Z);
            lilin.translate(_pos.X, _pos.Y, _pos.Z);


            // Near Office
            lemari_near.translate(_pos.X, _pos.Y, _pos.Z);


            // Main Hall
            mejaHall1.translate(_pos.X, _pos.Y, _pos.Z);
            mejaHall2.translate(_pos.X, _pos.Y, _pos.Z);
            mejaHall3.translate(_pos.X, _pos.Y, _pos.Z);
            mejaHall4.translate(_pos.X, _pos.Y, _pos.Z);
            mejaHall5.translate(_pos.X, _pos.Y, _pos.Z);
            mejaHall6.translate(_pos.X, _pos.Y, _pos.Z);
            mejaHall7.translate(_pos.X, _pos.Y, _pos.Z);
            mejaHall8.translate(_pos.X, _pos.Y, _pos.Z);
            mejaHall9.translate(_pos.X, _pos.Y, _pos.Z);
            papanHall.translate(_pos.X, _pos.Y, _pos.Z);


            // Storage
            lemari_gudang.translate(_pos.X, _pos.Y, _pos.Z);
            lemari_gudang1.translate(_pos.X, _pos.Y, _pos.Z);
            meja_gudang.translate(_pos.X, _pos.Y, _pos.Z);

            // Storage Item
            endoHead.translate(_pos.X, _pos.Y, _pos.Z);
            endoBody.translate(_pos.X, _pos.Y, _pos.Z);

            freedy.translate(_pos.X, _pos.Y, _pos.Z);
            Fbl.translate(_pos.X, _pos.Y, _pos.Z);
            Wbl.translate(_pos.X, _pos.Y, _pos.Z);

            chica.translate(_pos.X, _pos.Y, _pos.Z);
            chicaALis.translate(_pos.X, _pos.Y, _pos.Z);
            chicaMata.translate(_pos.X, _pos.Y, _pos.Z);
            chicaParuh.translate(_pos.X, _pos.Y, _pos.Z);

            bonnie.translate(_pos.X, _pos.Y, _pos.Z);
            bonnieKe.translate(_pos.X, _pos.Y, _pos.Z);
            bonnieGi.translate(_pos.X, _pos.Y, _pos.Z);

        }

        public void Render_object(Camera _camera, List<List<mesh>> all_lights)
        {

            tembok1.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok2.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);

            tembok3.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);

            tembok4.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok5.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok6.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok7.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok8.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok9.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok10.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok11.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok12.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok13.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok14.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok15.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok16.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok17.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok18.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok19.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok20.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok21.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok22.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok23.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok24.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok25.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok26.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok27.Render(_camera, new Vector3(0.95f, 0.593f, 0.222f), all_lights); // tembok kanan di hall kanan dari office
            tembok28.Render(_camera, new Vector3(0.95f, 0.593f, 0.222f), all_lights); // tembok kanan di hall kanan dari office
            tembok29.Render(_camera, new Vector3(0.95f, 0.593f, 0.222f), all_lights); // tembok tepat disamping office
            tembok30.Render(_camera, new Vector3(1f, 1f, 1f), all_lights); // tembok office depan
            tembok31.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            tembok32.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);
            tembok33.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);

            floor.Render(_camera, new Vector3(0.98f, 0.165f, 0.062f), all_lights); // merah
            floor2.Render(_camera, new Vector3(0, 0.667f, 0.98f), all_lights); // biru
            floor3.Render(_camera, new Vector3(1f, 1f, 1f), all_lights); // putih

            atap.Render(_camera, new Vector3(0.6f, 0.6f, 0.6f), all_lights);


            // Stage - Panggung
            sunStage.Render(_camera, new Vector3(0.865f, 0.842f, 0.162f), all_lights);
            cloud.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            curtain.Render(_camera, new Vector3(0.638f, 0.192f, 0.805f), all_lights);
            panggung.Render(_camera, new Vector3(0.017f, 0.483f, 0.99f), all_lights);


            // Office
            kertas.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            mejaOffice.Render(_camera, new Vector3(0.548f, 0.565f, 0.542f), all_lights);
            // Office-Kue
            kue.Render(_camera, new Vector3(0.913f, 0.028f, 0.95f), all_lights);
            badanKue.Render(_camera, new Vector3(0.95f, 0.593f, 0.222f), all_lights);
            mataKue.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            lilin.Render(_camera, new Vector3(0.033f, 0.950f, 0.094f), all_lights);


            // Near Office
            lemari_near.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);


            // Main Hall
            mejaHall1.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            mejaHall2.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            mejaHall3.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            mejaHall4.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            mejaHall5.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            mejaHall6.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            mejaHall7.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            mejaHall8.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            mejaHall9.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            papanHall.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);


            // Storage
            lemari_gudang.Render(_camera, new Vector3(0.21f, 0.21f, 0.21f), all_lights);
            lemari_gudang1.Render(_camera, new Vector3(0.21f, 0.21f, 0.21f), all_lights);
            meja_gudang.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);

            // Storage Item
            endoHead.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            endoBody.Render(_camera, new Vector3(0.235f, 0.235f, 0.235f), all_lights);

            freedy.Render(_camera, new Vector3(0.370f, 0.316f, 0.238f), all_lights);
            Fbl.Render(_camera, new Vector3(0f, 0f, 0f), all_lights);
            Wbl.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);

            chica.Render(_camera, new Vector3(0.865f, 0.842f, 0.162f), all_lights);
            chicaALis.Render(_camera, new Vector3(0.01f, 0.01f, 0.01f), all_lights);
            chicaMata.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);
            chicaParuh.Render(_camera, new Vector3(0.98f, 0.606f, 0.014f), all_lights);

            bonnie.Render(_camera, new Vector3(0.362f, 0.417f, 0.83f), all_lights);
            bonnieKe.Render(_camera, new Vector3(0f, 0f, 0f), all_lights);
            bonnieGi.Render(_camera, new Vector3(1f, 1f, 1f), all_lights);

        }

    }
}
