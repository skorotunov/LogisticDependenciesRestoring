using LDR.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LDR.WebAPI.BLL
{
    public class AlgorithmLogic
    {
        public void A1(Function function)
        {
            var fn = function.Points.OrderByDescending(x => x.Value.Value).First(); //check for null
            
            if (fn.Key == 1)
            {
                //A
            }
            else
            {

            }
        }
    }
}
