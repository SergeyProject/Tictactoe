using Microsoft.AspNetCore.Mvc;
using Tictactoe.BL;

namespace Tictactoe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TictactoeController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        Tictok tictok = new Tictok();

        [HttpGet]
        [Route("api/GetStep")]
        public JsonResult GetStep(int idx)
        {
            tictok.InputStep(idx);  // число 0-9 (адрес ячейки)           
            return new JsonResult("");           
        }

        [HttpGet]
        [Route("api/GetIdx")]
        public JsonResult GetIdx()
        {       
            return new JsonResult(tictok.GetIdx());
        }

        [HttpGet]
        [Route("api/GetMap")]
        public JsonResult GetMap()
        {
            return new JsonResult(tictok.Step());
        }

        [HttpGet]
        [Route("api/GetValue")]
        public JsonResult GetValue()
        {            
            return new JsonResult(tictok.GetValue());
        }

        [HttpGet]
        [Route("api/TestWin")]
        public JsonResult TestWin()
        {
            if (tictok.TestWin() != "")
            {
                return new JsonResult(tictok.TestWin());
            }
            return new JsonResult("");
        }


        [HttpGet]
        [Route("api/IsFinish")]
        public JsonResult IsFinish()
        {
            return new JsonResult(tictok.IsFinish());
        }


        [HttpGet]
        [Route("api/NewGame")]
        public JsonResult NewGame()
        {
            tictok.NewGame();
            return new JsonResult("Start New Game");
        }
    }
}
