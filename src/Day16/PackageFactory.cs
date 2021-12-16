using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    public static class PackageFactory
    {
        public static IEnumerable<Package> GeneratePackages(string input)
        {
            List<Package> result = new List<Package>();
            while (input.Length > 7)
            {
                int offset = 0;
                int version = Convert.ToInt32(input.Substring(offset, 3), 2);
                offset += 3;
                int type = Convert.ToInt32(input.Substring(offset, 3), 2);
                offset += 3;
                if (type != 4)
                {
                    int lengthtype = input.Substring(offset, 1) == "1" ? 11 : 15;
                    offset += 1;
                    int subbitlenght = Convert.ToInt32(input.Substring(offset, lengthtype), 2);
                    offset += lengthtype;
                    if (lengthtype == 11)
                    {
                        subbitlenght = subLength(input.Substring(offset), subbitlenght);
                    }
                    string content = input.Substring(offset, subbitlenght);
                    result.Add(new OperatorPackage(version, type, lengthtype, subbitlenght, content));
                    input = input.Substring(offset + subbitlenght);
                }
                else
                {
                    string content = input.Substring(offset);
                    int index;
                    for (index = 0; index < content.Length-1; index+= 5)
                    {
                        if (content[index] == '0')
                            break;
                    }
                    string value = content.Substring(0, index+5);
                    result.Add(new ValuePackage(version, type, value));
                    input = input.Substring(offset + index + 5);
                }
            }
            return result;
        }

        private static int subLength(string content, int count)
        {
            int result = 0;
            for(int k = 0; k < count; k++)
            {
                int offset = 0;
                int version = Convert.ToInt32(content.Substring(offset, 3), 2);
                offset += 3;
                int type = Convert.ToInt32(content.Substring(offset, 3), 2);
                offset += 3;
                if (type != 4)
                {
                    int lengthtype = content.Substring(offset, 1) == "1" ? 11 : 15;
                    offset += 1;
                    int subbitlenght = Convert.ToInt32(content.Substring(offset, lengthtype), 2);
                    offset += lengthtype;
                    if (lengthtype == 11)
                    {
                        int sl = subLength(content.Substring(offset), subbitlenght);
                        content = content.Substring(offset + sl);
                        result += sl+offset;
                    }
                    else
                    {
                        result += offset + subbitlenght;
                        content = content.Substring(offset + subbitlenght);
                    }
                }
                else
                {
                    string c = content.Substring(offset);
                    int index;
                    for (index = 0; index < c.Length - 1; index += 5)
                    {
                        if (c[index] == '0')
                            break;
                    }
                    content = content.Substring(offset + index + 5);
                    result += offset + index + 5;
                }
            }
            return result;
        }
    }
}
