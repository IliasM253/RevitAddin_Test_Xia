using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using ClassFloor = IIB1_UE1_Gruppe05_AbgabeCP.Klassen.Geschoss;
using ClassRoom = IIB1_UE1_Gruppe05_AbgabeCP.Klassen.Raum;
using IIB1_UE1_Gruppe05_AbgabeCP;
using IIB1_UE1_Gruppe05_AbgabeCP.Klassen;
using System.ComponentModel;
using RevitRoom = Autodesk.Revit.DB.Architecture.Room;

namespace RevitAddin_Test4
{
    class Util
    {
        private static Document _doc = null;
        public static Document Doc { get => _doc; set => _doc = value; }

        public static Gebaeude parseGebaeude()
        {
            ProjectInfo pi = _doc.ProjectInformation;
            Gebaeude gebaeude = new Gebaeude(pi.Name,pi.Address,new BindingList<ClassFloor>());
            gebaeude.ListGeschosse = getGeschosse();
            return gebaeude;
        }

        public static BindingList<ClassFloor> getGeschosse()
        {
            FilteredElementCollector collector = new FilteredElementCollector(_doc);
            ICollection<Element> floors = collector.OfClass(typeof(Level)).ToElements();
            BindingList<ClassFloor> cfls = new BindingList<ClassFloor>();
            foreach (Element fl in floors)
            {
                ClassFloor cfl = parseGeschoss((Level)fl);
                cfl.ListRaeume = getRaeume((Level)fl);

                if (cfl.ListRaeume.Count != 0)
                {
                    cfls.Add(cfl);
                }
            }
            return cfls;
        }

        public static ClassFloor parseGeschoss(Level floor)
        {
            ClassFloor cfl = new ClassFloor(floor.Name, new BindingList<ClassRoom>());
            cfl.Id = floor.Id.ToString();
            return cfl;
        }

        public static BindingList<ClassRoom> getRaeume(Level floor)
        {

            IEnumerable<Element> Roomsbylevel_filcol = new FilteredElementCollector(_doc) //search only in this level
            .OfClass(typeof(SpatialElement)).Where(room => room.GetType() == typeof(Autodesk.Revit.DB.Architecture.Room))  //get all rooms
            .Cast<SpatialElement>()  //cast results to SpatialElements
            .Where(o => o.LevelId == floor.Id); //search by the above mentioned Level

            BindingList<ClassRoom> crooms = new BindingList<ClassRoom>();
            foreach (SpatialElement r in Roomsbylevel_filcol)
            {
                ClassRoom croom = parseRaum((RevitRoom)r);
                if (croom != null)
                {
                    crooms.Add(croom);
                }
            }
            return crooms;
        }

        public static ClassRoom parseRaum(RevitRoom room)
        {
            //double area = squarefeetToMeter(room.Area);
            //string roomtyp = room.GetParameters("Nutzungsgruppe DIN 277-2")[0].AsString();

            //if (roomtyp == "2-Büroarbeit")
            //{
            //    Office office = new Office(room.UniqueId, room.Name, area);
            //    return office;
            //}
            //else if (roomtyp == "1-Wohnen und Aufenthalt")
            //{
            //    Livingroom live = new Livingroom(room.UniqueId, room.Name, area);
            //    return live;
            //}
            // return null;

            ClassRoom raum = new ClassRoom();
            raum.Flaeche = squarefeetToMeter(room.Area);   //Kontrolle Flächeneinheit m² oder squarefeet in revit?  - Ansonsten squarefeetToMeter() löschen 
            raum.Typ = room.GetParameters("Nutzungsgruppe DIN 277-2")[0].AsString();
            raum.Id = room.UniqueId;
            raum.InvMoebel = new BindingList<Mobiliar>(); //decodeMobiliar(room.GetParameters("Mobiliar")[0].AsString());     //"Mobibliar" ist vermutlich die Attributbezeichnung der Möbel in Revit
            raum.Bez = room.GetParameters("Name")[0].AsString();
            return raum;
        }

        public static double squarefeetToMeter(double squarefeet)
        {
            double quadratmeter = (squarefeet / 10.7639);
            return Math.Round(quadratmeter, 2);
        }

    }
}
