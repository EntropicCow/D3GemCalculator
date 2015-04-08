﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace D3GemCalculator
{
    public class Gem // the class that does all the work, methods are self-explainatory for the most part
    {

        int[] GemChart;
        int[] GoldChart;
        int[] DeathsBreathChart;

        public Gem() 
        { // this used to be 3 txt files read from assembly and stored in a multi-dimensional array, but this is more efficient
            GemChart = new int[18] {2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3};
            GoldChart = new int[18]{10, 25, 40, 55, 70, 85, 100, 5000, 10000, 20000, 30000, 50000, 75000, 100000, 200000, 300000, 400000, 500000};
            DeathsBreathChart = new int[18]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1};
        }

        private string CalculateGold(int typeused, int typewanted, int amountwanted)
        {
            BigInteger goldresult = 0;
            
            
            for (int i = typeused; i < typewanted; i++)
            {
                goldresult = goldresult * GemChart[i] + GoldChart[i];
            }


            goldresult = BigInteger.Multiply(goldresult, amountwanted);
            return goldresult.ToString("#,#") + " Gold needed.";
        }
        private string CalculateGemsNeeded(int typeused, int typewanted, int amountwanted, string gemtype)
        {
            BigInteger gemresult = 0;

            for (int i = typeused; i < typewanted; i++ )
            {
                gemresult = (gemresult > 0) ?  gemresult * GemChart[i] : gemresult + GemChart[i];
            }

               gemresult = BigInteger.Multiply(gemresult, amountwanted);
            return gemresult.ToString("#,#") + " " + gemtype + " needed.";
        }
        private string CalculateDeathsBreath(int typeused, int typewanted, int amountwanted)
        {
            BigInteger deathsbreathresult = 0;

            for (int i = typeused; i < typewanted; i++ )
            {         
              deathsbreathresult = (deathsbreathresult > 0) ? deathsbreathresult * DeathsBreathChart[i] + GemChart[i] : DeathsBreathChart[i];
            } // I am not sure why I did it like this, but it seems to work, so I left it alone.

            deathsbreathresult = BigInteger.Multiply(deathsbreathresult, amountwanted);
            if (deathsbreathresult > 0)
                return deathsbreathresult.ToString("#,#") + " Death's Breath needed."; 
            else
                return ""; //no need to render text for death's breath output if none are needed

        }
        private string CalculateTime(int typeused, int typewanted, int amountwanted)
        { // fancy time calculations
            long seconds = 0;
            long minutes = 0;
            long hours = 0;
            long timepercombine = 3; //actually takes about 2.5 seconds but I rounded up to 3 for simplicity and accounting for player input
            long timeresult = 0;
            string Seconds, Minutes, Hours;

            for (int i = typeused; i < typewanted; i++ )
            {
                timeresult = (timeresult >0) ? timepercombine + timeresult * GemChart[i] : timepercombine;
            } //it works, that's all I know.
            timeresult = timeresult * amountwanted;
            minutes = Math.DivRem(timeresult, 60, out seconds); //some modulus/remainder division to get minutes and seconds
            hours = Math.DivRem(minutes, 60, out minutes); // same for hours/minutes
            Seconds = seconds.ToString(); //turn those longints into strings for final processing
            Minutes = minutes.ToString();
            Hours = hours.ToString("#,#");

            return("Total Time: " + Hours.PadLeft(2, '0') + ":" + Minutes.PadLeft(2, '0') + ":" + Seconds.PadLeft(2, '0')); //padded output
        }

        public string[] SanityCheck(int typeused, int typewanted, string amountwanted, object gemtypeused, object gemtypewanted)
        { // new and improved sanity check code. replaces the old error checking in buttonclick event method thing.
            int quantity = 0;
            string[] output;

             if (typeused == -1 && typewanted == -1)
            {
                output = new string[1];
                output[0] = "Both gem types are blank.";
                return output;
            }
            else if (typeused == -1)
            {
                output = new string[1];
                output[0] = "Gemtype used not set.";
                return output;
            }
            else if (typewanted == -1)
            {
                output = new string[1];
                output[0] = "Gemtype wanted not set.";
                return output;
            }
            else if (typeused == typewanted)
            {
                output = new string[1];
                output[0] = "Both Gemtypes are the same.";
                return output;
            }
            else if (typeused > typewanted)
            {
                output = new string[1];
                output[0] = "Gem used is greather than Gem wanted.";
                return output;
            }
            else if (amountwanted == "" || amountwanted == "0")
            {
                output = new string[1];
                output[0] = "Please enter a number.";
                return output;
            }
            else if (!int.TryParse(amountwanted, out quantity))
            {
                output = new string[1];
                output[0] = amountwanted + " is not a number.";
                return output;
            }
            else
                output = new string[6];
            output[0] = CalculateGemsNeeded(typeused, typewanted, quantity, gemtypeused.ToString());
            output[1] = CalculateGold(typeused, typewanted, quantity);
            output[2] = CalculateDeathsBreath(typeused, typewanted, quantity);
            output[3] = CalculateTime(typeused, typewanted, quantity);
            output[4] = "To make " + quantity + " " + gemtypewanted.ToString() + ".";
            output[5] = "Using " + gemtypeused.ToString() + ".";
            return output;
        }
    }
}