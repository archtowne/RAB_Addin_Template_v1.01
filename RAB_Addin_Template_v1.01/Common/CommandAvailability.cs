namespace RAB_Addin_Template_v1._01.Common
{
    internal class CommandAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            bool result = false;
            UIDocument activeDoc = applicationData.ActiveUIDocument;
            if (activeDoc != null && activeDoc.Document != null)
            {
                result = true;
            }

            return result;
        }
    }
}
