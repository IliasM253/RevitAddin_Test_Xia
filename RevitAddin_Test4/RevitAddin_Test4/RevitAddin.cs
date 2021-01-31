using System;
using System.Collections.Generic;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.ComponentModel;
using RevitAddin_Forms;
using IIB1_UE1_Gruppe05_AbgabeCP;
using IIB1_UE1_Gruppe05_AbgabeCP.Klassen;
using System.Linq;
using Autodesk.Revit.DB.Structure;
using ClassFloor = IIB1_UE1_Gruppe05_AbgabeCP.Klassen.Geschoss;
using ClassRoom = IIB1_UE1_Gruppe05_AbgabeCP.Klassen.Raum;
using RevitRoom = Autodesk.Revit.DB.Architecture.Room;

namespace RevitAddin_Test4
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class RevitAddin : IExternalCommand
    {
        // ModelessForm instance
        public static MainWindow _demoForm;
        private static Document _doc;
        private static UIDocument _uidoc;
        private static ExternalCommandData _ecd;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                _ecd = commandData;
                BindingList<Gebaeude> gebaeude = new BindingList<Gebaeude>();
                //List<Gebaeude> gebaeude = new List<Gebaeude>();
                UIApplication uiApp = commandData.Application;
                _uidoc = uiApp.ActiveUIDocument;
                _doc = Util.Doc = _uidoc.Document;

                gebaeude.Add(Util.parseGebaeude());

                if (_demoForm != null /*&& _demoForm.Visible*/)
                {
                    _demoForm.Close();
                    _demoForm = null;
                }

                ShowForm(gebaeude);
                return Autodesk.Revit.UI.Result.Succeeded;
            }
            catch (Exception e)
            {
                TaskDialog.Show("Revit", e.ToString());
                return Result.Failed;
            }
        }

        public void ShowForm(BindingList<Gebaeude> buildings)
        {
            //This method creates an instance of the Form for use with revit
            if (_demoForm == null /*|| _demoForm.IsDisposed*/)
            {
                //Create a updateHandler for updating the revit doc
                RoomInfoUpdater updateHandler = new RoomInfoUpdater();
                ExternalEvent updateEvent = ExternalEvent.Create(updateHandler);
                TelPlacer lampPlacer = new TelPlacer();
                ExternalEvent placeEvent = ExternalEvent.Create(lampPlacer);

                //Initialize Form
                _demoForm = new MainWindow(buildings,updateEvent,placeEvent);
                _demoForm.Show();
            }
        }


        #region Event Handler
        public class RoomInfoUpdater : IExternalEventHandler
        {
            // This class serves as the Eventhandler for the Revit API

            public static readonly string typeofuse = "Nutzungsgruppe DIN 277-2";

            public void Execute(UIApplication app)
            {
                //update(_demoForm.Buildingdetails._clr);
                update(_demoForm.MainRaum);
            }


            //Ist anders als in den vorherigen Versionen!!
            private static void update(ClassRoom _clr)
            {
                if (_clr != null)
                {
                    try
                    {
                        bool worked;
                        if (_clr.Id != null)
                        {
                            RevitRoom room = (RevitRoom)_doc.GetElement(_clr.Id);
                            using (Transaction trans = new Transaction(_doc))
                            {
                                if (trans.Start("Change Room Parameters") == TransactionStatus.Started)
                                {
                                    room.Name = _clr.Bez;
                                    worked = room.GetParameters(typeofuse).First().Set(idenfifyTyp(_clr));
                                    trans.Commit();
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        TaskDialog.Show("RoomInfoUpdater", e.Message);
                    }
                }
                else
                {
                    TaskDialog.Show("RoomInfoUpdater", "Please select a Room!");
                }
            }

            //Hier könnte noch Encode Mobiliar stehen!

            private static string idenfifyTyp(ClassRoom r)
            {
                return "2-Büroarbeit";
                //if (r is Office)
                //    return "2-Büroarbeit";
                //else if (r is Livingroom)
                //    return "1 - Wohnen und Aufenthalt";
                //else return "please extend the classdiagram";
            }

            public string GetName()
            {
                return "Update Room Information";
            }
        }

        public class TelPlacer : IExternalEventHandler
        {

            public void Execute(UIApplication app)
            {
                loadFamilyExample(_doc);
                //placeTel(_demoForm.Buildingdetails._clr);
                placeTel(_demoForm.MainRaum);
            }

            private void placeTel(ClassRoom clr)
            {
                XYZ locR = _ecd.Application.ActiveUIDocument.Selection.PickPoint("Pick a point to place Lamp");

                //RevitRoom rr = _doc.GetElement(clr.Identity) as RevitRoom;

                //XYZ p = ((LocationPoint)rr.Location).Point;

                //RevitRoom roomAtPoint = _doc.GetRoomAtPoint(p);

                //if (roomAtPoint.UniqueId != rr.UniqueId)
                //{
                //TaskDialog.Show("ERROR", "Punkt liegt nicht in ausgewählten Raum.");
                //return;
                //}
                if (null != locR)
                {
                    using (Transaction trans = new Transaction(_doc))
                    {
                        if (trans.Start("PlaceFamily") == TransactionStatus.Started)
                        {

                            FamilyInstance fi = _doc.Create.NewFamilyInstance(locR,
                                GetFamilySymbolByName(BuiltInCategory.OST_SpecialityEquipment, "Telefon")
                                , StructuralType.NonStructural);
                            trans.Commit();
                        }
                    }
                }
            }
            private static FamilySymbol GetFamilySymbolByName(BuiltInCategory bic, string name)
            {
                return new FilteredElementCollector(_doc).OfCategory(bic).OfClass(typeof(FamilySymbol))
                    .FirstOrDefault<Element>(e => e.Name.Equals(name)) as FamilySymbol;
            }

            public string GetName()
            {
                return "TelefonPlatzierer";

            }

            public void loadFamilyExample(Document doc)
            {
                Family family = null;
                FilteredElementCollector a = new FilteredElementCollector(doc).OfClass(typeof(Family));
                family = a.FirstOrDefault<Element>(e => e.Name.Equals("Leiter-02")) as Family;
                if (family == null) // Check if the Familytyp already loaded.
                {
                    string fileName = @"C:\ProgramData\Autodesk\RVT 2020\Libraries\Germany\Architektur - Bauteil\Sonderausstattung\vertikale Erschließung\Leiter-02.rfa";
                    using (Transaction t = new Transaction(doc))
                    {
                        if (t.Start("LoadFamily") == TransactionStatus.Started)
                        {
                            if (!doc.LoadFamily(fileName, out family))// try to load family 
                            {
                                throw new Exception("Unable to load " + fileName);
                            }
                            t.Commit();
                        }
                    }
                    // loop through family symbols
                    System.Collections.Generic.ISet<ElementId> familySymbolsId = family.GetFamilySymbolIds();
                    string symbolNames = "";
                    foreach (ElementId symbolId in familySymbolsId)
                    {
                        symbolNames += family.Name + " - " + ((FamilySymbol)
                       family.Document.GetElement(symbolId)).Name + "\n";
                    }
                    TaskDialog.Show("Loaded", symbolNames);
                }
                else TaskDialog.Show("Warning", "Family already loaded!");
            }
        }

        #endregion
    }
}
