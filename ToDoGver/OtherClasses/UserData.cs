using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace ToDoGver.OtherClasses
{
    class UserData
    {
        // Path to the user data file
        string filePath = @"E:\Project\C#\GverReminder\ToDoGver\GverReminder\ToDoGver\Data\UserSettings.txt";

        // id and name from user data for editing userdata
        private int goldID = 2;
        private string goldName = "gold";
        private int diaID = 3;
        private string diaName = "dia";
        private int OTE_gold_ID = 4;
        private string OTE_gold_name = "OTE_gold";
        private int OTE_dia_ID = 5;
        private string OTE_dia_name = "OTE_dia";

        // Golds, Hp, etc.
        public int Gold = 0;
        public int Dia = 0;
        private int hp = 0;
        private int mb = 0;
        private int maxHp = 1000;
        private int maxMb = 1000;
        private int goldLimit = 500000000;
        private int diaLimit = 10000000;

        public int Hp { get => hp; set => hp = value; }
        public int Mb { get => mb; set => mb = value; }
        public int MaxHp { get => maxHp; set => maxHp = value; }
        public int MaxMb { get => maxMb; set => maxMb = value; }

        // One time event bonus
        public int OTE_gold;
        public int OTE_dia;


        // List to store data to save to the file
        private List<UserDataHelper> ListUserData = new List<UserDataHelper>();

        // The list to load file text to parse to the ListOneTimeEvents
        private List<string> lines;

        public UserData()
        {
            LoadFile();
        }

        // Simple load data to ListUserData from filePath, it separates and translate it
        private void LoadFile()
        {
            try
            {
                lines = File.ReadAllLines(filePath).ToList();
                foreach (var line in lines)
                {
                    string[] entries = line.Split(',');
                    UserDataHelper userdatahelper = new UserDataHelper();

                    userdatahelper.IdProperty = Int32.Parse(entries[0]);
                    userdatahelper.nameProperty = entries[1];
                    userdatahelper.value = Int32.Parse(entries[2]);
                    ListUserData.Add(userdatahelper);
                }
                if (lines != null)
                TranslateListToUserdata();
            }
            catch
            {
                MessageBox.Show("Can´t load user data file");
            }
        }

        // This transform data to specific value like gold
        private void TranslateListToUserdata()
        {
            foreach(var index in ListUserData)
            {
                if (index.IdProperty.ToString().Equals(goldID.ToString()))
                {
                    Gold = index.value;
                }
                else if (index.IdProperty.ToString().Equals(diaID.ToString()))
                {
                    Dia = index.value;
                }
                else if (index.IdProperty.ToString().Equals(OTE_gold_ID.ToString()))
                {
                    OTE_gold = index.value;
                }
                else if (index.IdProperty.ToString().Equals(OTE_dia_ID.ToString()))
                {
                    OTE_dia = index.value;
                }
            }
        }

        // This add golds and rewrite userdata
        public void AddGold(int goldAmount)
        {
            double goldCheck = Gold;
            if((goldCheck + goldAmount) < goldLimit)
            {
                Gold += goldAmount;
                List<string> output = new List<string>();
                ListUserData.Remove(new UserDataHelper() { IdProperty = goldID });
                ListUserData.Add(new UserDataHelper() { IdProperty = goldID, nameProperty = goldName, value = Gold });
                foreach (var data in ListUserData)
                {
                    output.Add($"{data.IdProperty},{data.nameProperty},{data.value}");
                }
                File.WriteAllLines(filePath, output);
            }
            else
            {
                Gold = goldLimit - 1;
                List<string> output = new List<string>();
                ListUserData.Remove(new UserDataHelper() { IdProperty = goldID });
                ListUserData.Add(new UserDataHelper() { IdProperty = goldID, nameProperty = goldName, value = Gold });
                foreach (var data in ListUserData)
                {
                    output.Add($"{data.IdProperty},{data.nameProperty},{data.value}");
                }
                File.WriteAllLines(filePath, output);
            }
        }

        // This add diamonds and rewrite userdata
        public void AddDiamond(int diamondAmount)
        {
            double diaCheck = Dia;
            if ((diaCheck + diamondAmount) < diaLimit)
            {
                Dia += diamondAmount;
                List<string> output = new List<string>();
                ListUserData.Remove(new UserDataHelper() { IdProperty = diaID });
                ListUserData.Add(new UserDataHelper() { IdProperty = diaID, nameProperty = diaName, value = Dia });
                foreach (var data in ListUserData)
                {
                    output.Add($"{data.IdProperty},{data.nameProperty},{data.value}");
                }
                File.WriteAllLines(filePath, output);
            }
            else{
                Dia = diaLimit - 1;
                List<string> output = new List<string>();
                ListUserData.Remove(new UserDataHelper() { IdProperty = diaID });
                ListUserData.Add(new UserDataHelper() { IdProperty = diaID, nameProperty = diaName, value = Dia });
                foreach (var data in ListUserData)
                {
                    output.Add($"{data.IdProperty},{data.nameProperty},{data.value}");
                }
                File.WriteAllLines(filePath, output);
            }

        }

    }

    // Class that define variables for ListUserData
    class UserDataHelper : IEquatable<UserDataHelper>
    {
        public string nameProperty { get; set; }
        public int value { get; set; }
        public int IdProperty { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            UserDataHelper objAsPart = obj as UserDataHelper;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public override int GetHashCode()
        {
            return IdProperty;
        }

        public bool Equals(UserDataHelper other)
        {
            if (other == null) return false;
            return (this.IdProperty.Equals(other.IdProperty));
        }
    }

}
