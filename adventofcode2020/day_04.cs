using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace adventofcode2020
{
    public static class DayFour
    {
        public static void Execute()
        {
            List<Passport> passports = new List<Passport>();
            Passport passport;

            // load in file
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_04.txt")))
            {
                string line;
                bool emptyLineBefore = true;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        emptyLineBefore = true;
                        passport = ConvertToPassport(sb.ToString());
                        if(passport != null) passports.Add(passport);
                        sb.Clear();
                    }
                    else
                    {                        
                        line = emptyLineBefore ? line : " " + line;
                        sb.Append(line);
                        emptyLineBefore = false;
                    }
                }

                passport = ConvertToPassport(sb.ToString());
                if (passport != null) passports.Add(passport);
            }

            Console.WriteLine($"Valid passport count: {passports.Count}");
        }

        private static Passport ConvertToPassport(string passportString)
        {
            Passport passport = new Passport();

            var passportSplitString = passportString.Split(' ');
            foreach(var part in passportSplitString)
            {
                try
                {
                    var partSplit = part.Split(':');
                    var value = partSplit[1];
                    switch (partSplit[0])
                    {
                        case "byr":
                            passport.BirthYear = Convert.ToInt32(value);
                            break;
                        case "iyr":
                            passport.IssueYear = Convert.ToInt32(value);
                            break;
                        case "eyr":
                            passport.ExpirationYear = Convert.ToInt32(value);
                            break;
                        case "hgt":
                            passport.Height = value;
                            break;
                        case "hcl":
                            passport.HairColor = value;
                            break;
                        case "ecl":
                            passport.EyeColor = value;
                            break;
                        case "pid":
                            passport.PassportID = value;
                            break;
                        case "cid":
                            passport.CountryID = value;
                            break;
                    }
                }
                catch
                {
                    return null;
                }
            }

            return passport.IsValid ? passport : null;
        }
    }

    public class Passport
    {
        private int _birthYear;
        private int _issueYear;
        private int _expirationYear;
        private string _height;
        private string _hairColor;
        private string _eyeColor;
        private string _passportId;

        public int BirthYear
        {
            get => _birthYear;
            set
            {
                if((value >= 1920) &&  (value <= 2002))
                {
                    _birthYear = value;
                }
                else
                {
                    throw new Exception($"Birth Year value of '{value}' is not valid");
                }
            }
        }
        public int IssueYear 
        {
            get => _issueYear;
            set
            {
                if ((value >= 2010) && (value <= 2020))
                {
                    _issueYear = value;
                }
            }
        }
        public int ExpirationYear 
        {
            get => _expirationYear;
            set
            {
                if ((value >= 2020) && (value <= 2030))
                {
                    _expirationYear = value;
                }
            }
        }
        public string Height 
        {
            get => _height;
            set
            {
                if (Regex.IsMatch(value, @"(1[5-8][0-9]|19[0-3])cm") || Regex.IsMatch(value, @"(59|6[0-9]|7[0-6])in"))
                {
                    _height = value;
                }
                else
                {
                    throw new Exception($"Height value of '{value}' is not valid");
                }
            }
        }
        public string HairColor 
        {
            get => _hairColor;
            set
            {
                if(Regex.IsMatch(value, @"^#([0-9a-fA-F]{6})$"))
                {
                    _hairColor = value;
                }
                else
                {
                    throw new Exception($"Hair Color value of '{value}' is not valid");
                }
            }
        }
        public string EyeColor 
        {
            get => _eyeColor;
            set
            {
                if (Regex.IsMatch(value, @"(amb|blu|brn|gry|grn|hzl|oth)"))
                {
                    _eyeColor = value;
                }
                else
                {
                    throw new Exception($"Eye Color value of '{value}' is not valid");
                }
            }
        }
        public string PassportID 
        {
            get => _passportId;
            set
            {
                if(Regex.IsMatch(value, @"^[0-9]{9}$"))
                {
                    _passportId = value;
                }
                else
                {
                    throw new Exception($"Passport ID value of '{value}' is not valid");
                }
            }
        }
        public string CountryID { get; set; }

        public bool IsValid
        {
            get
            {
                if (BirthYear != 0 &&
                    IssueYear != 0 &&
                    ExpirationYear != 0 &&
                    !string.IsNullOrEmpty(Height) &&
                    !string.IsNullOrEmpty(HairColor) &&
                    !string.IsNullOrEmpty(EyeColor) &&
                    !string.IsNullOrEmpty(PassportID))
                {
                    return true;
                }

                return false;
            }
        }        
    }
}
