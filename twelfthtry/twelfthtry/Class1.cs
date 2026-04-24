using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace twelfthtry
{
    class BankCard
    {

        private string owner;
        private readonly int cardnum;
        private int limit;
        private static int countcards=0;
        private static HashSet<int> cards=new HashSet<int>();


        public BankCard(string owner,int number,  int limit)
        {
            this.owner = owner;
            this.cardnum = number;
            this.limit = limit;
            
            
            countcards++;

        }
        public BankCard(string owner, int number) 
        {
            this.owner= owner;
            this.cardnum = 0;



        }
        public static int Count() 
        { 
            return countcards;  
        }



        public void PrintCount()
        {

            Console.WriteLine(countcards);
        
        }
        public void SetLimit(int newLimit)
        {
            if (newLimit >= 0)
            {
                limit = newLimit;
            }
        }
        public int Limit
        {
            get { return limit; }
        }
    }

}
