using System;
using System.Threading.Tasks;

namespace Reinco
{
    public interface ISaveAndLoads
    {
        void SaveText(string filename, string text);
        string LoadText(string filename);

    }
}
