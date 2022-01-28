using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigNums
{
    [System.Serializable]
    public struct ScienceNum
    {
        //Should always be between 1 and 9.9999
        public float baseValue;
        public int eFactor;

        public static ScienceNum operator +(ScienceNum sn1, ScienceNum sn2)
        {
            //Bring the 2 numbers to the same power of 10.
            int factorDiff = sn1.eFactor - sn2.eFactor;
            //arbitrary limit, but if the difference in factors is more than 10^8, ignore the operation
            if (factorDiff >= 8)
                return sn1;

            sn2.baseValue /= Mathf.Pow(10, factorDiff);
            sn2.eFactor += factorDiff;

            //Add
            sn1.baseValue += sn2.baseValue;

            //If 0, then 0 can be returned now to avoid div/0 errors
            if (sn1.baseValue == 0)
                return sn1;

            //Convert resulting baseValue back to single digit range
            int eChange = Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(sn1.baseValue)));
            sn1.baseValue /= Mathf.Pow(10, eChange);
            sn1.eFactor += eChange;
            return sn1;
        }

        public static ScienceNum operator -(ScienceNum sn1, ScienceNum sn2)
        {
            //Bring the 2 numbers to the same power of 10.
            int factorDiff = sn1.eFactor - sn2.eFactor; //1
            //arbitrary limit, but if the difference in factors is more than 10^8, ignore the operation.
            if (factorDiff >= 8)
                return sn1;
            sn2.baseValue /= Mathf.Pow(10, factorDiff);
            sn2.eFactor += factorDiff;

            //Subtract
            sn1.baseValue -= sn2.baseValue;

            //If 0, then 0 can be returned now to avoid div/0 errors
            if (sn1.baseValue == 0)
                return sn1;

            //Convert resulting baseValue back to single digit range
            int eChange = Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(sn1.baseValue)));
            sn1.baseValue /= Mathf.Pow(10, eChange);
            sn1.eFactor += eChange;
            return sn1;
        }

        public static ScienceNum operator *(ScienceNum sn1, ScienceNum sn2)
        {
            sn1.baseValue *= sn2.baseValue;
            sn1.eFactor += sn2.eFactor;

            if (sn1.baseValue >= 10f)
            {
                sn1.eFactor += 1;
                sn1.baseValue /= 10;
            }

            return sn1;
        }

        public static ScienceNum operator /(ScienceNum sn1, ScienceNum sn2)
        {

            sn1.baseValue /= sn2.baseValue;
            sn1.eFactor -= sn2.eFactor;

            if (sn1.baseValue < 1f)
            {
                sn1.eFactor -= 1;
                sn1.baseValue *= 10;
            }

            return sn1;
        }

        public static bool operator <(ScienceNum sn1, ScienceNum sn2)
        {
            bool _sn1Smaller;

            if ((sn1.eFactor < sn2.eFactor) || ((sn1.baseValue < sn2.baseValue) && (sn1.eFactor <= sn2.eFactor)))
            {
                _sn1Smaller = true;
            }
            else
            {
                _sn1Smaller = false;
            }

            /*
            if (sn1.baseValue < sn2.baseValue)
            {
                _sn1Smaller = true;
            }
            else
            {
                _sn1Smaller = false;
            }
            if (sn1.eFactor <= sn2.eFactor)
            {
                _sn1Smaller = true;
            }
            else
            {
                _sn1Smaller = false;
            }*/

            return _sn1Smaller;
        }

        public static bool operator >(ScienceNum sn1, ScienceNum sn2)
        {
            bool _sn1Bigger;
            if ((sn1.eFactor > sn2.eFactor) || ((sn1.baseValue > sn2.baseValue) && (sn1.eFactor >= sn2.eFactor)))
            {
                _sn1Bigger = true;
            }
            else
            {
                _sn1Bigger = false;
            }
            /*

            if (sn1.baseValue > sn2.baseValue)
            {
                _sn1Bigger = true;
            }
            else
            {
                _sn1Bigger = false;
            }
            if (sn1.eFactor >= sn2.eFactor)
            {
                _sn1Bigger = true;
            }
            else
            {
                _sn1Bigger = false;
            }*/

            return _sn1Bigger;
        }

        public static bool operator >=(ScienceNum sn1, ScienceNum sn2)
        {
            bool _sn1BiggerEQ;
            if ((sn1.eFactor > sn2.eFactor) || ((sn1.baseValue >= sn2.baseValue) && (sn1.eFactor == sn2.eFactor)))
            {
                _sn1BiggerEQ = true;
            }
            else
            {
                _sn1BiggerEQ = false;
            }

            /*if (sn1.baseValue >= sn2.baseValue)
            {
                _sn1BiggerEQ = true;
            }
            else
            {
                _sn1BiggerEQ = false;
            }
            if (sn1.eFactor >= sn2.eFactor)
            {
                _sn1BiggerEQ = true;
            }
            else
            {
                _sn1BiggerEQ = false;
            }*/

            return _sn1BiggerEQ;
        }

        public static bool operator <=(ScienceNum sn1, ScienceNum sn2)
        {
            bool _sn1SmallerEQ;
            if ((sn1.eFactor < sn2.eFactor) || ((sn1.baseValue <= sn2.baseValue) && (sn1.eFactor == sn2.eFactor)))
            {
                _sn1SmallerEQ = true;
            }
            else
            {
                _sn1SmallerEQ = false;
            }

            /*
            if (sn1.eFactor <= sn2.eFactor)
            {
                _sn1SmallerEQ = true;
            }
            else
            {
                _sn1SmallerEQ = false;
            }*/

            return _sn1SmallerEQ;
        }

        public static bool operator ==(ScienceNum sn1, ScienceNum sn2)
        {
            bool _equal;
            if ((sn1.baseValue == sn2.baseValue) && (sn1.eFactor == sn2.eFactor))
            {
                _equal = true;
            }
            else
            {
                _equal = false;
            }

            return _equal;
        }

        public static bool operator !=(ScienceNum sn1, ScienceNum sn2)
        {
            bool _equal;
            if ((sn1.baseValue == sn2.baseValue) && (sn1.eFactor == sn2.eFactor))
            {
                _equal = true;
            }
            else
            {
                _equal = false;
            }

            return !_equal;
        }

        public float Conversion()
        {
            return baseValue * Mathf.Pow(10, eFactor);
        }

        public override string ToString()
        {
            String index = "";
            float baseVal = baseValue;
            int eFacVal = eFactor;

            if (eFactor == -1)
            {
                baseVal = baseValue / 10;
                eFactor = 0;
            }
            if (eFactor >= 3)
            {
                index = "k";
                eFacVal = eFactor - 3;
            }
            if (eFactor >= 6)
            {
                index = "M";
                eFacVal = eFactor - 6;
            }
            if (eFactor >= 9)
            {
                index = "G";
                eFacVal = eFactor - 9;
            }
            if (eFactor >= 12)
            {
                index = "T";
                eFacVal = eFactor - 12;
            }
            if (eFactor >= 15)
            {
                index = "P";
                eFacVal = eFactor - 15;
            }
            if (eFactor >= 18)
            {
                index = "E";
                eFacVal = eFactor - 18;
            }
            if (eFactor >= 21)
            {
                index = "Z";
                eFacVal = eFactor - 21;
            }
            if (eFactor >= 24)
            {
                index = "Y";
                eFacVal = eFactor - 24;
            }

            return String.Format("{0}{1}", Mathf.Ceil(baseVal*(Mathf.Pow(10,eFacVal))), index);
        }

        public static ScienceNum FromString(string str)
        {
            ScienceNum scienceNum = new ScienceNum();
            var split = str.Split('e');
            scienceNum.baseValue = Convert.ToSingle(split[0]);
            scienceNum.eFactor = Convert.ToInt32(split[1]);
            return scienceNum;
        }
    }
}