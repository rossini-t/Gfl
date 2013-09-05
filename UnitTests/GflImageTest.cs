using Gfl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing.Imaging;
using System.Drawing;

namespace UnitTests
{        
    /// <summary>
    ///Classe de test pour GflImageTest, destinée à contenir tous
    ///les tests unitaires GflImageTest
    ///</summary>
    [TestClass()]
    public class GflImageTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        // 
        //Vous pouvez utiliser les attributs supplémentaires suivants lors de l'écriture de vos tests :
        //
        //Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            GflAPI.Init();
        }
        //
        //Utilisez ClassCleanup pour exécuter du code après que tous les tests ont été exécutés dans une classe
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            if (System.IO.File.Exists("test.tiff"))
                System.IO.File.Delete("test.tiff");
            GflAPI.Exit();
        }
        //
        //Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void TestConstructors()
        {
            var img = new GflImage(500, 500, 500, GFL_BITMAP_TYPE.GFL_COLORS);
            string fileName = "test.tiff";
            Assert.AreEqual(img.Width, 500);
            Assert.AreEqual(img.Height, 500);            
            img.Save(fileName);
            img.Dispose();
            img = new GflImage(fileName);
            Assert.AreEqual(img.Width, 500);
            Assert.AreEqual(img.Height, 500);
            img.Dispose();
        }
  
    }
}
