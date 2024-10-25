using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfTest.Source
{
    class OneClick
    {
        static long Firsttime;
        
        public static bool One_Click()
        {
            long CurrentTime = DateTime.Now.Ticks;
            if (CurrentTime - Firsttime < 4000000) // 0.4초 ( MS에서는 더블클릭 평균 시간을 0.4초로 보는거 같다.)
            {
                Firsttime = CurrentTime;   // 더블클릭 또는 2회(2회, 3회 4회...)클릭 시 실행되지 않도록 함
                return false;   // 더블클릭 됨
            }
            else
            {
                Firsttime = CurrentTime;   // 1번만 실행되도록 함
                return true;   // 더블클릭 아님
            }
        }
    }
}
