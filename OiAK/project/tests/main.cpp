#include <fstream>
#include <string>
using namespace std;

int main()
{
    long sizes[7]{100, 200, 500, 1000, 2000, 5000, 10000};
    unsigned long long matrix[7][7]{0};
    ifstream fin;
    for (uint8_t i = 0; i < 7; ++i)
    {
        for (uint8_t j = 0; j < 7; ++j)
        {
            fin.open(to_string(sizes[j]) + "x" + to_string(sizes[i]) + "_test.bmp_testTime.txt");
            if(fin.good())
                fin >> matrix[i][j];
            fin.close();
        }
    }
    ofstream fout("testSum.txt");
    for (uint8_t i = 0; i < 7; ++i)
    {
        for (uint8_t j = 0; j < 6; ++j)
        {
            fout << matrix[i][j] << "; ";
        }
        fout << matrix[i][6] << endl;
    }
    fout.close();
    return 0;
}