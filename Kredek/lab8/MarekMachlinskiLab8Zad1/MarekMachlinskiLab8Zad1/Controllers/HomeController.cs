using System;
using System.Linq;
using System.Web.Mvc;

namespace MarekMachlinskiLab8Zad1.Controllers
{
    /// <summary>
    /// Główny kontroler stron
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Ekran logowania
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// Ekran rejestracji
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View("Register");
        }

        /// <summary>
        /// Po rejestracji dodaje gracza do bazy danych i wraca na ekran główny
        /// </summary>
        /// <param name="player">Nowo zarejestrowany gracz</param>
        /// <returns></returns>
        public ActionResult AddPlayer(Models.PlayerModel player)
        {
            if(player != null)
            {
                if(player.Login.Length > 0 && player.Password.Length > 0)
                {
                    using (Database.DB_9B1FC5_cpc20181Entities context = new Database.DB_9B1FC5_cpc20181Entities())
                    {
                        if(context.MarekMachlinskiPlayers.Find(player.Login) == null)
                        {
                            context.MarekMachlinskiPlayers.Add(new Database.MarekMachlinskiPlayer() { Login = player.Login, Password = player.Password, CreationDate = DateTime.Now });
                            context.SaveChanges();
                        }
                    }
                }
            }
            return Index();
        }

        /// <summary>
        /// Główne menu gry z listą plików zapisu
        /// </summary>
        /// <param name="player">Dane logowania gracza</param>
        /// <returns></returns>
        public ActionResult Menu(Models.PlayerModel player)
        {
            if (player != null)
            {
                if (player.Login.Length > 0 && player.Password.Length > 0)
                {
                    using (Database.DB_9B1FC5_cpc20181Entities context = new Database.DB_9B1FC5_cpc20181Entities())
                    {
                        var sourcePlayer = context.MarekMachlinskiPlayers.Find(player.Login);
                        if(sourcePlayer != null)
                            if (sourcePlayer.Password == player.Password)
                                return View("Menu", context.MarekMachlinskiSavedGames.Where(save=>save.PlayerLogin==sourcePlayer.Login).ToList());
                    }
                }
            }
            return Index();
        }

        /// <summary>
        /// Główny ekran gry
        /// </summary>
        /// <param name="save">Aktualnie wybrany plik zapisu</param>
        /// <returns></returns>
        public ActionResult GameScreen(Models.SaveGameModel save)
        {
            return View("GameScreen", save);
        }

        /// <summary>
        /// Odbiera dane z ekranu gry i zapisuje je do bazy danych
        /// </summary>
        /// <param name="save">Plik gry do zapisania</param>
        [HttpPost]
        public void Save(Models.SaveGameModel save)
        {
            if (save != null)
            {
                if (save.PlayerLogin != null)
                {
                    using (Database.DB_9B1FC5_cpc20181Entities context = new Database.DB_9B1FC5_cpc20181Entities())
                    {
                        var sourceSave = context.MarekMachlinskiSavedGames.Find(save.ID);
                        if (sourceSave == null)
                            context.MarekMachlinskiSavedGames.Add(new Database.MarekMachlinskiSavedGame()
                            {
                                PlayerLogin = save.PlayerLogin,
                                LastSaveDate = DateTime.Now,
                                PointsLeft = save.PointsLeft,
                                AssemblerLevel = save.AssemblerLevel,
                                CLevel = save.CLevel,
                                CppLevel = save.CppLevel,
                                CSharpLevel = save.CSharpLevel,
                                FSharpLevel = save.FSharpLevel,
                                JavaLevel = save.JavaLevel,
                                JavaScriptLevel = save.JavaScriptLevel,
                                PHPLevel = save.PHPLevel,
                                PythonLevel = save.PythonLevel,
                                VisualBasicLevel = save.VisualBasicLevel
                            });
                        else
                        {
                            sourceSave.PointsLeft = save.PointsLeft;
                            sourceSave.AssemblerLevel = save.AssemblerLevel;
                            sourceSave.VisualBasicLevel = save.VisualBasicLevel;
                            sourceSave.CLevel = save.CLevel;
                            sourceSave.CppLevel = save.CppLevel;
                            sourceSave.CSharpLevel = save.CSharpLevel;
                            sourceSave.FSharpLevel = save.FSharpLevel;
                            sourceSave.JavaLevel = save.JavaLevel;
                            sourceSave.JavaScriptLevel = save.JavaScriptLevel;
                            sourceSave.PHPLevel = save.PHPLevel;
                            sourceSave.PythonLevel = save.PythonLevel;
                            sourceSave.LastSaveDate = DateTime.Now;
                        }
                        context.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Usuwa wybranych przez gracza plik gry
        /// </summary>
        /// <param name="id">Identyfikator pliku gry</param>
        /// <param name="login">Login aktualnego gracza</param>
        /// <returns></returns>
        public ActionResult Delete(int id, string login)
        {
            if (login != null)
            {
                using (Database.DB_9B1FC5_cpc20181Entities context = new Database.DB_9B1FC5_cpc20181Entities())
                {
                    var toDelete = context.MarekMachlinskiSavedGames.Find(id);
                    if (toDelete != null)
                    {
                        context.MarekMachlinskiSavedGames.Remove(toDelete);
                        context.SaveChanges();
                    }
                    return Menu(new Models.PlayerModel() { Login = login, Password = context.MarekMachlinskiPlayers.Find(login).Password });
                }
            }
            else
                return Index();
        }
    }
}