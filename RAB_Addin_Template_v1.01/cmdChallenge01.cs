namespace RAB_Addin_Template_v1._01
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge01 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Your Module 01 Challenge code goes here
            // Declare variables
            int number = 250;
            double currentElevation = 0;
            double floorHeight = 15;

            using (Transaction tx = new Transaction(doc, "Create Levels and Views"))
            {
                tx.Start();

                for (int i = 1; i <= number; i++)
                {
                    // Create Level
                    Level level = Level.Create(doc, currentElevation);
                    level.Name = "Level " + i;

                    // Increment elevation
                    currentElevation += floorHeight;

                    // FIZZ_#: Floor Plan
                    if (i % 3 == 0 && i % 5 != 0)
                    {
                        CreatePlanView(doc, level, "FIZZ_" + i, ViewFamily.FloorPlan);
                    }

                    // BUZZ_#: Ceiling Plan
                    else if (i % 5 == 0 && i % 3 != 0)
                    {
                        CreatePlanView(doc, level, "BUZZ_" + i, ViewFamily.CeilingPlan);
                    }

                    // FIZZBUZZ_#: Sheet
                    else if (i % 3 == 0 && i % 5 == 0)
                    {
                        CreateSheet(doc, "FIZZBUZZ_" + i);
                    }
                }

                tx.Commit();
            }

            return Result.Succeeded;
        }

        private void CreatePlanView(Document doc, Level level, string viewName, ViewFamily viewFamily)
        {
            // Find the appropriate ViewFamilyType
            ViewFamilyType viewFamilyType = new FilteredElementCollector(doc)
                .OfClass(typeof(ViewFamilyType))
                .Cast<ViewFamilyType>()
                .FirstOrDefault(vft => vft.ViewFamily == viewFamily);

            if (viewFamilyType != null)
            {
                ViewPlan view = ViewPlan.Create(doc, viewFamilyType.Id, level.Id);
                view.Name = viewName;
            }
        }

        private void CreateSheet(Document doc, string sheetName)
        {
            // Find a title block family
            ElementId titleBlockId = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_TitleBlocks)
                .OfClass(typeof(FamilySymbol))
                .FirstElementId();

            if (titleBlockId != ElementId.InvalidElementId)
            {
                ViewSheet sheet = ViewSheet.Create(doc, titleBlockId);
                sheet.Name = sheetName;

                return Result.Succeeded;
            }
            internal static PushButtonData GetButtonData()
            {
                // use this method to define the properties for this command in the Revit ribbon
                string buttonInternalName = "btnChallenge01";
                string buttonTitle = "Module\r01";

                Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                    buttonInternalName,
                    buttonTitle,
                    MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                    Properties.Resources.Module01,
                    Properties.Resources.Module01,
                    "Module 01 Challenge");

                return myButtonData.Data;
            }
        }

    } }
