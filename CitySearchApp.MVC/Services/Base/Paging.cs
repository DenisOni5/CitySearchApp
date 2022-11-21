using System.Text;

namespace CitySearchApp.MVC.Services.Base
{
    public static class Paging
    {
        public static Pagin GetPagingDone(int thisPageNo, int totalCount, int pageSize, string pageName, string extraQstringToAdd)
        {

            int _pageSize = 10;

            Pagin pagin = new Pagin();
            int[] numbers = pagin.nums;

            string onPageRef;

            decimal allp = decimal.Divide(totalCount, pageSize);
            int roundedUp = (int)Math.Ceiling(allp);
            int allpages = Convert.ToInt32(roundedUp);


            int bprevPage = thisPageNo - 1;
            int bnextPage = thisPageNo + 1;

            string onPageSize = "";

            if (_pageSize != numbers[0])
            {
                onPageSize = "&onpage=" + pageSize;
            }
            onPageRef = "";

            StringBuilder strB = new StringBuilder(" ", 500);

            int current = thisPageNo;
            int last = allpages;
            int delta = 2;
            int left = current - delta;
            int right = current + delta + 1;

            List<int> range = new List<int>();
            List<object> rangeWithDots = new List<object>();

            int l = 0;

            for (int i = 1; i <= last; i++)
            {
                if (i == 1 || i == last || i >= left && i < right)
                {
                    range.Add(i);
                }
            }

            foreach (var item in range)
            {
                if (l > 0)
                {
                    if (item - l == 2)
                    {
                        rangeWithDots.Add(l + 1);
                    }
                    else if (item - l != 1)
                    {
                        rangeWithDots.Add("...");
                    }
                }
                rangeWithDots.Add(item);
                l = item;
            }

            if (last != 1)
            {
                foreach (var rangeItem in rangeWithDots)
                {
                    var isNumeric = int.TryParse(rangeItem.ToString(), out int n);

                    if (isNumeric)
                    {
                        if (thisPageNo != 1 && (Convert.ToInt32(rangeItem) == 1))
                        {

                            strB.Append("<li class='page-item'><a class='page-link' aria-label='Previous' href=\"" + pageName + bprevPage + extraQstringToAdd + onPageSize + onPageRef + "\" title=\"Go to Page " + bprevPage + "\">" + "<" + "</a></li>");
                        }
                    }
                    if (isNumeric)
                    {
                        if (Convert.ToInt32(rangeItem) != thisPageNo)
                        {
                            strB.Append("<li class='page-item'><a class='page-link' href=\"" + pageName + rangeItem + extraQstringToAdd + onPageSize + onPageRef + "\" title=\"Go to Page " + rangeItem + "\">" + rangeItem + "</a></li>");
                        }
                        else
                        {
                            strB.Append($"<li class='page-item active' aria-current='page'><a class='page-link' href='#'>{rangeItem}</a></li>");
                        }
                    }
                    else
                    {
                        strB.Append("<li class='page-item disabled'><span class='page-link'>...</span></li>");
                    }
                    if (isNumeric)
                    {
                        if ((Convert.ToInt32(rangeItem) == last) && (allpages != thisPageNo))
                        {
                            strB.Append("<li class='page-item'><a class='page-link' aria-label='Next' href=\"" + pageName + bnextPage + extraQstringToAdd + onPageSize + onPageRef + "\" title=\"Go to Page " + bnextPage + "\">" + ">" + "</a></li>");
                        }
                    }


                }
            }

            var result = new Pagin
            {
                totalcount = totalCount,
                totalpages = allpages,
                pageSize = pageSize,
                current = current,
                paginstring = $"<ul class='pagination'>{strB.ToString()}</ul>" //  + "<hr /> first: "+ firstpage + " last: " + lastpage + " allpages: " + allpages
            };
            return result;

        }
    }

    public class Pagin
    {
        public int totalcount;
        public int totalpages;
        public string paginstring;
        public int pageSize;
        public int current;
        public int[] nums = new int[4] { 10, 20, 50, 100 };
    }
}
