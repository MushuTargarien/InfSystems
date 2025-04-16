using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;



namespace regularExpr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckPass();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckColor();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var tokens = Tokenize(textBox3.Text);
            richTextBox1.Clear();
            foreach (var token in tokens)
            {
                richTextBox1.Text += ($" \n {{\"type\": \"{token.Type}\", \"span\": [{token.Start}, {token.End}]}}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkDate();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (IsValidBracketSequence(textBox5.Text))
            {
                label10.Text = "valid";
            }
            else
            {
                label10.Text = "notvalid";
            }
        }

        //для пароля -- без регулярок                 
        private void CheckPass()
        {
            try
            {
                string specialChars = "^$%@#&*!?";
                bool Long = false;
                bool Big = false;
                bool Small = false;
                bool Digit = false;
                List<string> symbols = new List<string>();
                bool twoSymbRazn = false;
                bool TwoInRow = false;
                string str = textBox1.Text;
                for (int i = 0; i < str.Length; i++)
                {
                    if (str.Length >= 8)
                    {
                        Long = true;
                    }
                    if (Char.IsUpper(str[i]))
                    {
                        Big = true;
                    }
                    if (Char.IsLower(str[i]))
                    {
                        Small = true;
                    }
                    if (Char.IsDigit(str[i]))
                    {
                        Digit = true;
                    }
                    if (specialChars.Contains(str[i]))
                    {
                        symbols.Add(str[i].ToString());
                    }
                    if (i >= 1 && str[i] == str[i - 1])
                    {
                        TwoInRow = true;
                    }
                    HashSet<string> uniqueSymbols = new HashSet<string>(symbols);
                    if (symbols.Count >= 2 && uniqueSymbols.Count >=2) {
                        twoSymbRazn = true;
                    }
                }

                if (Long == true && Big == true && Small == true && Digit == true && twoSymbRazn == true && TwoInRow == false)
                {

                    label1.Text = "Пароль корректен";
                }
                else
                {
                    label1.Text = "Пароль НЕкорректен";
                    if (!Long)
                    {
                        label1.Text += "\n Пароль должен состоять не менее чем из 8 символов";
                    }
                    if (!Big)
                    {
                        label1.Text += "\n Нет символа в верхнем регистре";
                    }
                    if (!Small)
                    {
                        label1.Text += "\n Нет символа в нижнем регистре";
                    }
                    if (!Digit)
                    {
                        label1.Text += "\n Нет цифры";
                    }
                    if (!twoSymbRazn)
                    {
                        label1.Text += "\n Не хватает специальных символов или у вас 2 одинаковых";
                    }
                    if (TwoInRow)
                    {
                        label1.Text += "\n У вас есть 2 повторяющихся подряд символа";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //для цвета -- без регулярок         
        private void CheckColor()
        {
            try
            {
                string str = textBox2.Text;
                switch (textBox2.Text[0])
                {
                    case 'r':
                        if (!textBox2.Text.StartsWith("rgb(") || !textBox2.Text.EndsWith(")"))
                        {
                            label2.Text = " запись цвета rgb НЕправильная";
                            label2.Text += "\n неправильное начало или не закрыты скобки";
                            return;
                        }

                        string content = textBox2.Text.Substring(4, textBox2.Text.Length - 5).Trim();
                        string[] parts = content.Split(',');

                        if (parts.Length != 3)
                        {
                            label2.Text = "запись цвета rgb НЕправильная";
                            label2.Text += " \n недостаточно элементов внутри скобок";
                            return;
                        }

                        int count = 0;
                        foreach (string part in parts)
                        {
                            string trimmedPart = part.Trim();

                            if (part.EndsWith("%"))
                            {
                                count += 1;
                                trimmedPart = part.Substring(0, part.Length - 1);
                                if (!int.TryParse(trimmedPart, out int value))
                                {
                                    label2.Text = "Запись цвета типа rgb НЕверна";
                                    label2.Text += "\n не число в записи";
                                    return;
                                }

                                if (value < 0 || value > 100)
                                {
                                    label2.Text = "Запись цвета типа rgb НЕверна";
                                    label2.Text += " \n  процент выходит за рамки 0-100";
                                    return;
                                }
                            }
                            else
                            {
                                if (!int.TryParse(trimmedPart, out int value))
                                {
                                    label2.Text = "Запись цвета типа rgb НЕверна";
                                    label2.Text += " \n  не число в записи";
                                    return;
                                }

                                if (value < 0 || value > 255)
                                {
                                    label2.Text = "Запись цвета типа rgb НЕверна";
                                    label2.Text += " \n значение rgb выходит за рамки 0-255";
                                    return;
                                }
                            }
                        }
                        if (count > 0 && count < 3)
                        {
                            label2.Text = " запись цвета rgb НЕправильная";
                            label2.Text += " \n есть и проценты и значение";
                            return;
                        }
                        label2.Text = "Запись цвета типа rgb верна";
                        break;

                    case 'h':
                        if (!textBox2.Text.StartsWith("hsl(") || !textBox2.Text.EndsWith(")"))
                        {
                            label2.Text = " запись цвета hsl НЕправильная";
                            label2.Text += " \n неправильное начало или не закрыта скобка";
                            return;
                        }

                        string contentHSL = textBox2.Text.Substring(4, textBox2.Text.Length - 5).Trim();
                        string[] partsHSL = contentHSL.Split(',');

                        if (partsHSL.Length != 3)
                        {
                            label2.Text = " запись цвета hsl НЕправильная";
                            label2.Text = "\n должно быть 3 параметра ";
                            return;
                        }
                        if (partsHSL[0].EndsWith("%"))
                        {
                            label2.Text = " запись цвета hsl НЕправильная";
                            label2.Text = " \n тон не должен заканчиваться процентом";
                            return;
                        }
                        if (!partsHSL[1].EndsWith("%"))
                        {
                            label2.Text = " запись цвета hsl НЕправильная";
                            label2.Text += " \n насыщенность должна измеряться в процентах";
                            return;
                        }
                        if (!partsHSL[2].EndsWith("%"))
                        {
                            label2.Text = " запись цвета hsl НЕправильная";
                            label2.Text += " \n светлота должна измеряться в процентах";
                            return;
                        }

                        foreach (string part in partsHSL)
                        {
                            string trimmedPart = part.Trim();

                            if (part.EndsWith("%"))
                            {
                                trimmedPart = part.Substring(0, part.Length - 1);
                                if (!int.TryParse(trimmedPart, out int value))
                                {
                                    label2.Text = "Запись цвета типа hsl НЕверна";
                                    label2.Text += " \n не число в записи";
                                    return;
                                }

                                if (value < 0 || value > 100)
                                {
                                    label2.Text = "Запись цвета типа hsl НЕверна";
                                    label2.Text += " \n  процент выходит за рамки 0-100";
                                    return;
                                }
                            }
                            else
                            {
                                if (!int.TryParse(trimmedPart, out int value))
                                {
                                    label2.Text = "Запись цвета типа hsl НЕверна";
                                    label2.Text += " \n не число в записи";
                                    return;
                                }

                                if (value < 0 || value > 360)
                                {
                                    label2.Text = "Запись цвета типа hsl НЕверна";
                                    label2.Text += " \n тон выходит за рамки 0-360";
                                    return;
                                }
                            }
                        }
                        label2.Text = "Запись цвета типа hsl верна";
                        break;
                    case '#':
                        string HexLetter = "ABCDEF";
                        for (int i = 1; i < textBox2.Text.Length; i++)
                        {
                            if ((Char.IsDigit(str[i]) || HexLetter.Contains(str[i]) && (str.Length == 7 || str.Length == 4)))
                            {
                                label2.Text = "Запись цвета типа hex верна";

                            }
                            else
                            {
                                label2.Text = " запись цвета hex НЕправильная";
                                label2.Text += "\n непаравильная длина или вы использвали не цифры или буквы";
                                return;
                            }
                        }
                        break;

                    default:
                        label2.Text = "неверный формат цвета";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        // для токенов   -- с регулярками
        public class Token
        {
            public string Type { get; }
            public int Start { get; }
            public int End { get; }

            public Token(string type, int start, int end)
            {
                Type = type;
                Start = start;
                End = end;
            }
        }

        static List<Token> Tokenize(string expression)
        {
            // Регулярное выражение для всех типов токенов
            var tokenPattern = new Regex(
                @"(?<function>sin|cos|tg|ctg|tan|cot|sinh|cosh|th|cth|tanh|coth|ln|lg|log|exp|sqrt|cbrt|abs|sign)|" +
                @"(?<constant>pi|e|sqrt2|ln2|ln10)|" +
                @"(?<number>\d+(\.\d+)?)|" +
                @"(?<variable>[a-zA-Z_]\w*)|" +
                @"(?<operator>[\+\-\*/])|" +
                @"(?<left_parenthesis>\()|" +
                @"(?<right_parenthesis>\))",
                RegexOptions.Compiled);

            var tokens = new List<Token>();
            var matches = tokenPattern.Matches(expression);             // ищет все свпадения в строке с регулярками

            foreach (Match match in matches)                             // для каждого совпадения определяется к какой группе принадлежит и записывает соответствующий токен
            {
                switch (true)
                {
                    case var _ when match.Groups["variable"].Success:
                        tokens.Add(new Token("variable", match.Index, match.Index + match.Length));
                        break;
                    case var _ when match.Groups["number"].Success:
                        tokens.Add(new Token("number", match.Index, match.Index + match.Length));
                        break;
                    case var _ when match.Groups["constant"].Success:
                        tokens.Add(new Token("constant", match.Index, match.Index + match.Length));
                        break;
                    case var _ when match.Groups["function"].Success:
                        tokens.Add(new Token("function", match.Index, match.Index + match.Length));
                        break;
                    case var _ when match.Groups["operator"].Success:
                        tokens.Add(new Token("operator", match.Index, match.Index + match.Length));
                        break;
                    case var _ when match.Groups["left_parenthesis"].Success:
                        tokens.Add(new Token("left_parenthesis", match.Index, match.Index + match.Length));
                        break;
                    case var _ when match.Groups["right_parenthesis"].Success:
                        tokens.Add(new Token("right_parenthesis", match.Index, match.Index + match.Length));
                        break;
                }
            }

            return tokens;
        }


        /*
         . — любой символ.
        \d — цифра (эквивалент [0-9]).
        \w — буква, цифра или _ (эквивалент [a-zA-Z0-9_]).
        \s пробел
        * — предыдущий символ повторяется 0 или более раз.
        + — предыдущий символ повторяется 1 или более раз.
        ? — предыдущий символ встречается 0 или 1 раз.
        | — логическое "или" (альтернатива).
        () — группировка (выделение подвыражения
        Якорь ^  Указывает, что совпадение должно начинаться с начала строки.
        Якорь $  Указывает, что совпадение должно заканчиваться в конце строки
         */

        // для даты -- с регулярками            
        private void checkDate()
        {
            var patterns = new Regex(
               @"(?<ddmmyyyy1>^(?<day>\d{1,2})\.(?<month>\d{1,2})\.(?<year>\d{1,})$)|" +
               @"(?<ddmmyyyy2>^(?<day>\d{1,2})\/(?<month>\d{1,2})\/(?<year>\d{1,})$)|" +
               @"(?<ddmmyyyy3>^(?<day>\d{1,2})\-(?<month>\d{1,2})\-(?<year>\d{1,})$)|" +
               @"(?<yyyymmdd1>^(?<year>\d{1,4})\.(?<month>\d{1,2})\.(?<day>\d{1,})$)|" +
               @"(?<yyyymmdd2>^(?<year>\d{1,})\/(?<month>\d{1,2})\/(?<day>\d{1,2})$)|" +
               @"(?<yyyymmdd3>^(?<year>\d{1,})\-(?<month>\d{1,2})\-(?<day>\d{1,2})$)|" +
               @"(?<ddmmrusyyyy>^(?<day>\d{1,2})\s(?<monthName>января|февраля|марта|апреля|мая|июня|июля|августа|сентября|октября|ноября|декабря)\s(?<year>\d{1,})$)|" +
               @"(?<mm_engddyyyy1>^(?<monthName>January|February|March|April|May|June|July|August|September|October|November|December)\s(?<day>\d{1,2})\s(?<year>\d{1,})$)|" +
               @"(?<mm_engddyyyy2>^(?<monthName>Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\s(?<day>\d{1,2})\,\s(?<year>\d{1,})$)|" +
               @"(?<yyyymm_engdd1>^(?<year>\d{1,})\,\s(?<monthName>January|February|March|April|May|June|July|August|September|October|November|December)\s(?<day>\d{1,2})$)|" +
               @"(?<yyyymm_engdd2>^(?<year>\d{1,})\,\s(?<monthName>Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\s(?<day>\d{1,2})$)",
               RegexOptions.Compiled);

            var match = patterns.Match(textBox4.Text);

            if (match.Success)
            {
                switch (true)
                {
                    case var _ when match.Groups["ddmmyyyy1"].Success || match.Groups["ddmmyyyy2"].Success || match.Groups["ddmmyyyy3"].Success:
                        label8.Text = "Тип даты: день месяц год";
                        break;

                    case var _ when match.Groups["yyyymmdd1"].Success || match.Groups["yyyymmdd2"].Success || match.Groups["yyyymmdd3"].Success:
                        label8.Text = "Тип даты: год месяц день";
                        break;

                    case var _ when match.Groups["ddmmrusyyyy"].Success:
                        label8.Text = "Тип даты: день месяц_rus год";
                        break;

                    case var _ when match.Groups["mm_engddyyyy1"].Success:
                        label8.Text = "Тип даты: Месяц_eng день год";
                        break;

                    case var _ when match.Groups["mm_engddyyyy2"].Success:
                        label8.Text = "Тип даты: Мес_eng день, год";
                        break;

                    case var _ when match.Groups["yyyymm_engdd1"].Success:
                        label8.Text = "Тип даты: год, Месяц_eng день";
                        break;

                    case var _ when match.Groups["yyyymm_engdd2"].Success:
                        label8.Text = "Тип даты: год, Мес_eng день";
                        break;

                    default:
                        label8.Text = "Неправильный тип даты";
                        break;
                }

                try
                {
                    int day = 0, month = 0, year = 0;

                    if (match.Groups["day"].Success && match.Groups["month"].Success && match.Groups["year"].Success)
                    {
                        day = int.Parse(match.Groups["day"].Value);
                        month = int.Parse(match.Groups["month"].Value);
                        year = int.Parse(match.Groups["year"].Value);


                    }
                    else
                    {

                        day = int.Parse(match.Groups["day"].Value);
                        year = int.Parse(match.Groups["year"].Value);
                        string monthName = match.Groups["monthName"].Value;

                        switch (monthName)
                        {
                            case "января":
                            case "Jan":
                            case "January":
                                month = 1;
                                break;
                            case "февраля":
                            case "Feb":
                            case "February":
                                month = 2;
                                break;
                            case "марта":
                            case "Mar":
                            case "March":
                                month = 3;
                                break;
                            case "апреля":
                            case "Apr":
                            case "April":
                                month = 4;
                                break;
                            case "мая":
                            case "May":
                                month = 5;
                                break;
                            case "июня":
                            case "Jun":
                            case "June":
                                month = 6;
                                break;
                            case "июля":
                            case "Jul":
                            case "July":
                                month = 7;
                                break;
                            case "августа":
                            case "Aug":
                            case "August":
                                month = 8;
                                break;
                            case "сентября":
                            case "Sep":
                            case "September":
                                month = 9;
                                break;
                            case "октября":
                            case "Oct":
                            case "October":
                                month = 10;
                                break;
                            case "ноября":
                            case "Nov":
                            case "Novembver":
                                month = 11;
                                break;
                            case "декабря":
                            case "Dec":
                            case "December":
                                month = 12;
                                break;
                        }
                    }

                    if (isCorrectData(month, day, year))
                    {
                        label7.Text = "успешно";
                    }
                    else
                    {

                        label7.Text += "\n НЕправильно";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                label7.Text = "неизвестный тип даты";
                label8.Text = "";
            }
        }


        // для проверки количества дней в месяце
        private bool isCorrectData(int month, int day, int year)
        {
            if (year < 0)
            {
                label7.Text = "Год отрицательный";
                return false;
            }

            switch (month)
            {
                case 1:
                    if (day < 0 || day > 31)
                    {
                        label7.Text = "день выходит за рамки месяца";
                        return false;
                    }
                    break;
                case 2:
                    if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0)) {
                        if (day > 0 && day <= 29)
                        {
                            return true;
                        }
                    }
                    if ((year % 4 != 0 && year % 100 == 0) || (year % 400 != 0))
                    {
                        if (day < 0 || day > 28)
                        {
                            label7.Text = "день выходит за рамки месяца";
                            return false;
                        }
                    }
                    break;
                case 3:
                    if (day < 0 || day > 31)
                    {
                        label7.Text = "день выходит за рамки месяца";
                        return false;
                    }
                    break;
                case 4:
                    if (day < 0 || day > 30)
                    {
                        label7.Text = "день выходит за рамки месяца";
                        return false;
                    }
                    break;
                case 5:
                    if (day < 0 || day > 31)
                    {
                        label7.Text = "день выходит за рамки месяца";
                        return false;
                    }
                    break;
                case 6:
                    if (day < 0 || day > 30)
                    {
                        label7.Text = "день выходит за рамки месяца";
                        return false;
                    }
                    break;
                case 7:
                    if (day < 0 || day > 31)
                    {
                        label7.Text = "день выходит за рамки месяца";
                        return false;
                    }
                    break;
                case 8:
                    if (day < 0 || day > 31)
                    {
                        label7.Text = "день выходит за рамки месяца";
                        return false;
                    }
                    break;
                case 9:
                    if (day < 0 || day > 30)
                    {
                        label7.Text = "день выходит за рамки месяца";
                        return false;
                    }
                    break;
                case 10:
                    if (day < 0 || day > 31)
                    {
                        label7.Text = "день выходит за рамки месяца";
                        return false;
                    }
                    break;
                case 11:
                    if (day < 0 || day > 30)
                    {
                        label7.Text = "день выходит за рамки месяца";
                        return false;
                    }
                    break;
                case 12:
                    if (day < 0 || day > 31)
                    {
                        label7.Text = "день выходит за рамки месяца";
                        return false;
                    }
                    break;
                default:
                    return false;
            }

            return true;
        }


        // для скобочек -- без регулярок
        static bool IsValidBracketSequence(string str)
        {
            Stack<char> stack = new Stack<char>();

            Dictionary<char, char> bracketsMap = new Dictionary<char, char>
        {
            { ')', '(' },
            { '}', '{' },
            { ']', '[' }
        };

            foreach (char ch in str)
            {  
                if (bracketsMap.ContainsValue(ch))                                   //Открыфвающие скобки сразу в стек
                {
                    stack.Push(ch);
                }
                else if (bracketsMap.ContainsKey(ch))
                {
                    if (stack.Count == 0 || stack.Pop() != bracketsMap[ch])           //проверка на соответсие скобок по словарю
                    {
                        return false;
                    }
                }
                else
                {
                    continue;
                }
            }
            return stack.Count == 0;
        }

        static MatchCollection FindSentenses(string str) {
            string pattern = @"[^.!?]+\.\s?[а-яa-z][^.!?]+|([^.!?\n:]+:(\s\d\..+)+;\s\d\..+.)|([^.!?\n:]+:\n)|(\d+\.\s[^.!?]+)|([А-Яа-я][^.?!]+)|([А-Яа-я][^.?!]+)|(\d[^\n]+)";

            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(str);

            return matches;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int number = 1;
            richTextBox3.Clear();
            foreach (Match match in FindSentenses(richTextBox2.Text))
            {
              
              richTextBox3.Text+="предложение №"+ number + ": "+ (match.Value.ToString().Trim()) + "\n";
                number += 1;
            }
        }

    }
}