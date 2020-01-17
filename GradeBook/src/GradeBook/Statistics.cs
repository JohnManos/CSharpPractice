namespace GradeBook
{
    public class Statistics
    {
        public double Lowest;
        public double Highest;
        public char Letter 
        { 
            get
            {
                // Csharp's newer switch syntax (i would just use if/else here but heck why not)
                switch(Average)
                {
                    case var g when g >= 90:
                        return 'A';
                    case var g when g >= 80:
                        return 'B';
                    case var g when g >= 70:
                        return 'C';
                    case var g when g >= 60:
                        return 'D';                        
                    default:
                        return 'F';
                }
            }
        }
        public double Sum;
        public int Count;
        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }

        public Statistics()
        {
            Lowest = double.MaxValue;
            Highest = double.MinValue;
            Sum = 0.0;
            Count = 0;
        }

        public void Add(double number)
        {
            Sum += number;
            Count++;
            if (number < Lowest)
            { 
                Lowest = number;
            }
            if (number > Highest)
            { 
                Highest = number;
            }
        }
    }
}