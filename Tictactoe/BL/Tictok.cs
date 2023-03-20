using Newtonsoft.Json;
using Tictactoe.Models;

namespace Tictactoe.BL
{
    public class Tictok
    {     
        private int[,] winLine = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 2, 4, 6 }, { 1, 4, 7 }, { 0, 4, 8 }, { 0, 3, 6 }, { 2, 5, 8 } };       
        Model model = new Model();
        
        public Tictok()
        {
            using (StreamReader sr = new StreamReader("map.json"))
            {
                model = JsonConvert.DeserializeObject<Model>(sr.ReadLine());
            }
        }
      
        public int GetIdx()
        {
            using (StreamReader sr = new StreamReader("map.json"))
            {
                model = JsonConvert.DeserializeObject<Model>(sr.ReadLine());
            }
            return model.IdxCel;
        }
        public void InputStep(int addressCel) 
        {
            model.IdxCel = addressCel;
            if (model.Map[addressCel] == 5 & !model.IsFinish)
            {
                if (!model.IsCros)
                {
                    model.IsCros = true;
                    model.Map[addressCel] = 1;
                    model.Value = "X";
                    WriteJson();
                    TestWin();
                }
                else
                {
                    model.IsCros = false;
                    model.Map[addressCel] = 0;
                    model.Value = "O";
                    WriteJson();
                    TestWin();
                }
            }
        }

        public string TestWin()
        {
            for (int i = 0; i < 8; i++)
            {
                if (Step()[winLine[i, 0]] == 0 & Step()[winLine[i, 1]] == 0 & Step()[winLine[i, 2]] == 0)  
                {
                    model.IsFinish = true;
                    WriteJson();
                    return ("Winner O");
                }
                if (Step()[winLine[i, 0]] == 1 & Step()[winLine[i, 1]] == 1 & Step()[winLine[i, 2]] == 1) 
                {
                    model.IsFinish = true;
                    WriteJson();
                    return ("Winner X");
                }
            }
            return "";
        }

        public int[] Step()
        {
            using (StreamReader sr = new StreamReader("map.json"))
            {
                model = JsonConvert.DeserializeObject<Model>(sr.ReadLine());
            }
            return model.Map;
        }

        public string GetValue()
        {
            using (StreamReader sr = new StreamReader("map.json"))
            {
                model = JsonConvert.DeserializeObject<Model>(sr.ReadLine());
            }
            return model.Value;
        }

        private void WriteJson()
        {
            using (StreamWriter sw = new StreamWriter("map.json"))
            {
                string str = JsonConvert.SerializeObject(model);
                sw.WriteLine(str);
            }
        }

        public bool IsFinish()
        {
            using (StreamReader sr = new StreamReader("map.json"))
            {
                model = JsonConvert.DeserializeObject<Model>(sr.ReadLine());
            }
            return model.IsFinish;
        }

        public void NewGame()
        {
            model.IsFinish = false;
            model.IsCros = false;
            model.Value = null;
            model.Map = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            WriteJson();
        }
    }
}
