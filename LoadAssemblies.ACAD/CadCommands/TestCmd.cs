using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using LoadAssemblies.Views.Windows;

namespace LoadAssemblies.ACAD.CadCommands
{
    public static class TestCmd
    {
        [CommandMethod("test-aio")]
        public static void TestAllInOneCommand()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
            {
                return;
            }

            TestWindow win = new TestWindow();
            Application.ShowModalWindow(win);
        }
    }
}
